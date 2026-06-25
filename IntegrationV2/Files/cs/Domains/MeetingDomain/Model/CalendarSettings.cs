namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Model
{
	using Creatio.FeatureToggling;
	using IntegrationApi.Interfaces;
	using IntegrationApi.MailboxDomain.Exceptions;
	using IntegrationApi.Utils;
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: CalendarSettings

	/// <summary>
	/// Calendar settings domain model.
	/// </summary>
	[Serializable]
	public class CalendarSettings
	{

		#region Fields: Private

		private readonly string _oauthClassName;

		private readonly string _office365OauthScope = "https://outlook.office365.com/.default";

		private const string EwsAccessAsUserScope = "EWS.AccessAsUser.All";

		#endregion

		#region Properties: Public

		public Guid Id { get; set; }

		public string SenderEmailAddress { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public Guid UserId { get; set; }

		public string UserName { get; set; }

		public string ServiceUrl { get; set; }

		public Guid OAuthApplicationId { get; set; }

		public string AccessToken { get; private set; }

		public bool SyncEnabled { get; set; }

		public bool UseImpersonation { get; set; }

		public bool UseOAuth => OAuthApplicationId != Guid.Empty;

		public Guid WarningCodeId { get; set; }

		public bool IsLimitMode { get; set; }

		public string ServiceAccountToken { get; set; }

		public bool UseEwsOAuthScope { get; set; }

		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="oauthClassName">Oauth class name.</param>
		public CalendarSettings(string oauthClassName = null) {
			_oauthClassName = oauthClassName;
		}

		#endregion

		#region Methods: Public

		public string GetAccessToken(UserConnection uc, string scope = null) {
			if (UseImpersonation && Features.GetIsEnabled("ApplicationAsServiceAccount")) {
				return OAuthUtils.GetServiceAccountToken(OAuthApplicationId, _office365OauthScope, uc);
			}
			string oauthEmailAddress = UseImpersonation ? Login : SenderEmailAddress;
			if (string.IsNullOrEmpty(_oauthClassName) || uc == null || string.IsNullOrEmpty(oauthEmailAddress)) {
				throw new ArgumentException("Failed refresh calendar token, invalid arguments states.");
			}
			return OAuthUtils.RefreshAccessToken(_oauthClassName, oauthEmailAddress, uc, GetAccessTokenScope(scope));
		}

		/// <summary>
		/// Refresh calendar access token.
		/// </summary>
		/// <exception cref="ArgumentException">When <paramref name="uc"/>, <see cref="UseOAuth"/> 
		/// or <see cref="SenderEmailAddress"/> has invalid states.</exception>
		public void RefreshAccessToken(UserConnection uc) {
			if (!UseOAuth)
				return;
			if (string.IsNullOrEmpty(_oauthClassName) || uc == null){
				throw new ArgumentException("Failed refresh calendar token, invalid arguments states.");
			}
			try {
				AccessToken = GetAccessToken(uc);
			} catch (Exception e) {
				Exception ex = e is AggregateException ? e.InnerException : e;
				ex = UseOAuth ? (Exception)new OAuthSyncException(ex) : new BasicAuthSyncException(ex);
				var syncErrorHelper = ClassFactory.Get<ISynchronizationErrorHelper>(new ConstructorArgument("userConnection", uc));
				syncErrorHelper.ProcessSynchronizationError(SenderEmailAddress, ex);
				throw;
			}
		}

		/// <inheritdoc cref="object.ToString"/>
		public override string ToString() {
			return $"[Calendar settings => \"{Id}\" \"{ServiceUrl}\" \"{(UseOAuth ? "Oauth" : "Login")}\"]";
		}

		/// <summary>
		/// Temporary implementation without core change. 
		/// Todo move to OAuthUtils.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		public void LoadOauthAppParams(UserConnection uc) {
			if (!UseOAuth)
				return;
			ServiceAccountToken =
				OAuthUtils.GetServiceAccountToken(OAuthApplicationId, "https://graph.microsoft.com/.default", uc);
		}

		#endregion

		#region Methods: Private

		private string GetAccessTokenScope(string scope) {
			return string.IsNullOrEmpty(scope) && UseEwsOAuthScope
				? EwsAccessAsUserScope
				: scope;
		}

		#endregion

	}

	#endregion

}
