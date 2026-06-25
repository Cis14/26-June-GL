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
	/// Sends a client-side notification message to the user within the current Copilot chat session.
	/// </summary>
	public class NotifyUserAction : BaseExecutableCodeAction
	{

		#region Constants: Private

		private const string DefaultUserMessage = "Your workflow is in progress.";

		#endregion

		#region Fields: Private

		private readonly ILog _log = LogManager.GetLogger("Copilot");

		#endregion

		#region Constructors: Public

		public NotifyUserAction() {
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

		private CopilotActionExecutionResult GetExecutionResult(string messageContent) {
			return new CopilotActionExecutionResult {
				Status = CopilotActionExecutionStatus.Completed,
				Response = messageContent,
				ResponseOptions = new ActionResponseOptions() {
					ContentType = CopilotContentType.Notification,
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
			out string resultMessage) {
			resultMessage = null;
			var session = sessionManager.FindById(sessionId);
			if (session.RootSessionId != null && sessionManager.FindById((Guid)session.RootSessionId) != null) {
				return true;
			}
			_log.Warn($"Root session '{session.RootSessionId}' not found for NotifyUserAction.");
			resultMessage = "Unable to notify user because the chat session was not found.";
			return false;
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
			if (!TryFindRootSession(sessionManager, options.CopilotSessionUId, toolCallId, out string resultMessage)) {
				return GetExecutionResult(resultMessage);
			}
			if (message.IsNullOrWhiteSpace()) {
				message = DefaultUserMessage;
			}
			return GetExecutionResult(message);
		}

		/// <summary>Gets the caption of the executable code action.</summary>
		/// <remarks>
		/// The caption provides a localized, user-friendly name for the action.
		/// </remarks>
		public override LocalizableString GetCaption() {
			return new LocalizableString("Notify user");
		}

		/// <summary>Gets the description of the executable code action.</summary>
		/// <remarks>
		/// The description provides a localized explanation of the action's purpose.
		/// </remarks>
		public override LocalizableString GetDescription() {
			return new LocalizableString("Notify user");
		}

		#endregion

	}
}

