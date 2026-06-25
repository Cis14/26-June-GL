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
	using Terrasoft.Common.Json;
	using System.Collections.Generic;
	using Newtonsoft.Json.Converters;
	using Newtonsoft.Json;

	/// <summary>
	/// Provides api to send response to external webhook service.
	/// </summary>
	public interface IWebhookServiceApi
	{
		/// <summary>
		/// Flushes the webhook entities cache.
		/// </summary>
		FlushWebhookEntitiesCacheResponse FlushWebhookEntitiesCache();

		/// <summary>
		/// Gets unprocessed webhooks count.
		/// </summary>
		GetUnprocessedWebhooksCountResponse GetUnprocessedWebhooksCount(GetUnprocessedWebhooksCountRequest request);

		/// <summary>
		/// Resend unprocessed webhooks.
		/// </summary>
		ResendUnprocessedWebhooksResponse ResendUprocessedWebhooks(ResendUnprocessedWebhooksRequest request);
	}

	/// <summary>
	/// Extends api to send response to external webhook service.
	/// </summary>
	public interface IWebhookExtendedServiceApi: IWebhookServiceApi
	{
		/// <summary>
		/// Gets unprocessed webhooks count.
		/// </summary>
		GetUnprocessedWebhooksByReasonCountResponse GetUnprocessedWebhooksByReasonCount(GetUnprocessedWebhooksCountRequest request);
	}

	/// <inheritdoc cref="IWebhookServiceApi"/>
	[DefaultBinding(typeof(IWebhookServiceApi))]
	[DefaultBinding(typeof(IWebhookExtendedServiceApi))]
	public class WebhookServiceApi: IWebhookExtendedServiceApi
	{

		#region Constants: Private

		private const string Scope = "webhook.creatio.full_access";

		private const string WebhookServiceUrlSysSettingCode = "WebhookServiceUrl";

		private const string CountEndpoint = "api/webhook-retainer/count";

		private const string ResendEndpoint = "api/webhook-retainer/re-send";

		#endregion

		#region Fields: Private

		private IHttpRequestClient _httpRequestClient;

		private IdentityServerProvider _identityServerProvider;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Constructor of a class.
		/// </summary>
		/// <param name="userConnection">Instance of UserConnection.</param>
		public WebhookServiceApi(UserConnection userConnection) {
			UserConnection = userConnection;
		}

		#endregion

		#region Properties: Private

		private IdentityServerProvider IdentityServerProvider =>
			_identityServerProvider ?? (_identityServerProvider = CreateIdentityServerProvider());

		private string WebhookServiceUrl {
			get {
				var webhookServiceUrl = CoreConfig.SysSettings.GetValue(UserConnection, WebhookServiceUrlSysSettingCode)
					.ToString();
				if (string.IsNullOrEmpty(webhookServiceUrl)) {
					throw new Common.ItemNotFoundException(WebhookServiceUrlSysSettingCode);
				}
				return webhookServiceUrl;
			}
		}

		private IHttpRequestClient HttpRequestClient {
			get {
				if (_httpRequestClient != null) {
					return _httpRequestClient;
				}
				_httpRequestClient = ClassFactory.Get<IHttpRequestClient>();
				return _httpRequestClient;
			}
			set => _httpRequestClient = value;
		}

		private UserConnection UserConnection { get; }

		#endregion

		#region Private: Classes
		
		private class ExtendedGetUnprocessedWebhooksCountRequest: GetUnprocessedWebhooksCountRequest {
			public ExtendedGetUnprocessedWebhooksCountRequest(GetUnprocessedWebhooksCountRequest request) {
				this.Types = request.Types;
				this.ApiKeys = request.ApiKeys;
			}
			public string By { get; set; } = "Type";
		}

		#endregion

		#region Methods: Private

		private HttpRequestConfig CreateHttpRequestConfigWithToken(string url) {
			string token = IdentityServerProvider.GetAccessTokenWithExceptionHandle(Scope);
			var request = new HttpRequestConfig {
				Url = new Uri(url),
				Method = HttpRequestMethod.POST
			};
			request.Headers.Add("Authorization", $"Bearer {token}");
			return request;
		}

		private IdentityServerProvider CreateIdentityServerProvider() {
			var userConnectionArg = new ConstructorArgument("userConnection", UserConnection);
			return ClassFactory.Get<IdentityServerProvider>(userConnectionArg);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IWebhookServiceApi.FlushWebhookEntitiesCache"/>
		public FlushWebhookEntitiesCacheResponse FlushWebhookEntitiesCache() {
			var url = $"{WebhookServiceUrl}/flush_entities_cache";
			HttpRequestConfig httpRequestConfig = CreateHttpRequestConfigWithToken(url);
			IHttpResponse response = HttpRequestClient.Send(httpRequestConfig);
			return new FlushWebhookEntitiesCacheResponse {
				IsSuccess = response.StatusCode == HttpStatusCode.OK
			};
		}

		/// <inheritdoc cref="IWebhookServiceApi.GetUnprocessedWebhooksCount"/>
		public GetUnprocessedWebhooksCountResponse GetUnprocessedWebhooksCount(GetUnprocessedWebhooksCountRequest request) {
			var url = $"{WebhookServiceUrl}/{CountEndpoint}";
			HttpRequestConfig httpRequestConfig = CreateHttpRequestConfigWithToken(url);
			var apiRequest = new ExtendedGetUnprocessedWebhooksCountRequest(request);
			apiRequest.By = "Type";
			var jsonBody = JsonConvert.SerializeObject(apiRequest, new StringEnumConverter());
			IHttpResponse httpResponse = HttpRequestClient.SendWithJsonBody(httpRequestConfig, jsonBody);
			GetUnprocessedWebhooksCountResponse response = new GetUnprocessedWebhooksCountResponse() {
				Success = httpResponse.StatusCode == HttpStatusCode.OK
			};
			if (response.Success) {
				response.Counts = JsonConvert.DeserializeObject<Dictionary<WebhookPersistenceType, long>>(httpResponse.Content);
			}
			return response;
		}

		/// <inheritdoc cref="IWebhookExtendedServiceApi.GetUnprocessedWebhooksByReasonCount"/>
		public GetUnprocessedWebhooksByReasonCountResponse GetUnprocessedWebhooksByReasonCount(GetUnprocessedWebhooksCountRequest request) {
			var url = $"{WebhookServiceUrl}/{CountEndpoint}";
			HttpRequestConfig httpRequestConfig = CreateHttpRequestConfigWithToken(url);
			var apiRequest = new ExtendedGetUnprocessedWebhooksCountRequest(request);
			apiRequest.By = "RejectionReason";
			var jsonBody = JsonConvert.SerializeObject(apiRequest, new StringEnumConverter());
			IHttpResponse httpResponse = HttpRequestClient.SendWithJsonBody(httpRequestConfig, jsonBody);
			GetUnprocessedWebhooksByReasonCountResponse response = new GetUnprocessedWebhooksByReasonCountResponse() {
				Success = httpResponse.StatusCode == HttpStatusCode.OK
			};
			if (response.Success) {
				response.Counts = JsonConvert.DeserializeObject<Dictionary<WebhookRejectionReason, long>>(httpResponse.Content);
			}
			return response;
		}

		/// <inheritdoc cref="IWebhookServiceApi.ResendUprocessedWebhooks"/>
		public ResendUnprocessedWebhooksResponse ResendUprocessedWebhooks(ResendUnprocessedWebhooksRequest request) {
			var url = $"{WebhookServiceUrl}/{ResendEndpoint}";
			HttpRequestConfig httpRequestConfig = CreateHttpRequestConfigWithToken(url);
			var jsonBody = JsonConvert.SerializeObject(request, new StringEnumConverter());
			IHttpResponse httpResponse = HttpRequestClient.SendWithJsonBody(httpRequestConfig, jsonBody);
			ResendUnprocessedWebhooksResponse response = new ResendUnprocessedWebhooksResponse() {
				Success = httpResponse.StatusCode == HttpStatusCode.Accepted
			};
			return response;
		}

		#endregion

	}

}

