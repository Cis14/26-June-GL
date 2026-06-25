namespace Creatio.Copilot
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion.Responses;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion.Requests;
	using Terrasoft.Configuration.GenAI;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.FeatureToggling;
	using Terrasoft.Common;
	using Terrasoft.Core.Applications.GenAI;
	using System.Security;
	using Terrasoft.Core;

	internal abstract class BaseCopilotExecutor
	{

		#region Fields: Private

		private readonly IGenAICompletionServiceProxy _completionService;
		private readonly ILlmModelResolver _llmModelResolver;
		private readonly ICopilotContextBuilder _contextBuilder;
		private readonly ICopilotRequestLogger _requestLogger;
		private readonly ICopilotHyperlinkUtils _hyperlinkUtils;
		private readonly IToolFilterResolver _toolFilterResolver;

		#endregion

		#region Fields: Protected

		protected readonly UserConnection _userConnection;
		protected readonly IIntentPromptBuilder _intentPromptBuilder;
		protected readonly ICopilotIntentsStorage _intentsStorage;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseCopilotExecutor"/>
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="completionService">GenAI Completion service.</param>
		/// <param name="llmModelResolver">An instance of <see cref="ILlmModelResolver"/></param>
		/// <param name="contextBuilder">Copilot context builder.</param>
		/// <param name="requestLogger">Copilot request logger.</param>
		/// <param name="hyperlinkUtils">An instance of <see cref="ICopilotHyperlinkUtils"/>
		/// used for handling hyperlink-related utilities.</param>
		/// <param name="intentPromptBuilder">An instance of <see cref="IIntentPromptBuilder"/></param>
		/// <param name="intentsStorage"></param>
		/// <param name="toolFilterResolver">An instance of <see cref="IToolFilterResolver"/></param>
		protected BaseCopilotExecutor(UserConnection userConnection, IGenAICompletionServiceProxy completionService,
				ILlmModelResolver llmModelResolver, ICopilotContextBuilder contextBuilder,
				ICopilotRequestLogger requestLogger, ICopilotHyperlinkUtils hyperlinkUtils,
				IIntentPromptBuilder intentPromptBuilder, ICopilotIntentsStorage intentsStorage,
				IToolFilterResolver toolFilterResolver) {
			_userConnection = userConnection;
			_completionService = completionService;
			_llmModelResolver = llmModelResolver;
			_contextBuilder = contextBuilder;
			_requestLogger = requestLogger;
			_hyperlinkUtils = hyperlinkUtils;
			_toolFilterResolver = toolFilterResolver;
			_intentPromptBuilder = intentPromptBuilder;
			_intentsStorage = intentsStorage;
		}

		#endregion

		#region Methods: Private

		private ChatCompletionRequest ApplyLlmModelToRequest(
			ChatCompletionRequest completionRequest, CopilotSession session) {
			if (Features.GetIsDisabled<Terrasoft.Configuration.GenAI.GenAIFeatures.MultiLlmSupport>()) {
				return completionRequest;
			}
			var intentId = session.CurrentIntentId ?? session.RootIntentId;
			LlmModelReference modelReference = _llmModelResolver.ResolveForIntent(intentId);
			string llmModelCode = modelReference?.Code;
			if (!string.IsNullOrEmpty(llmModelCode)) {
				completionRequest.Model = llmModelCode;
			}
			return completionRequest;
		}

		private static IEnumerable<ChatChoiceResponse> GetResponsesWithMessage(
			ChatCompletionResponse completionResponse) {
			return completionResponse.Choices.Where(response => response.Message.Content.IsNotNullOrEmpty());
		}

		#endregion

		#region Methods: Protected

		protected async Task<(DateTime? start, DateTime? end, ChatCompletionResponse response)> ProcessCompletionRequest(
			ChatCompletionRequest request, bool sync = true, CancellationToken token = default) {
			DateTime? start = DateTime.Now;
			ChatCompletionResponse response;
			if (sync) {
				response = _completionService.ChatCompletion(request);
			} else {
				response = await _completionService.ChatCompletionAsync(request, token).ConfigureAwait(false);
			}
			DateTime? end = DateTime.Now;
			return (start, end, response);
		}

		protected ChatCompletionRequest CreateCompletionRequest(CopilotSession session,
				CopilotToolContext copilotToolContext, CopilotContext copilotContext = null) {
			List<ChatMessage> messages = CreateCompletionMessages(session, copilotToolContext, copilotContext);
			var completionRequest = new ChatCompletionRequest {
				Messages = messages,
				Tools = copilotToolContext?.Tools ?? Array.Empty<ToolDefinition>(),
				Temperature = null,
				MaxOutputTokens = session.MaxOutputTokens
			};
			return ApplyLlmModelToRequest(completionRequest, session);
		}

		protected static List<CopilotMessage> GetAssistantMessagesWithoutToolCalls(
			ChatCompletionResponse completionResponse) {
			List<CopilotMessage> assistantMessages = GetResponsesWithMessage(completionResponse)
				.Select(response => new CopilotMessage(response.Message, skipToolCalls: true))
				.ToList();
			return assistantMessages;
		}

		protected static (string errorMessage, string errorCode) GetErrorInfo(Exception e) {
			if (e is GenAIException genAiException) {
				return (genAiException.RawError, genAiException.ErrorCode);
			}
			if (e is SecurityException) {
				return (e.Message, CopilotServiceErrorCode.InsufficientPermissions);
			}
			return (e.Message, CopilotServiceErrorCode.UnknownError);
		}

		protected Guid SaveRequestInfo(DateTime? start, DateTime? end, UsageResponse usage, string error, bool isFailed) {
			start = start ?? DateTime.Now;
			end = end ?? DateTime.Now;
			var duration = (long)(end - start).Value.TotalMilliseconds;
			var requestInfo = new CopilotRequestInfo {
				StartDate = start.Value,
				Error = error,
				TotalActions = usage?.TotalActions ?? 0,
				TotalTokens = usage?.TotalTokens ?? 0,
				PromptTokens = usage?.PromptTokens ?? 0,
				CompletionTokens = usage?.CompletionTokens,
				Duration = duration,
				IsFailed = isFailed
			};
			return _requestLogger.SaveCopilotRequest(requestInfo);
		}

		protected IEnumerable<CopilotIntentSchema> FilterSystemIntents(CopilotSession session,
				IEnumerable<CopilotIntentSchema> systemIntents) {
			IEnumerable<CopilotIntentSchema> intents = systemIntents ?? Enumerable.Empty<CopilotIntentSchema>();
			if (session?.FilterDescriptors == null || !session.FilterDescriptors.Any()) {
				return intents;
			}
			if (Features.GetIsDisabled("GenAIFeatures.EnableIntentFiltering")) {
				return intents;
			}
			IEnumerable<IAIToolFilter> filters = _toolFilterResolver?.Resolve(session.FilterDescriptors) ??
				Enumerable.Empty<IAIToolFilter>();
			return intents.Where(intent => {
				var context = new AIIntentFilterContext(intent);
				return filters.All(filter => filter?.ShouldIncludeIntent(context) != false);
			});
		}

		protected void AdjustSessionSystemIntentPromptUsingSystemIntents(CopilotSession copilotSession) {
			AdjustSessionSystemIntentPrompt(copilotSession,
				() => FilterSystemIntents(copilotSession, _intentsStorage.SystemIntents));
		}

		protected void AdjustSessionSystemIntentPrompt(CopilotSession copilotSession, Func<IEnumerable<CopilotIntentSchema>> getSystemIntents) {
			var intentId = copilotSession.RootIntentId;
			if (intentId.IsNullOrEmpty()) {
				return;
			}
			if (copilotSession.Messages.Any(copilotMessage => copilotMessage.IsFromSystemIntent)) {
				return;
			}
			string baseApplicationUrl = _hyperlinkUtils.GetBaseApplicationUrl();
			var parameters = new Dictionary<string, object> {
				{ "BaseAppUrl", baseApplicationUrl }
			};
			var systemIntents = getSystemIntents();
			if (Features.GetIsDisabled<Terrasoft.Configuration.GenAI.GenAIFeatures
					.SplitSystemIntentsIntoSeparateMessages>()) {
				var separator = Environment.NewLine + Environment.NewLine;
				var prompt = systemIntents.Select(intent => 
					_intentPromptBuilder.GenerateIntentPrompt(intent, parameters)).ConcatIfNotEmpty(separator);
				var message = CopilotMessage.FromSystem(prompt);
				message.IsFromSystemIntent = true;
				copilotSession.AddMessage(message);
				return;
			}
			foreach (var systemIntent in systemIntents) {
				var prompt = $"# {systemIntent.IntentDescription}" + Environment.NewLine +
					_intentPromptBuilder.GenerateIntentPrompt(systemIntent, parameters);
				var message = CopilotMessage.FromSystem(prompt);
				message.IsFromSystemIntent = true;
				copilotSession.AddMessage(message);
			}
		}

		protected void AddContextCompletionMessage(CopilotContext copilotContext, List<ChatMessage> messages) {
			if (copilotContext == null || copilotContext.Parts.Count == 0) {
				return;
			}
			string contextContent = _contextBuilder.BuildMessageContent(copilotContext);
			if (contextContent.IsNullOrEmpty()) {
				return;
			}
			var contextMessage = CopilotMessage.FromSystem(contextContent);
			var completionMessage = contextMessage.ToCompletionApiMessage();
			var lastUserMessageIndex = messages.FindLastIndex(m => m.Role == CopilotMessageRole.User);
			messages.Insert(lastUserMessageIndex == -1 ? 0 : lastUserMessageIndex, completionMessage);
		}

		protected CopilotIntentSchema GetCurrentIntent(CopilotSession session) {
			if (session.CurrentIntentId.IsNullOrEmpty()) {
				return null;
			}
			return _intentsStorage.FindSchemaByUId(session.CurrentIntentId.Value);
		}

		protected abstract List<ChatMessage> CreateCompletionMessages(CopilotSession session,
			CopilotToolContext copilotToolContext, CopilotContext copilotContext);

		#endregion

	}
}



