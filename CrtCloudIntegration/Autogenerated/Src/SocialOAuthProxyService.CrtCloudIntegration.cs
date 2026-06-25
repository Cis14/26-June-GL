namespace CrtCloudIntegration.Services
{
	using System;
	using System.Linq;
	using System.ServiceModel;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Web;
	using Common.Logging;
	using CrtCloudIntegration.Models.Requests;
	using CrtCloudIntegration.Utilities;
	using CrtCloudIntegration.Utilities.Errors;
	using Terrasoft.Configuration.BpmonlineCloudIntegration;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Web.Common;

	#region Class: SocialOAuthProxyService

	/// <summary>
	/// Provides logic to authenticate to social services.
	/// </summary>
	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class SocialOAuthProxyService: BaseService
	{

		#region Fields: Private

		private ILog _logger;
		private SocialOAuthHandlerFactory _authHandlerFactory;
		private readonly SysSettingsValidator _sysSettingsValidator;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="SocialOAuthProxyService"/> class.
		/// </summary>
		public SocialOAuthProxyService() {
			_sysSettingsValidator = new SysSettingsValidator(Logger);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SocialOAuthProxyService"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		public SocialOAuthProxyService(UserConnection userConnection)
			: base(userConnection) {
			_sysSettingsValidator = new SysSettingsValidator(Logger);
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Gets or sets the logger.
		/// </summary>
		/// <value>
		/// The logger.
		/// </value>
		public ILog Logger {
			get => _logger ?? LogManager.GetLogger("AuthProxyService");
			set => _logger = value;
		}

		/// <summary>
		/// Factory that provides authentication handlers for different social platforms.
		/// </summary>
		public SocialOAuthHandlerFactory OAuthHandlerFactory {
			get =>
				_authHandlerFactory ??
				(_authHandlerFactory = new SocialOAuthHandlerFactory());
			set => _authHandlerFactory = value;
		}

		#endregion

		#region Methods: Private

		private void ReportGetStartFlowUrlError() {
			var notifier = ClassFactory.Get<IUINotifier>(CrtCloudIntegrationConsts.UINotifierSenderName);
			notifier.ReportError(new PlatformServicesUnavailable());
		}

		private void ReportOAuthError(RequestTokenException e) {
			Logger.Error("Error while trying to handle OAuth request", e);
			var notifier = ClassFactory.Get<IUINotifier>(CrtCloudIntegrationConsts.UINotifierSenderName);
			notifier.ReportError(new CouldNotAuthenticateToPlatformError());
		}

		private bool ValidateSysSettings(string websocketSessionId) {
			var errors = _sysSettingsValidator.Validate(UserConnection);
			if (!errors.Any()) {
				return true;
			}
			var notifier = ClassFactory.Get<IUINotifier>(CrtCloudIntegrationConsts.UINotifierSenderName);
			notifier.ReportError(errors.First(), websocketSessionId: websocketSessionId);
			return false;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Authenticates Creatio in one of social platforms.
		/// </summary>
		/// <param name="request">DTO which contains information about an application to authenticate with.</param>
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
			ResponseFormat = WebMessageFormat.Json)]
		public string AuthenticateToPlatform(BaseRequest request) {
			try {
				if (!ValidateSysSettings(request.WebsocketSessionId)) {
					return string.Empty;
				}

				var handler = OAuthHandlerFactory.CreateHandler(request.Application, request.Platform);
				var startFlowRequest = new StartFlowRequest {
					Application = request.Application,
					Platform = request.Platform,
					CreatioUserId = UserConnection.CurrentUser.Id
				};
				var startFlowUrl = handler.StartFlow(startFlowRequest);
				if (!string.IsNullOrEmpty(startFlowUrl)) {
					return startFlowUrl;
				}
				Logger.Warn("Something went wrong while trying get start flow url");
				ReportGetStartFlowUrlError();
				return string.Empty;
			} catch (RequestTokenException e) {
				ReportOAuthError(e);
				return string.Empty;
			} catch (Exception e) {
				Logger.Error("Error while trying to get start flow url", e);
				ReportGetStartFlowUrlError();
				return string.Empty;
			}
		}

		#endregion

	}

	#endregion

}
