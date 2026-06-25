namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using System.ServiceModel;
	using System.ServiceModel.Web;
	using System.ServiceModel.Activation;
	using System.Web.SessionState;
	using Terrasoft.Core;
	using Terrasoft.Configuration.CESModels;
	using Terrasoft.Configuration.CESResponses;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.ServiceModelContract;
	using Terrasoft.Web.Common;

	#region Class: BpmonlineCloudService

	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class BpmonlineCloudService : BaseService, IReadOnlySessionState
	{

		#region Fields: Private

		private BpmonlineCloudEngine _cloudEngine;

		private UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		public BpmonlineCloudService() { }

		public BpmonlineCloudService(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Properties: Private

		private BpmonlineCloudEngine CloudEngine {
			get {
				if (_cloudEngine == null) {
					_cloudEngine = ClassFactory.Get<BpmonlineCloudEngine>(
						new ConstructorArgument("userConnection", UserConnection));
				}
				return _cloudEngine;
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Returns account info.
		/// </summary>
		/// <returns>Account info.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "AccountInfo", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = "AccountInfo")]
		public AccountInfo AccountInfo() {
			return CloudEngine.AccountInfo();
		}

		/// <summary>
		/// Adds sender domains info.
		/// </summary>
		/// <param name="domain">Domain.</param>
		/// <returns>Sender domains info.</returns>
		[Obsolete("Use " + nameof(AddSenderDomainV2) + " instead.")]
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = "DomainsInfo")]
		public CloudSenderDomainsInfo AddSenderDomain(string domain) {
			return CloudEngine.AddSenderDomain(domain);
		}

		/// <summary>
		/// Adds sender domains info for the provider.
		/// </summary>
		/// <param name="addSenderDomainsInfoRequest">The add sender domains information request.</param>
		/// <returns>Sender domains info.</returns>
		[Obsolete("Use " + nameof(AddSenderDomainV2) + " instead.")]
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "AddSenderDomainForProvider", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = "DomainsInfo")]
		public CloudSenderDomainsInfo AddSenderDomainForProvider(AddSenderDomainsInfoRequest addSenderDomainsInfoRequest) {
			return CloudEngine.AddSenderDomain(addSenderDomainsInfoRequest);
		}
		
		/// <summary>
		/// Adds a sender domain and returns the corresponding domain information.
		/// </summary>
		/// <param name="request">The request containing the domain to be added.</param>
		/// <returns>Information about the added sender domain.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = nameof(AddSenderDomainV2))]
		public AddSenderDomainV2Response AddSenderDomainV2(AddSenderDomainV2Request request) {
			return CloudEngine.AddSenderDomainV2(request);
		}

		/// <summary>
		/// Validates sender domain.
		/// </summary>
		/// <param name="request">Request parameters</param>
		/// <returns>Sender domain validation result.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = "ValidateSenderDomain")]
		public ValidateDomainResponse ValidateSenderDomain(ValidateDomainRequest request) {
			return CloudEngine.ValidateSenderDomain(request.DomainId);
		}

		/// <summary>
		/// Gets checked emails from provider.
		/// </summary>
		/// <param name="emails">Enumerable of emails.</param>
		/// <returns>Sender domains info.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = "GetCheckedEmails")]
		public CheckedEmailResponse GetCheckedEmails(IEnumerable<string> emails) {
			return CloudEngine.GetCheckedEmails(emails);
		}

		/// <summary>
		/// Returns sender domains info.
		/// </summary>
		/// <returns>Sender domains info.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "SenderDomainsInfo", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = "DomainsInfo")]
		public CloudSenderDomainsInfo SenderDomainsInfo() {
			return CloudEngine.SenderDomainsInfo();
		}

		/// <summary>
		/// Returns sender domains info by the provider name.
		/// </summary>
		/// <param name="senderDomainsInfoRequest">The sender domains information request.</param>
		/// <returns>Sender domains info.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "RefreshSenderDomains", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = "DomainsInfo")]
		public CloudSenderDomainsInfo RefreshSenderDomains(SenderDomainsInfoRequest senderDomainsInfoRequest) {
			return CloudEngine.RefreshSenderDomains(senderDomainsInfoRequest);
		}

		/// <summary>
		/// Returns sender domains info by the provider name.
		/// </summary>
		/// <param name="senderDomainsInfoRequest">The sender domains information request.</param>
		/// <returns>Sender domains info.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "GetSenderDomainsInfoByProvider", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = "DomainsInfo")]
		public CloudSenderDomainsInfo GetSenderDomainsInfoByProvider(SenderDomainsInfoRequest senderDomainsInfoRequest) {
			return CloudEngine.GetSenderDomainsInfo(senderDomainsInfoRequest);
		}

		/// <summary>
		/// Updates user settings.
		/// </summary>
		/// <param name="webHookAppDomain">WebHooks app domain.</param>
		/// <param name="authKey"></param>
		/// <returns>Account info.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "UpdateUserSettings", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = "AccountInfo")]
		public AccountInfo UpdateUserSettings(string webHookAppDomain, string authKey) {
			return CloudEngine.UpdateUserSettings(webHookAppDomain, authKey);
		}

		/// <summary>
		/// Gets <see cref="SenderValidationInfo"/> with separate emails enums.
		/// </summary>
		/// <param name="emails">Enumerable of emails.</param>
		/// <returns>Sender domains info.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = "ValidateSender")]
		public SenderValidationInfo ValidateSender(IEnumerable<string> emails) {
			return CloudEngine.ValidateSender(emails);
		}

		/// <summary>
		/// Deletes a sender domain.
		/// </summary>
		/// <param name="request">The details of the sender domain to delete.</param>
		/// <returns>The response indicating the result of the delete operation.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = nameof(DeleteSenderDomain))]
		public DeleteSenderDomainResponse DeleteSenderDomain(DeleteSenderDomainRequest request) {
			return CloudEngine.DeleteSenderDomain(request);
		}

		/// <summary>
		/// Shares a sender domain.
		/// </summary>
		/// <param name="request">The details of the sender domain to share.</param>
		/// <returns>The response containing the result of the share operation.</returns
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = nameof(ShareSenderDomain))]
		public ShareSenderDomainResponse ShareSenderDomain(ShareSenderDomainRequest request) {
			return CloudEngine.ShareSenderDomain(request);
		}

		/// <summary>
		/// Authorizes a sender domain based on the provided request.
		/// </summary>
		/// <param name="request">The request containing the sender domain authorization details.</param>
		/// <returns>The response indicating the result of the sender domain authorization process.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = nameof(AuthorizeSenderDomain))]
		public AuthorizeSenderDomainResponse AuthorizeSenderDomain(AuthorizeSenderDomainRequest request) {
			return CloudEngine.AuthorizeSenderDomain(request);
		}

		#endregion

	}

	#endregion
	
	#region Class: AddSenderDomainV2Request

	/// <summary>
	/// Represents a request to add sender domain information for a cloud service.
	/// This class is used to pass the required data, such as the domain name,
	/// for adding a sender domain within the context of the service.
	/// </summary>
	[DataContract, Serializable]
	public class AddSenderDomainV2Request
	{
		/// <summary>
		/// Gets or sets the domain name associated with the sender domain.
		/// This property is used to specify or retrieve the domain information
		/// required for operations involving the sender domain within the cloud service.
		/// </summary>
		[DataMember(Name = "domain")]
		public string Domain { get; set; }
	}
	
	#endregion
	
	#region Class: AddSenderDomainV2Response

	/// <summary>
	/// Represents the response received after attempting to add sender domain information in the cloud service.
	/// This class encapsulates the details about the sender domains and related cloud service data returned by the operation.
	/// </summary>
	[DataContract, Serializable]
	public class AddSenderDomainV2Response : BaseResponse
	{
		/// <summary>
		/// Gets or sets the unique identifier of the sender domain.
		/// This property is used to distinguish and reference specific sender domains within the cloud service.
		/// </summary>
		[DataMember(Name = "senderDomainId")]
		public string SenderDomainId { get; set; }
	}
	
	#endregion
	
	#region Class: AuthorizeSenderDomainRequest

	/// <summary>
	/// Represents a request to authorize a sender domain within the cloud service.
	/// This class contains the necessary information required for authorizing a sender domain,
	/// such as the unique identifier of the sender domain and the corresponding authorization key.
	/// </summary>
	[DataContract, Serializable]
	public class AuthorizeSenderDomainRequest
	{
		/// <summary>
		/// Gets or sets the unique identifier of the sender domain.
		/// This property is used to distinguish and reference specific sender domains within the cloud service.
		/// </summary>
		[DataMember(Name = "senderDomainId")]
		public string SenderDomainId { get; set; }
		
		/// <summary>
		/// Gets or sets the authorization key associated with the sender domain operation.
		/// The key is used to verify and authenticate the request for domain sharing.
		/// </summary>
		[DataMember(Name = "authorizationKey")]
		public string AuthorizationKey { get; set; }
	}

	#endregion
	
	#region Class: AuthorizeSenderDomainResponse

	/// <summary>
	/// Represents the response received after authorizing a sender domain in the cloud service.
	/// This class encapsulates the status and any relevant details of the authorization process.
	/// </summary>
	[DataContract, Serializable]
	public class AuthorizeSenderDomainResponse : BaseResponse
	{
		/// <summary>
		/// Gets or sets the unique identifier of the sender domain.
		/// This property is used to distinguish and reference specific sender domains within the cloud service.
		/// </summary>
		[DataMember(Name = "senderDomainId")]
		public string SenderDomainId { get; set; }
	}

	#endregion

	#region Class: ShareSenderDomainRequest

	/// <summary>
	/// Represents a request to share a sender domain within the cloud service.
	/// This class encapsulates the required data necessary for sharing access to a sender domain,
	/// including the unique identifier of the sender domain.
	/// </summary>
	[DataContract, Serializable]
	public class ShareSenderDomainRequest
	{
		/// <summary>
		/// Gets or sets the unique identifier of the sender domain.
		/// This property is used to distinguish and reference specific sender domains within the cloud service.
		/// </summary>
		[DataMember(Name = "senderDomainId")]
		public string SenderDomainId { get; set; }
	}

	#endregion
	
	#region Class: ShareSenderDomainResponse

	/// <summary>
	/// Represents the response for sharing a sender domain within the cloud service.
	/// This class includes information such as the unique identifier of the sender domain
	/// and the associated authorization key required for the operation.
	/// </summary>
	[DataContract, Serializable]
	public class ShareSenderDomainResponse : BaseResponse
	{
		/// <summary>
		/// Gets or sets the unique identifier of the sender domain.
		/// This property is used to distinguish and reference specific sender domains within the cloud service.
		/// </summary>
		[DataMember(Name = "senderDomainId")]
		public string SenderDomainId { get; set; }

		/// <summary>
		/// Gets or sets the authorization key associated with the sender domain operation.
		/// The key is used to verify and authenticate the request for domain sharing.
		/// </summary>
		[DataMember(Name = "authorizationKey")]
		public string AuthorizationKey { get; set; }
	}

	#endregion

	#region Class: DeleteSenderDomainRequest

	/// <summary>
	/// Represents a request to delete a sender domain within the cloud service.
	/// This class contains the necessary information required for deleting a sender domain,
	/// such as the unique identifier of the sender domain and the corresponding authorization key.
	/// </summary>
	[DataContract, Serializable]
	public class DeleteSenderDomainRequest {
		/// <summary>
		/// Gets or sets the unique identifier of the sender domain.
		/// This property is used to distinguish and reference specific sender domains within the cloud service.
		/// </summary>
		[DataMember(Name = "senderDomainId")]
		public Guid SenderDomainId { get; set; }
	}

	#endregion

	#region Class: DeleteSenderDomainResponse

	/// <summary>
	/// Represents a request to delete a sender domain within the cloud service.
	/// This class contains the necessary information required for deleting a sender domain,
	/// such as the unique identifier of the sender domain and the corresponding authorization key.
	/// </summary>
	[DataContract, Serializable]
	public class DeleteSenderDomainResponse : BaseResponse {
		/// <summary>
		/// Gets or sets the unique identifier of the sender domain.
		/// This property is used to distinguish and reference specific sender domains within the cloud service.
		/// </summary>
		[DataMember(Name = "Domain")]
		public string Domain { get; set; }
	}

	#endregion
}

