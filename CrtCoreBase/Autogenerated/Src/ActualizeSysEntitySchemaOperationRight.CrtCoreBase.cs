namespace Terrasoft.Configuration 
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using System.Collections.Generic;

	#region Class: ActualizeSysEntitySchemaOperationRightScriptExecutor

	public class ActualizeSysEntitySchemaOperationRightScriptExecutor : IInstallScriptExecutor {
		
		#region Fields: Private

		private readonly string _sysInstalledAppSchemaUId = "91d3eeb0-086c-4143-b671-dd2b77634d39";
		private readonly string _sysPackageInInstalledAppSchemaUId = "c5da825e-81b7-44cf-b07e-a7d73474b98c";
		private readonly string _allEmployeeAdminUnitId = "a29a3ba5-4b0d-de11-9a51-005056c00008";
		private readonly string _developerAdminUnitId = "ef35155c-c7db-485d-a9b1-b6a1f7d23412";

		#endregion

		#region Methods: Private

		private void AddPermissionsForDeveloper(UserConnection userConnection, string[] entitySchemaUIds) {
			foreach (string schemaUId in entitySchemaUIds) {
				var entity = userConnection.EntitySchemaManager.GetEntityByName("SysEntitySchemaOperationRight", userConnection);
				var condition = new Dictionary<string, object> {
						{ "SubjectSchemaUId", schemaUId },
						{ "SysAdminUnit", _developerAdminUnitId }
				};
				if (entity.FetchFromDB(condition)) {
					return;
				};
				entity.SetDefColumnValues();
				entity.SetColumnValue("SubjectSchemaUId", schemaUId);
				entity.SetColumnValue("SysAdminUnit", _developerAdminUnitId);
				entity.SetColumnValue("CanRead", true);
				entity.SetColumnValue("CanAppend", true);
				entity.SetColumnValue("CanEdit", true);
				entity.SetColumnValue("CanDelete", true);
				entity.SetColumnValue("Position", 1);
				entity.Save();
			}
		}
		
		#endregion

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			string[] entitySchemaUIds = new string[] { _sysInstalledAppSchemaUId, _sysPackageInInstalledAppSchemaUId };
			foreach (string schemaUId in entitySchemaUIds) {
				var entity = userConnection.EntitySchemaManager.GetEntityByName("SysEntitySchemaOperationRight", userConnection);
				var condition = new Dictionary<string, object> {
						{ "SubjectSchemaUId", schemaUId },
						{ "SysAdminUnit", _allEmployeeAdminUnitId }
				};
				if (entity.FetchFromDB(condition)) {
					entity.Delete();
				}
			}
			AddPermissionsForDeveloper(userConnection, entitySchemaUIds);
		}

		#endregion

	}

	#endregion

}

