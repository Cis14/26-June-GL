 namespace Terrasoft.Configuration.CESModels
{

	using System.Runtime.Serialization;

	#region Class: SenderDomainsInfoRequest

	/// <summary>
	/// Class for request to get sender domains info.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.CESModels.BaseServiceRequest" />
	[DataContract]
	public class SenderDomainsInfoRequest : BaseServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the provider name to get domains for.
		/// </summary>
		[DataMember(Name = "providerName")]
		public string ProviderName { get; set; }

		#endregion

	}

	#endregion

}
