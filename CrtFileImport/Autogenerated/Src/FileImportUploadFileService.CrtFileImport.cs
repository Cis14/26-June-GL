namespace Terrasoft.Configuration.FileImport {
	using System;
	using System.IO;
	using System.ServiceModel;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Web;
	using System.Web.SessionState;
#if NETSTANDARD2_0 // TODO CRM-46497
	using TS = Terrasoft.Web.Http.Abstractions;
#else
	using MS = System.Web;
#endif
	using Terrasoft.Configuration.FileUpload;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Web.Common;
	using Terrasoft.Web.Common.ServiceRouting;
	using TSConfiguration = Terrasoft.Core.Configuration;

	#region Class: FileImportUploadFileService

	/// <summary>
	/// Represents end-point for upload file for import.
	/// </summary>
	[DefaultServiceRoute]
	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class FileImportUploadFileService : BaseService, IReadOnlySessionState {

		#region Fields: Private

		private readonly IFileUploadInfo _fileUploadInfo;
		private FileUploader _fileUploader;
		private IPersistentFileImporter _fileImporter;

		#endregion

		#region Constructors: Public

		public FileImportUploadFileService() { }

		public FileImportUploadFileService(UserConnection userConnection, FileUploader fileUploader,
			IFileUploadInfo fileUploadInfo)
			: base(userConnection) {
			_fileUploader = fileUploader;
			_fileUploadInfo = fileUploadInfo;
		}

		#endregion

		#region Properties: Private
		
		private ConstructorArgument UserConnectionArg => new ConstructorArgument("userConnection", UserConnection);

		private FileUploader FileUploader =>
			_fileUploader ?? (_fileUploader = ClassFactory.Get<FileUploader>(UserConnectionArg));

		private IPersistentFileImporter PersistentFileImporter => 
			_fileImporter ?? (_fileImporter =  ClassFactory.Get<IPersistentFileImporter>(UserConnectionArg));
		
		#endregion

		#region Methods: Private

		private IFileUploadInfo GetFileUploadInfo(Stream fileContent) {
			if (_fileUploadInfo != null) {
				return _fileUploadInfo;
			}

			return ClassFactory.Get<FileUploadInfo>(
				new ConstructorArgument("fileContent", fileContent),
#if NETSTANDARD2_0
				new ConstructorArgument("request", TS.HttpContext.Current.Request),
#else
				new ConstructorArgument("request",
					new MS.HttpRequestWrapper(MS.HttpContext.Current.Request)),
#endif
				new ConstructorArgument("storage", UserConnection.Workspace.ResourceStorage));
		}

		private decimal GetMaxFileSizeFromSysSettings() {
			int value = TSConfiguration.SysSettings.GetValue(UserConnection, "FileImportMaxFileSize", 0);
			return FileSizeConverter.MbToBytes(value);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Save file/file parts to db.
		/// </summary>
		/// <param name="fileContent">File content <see cref="Stream"/></param>
		/// <returns></returns>
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
			ResponseFormat = WebMessageFormat.Json)]
		public ConfigurationServiceResponse SaveFile(Stream fileContent) {
			IFileUploadInfo fileUploadInfo = GetFileUploadInfo(fileContent);
			decimal maxFileSize = GetMaxFileSizeFromSysSettings();
			var uploadSettings = new FileUploadConfig(fileUploadInfo) {
				SetCustomColumnsFromConfig = false,
				MaxFileSize = maxFileSize
			};
			var response = new ConfigurationServiceResponse();
			try {
				FileUploader.UploadFile(uploadSettings);
			} catch (MaxFileSizeExceededException ex) {
				response.Exception = ex;
				response.Success = false;
			}
			return response;
		}

		/// <summary>
		/// Delete file from import parameters.
		/// </summary>
		/// <param name="importSessionId">Import session id.</param>
		/// <returns></returns>
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
			ResponseFormat = WebMessageFormat.Json)]
		public ConfigurationServiceResponse DeleteFile(Guid importSessionId) {
			var response = new ConfigurationServiceResponse();
			try {
				PersistentFileImporter.DeleteFile(importSessionId);
			} catch (ItemNotFoundException ex) {
				response.Exception = ex;
				response.Success = false;
			}
			return response;
		}

		/// <summary>
		/// Validate uploaded file. 
		/// </summary>
		/// <param name="importSessionId">Import session id.</param>
		/// <returns></returns>
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
			ResponseFormat = WebMessageFormat.Json)]
		public ConfigurationServiceResponse CheckIsFileValid(Guid importSessionId) {
			var response = new ConfigurationServiceResponse();
			try {
				PersistentFileImporter.CheckIsFileValid(importSessionId);
			} catch (Exception ex) {
				response.Exception = ex;
				response.Success = false;
			}
			return response;
		}
		#endregion

	}

	#endregion

}

