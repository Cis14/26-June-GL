namespace Terrasoft.Configuration {
	using Terrasoft.Core;
	using CoreSysSettings = Terrasoft.Core.Configuration.SysSettings;

	public class UpdateMailboxSyncIntervalSysSettingsValueInstallScriptExecutor: IInstallScriptExecutor {
		private const int _oldDefValue = 1;
		private const int _newDefValue = 15;
		private const string _sysSettingCode = "MailboxSyncInterval";

		public void Execute(UserConnection userConnection) {
			var value = (int)CoreSysSettings.GetValue(userConnection, _sysSettingCode);
			if (value == _oldDefValue) {
				CoreSysSettings.SetDefValue(userConnection, _sysSettingCode, _newDefValue);
			}
		}
	}
}

