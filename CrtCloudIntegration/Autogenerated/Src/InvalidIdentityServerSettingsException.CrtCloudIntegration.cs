namespace Terrasoft.Configuration.BpmonlineCloudIntegration
{
	using System;

	#region Class: InvalidIdentityServerSettingsException

	[Serializable()]
	public class InvalidIdentityServerSettingsException : Exception
	{
		#region Constructors: Public

		public InvalidIdentityServerSettingsException() {
		}

		public InvalidIdentityServerSettingsException(string message) : base(message) {
		}

		public InvalidIdentityServerSettingsException(string message, Exception innerException) : base(message, innerException) {
		}

		#endregion

		#region Properties: Public

		public override string Message => "Identity Server settings is not valid - server url, clientId or clientSecret";

		public string ErrorCode { get; } = "identity_invalid_settings";

		#endregion

	}

	#endregion
}
