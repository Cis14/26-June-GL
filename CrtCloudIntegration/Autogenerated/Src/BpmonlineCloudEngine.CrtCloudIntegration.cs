namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Configuration.CES;
	using Terrasoft.Configuration.CESModels;
	using Terrasoft.Configuration.CESResponses;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.ServiceModelContract;
	using Terrasoft.Web.Http.Abstractions;


	#region Class: BpmonlineCloudEngine
	/// <summary>
	/// Implements tools for integration with bpmonline cloud.
	/// </summary>
	public class BpmonlineCloudEngine
	{

		#region Constants: Private

		private const string BpmonlineSignatureKey = "bpmonline-signature";

		#endregion

		#region Fields: Private
		
		private ISenderDomainCacheService _senderDomainCacheService;

		private ISenderDomainRepository _senderDomainRepository;

		private ISenderDomainValidationService _senderDomainValidationService;

		private ICESApi _serviceApi;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="userConnection">Instance of <see cref="UserConnection"/>.</param>
		public BpmonlineCloudEngine(UserConnection userConnection) {
			UserConnection = userConnection;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BpmonlineCloudEngine"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="serviceApi">The service API.</param>
		public BpmonlineCloudEngine(UserConnection userConnection, ICESApi serviceApi) {
			_serviceApi = serviceApi;
			UserConnection = userConnection;
		}

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Instance of <see cref="UserConnection"/>.
		/// </summary>
		protected UserConnection UserConnection { get; set; }

		#endregion

		#region Properties: Public

		/// <summary>
		/// Url of the remote service.
		/// </summary>
		public string ServiceUrl =>
			(string)Core.Configuration.SysSettings.GetValue(UserConnection, "CloudServicesBaseUrl");

		/// <summary>
		/// Api to interact with a remote service.
		/// </summary>
		public virtual ICESApi ServiceApi => _serviceApi ?? (_serviceApi = new CESApi(UserConnection, GetAPIKey(UserConnection), ServiceUrl));
		
		public virtual ISenderDomainCacheService SenderDomainCacheService =>
			_senderDomainCacheService ??
			(_senderDomainCacheService = ClassFactory.Get<ISenderDomainCacheService>(new ConstructorArgument("userConnection", UserConnection), new ConstructorArgument("serviceApi", ServiceApi)));

		/// <summary>
		/// Data caching service for the sender's domain.
		/// </summary>
		public virtual ISenderDomainRepository SenderDomainRepository =>
			_senderDomainRepository ??
			(_senderDomainRepository = ClassFactory.Get<ISenderDomainRepository>(new ConstructorArgument("userConnection", UserConnection), new ConstructorArgument("serviceApi", ServiceApi)));

		/// <summary>
		/// Validation service for the sender's domain.
		/// </summary>
		public virtual ISenderDomainValidationService SenderDomainValidationService =>
			_senderDomainValidationService ??
			(_senderDomainValidationService = ClassFactory.Get<ISenderDomainValidationService>(new ConstructorArgument("userConnection", UserConnection), new ConstructorArgument("serviceApi", ServiceApi)));

		#endregion

		#region Methods: Private

		private static string GetLczStringValue(UserConnection userConnection, string lczName) {
			string localizableStringName = string.Format("LocalizableStrings.{0}.Value", lczName);
			var localizableString = new LocalizableString(userConnection.Workspace.ResourceStorage,
				"BpmonlineCloudEngine", localizableStringName);
			string value = localizableString.Value;
			if (value.IsNullOrEmpty()) {
				value = localizableString.GetCultureValue(GeneralResourceStorage.DefCulture, false);
			}
			return value;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Authenticate cloud key in request.
		/// </summary>
		/// <param name="request">Instance of <see cref="HttpRequest"/> current request.</param>
		/// <param name="userConnection">Instance of <see cref="UserConnection"/> current request.</param>
		/// <returns>Authentication result <see cref="Terrasoft.Configuration.AuthenticationResult"/>.</returns>
		public static AuthenticationResult Authenticate(HttpRequest request, UserConnection userConnection) {
			var authenticationResult = new AuthenticationResult();
			if (!request.Headers.AllKeys.Contains(BpmonlineSignatureKey, StringComparer.InvariantCultureIgnoreCase)) {
				authenticationResult.Message = GetLczStringValue(userConnection, "SignatureNotFoundMessage");
				return authenticationResult;
			}
			string incomingAuthKey = request.Headers[BpmonlineSignatureKey];
			string cloudServicesAuthKey = GetAuthKey(userConnection);
			if (string.IsNullOrEmpty(cloudServicesAuthKey)) {
				authenticationResult.Message = GetLczStringValue(userConnection, "BpmonlineHasNoAuthKeyMessage");
			} else if (string.IsNullOrEmpty(incomingAuthKey)) {
				authenticationResult.Message = GetLczStringValue(userConnection, "RequestHasNoAuthKeyMessage");
			} else if (!string.Equals(incomingAuthKey, cloudServicesAuthKey)) {
				authenticationResult.Message = GetLczStringValue(userConnection, "WrongAuthKeyMessage");
			} else {
				authenticationResult.Message = GetLczStringValue(userConnection, "SuccessAuthenticationMessage");
				authenticationResult.Success = true;
			}
			return authenticationResult;
		}

		/// <summary>
		/// Gets the API key.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <returns>Api key withoud leading and trailing whitespaces.</returns>
		public static string GetAPIKey(UserConnection userConnection) {
			var key = (string)Core.Configuration.SysSettings.GetValue(userConnection, "CloudServicesAPIKey");
			if (key.IsNotNullOrEmpty()) {
				key = key.Trim();
			}
			return key;
		}

		public static string GetAuthKey(UserConnection userConnection) {
			return (string)Core.Configuration.SysSettings.GetValue(userConnection, "CloudServicesAuthKey");
		}

		/// <summary>
		/// Returns account info.
		/// </summary>
		/// <returns>Account info.</returns>
		public virtual AccountInfo AccountInfo() {
			string authKey = GetAuthKey(UserConnection);
			return ServiceApi.AccountInfo(authKey);
		}

		/// <summary>
		/// Adds sender domains.
		/// </summary>
		/// <param name="domain">Domain.</param>
		/// <returns>Sender domains info.</returns>
		public virtual CloudSenderDomainsInfo AddSenderDomain(string domain) {
			var addSenderDomainsInfoRequest = new AddSenderDomainsInfoRequest {
				ApiKey = ServiceApi.ApiKey,
				Domain = domain
			};
			var addDomainResponse = ServiceApi.AddSenderDomainsInfo(domain);
			if (addDomainResponse.Status == "ok") {
				var domainInfo = addDomainResponse.Domains.FirstOrDefault(dm => dm.Domain == domain);
				SenderDomainRepository.AddDomain(addSenderDomainsInfoRequest, domainInfo);
			}
			return addDomainResponse;
		}

		private static readonly int[] ShareDomainErrorCodes = new int[] { 115, 118 };

		/// <summary>
		/// Adds a sender domain for email services and retrieves related domain information.
		/// </summary>
		/// <param name="request">The request containing the domain to be added.</param>
		/// <returns>A response containing the status, domain information, and any error details if the operation fails.</returns>
		public virtual AddSenderDomainV2Response AddSenderDomainV2(AddSenderDomainV2Request request) {
			var result = ServiceApi.AddSenderDomainsInfo(request.Domain);
			if (result.Status == "ok") {
				var domainWithProvider = new AddSenderDomainsInfoRequest {
					ApiKey = ServiceApi.ApiKey,
					Domain = request.Domain
				};
				var domainInfo = result.Domains.FirstOrDefault(dm => dm.Domain == request.Domain);
				var senderDomain = SenderDomainRepository.AddDomain(domainWithProvider, domainInfo);
				return new AddSenderDomainV2Response {
					SenderDomainId = senderDomain.Id.ToString(),
					Success = true
				};
			}
			if (UserConnection.GetIsFeatureEnabled("BulkEmailSenderDomainSharing") && ShareDomainErrorCodes.Contains(result.Code)) {
				AccountInfo accountInfo = AccountInfo();
				var senderDomain = SenderDomainRepository.AddRequestedDomain(request.Domain, accountInfo.GetProviderName());
				return new AddSenderDomainV2Response {
					SenderDomainId = senderDomain.Id.ToString(),
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = result.Code.ToString(),
						Message = result.Message
					}
				};
			}
			return new AddSenderDomainV2Response {
				SenderDomainId = null,
				Success = false,
				ErrorInfo = new ErrorInfo {
					ErrorCode = result.Code.ToString(),
					Message = result.Message
				}
			};
		}

		/// <summary>
		/// Adds the sender domain.
		/// </summary>
		/// <param name="request">The add sender domains information request.</param>
		public CloudSenderDomainsInfo AddSenderDomain(AddSenderDomainsInfoRequest request) {
			request.ApiKey = ServiceApi.ApiKey;
			var addDomainResponse = ServiceApi.AddSenderDomainsInfo(request);
			if (addDomainResponse.Status == "ok") {
				var domainInfo = addDomainResponse.Domains.FirstOrDefault(domain => domain.Domain == request.Domain);
				SenderDomainRepository.AddDomain(request, domainInfo);
			}
			return addDomainResponse;
		}

		/// <summary>
		/// Authenticate cloud key in request.
		/// </summary>
		/// <param name="request">Instance of <see cref="HttpRequest"/> current request.</param>
		/// <returns>Authentication result <see cref="Terrasoft.Configuration.AuthenticationResult"/>.</returns>
		public virtual AuthenticationResult Authenticate(HttpRequest request) {
			return Authenticate(request, UserConnection);
		}

		/// <summary>
		/// Gets checked emails from provider.
		/// </summary>
		/// <param name="emails">Enumerable of emails.</param>
		public virtual CheckedEmailResponse GetCheckedEmails(IEnumerable<string> emails) {
			return ServiceApi.GetCheckedEmails(emails);
		}

		/// <summary>
		/// Returns sender domains info by the provider name.
		/// </summary>
		/// <param name="senderDomainsInfoRequest">The sender domains information request.</param>
		/// <returns>Sender domains info.</returns>
		public virtual CloudSenderDomainsInfo GetSenderDomainsInfo(SenderDomainsInfoRequest senderDomainsInfoRequest) {
			senderDomainsInfoRequest.ApiKey = ServiceApi.ApiKey;
			CloudSenderDomainsInfo senderDomainsInfo =
				SenderDomainCacheService.GetSenderDomainsInfo(senderDomainsInfoRequest);
			return senderDomainsInfo;
		}
		
		/// <summary>
		/// Refreshes sender domains info by the provider name.
		/// </summary>
		/// <param name="request">The sender domains information request.</param>
		/// <returns>Sender domains info.</returns>
		public virtual CloudSenderDomainsInfo RefreshSenderDomains(SenderDomainsInfoRequest request) {
			request.ApiKey = ServiceApi.ApiKey;
			CloudSenderDomainsInfo senderDomainsInfo =
				SenderDomainRepository.RefreshSenderDomains(request);
			return senderDomainsInfo;
		}

		/// <summary>
		/// Registers the sender's domain.
		/// </summary>
		/// <param name="domain">The sender's domain.</param>
		public virtual void RegisterSenderDomain(string domain) {
			CloudSenderDomainsInfo domainInfo = SenderDomainsInfo();
			if (!domainInfo.Domains.Exists(x => x.Domain == domain))
			{
				AddSenderDomain(domain);
			}
		}

		/// <summary>
		/// Returns sender domains info.
		/// </summary>
		/// <returns>Sender domains info.</returns>
		public virtual CloudSenderDomainsInfo SenderDomainsInfo() {
			var senderDomainsInfoRequest = new SenderDomainsInfoRequest {
				ApiKey = ServiceApi.ApiKey
			};
			CloudSenderDomainsInfo senderDomainsInfo =
				SenderDomainCacheService.GetSenderDomainsInfo(senderDomainsInfoRequest);
			return senderDomainsInfo;
		}

		/// <summary>
		/// Updates user settings.
		/// </summary>
		/// <param name="webHookAppDomain">WebHooks app domain.</param>
		/// <param name="authKey"></param>
		/// <returns>Account info.</returns>
		public virtual AccountInfo UpdateUserSettings(string webHookAppDomain, string authKey) {
			if (string.IsNullOrEmpty(authKey)) {
				authKey = GetAuthKey(UserConnection);
			}
			return ServiceApi.UpdateUserSettings(webHookAppDomain, authKey);
		}

		/// <summary>
		/// Gets <see cref="SenderValidationInfo"/> with separate emails enums.
		/// </summary>
		/// <param name="emails">Enumerable of emails.</param>
		public virtual SenderValidationInfo ValidateSender(IEnumerable<string> emails) {
			var request = new ValidateSenderRequest {
				ApiKey = ServiceApi.ApiKey,
				EmailList = emails
			};
			return ServiceApi.ValidateSenderForProvider(request);
		}

		/// <summary>
		/// Validates sender domain.
		/// </summary>
		/// <param name="domainId">Domain.</param>
		/// <returns>Sender domains info.</returns>
		public virtual ValidateDomainResponse ValidateSenderDomain(Guid domainId) {
			return SenderDomainValidationService.ValidateSenderDomain(domainId);
		}

		/// <summary>
		/// Shares a sender domain based on the provided request details.
		/// </summary>
		/// <param name="request">An instance of <see cref="ShareSenderDomainRequest"/> containing the details for sharing the sender domain.</param>
		/// <returns>An instance of <see cref="ShareSenderDomainResponse"/> containing the sender domain ID and authorization key.</returns>
		public ShareSenderDomainResponse ShareSenderDomain(ShareSenderDomainRequest request) {
			if (!Guid.TryParse(request.SenderDomainId, out Guid domainId)) {
				return new ShareSenderDomainResponse {
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = "1",
						Message = "SenderDomainId is not valid"
					}
				};
			}
			var senderDomain = SenderDomainRepository.GetDomainById(domainId);
			if (senderDomain == null) {
				return new ShareSenderDomainResponse {
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = "2",
						Message = "SenderDomain is not found"
					}
				};
			}
			var shareDomainRequest = new ShareDomainRequest {
				ApiKey = ServiceApi.ApiKey,
				Domain = senderDomain.Domain,
				ProviderName = senderDomain.ProviderName
			};
			try {
				var response = ServiceApi.ShareDomain(shareDomainRequest);
				if (response.Status == "ok") {
					return new ShareSenderDomainResponse {
						SenderDomainId = request.SenderDomainId,
						Success = true,
						AuthorizationKey = response.Token,
					};
				}
				return new ShareSenderDomainResponse {
					SenderDomainId = request.SenderDomainId,
					Success = false,
					AuthorizationKey = null,
					ErrorInfo = new ErrorInfo {
						ErrorCode = response.Code.ToString(),
						Message = response.Message
					}
				};
			} catch (Exception ex) {
				return new ShareSenderDomainResponse {
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = "3",
						Message = ex.Message
					}
				};
			}
		}

		/// <summary>
		/// Authorizes a sender domain based on the provided request data.
		/// </summary>
		/// <param name="request">An instance of <see cref="AuthorizeSenderDomainRequest"/> that contains the domain authorization details.</param>
		/// <returns>Returns an instance of <see cref="AuthorizeSenderDomainResponse"/> that indicates the result of the authorization process.</returns>
		public AuthorizeSenderDomainResponse AuthorizeSenderDomain(AuthorizeSenderDomainRequest request) {
			if (!Guid.TryParse(request.SenderDomainId, out Guid domainId)) {
				return new AuthorizeSenderDomainResponse {
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = "1",
						Message = "SenderDomainId is not valid"
					}
				};
			}
			var senderDomain = SenderDomainRepository.GetDomainById(domainId);
			if (senderDomain == null) {
				return new AuthorizeSenderDomainResponse {
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = "2",
						Message = "SenderDomain is not found"
					}
				};
			}
			var authorizeDomainRequest = new AuthorizeDomainRequest {
				ApiKey = ServiceApi.ApiKey,
				Domain = senderDomain.Domain,
				ProviderName = senderDomain.ProviderName,
				Token = request.AuthorizationKey
			};
			try
			{
				var response = ServiceApi.AuthorizeDomain(authorizeDomainRequest);
				if (response.Status == "ok") {
					var domainWithProvider = new AddSenderDomainsInfoRequest {
						ApiKey = ServiceApi.ApiKey,
						Domain = senderDomain.Domain
					};
					var domainInfo = response.Domains.FirstOrDefault(dm => dm.Domain == senderDomain.Domain);
					SenderDomainRepository.AddDomain(domainWithProvider, domainInfo);
					return new AuthorizeSenderDomainResponse {
						SenderDomainId = request.SenderDomainId,
						Success = true
					};
				}
				return new AuthorizeSenderDomainResponse {
					SenderDomainId = request.SenderDomainId,
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = response.Code.ToString(),
						Message = response.Message
					}
				};
			} catch (Exception ex) {
				return new AuthorizeSenderDomainResponse {
					SenderDomainId = request.SenderDomainId,
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = "3",
						Message = ex.Message
					}
				};
			}
		}

		/// <summary>
		/// Deletes the sender domain based on the provided request data.
		/// </summary>
		/// <param name="request">The delete sender domain information request.</param>
		public DeleteSenderDomainResponse DeleteSenderDomain(DeleteSenderDomainRequest request) {
			var domainToDelete = SenderDomainRepository.GetDomainById(request.SenderDomainId);
			if (domainToDelete == null) {
				return new DeleteSenderDomainResponse {
					Success = false,
					ErrorInfo = new ErrorInfo {
						Message = "SenderDomain is not found"
					}
				};
			}
			bool isUnvalidated = domainToDelete.StatusId == SenderDomainStatusValues.Unvalidated ||
				domainToDelete.StatusId == SenderDomainStatusValues.Requested;
			if (!isUnvalidated) {
				return new DeleteSenderDomainResponse {
					Success = false,
					ErrorInfo = new ErrorInfo {
						Message = "Unable to delete the sender domain because its status is neither \"Unvalidated\" nor \"Requested\""
					}
				};
			}
			var deleteRequest = new DeleteSenderDomainInfoRequest {
				ApiKey = ServiceApi.ApiKey,
				Domain = domainToDelete.Domain,
				ProviderName = domainToDelete.ProviderName,
			};
			try {
				if (domainToDelete.StatusId == SenderDomainStatusValues.Unvalidated) {
					var deleteServiceResponse = ServiceApi.DeleteSenderDomain(deleteRequest);
					if (!deleteServiceResponse.IsSuccess) {
						return new DeleteSenderDomainResponse {
							Success = false,
							ErrorInfo = new ErrorInfo {
								ErrorCode = deleteServiceResponse.Code.ToString(),
								Message = deleteServiceResponse.Message
							}
						};
					}
				}
				var isDeleted = SenderDomainRepository.DeleteSenderDomain(request);
				return new DeleteSenderDomainResponse {
					Domain = deleteRequest.Domain,
					Success = isDeleted
				};
			} catch (Exception) {
				return new DeleteSenderDomainResponse {
					Success = false,
					ErrorInfo = new ErrorInfo {
						Message = "SenderDomain is not found or cannot be deleted"
					}
				};
			}
		}

		#endregion

	}

	#endregion

}

