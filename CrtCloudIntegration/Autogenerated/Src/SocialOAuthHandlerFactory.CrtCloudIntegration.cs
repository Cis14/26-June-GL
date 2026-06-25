namespace CrtCloudIntegration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using CrtCloudIntegration.Interfaces;
	using Terrasoft.Core.Factories;

	#region Class: SocialOAuthHandlerFactory

	/// <summary>
	/// Provides logic to get authentication handlers.
	/// </summary>
	public class SocialOAuthHandlerFactory
	{

		#region Properties: Private

		private IEnumerable<ISocialOAuthHandler> AuthHandlers => ClassFactory.GetAll<ISocialOAuthHandler>();

		#endregion

		#region Methods: Public

		/// <summary>
		/// Creates authorization handler.
		/// </summary>
		/// <param name="application">The application name.</param>
		/// <param name="platform">The platform name.</param>
		/// <returns>Instance of <see cref="ISocialOAuthHandler"/>></returns>
		/// <exception cref="ArgumentException"> when no matching handler is found</exception>
		public ISocialOAuthHandler CreateHandler(string application, string platform) {
			var handler = AuthHandlers.FirstOrDefault(h =>
				h.Application.Equals(application, StringComparison.InvariantCultureIgnoreCase) &&
				h.Platform.Any(x => x.Equals(platform, StringComparison.InvariantCultureIgnoreCase)));
			if (handler == null) {
				throw new ArgumentException("No matching handler found.");
			}
			return handler;
		}

		#endregion

	}

	#endregion

}
