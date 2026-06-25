namespace CrtCloudIntegration.Utilities
{
	using Common.Logging;
	using CrtCloudIntegration.Utilities.Errors;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: SysSettingsValidator

	/// <summary>
	/// Implements the validator for the important sys settings.
	/// </summary>
	public class SysSettingsValidator
	{

		#region Constants: Private

		private const string IdentityServerGroupName = "IdentityServer";
		private const string OAuthGroupName = "OAuth";
		private const string SocialPlatformServiceGroupName = "SocialPlatformService";

		#endregion

		#region Fields: Private

		private readonly ILog _logger;

		private readonly Dictionary<string, Predicate<object>> _identityServerSettingsToValidate =
			new Dictionary<string, Predicate<object>> {
				{ "IdentityServerUrl", value => string.IsNullOrWhiteSpace(value?.ToString()) },
				{ "IdentityServerClientId", value => string.IsNullOrWhiteSpace(value?.ToString()) },
				{ "IdentityServerClientSecret", value => string.IsNullOrWhiteSpace(value?.ToString()) }
			};

		private readonly Dictionary<string, Predicate<object>> _oAuthSettingsToValidate =
			new Dictionary<string, Predicate<object>> {
				{ "OAuth20IdentityServerUrl", value => string.IsNullOrWhiteSpace(value?.ToString()) },
				{ "OAuth20IdentityServerClientId", value => string.IsNullOrWhiteSpace(value?.ToString()) },
				{ "OAuth20IdentityServerClientSecret", value => string.IsNullOrWhiteSpace(value?.ToString()) }
			};

		private readonly Dictionary<string, Predicate<object>> _socialPlatformServiceSettingsToValidate =
			new Dictionary<string, Predicate<object>> {
				{ "SocialAccountServiceUrl", value => string.IsNullOrWhiteSpace(value?.ToString()) },
				{ "SocialPlatformServiceUrl", value => string.IsNullOrWhiteSpace(value?.ToString()) },
			};

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="SysSettingsValidator"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		public SysSettingsValidator(ILog logger) {
			_logger = logger;
		}

		#endregion

		#region Methods: Private

		private IEnumerable<string> GetEmptySysSettings(UserConnection userConnection,
			Dictionary<string, Predicate<object>> settings) {
			foreach (var setting in settings) {
				if (!SysSettings.TryGetValue(userConnection, setting.Key, out var sysSettingValue) ||
					setting.Value.Invoke(sysSettingValue)) {
					yield return setting.Key;
				}
			}
		}

		private IEnumerable<IError> InternalValidate(UserConnection userConnection,
			Dictionary<string, Predicate<object>> settingsToValidate, string groupName) {
			var emptySysSettings = GetEmptySysSettings(userConnection, settingsToValidate);
			if (emptySysSettings.Any()) {
				_logger.Warn(
					$"SocialOAuthProxyService. Not filled required system settings: {string.Join(", ", emptySysSettings)}");
				yield return new SysSettingError(emptySysSettings.FirstOrDefault(), groupName);
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Validates the predefined sys settings using specified userConnection instance.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <returns><see cref="IEnumerable<IError>"/> of validation errors.</returns>
		public IEnumerable<IError> Validate(UserConnection userConnection) {
			var oauthErrors = InternalValidate(userConnection, _oAuthSettingsToValidate, OAuthGroupName);
			var socialErrors = InternalValidate(userConnection, _socialPlatformServiceSettingsToValidate,
				SocialPlatformServiceGroupName);
			var identityErrors =
				InternalValidate(userConnection, _identityServerSettingsToValidate, IdentityServerGroupName);
			var result = oauthErrors.Concat(socialErrors).Concat(identityErrors);
			foreach (var error in result) {
				yield return error;
			}
		}

		#endregion

	}

	#endregion

}
