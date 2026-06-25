namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.FeatureToggling;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Core;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;
	using ConfGenAIFeatures = Terrasoft.Configuration.GenAI.GenAIFeatures;

	#region Class: IntentToolExecutor

	/// <summary>
	/// Executor of the Intents tools.
	/// </summary>
	public class IntentToolExecutor : IToolExecutor, IToolExecutionTrigger
	{

		#region Fields: Private

		private readonly CopilotIntentSchema _instance;
		private readonly UserConnection _userConnection;
		private readonly ICopilotMsgChannelSender _msgChannelSender;
		private readonly IDocumentTool _documentTool;
		private readonly ICopilotSessionManager _copilotSessionManager;
		private readonly CopilotIntentSchemaManager _intentSchemaManager;

		#endregion

		#region Properties: Private

		private string MessageType => _instance.Type == CopilotIntentType.Agent ? "Agent" : "Skill";

		#endregion

		#region Properties: Public

		/// <summary>
		/// Gets a value indicating whether confirmation is required before proceeding with the intent.
		/// </summary>
		public bool IsConfirmationRequired => false;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="IntentToolExecutor"/> class with the specified
		/// intent schema, user connection, message channel sender, and document tool.
		/// </summary>
		/// <param name="instance">The <see cref="CopilotIntentSchema"/>
		/// instance representing the intent schema.</param>
		/// <param name="userConnection">The <see cref="UserConnection"/>
		/// instance representing the current user connection.</param>
		/// <param name="msgChannelSender">The <see cref="ICopilotMsgChannelSender"/>
		/// instance for sending messages through the channel.</param>
		/// <param name="documentTool">The <see cref="IDocumentTool"/>
		/// instance for handling document-related operations.</param>
		/// /// <param name="copilotSessionManager">The <see cref="ICopilotSessionManager"/>
		/// instance for managing Copilot sessions.</param>
		public IntentToolExecutor(CopilotIntentSchema instance, UserConnection userConnection,
				ICopilotMsgChannelSender msgChannelSender, IDocumentTool documentTool,
				ICopilotSessionManager copilotSessionManager) {
			_instance = instance;
			_userConnection = userConnection;
			_msgChannelSender = msgChannelSender;
			_documentTool = documentTool;
			_copilotSessionManager = copilotSessionManager;
			_intentSchemaManager = _userConnection.GetIntentSchemaManager();
		}

		#endregion

		#region Methods: Private

		private void TryFillDocuments(CopilotSession session) {
			if (Features.GetIsDisabled<GenAIFeatures.UseFileHandling>() || _userConnection == null ||
					_documentTool == null) {
				return;
			}
			_documentTool.RemoveIntentDocumentsFromSession(session);
			IList<CreatioAIDocument> skillDocuments = _documentTool.GetDocuments(_userConnection,
				session.CurrentIntentId, session.RootIntentId);
			if (skillDocuments.Count > 0) {
				session.Documents.AddRange(skillDocuments);
				_copilotSessionManager.Update(session, null);
			}
		}

		private string GetCurrentIntentName(Guid? intentId, CopilotIntentType intentType) {
			if (_instance.Type == intentType) {
				return _instance.Name;
			}
			if (!intentId.HasValue) {
				return null;
			}
			CopilotIntentSchema skill = _intentSchemaManager.FindInstanceByUId(intentId.Value);
			return skill?.Type == intentType ? skill.Name : null;
		}

		private IntentToolResult CreateIntentToolResult(CopilotSession session) {
			string currentAgentName = GetCurrentIntentName(session.RootIntentId, CopilotIntentType.Agent);
			string currentSkillName = GetCurrentIntentName(session.CurrentIntentId, CopilotIntentType.Skill);
			var result = new IntentToolResult {
				EventType = $"{MessageType}Selected",
				Caption = _instance.Caption,
				Description = _instance.IntentDescription ?? _instance.Description,
				CurrentAgent = currentAgentName,
				CurrentSkill = currentSkillName
			};
			return result;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Executes the intent tool logic and generates a list of messages for the Copilot session.
		/// </summary>
		/// <param name="toolCallId">The unique identifier of the tool call.</param>
		/// <param name="arguments">A dictionary containing the arguments required for the tool execution.</param>
		/// <param name="session">The current <see cref="CopilotSession"/> instance where the tool is executed.</param>
		/// <returns>
		/// A list of <see cref="CopilotMessage"/> objects representing the messages generated during the tool execution.
		/// </returns>
		/// <remarks>
		/// This method sets the current intent for the session, constructs a tool message based on the intent type,
		/// and adds system and user messages to the session. It also attempts to populate the session with relevant
		/// document metadata.
		/// </remarks>
		public List<CopilotMessage> Execute(string toolCallId, Dictionary<string, object> arguments,
				CopilotSession session) {
			session.SetCurrentIntent(_instance.UId);
			var progressState = CopilotSessionProgressStates.SkillSelected;
			if (_instance.Type == CopilotIntentType.Agent) {
				session.RootIntentId = _instance.UId;
				progressState = CopilotSessionProgressStates.AgentSelected;
			}
			IntentToolResult toolResult = CreateIntentToolResult(session);
			string toolMessage = Json.Serialize(toolResult, true);
			_msgChannelSender.SendSessionProgress(CopilotSessionProgress.Create(_userConnection, session,
				progressState, _instance.Caption), _userConnection.CurrentUser.Id);
			var messages = new List<CopilotMessage> {
				CopilotMessage.FromTool(toolMessage, toolCallId),
				CopilotMessage.FromSystem($"# {MessageType} '{_instance.Name}'\n{_instance.FullPrompt}\n" +
					$"Now you should resolve the User's request or call additional tools.")
			};
			TryFillDocuments(session);
			return messages;
		}

		public CopilotSession TriggerExecution(CopilotSession session) {
			var randomCallId = CopilotExtensions.GenerateToolCallId();
			var toolCallMessage = CopilotMessage.FromAssistant(new ToolCall {
				Id = randomCallId,
				FunctionCall = new FunctionCall() {
					Name = _instance.Name,
					Arguments = string.Empty,
				}
			});

			session.AddMessage(toolCallMessage);
			var invokeAgentMessages = Execute(randomCallId, new Dictionary<string, object>(), session);
			session.AddMessages(invokeAgentMessages);

			return session;
		}

		#endregion

	}

	#endregion

}

