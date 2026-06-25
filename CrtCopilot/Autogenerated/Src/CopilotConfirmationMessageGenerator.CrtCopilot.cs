namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Common.Logging;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	[DefaultBinding(typeof(ICopilotConfirmationMessageGenerator))]
	public class CopilotConfirmationMessageGenerator : ICopilotConfirmationMessageGenerator
	{

		#region Fields: Private

		private readonly ILog _log = LogManager.GetLogger("Copilot");
		private readonly UserConnection _userConnection;

		#endregion

		#region Constants: Private

		private const string ParameterActionsData = "ActionsData";
		private const string ConfirmationButtonAction = "sendMessage";
		private const string IntentName = "SkillGenerateConfirmationMessage";
		private const string FallBackCancelButtonLsCode = "FallBackCancelButton";
		private const string FallBackConfirmButtonLsCode = "FallBackConfirmButton";
		private const string FallBackConfirmationMessageLsCode = "FallBackConfirmationMessage";

		#endregion

		#region Constructors: Public

		public CopilotConfirmationMessageGenerator(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		private static CopilotIntentCall GetIntentCall(CopilotConfirmationToolContextModel toolContext) {
			return new CopilotIntentCall {
				IntentName = IntentName,
				Parameters = new Dictionary<string, object> {
					{ ParameterActionsData, toolContext }
				}
			};
		}

		private CopilotConfirmationMessageModel TryGenerateModel(CopilotConfirmationToolContextModel toolContext) {
			try {
				CopilotIntentCall intentCall = GetIntentCall(toolContext);
				CopilotIntentCallResult response = _userConnection.CopilotEngine.ExecuteIntent(intentCall);
				string content = response.Content;
				if (!string.IsNullOrWhiteSpace(content) && response.IsSuccess) {
					return new CopilotConfirmationMessageModel {
						MessageBody = response.ResultParameters["MessageBody"]?.ToString(),
						ConfirmButtonTitle = response.ResultParameters["ConfirmButtonTitle"]?.ToString(),
						CancelButtonTitle = response.ResultParameters["CancelButtonTitle"]?.ToString()
					};
				}
				_log.Error(
					$"CopilotConfirmationMessageGenerator: Received empty response from copilot service.\n{response.ErrorMessage}");
				return null;
			} catch (OperationCanceledException ex) {
				_log.Error($"CopilotConfirmationMessageGenerator: Operation was canceled.\n{ex.Message}");
				throw;
			}
			catch (Exception ex) {
				_log.Error($"CopilotConfirmationMessageGenerator: Failed to generate confirmation message model.\n{ex.Message}");
				return null;
			} 
		}

		private CopilotConfirmationMessageModel GetFallbackModel() =>
			new CopilotConfirmationMessageModel() {
				CancelButtonTitle = _userConnection.GetLocalizableString(FallBackCancelButtonLsCode, nameof(CopilotConfirmationMessageGenerator)).ToString(),
				MessageBody = _userConnection.GetLocalizableString(FallBackConfirmationMessageLsCode, nameof(CopilotConfirmationMessageGenerator)).ToString(),
				ConfirmButtonTitle = _userConnection.GetLocalizableString(FallBackConfirmButtonLsCode, nameof(CopilotConfirmationMessageGenerator)).ToString(),
			};

		private static CopilotMessage BuildCopilotMessage(CopilotConfirmationMessageModel messageModel, CopilotConfirmationToolContextModel toolContext) {
			string[] toolCallIds = (toolContext.Actions ?? new List<CopilotConfirmationToolActionModel>())
				.Where(x => !string.IsNullOrWhiteSpace(x.ToolCallId))
				.Select(toolCall => toolCall.ToolCallId)
				.ToArray();
			
			List<CopilotMessageButton> buttons = CreateButtons(messageModel);
			
			return new CopilotMessage(messageModel.MessageBody, CopilotMessageRole.Assistant) {
				ContentType = CopilotContentType.Confirmation,
				OmitAssistantResponse = true,
				ForwardToClient = true,
				ConfirmationResult = null,
				Buttons = buttons,
				ToolCallIdsRequireConfirmation = string.Join(",", toolCallIds)
			};
		}

		private static List<CopilotMessageButton> CreateButtons(CopilotConfirmationMessageModel m) =>
			new List<CopilotMessageButton>() {
				new CopilotMessageButton(m.ConfirmButtonTitle, ConfirmationButtonAction, CopilotButtonCode.Yes),
				new CopilotMessageButton(m.CancelButtonTitle, ConfirmationButtonAction, CopilotButtonCode.No)
			};

		#endregion

		#region Methods: Public

		/// <summary>
		/// Generates a confirmation message based on the provided tool context.
		/// </summary>
		/// <param name="toolContext">The context model containing description and parameters for the confirmation message.</param>
		/// <returns>A <see cref="CopilotMessage"/> representing the confirmation message.</returns>
		public CopilotMessage GenerateConfirmationMessage(CopilotConfirmationToolContextModel toolContext) {
			CopilotConfirmationMessageModel model = TryGenerateModel(toolContext) ?? GetFallbackModel();
			return BuildCopilotMessage(model, toolContext);
		}

		#endregion

	}
}

