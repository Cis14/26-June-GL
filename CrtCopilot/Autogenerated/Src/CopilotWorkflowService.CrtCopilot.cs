namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::Common.Logging;
	using Creatio.Copilot.Metadata;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Hooks;
	using Terrasoft.Core.Tasks;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;
	using Creatio.FeatureToggling;

	#region Interface: ICopilotWorkflowService

	/// <summary>
	/// Copilot workflow service.
	/// </summary>
	public interface ICopilotWorkflowService
	{

		#region Methods: Public

		/// <summary>
		/// Initiates workflow for given session if workflow agent is configured.
		/// </summary>
		/// <param name="session">Session.</param>
		/// <param name="workflowUId">Workflow identifier.</param>
		/// <param name="arguments">Arguments.</param>
		IList<CopilotMessage> Start(CopilotSession session, Guid workflowUId, Dictionary<string, string> arguments);

		/// <summary>
		/// Handles confirmation response from user for a pending workflow confirmation.
		/// </summary>
		/// <param name="session">Copilot session.</param>
		/// <param name="userMessage">User message containing confirmation response.</param>
		/// <returns>True if the workflow element was successfully completed; otherwise, false.</returns>
		bool HandleConfirmation(CopilotSession session, string userMessage);

		/// <summary>
		/// Handles clarification response from user for a pending workflow clarification request.
		/// </summary>
		/// <param name="session">Copilot session.</param>
		/// <param name="userMessage">User message containing clarification response.</param>
		/// <returns>True if the workflow element was successfully completed; otherwise, false.</returns>
		bool HandleClarification(CopilotSession session, string userMessage);

		/// <summary>
		/// Cancels the workflow if it is currently running.
		/// </summary>
		/// <param name="session">Copilot session.</param>
		void Cancel(CopilotSession session);

		#endregion

	}

	#endregion

	#region Class: CopilotWorkflowExecutionResult

	/// <summary>
	/// Copilot workflow execution result.
	/// </summary>
	public class CopilotWorkflowExecutionResult
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the execution status.
		/// </summary>
		public CopilotWorkflowExecutionStatus Status { get; set; }

		/// <summary>
		/// Gets or sets the error message if execution failed.
		/// </summary>
		public string ErrorMessage { get; set; }

		/// <summary>
		/// Gets or sets the response from the workflow execution.
		/// </summary>
		public string Response { get; set; }

		/// <summary>
		/// Workflow instance identifier.
		/// </summary>
		public string WorkflowInstanceId { get; set; }

		/// <summary>
		/// Workflow name.
		/// </summary>
		public string WorkflowName { get; set; }

		/// <summary>
		/// Tool call identifier.
		/// </summary>
		public string ToolCallId { get; set; }

		#endregion

	}

	#endregion

	#region Class: CopilotWorkflowService

	/// <summary>
	/// Copilot workflow service.
	/// </summary>
	/// <inheritdoc cref="Creatio.Copilot.ICopilotWorkflowService"/>
	[DefaultBinding(typeof(ICopilotWorkflowService))]
	internal class CopilotWorkflowService : ICopilotWorkflowService
	{

		#region Class: SyncExecutionResultBucket

		/// <summary>
		/// Sync execution result bucket.
		/// </summary>
		internal class SyncExecutionResultBucket
		{

			#region Properties: Public

			/// <summary>
			/// Tool call messages.
			/// </summary>
			public IList<CopilotMessage> Messages { get; set; }

			#endregion

		}

		#endregion

		#region Class: WorkflowCompletionHookArgs

		/// <summary>
		/// Represents arguments for the workflow completion hook.
		/// </summary>
		internal class WorkflowCompletionHookArgs
		{

			#region Properties: Public

			/// <summary>
			/// Gets or sets the unique identifier of the Copilot session.
			/// </summary>
			public Guid CopilotSessionUId { get; set; }

			/// <summary>
			/// Gets or sets the unique identifier of the tool call.
			/// </summary>
			public string ToolCallId { get; set; }

			#endregion

		}

		#endregion

		#region Class: WorkflowCompletionHook

		/// <summary>
		/// Represents a background task for the workflow completion hook.
		/// </summary>
		[DefaultBinding(typeof(WorkflowCompletionHook))]
		internal class WorkflowCompletionHook : CopilotBaseCompletionHook,
			IBackgroundTask<ProcessExecutionHookArgs<WorkflowCompletionHookArgs>>
		{

			#region Fields: Private

			private readonly ICopilotSessionManager _sessionManager;
			private readonly ICopilotChatExecutor _chatExecutor;
			private readonly ICopilotParametrizedActionResponseProvider _responseProvider;

			#endregion

			#region Constructors: Public

			/// <summary>
			/// Initializes a new instance of the <see cref="WorkflowCompletionHook"/> type.
			/// </summary>
			/// <param name="sessionManager">Session manager.</param>
			/// <param name="chatExecutor">Chat executor.</param>
			/// <param name="responseProvider">Response provider.</param>
			public WorkflowCompletionHook(ICopilotSessionManager sessionManager, ICopilotChatExecutor chatExecutor,
				ICopilotParametrizedActionResponseProvider responseProvider)
			{
				_sessionManager = sessionManager;
				_chatExecutor = chatExecutor;
				_responseProvider = responseProvider;
			}

			#endregion

			#region Methods: Private

			private static CopilotWorkflowExecutionStatus GetExecutionStatus(ProcessStatus processStatus) {
				switch (processStatus) {
					case ProcessStatus.Cancelled:
						return CopilotWorkflowExecutionStatus.Cancelled;
					case ProcessStatus.Done:
						return CopilotWorkflowExecutionStatus.Completed;
					default:
						return CopilotWorkflowExecutionStatus.Failed;
				}
			}

			private static List<ICopilotParameterMetaInfo> CreateParameters(
					ProcessExecutionHookArgs<WorkflowCompletionHookArgs> processHookInfo) {
				var result = new List<ICopilotParameterMetaInfo>();
				var parameters = new FactoryMetaItemCollection<CopilotActionParameterMetaItem>();
				ProcessSchemaManager manager = processHookInfo.UserConnection.ProcessSchemaManager;
				ISchemaManagerItem<ProcessSchema> activeVersion;
				Guid processSchemaUId = processHookInfo.ProcessHookInfo.Process.Schema.UId;
				try {
					activeVersion = manager.GetActiveVersionItemByUId(processSchemaUId);
				} catch (ItemNotFoundException e) {
					throw new ItemNotFoundException($"Workflow failed. Process schema was not " +
						$"found by UId {processSchemaUId}", e);
				}
				ProcessSchema processSchema = activeVersion.Instance;
				foreach (ProcessSchemaParameter processParameter in processSchema.Parameters)
				{
					if (processParameter.Direction != ProcessSchemaParameterDirection.Internal)
					{
						var adapter = new ProcessParameterAdapter(processParameter, parameters);
						result.Add(adapter);
					}
				}
				return result;
			}

			private CopilotWorkflowExecutionResult FillResults(ProcessExecutionHookArgs<WorkflowCompletionHookArgs> args,
						IReadOnlyList<ICopilotParameterMetaInfo> processParameters) {
				var result = new CopilotWorkflowExecutionResult();
				try {
					UserConnection userConnection = args.UserConnection;
					IProcessInfo processInfo = args.ProcessHookInfo.Process;
					string response = GetResponse(userConnection, processInfo, processParameters, _responseProvider);
					ProcessStatus processStatus = processInfo.Descriptor.ProcessStatus;
					var errorHook = args.ProcessHookInfo as ProcessExecutionErrorHookArgs;
					string errorMessage = errorHook?.ErrorMessage;
					result.Status = GetExecutionStatus(processStatus);
					result.ErrorMessage = errorMessage;
					result.Response = response;
					result.WorkflowInstanceId = processInfo.Descriptor.UId.ToString();
					result.WorkflowName = processInfo.Schema.Name;
					result.ToolCallId = args.Arguments.ToolCallId;
				} catch (Exception e) {
					result.Status = CopilotWorkflowExecutionStatus.Failed;
					string message = $"An error occured during workflow execution: {e.Message}";
					result.ErrorMessage = message;
					Log.Error(message, e);
				}
				return result;
			}

			private List<CopilotMessage> PrepareResult(CopilotSession session, CopilotWorkflowExecutionResult result) {
				Guid? rootIntentId = session.RootIntentId;
				session.RootIntentId = null;
				session.CurrentIntentId = null;
				string resultContent;
				if (result.Status == CopilotWorkflowExecutionStatus.Completed) {
					resultContent = result.Response != null ? Json.Serialize(result.Response, true) : "Ok";
				} else if (result.Status == CopilotWorkflowExecutionStatus.Cancelled) {
					resultContent = WorkflowCancellationMessage;
				} else {
					string error = result.ErrorMessage ?? "Unknown error occurred";
					resultContent = string.Format(WorkflowFailedContextMessageFormat, error);
				}
				var toolCallMessage = CopilotMessage.FromAssistant(new ToolCall {
					Id = result.ToolCallId,
					FunctionCall = new FunctionCall() {
						Name = "Workfllow" + result.ToolCallId,
						Arguments = string.Empty,
					}
				});
				var resultMessage = CopilotMessage.FromTool(resultContent, result.ToolCallId);
				resultMessage.RootIntentId = rootIntentId;
				if (result.Status == CopilotWorkflowExecutionStatus.Completed &&
					Features.GetIsEnabled<GenAIFeatures.DisableAssistantResponseOnWorkflowCompletion>()) {
					resultMessage.OmitAssistantResponse = true;
				}
				var messages = new List<CopilotMessage> {
					toolCallMessage,
					resultMessage
				};
				if (result.Status == CopilotWorkflowExecutionStatus.Completed) {
					var systemMessage = CopilotMessage.FromSystem(WorkflowCompletedSystemMessage);
					systemMessage.RootIntentId = rootIntentId;
					messages.Add(systemMessage);
				}
				return messages;
			}

			private void HandleCompletion(ProcessExecutionHookArgs<WorkflowCompletionHookArgs> parameters) {
				WorkflowCompletionHookArgs hookArgs = parameters.Arguments;
				CopilotSession session = null;
				try {
					session = _sessionManager.GetById(hookArgs.CopilotSessionUId);
					if (session == null) {
						Log.Warn($"Session {hookArgs.CopilotSessionUId} not found for workflow completion");
						return;
					}
					IReadOnlyList<ICopilotParameterMetaInfo> processParameters = CreateParameters(parameters);
					var result = FillResults(parameters, processParameters);
					var completionMessages = PrepareResult(session, result);
					if (parameters.InlineExecutionContext is SyncExecutionResultBucket resultBucket) {
						resultBucket.Messages = completionMessages;
					} else {
						var response = new AsyncChatWorkflowResult {
							CompletionMessages = completionMessages
						};
						_chatExecutor.CompleteAsyncChatWorkflow(parameters.Arguments.CopilotSessionUId, response);
					}
				}
				catch (Exception e) {
					Log.Error($"Error in workflow completion hook: {e.Message}", e);
					if (session != null) {
						session.RootIntentId = null;
						session.CurrentIntentId = null;
					}
				}
			}

			#endregion

			#region Methods: Public

			/// <summary>
			/// Runs the workflow completion hook.
			/// </summary>
			public void Run(ProcessExecutionHookArgs<WorkflowCompletionHookArgs> parameters)
			{
				HandleCompletion(parameters);
			}

			#endregion

		}

		#endregion

		#region Constants: Internal

		/// <summary>
		/// Workflow cancellation message.
		/// </summary>
		internal const string WorkflowCancellationMessage = "The workflow has been cancelled. Workflow agent unassigned.";

		/// <summary>
		/// Workflow failed message format for LLM context.
		/// </summary>
		internal const string WorkflowFailedContextMessageFormat = "Workflow failed: {0}. Workflow agent unassigned.";

		/// <summary>
		/// Workflow completed successfully system message for user.
		/// </summary>
		internal const string WorkflowCompletedSystemMessage = "The workflow has completed successfully. " +
			"The workflow agent has been unassigned and you can continue the conversation.";

		/// <summary>
		/// Set of system messages related to workflow execution.
		/// </summary>
		internal static readonly HashSet<string> WorkflowSystemMessages = new HashSet<string> {
			WorkflowFailedContextMessageFormat,
			WorkflowCancellationMessage,
			WorkflowCompletedSystemMessage
		};

		#endregion

		#region Fields: Private

		private readonly ICopilotProcessExecutor _processExecutor;
		private readonly ICopilotSessionManager _sessionManager;

		#endregion

		#region Properties: Private

		private readonly ILog _log = LogManager.GetLogger("Copilot");

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotWorkflowService"/> type.
		/// </summary>
		/// <param name="processExecutor">Process executor.</param>
		/// <param name="sessionManager">Session manager.</param>
		public CopilotWorkflowService(ICopilotProcessExecutor processExecutor, ICopilotSessionManager sessionManager)
		{
			_processExecutor = processExecutor;
			_sessionManager = sessionManager;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public IList<CopilotMessage> Start(CopilotSession session, Guid workflowUid,
				Dictionary<string, string> arguments) {
			var parameterValues = new Dictionary<string, string>(arguments);
			parameterValues["CopilotWorkflowRootSessionId"] = session.Id.ToString();
			ProcessExecutionHookEvents hookEvents =
				ProcessExecutionHookEvents.ProcessCompleted | ProcessExecutionHookEvents.ProcessCanceled | ProcessExecutionHookEvents.ElementFailed;
			var toolCallId = CopilotExtensions.GenerateToolCallId();
			var hookArgs = new WorkflowCompletionHookArgs
			{
				CopilotSessionUId = session.Id,
				ToolCallId = toolCallId
			};
			try {
				var syncExecutionResultBucket = new SyncExecutionResultBucket();
				 _processExecutor.StartProcess(
					workflowUid,
					parameterValues,
					(options, args, events, context) => options.WithHook<WorkflowCompletionHook, WorkflowCompletionHookArgs>(
						(WorkflowCompletionHookArgs)args, events, context),
					hookArgs,
					hookEvents,
					syncExecutionResultBucket);
				var messages = new List<CopilotMessage>();
				if (syncExecutionResultBucket.Messages?.Any() == true) {
					messages.AddRange(syncExecutionResultBucket.Messages);
				}
				return messages;
			}
			catch (Exception e) {
				_log.Error($"Error starting workflow process: {e.Message}", e);
				session.AddMessage(CopilotMessage.FromSystem($"Error starting workflow: {e.Message}"));
				session.RootIntentId = null;
				session.CurrentIntentId = null;
				return new List<CopilotMessage>();
			}
		}

		/// <inheritdoc />
		public bool HandleConfirmation(CopilotSession session, string userMessage) {
			string processElementUId = session.Messages.FirstOrDefault(CopilotExtensions.IsPendingConfirmation)?.ProcessElementId;
			if (string.IsNullOrWhiteSpace(processElementUId)) {
				return false;
			}
			_sessionManager.Update(session, null);
			return _processExecutor.CompleteProcess(new Guid(processElementUId), userMessage);
		}

		/// <inheritdoc />
		public bool HandleClarification(CopilotSession session, string userMessage) {
			var message = session.Messages.LastOrDefault(m => m.IsPendingClarification());
			if (message == null || string.IsNullOrWhiteSpace(message.ProcessElementId)) {
				return false;
			}
			message.ConfirmationResult = userMessage;
			_sessionManager.Update(session, null);
			return _processExecutor.CompleteProcess(new Guid(message.ProcessElementId), userMessage);
		}

		/// <inheritdoc />
		public void Cancel(CopilotSession session) {
			CopilotSession activeChildSession = _sessionManager
				.GetActiveSessions(session.UserId)
				.Where(s => s.RootSessionId == session.Id && s.EndDate == null)
				.OrderBy(s => s.StartDate)
				.FirstOrDefault();
			if (activeChildSession?.ProcessElementId.IsNullOrEmpty() ?? true) {
				return;
			}
			_processExecutor.CancelProcess(activeChildSession.ProcessElementId.Value);
		}

		#endregion

	}

	#endregion

}

