namespace CrtCloudIntegration.Models.Responses
{
	using Terrasoft.Configuration;

	#region Class: ${Name}

	/// <summary>
	/// Response from social account service, contains all info to start auth flow.
	/// </summary>
	public class ActualizeTokensResponse : BaseAccountResponse
	{

		#region Properties: Public

		/// <summary>
		/// Number of valid tokens.
		/// </summary>
		public int NumberOfValidTokens { get; set; }

		#endregion

	}

	#endregion

}

