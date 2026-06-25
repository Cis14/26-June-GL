namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using Creatio.FeatureToggling;
	using Terrasoft.Common;
	using Terrasoft.Core.Factories;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;
	using Terrasoft.Core;
	using System.Security;
	using Creatio.Copilot.Actions;
	using Terrasoft.Core.DB;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion.Responses;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion.Requests;
	using Terrasoft.Configuration.GenAI;
	using Terrasoft.Core.Configuration;
	using Polly;

	[DefaultBinding(typeof(ICopilotApiSkillExecutor))]
	internal class CopilotApiSkillExecutor : BaseCopilotExecutor, ICopilotApiSkillExecutor
	{

		#region Constants: Private

		private const string CanRunCopilotApiOperation = "CanRunCreatioAIApi";
		private const string CanDevelopCopilotIntentsOperation = "CanDevelopAISkills";
		private const string IntentExecutionResult = "IntentExecutionResult";
		private const string ResultDataPropertyName = "ResultData";
		private const string CompletionReasonPropertyName = "CompletionReason";
		private const string QualityScorePropertyName = "QualityScore";
		private const string OutputParametersDescription =
			"Structured result of this skill. Fields MUST be based only on user input, context, or deterministic mappings; do not invent values. If some required value is uncertain, keep the best value you have, lower QualityScore, and explain in CompletionReason.";
		private const string QualityScoreDescription =
			"Overall reliability of ResultData from 0.0 (unreliable) to 1.0 (fully reliable). Use 1.0 only when all required fields are present, validated according to their description, and need no guessing; reduce the score for missing, invalid, or assumed values.";
		private const string CompletionReasonDescription =
			"Short explanation of why the skill finished and how trustworthy the result is, mentioning any uncertainties that affected QualityScore.";
		private const decimal QualityScoreThresholdDefault = 0.85m;
		private const int MaxLowQualityRetries = 2;
		private const string RejectedToolCallToolMessage = "The tool call was rejected.";
		private const string RejectedToolCallUserMessage =
			"I rejected the last tool call{0}. Do not finish the task yet. Continue execution. " +
			"Your next response must be a function tool call only. If you have [SendMessageToUserAction], " +
			"call it to clarify next steps; otherwise correct parameters and retry {1}.";

		#endregion

		#region Fields: Private

		private readonly ICopilotOutputParametersHandler _outputParametersHandler;
		private readonly ICopilotSessionManager _sessionManager;
		private readonly IKnwPromptBuilder _knwPromptBuilder;
		private readonly ICopilotDocumentManager _documentManager;
		private readonly ICopilotToolProcessor _toolProcessor;
		private readonly ICopilotLinkValidator _linkValidator;
		private readonly ICopilotMsgChannelSender _copilotMsgChannelSender;
		private readonly ICopilotToolContextBuilder _toolContextBuilder;
		private readonly ICopilotMessageConfirmationHandler _confirmationHandler;
		private readonly ICopilotProcessExecutor _processExecutor;

		#endregion
		
		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotApiSkillExecutor"/>
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="copilotSessionManager">Copilot session manager.</param>
		/// <param name="outputParametersHandler">Copilot output parameters handler.</param>
		/// <param name="intentPromptBuilder">An instance of <see cref="IIntentPromptBuilder"/></param>
		/// <param name="knwPromptBuilder">An instance of <see cref="knwPromptBuilder"/></param>
		/// <param name="documentManager">An instance of <see cref="ICopilotDocumentManager"/></param>
		/// <param name="toolProcessor">Copilot tool processor.</param>
		/// <param name="completionService">GenAI Completion service.</param>
		/// <param name="linkValidator">An instance of <see cref="ICopilotLinkValidator"/>.</param>
		/// <param name="llmModelResolver">An instance of <see cref="ILlmModelResolver"/></param>
		/// <param name="contextBuilder">Copilot context builder.</param>
		/// <param name="requestLogger">Copilot request logger.</param>
		/// <param name="hyperlinkUtils">An instance of <see cref="ICopilotHyperlinkUtils"/>
		///     used for handling hyperlink-related utilities.</param>
		/// <param name="copilotMsgChannelSender">Coplot message channel sender.</param>
		/// <param name="intentsStorage">Copilot intents storage.</param>
		/// <param name="toolContextBuilder">Copilot tool context builder.</param>
		/// <param name="confirmationHandler">Copilot message confirmation handler.</param>
		/// <param name="processExecutor">Copilot process executor.</param>
		/// <param name="toolFilterResolver">An instance of <see cref="IToolFilterResolver"/></param>
		public CopilotApiSkillExecutor(UserConnection userConnection, ICopilotSessionManager copilotSessionManager,
				ICopilotOutputParametersHandler outputParametersHandler, IIntentPromptBuilder intentPromptBuilder,
				IKnwPromptBuilder knwPromptBuilder, ICopilotDocumentManager documentManager,
				ICopilotToolProcessor toolProcessor, IGenAICompletionServiceProxy completionService,
				ICopilotLinkValidator linkValidator, ILlmModelResolver llmModelResolver,
				ICopilotContextBuilder contextBuilder, ICopilotRequestLogger requestLogger,
				ICopilotHyperlinkUtils hyperlinkUtils, ICopilotMsgChannelSender copilotMsgChannelSender,
				ICopilotIntentsStorage intentsStorage, ICopilotToolContextBuilder toolContextBuilder,
				ICopilotMessageConfirmationHandler confirmationHandler, ICopilotProcessExecutor processExecutor,
				IToolFilterResolver toolFilterResolver)
					: base(userConnection, completionService, llmModelResolver,
						contextBuilder, requestLogger, hyperlinkUtils,
						intentPromptBuilder, intentsStorage, toolFilterResolver) {
			_sessionManager = copilotSessionManager;
			_outputParametersHandler = outputParametersHandler;
			_knwPromptBuilder = knwPromptBuilder;
			_documentManager = documentManager;
			_toolProcessor = toolProcessor;
			_linkValidator = linkValidator;
			_copilotMsgChannelSender = copilotMsgChannelSender;
			_toolContextBuilder = toolContextBuilder;
			_confirmationHandler = confirmationHandler;
			_processExecutor = processExecutor;
		}

		#endregion

		#region Methods: Private

		private bool ValidateApiSkillForExecution(string intentName, CopilotIntentCallResult response,
				out CopilotIntentSchema intent) {
			intent = null;
			try {
				_userConnection.DBSecurityEngine.CheckCanExecuteOperation(CanRunCopilotApiOperation);
			} catch (SecurityException e) {
				SetErrorResponse(response, e.GetFullMessage(), IntentCallStatus.InsufficientPermissions);
				return false;
			}
			if (Features.GetIsDisabled<GenAIFeatures.EnableStandaloneApi>()) {
				SetErrorResponse(response, _userConnection.GetLocalizableString("StandaloneApiFeatureOff", nameof(CopilotApiSkillExecutor)),
					IntentCallStatus.InsufficientPermissions);
				return false;
			}
			try {
				intent = _intentsStorage.FindSchemaByName(intentName);
			} catch (SecurityException) {
				LocalizableString ls = _userConnection.GetLocalizableString("NoSkillReadRight", nameof(CopilotApiSkillExecutor));
				SetErrorResponse(response, ls.Format(intentName),
					IntentCallStatus.InsufficientPermissions);
				intent = null;
				return false;
			}
			if (intent == null) {
				LocalizableString ls = _userConnection.GetLocalizableString("IntentNotFound", nameof(CopilotApiSkillExecutor));
				SetErrorResponse(response, ls.Format(intentName), IntentCallStatus.IntentNotFound);
				return false;
			}
			if (!_intentsStorage.HasExecutionPermitted(intent.UId)) {
				LocalizableString ls = _userConnection.GetLocalizableString("NoIntentExecutionRight", nameof(CopilotApiSkillExecutor));
				SetErrorResponse(response, ls.Format(intentName),
					IntentCallStatus.InsufficientPermissions);
				return false;
			}
			if (!IsActiveIntent(intent)) {
				LocalizableString ls = _userConnection.GetLocalizableString("InactiveIntent", nameof(CopilotApiSkillExecutor));
				SetErrorResponse(response, ls.Format(intentName), IntentCallStatus.InactiveIntent);
				return false;
			}
			if (!intent.Behavior.SkipForChat) {
				LocalizableString ls = _userConnection.GetLocalizableString("WrongIntentMode", nameof(CopilotApiSkillExecutor));
				SetErrorResponse(response, ls.Format(intentName, CopilotIntentMode.Chat),
					IntentCallStatus.WrongIntentMode);
				return false;
			}
			return true;
		}

		private static void SetErrorResponse(CopilotIntentCallResult response, string errorMessage,
				IntentCallStatus status = IntentCallStatus.FailedToExecute) {
			response.ErrorMessage = errorMessage;
			response.Status = status;
		}

		private bool IsActiveIntent(CopilotIntentSchema intent) {
			switch (intent.Status) {
				case CopilotIntentStatus.Deactivated:
					return false;
				case CopilotIntentStatus.InDevelopment:
					bool canDevelopIntents = _userConnection.DBSecurityEngine.GetCanExecuteOperation(
						CanDevelopCopilotIntentsOperation, new GetCanExecuteOperationOptions {
							ThrowExceptionIfNotFound = false
						});
					return canDevelopIntents;
				case CopilotIntentStatus.Active:
					return true;
				default:
					return false;
			}
		}

		private CopilotSession CreateSession(Guid sessionId, CreateSessionOptions options) {
			CopilotSession session = null;
			if (sessionId != Guid.Empty) {
				session = _sessionManager.FindById(sessionId);
				if (session != null) {
					throw new ArgumentException($"Session with the specified SessionId {sessionId} already exists.");
				}
				session = _sessionManager.CreateSession(CopilotSessionType.Api, sessionId, options);
			} else {
				session = _sessionManager.CreateSession(CopilotSessionType.Api, options: options);
			}
			return session;
		}

		private CopilotSession CreateApiSkillSession(CopilotIntentCall call, CopilotIntentSchema intent,
				CopilotIntentCallResult response, bool useJsonSchema) {
			List<CopilotMessage> promptMessages = _intentPromptBuilder.GenerateIntentPromptMessages(call.Parameters, call.AdditionalPromptText, intent,
				response.Warnings, !useJsonSchema);
			CopilotSession session = CreateSession(call.ExecutionOptions.SessionId.GetValueOrDefault(),
				new CreateSessionOptions {
					HasInputParameters = intent.InputParameters.Count > 0,
					UseDefaultSystemPrompt = intent.Behavior?.UseDefaultSystemPrompt ?? true
				});
			session.CurrentIntentId = intent.UId;
			session.RootIntentId = intent.UId;
			Guid rootSessionId = call.ExecutionOptions.RootSessionId.GetValueOrDefault();
			if (rootSessionId != Guid.Empty) {
				CopilotSession rootSession = _sessionManager.FindById(rootSessionId);
				if (rootSession != null) {
					session.RootIntentId = rootSession.CurrentIntentId ?? intent.UId;
					session.RootSessionId = rootSessionId;
				}
			}
			_knwPromptBuilder.AddKnwSourceMsg(session);
			session.ProcessElementId = call.ExecutionOptions.ProcessElementId;
			session.AddMessages(promptMessages);
			_documentManager.AddIntentDocuments(session);
			_documentManager.AddCallDocuments(session, call);
			_sessionManager.Add(session);
			return session;
		}

		private bool IsAdvancedOutputParametersMode(bool useJsonSchema, CopilotIntentSchema intent) {
			return useJsonSchema && !string.IsNullOrWhiteSpace(intent.ResponseFormatJsonSchema.PackageName) &&
				!string.IsNullOrWhiteSpace(intent.ResponseFormatJsonSchema.SchemaName);
		}

		private void ParseAndHandleOutputParameters(CopilotIntentCallResult response, CopilotIntentSchema intent,
				bool useJsonSchema) {
			if (!response.IsSuccess) {
				return;
			}
			var parseOutputParameters = !IsAdvancedOutputParametersMode(useJsonSchema, intent);
			try {
				HandleOutputParameters(response, intent, parseOutputParameters);
			} catch (Exception e) {
				response.Status = IntentCallStatus.ResponseParsingFailed;
				LocalizableString ls = _userConnection.GetLocalizableString("ParsingFailed", nameof(CopilotApiSkillExecutor));
				response.ErrorMessage = ls.Format(e.GetFullMessage());
			}
		}

		private static Dictionary<string, JToken> ParseContent(string content) {
			if (content.StartsWith("```json")) {
				content = content.Substring(7, content.Length - 10);
			}

			var result = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(content);
			return result;
		}

		private static string GetContentFromExecutionResult(Dictionary<string, JToken> intentExecutionResult,
				string fallbackContent) {
			if (intentExecutionResult.TryGetValue(ResultDataPropertyName, out JToken outputParametersToken)) {
				JToken contentToken = outputParametersToken?["content"];
				if (contentToken != null) {
					return contentToken.Value<string>();
				}
				return outputParametersToken?.ToString(Formatting.None);
			}

			return intentExecutionResult.TryGetValue("content", out JToken directContentToken)
				? directContentToken?.Value<string>()
				: fallbackContent;
		}

		private bool ShouldRetryOnLowQuality(CopilotIntentCallResult response) {
			if (!Features.GetIsEnabled<GenAIFeatures.UseApiSkillReasoningWithRetries>()) {
				return false;
			}
			
			if (response.Status == IntentCallStatus.InProgress ||
					!response.IsSuccess || response.Content.IsNullOrEmpty()) {
				return false;
			}

			var qualityScore = GetOutputParametersQualityScore(response.Content);
			var qualityScoreThreshold = SysSettings.GetValue(
				_userConnection, "CopilotApiSkillRetryQualityScoreThreshold", QualityScoreThresholdDefault);
			return qualityScore < qualityScoreThreshold;
		}

		private decimal? GetOutputParametersQualityScore(string responseContent) {
			Dictionary<string, JToken> intentExecutionResult = ParseContent(responseContent);
			return intentExecutionResult.TryGetValue(QualityScorePropertyName,
					out JToken resultQualityToken)
				? resultQualityToken?.Value<decimal?>()
				: null;
		}

		private static JsonSchema AddReasoningToStructuredOutputIfNeeded(JsonSchema baseSchema) {
			if (!Features.GetIsEnabled<GenAIFeatures.UseApiSkillReasoningWithRetries>() || baseSchema?.Schema == null) {
				return baseSchema;
			}
			var resultDataObject = new PropertyDefinition {
				Type = baseSchema.Schema.Type,
				Description = OutputParametersDescription,
				Properties = baseSchema.Schema.Properties,
				Required = baseSchema.Schema.Required,
				Items = baseSchema.Schema.Items,
				AdditionalProperties = baseSchema.Schema.AdditionalProperties
			};
			var modifiedSchema = new JsonSchema {
				Name = IntentExecutionResult,
				Strict = true,
				Schema = PropertyDefinition.DefineObject(new Dictionary<string, PropertyDefinition> {
					{ ResultDataPropertyName, resultDataObject },
					{ QualityScorePropertyName, PropertyDefinition.DefineNumber(QualityScoreDescription) },
					{ CompletionReasonPropertyName, PropertyDefinition.DefineString(CompletionReasonDescription) }
				}, new List<string> {
					ResultDataPropertyName,
					QualityScorePropertyName,
					CompletionReasonPropertyName
				}, null)
			};
			return modifiedSchema;
		}

		private async Task<Guid> CompleteIntentWithReasoningAndRetriesAsync(CopilotSession session,
				CopilotIntentCallResult response,
				JsonSchema jsonSchema,
				CancellationToken token,
				bool enableApiToolCalls) {

			var currentRequestId = await Policy.HandleResult<Guid>(_ => ShouldRetryOnLowQuality(response))
				.RetryAsync(MaxLowQualityRetries)
				.ExecuteAsync(() => CompleteIntentAsync(session, response, jsonSchema, token, enableApiToolCalls))
				.ConfigureAwait(false);
			if (ShouldRetryOnLowQuality(response)) {
				throw new InvalidOperationException("The intent execution result quality score is below the threshold.");
			}
			return currentRequestId;
		}

		private void HandleOutputParameters(CopilotIntentCallResult response, CopilotIntentSchema intent,
				bool parseOutputParameters) {
			if (response.Content.IsNullOrEmpty()) {
				throw new ArgumentNullOrEmptyException(nameof(response.Content));
			}

			Dictionary<string, JToken> intentExecutionResult = ParseContent(response.Content);
			if (parseOutputParameters && intent.OutputParameters.Count > 0) {
				if (intentExecutionResult.TryGetValue(ResultDataPropertyName, out JToken outputParametersToken)) {
					intentExecutionResult = outputParametersToken.ToObject<Dictionary<string, JToken>>();
				}
				response.ResultParameters = _outputParametersHandler.HandleOutputParameters(intentExecutionResult,
					intent.OutputParameters);
			} else {
				response.Content = GetContentFromExecutionResult(intentExecutionResult, response.Content);
			}
		}

		private async Task<Guid> CompleteIntentAsync(CopilotSession session, CopilotIntentCallResult response,
				JsonSchema jsonSchema, CancellationToken token, bool enableApiToolCalls) {
			DateTime? startDate = null;
			DateTime? endDate = null;
			ChatCompletionResponse completionResponse = null;
			Guid requestId;
			CopilotToolContext copilotToolContext = null;
			if (enableApiToolCalls) {
				if (Features.GetIsEnabled<GenAIFeatures.UseAgenticProcesses>()) {
					AdjustSessionSystemIntentPromptUsingSubIntents(session);
				}
				copilotToolContext = _toolContextBuilder.BuildApiSkillToolContext(session);
			}
			ChatCompletionRequest completionRequest = CreateApiCompletionRequest(session, copilotToolContext,
				jsonSchema);
			try {
				(startDate, endDate, completionResponse) = await ProcessCompletionRequest(completionRequest,
						false, token)
					.ConfigureAwait(true);
				MapSkillResponse(completionResponse, response, session);
			} catch (Exception e) {
				(response.ErrorMessage, _) = GetErrorInfo(e);
				response.Status = IntentCallStatus.FailedToExecute;
			} finally {
				UsageResponse usage = completionResponse?.Usage;
				requestId = SaveRequestInfo(startDate, endDate, usage, response.ErrorMessage, !response.IsSuccess);
			}
			if (enableApiToolCalls) {
				await HandleApiToolCallsAsync(session, completionResponse, copilotToolContext, response,
					jsonSchema, token);
			}
			return requestId;
		}

		private void AdjustSessionSystemIntentPromptUsingSubIntents(CopilotSession copilotSession) {
			AdjustSessionSystemIntentPrompt(copilotSession, () => _intentsStorage.GetSystemSubIntents(copilotSession.CurrentIntentId));
		}

		private async Task HandleApiToolCallsAsync(CopilotSession session, ChatCompletionResponse response,
				CopilotToolContext toolContext, CopilotIntentCallResult intentResponse, JsonSchema jsonSchema,
				CancellationToken token) {
			List<CopilotMessage> messages = _toolProcessor.HandleToolCalls(response, session, toolContext)
				?? new List<CopilotMessage>();
			session.AddMessages(messages);
			if (session.IsTransient && session.RootSessionId.HasValue) {
				if (messages.LastOrDefault(message => message.IsCancellation()) is CopilotMessage cancellationMessage) {
					throw new OperationCanceledException(cancellationMessage.Content);
				}
				List<CopilotMessage> pendingUserInteractionMessages = messages
					.Where(m => m.IsPendingConfirmation() || m.IsPendingClarification())
					.ToList();
				List<CopilotMessage> userFacingMessages = messages
					.Where(m => m.IsPendingConfirmation() || m.IsPendingClarification() ||
						m.IsNotification() || m.ContentType == CopilotContentType.ViewElement)
					.ToList();
				if (userFacingMessages.Any()) {
					SendMessagesToRootChat(session, userFacingMessages);
				}
				if (pendingUserInteractionMessages.Any()) {
					_sessionManager.Update(session, null);
					intentResponse.Status = IntentCallStatus.InProgress;
					return;
				}
			}
			if (session.IsTransient && session.ProcessElementId.GetValueOrDefault() != Guid.Empty) {
				List<CopilotMessage> pendingAsyncMessages = messages.Where(m =>
					m.IsPendingAsyncAction()).ToList();
				if (pendingAsyncMessages.Any()) {
					session.AddMessages(messages);
					_sessionManager.Update(session, null);
					intentResponse.Status = IntentCallStatus.InProgress;
					return;
				}
			}
			await HandleApiToolCallsCompletedAsync(messages, session, intentResponse, jsonSchema, token);
		}

		private void SendMessagesToRootChat(CopilotSession session, List<CopilotMessage> messages) {
			if (!session.RootSessionId.HasValue) {
				return;
			}
			CopilotSession rootSession = _sessionManager.FindById(session.RootSessionId.Value);
			List<BaseCopilotMessage> messagesToSend = PrepareMessagesBeforeSend(messages, session);
			_copilotMsgChannelSender.SendMessages(new CopilotChatPart(messagesToSend,
				new BaseCopilotSession(rootSession)));
			rootSession.AddMessages(messages, skipIntentUpdate: true);
			_sessionManager.Update(rootSession, null);
		}

		private List<BaseCopilotMessage> PrepareMessagesBeforeSend(List<CopilotMessage> messagesToSend, CopilotSession session) {
			if (messagesToSend.Count == 0) {
				return new List<BaseCopilotMessage>();
			}
			messagesToSend
				.Where(message => !message.IsSentToClient && !message.IsFromSystemPrompt && !message.TruncateOnSave)
				.ForEach(message => {
					message.RootIntentCaption = _intentsStorage.GetIntentCaptionByIntentId(session.RootIntentId);
					message.RootIntentId = session.RootIntentId;
					message.ProcessElementId = session?.ProcessElementId?.ToString();
					message.IsSentToClient = true;
					message.Role = CopilotMessageRole.Assistant;
					message.ForwardToClient = true;
				});
			return messagesToSend
				.Cast<BaseCopilotMessage>().ToList();;
		}

		private async Task HandleApiToolCallsCompletedAsync(List<CopilotMessage> toolMessages, CopilotSession session,
				CopilotIntentCallResult intentResponse, JsonSchema jsonSchema, CancellationToken token) {
			if (toolMessages.IsNullOrEmpty()) {
				return;
			}
			if (toolMessages.IsAllToolsShouldOmitAssistantResponse()) {
				_sessionManager.Update(session, null);
				return;
			}
			if (Features.GetIsEnabled<GenAIFeatures.UseAgenticProcesses>()) {
				AdjustSessionSystemIntentPromptUsingSubIntents(session);
			} else {
				AdjustSessionSystemIntentPromptUsingSystemIntents(session);
			}
			bool enableApiToolCalls = Features.GetIsEnabled<GenAIFeatures.EnableApiToolCalls>();
			await CompleteIntentAsync(session, intentResponse, jsonSchema, token, enableApiToolCalls);
		}

		private void MapSkillResponse(ChatCompletionResponse completionResponse,
				CopilotIntentCallResult skillResponse, CopilotSession session) {
			if (completionResponse?.Choices == null) {
				skillResponse.Status = IntentCallStatus.CantGenerateGoodResponse;
				return;
			}
			if (session.RootSessionId.HasValue &&
				(session.Messages.Any(m => m.IsPendingClarification()) ||
					session.Messages.Any(m => m.IsPendingConfirmation()))
				|| session.Messages.Any(m => m.IsPendingAsyncAction())) {
				skillResponse.Status = IntentCallStatus.InProgress;
				return;
			}
			List<CopilotMessage> assistantMessages = GetAssistantMessagesWithoutToolCalls(completionResponse);
			if (Features.GetIsEnabled<GenAIFeatures.EnableApiLinkValidation>()) {
				assistantMessages.ForEach(message => {
					message.Content = _linkValidator.ValidateLinks(session, message.Content, isApi: true);
				});
			}
			session.AddMessages(assistantMessages);
			skillResponse.Status = IntentCallStatus.ExecutedSuccessfully;
			IEnumerable<string> messages = assistantMessages
				.Select(message => message.Content);
			skillResponse.Content = string.Join(" ", messages);
		}

		private ChatCompletionRequest CreateApiCompletionRequest(CopilotSession session,
				CopilotToolContext copilotToolContext, JsonSchema jsonSchema) {
			var request = CreateCompletionRequest(session, copilotToolContext);
			if (jsonSchema != null) {
				request.ResponseFormat = new ResponseFormat {
					Type = CompletionStatics.ResponseFormat.JsonSchema,
					JsonSchema = jsonSchema
				};
			}
			return request;
		}

		private async Task<Guid> ExecuteIntentAsync(CopilotSession session, CopilotIntentSchema intent,
				bool useJsonSchema, CancellationToken token, CopilotIntentCallResult response,
				bool enableApiToolCalls) {
			var jsonSchema = useJsonSchema
				? _outputParametersHandler.GetOutputParametersJsonSchema(intent)
				: null;
			jsonSchema = AddReasoningToStructuredOutputIfNeeded(jsonSchema);
			var requestId = await CompleteIntentWithReasoningAndRetriesAsync(session, response, jsonSchema, token, enableApiToolCalls)
				.ConfigureAwait(false);

			ParseAndHandleOutputParameters(response, intent, useJsonSchema);

			return requestId;
		}

		private static CopilotMessage GetUserMessageFromConfirmationResult(CopilotSession session, CopilotMessage lastUserMessage) {
			CopilotMessage confirmationMessage = session.Messages.LastOrDefault(message => message.IsConfirmation());
			if (confirmationMessage?.ConfirmationResult != CopilotToolConfirmationResult.Approve) {
				return CopilotMessage.FromUser(BuildRejectedToolCallUserMessage(session));
			}
			CopilotMessageButton resultButton = confirmationMessage.Buttons.FirstOrDefault(button => button.Code == CopilotButtonCode.Yes);
			return resultButton != null
				? CopilotMessage.FromUser(resultButton.Caption)
				: lastUserMessage;
		}

		private static string BuildRejectedToolCallUserMessage(CopilotSession session) {
			ToolCall rejectedToolCall = session.Messages
				.Where(m => m.Role == CopilotMessageRole.Assistant)
				.SelectMany(message => message.ToolCalls ?? Enumerable.Empty<ToolCall>())
				.LastOrDefault(toolCall => toolCall.ConfirmationResult == CopilotToolConfirmationResult.Reject);
			string functionName = rejectedToolCall?.FunctionCall?.Name;
			(string functionSuffix, string retryTarget) = BuildRejectedToolCallMessageTokens(functionName);
			return FormatRejectedToolCallUserMessage(functionSuffix, retryTarget);
		}

		private static (string FunctionSuffix, string RetryTarget) BuildRejectedToolCallMessageTokens(string functionName) {
			return string.IsNullOrWhiteSpace(functionName)
				? (string.Empty, "the tool call")
				: ($" ({functionName})", functionName);
		}

		private static string FormatRejectedToolCallUserMessage(string functionSuffix, string retryTarget) {
			return string.Format(RejectedToolCallUserMessage, functionSuffix, retryTarget);
		}

		private static void EnrichToolCallByToolUserMessage(CopilotSession session, CopilotMessage lastUserMessage) {
			CopilotMessage toolCallMessage = session.Messages.LastOrDefault(msg => msg.IsToolCall());
			List<CopilotMessage> toolMessages = session.CreateToolCallMessages(toolCallMessage?.ToolCallId, lastUserMessage?.Content);
			session.AddMessages(toolMessages);
			session.RemoveLastMessage(CopilotContentType.Clarification);
		}

		private static void AddRejectedToolCallMessage(CopilotSession session) {
			string lastRejectedToolCallId = session.Messages
				.Where(m => m.Role == CopilotMessageRole.Assistant)
				.SelectMany(message => (message.ToolCalls ?? Enumerable.Empty<ToolCall>())
					.Where(toolCall => toolCall.ConfirmationResult == CopilotToolConfirmationResult.Reject)
					.Select(toolCall => toolCall.Id))
				.LastOrDefault();
			if (string.IsNullOrEmpty(lastRejectedToolCallId)) {
				return;
			}
			bool hasRejectedToolMessage = session.Messages.Any(message =>
				message.IsToolById(lastRejectedToolCallId) && message.Content == RejectedToolCallToolMessage);
			if (hasRejectedToolMessage) {
				return;
			}
			List<CopilotMessage> rejectedToolMessages =
				session.CreateToolCallMessages(lastRejectedToolCallId, RejectedToolCallToolMessage);
			session.AddMessages(rejectedToolMessages);
		}

		private void HandleConfirmationToolCalls(CopilotSession session, CopilotMessage lastUserMessage, CopilotIntentSchema currentIntent) {
			if (!session.CurrentIntentId.HasValue || !session.Messages.Any(CopilotExtensions.IsPendingConfirmation)) {
				return;
			}
			CopilotToolContext copilotToolContext = _toolContextBuilder.BuildApiSkillToolContext(session);
			List<CopilotMessage> messages = _confirmationHandler.HandleConfirmedToolCalls(
				lastUserMessage, session, copilotToolContext);
			bool hasSendMessagesToUserCapability = currentIntent.HasSendMessagesToUserCapability();
			if (!hasSendMessagesToUserCapability) {
				AddRejectedToolCallMessage(session);
			}
			session.AddMessage(hasSendMessagesToUserCapability
				? lastUserMessage
				: GetUserMessageFromConfirmationResult(session, lastUserMessage));
			session.AddMessages(messages);
			session.CleanToolCallsWithoutTools();
		}

		private List<ChatMessage> GetMessagesMergedWithRootMessages(CopilotSession session, CopilotSession rootSession, List<ChatMessage> messages) {
			IEnumerable<CopilotMessage> rootMessages = rootSession.GetMergedChatHistoryMessages();
			if (session.StartDate.HasValue) {
				rootMessages = rootMessages.Where(msg => msg.Date <= session.StartDate.Value);
			}
			messages = rootMessages
				.Select(msg => msg.ToCompletionApiMessage())
				.Concat(messages)
				.ToList();
			return messages;
		}

		private void HandleAsyncToolCall(CopilotSession session) {
			CopilotMessage toolCallMessage = session.Messages.LastOrDefault(msg => msg.IsToolCall());
			var asyncResponseMessage =
				session.Messages.LastOrDefault(msg => msg.ContentType == CopilotContentType.AsyncAction);
			List<CopilotMessage> toolMessages = session.CreateToolCallMessages(toolCallMessage?.ToolCallId,
				asyncResponseMessage?.ConfirmationResult);
			session.AddMessages(toolMessages);
			session.RemoveLastMessage(CopilotContentType.AsyncAction);
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc />
		protected override List<ChatMessage> CreateCompletionMessages(CopilotSession session, CopilotToolContext copilotToolContext,
				CopilotContext copilotContext) {
			List<ChatMessage> messages = session.GetMergedMessages()
				.Select(msg => msg.ToCompletionApiMessage())
				.ToList();
			if (GetCurrentIntent(session) is CopilotIntentSchema intent && _sessionManager.GetRootSession(session) is CopilotSession rootSession) {
				if (intent.Behavior.UsePageContext) {
					AddContextCompletionMessage(rootSession.CurrentContext, messages);
				}
				if (intent.Behavior.UseChatHistory) {
					messages = GetMessagesMergedWithRootMessages(session, rootSession, messages);
				}
			}
			_knwPromptBuilder.AddKnwSourcePrompt(session, messages);
			_documentManager.AddDocumentsCompletionMessages(session, messages);
			return messages;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public async Task<CopilotIntentCallResult> ExecuteAsync(CopilotIntentCall call, CancellationToken token = default) {
			call.CheckArgumentNull(nameof(call));
			var response = new CopilotIntentCallResult {
				Warnings = new List<string>()
			};
			if (!ValidateApiSkillForExecution(call.IntentName, response, out CopilotIntentSchema intent)) {
				return response;
			}
			bool useJsonSchema = Features.GetIsEnabled<GenAIFeatures.UseJsonSchemaForApiOutputParameters>();
			Guid requestId = Guid.Empty;
			CopilotSession session = null;
			try {
				session = CreateApiSkillSession(call, intent, response, useJsonSchema);
				bool enableApiToolCalls = Features.GetIsEnabled<GenAIFeatures.EnableApiToolCalls>();
				requestId = await ExecuteIntentAsync(session, intent, useJsonSchema, token, response, enableApiToolCalls);
			} catch (OperationCanceledException e) {
				SetErrorResponse(response, e.GetFullMessage(), IntentCallStatus.Canceled);
			} catch (Exception e) {
				SetErrorResponse(response, e.GetFullMessage());
			} finally {
				if (session != null && response.Status != IntentCallStatus.InProgress) {
					_sessionManager.CloseSession(session, requestId);
				}
			}
			return response;
		}

		public async Task<CopilotIntentCallResult> CompleteExecutingIntentAsync(CopilotSession session,
				CancellationToken token) {
			var response = new CopilotIntentCallResult {
				Warnings = new List<string>()
			};
			CopilotSession rootSession = _sessionManager.GetRootSession(session);
			CopilotMessage userMessage = rootSession?.Messages.LastOrDefault(m => m.IsFromUser());
			CopilotIntentSchema currentIntent = GetCurrentIntent(session);
			_copilotMsgChannelSender.SendSessionProgress(CopilotSessionProgress.Create(_userConnection, rootSession,
				CopilotSessionProgressStates.WaitingForAssistantMessage), session.UserId);
			if (session.Messages.Any(CopilotExtensions.IsPendingConfirmation)) {
				HandleConfirmationToolCalls(session, userMessage, currentIntent);
			} else if (session.Messages.Any(m => m.ContentType == CopilotContentType.AsyncAction)) {
				HandleAsyncToolCall(session);
			} else {
				EnrichToolCallByToolUserMessage(session, userMessage);
			}
			if (!ValidateApiSkillForExecution(currentIntent?.Name, response, out CopilotIntentSchema intent)) {
				return response;
			}
			bool useJsonSchema = Features.GetIsEnabled<GenAIFeatures.UseJsonSchemaForApiOutputParameters>();
			var requestId = Guid.Empty;
			try {
				requestId = await ExecuteIntentAsync(session, intent, useJsonSchema, token, response, enableApiToolCalls: true);
			} catch (OperationCanceledException e) {
				SetErrorResponse(response, e.GetFullMessage(), IntentCallStatus.Canceled);
			} catch (Exception e) {
				SetErrorResponse(response, e.GetFullMessage());
			} finally {
				if (response.Status != IntentCallStatus.InProgress) {
					_sessionManager.CloseSession(session, requestId);
				}
			}
			return response;
		}

		/// <inheritdoc />
		public void CompleteAction(CopilotSession session, string actionInstanceUId,
				CopilotActionExecutionResult result) {
			CopilotMessage asyncActionMessage = session.Messages
				.FirstOrDefault(m => m.IsPendingAsyncAction() && m.ToolCallId == actionInstanceUId);
			if (asyncActionMessage != null && Guid.TryParse(asyncActionMessage.ProcessElementId, out Guid prcElId)) {
				string resultContent = result.Status == CopilotActionExecutionStatus.Completed
					? result.Response ?? "Ok"
					: result.ErrorMessage ?? "Unknown error occurred";
				asyncActionMessage.ConfirmationResult = resultContent;
				_sessionManager.Update(session, null);
				_processExecutor.CompleteProcess(prcElId, resultContent);
			}
		}

		#endregion

	}
}

