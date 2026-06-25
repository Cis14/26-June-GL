namespace Terrasoft.Configuration.CESModels 
{
	using System.Runtime.Serialization;

	#region Class: DeleteSenderDomainRequest

	/// <summary>
	/// Represents request for single domain deletion.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.CESModels.BaseServiceRequest" />
	[DataContract]
	public class DeleteSenderDomainInfoRequest: BaseServiceRequest {

		#region Properties: Public

		/// <summary>
		/// Is Success.
		/// </summary>
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

