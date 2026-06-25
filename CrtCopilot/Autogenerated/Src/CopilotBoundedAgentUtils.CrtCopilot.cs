namespace Creatio.Copilot
{
	using System;
	using Creatio.FeatureToggling;
	using Terrasoft.Core.Factories;

	#region Interface: ICopilotBoundedAgentUtils

	/// <summary>
	/// Defines accessors for reading and updating <see cref="CopilotSession.BoundedIntentId"/>
	/// under guided-session (orchestrated) rules, including Creatio AI as the unbonded state.
	/// </summary>
	public interface ICopilotBoundedAgentUtils
	{
		bool IsAgentBounded(CopilotSession session);
		void ClearBoundedAgent(CopilotSession session);
		Guid? GetBoundedAgentIntentId(CopilotSession session);
		bool SetBoundedAgent(CopilotSession session, Guid? intentId);
		void EnsureBoundedAgentAssigned(CopilotSession session);
	}

	#endregion

	#region Class: CopilotBoundedAgentUtils

	/// <summary>
	/// Bounded agent rules: BoundedIntentId is set only by explicit bonding flows and cleared only
	/// by explicit unbond (exit or mention switch to Creatio AI). It is not cleared on workflow completion.
	/// </summary>
	[DefaultBinding(typeof(ICopilotBoundedAgentUtils))]
	internal class CopilotBoundedAgentUtils : ICopilotBoundedAgentUtils
	{
		#region Fields: Private

		private static readonly Guid CreatioAiIntentId = new Guid("7439e0df-4e1a-7a35-641d-9a2907b0b8e3");

		#endregion

		#region Methods: Private

		private static void AddSystemPrompt(CopilotSession session, string prompt) {
			if (session == null || string.IsNullOrWhiteSpace(prompt)) {
				return;
			}
			CopilotMessage message = CopilotMessage.FromSystem(prompt);
			message.IsFromSystemPrompt = true;
			session.AddMessage(message);
		}

		#endregion

		#region Methods: Public

		public bool IsAgentBounded(CopilotSession session) {
			return GetBoundedAgentIntentId(session) != null;
		}

		public void ClearBoundedAgent(CopilotSession session) {
			if (session == null) {
				return;
			}
			Guid? previousBoundedId = GetBoundedAgentIntentId(session);
			SetBoundedAgent(session, null);
			session.RootIntentId = null;
			session.CurrentIntentId = null;
			if (previousBoundedId.HasValue) {
				AddSystemPrompt(session, CopilotSessionResponseHandlerPrompts.BoundedSessionEndPrompt);
			}
		}

		public Guid? GetBoundedAgentIntentId(CopilotSession session) {
			if (session == null || Features.GetIsDisabled<GenAIFeatures.EnableGuidedSession>()) {
				return null;
			}
			Guid? boundedId = session.BoundedIntentId;
			if (!boundedId.HasValue || boundedId.Value == Guid.Empty) {
				return null;
			}
			return boundedId;
		}

		public bool SetBoundedAgent(CopilotSession session, Guid? intentId) {
			if (session == null || Features.GetIsDisabled<GenAIFeatures.EnableGuidedSession>()) {
				return false;
			}
			Guid? previousBoundedId = GetBoundedAgentIntentId(session);
			if (intentId.HasValue && intentId.Value == CreatioAiIntentId) {
				session.BoundedIntentId = null;
				return true;
			}
			session.BoundedIntentId = intentId;
			if (intentId.HasValue && intentId.Value != CreatioAiIntentId && previousBoundedId != intentId) {
				AddSystemPrompt(session, CopilotSessionResponseHandlerPrompts.BoundedSessionStartPrompt);
			}
			return true;
		}

		public void EnsureBoundedAgentAssigned(CopilotSession session) {
			if (!IsAgentBounded(session) && session.RootIntentId.HasValue) {
				SetBoundedAgent(session, session.RootIntentId.Value);
			}
		}

		#endregion

	}

	#endregion

}

