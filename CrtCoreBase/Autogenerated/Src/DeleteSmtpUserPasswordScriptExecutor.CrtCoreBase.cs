 namespace Terrasoft.Configuration 
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using System.Collections.Generic;

	#region Class: DeleteSmtpUserPasswordScriptExecutor

	public class DeleteSmtpUserPasswordScriptExecutor : IInstallScriptExecutor {

		private EntitySchema GetEntitySchema(UserConnection userConnection, string schemaName) {
			EntitySchemaManager entitySchemaManager = userConnection.EntitySchemaManager;
			ISchemaManagerItem<EntitySchema> managerItem = entitySchemaManager.GetItemByName(schemaName);
			return entitySchemaManager.GetRuntimeInstanceFromMetadata(managerItem.UId);
		}

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			var ssCode = "SmtpUserPassword";

			var sysSettingEntity = userConnection.EntitySchemaManager.GetEntityByName("SysSettings", userConnection);
			var settingsCondition = new Dictionary<string, object> {
					{ "Code", ssCode }
			};

			if (sysSettingEntity.FetchFromDB(settingsCondition)) {
				var settingId = sysSettingEntity.GetTypedColumnValue<Guid>("Id");

				EntitySchema sysSettingsValueEntitySchema = GetEntitySchema(userConnection, "SysSettingsValue");
				var esq = new EntitySchemaQuery(sysSettingsValueEntitySchema);
				esq.AddAllSchemaColumns();
				esq.Filters.Add(esq.CreateFilterWithParameters(
					FilterComparisonType.Equal, "SysSettings", settingId));

				var sysValues = esq.GetEntityCollection(userConnection);

				bool allEmpty = true;
				foreach (var sysValue in sysValues) {
					if (!string.IsNullOrEmpty(sysValue.GetTypedColumnValue<string>("TextValue"))) {
						allEmpty = false;
						break;
					}
				}

				if (allEmpty) {
					for (int i = 0; i < sysValues.Count; i++) {
						sysValues[i].Delete();
					}
					sysSettingEntity.Delete();
				}
			}
		}
		#endregion

	}

	#endregion

}

