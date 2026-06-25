namespace Terrasoft.Configuration.CESModels
{
	using System.Runtime.Serialization;

	#region Class: GetAvailableProvidersRequest

	/// <summary>
	/// Represents request to get available mailing providers.
	/// </summary>
	[DataContract]
	public class GetAvailableProvidersRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the API key.
		/// </summary>
		[DataMember(Name = "apiKey")]
		public string ApiKey { get; set; }

		#endregion

	}

	#endregion

}
