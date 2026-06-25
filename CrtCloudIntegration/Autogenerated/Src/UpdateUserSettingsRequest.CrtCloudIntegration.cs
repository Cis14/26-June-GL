namespace Terrasoft.Configuration.CESModels
{

	using System.Runtime.Serialization;

	#region Class: UpdateUserSettingsRequest

	/// <summary>
	/// Class for request to update account's user settings.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.CESModels.BaseServiceRequest" />
	[DataContract]
	public class UpdateUserSettingsRequest : BaseServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the web hook application domain.
		/// </summary>
		/// <value>
		/// The web hook application domain.
		/// </value>
		[DataMember(Name = "webHookAppDomain")]
		public string WebHookAppDomain { get; set; }

		/// <summary>
		/// Gets or sets the authentication key.
		/// </summary>
		/// <value>
		/// The authentication key.
		/// </value>
		[DataMember(Name = "authKey")]
		public string AuthKey { get; set; }

		#endregion

	}

	#endregion

}

