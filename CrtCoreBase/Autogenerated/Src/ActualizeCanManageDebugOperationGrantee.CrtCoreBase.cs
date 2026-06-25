namespace Terrasoft.Configuration 
{
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: ActualizeCanManageDebugOperationGranteeScriptExecutor

	public class ActualizeCanManageDebugOperationGranteeScriptExecutor : IInstallScriptExecutor {
		
		#region Fields: Private

		private readonly string _canManageDebugOperationId = "07117592-3100-453c-b8e9-b0c962e85f56";
		private readonly string _developerAdminUnitId = "ef35155c-c7db-485d-a9b1-b6a1f7d23412";
		private readonly bool _developerAdminUnitCanExecute = true;
		private readonly int _developerAdminUnitPosition = 1;

		#endregion

		#region Methods: Private

		private void AddOperationGranteeForDeveloper(UserConnection userConnection) {
			var adminUnitEntity = userConnection.EntitySchemaManager.GetEntityByName("SysAdminUnit", userConnection);
			var adminUnitCondition = new Dictionary<string, object> {
						{ "Id", _developerAdminUnitId }
				};
			if (!adminUnitEntity.FetchFromDB(adminUnitCondition)) {
				return;
			}
			var granteeEntity = userConnection.EntitySchemaManager.GetEntityByName("SysAdminOperationGrantee", userConnection);
			var condition = new Dictionary<string, object> {
						{ "SysAdminOperation", _canManageDebugOperationId },
						{ "SysAdminUnit", _developerAdminUnitId }
				};
			if (granteeEntity.FetchFromDB(condition)) {
				return;
			}
			granteeEntity.SetDefColumnValues();
			granteeEntity.SetColumnValue("SysAdminOperationId", _canManageDebugOperationId);
			granteeEntity.SetColumnValue("SysAdminUnitId", _developerAdminUnitId);
			granteeEntity.SetColumnValue("CanExecute", _developerAdminUnitCanExecute);
			granteeEntity.SetColumnValue("Position", _developerAdminUnitPosition);
			granteeEntity.Save();
		}
		
		#endregion

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			AddOperationGranteeForDeveloper(userConnection);
		}

		#endregion

	}

	#endregion

}
