namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using Common.Logging;
	using Creatio.FeatureToggling;
	using Newtonsoft.Json;
	using Terrasoft.Common;
	using Terrasoft.Configuration.GenAI;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion.Requests;
	using ConfGenAIFeatures = Terrasoft.Configuration.GenAI.GenAIFeatures;

	#region Calss: UnifiedSummarizationStrategy

	/// <summary>
	/// Provides a unified strategy for summarizing messages within a copilot session.
	/// </summary>
	/// <remarks>
	/// This class is responsible for determining whether a summarization is needed based on session data
	/// and executing the summarization process when required. It utilizes various injected dependencies,
	/// such as user connection, AI completion service, session management, message channel sender,
	/// and specified summarization settings, to perform its operations effectively.
	/// </remarks>
	[DefaultBinding(typeof(ISummarizationStrategy))]
	public class UnifiedSummarizationStrategy : ISummarizationStrategy {

		#region Fields: Private

		private static readonly ILog _log = LogManager.GetLogger("Copilot");
		private readonly UserConnection _userConnection;
		private readonly IGenAICompletionServiceProxy _completionService;
		private readonly ICopilotSessionManager _sessionManager;
		private readonly ICopilotMsgChannelSender _msgChannelSender;
		private readonly IUnifiedSummarizationSettings _settings;

		#endregion

		#region Constructors: Public

		public UnifiedSummarizationStrategy(UserConnection userConnection,
				IGenAICompletionServiceProxy completionService,
				ICopilotSessionManager sessionManager,
				ICopilotMsgChannelSender msgChannelSender,
				IUnifiedSummarizationSettings settings) {
			_userConnection = userConnection;
			_completionService = completionService;
			_sessionManager = sessionManager;
			_msgChannelSender = msgChannelSender;
			_settings = settings;
		}

		#endregion

		#region Methods: Private

		private List<CopilotMessage> GetMessagesForSummarization(CopilotSession session) {
			var result = session.Messages.Where(x => 
				!x.IsFromSystemIntent &&
				!x.IsFromSystemPrompt &&
				x.IntentId != session.CurrentIntentId &&
				x.SummarizedById.IsNullOrEmpty());
			if (Features.GetIsDisabled<ConfGenAIFeatures.IncludeSystemMessagesInSummarization>()) {
				return result.Where(x => x.Role != CopilotMessageRole.System).ToList();
			}
			return result.ToList();
		}

		private ChatMessage BuildPromptMessage() =>
			CopilotMessage.FromSystem(_settings.PromptTemplate).ToCompletionApiMessage();

		private List<ChatMessage> BuildMessagesForRequest(List<CopilotMessage> messagesToSummarize) {
			var promptMessage = BuildPromptMessage();
			var historyMessages = messagesToSummarize.Select(m => m.ToCompletionApiMessage());
			var jsonHistoryMessages = JsonConvert.SerializeObject(historyMessages, Formatting.None);
			var userMessage = CopilotMessage.FromUser(jsonHistoryMessages).ToCompletionApiMessage();
			return new List<ChatMessage> { promptMessage, userMessage };
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Determines whether summarization is needed for the given Copilot session.
		/// </summary>
		/// <param name="session">The Copilot session containing messages and context to evaluate for summarization.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether summarization is needed.</returns>
		public Task<bool> NeedSummarizeAsync(CopilotSession session, CancellationToken cancellationToken) {
			if (Features.GetIsDisabled<ConfGenAIFeatures.SummarizeCopilotSessionMessages>()) {
				_log.Debug($"Summarization feature is disabled, skipping for session {session.Id}");
				return Task.FromResult(false);
			}
			if (session.Messages.All(m => m.IntentId == session.CurrentIntentId 
					|| m.IsFromSystemIntent || m.IsFromSystemPrompt)) {
				_log.Debug($"No other intents found in session {session.Id}, summarization not needed.");
				return Task.FromResult(false);
			}
			if (!session.Messages.Any(m => m.Role == CopilotMessageRole.User && m.IntentId == session.CurrentIntentId)) {
				_log.Debug($"User message not found in current intent for session {session.Id}, summarization not needed.");
				return Task.FromResult(false);
			}  
			if (session.Messages.Select(m => m.IntentId).Distinct().Count() < _settings.MinIntentsCountInSession) {
				_log.Debug($"Not enough unique intents found in session {session.Id}, summarization not needed.");
				return Task.FromResult(false);
			}
			var totalCharactersOfSummarization = GetMessagesForSummarization(session).Sum(m => m.Content?.Length ?? 0);
			if (totalCharactersOfSummarization <= _settings.MinRequiredCharacters) {
				_log.Debug($"Not enough content length ({totalCharactersOfSummarization}) for session {session.Id}, summarization not needed.");
				return Task.FromResult(false);
			}
			var messagesInCurrentIntentCount = session.Messages.Count(m =>
				m.IntentId == session.CurrentIntentId
				&& m.Role == CopilotMessageRole.Assistant
				&& m.ToolCalls.Count == 0);
			if (messagesInCurrentIntentCount < _settings.MinRequiredMessages) {
				_log.Debug($"Not enough messages in current intent ({messagesInCurrentIntentCount}) for session {session.Id}, summarization not needed.");
				return Task.FromResult(false);
			}
			return Task.FromResult(true);
		}

		/// <summary>
		/// Performs summarization of messages within the provided Copilot session asynchronously.
		/// </summary>
		/// <param name="session">The Copilot session containing the messages to be summarized.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests during the operation.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		//
		public async Task SummarizeAsync(CopilotSession session, CancellationToken cancellationToken) {
			var messagesToSummarize = GetMessagesForSummarization(session);
			if (!messagesToSummarize.Any()) {
				_log.Debug($"No messages to summarize for session {session.Id}");
				return;
			}
			_log.Info($"Starting unified summarization for {messagesToSummarize.Count} messages in session {session.Id}");
			try {
				var lastSummarizedMessage = messagesToSummarize.OrderBy(x => x.Date).Last();
				var request = new ChatCompletionRequest { Messages = BuildMessagesForRequest(messagesToSummarize) };
				var options = new ChatCompletionRequestOptions {
					SkipValidation = true
				};
				var response = await _completionService.ChatCompletionAsync(request, cancellationToken, options);
				var responseContent = response.Choices.FirstOrDefault()?.Message.Content;
				if (string.IsNullOrWhiteSpace(responseContent)) {
					_log.Warn("Empty response received for unified summarization");
					return;
				}
				var summaryMessage = CopilotMessage.FromSystem(responseContent, lastSummarizedMessage.CreatedOnTicks);
				summaryMessage.IsSummary = true;
				foreach (var msg in messagesToSummarize) {
					msg.SummarizedById = summaryMessage.Id;
					msg.IsSaved = false;
				}
				_log.Info($"Unified summarization completed for session {session.Id}");
				session.AddSummaryMessage(summaryMessage);
				_sessionManager.Update(session, null);
				_msgChannelSender.SendSessionProgress(CopilotSessionProgress.Create(_userConnection, session,
					CopilotSessionProgressStates.SkillMessageSummarized, summaryMessage.Content),
					_userConnection.CurrentUser.Id);
				_log.Info($"Completed unified summarization for session {session.Id}");
			} catch (OperationCanceledException) {
				_log.Warn("Unified summarization cancelled");
				throw;
			} catch (Exception ex) {
				_log.Error($"Unified summarization failed: {ex.Message}", ex);
				throw;
			}
		}

		#endregion

	}

	#endregion

}
