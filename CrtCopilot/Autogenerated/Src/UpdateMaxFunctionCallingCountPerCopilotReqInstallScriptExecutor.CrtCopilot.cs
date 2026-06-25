 namespace Terrasoft.Configuration {
	using Terrasoft.Core;
	using CoreSysSettings = Terrasoft.Core.Configuration.SysSettings;

	public class UpdateMaxFunctionCallingCountPerCopilotReqInstallScriptExecutor: IInstallScriptExecutor {
		private const int _newDefValue = 25;
		private const string _sysSettingCode = "MaxFunctionCallingCountPerCopilotRequest";

		public void Execute(UserConnection userConnection) {
			var value = (int)CoreSysSettings.GetValue(userConnection, _sysSettingCode);
			if (value < _newDefValue) {
				CoreSysSettings.SetDefValue(userConnection, _sysSettingCode, _newDefValue);
			}
		}
	}
}

