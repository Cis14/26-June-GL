namespace CrtCloudIntegration.Models.Requests
{
	using System.Runtime.Serialization;
	using Newtonsoft.Json;

	#region Class: ActualizeTokensRequest

	[DataContract]
	public class ActualizeTokensRequest
	{

		#region Properties: Public

		/// <summary>
		/// Application name
		/// </summary>
		[DataMember(Name = "application")]
		[JsonProperty("application")]
		public string Application { get; set; }

		/// <summary>
		/// Platform name
		/// </summary>
		[DataMember(Name = "platformName")]
		[JsonProperty("platformName")]
		public string PlatformName { get; set; }

		#endregion

	}

	#endregion

}

