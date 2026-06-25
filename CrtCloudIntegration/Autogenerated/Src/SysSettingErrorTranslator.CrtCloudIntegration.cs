namespace CrtCloudIntegration.Utilities
{
	using CrtCloudIntegration.Dto;
	using CrtCloudIntegration.Utilities.Errors;
	using Terrasoft.Core;

	#region Class: SysSettingErrorTranslator

	public class SysSettingErrorTranslator: BaseErrorTranslator, IErrorTranslator
	{
		#region Fields: Private

		private readonly SysSettingError _error;
		private readonly UserConnection _userConnection;

		#endregion

		public SysSettingErrorTranslator(SysSettingError error, UserConnection userConnection) {
			_error = error;
			_userConnection = userConnection;
		}

		public WebSocketDto Translate() {
			var descriptionLcz = $"SysSetting_{_error.SysSettingGroup}InvalidValue_Description";
			return CreateErrorDto(GetLocalizableValue(
					_userConnection, _error.SysSettingCode, descriptionLcz));
		}
	}

	#endregion
}

