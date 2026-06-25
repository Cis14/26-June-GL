namespace Terrasoft.Configuration 
{
	using Terrasoft.Core;
	using CoreSysSettings = Terrasoft.Core.Configuration.SysSettings;

	public class UpdateSysSettingCreatioAIMaxUserQueryLengthInstallScriptExecutor: IInstallScriptExecutor
	{
		private const int _newValue = 100000;
		private const string _syncSettingCode = "CreatioAIMaxUserQueryLength";

		public void Execute(UserConnection userConnection) {
			var value = (int)CoreSysSettings.GetValue(userConnection, _syncSettingCode);
			if (value < _newValue) {
				CoreSysSettings.SetDefValue(userConnection, _syncSettingCode, _newValue);
			}
		}
	}
}

