 namespace Terrasoft.Configuration
{
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: UpdateOpenIDProvisionSettingInstallScriptExecutor

	internal class UpdateOpenIDProvisionSettingInstallScriptExecutor : IInstallScriptExecutor
	{

		#region Constants: Private

		const string SsoSamlSettingsSchemaName = "SsoSamlSettings";
		const string SsoOpenIdSettingsSchemaName = "SsoOpenIdSettings";
		const string SsoServiceProviderSchemaName = "SsoServiceProvider";
		const string UseJitColumnName = "UseJit";
		const string SspUseJitColumnName = "SspUseJit";
		const string FeatureName = "AllowDisablingOfUserProvisionDuringOpenIdLogin";

		#endregion

		#region Methods: Private

		private EntitySchemaQuery GetEntitySchemaQuery(UserConnection userConnection, string schemaName) {
			var openIdEntitySchemaQuery =
				new EntitySchemaQuery(userConnection.EntitySchemaManager, schemaName);
			openIdEntitySchemaQuery.PrimaryQueryColumn.IsAlwaysSelect = true;
			return openIdEntitySchemaQuery;
		}

		private void DisableFeature(UserConnection userConnection) {
			var feature = userConnection.EntitySchemaManager.GetEntityByName("Feature", userConnection);
			Dictionary<string, object> conditions = new Dictionary<string, object> {
				{ "Name", FeatureName }
			};
			if (feature.FetchFromDB(conditions, false)) {
				feature.SetColumnValue("DefaultState", false);
				feature.Save();
			}
		}

		#endregion

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			var samlEntitySchemaQuery = GetEntitySchemaQuery(userConnection, SsoSamlSettingsSchemaName);
			var openIdEntitySchemaQuery = GetEntitySchemaQuery(userConnection, SsoOpenIdSettingsSchemaName);
			var samlSettings = samlEntitySchemaQuery.GetEntityCollection(userConnection);
			var openIdSettings = openIdEntitySchemaQuery.GetEntityCollection(userConnection);
			if (openIdSettings.Count > 0 && samlSettings.Count > 0) {
				DisableFeature(userConnection);
			}
			var settingsProviderSchemaQuery = GetEntitySchemaQuery(userConnection, SsoServiceProviderSchemaName);
			settingsProviderSchemaQuery.AddColumn(SspUseJitColumnName);
			settingsProviderSchemaQuery.AddColumn(UseJitColumnName);
			EntityCollection settingsProviderCollection =
				settingsProviderSchemaQuery.GetEntityCollection(userConnection);
			if (openIdSettings.Count > 0 && samlSettings.Count == 0) {
				foreach (var settings in settingsProviderCollection) {
					settings.SetColumnValue(UseJitColumnName, true);
					settings.SetColumnValue(SspUseJitColumnName, true);
					settings.Save();
				}
			}
		}

		#endregion

	}

	#endregion

}
