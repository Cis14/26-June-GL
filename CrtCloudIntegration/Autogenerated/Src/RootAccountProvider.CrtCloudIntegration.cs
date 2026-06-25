namespace Terrasoft.Configuration.BpmonlineCloudIntegration
{
	using System;
	using System.Text.RegularExpressions;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Web.Common;
	using Terrasoft.Web.Http.Abstractions;
	using CoreConfig = Terrasoft.Core.Configuration;

	#region Class: RootAccountProvider

	/// <inheritdoc cref="IRootAccountProvider"/>
	public class RootAccountProvider : IRootAccountProvider, IRootAccountRetriever
	{

		#region Constants: Private

		private const string CreatioUrlSysSettingCode = "SiteUrl";
		private const string IdentityServerUrlCode = "OAuth20IdentityServerUrl";

		#endregion

		#region Fields: Private

		private readonly string _scope;

		private readonly UserConnection _userConnection;
		private string _baseApplicationUrl;
		private IRootAccountServiceApi _rootAccountServiceApi;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="RootAccountProvider"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="scope">The name of identity scope.</param>
		public RootAccountProvider(UserConnection userConnection, string scope) {
			_userConnection = userConnection;
			_scope = scope;
		}

		#endregion

		#region Properties: Private

		private string CreatioIdentityServerUrl =>
			CoreConfig.SysSettings.GetValue(_userConnection, IdentityServerUrlCode).ToString();

		private string CreatioUrl => GetClearSiteUrl(BaseApplicationUrl);

		private string BaseApplicationUrl => _baseApplicationUrl ?? (_baseApplicationUrl=  GetBaseApplicationUrl());

		private bool IsNetCore => !BaseApplicationUrl.EndsWith("/0");

		#endregion

		#region Properties: Public

		/// <summary>
		/// The root account service api.
		/// </summary>
		public IRootAccountServiceApi RootAccountServiceApi {
			get =>
				_rootAccountServiceApi ?? (_rootAccountServiceApi = new RootAccountServiceApi(_userConnection, _scope));
			set => _rootAccountServiceApi = value;
		}

		#endregion

		#region Methods: Private

		private RootAccountInfoRequest CreateOrUpdateRootAccount(RootAccountInfoResponse existingRootAccount) {
			RootAccountInfoRequest accountRequest = GetRootAccountInfoRequest();
			BaseAccountResponse response = IsExistingAccountNull(existingRootAccount)
				? RootAccountServiceApi.CreateAccount(accountRequest)
				: RootAccountServiceApi.UpdateAccount(accountRequest);
			existingRootAccount.ErrorMessage = response?.ErrorMessage;
			return accountRequest;
		}

		private void FillRootAccount(RootAccountInfoResponse existingRootAccount,
			RootAccountInfoRequest accountRequest) {
			existingRootAccount.CreatioUrl = accountRequest.CreatioUrl;
			existingRootAccount.CreatioIdentityServerUrl = accountRequest.CreatioIdentityServerUrl;
		}

		private void FillRootAccountIfNecessary(RootAccountInfoResponse existingRootAccount) {
			if (existingRootAccount != null && IsRootAccountFilledCorrectly(existingRootAccount)) {
				return;
			}
			RootAccountInfoRequest accountRequest = CreateOrUpdateRootAccount(existingRootAccount);
			FillRootAccount(existingRootAccount, accountRequest);
		}

		private string GetClearSiteUrl(string creatioUrl) {
			var uri = new Uri(creatioUrl);
			var resultUri = uri.Scheme + "://" + uri.Authority; 
			if (uri.AbsolutePath != "/") {
				resultUri += Regex.Replace(uri.AbsolutePath, @"\/0.*", string.Empty);
			}
			return resultUri;
		}

		private string GetBaseApplicationUrl() {
			if (!string.IsNullOrEmpty(_baseApplicationUrl)) {
				return _baseApplicationUrl;
			}
			var httpContextAccessor = ClassFactory.Get<IHttpContextAccessor>();
			HttpContext httpContext = httpContextAccessor.GetInstance();
			string baseApplicationUrl = httpContext != null ? WebUtilities.GetBaseApplicationUrl(httpContext.Request)
				: (string)CoreConfig.SysSettings.GetValue(_userConnection, CreatioUrlSysSettingCode);
			if (string.IsNullOrEmpty(baseApplicationUrl)) {
				var message = "Failed to get the Creatio url.";
				throw new GetCreatioUrlException(message);
			}
			return _baseApplicationUrl = baseApplicationUrl;
		}

		private RootAccountInfoRequest GetRootAccountInfoRequest() {
			var accountRequest = new RootAccountInfoRequest {
				CreatioUrl = CreatioUrl,
				CreatioIdentityServerUrl = CreatioIdentityServerUrl,
				PingCreatioInstance = false,
				IsActive = true,
				IsNetCore = IsNetCore
			};
			return accountRequest;
		}

		private bool IsExistingAccountNull(RootAccountInfoResponse existingRootAccount) {
			return existingRootAccount == null || string.IsNullOrEmpty(existingRootAccount.CreatioUrl);
		}

		private bool IsGetAccountRequestFailed(RootAccountInfoResponse existingRootAccount) {
			return existingRootAccount != null && !string.IsNullOrEmpty(existingRootAccount.ErrorMessage);
		}

		private bool IsRootAccountFilledCorrectly(RootAccountInfoResponse existingRootAccount) {
			return existingRootAccount.CreatioIdentityServerUrl == CreatioIdentityServerUrl &&
				existingRootAccount.CreatioUrl == CreatioUrl;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IRootAccountProvider.GetOrCreateSocialRootAccount"/>
		public RootAccountInfoResponse GetOrCreateSocialRootAccount() {
			RootAccountInfoResponse existingRootAccount = RootAccountServiceApi.GetRootAccount();
			FillRootAccountIfNecessary(existingRootAccount);
			return existingRootAccount;
		}

		/// <inheritdoc cref="IRootAccountRetriever.GetSocialRootAccount"/>
		public RootAccountInfoResponse GetSocialRootAccount() {
			RootAccountInfoResponse existingRootAccount = RootAccountServiceApi.GetRootAccount();
			if (IsGetAccountRequestFailed(existingRootAccount)) {
				return existingRootAccount;
			}
			if (IsExistingAccountNull(existingRootAccount)) {
				return default;
			}
			return existingRootAccount;
		}

		#endregion

	}

	#endregion

	/// <summary>
	/// Provides a root account from the external account service.
	/// </summary>
	public interface IRootAccountProvider
	{
		/// <summary>
		/// Gets the or create social root account.
		/// </summary>
		/// <remarks>
		/// Fills the Error Message property when an error occurs.
		/// </remarks>
		/// <returns>The root account.</returns>
		RootAccountInfoResponse GetOrCreateSocialRootAccount();
	}

	/// <summary>
	/// Retrieves a root account from the external account service.
	/// </summary>
	public interface IRootAccountRetriever
	{
		/// <summary>
		/// Gets the or create social root account.
		/// </summary>
		/// <remarks>
		/// Fills the Error Message property when an error occurs.
		/// </remarks>
		/// <returns>The root account.</returns>
		RootAccountInfoResponse GetSocialRootAccount();
	}
}

