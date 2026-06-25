namespace CrtCloudIntegration.Models.Responses
{
	using Terrasoft.Configuration;

	#region Class: StartFlowRequest


	/// <summary>
	/// Response from social account service, contains all info to start auth flow.
	/// </summary>
	public class StartOAuthFlowResponse : BaseAccountResponse
	{

		#region Properties: Public

		/// <summary>
		/// Url to start authorization flow.
		/// </summary>
		public string StartFlowUrl { get; set; }

		#endregion

	}

	#endregion

}
