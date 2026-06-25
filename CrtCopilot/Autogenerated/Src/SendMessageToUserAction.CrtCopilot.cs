namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using Common.Logging;
	using Creatio.Copilot.Actions;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	/// <summary>
	/// Specialized tool executor for SendMessagesToUser that pauses execution until the user responds in chat.
	/// </summary>
	public class SendMessageToUserAction : BaseExecutableCodeAction
	{

		#region Constants: Private

		private const string DefaultUserMessage = "Additional information is required. Please provide more details.";

		#endregion

		#region Fields: Private

		private readonly ILog _log = LogManager.GetLogger("Copilot");

		#endregion

		#region Constructors: Public

		public SendMessageToUserAction() {
			Parameters = new List<SourceCodeActionParameter> {
				new SourceCodeActionParameter {
					Name = "message",
					Caption = new LocalizableString("Message"),
					Description = new LocalizableString("Message to user"),
					IsRequired = false,
					DataValueTypeUId = DataValueType.MediumTextDataValueTypeUId,
				}
			}.AsReadOnly();
		}

		#endregion

		#region Methods: Private

		private CopilotActionExecutionResult GetExecutionResult(CopilotMessage resultMessage) {
			return new CopilotActionExecutionResult {
				Status = CopilotActionExecutionStatus.Completed,
				Response = resultMessage.Content,
				ResponseOptions = new ActionResponseOptions() {
					ContentType = CopilotContentType.Clarification,
					OmitAssistantResponse = true,
					ForwardToClient = true
				}
			};
		}

		private void EnsureRequiredParameters(ActionExecutionOptions options, out string toolCallId,
			out string message) {
			if (options is CopilotActionExecutionOptions copilotOptions) {
				toolCallId = copilotOptions.InstanceUId;
			} else {
				throw new ArgumentException("ToolCallId is required.");
			}
			if (!options.ParameterValues.TryGetValue("message", out message)) {
				throw new ArgumentException("The 'message' parameter is required.");
			}
		}

		private bool TryFindRootSession(ICopilotSessionManager sessionManager, Guid sessionId, string toolCallId,
			out CopilotMessage resultMessage) {
			resultMessage = null;
			var session = sessionManager.FindById(sessionId);
			if (session.RootSessionId != null && sessionManager.FindById((Guid)session.RootSessionId) != null) {
				return true;
			}
			_log.Warn($"Root session '{session.RootSessionId}' not found for SendMessageToUserAction.");
			resultMessage = new CopilotMessage("Unable to send message to user because the chat session was not found.",
				CopilotMessageRole.Tool, toolCallId);
			return false;
		}

		private static CopilotMessage CreateWaitingMessage(string toolCallId, CopilotSession session, string message) {
			return new CopilotMessage(message, CopilotMessageRole.Tool,
				toolCallId) {
				OmitAssistantResponse = true,
				ProcessElementId = session.ProcessElementId?.ToString(),
				ContentType = CopilotContentType.Clarification,
				ForwardToClient = true,
			};
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Executes the code action using the specified execution options.
		/// </summary>
		/// <param name="options">The options specifying the parameters for the action execution.</param>
		/// <returns>
		/// A <see cref="T:Creatio.Copilot.Actions.CopilotActionExecutionResult" /> object containing the result of the action execution.
		/// </returns>
		public override CopilotActionExecutionResult Execute(ActionExecutionOptions options) {
			EnsureRequiredParameters(options, out string toolCallId, out string message);
			var sessionManager = ClassFactory.Get<ICopilotSessionManager>();
			if (!TryFindRootSession(sessionManager, options.CopilotSessionUId, toolCallId, out CopilotMessage resultMessage)) {
				return GetExecutionResult(resultMessage);
			}
			if (message.IsNullOrWhiteSpace()) {
				message = DefaultUserMessage;
			}
			var session = sessionManager.FindById(options.CopilotSessionUId);
			var waitingMessage = CreateWaitingMessage(toolCallId, session, message);
			return GetExecutionResult(waitingMessage);
		}

		/// <summary>Gets the caption of the executable code action.</summary>
		/// <remarks>
		/// The caption provides a localized, user-friendly name for the action.
		/// </remarks>
		public override LocalizableString GetCaption() {
			return new LocalizableString("Send message to user");
		}

		/// <summary>Gets the description of the executable code action.</summary>
		/// <remarks>
		/// The description provides a localized explanation of the action's purpose.
		/// </remarks>
		public override LocalizableString GetDescription() {
			return new LocalizableString("Send message to user");
		}

		#endregion

	}
}

