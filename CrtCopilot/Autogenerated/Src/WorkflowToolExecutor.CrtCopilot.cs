using System;
using System.Collections.Generic;
using System.Linq;
using Terrasoft.Common;
using Terrasoft.Common.Json;
using Terrasoft.Core;
using Terrasoft.Enrichment.Interfaces.ChatCompletion;
using Creatio.FeatureToggling;

namespace Creatio.Copilot
{
	/// <summary>
	/// Executes workflow-based tools for copilot agents.
	/// </summary>
	/// <remarks>
	/// This executor handles the execution of workflow agents, managing session state
	/// and triggering business process workflows through the copilot interface.
	/// </remarks>
	internal class WorkflowToolExecutor : IToolExecutor, IToolExecutionTrigger
	{

		#region Fields: Private

		private readonly CopilotIntentSchema _instance;
		private readonly ICopilotWorkflowService _workflowService;
		private readonly CopilotIntentSchemaManager _intentSchemaManager;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowToolExecutor"/> class.
		/// </summary>
		/// <param name="instance">The copilot intent schema instance.</param>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="workflowService">The copilot workflow service.</param>
		public WorkflowToolExecutor(CopilotIntentSchema instance, UserConnection userConnection,
				 ICopilotWorkflowService workflowService) {
			_instance = instance;
			_workflowService = workflowService;
			_intentSchemaManager = userConnection.GetIntentSchemaManager();
		}

		#endregion

		#region Methods: Private

		private static void AddSyncWorkflowResults(CopilotSession session, List<CopilotMessage> toolMessages) {
			const int agentToolInvocationMessagesCountToSkip = 1;
			if (toolMessages.Count > agentToolInvocationMessagesCountToSkip) {
				session.AddMessages(toolMessages.Skip(agentToolInvocationMessagesCountToSkip));
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
				EventType = "Workflow Agent Selected",
				Caption = _instance.Caption,
				Description = _instance.IntentDescription ?? _instance.Description,
				CurrentAgent = currentAgentName,
				CurrentSkill = currentSkillName
			};
			return result;
		}

		private List<CopilotMessage> GetToolResultMessages(string toolCallId, CopilotSession session) {
			IntentToolResult toolResult = CreateIntentToolResult(session);
			string toolMessage = Json.Serialize(toolResult, true);
			CopilotMessage selectedWorkflowMessage = CopilotMessage.FromTool(toolMessage, toolCallId);
			if (Features.GetIsEnabled<GenAIFeatures.DisableAssistantResponseOnWorkflowCompletion>()) {
				selectedWorkflowMessage.OmitAssistantResponse = true;
			}
			var messages = new List<CopilotMessage> {
				selectedWorkflowMessage
			};
			return messages;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public bool IsConfirmationRequired {
			get => false;
		}

		/// <inheritdoc />
		public List<CopilotMessage> Execute(string toolCallId, Dictionary<string, object> arguments,
				CopilotSession session) {
			session.SetCurrentIntent(_instance.UId);
			session.RootIntentId = _instance.UId;
			List<CopilotMessage> messages = GetToolResultMessages(toolCallId, session);
			Dictionary<string, string> customParameters =
				arguments.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.ToString() ?? string.Empty);
			if (_instance.Type != CopilotIntentType.WorkflowAgent) {
				throw new InvalidOperationException(
					$"IntentToolExecutor can execute only WorkflowAgent type. Current type: {_instance.Type}.");
			}
			if (_instance.Workflow.WorkflowSchemaUId.IsEmpty()) {
				throw new InvalidObjectStateException($"No workflow is attached to the selected agent {_instance.UId}" +
					$" in session {session.Id} for workflow start.");
			}
			var toolCalls = _workflowService.Start(session,
				_instance.Workflow.WorkflowSchemaUId, customParameters);
			messages.AddRange(toolCalls);
			return messages;
		}

		/// <inheritdoc />
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
			session.AddMessages(GetToolResultMessages(randomCallId, session));
			List<CopilotMessage> toolMessages = Execute(randomCallId, new Dictionary<string, object>(), session);
			AddSyncWorkflowResults(session, toolMessages);
			return session;
		}

		#endregion

	}
}

