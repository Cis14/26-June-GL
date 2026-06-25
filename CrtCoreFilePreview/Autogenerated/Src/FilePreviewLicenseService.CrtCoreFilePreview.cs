namespace Terrasoft.Configuration
{
	using System;
	using System.Runtime.Serialization;
	using System.ServiceModel;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Web;
	using System.Text;
	using System.Web.SessionState;
	using Terrasoft.Web.Common;
	using Terrasoft.Web.Common.ServiceRouting;
	using CoreSysSettings = Terrasoft.Core.Configuration.SysSettings;

	#region Class: FilePreviewLicenseResponse

	/// <summary>
	/// Response contract for file preview license service.
	/// </summary>
	[DataContract]
	public class FilePreviewLicenseResponse : ConfigurationServiceResponse
	{
		#region Constructors: Public

		/// <summary>
		/// Default constructor to create instance of FilePreviewLicenseResponse.
		/// </summary>
		public FilePreviewLicenseResponse() { }

		/// <summary>
		/// Initializes a new instance of the FilePreviewLicenseResponse class with the specified exception.
		/// </summary>
		/// <param name="e">The exception that describes the error condition for this response.</param>
		public FilePreviewLicenseResponse(Exception e)
			: base(e) { }

		/// <summary>
		/// Create instance of FilePreviewLicenseResponse with license value.
		/// </summary>
		/// <param name="value">Base64-encoded license key value.</param>
		public FilePreviewLicenseResponse(string value) {
			Value = value;
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Base64-encoded license key value.
		/// </summary>
		[DataMember(Name = "value")]
		public string Value { get; set; }

		#endregion
	}

	#endregion

	#region Class: FilePreviewLicenseService

	///<summary>Service for managing file preview license operations.</summary>
	[ServiceContract]
	[DefaultServiceRoute]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class FilePreviewLicenseService : BaseService, IReadOnlySessionState
	{
		#region Fields: Private

		private readonly string _filePreviewSettingName = "ApryseLicenseKey";

		#endregion

		#region Methods: Private

		private object GetLicenseKeyFromSettings() {
			return CoreSysSettings.GetValue(UserConnection, _filePreviewSettingName);
		}

		private bool IsLicenseKeyValid(string licenseKey) {
			return !string.IsNullOrWhiteSpace(licenseKey);
		}

		private string EncodeLicenseKeyToBase64(string licenseKey) {
			byte[] licenseKeyBytes = Encoding.UTF8.GetBytes(licenseKey);
			return Convert.ToBase64String(licenseKeyBytes);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Retrieves the Apryse license key for file preview functionality.
		/// </summary>
		/// <returns>Response containing the base64-encoded license key.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "GetFilePreviewLicense", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public FilePreviewLicenseResponse GetFilePreviewLicense() {
			try {
				var settingValue = GetLicenseKeyFromSettings();
				if (settingValue == null) {
					return new FilePreviewLicenseResponse(string.Empty);
				}

				string licenseKey = settingValue.ToString();
				if (!IsLicenseKeyValid(licenseKey)) {
					return new FilePreviewLicenseResponse(string.Empty);
				}

				string base64LicenseKey = EncodeLicenseKeyToBase64(licenseKey);
				return new FilePreviewLicenseResponse(base64LicenseKey);
			} catch (Exception e) {
				return new FilePreviewLicenseResponse(e);
			}
		}

		#endregion
	}

	#endregion
}

