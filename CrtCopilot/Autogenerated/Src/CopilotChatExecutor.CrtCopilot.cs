namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using FeatureToggling;
	using Terrasoft.Common;
	using Terrasoft.Common.Threading;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core;
	using Newtonsoft.Json;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion.Responses;
	using Terrasoft.Configuration.GenAI;
	using Common.Logging;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion.Requests;

	#region Class: CopilotChatExecutor

	/// <summary>
	/// Copilot chat executor.
	/// </summary>
	/// <inheritdoc cref="Creatio.Copilot.BaseCopilotExecutor"/>
	/// <inheritdoc cref="Creatio.Copilot.ICopilotChatExecutor"/>
	[DefaultBinding(typeof(ICopilotChatExecutor))]
	internal class CopilotChatExecutor : BaseCopilotExecutor, ICopilotChatExecutor
	{

		#region Constants: Private

		private const string CanRunCopilotOperation = "CanRunCreatioAI";

		#endregion

		#region Fields: Private

		private readonly ILog _log = LogManager.GetLogger("Copilot");
		private readonly ICopilotSessionManager _sessionManager;
		private readonly ICopilotToolProcessor _toolProcessor;
		private readonly ICopilotLinkValidator _linkValidator;
		private readonly IIntentToolExecutorFactory _intentToolExecutorFactory;
		private readonly ICopilotMessageConfirmationHandler _confirmationHandler;
		private readonly ICopilotMsgChannelSender _copilotMsgChannelSender;
		private readonly ICopilotSessionResponseDispatcher _sessionDispatcher;
		private readonly ICopilotDocumentManager _documentManager;
		private readonly IKnwPromptBuilder _knwPromptBuilder;
		private readonly ICopilotWorkflowService _workflowService;
		private readonly ICopilotToolContextBuilder _toolContextBuilder;
		private readonly ICopilotBoundedAgentUtils _boundedAgentUtils;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotChatExecutor"/>
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="copilotSessionManager">Copilot session manager.</param>
		/// <param name="intentPromptBuilder">An instance of <see cref="IIntentPromptBuilder"/></param>
		/// <param name="toolProcessor">Copilot tool processor.</param>
		/// <param name="completionService">GenAI Completion service.</param>
		/// <param name="linkValidator">An instance of <see cref="ICopilotLinkValidator"/>.</param>
		/// <param name="intentToolExecutorFactory">An instance of <see cref="IIntentToolExecutorFactory"/></param>
		/// <param name="confirmationHandler">An instance of</param>
		/// <param name="copilotMsgChannelSender">Copilot message sender.</param>
		/// <param name="sessionDispatcher">An instance of <see cref="ICopilotSessionResponseDispatcher"/></param>
		/// <param name="llmModelResolver">An instance of <see cref="ILlmModelResolver"/></param>
		/// <param name="contextBuilder">Copilot context builder.</param>
		/// <param name="requestLogger">Copilot request logger.</param>
		/// <param name="hyperlinkUtils">An instance of <see cref="ICopilotHyperlinkUtils"/>
		/// used for handling hyperlink-related utilities.</param>
		/// <param name="documentManager">An instance of <see cref="ICopilotDocumentManager"/></param>
		/// <param name="knwPromptBuilder">An instance of <see cref="knwPromptBuilder"/></param>
		/// <param name="workflowService">An instance of <see cref="ICopilotWorkflowService"/></param>
		/// <param name="toolContextBuilder">An instance of <see cref="ICopilotToolContextBuilder"/></param>
		/// <param name="intentsStorage">An instance of <see cref="ICopilotIntentsStorage"/></param>
		/// <param name="boundedAgentUtils">An instance of <see cref="ICopilotBoundedAgentUtils"/></param>
		/// <param name="toolFilterResolver">An instance of <see cref="IToolFilterResolver"/></param>
		public CopilotChatExecutor(UserConnection userConnection, ICopilotSessionManager copilotSessionManager,
				IIntentPromptBuilder intentPromptBuilder, ICopilotToolProcessor toolProcessor,
				IGenAICompletionServiceProxy completionService, ICopilotLinkValidator linkValidator,
				IIntentToolExecutorFactory intentToolExecutorFactory, ICopilotMessageConfirmationHandler confirmationHandler,
				ICopilotMsgChannelSender copilotMsgChannelSender, ICopilotSessionResponseDispatcher sessionDispatcher,
				ILlmModelResolver llmModelResolver, ICopilotContextBuilder contextBuilder,
				ICopilotRequestLogger requestLogger, ICopilotHyperlinkUtils hyperlinkUtils,
				ICopilotDocumentManager documentManager, IKnwPromptBuilder knwPromptBuilder,
				ICopilotWorkflowService workflowService, ICopilotToolContextBuilder toolContextBuilder,
				ICopilotIntentsStorage intentsStorage, ICopilotBoundedAgentUtils boundedAgentUtils,
				IToolFilterResolver toolFilterResolver)
					: base(userConnection, completionService, llmModelResolver,
						contextBuilder, requestLogger, hyperlinkUtils,
						intentPromptBuilder, intentsStorage, toolFilterResolver) {
			_sessionManager = copilotSessionManager;
			_toolProcessor = toolProcessor;
			_linkValidator = linkValidator;
			_intentToolExecutorFactory = intentToolExecutorFactory;
			_confirmationHandler = confirmationHandler;
			_copilotMsgChannelSender = copilotMsgChannelSender;
			_sessionDispatcher = sessionDispatcher;
			_knwPromptBuilder = knwPromptBuilder;
			_documentManager = documentManager;
			_workflowService = workflowService;
			_toolContextBuilder = toolContextBuilder;
			_boundedAgentUtils = boundedAgentUtils;
		}

		#endregion

		#region Methods: Private

		private bool TryFindTriggeredIntent(CopilotSession session, List<Guid> rootIntentIds,
				out IToolExecutionTrigger toolExecutionTrigger, out Guid? triggeredIntentId) {
			triggeredIntentId = null;
			toolExecutionTrigger = null;
			if (rootIntentIds == null) {
				return false;
			}
			if (rootIntentIds.Count != 1) {
				return false;
			}
			Guid intentId = rootIntentIds[0];
			if (session.CurrentIntentId == intentId || session.RootIntentId == intentId) {
				return false;
			}
			IEnumerable<CopilotIntentSchema> agents = _intentsStorage.FindAgents();
			CopilotIntentSchema agent = agents.FirstOrDefault(a => a.UId == intentId);
			if (agent == null) {
				return false;
			}
			toolExecutionTrigger = _intentToolExecutorFactory.CreateTrigger(agent);
			triggeredIntentId = agent.UId;
			return true;
		}

		private bool TryHandleConfirmationToolCalls(CopilotMessage userMessage, CopilotSession session,
				bool workflowExecuting) {
			if (!session.CurrentIntentId.HasValue || !session.Messages.Any(CopilotExtensions.IsPendingConfirmation)) {
				return false;
			}
			if (workflowExecuting) {
				_workflowService.HandleConfirmation(session, userMessage.Content);
				return true;
			}
			CopilotToolContext copilotToolContext = _toolContextBuilder.BuildChatSessionToolContext(session);
			List<CopilotMessage> messages =
				_confirmationHandler.HandleConfirmedToolCalls(userMessage, session, copilotToolContext);
			AdjustSessionOnToolCallsCompleted(session, messages);
			session.CleanToolCallsWithoutTools();
			return true;
		}

		private void AdjustSessionOnToolCallsCompleted(CopilotSession session, List<CopilotMessage> toolMessages) {
			if (toolMessages.IsNullOrEmpty()) {
				return;
			}
			session.AddMessages(toolMessages);
			AdjustSessionSystemIntentPromptUsingSystemIntents(session);
			_boundedAgentUtils.EnsureBoundedAgentAssigned(session);
		}

		private void ProcessSessionCycle(CopilotSession session, CopilotContext copilotContext = null) {
			lock (session) {
				DateTime? startDate = null, endDate = null;
				ChatCompletionResponse completionResponse = null;
				var errorMessage = string.Empty;
				CopilotToolContext copilotToolContext = _toolContextBuilder.BuildChatSessionToolContext(session);
				ChatCompletionRequest completionRequest = CreateCompletionRequest(session, copilotToolContext, copilotContext);
				var isFailed = false;
				try {
					SendMessagesToClient(session);
					_copilotMsgChannelSender.SendSessionProgress(CopilotSessionProgress.Create(_userConnection, session,
						CopilotSessionProgressStates.WaitingForAssistantMessage), session.UserId);
					LogCompletionRequest(session, completionRequest, CopilotSessionProgressStates.RequestSending);
					(startDate, endDate, completionResponse) = ProcessCompletionRequest(completionRequest)
						.GetAwaiter()
						.GetResult();
					LogCompletionRequest(session, completionResponse, CopilotSessionProgressStates.ResponseReceived);
					HandleCompletionResponse(completionResponse, session);
				} catch (Exception e) {
					(errorMessage, _) = GetErrorInfo(e);
					isFailed = true;
					throw;
				} finally {
					UsageResponse usage = completionResponse?.Usage;
					Guid requestId = SaveRequestInfo(startDate, endDate, usage, errorMessage, isFailed);
					_sessionManager.Update(session, requestId);
				}
				_sessionDispatcher.DispatchAsync(session).GetAwaiter().GetResult();
				List<CopilotMessage> messages = _toolProcessor.HandleToolCalls(completionResponse, session, copilotToolContext);
				HandleToolCallsOnCompleted(messages, session, out bool sendSessionRequired);
				if (sendSessionRequired) {
					ProcessSessionCycle(session, copilotContext);
				}
			}
		}

		private void HandleToolCallsOnCompleted(List<CopilotMessage> toolMessages, CopilotSession session,
				out bool sendSessionRequired) {
			sendSessionRequired = false;
			AdjustSessionOnToolCallsCompleted(session, toolMessages);
			if (IsWorkflowExecuting(session)) {
				return;
			}
			if (toolMessages.IsNullOrEmpty() || toolMessages.IsAllToolsShouldOmitAssistantResponse()) {
				SendMessagesToClient(session);
				_sessionManager.Update(session, null);
				return;
			}
			sendSessionRequired = true;
		}

		private void LogCompletionRequest(CopilotSession session, object completionObject,
				CopilotSessionProgressStates state) {
			if (!Features.GetIsEnabled<Terrasoft.Configuration.GenAI.GenAIFeatures.LogCreatioAIRequest>()) {
				return;
			}
			string json = JsonConvert.SerializeObject(completionObject, Formatting.Indented);
			_copilotMsgChannelSender.SendSessionProgress(CopilotSessionProgress.Create(_userConnection, session, state,
				json), _userConnection.CurrentUser.Id);
			_log.Debug($"Completion API - {state.ToString()}: {json}");
		}

		private void HandleCompletionResponse(ChatCompletionResponse completionResponse, CopilotSession session) {
			if (completionResponse?.Choices == null) {
				return;
			}
			List<CopilotMessage> assistantMessages = GetAssistantMessagesWithoutToolCalls(completionResponse);
			AssignWorkflowRootIntentIfNeeded(session, assistantMessages);
			assistantMessages.ForEach(message => {
				message.Content = _linkValidator.ValidateLinks(session, message.Content, isApi: false);
			});
			session.AddMessages(assistantMessages);
			SendMessagesToClient(session);
		}

		private void SendMessagesToClient(CopilotSession copilotSession) {
			var messagesToSend = GetMessagesToSend(copilotSession);
			if (messagesToSend.Count == 0) {
				return;
			}
			messagesToSend.ForEach(message => message.RootIntentCaption =
				_intentsStorage.GetIntentCaptionByIntentId(message.RootIntentId));
			_copilotMsgChannelSender.SendMessages(new CopilotChatPart(messagesToSend,
				new BaseCopilotSession(copilotSession)));
			messagesToSend.ForEach(message => message.IsSentToClient = true);
		}

		private List<BaseCopilotMessage> GetMessagesToSend(CopilotSession copilotSession) {
			return copilotSession.Messages
				.Where(message => !message.IsSentToClient && !message.IsFromSystemPrompt && !message.TruncateOnSave)
				.Cast<BaseCopilotMessage>().ToList();
		}

		private CopilotMessage GetAgentsDescriptionMessage(IEnumerable<CopilotIntentSchema> intents) {
			if (intents == null) {
				return null;
			}
			var agentsWithSubIntents = intents
				.Where(i => i.Type == CopilotIntentType.Agent)
				.Select(agent => {
					var subIntents = _intentsStorage.GetSubIntents(agent.UId).Select(i => new {
						Caption = i.Caption.Value,
						i.Name
					}).ToList();
					return new {
						agent.Name,
						SubIntents = subIntents
					};
				})
				.Where(x => x.SubIntents.Any())
				.ToDictionary(x => x.Name, x => x.SubIntents);
			if (agentsWithSubIntents.Any()) {
				string message = JsonConvert.SerializeObject(agentsWithSubIntents, Formatting.None);
				return new CopilotMessage(message, CopilotMessageRole.System);
			}
			return null;
		}

		private void AddAgentsDescriptionCompletionMessage(CopilotSession session,
			CopilotToolContext copilotToolContext, List<ChatMessage> messages) {
			if (_boundedAgentUtils.IsAgentBounded(session)) {
				return;
			}
			if (copilotToolContext == null) {
				return;
			}
			CopilotMessage agentsDescriptionMessage = GetAgentsDescriptionMessage(copilotToolContext.Intents);
			if (agentsDescriptionMessage != null) {
				messages.Insert(1, agentsDescriptionMessage.ToCompletionApiMessage());
			}
		}

		private bool TryHandleClarificationToolCalls(CopilotMessage userMessage, CopilotSession session, bool workflowExecuting) {
			if (!workflowExecuting) {
				return false;
			}
			_workflowService.HandleClarification(session, userMessage.Content);
			return true;
		} 

		private void AssignWorkflowRootIntentIfNeeded(CopilotSession session, List<CopilotMessage> assistantMessages) {
			if (assistantMessages.IsNullOrEmpty()) {
				return;
			}

			var lastMessage = session.Messages.LastOrDefault();
			var isFromWorkflow = lastMessage?.IsFromWorkflow();
			if (isFromWorkflow == true) {
				assistantMessages.ForEach(message => { message.RootIntentId = lastMessage.RootIntentId; });
			}
		}

		private void UpdateContext(CopilotContext copilotContext, CopilotSession session) {
			if (copilotContext != null) {
				session.CurrentContext = copilotContext;
			}
		}

		private void AddContextCompletionMessage(CopilotSession session, List<ChatMessage> messages,
				CopilotContext copilotContext) {
			UpdateContext(copilotContext, session);
			CopilotContext newCopilotContext = session.CurrentContext;
			AddContextCompletionMessage(newCopilotContext, messages);
		}

		private bool TryHandleAgentInvocation(CopilotSession session, List<Guid> rootIntentIds) {
			if (!TryFindTriggeredIntent(session, rootIntentIds, out IToolExecutionTrigger agentToolExecutor, out Guid? triggeredIntentId)) {
				return false;
			}
			_boundedAgentUtils.ClearBoundedAgent(session);
			agentToolExecutor.TriggerExecution(session);
			_boundedAgentUtils.SetBoundedAgent(session, triggeredIntentId);
			return true;
		}

		private void HandleUserMessage(CopilotSendMessageOptions options, CopilotSession session,
			CopilotMessage userMessage, out bool sendSessionRequired) {
			bool agentInvoked = TryHandleAgentInvocation(session, options?.RootIntentIds);
			bool workflowExecuting = IsWorkflowExecuting(session);
			if (agentInvoked && workflowExecuting) {
				sendSessionRequired = false;
				if (string.IsNullOrWhiteSpace(session.Title)) {
					AsyncPump.Run(() => _sessionDispatcher.DispatchAsync(session, CancellationToken.None)); 
				}
				return;
			}
			sendSessionRequired = !workflowExecuting;
			if (TryHandleConfirmationToolCalls(userMessage, session, workflowExecuting)) {
				return;
			}
			TryHandleClarificationToolCalls(userMessage, session, workflowExecuting);
		}

		private bool IsWorkflowExecuting(CopilotSession session) {
			if (session?.RootIntentId == null) {
				return false;
			}
			CopilotIntentSchema intent = _intentsStorage.FindSchemaByUId(session.RootIntentId.Value);
			return intent != null && intent.Workflow.WorkflowSchemaUId != Guid.Empty;
		}

		#endregion

		#region Methods: Protected

		protected override List<ChatMessage> CreateCompletionMessages(CopilotSession session, CopilotToolContext copilotToolContext,
				CopilotContext copilotContext) {
			List<ChatMessage> messages = session.GetMergedMessages()
				.Select(msg => msg.ToCompletionApiMessage())
				.ToList();
			AddContextCompletionMessage(session, messages, copilotContext);
			AddAgentsDescriptionCompletionMessage(session, copilotToolContext, messages);
			_documentManager.AddDocumentsCompletionMessages(session, messages);
			_knwPromptBuilder.AddKnwSourcePrompt(session, messages);
			return messages;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public CopilotChatPart SendSession(string content, Guid? copilotSessionId = null,
				CopilotContext copilotContext = null, CopilotSendMessageOptions options = null) {
			_userConnection.DBSecurityEngine.CheckCanExecuteOperation(CanRunCopilotOperation);
			CopilotSession session = null;
			if (copilotSessionId.HasValue && copilotSessionId.Value.IsNotEmpty()) {
				session = _sessionManager.FindById(copilotSessionId.Value);
			}
			if (session == null) {
				session = _sessionManager.CreateSession(CopilotSessionType.Chat, copilotSessionId);
				UpdateContext(copilotContext, session);
				_sessionManager.Add(session);
			}
			CopilotMessage userMessage = CopilotMessage.FromUser(content);
			if (options?.MessageId != null && options.MessageId.Value != Guid.Empty) {
				userMessage.Id = options.MessageId.Value;
			}
			session.AddMessage(userMessage);
			DateTime lastMessageDate = userMessage.Date;
			SendMessagesToClient(session);
			_sessionManager.Update(session, null);
			string errorMessage = null;
			string errorCode = null;
			try {
				HandleUserMessage(options, session, userMessage, out bool sendSessionRequired);
				AdjustSessionSystemIntentPromptUsingSystemIntents(session);
				if (sendSessionRequired) {
					ProcessSessionCycle(session, copilotContext);
				}
			} catch (Exception e) {
				(errorMessage, errorCode) = GetErrorInfo(e);
				_log.Error(e);
			} finally {
				_copilotMsgChannelSender.SendSessionProgress(CopilotSessionProgress.Create(_userConnection, session,
					CopilotSessionProgressStates.WaitingForUserMessage), session.UserId);
			}
			List<BaseCopilotMessage> lastMessages = session.Messages
				.Where(message => message.Date >= lastMessageDate)
				.Cast<BaseCopilotMessage>().ToList();
			return new CopilotChatPart(lastMessages, session, errorMessage, errorCode);
		}

		/// <inheritdoc />
		public void HandleToolCallsCompletedAndSendSession(List<CopilotMessage> toolMessages, CopilotSession session,
				CopilotContext copilotContext = null) {
			HandleToolCallsOnCompleted(toolMessages, session, out bool sendSessionRequired);
			if (sendSessionRequired) {
				ProcessSessionCycle(session, copilotContext);
			}
		}

		/// <inheritdoc />
		public void CompleteAsyncChatWorkflow(Guid copilotSessionId, AsyncChatWorkflowResult workflowResult) {
			CopilotSession session = _sessionManager.GetById(copilotSessionId);
			try {
				session.SkipIntentUpdate = true;
				HandleToolCallsCompletedAndSendSession(workflowResult.CompletionMessages, session);
			} catch (Exception e) {
				_log.Error(e);
				throw;
			} finally {
				session.SkipIntentUpdate = false;
				_copilotMsgChannelSender.SendSessionProgress(CopilotSessionProgress.Create(_userConnection, session,
					CopilotSessionProgressStates.WaitingForUserMessage), session.UserId);
			}
		}

		#endregion

	}

	#endregion

}

