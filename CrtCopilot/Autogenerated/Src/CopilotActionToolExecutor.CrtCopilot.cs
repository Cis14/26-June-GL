namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.Copilot.Actions;
	using Creatio.Copilot.Metadata;
	using Creatio.FeatureToggling;
	using global::Common.Logging;
	using Newtonsoft.Json.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using SystemSettings = Terrasoft.Core.Configuration.SysSettings;
	using Newtonsoft.Json;

	#region Class: CopilotActionToolExecutor

	/// <summary>
	/// Executor of the Actions tools.
	/// </summary>
	public class CopilotActionToolExecutor : IToolExecutor
	{

		#region Consts: Private

		private const int DefaultTextColumnLengthLimit = 1000;
		private const string AsyncFnTemporaryResultMessage =
			"The tool is executing and has not been completed yet. You will receive the results a bit later in another message. Please wait and avoid re-executing the tool until then. Kindly ask the user to be patient.";
		private const string AsyncFnNotSupportedMessage = "The session does not support asynchronous execution of tools.";

		#endregion

		#region Fields: Private

		private readonly ILog _log = LogManager.GetLogger("Copilot");
		private readonly CopilotActionMetaItem _instance;
		private readonly UserConnection _userConnection;
		private readonly ICopilotMsgChannelSender _msgChannelSender;
		private readonly bool _isHtmlFiltrationDisabled = Features.GetIsEnabled("DisableCopilotOutputParamsHtmlFiltration");

		#endregion

		#region Constructors: Public

		public CopilotActionToolExecutor(CopilotActionMetaItem instance, UserConnection userConnection,
				ICopilotMsgChannelSender msgChannelSender) {
			_instance = instance;
			_userConnection = userConnection;
			_msgChannelSender = msgChannelSender;
		}

		#endregion

		#region Properties: Private

		private int? _textColumnLengthLimit;
		private int TextColumnLengthLimit {
			get {
				if (!_textColumnLengthLimit.HasValue) {
					_textColumnLengthLimit = SystemSettings.GetValue(_userConnection, "CopilotTextColumnLengthLimit",
						DefaultTextColumnLengthLimit);
				}
				return _textColumnLengthLimit.Value;
			}
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Gets a value indicating whether confirmation is required for the current action.
		/// </summary>
		public bool IsConfirmationRequired => _instance?.IsConfirmRequired ?? false;

		#endregion

		#region Methods: Private

		private void ShortenAllProperties(JToken token) {
			if (token == null) {
				return;
			}
			if (token.Type == JTokenType.Object) {
				foreach (var property in (JObject)token) {
					ShortenAllProperties(property.Value);
				}
			} else if (token.Type == JTokenType.Array) {
				foreach (var element in (JArray)token) {
					ShortenAllProperties(element);
				}
			} else if (token.Type == JTokenType.String) {
				var sourceString = token.ToString();
				var lengthLimit = Math.Min(sourceString.Length, Math.Max(TextColumnLengthLimit, 0));
				var newString = _isHtmlFiltrationDisabled
					? sourceString.Substring(0, lengthLimit)
					: StringUtilities.GetPlainTextFromHtml(sourceString, lengthLimit);
				if (newString.Length == TextColumnLengthLimit) {
					newString += "...";
				}
				((JValue)token).Value = newString;
			}
		}

		private string ShortenActionResponse(string response) {
			if (_instance.Name != "RetrieveEntityData") {
				return response;
			}
			try {
				var jObject = JObject.Parse(response);
				ShortenAllProperties(jObject);
				return jObject.ToString(Formatting.None);
			} catch(Exception e) {
				_log.ErrorFormat($"Action {_instance.Name} returned response in an unexpected format: {response}", e);
				return response.Truncate(TextColumnLengthLimit);
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Executes the tool call from Copilot as Action.
		/// </summary>
		/// <param name="toolCallId">Tool call id.</param>
		/// <param name="arguments">Args.</param>
		/// <param name="session">Copilot session.</param>
		/// <returns>Message as response for the tool call.</returns>
		public List<CopilotMessage> Execute(string toolCallId, Dictionary<string, object> arguments,
					CopilotSession session) {
			Dictionary<string, string> stringifiedArgs = arguments != null
				? arguments.ToDictionary(k => k.Key, k => k.Value?.ToString())
				: new Dictionary<string, string>();
			if (!session.IsTransient) {
				_msgChannelSender.SendSessionProgress(CopilotSessionProgress.Create(_userConnection, session,
						CopilotSessionProgressStates.ExecutingAction, _instance.Descriptor.Caption),
					_userConnection.CurrentUser.Id);
			}
			CopilotAction action = _instance.CreateActionInstance(_userConnection);
			CopilotActionExecutionResult result;
			try {
				result = action.Run(new CopilotActionExecutionOptions {
					InstanceUId = toolCallId,
					CopilotSessionUId = session.Id,
					ParameterValues = stringifiedArgs
				});
			} catch (Exception e) {
				_log.Error(e);
				result = new CopilotActionExecutionResult {
					ErrorMessage = e.Message,
					Status = CopilotActionExecutionStatus.Failed
				};
			}
			var messages = new List<CopilotMessage>();
			if (result == null) {
				var copilotMessage = new CopilotMessage(string.Empty, CopilotMessageRole.Tool, toolCallId) {
					OmitAssistantResponse = session.Messages.Any(
						m => m.IsPendingClarification() || m.IsPendingConfirmation())
				};
				if (session.IsTransient) {
					if (session.ProcessElementId.GetValueOrDefault() != Guid.Empty) {
						copilotMessage.Content = AsyncFnTemporaryResultMessage;
						copilotMessage.ProcessElementId = session.ProcessElementId.ToString();
						copilotMessage.ContentType = CopilotContentType.AsyncAction;
						copilotMessage.OmitAssistantResponse = true;
					} else {
						copilotMessage.Content = AsyncFnNotSupportedMessage;
					}
				} else {
					copilotMessage.Content = AsyncFnTemporaryResultMessage;
				}
				messages.Add(copilotMessage);
			} else {
				string content = result.Status == CopilotActionExecutionStatus.Failed
					? result.ErrorMessage.ToNullIfEmpty() ?? "Unknown error"
					: ShortenActionResponse(result.Response);
				var message = new CopilotMessage(content, CopilotMessageRole.Tool, toolCallId) {
					TruncateOnSave = result.ResponseOptions?.TruncateContentOnSave ?? false,
					ForwardToClient = result.ResponseOptions?.ForwardToClient ?? false,
					ContentType = result.ResponseOptions?.ContentType,
					OmitAssistantResponse = result.ResponseOptions?.OmitAssistantResponse ?? false
				};
				messages.Add(message);
			}
			return messages;
		}

		#endregion

	}

	#endregion

}

