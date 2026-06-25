namespace CrtCloudIntegration.Handlers
{
	using CrtCloudIntegration.Interfaces;
	using CrtCloudIntegration.Models.Requests;
	using Terrasoft.Configuration.BpmonlineCloudIntegration;
	using Terrasoft.Core;

	/// <summary>
	/// Handles auth request to facebook digital adds app.
	/// </summary>
	public abstract class DefaultOAuthHandler : ISocialOAuthHandler {

		#region Fields: Private

		private IRootAccountProvider _rootAccountProvider;
		private IOAuthAccountServiceApi _authAccountServiceApi;

		#endregion

		#region Properties: Public

		/// <summary>
		/// Application name.
		/// </summary>
		public abstract string Application { get; }

		/// <summary>
		/// Api to connect to social authentication service.
		/// </summary>
		public IOAuthAccountServiceApi AuthAccountServiceApi {
			get =>
				_authAccountServiceApi ??
				(_authAccountServiceApi = new OAuthAccountServiceApi(UserConnection.Current, Scope));
			set => _authAccountServiceApi = value;
		}

		/// <summary>
		/// Platform name.
		/// </summary>
		public string[] Platform => new[] { "facebook", "google" };

		/// <summary>
		/// Provide to manipulate by main cloud account.
		/// </summary>
		public IRootAccountProvider RootAccountProvider {
			get =>
				_rootAccountProvider ??
				(_rootAccountProvider = new RootAccountProvider(UserConnection.Current, Scope));
			set => _rootAccountProvider = value;
		}

		/// <summary>
		/// The identity scope.
		/// </summary>
		public abstract string Scope { get; }

		#endregion

		#region Methods: Public

		/// <summary>
		/// Action to start the authorization flow.
		/// </summary>
		/// <param name="request">Request that contains main auth info.</param>
		/// <returns></returns>
		public string StartFlow(StartFlowRequest request) {
			RootAccountProvider.GetOrCreateSocialRootAccount();
			var response = AuthAccountServiceApi.StartFlow(request);
			return response.StartFlowUrl;
		}

		#endregion

	}
}

