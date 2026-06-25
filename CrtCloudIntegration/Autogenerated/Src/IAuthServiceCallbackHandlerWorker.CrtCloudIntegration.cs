namespace CrtCloudIntegration.Services
{
	using System;

	#region Interface: IAuthServiceCallbackHandlerWorker

	/// <summary>
	/// Represents the interface for the handling callback request. 
	/// </summary>
	public interface IAuthServiceCallbackHandlerWorker
	{

		#region Methods: Public

		/// <summary>
		/// Handles callback request for the specified platform.
		/// </summary>
		/// <param name="platform">The platform.</param>
		/// <param name="platformUserId">The platform user identifier.</param>
		/// <param name="application">The application.</param>
		void Handle(string platform, string platformUserId, string application);

		#endregion

	}

	#endregion
}
