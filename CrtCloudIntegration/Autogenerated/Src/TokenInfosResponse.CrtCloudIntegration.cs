namespace CrtCloudIntegration.Models.Responses
{
	using Newtonsoft.Json;
	using System.Collections.Generic;
	using Terrasoft.Configuration;

	#region Class: TokenInfos

	/// <summary>
	/// Summary info about token by platform and application
	/// </summary>
	public class TokenInfo : TokenInfoBase
	{

		#region Properties: Public

		[JsonProperty("token")]
		public string Token { get; set; }

		#endregion
	}

	#endregion

	#region Class: TokenInfos

	/// <summary>
	/// Summary info about tokens by platform and application
	/// </summary>
	public class TokenInfos : TokenInfoBase
	{

		#region Properties: Public

		[JsonProperty("tokens")]
		public IEnumerable<string> Tokens { get; set; }

		#endregion
	}

	#endregion

	#region Class: TokenInfoBase

	/// <summary>
	/// Info about Main account
	/// </summary>
	public class TokenInfoBase : BaseAccountResponse {}

	#endregion
}
