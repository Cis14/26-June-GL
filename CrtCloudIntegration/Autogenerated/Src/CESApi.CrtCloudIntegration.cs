namespace Terrasoft.Configuration.CES
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using global::Common.Logging;
	using Terrasoft.Configuration.BpmonlineCloudIntegration;
	using Terrasoft.Configuration.CESModels;
	using Terrasoft.Configuration.CESResponses;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Factories;

	#region Class: CESApi

	/// <summary>
	/// Introduces API for Cloud Email Service.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.CES.ICESApi"/>
	public class CESApi : ICESApi
	{

		#region Constants: Private

		private const string AccountInfoApiPath = "/account/info.json";
		private const string AddSenderDomainsInfoApiPath = "/account/mailing/sender-domains/add.json";

		private const string AuthScope = "redskins.ces_client";
		private const string ConfigurationVersionSysSettingCode = "ConfigurationVersion";
		private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
		private const string GetCheckedEmailsApiPath = "/account/mailing/checked-email.json";
		private const string PingApiPath = "/users/ping.json";
		private const string SenderDomainsInfoApiPath = "/account/mailing/sender-domains.json";
		private const string SendMessageApiPath = "/messages/send.json";
		private const string SendTemplateApiPath = "/messages/send-template.json";
		private const string UpdateUserSettingsApiPath = "/account/update-user-settings.json";
		private const string ValidateSenderApiPath = "/users/validate";

		#endregion

		#region Fields: Private

		private readonly IdentityServerProvider _isProvider;
		
		private readonly ILog _logger = LogManager.GetLogger(typeof(CESApi));

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Provides an API for Cloud Email Service operations.
		/// </summary>
		public CESApi(UserConnection userConnection, string apiKey, string baseUrl) {
			_userConnection = userConnection;
			_isProvider = ClassFactory.Get<IdentityServerProvider>(new ConstructorArgument("userConnection",
				userConnection));
			ApiKey = apiKey;
			BaseUrl = baseUrl;
			ServiceClient = CreateServiceClient(baseUrl);
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// The main rest client for use throughout the whole app.
		/// </summary>
		public IWebServiceClient ServiceClient { get; set; }

		/// <summary>
		/// The Api Key for the project received from the CESApp website.
		/// </summary>
		public string ApiKey { get; }

		/// <summary>
		/// Service base url.
		/// </summary>
		public string BaseUrl { get; set; }

		/// <summary>
		/// Service base secure url.
		/// </summary>
		public string BaseSecureUrl { get; set; }

		#endregion

		#region Methods: Private

		private WebServiceClient CreateServiceClient(string baseUrl) {
			
			var client = new WebServiceClient(baseUrl) {
				ContentType = RequestContentType.Json,
				Headers = {}
			};
			try {
				string token = _isProvider.GetAccessTokenWithExceptionHandle(AuthScope);
				client.Headers["Authorization"] = $"Bearer {token}";
			} catch (InvalidIdentityServerSettingsException ex) {
				_logger.WarnFormat("Token for IdentityServerProvider has not been obtained.", ex.Message);
			} catch (RequestTokenException ex) {
				_logger.ErrorFormat("Token for IdentityServerProvider has not been obtained: '{0}'", ex.ErrorCode);
			} catch (Exception ex) {
				_logger.ErrorFormat("Token for IdentityServerProvider has not been obtained: '{0}'", ex.Message);
			}
			return client;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Returns the mailing account info.
		/// </summary>
		/// <param name="authKey"></param>
		/// <returns>The <see cref="AccountInfo"/>.</returns>
		public AccountInfo AccountInfo(string authKey) {
			if (string.IsNullOrEmpty(BaseUrl)) {
				_logger.Debug("CES service is not configured: BaseUrl is empty.");
				return null;
			}
			var request = new AccountInfoRequest {
				AuthKey = authKey,
				ApiKey = ApiKey
			};
			return ServiceClient.Post<AccountInfo>(request, AccountInfoApiPath);
		}

		/// <summary>
		/// Adds the sender domain.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <returns>Information about added domain from provider. <see cref="CloudSenderDomainsInfo"/></returns>
		public CloudSenderDomainsInfo AddSenderDomainsInfo(string domain) {
			var request = new AddSenderDomainsInfoRequest {
				ApiKey = ApiKey,
				Domain = domain
			};
			return ServiceClient.Post<CloudSenderDomainsInfo>(request, AddSenderDomainsInfoApiPath);
		}

		/// <summary>
		/// Add a new template.
		/// </summary>
		/// <param name="name">The name for the new template - must be unique.</param>
		/// <param name="fromEmail">A default sending address for emails sent using this template.</param>
		/// <param name="fromName">A default from name to be used.</param>
		/// <param name="subject">A default subject line to be used.</param>
		/// <param name="code">
		/// The HTML code for the template with <c>mc:edit</c> attributes for
		/// the editable elements.
		/// </param>
		/// <param name="text">A default text part to be used when sending with this template.</param>
		/// <param name="emailId">Unique id of email.</param>
		/// <param name="images">List of inline attachments.</param>
		/// <param name="checksum">Current template checksum.</param>
		/// <param name="replicaId">The replica identifier.</param>
		/// <returns>
		/// A <see cref="BaseCloudResponse"/> object.
		/// </returns>
		public BaseCloudResponse AddTemplate(string name, string fromEmail, string fromName, string subject,
			string code, string text, Guid emailId, IEnumerable<image> images = null, string checksum = null,
			string replicaId = null) {
			var request = new AddTemplateRequest {
				Name = name,
				FromEmail = fromEmail,
				FromName = fromName,
				Subject = subject,
				Code = code,
				Text = text,
				EmailId = emailId,
				Images = images,
				ReplicaChecksum = checksum,
				ReplicaId = replicaId
			};
			return this.AddTemplate(request, ServiceClient);
		}

		/// <summary>
		/// Gets the checked emails by api key.
		/// </summary>
		/// <param name="emails">Enumerable of emails.</param>
		/// <returns></returns>
		public CheckedEmailResponse GetCheckedEmails(IEnumerable<string> emails) {
			var request = new CheckedEmailRequest {
				ApiKey = ApiKey,
				Emails = emails
			};
			return ServiceClient.Post<CheckedEmailResponse>(request, GetCheckedEmailsApiPath);
		}

		/// <summary>
		/// Validate an API key and respond to a ping
		/// </summary>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public string Ping(ActualLicenseInfoContract licInfo) {
			ServiceClient.ContentType = RequestContentType.Json;
			var version = SysSettings.GetValue(_userConnection, 
				ConfigurationVersionSysSettingCode)?.ToString() ?? string.Empty;
			var request = new PingServiceRequest {
				ApiKey = ApiKey,
				ActualLicenseInfo = licInfo,
				Version = version
			};
			return ServiceClient.Post<string>(request, PingApiPath);
		}

		/// <summary>
		/// Gets the list of sender's domains from provider.
		/// </summary>
		/// <returns>
		/// The <see cref="CloudSenderDomainsInfo"/>.
		/// </returns>
		public CloudSenderDomainsInfo SenderDomainsInfo() {
			var request = new BaseServiceRequest {
				ApiKey = ApiKey
			};
			return ServiceClient.Post<CloudSenderDomainsInfo>(request, SenderDomainsInfoApiPath);
		}

		/// <summary>
		/// Sends a new transactional message through CES.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="emailId"></param>
		/// <param name="sendAt">The send_at.</param>
		/// <returns>
		///     <see cref="BaseCloudResponse"/>
		/// </returns>
		public BaseCloudResponse SendMessage(EmailMessage message, Guid emailId, DateTime sendAt = new DateTime()) {
			sendAt = sendAt.ToUniversalTime();
			var request = new SendMessageRequest {
				ApiKey = ApiKey,
				Message = message
			};
			if (emailId != Guid.Empty) {
				request.EmailId = emailId;
			}
			request.SendAt = sendAt == DateTime.MinValue ? string.Empty : sendAt.ToString(DateTimeFormat);
			return ServiceClient.Post<BaseCloudResponse>(request, SendMessageApiPath);
		}

		/// <summary>
		/// Send a new transactional message through CES using a template
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="templateName"></param>
		/// <param name="templateContents"></param>
		/// <param name="emailId">Identifier of email.</param>
		/// <param name="sendAt">The send_at.</param>
		/// <returns>
		///     <see cref="BaseCloudResponse"/>
		/// </returns>
		public BaseCloudResponse SendTemplate(EmailMessage message, string templateName,
			IEnumerable<TemplateContent> templateContents, Guid emailId, DateTime sendAt = new DateTime()) {
			sendAt = sendAt.ToUniversalTime();
			var request = new SendTemplateRequest {
				ApiKey = ApiKey,
				Message = message,
				TemplateName = templateName,
				EmailId = emailId,
				SendAt = sendAt == DateTime.MinValue ? string.Empty : sendAt.ToString(DateTimeFormat)
			};
			return ServiceClient.Post<BaseCloudResponse>(request, SendTemplateApiPath);
		}

		/// <summary>
		/// Updates the user settings.
		/// </summary>
		/// <param name="webHookAppDomain"></param>
		/// <param name="authKey"></param>
		/// <returns>
		/// The <see cref="AccountInfo"/>.
		/// </returns>
		public AccountInfo UpdateUserSettings(string webHookAppDomain, string authKey) {
			var request = new UpdateUserSettingsRequest {
				ApiKey = ApiKey,
				AuthKey = authKey,
				WebHookAppDomain = webHookAppDomain
			};
			return ServiceClient.Post<AccountInfo>(request, UpdateUserSettingsApiPath);
		}

		/// <summary>
		/// Validates sender's emails and send validation emails if needed.
		/// </summary>
		/// <param name="emails">List of sender's emails for validation</param>
		/// <returns>
		/// Lists of incorrect and validated emails
		/// </returns>
		public SenderValidationInfo ValidateSender(IEnumerable<string> emails) {
			var request = new ValidateSenderRequest {
				EmailList = emails,
				ApiKey = ApiKey
			};
			return ServiceClient.Post<SenderValidationInfo>(request, ValidateSenderApiPath);
		}

		#endregion

	}

	/// <summary>
	/// Provides extensions for Cloud email service API methods for <see cref="ICESApi"/>.
	/// </summary>
	public static class CESApiExtension
	{

		#region Constants: Private

		private const string AddSenderDomainsInfoApiPath = "/account/mailing/sender-domains/add.json";
		private const string AddTemplateApiPath = "/templates/add.json";
		private const string AuthorizeDomainApiPath = "/account/mailing/sender-domains/authorize";
		private const string GetAvailableProvidersApiPath = "/account/mailing/providers";
		private const string GetThrottlingSchedulesApiPath = "/throttling/get-schedules";
		private const string LicenceInformationApiPath = "/license/get-info";
		private const string MessagesUpdateScheduleApiPath = "/messages/update-schedule";
		private const string SenderDomainsInfoApiPath = "/account/mailing/sender-domains.json";
		private const string ShareDomainApiPath = "/account/mailing/sender-domains/share";
		private const string StopEmailApiPath = "/messages/stop.json";
		private const string ThrottlingScheduleApiPath = "/throttling/schedule.json";
		private const string UpdateAccountMailingProviderApiPath = "/account/update-mailing-provider-settings";
		private const string ValidateSenderApiPath = "/users/validate";
		private const string ValidateSenderDomainApiPath = "/account/mailing/sender-domains/validate.json";
		private const string DeleteSenderDomainApiPath = "/account/mailing/sender-domains/delete.json";

		#endregion

		#region Methods: Private

		private static IWebServiceClient GetServiceClient(ICESApi cesApi, IWebServiceClient serviceClient = null) {
			if (serviceClient != null) {
				return serviceClient;
			}
			if (cesApi is CESApi cesApiInstance) {
				return cesApiInstance.ServiceClient;
			}
			return new WebServiceClient(cesApi.BaseUrl) {
				ContentType = RequestContentType.Json
			};
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Adds the sender domains information.
		/// </summary>
		/// <param name="cesApi">The ces API.</param>
		/// <param name="request">The request.</param>
		/// <param name="serviceClient">The service client.</param>
		public static CloudSenderDomainsInfo AddSenderDomainsInfo(this ICESApi cesApi,
			AddSenderDomainsInfoRequest request, IWebServiceClient serviceClient = null) {
			request.ApiKey = cesApi.ApiKey;
			IWebServiceClient internalServiceClient = GetServiceClient(cesApi, serviceClient);
			return internalServiceClient.Post<CloudSenderDomainsInfo>(request, AddSenderDomainsInfoApiPath);
		}

		/// <summary>
		/// Sends request to add email template.
		/// </summary>
		/// <param name="cesApi">Extending instance.</param>
		/// <param name="request">Template add request.</param>
		/// <param name="serviceClient">Web service client. If Null - uses default instance.</param>
		/// <returns></returns>
		public static BaseCloudResponse AddTemplate(this ICESApi cesApi, AddTemplateRequest request,
			IWebServiceClient serviceClient = null) {
			request.ApiKey = cesApi.ApiKey;
			IWebServiceClient internalServiceClient = GetServiceClient(cesApi, serviceClient);
			return internalServiceClient.Post<BaseCloudResponse>(request, AddTemplateApiPath);
		}

		/// <summary>
		/// Authorizes a domain with the cloud email service.
		/// </summary>
		/// <param name="cesApi">
		/// The CES API instance used to perform the operation.
		/// </param>
		/// <param name="request">
		/// The request containing the details for domain authorization.
		/// </param>
		/// <returns>
		/// A response object containing the result of the domain authorization operation.
		/// </returns>
		public static AuthorizeDomainResponse AuthorizeDomain(this ICESApi cesApi, AuthorizeDomainRequest request) {
			IWebServiceClient client = GetServiceClient(cesApi);
			return client.Post<AuthorizeDomainResponse>(request, AuthorizeDomainApiPath);
		}

		/// <summary>
		/// Connects mailing provider for account.
		/// </summary>
		/// <param name="cesApi">Extending instance.</param>
		/// <param name="request">Info about cloud account services.</param>
		/// <param name="serviceClient">Web service client. If Null - uses default instance.</param>
		/// <returns>The <see cref="BaseCloudResponse"/> with code, status and message.</returns>
		public static BaseCloudResponse ConnectMailingProvider(this ICESApi cesApi,
			UpdateCloudAccountServiceRequest request, IWebServiceClient serviceClient = null) {
			request.ApiKey = cesApi.ApiKey;
			IWebServiceClient internalServiceClient = GetServiceClient(cesApi, serviceClient);
			var response = internalServiceClient.Post<BaseCloudResponse>(request, UpdateAccountMailingProviderApiPath);
			return response;
		}

		/// <summary>
		/// Gets account's license information by given API key.
		/// </summary>
		/// <param name="cesApi">Extending instance.</param>
		/// <param name="request">Base cloud request with API key.</param>
		/// <param name="serviceClient">Web service client. If Null - uses default instance.</param>
		/// <returns></returns>
		public static EmailLicenseInformationResponse GetAccountLicenseInformation(this ICESApi cesApi,
			BaseServiceRequest request, IWebServiceClient serviceClient = null) {
			IWebServiceClient internalServiceClient = GetServiceClient(cesApi, serviceClient);
			return internalServiceClient is IWebServiceClientWithRetry retryServiceClient
				? retryServiceClient.PostWithRetry<EmailLicenseInformationResponse>(request, LicenceInformationApiPath)
				: internalServiceClient.Post<EmailLicenseInformationResponse>(request, LicenceInformationApiPath);
		}

		/// <summary>
		/// Sends request to set new throttling schedule.
		/// </summary>
		/// <param name="cesApi">Extending instance.</param>
		/// <param name="request">Request instance with new schedule <see cref="ThrottlingSchedule"/></param>
		/// <param name="serviceClient">Web service client. If Null - uses default instance.</param>
		/// <returns>The <see cref="GetAvailableProvidersResponse"/>.</returns>
		public static GetAvailableProvidersResponse GetAvailableProviders(this ICESApi cesApi,
			IWebServiceClient serviceClient = null) {
			var request = new GetAvailableProvidersRequest {
				ApiKey = cesApi.ApiKey
			};
			IWebServiceClient internalServiceClient = GetServiceClient(cesApi, serviceClient);
			return internalServiceClient.Post<GetAvailableProvidersResponse>(request, GetAvailableProvidersApiPath);
		}

		/// <summary>
		/// Gets throttling schedules for the current user by the specified strategy id.
		/// </summary>
		/// <param name="cesApi">Extending instance <see cref="ICESApi"/>.</param>
		/// <param name="throttlingScheduleRequest">Request instance with the required service id.</param>
		/// <returns>The <see cref="GetThrottlingSchedulesResponse"/> with throttling schedules.</returns>
		public static GetThrottlingSchedulesResponse GetThrottlingSchedules(this ICESApi cesApi,
			GetThrottlingScheduleRequest throttlingScheduleRequest) {
			IWebServiceClient internalServiceClient = GetServiceClient(cesApi);
			return internalServiceClient.Post<GetThrottlingSchedulesResponse>(throttlingScheduleRequest,
				GetThrottlingSchedulesApiPath);
		}

		/// <summary>
		/// Gets the list of sender's domains from provider.
		/// </summary>
		/// <returns>
		/// The <see cref="CloudSenderDomainsInfo"/>.
		/// </returns>
		public static CloudSenderDomainsInfo SenderDomainsInfo(this ICESApi cesApi, SenderDomainsInfoRequest request) {
			if (string.IsNullOrEmpty(cesApi.BaseUrl)) {
				LogManager.GetLogger(typeof(CESApiExtension)).Debug("CES service is not configured: BaseUrl is empty.");
				return null;
			}
			IWebServiceClient serviceClient = GetServiceClient(cesApi);
			return serviceClient.Post<CloudSenderDomainsInfo>(request, SenderDomainsInfoApiPath);
		}

		/// <summary>
		/// Sends request to set new throttling schedule.
		/// </summary>
		/// <param name="cesApi">Extending instance.</param>
		/// <param name="request">Request instance with new schedule <see cref="ThrottlingSchedule"/></param>
		/// <param name="serviceClient">Web service client. If Null - uses default instance.</param>
		/// <returns>The <see cref="BaseCloudResponse"/>.</returns>
		public static BaseCloudResponse SetThrottlingSchedule(this ICESApi cesApi, ThrottlingScheduleRequest request,
			IWebServiceClient serviceClient = null) {
			request.ApiKey = cesApi.ApiKey;
			IWebServiceClient internalServiceClient = GetServiceClient(cesApi, serviceClient);
			return internalServiceClient.Post<BaseCloudResponse>(request, ThrottlingScheduleApiPath);
		}

		/// <summary>
		/// Shares a sender domain using the Cloud Email Service API.
		/// </summary>
		/// <param name="cesApi">
		/// The instance of the <see cref="ICESApi"/> to execute the operation.
		/// </param>
		/// <param name="request">
		/// The details of the sender domain to be shared.
		/// </param>
		/// <returns>
		/// The response containing details about the shared domain.
		/// </returns>
		public static ShareDomainResponse ShareDomain(this ICESApi cesApi, ShareDomainRequest request) {
			IWebServiceClient client = GetServiceClient(cesApi);
			return client.Post<ShareDomainResponse>(request, ShareDomainApiPath);
		}

		/// <summary>
		/// Sends request to stop bulk email.
		/// </summary>
		/// <param name="cesApi">Extending instance.</param>
		/// <param name="bulkEmailId">BulkEmail Id</param>
		/// <param name="serviceClient">Web service client. If Null - uses default instance.</param>
		/// <returns>The <see cref="BaseCloudResponse"/>.</returns>
		public static BaseCloudResponse StopBulkEmail(this ICESApi cesApi, Guid bulkEmailId,
			IWebServiceClient serviceClient = null) {
			var request = new StopEmailRequest {
				ApiKey = cesApi.ApiKey,
				EmailId = bulkEmailId
			};
			IWebServiceClient internalServiceClient = GetServiceClient(cesApi, serviceClient);
			return internalServiceClient.Post<BaseCloudResponse>(request, StopEmailApiPath);
		}

		/// <summary>
		/// Sends request to set new email schedule.
		/// </summary>
		/// <param name="cesApi">Extending instance.</param>
		/// <param name="request">Request instance with new email schedule <see cref="UpdateEmailScheduleRequest"/></param>
		/// <returns>The <see cref="BaseCloudResponse"/>.</returns>
		public static BaseCloudResponse UpdateEmailSchedule(this ICESApi cesApi,
			UpdateEmailScheduleCloudRequest request) {
			request.ApiKey = cesApi.ApiKey;
			IWebServiceClient internalServiceClient = GetServiceClient(cesApi);
			return internalServiceClient.Post<BaseCloudResponse>(request, MessagesUpdateScheduleApiPath);
		}

		/// <summary>
		/// Validates sender domain.
		/// </summary>
		/// <param name="cesApi">Extending instance.</param>
		/// <param name="request"><see cref="CloudValidateDomainRequest"/>Request with parameters provided.</param>
		/// <returns>
		/// Validation result
		/// </returns>
		public static CloudDomainValidationResponse ValidateSenderDomain(this ICESApi cesApi,
			CloudValidateDomainRequest request) {
			IWebServiceClient serviceClient = GetServiceClient(cesApi);
			return serviceClient.Post<CloudDomainValidationResponse>(request, ValidateSenderDomainApiPath);
		}

		/// <summary>
		/// Validates sender's emails for stored ESP.
		/// </summary>
		/// <param name="cesApi">Extending instance.</param>
		/// <param name="request"><see cref="ValidateSenderRequest"/>Request with parameters provided.</param>
		/// <returns>
		/// Lists of incorrect and validated emails
		/// </returns>
		public static SenderValidationInfo
			ValidateSenderForProvider(this ICESApi cesApi, ValidateSenderRequest request) {
			IWebServiceClient serviceClient = GetServiceClient(cesApi);
			return serviceClient.Post<SenderValidationInfo>(request, ValidateSenderApiPath);
		}

		/// <summary>
		/// Validates sender domain.
		/// </summary>
		/// <param name="cesApi">Extending instance.</param>
		/// <param name="request"><see cref="CloudValidateDomainRequest"/>Request with parameters provided.</param>
		/// <returns>
		/// Validation result
		/// </returns>
		public static BaseCloudResponse DeleteSenderDomain(this ICESApi cesApi,
			DeleteSenderDomainInfoRequest request) {
			IWebServiceClient serviceClient = GetServiceClient(cesApi);
			return serviceClient.Post<BaseCloudResponse>(request, DeleteSenderDomainApiPath);
		}

		#endregion

	}

	/// <summary>
	/// Represents a request for sharing a domain with a cloud email service provider.
	/// </summary>
	[DataContract]
	public class ShareDomainRequest
	{

		#region Properties: Public

		/// <summary>
		/// The API key used for authentication with the CES cloud email services.
		/// </summary>
		[DataMember(Name = "apiKey")]
		public string ApiKey { get; set; }

		/// <summary>
		/// Specifies the domain associated with an email service provider for operations such as sharing or authorization.
		/// </summary>
		[DataMember(Name = "domain")]
		public string Domain { get; set; }

		/// <summary>
		/// The name of the cloud email service provider.
		/// </summary>
		[DataMember(Name = "providerName")]
		public string ProviderName { get; set; }

		#endregion

	}

	/// <summary>
	/// Represents a response for sharing a domain with a cloud email service provider.
	/// </summary>
	[DataContract]
	public class ShareDomainResponse
	{

		#region Properties: Public

		/// <summary>
		/// Represents the token used for authorization domains.
		/// </summary>
		[DataMember(Name = "token")]
		public string Token { get; set; }

		/// <summary>
		/// Indicates the current status of the domain sharing response,
		/// providing feedback from the cloud email service provider.
		/// </summary>
		[DataMember(Name = "status")]
		public string Status { get; set; }

		/// <summary>
		/// Provides a descriptive message in the response, typically conveying a status or error information.
		/// </summary>
		[DataMember(Name = "message")]
		public string Message { get; set; }

		/// <summary>
		/// The numeric error or status code associated with the response.
		/// </summary>
		[DataMember(Name = "code")]
		public int Code { get; set; }

		#endregion

	}

	/// <summary>
	/// Represents a request for authorizing a domain with a cloud email provider.
	/// </summary>
	[DataContract]
	public class AuthorizeDomainRequest
	{

		#region Properties: Public

		/// <summary>
		/// The API key used for authentication and authorization with the CES system.
		/// </summary>
		[DataMember(Name = "apiKey")]
		public string ApiKey { get; set; }

		/// <summary>
		/// The domain to be authorized with the cloud email provider.
		/// </summary>
		[DataMember(Name = "domain")]
		public string Domain { get; set; }

		/// <summary>
		/// The name of the email service provider.
		/// </summary>
		[DataMember(Name = "providerName")]
		public string ProviderName { get; set; }

		/// <summary>
		/// Represents the token used for authorization domains.
		/// </summary>
		[DataMember(Name = "token")]
		public string Token { get; set; }

		#endregion

	}

	/// <summary>
	/// Represents a response for authorizing a domain with a cloud email provider.
	/// </summary>
	public class AuthorizeDomainResponse : CloudSenderDomainsInfo { }

	#endregion

}

