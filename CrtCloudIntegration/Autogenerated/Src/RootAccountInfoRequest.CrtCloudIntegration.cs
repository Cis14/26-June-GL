namespace Terrasoft.Configuration
{

	#region Class: RootAccountInfoRequest

	/// <summary>
	/// DTO request for account.
	/// </summary>
	public class RootAccountInfoRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the creatio URL.
		/// </summary>
		public string CreatioUrl { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [ping creatio instance].
		/// </summary>
		public bool PingCreatioInstance { get; set; }

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

		/// <summary>
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is net core.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is net core; otherwise, <c>false</c>.
		/// </value>
		public bool IsNetCore { get; set; }

		#endregion

	}

	#endregion

}

