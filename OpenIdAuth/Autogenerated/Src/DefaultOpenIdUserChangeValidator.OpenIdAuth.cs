 namespace Terrasoft.Configuration.OpenIdAuth
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using SysSettings = Terrasoft.Core.Configuration.SysSettings;

	#region Class: DefaultOpenIdUserChangeValidator

	[DefaultBinding(typeof(IOpenIdUserChangeValidator))]
	public class DefaultOpenIdUserChangeValidator : IOpenIdUserChangeValidator
	{

		#region Methods: Public

		public bool CanChangeUser(UserConnection userConnection, Entity sysAdminUnitModified) {
			bool allowManualUserRoleModification =
				SysSettings.GetValue(userConnection, "OpenIdAllowManualUserRoleModification", false);
			if (allowManualUserRoleModification) {
				return true;
			}
			Guid userId = sysAdminUnitModified.GetTypedColumnValue<Guid>("Id");
			var schema = userConnection.EntitySchemaManager.GetInstanceByName("SysAdminUnit");
			var sysAdminUnit = schema.CreateEntity(userConnection);
			string[] columnsToFetch = { "OpenIDSub", "Active" };
			if (!sysAdminUnit.FetchFromDB(sysAdminUnit.Schema.PrimaryColumn.Name, userId, columnsToFetch)) {
				return true;
			}
			if (string.IsNullOrEmpty(sysAdminUnit.GetTypedColumnValue<string>("OpenIDSub"))) {
				return true;
			}
			if (sysAdminUnitModified.GetTypedColumnValue<bool>("Active") == sysAdminUnit.GetTypedColumnValue<bool>("Active")) {
				return true;
			}
			return false;
		}

		#endregion

	}

	#endregion

} 
