 namespace Terrasoft.Configuration
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	public class UpdateMobileSectionSchemaUIdsScript : IInstallScriptExecutor {

		private readonly Guid _accountSysModuleId = new Guid("EE586F85-5E21-4247-9AB2-1D906F0F0B46");
		private readonly Guid _contactSysModuleId = new Guid("24E07446-3A40-49F5-81D1-C20CB4C0CDC4");
		private readonly Guid _accountListPageUId = new Guid("01F88BDE-7BCC-431A-8CBF-EAF56AA954CA");
		private readonly Guid _contactListPageUId = new Guid("756C4BE6-8CDA-4FD8-BD87-BBA883619787");

		public void Execute(UserConnection userConnection) {
			Entity sysModule = userConnection.EntitySchemaManager.GetEntityByName("SysModule", userConnection);
			if (sysModule.FetchFromDB(_accountSysModuleId)) {
				if (sysModule.GetTypedColumnValue<Guid>("MobileSectionSchemaUId") == Guid.Empty) {
					sysModule.SetColumnValue("MobileSectionSchemaUId", _accountListPageUId);
					sysModule.Save(false);
				}
			}
			if (sysModule.FetchFromDB(_contactSysModuleId)) {
				if (sysModule.GetTypedColumnValue<Guid>("MobileSectionSchemaUId") == Guid.Empty) {
					sysModule.SetColumnValue("MobileSectionSchemaUId", _contactListPageUId);
					sysModule.Save(false);
				}
			}
		}
	}
}
