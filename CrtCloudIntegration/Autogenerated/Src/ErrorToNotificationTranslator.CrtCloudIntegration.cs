namespace CrtCloudIntegration.Utilities
{
	using System;
	using CrtCloudIntegration.Dto;
	using CrtCloudIntegration.Utilities.Errors;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Interface: IErrorTranslator

	/// <summary>
	/// Interface for error translators.
	/// </summary>
	public interface IErrorTranslator
	{
		WebSocketDto Translate();
	}

	#endregion

	#region Class: ErrorToNotificationTranslator

	/// <summary>
	/// Implements the translator from errors to user friendly messages.
	/// </summary>
	public class ErrorToNotificationTranslator
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorToNotificationTranslator"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		public ErrorToNotificationTranslator(UserConnection userConnection = null) {
			_userConnection = userConnection ?? ClassFactory.Get<UserConnection>();
		}

		#endregion
		#region Methods: Public

		/// <summary>
		/// Translates the error to <see cref="WebSocketDto"/>.
		/// </summary>
		/// <param name="error">The error.</param>
		/// <returns><see cref="WebSocketDto"/></returns>
		/// <exception cref="System.ArgumentOutOfRangeException"></exception>
		public WebSocketDto TranslateError(IError error) {
			IErrorTranslator translator;
			switch (error) {
				case SysSettingError sysSettingError:
					translator = new SysSettingErrorTranslator(sysSettingError, _userConnection);
					break;
				case KnownError knownError:
					translator = new KnownErrorTranslator(knownError, _userConnection);
					break;
				case GenericError genericError:
					translator = new GenericErrorTranslator(genericError);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return translator.Translate();
		}

		#endregion

	}

	#endregion

}

