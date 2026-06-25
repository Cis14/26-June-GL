namespace Terrasoft.Configuration
{

	#region Class: RootAccountInfoResponse

	/// <summary>
	/// Cloud response DTO with information about account.
	/// </summary>
	public class RootAccountInfoResponse : BaseAccountResponse
	{

		#region Properties: Public

		/// <summary>
		/// Creatio URL.
		/// </summary>
		public string CreatioUrl { get; set; }

		/// <summary>
		/// Gets or sets the creatio client identifier.
		/// </summary>
		public string CreatioClientId { get; set; }

		/// <summary>
		/// Gets or sets the creatio client secret.
		/// </summary>
		public string CreatioClientSecret { get; set; }

		/// <summary>
		/// Gets or sets the creatio identity server URL.
		/// </summary>
		public string CreatioIdentityServerUrl { get; set; }

		#endregion

	}

	#endregion

}

