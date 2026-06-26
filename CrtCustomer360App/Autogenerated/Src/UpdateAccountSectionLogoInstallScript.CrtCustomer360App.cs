namespace Terrasoft.Configuration
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	public class UpdateAccountSectionLogoInstallScript : IInstallScriptExecutor {

		private readonly Guid _accountSysModuleId = new Guid("EE586F85-5E21-4247-9AB2-1D906F0F0B46");
		private readonly Guid _oldAccountLogoId = new Guid("21399637-E87F-406C-A73B-74FAF2FD1BA2");
		private readonly Guid _accountLogoId = new Guid("DB9523D7-0E6B-45BC-B28C-D11CF51712B6");

		public void Execute(UserConnection userConnection) {
			Entity sysModule = userConnection.EntitySchemaManager.GetEntityByName("SysModule", userConnection);
			if (!sysModule.FetchFromDB(_accountSysModuleId)) {
				return;
			}
			var logoId = sysModule.GetTypedColumnValue<Guid>("LogoId");
			if (logoId.Equals(Guid.Empty) || logoId.Equals(_oldAccountLogoId)) {
				sysModule.SetColumnValue("LogoId", _accountLogoId);
				sysModule.Save(false);
			}
		}
	}
}
