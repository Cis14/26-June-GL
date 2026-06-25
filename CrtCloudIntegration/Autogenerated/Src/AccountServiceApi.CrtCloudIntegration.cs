namespace Terrasoft.Configuration
{
	using System;
	using System.Net;
	using Terrasoft.Configuration.BpmonlineCloudIntegration;
	using Terrasoft.Core;
	using CoreConfig = Terrasoft.Core.Configuration;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Requests;
	using IHttpResponse = Terrasoft.Core.Requests.IHttpResponse;
    using Newtonsoft.Json;

    #region Class: AccountServiceApi

    /// <summary>
    /// Provides api to send response to external account service.
    /// </summary>
    public class AccountServiceApi
	{

		#region Constants: Private

		/// <summary>
		/// The webhook account service URL system setting code
		/// </summary>
		private const string AccountServiceUrlSysSettingCode = "SocialAccountServiceUrl";

		#endregion

		#region Fields: Private

		private readonly string _scope;

		private IHttpRequestClient _httpRequestClient;

		private IdentityServerProvider _identityServerProvider;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Constructor of a class.
		/// </summary>
		/// <param name="userConnection">Instance of UserConnection.</param>
		/// <param name="scope">The name of identity scope.</param>
		public AccountServiceApi(UserConnection userConnection, string scope) {
			_scope = scope;
			UserConnection = userConnection;
		}

		#endregion

		#region Properties: Private

		/// <summary>
		/// Identity server provider instance. Returns established provider.
		/// If established is null then create instance of <see cref="IdentityServerProvider"/> 
		/// from <see cref="ClassFactory"/>.
		/// </summary>
		private IdentityServerProvider IdentityServerProvider =>
			_identityServerProvider ?? (_identityServerProvider = CreateIdentityServerProvider());

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Gets the webhook account service URL.
		/// </summary>
		protected string AccountServiceUrl {
			get {
				var webhookAccountServiceUrl = CoreConfig.SysSettings
					.GetValue(UserConnection, AccountServiceUrlSysSettingCode).ToString();
				if (string.IsNullOrEmpty(webhookAccountServiceUrl)) {
					throw new Common.ItemNotFoundException(AccountServiceUrlSysSettingCode);
				}
				return webhookAccountServiceUrl;
			}
		}

		/// <summary>
		/// Gets or sets the rest client for Api calls.
		/// </summary>
		/// <value>
		/// The rest client.
		/// </value>
		protected IHttpRequestClient HttpRequestClient {
			get {
				if (_httpRequestClient != null) {
					return _httpRequestClient;
				}
				_httpRequestClient = ClassFactory.Get<IHttpRequestClient>();
				return _httpRequestClient;
			}
			set => _httpRequestClient = value;
		}

		/// <summary>
		/// User connection.
		/// </summary>
		protected UserConnection UserConnection { get; }

		#endregion

		#region Methods: Private

		private HttpRequestConfig CreateHttpRequestConfigWithToken(string url) {
			string token =
				IdentityServerProvider.GetAccessTokenWithExceptionHandle(_scope); //"webhook.creatio.full_access");
			var request = new HttpRequestConfig {
				Url = new Uri(url)
			};
			request.Headers.Add("Authorization", $"Bearer {token}");
			return request;
		}

		private IdentityServerProvider CreateIdentityServerProvider() {
			var userConnectionArg = new ConstructorArgument("userConnection", UserConnection);
			return ClassFactory.Get<IdentityServerProvider>(userConnectionArg);
		}

		private T GetResponseData<T>(IHttpResponse response)
			where T : BaseAccountResponse, new() {
			return response.GetResult<T>() ?? new T();
		}

		private string GetResponseError(IHttpResponse response) {
			if (response.StatusCode != HttpStatusCode.OK) {
				string errorMessage = response.GetResult<BaseAccountResponse>()?.ErrorMessage ?? response.Exception?.Message ?? response.ReasonPhrase;
				return errorMessage;
			}
			return string.Empty;
		}

		private T SendRequest<T>(HttpRequestMethod method, string url, object bodyObject = default)
			where T : BaseAccountResponse, new() {
			HttpRequestConfig httpRequestConfig = CreateHttpRequestConfigWithToken(url);
			httpRequestConfig.Method = method;
			string jsonContent = string.Empty;
			if(bodyObject != default) {
                jsonContent = JsonConvert.SerializeObject(bodyObject);
            }
            IHttpResponse response = bodyObject != default ? HttpRequestClient.SendWithJsonBody(httpRequestConfig, jsonContent)
				: HttpRequestClient.Send(httpRequestConfig);
			var result = GetResponseData<T>(response);
			result.ErrorMessage = GetResponseError(response);
			return result;
		}

		#endregion

		#region Methods: Protected

		protected void CheckResponseForError<T>(IHttpResponse response)
			where T : BaseAccountResponse, new() {
			if (response.StatusCode != HttpStatusCode.OK) {
				string errorMessage = response.GetResult<BaseAccountResponse>().ErrorMessage ?? response.Exception?.Message ?? response.ReasonPhrase;
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Sends the post request.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="url">The URL.</param>
		/// <param name="bodyObject">The body object.</param>
		/// <returns>Raw response from API.</returns>
		public T SendPostRequest<T>(string url, object bodyObject)
			where T : BaseAccountResponse, new() {
			return SendRequest<T>(HttpRequestMethod.POST, url, bodyObject);
		}

		#endregion

	}

	#endregion

}

