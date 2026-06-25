namespace CrtCloudIntegration.Utilities
{
	using System;
	using CrtCloudIntegration.Dto;
	using CrtCloudIntegration.Utilities.Errors;
	using Terrasoft.Core;

	#region Class: KnownErrorTranslator

	public class KnownErrorTranslator: BaseErrorTranslator, IErrorTranslator
	{

		#region Fields: Private 

		private readonly KnownError _error;
		private readonly UserConnection _userConnection;

		#endregion

		#region Contructors: Public

		public KnownErrorTranslator(KnownError error, UserConnection userConnection) {
			_error = error;
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Public

		public WebSocketDto Translate() {
			switch (_error) {
				case PlatformServicesUnavailable platformServicesUnavailable:
					return CreateErrorDto(
						GetLocalizableValue(_userConnection, nameof(PlatformServicesUnavailable),
							platformServicesUnavailable.DescriptionLcz));
				case CouldNotGetAdAccountError couldNotGetAdAccountError:
					return CreateErrorDto(
						GetLocalizableValue(_userConnection, nameof(CouldNotGetAdAccountError),
							couldNotGetAdAccountError.DescriptionLcz));
				case CouldNotGetAccountError couldNotGetAccountError:
					return CreateErrorDto(
						GetLocalizableValue(_userConnection, nameof(CouldNotGetAccountError),
							couldNotGetAccountError.DescriptionLcz));
				case CouldNotAuthenticateToPlatformError couldNotAuthenticateToPlatformError:
					return CreateErrorDto(
						GetLocalizableValue(_userConnection, nameof(CouldNotAuthenticateToPlatformError),
							couldNotAuthenticateToPlatformError.DescriptionLcz));
				case AccessTokenDoesNotHaveRequiredScopes accessTokenDoesNotHaveRequiredScopes:
					return CreateErrorDto(
						GetLocalizableValue(_userConnection, nameof(AccessTokenDoesNotHaveRequiredScopes),
							$"{nameof(AccessTokenDoesNotHaveRequiredScopes)}_Description"));
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		#endregion

	}

	#endregion
}

