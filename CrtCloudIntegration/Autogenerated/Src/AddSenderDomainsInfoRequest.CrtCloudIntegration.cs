namespace Terrasoft.Configuration.CESModels
{

	using System.Runtime.Serialization;

	#region Class: AddSenderDomainsInfoRequest

	/// <summary>
	/// Class for request to add new sender domain.
	/// </summary>
	/// <seealso cref="BaseServiceRequest" />
	[DataContract]
	public class AddSenderDomainsInfoRequest : BaseServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the domain.
		/// </summary>
		/// <value>
		/// The domain.
		/// </value>
		[DataMember(Name = "domain")]
		public string Domain { get; set; }

		/// <summary>
		/// Gets or sets the name of the provider.
		/// </summary>
		/// <value>
		/// The name of the provider.
		/// </value>
		[DataMember(Name = "providerName")]
		public string ProviderName { get; set; }

		#endregion

	}

	#endregion

}

