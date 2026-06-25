namespace CrtCloudIntegration.Models.Requests
{
	using System;

	#region Class: StartFlowRequest

	/// <summary>
	/// Represents a request to start the authentication flow.
	/// </summary>
	public class StartFlowRequest : BaseRequest
	{

		#region Properties: Public

		/// <summary>
		/// The end user identifier.
		/// </summary>
		public Guid CreatioUserId { get; set; }

		#endregion

	}

	#endregion

}
