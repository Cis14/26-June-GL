using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace CrtCloudIntegration.Models.Requests
{

	#region Class: GetTokenRequest

	public class GetTokenRequest : BaseSocialPlatformServiceRequest
	{
	}

	#endregion

	#region Class: GetTokensRequest

	public class GetTokensRequest
	{
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
	}

	#endregion

}
