namespace Terrasoft.Configuration.CESModels
{
	using System.Linq;
	using Newtonsoft.Json;

	#region Class: AccountInfo

	/// <summary>
	/// Class for information about account status and its services.
	/// </summary>
	public class AccountInfo
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the API key.
		/// </summary>
		/// <value>
		/// The API key.
		/// </value>
		[JsonProperty("ApiKey")]
		public string ApiKey { get; set; }

		/// <summary>
		/// Gets or sets the name of the account.
		/// </summary>
		/// <value>
		/// The name of the account.
		/// </value>
		[JsonProperty("AccountName")]
		public string AccountName { get; set; }

		/// <summary>
		/// Gets or sets the web hook application domain.
		/// </summary>
		/// <value>
		/// The web hook application domain.
		/// </value>
		[JsonProperty("WebHookAppDomain")]
		public string WebHookAppDomain { get; set; }

		/// <summary>
		/// Gets or sets the array of services.
		/// </summary>
		/// <value>
		/// The services.
		/// </value>
		[JsonProperty("Services")]
		public Service[] Services { get; set; }

		/// <summary>
		/// Gets or sets the response status.
		/// </summary>
		/// <value>
		/// "ok" if request succeed; otherwise "error"
		/// </value>
		[JsonProperty("Status")]
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the response code.
		/// </summary>
		/// <value>
		/// The code.
		/// </value>
		[JsonProperty("Code")]
		public int Code { get; set; }

		/// <summary>
		/// Gets or sets the response message.
		/// </summary>
		/// <value>
		/// The message.
		/// </value>
		[JsonProperty("Message")]
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the integration status.
		/// </summary>
		/// <value>
		/// The integration status.
		/// </value>
		[JsonProperty("IntegrationStatus")]
		public InegrationStatusInfo IntegrationStatus { get; set; }

		#endregion

	}

	#endregion

	public static class AccountInfoExtensions
	{
		public static string GetProviderName(this AccountInfo accountInfo) {
			return accountInfo?.Services?.FirstOrDefault()?.Settings?.Provider;
		}
	}

	#region Class: InegrationStatusInfo

	/// <summary>
	/// Class for indicating status of integration.
	/// </summary>
	public class InegrationStatusInfo
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets a value indicating whether account has a valid authentication key.
		/// </summary>
		/// <value>
		/// <c>true</c> if this account has a valid authentication key; otherwise, <c>false</c>.
		/// </value>
		[JsonProperty("IsValidAuthKey")]
		public bool IsValidAuthKey { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this account has a valid web hook application domain.
		/// </summary>
		/// <value>
		/// <c>true</c> if this account has a  valid web hook application domain; otherwise, <c>false</c>.
		/// </value>
		[JsonProperty("IsValidWebHookAppDomain")]
		public bool IsValidWebHookAppDomain { get; set; }

		#endregion

	}

	#endregion

	#region Class: Service

	/// <summary>
	/// Class for information about service settings and status.
	/// </summary>
	public class Service
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the name of the service.
		/// </summary>
		/// <value>
		/// The name of the service.
		/// </value>
		[JsonProperty("ServiceName")]
		public string ServiceName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		/// <value>
		///	<c>true</c> if this service is active; otherwise, <c>false</c>.
		/// </value>
		[JsonProperty("IsActive")]
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets the service settings.
		/// </summary>
		/// <value>
		/// The settings.
		/// </value>
		[JsonProperty("Settings")]
		public Settings Settings { get; set; }

		/// <summary>
		/// Gets or sets the user settings.
		/// </summary>
		/// <value>
		/// The user settings.
		/// </value>
		[JsonProperty("UserSettings")]
		public UserSettings UserSettings { get; set; }

		#endregion

	}

	#endregion

	#region Class: Settings

	/// <summary>
	/// Class for predefined account settings.
	/// </summary>
	public class Settings
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the email provider name.
		/// </summary>
		/// <value>
		/// The provider name.
		/// </value>
		[JsonProperty("Provider")]
		public string Provider { get; set; }

		#endregion

	}

	#endregion

	#region Class: UserSettings

	/// <summary>
	/// Class for user settings.
	/// </summary>
	public class UserSettings
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the web hooks url.
		/// </summary>
		/// <value>
		/// The web hooks url.
		/// </value>
		[JsonProperty("Webhooks")]
		public string Webhooks { get; set; }

		#endregion

	}

	#endregion

}

