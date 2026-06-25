namespace CrtCloudIntegration.Interfaces
{
	using Models.Requests;

	#region Interface: ISocialOAuthHandler

	/// <summary>
	/// Interface for facebook digital adds app auth handler.
	/// </summary>
	public interface ISocialOAuthHandler
	{
		/// <summary>
		/// Application name.
		/// </summary>
		string Application { get; }

		/// <summary>
		/// Platform name.
		/// </summary>
		string[] Platform { get; }

		/// <summary>
		/// The identity scope.
		/// </summary>
		string Scope { get; }

		/// <summary>
		/// Action to start the authorization flow.
		/// </summary>
		/// <param name="request">Request that contains main auth info.</param>
		/// <returns>Authorization url.</returns>
		string StartFlow(StartFlowRequest request);
	}

	#endregion

}
