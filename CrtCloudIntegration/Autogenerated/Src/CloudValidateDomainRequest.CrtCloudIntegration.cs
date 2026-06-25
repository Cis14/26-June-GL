namespace Terrasoft.Configuration.CESModels
{
	using System.Runtime.Serialization;

	#region Class: ValidateDomainRequest
	/// <summary>
	/// Class for request to validate sender domain.
	/// </summary>
	[DataContract]
	public class CloudValidateDomainRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the API key.
		/// </summary>
		[DataMember(Name = "apiKey")]
		public string ApiKey { get; set; }

		/// <summary>
		/// Gets or sets the domain.
		/// </summary>
		[DataMember(Name = "domain")]
		public string Domain { get; set; }

		/// <summary>
		/// Gets or sets the provider name.
		/// </summary>
		[DataMember(Name = "providerName")]
		public string ProviderName { get; set; }

		#endregion

	}

	#endregion

}

