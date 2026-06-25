namespace CrtCloudIntegration.Services
{
	using System;
	using System.Net;
	using System.ServiceModel;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Web;
	using Common.Logging;
	using CrtCloudIntegration.Utilities;
	using CrtCloudIntegration.Utilities.Errors;
	using Terrasoft.Configuration;
	using Terrasoft.Core.Factories;
	using Terrasoft.Web.Common;

	#region Class: AuthServiceCallbackHandler

	/// <summary>
	/// Provides logic which handles callback requests.
	/// </summary>
	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class AuthServiceCallbackHandler : BaseService
	{

		#region Enum: Private

		private enum PageType
		{
			ErrorPage,
			SuccessPage		}

		#endregion

		#region Fields: Private

		private static readonly ILog Logger = LogManager.GetLogger("AuthServiceCallbackHandler");

		#endregion

		#region Methods: Private

		private void GetHtmlPage(PageType page) {
			var httpContext = HttpContextAccessor.GetInstance();
			var successPage = LocalizableStringHelper.GetValue(UserConnection, "AuthServiceCallbackHandler", "SuccessPage");
			var errorPage = LocalizableStringHelper.GetValue(UserConnection, "AuthServiceCallbackHandler", "ErrorPage");
			var htmlPage = page == PageType.SuccessPage ? successPage : errorPage;
			const string contentType = "text/html; charset=utf-8";

#if NETFRAMEWORK
			if (WebOperationContext.Current != null) {
				WebOperationContext.Current.OutgoingResponse.ContentType = contentType;
			}
#else
			httpContext.Response.ContentType = contentType;
#endif
			httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
			httpContext.Response.Write(htmlPage);
		}

		private void ReportGetStartFlowUrlError() {
			var notifier = ClassFactory.Get<IUINotifier>(CrtCloudIntegrationConsts.UINotifierSenderName);
			notifier.ReportError(new PlatformServicesUnavailable());
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Handles callback requests.
		/// A callback request is a request from sm-account to the application, when the OAuth flow ends
		/// </summary>
		/// <param name="platform">Platform name (e.g. facebook, google) </param>
		/// <param name="platformUserId">Id of the user on a remote platform</param>
		/// <param name="application">name of the application (e.g. digital_ads)</param>
		[OperationContract]
		[WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
			ResponseFormat = WebMessageFormat.Json)]
		public void Handle(string platform, string platformUserId, string application) {
			try {
				var worker = ClassFactory.Get<IAuthServiceCallbackHandlerWorker>(application);
				worker.Handle(platform, platformUserId, application);
				GetHtmlPage(PageType.SuccessPage);
			} catch (Exception e) {
				GetHtmlPage(PageType.ErrorPage);
				Logger.Error(e.Message);
				ReportGetStartFlowUrlError();
			}
		}

		/// <summary>
		/// Handles errors
		/// </summary>
		/// <param name="errorCode"></param>
		/// <param name="errorDescription"></param>
		[OperationContract]
		[WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
			ResponseFormat = WebMessageFormat.Json)]
		public void HandleError(string errorCode, string errorDescription) {
			var notifier = ClassFactory.Get<IUINotifier>(CrtCloudIntegrationConsts.UINotifierSenderName);
			if (errorCode == "403.1") {
				notifier.ReportError(new AccessTokenDoesNotHaveRequiredScopes());
				GetHtmlPage(PageType.ErrorPage);
				return;
			}
			if (errorCode == "callbackerror") {
				Logger.Error($"Callback error. Error code: {errorCode}, error description: {errorDescription}");
				GetHtmlPage(PageType.ErrorPage);
				return;
			}
			if (errorCode.Length > errorDescription.Length) {
				var error = new GenericErrorOne(errorDescription, errorCode);
				notifier.ReportError(error);
			} else {
				var error = new GenericErrorOne(errorCode, errorDescription);
				notifier.ReportError(error);
			}
			Logger.Error($"Error code: {errorCode}, error description: {errorDescription}");
			GetHtmlPage(PageType.ErrorPage);
		}

		#endregion

	}

	#endregion

}
