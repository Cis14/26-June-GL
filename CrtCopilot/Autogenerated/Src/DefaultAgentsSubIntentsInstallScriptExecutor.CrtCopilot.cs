namespace Terrasoft.Configuration
{
	using System;
	using Terrasoft.Core;
	using Creatio.Copilot.Designer.Commands;
	using Creatio.FeatureToggling;
	using global::Common.Logging;

	#region Class: DefaultAgentsSubIntentsInstallScriptExecutor

	public class DefaultAgentsSubIntentsInstallScriptExecutor : IInstallScriptExecutor
	{

		#region Fields: Private

		private static readonly Guid _defaultAgentUid = new Guid("7439e0df-4e1a-7a35-641d-9a2907b0b8e3");
		private readonly ILog _log = LogManager.GetLogger("Copilot");

		#endregion

		#region Methods: Private

		private void RunAssignFreeSkillsCommand(UserConnection userConnection) {
			var assignFreeSkillsToDefaultAgentCommand = new AssignFreeSkillsToDefaultAgentCommand(userConnection, _defaultAgentUid);
			assignFreeSkillsToDefaultAgentCommand.Execute();
		}

		#endregion

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			try {
				RunAssignFreeSkillsCommand(userConnection);
			} catch (Exception ex) {
				_log.Error("Failed to execute DefaultAgentsSubIntentsInstallScriptExecutor", ex);
			}
		}

		#endregion

	}

	#endregion

}

