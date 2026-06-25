 namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Entities;

	public class RenameEntityCollectionMemoryLimitSystemSetting : IInstallScriptExecutor
	{
		private const string TargetSysSettingCode = "MaxMemoryUsageToGetDataViaEntityCollection";
		private const string NewSysSettingName = "RAM limit for data selection via Entity collection";
		private const string NewSysSettingTitle = "The maximum RAM size in megabytes that Creatio can use to process requests for data selection via Entity collection. Applies to OData 3 and 4, Data Service and direct Entity API usage in source code. This protects end users from performance issues that can be caused by inefficient integrations. If you want to change the value of the system setting, make sure the RAM size you are going to set is optimal for both user work and integration operation";
		public void Execute(UserConnection userConnection)
		{
			if (userConnection == null) {
				throw new ArgumentNullException("userConnection");
			}
			if (string.IsNullOrWhiteSpace(NewSysSettingName) && string.IsNullOrWhiteSpace(NewSysSettingTitle)) {
				return;
			}
			EntitySchemaManager entitySchemaManager = userConnection.EntitySchemaManager;
			Entity sysSettingEntity = entitySchemaManager.GetEntityByName("SysSettings", userConnection);
			sysSettingEntity.UseAdminRights = true;
			var conditions = new Dictionary<string, object>	{
				{ "Code", TargetSysSettingCode }
			};
			bool exists = sysSettingEntity.FetchFromDB(conditions);
			if (!exists) {
				return;
			}
			if (!string.IsNullOrWhiteSpace(NewSysSettingName)) {
				sysSettingEntity.SetColumnValue("Name", NewSysSettingName);
			}
			if (!string.IsNullOrWhiteSpace(NewSysSettingTitle))	{
				sysSettingEntity.SetColumnValue("Description", NewSysSettingTitle);
			}
			sysSettingEntity.Save();
		}
	}
}

