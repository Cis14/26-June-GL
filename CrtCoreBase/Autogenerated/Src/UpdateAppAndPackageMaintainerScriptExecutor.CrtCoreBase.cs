namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: UpdateAppAndPackageMaintainer

	internal class UpdateAppAndPackageMaintainerScriptExecutor: IInstallScriptExecutor
	{

		#region Fields: Private

		private static readonly String _maintainerTerrasoft = "Terrasoft";
		private static readonly String _maintainerCreatio = "Creatio";

		#endregion

		#region Methods: Private
		
		private void _updateMaintainer(UserConnection userConnection, string schemaName) {			
			Entity entity = userConnection.EntitySchemaManager.GetEntityByName(schemaName, userConnection);
			var esq = new EntitySchemaQuery(entity.Schema);
			EntitySchemaQueryColumn idColumn = esq.AddColumn("Id");
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				"Maintainer", _maintainerTerrasoft));
			EntityCollection collection = esq.GetEntityCollection(userConnection);
			foreach (Entity record in collection) {
				var conditions = new Dictionary<string, object> {
					{ "Id", record.GetTypedColumnValue<Guid>(idColumn.Name) }
				};
                entity.FetchFromDB(conditions);
				entity.SetColumnValue("Maintainer", _maintainerCreatio);				
				entity.Save();
			}
		}

		#endregion

		#region Methods: Public
 
        public void Execute(UserConnection userConnection) {
            this._updateMaintainer(userConnection, "SysInstalledApp");
			this._updateMaintainer(userConnection, "SysPackage");
        }

		#endregion

	}

	#endregion

}
