namespace Terrasoft.Configuration
{
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: RenameSkillRightsFeatureNameInstallScript

	public class RenameSkillRightsFeatureNameInstallScript : IInstallScriptExecutor
	{

		#region Constans: Private

		private const string OldFeatureName = "GenAIFeatures.UseSkillSchemaOperationRights";
		private const string NewFeatureName = "GenAIFeatures.UseIntentSchemaOperationRights";
		private const string NewFeatureDescription = "Enable Access Rights for Skills/Agents.";

		#endregion

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			Entity feature = userConnection.EntitySchemaManager.GetEntityByName("Feature", userConnection);
			var conditions = new Dictionary<string, object> {
				{ "Code", OldFeatureName }
			};
			if (feature.FetchFromDB(conditions, false)) {
				feature.SetColumnValue("Name", NewFeatureName);
				feature.SetColumnValue("Code", NewFeatureName);
				feature.SetColumnValue("Description", NewFeatureDescription);
				feature.Save(false);
			}
		}

		#endregion

	}

	#endregion

}

