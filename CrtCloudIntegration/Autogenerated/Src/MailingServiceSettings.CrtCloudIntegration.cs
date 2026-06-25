namespace Terrasoft.Configuration.CESModels
{
	using System.Runtime.Serialization;

	#region Class: MailingServiceSettings

	/// <summary>
	/// Settings of the cloud service.
	/// </summary>
	[DataContract]
	public class MailingServiceSettings
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the name of the provider.
		/// </summary>
		[DataMember(Name = "provider")]
		public string ProviderName { get; set; }

		/// <summary>
		/// Gets or sets an opportunity to make provider default.
		/// </summary>
		[DataMember(Name = "useProviderAsDefault")]
		public bool UseProviderAsDefault { get; set; }

		#endregion

	}

	#endregion

}

