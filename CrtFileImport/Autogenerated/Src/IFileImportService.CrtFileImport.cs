namespace Terrasoft.Configuration.FileImport
{
	using System.ServiceModel;
	using System.ServiceModel.Web;

	#region  Interface: IFileImportService

	[ServiceContract(Name = "FileImportService")]
	public interface IFileImportService
	{
		#region Methods: Public

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
				ResponseFormat = WebMessageFormat.Json)]
		ConfigurationServiceResponse SaveFile();

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
				ResponseFormat = WebMessageFormat.Json)]
		ImportSessionInfoResponse GetImportSessionInfo(FileImportServiceRequest request);

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
				ResponseFormat = WebMessageFormat.Json)]
		ConfigurationServiceResponse SetImportObject(FileImportServiceRequest request);

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
				ResponseFormat = WebMessageFormat.Json)]
		ConfigurationServiceResponse SetFileInfo(FileImportServiceRequest request);

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
				ResponseFormat = WebMessageFormat.Json)]
		ConfigurationServiceResponse Import(FileImportServiceRequest request);

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
				ResponseFormat = WebMessageFormat.Json)]
		ColumnsMappingResponse GetColumnsMappingParameters(FileImportServiceRequest request);

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
				ResponseFormat = WebMessageFormat.Json)]
		void SetColumnsMappingParameters(FileImportServiceRequest request);

		#endregion
	}

	#endregion
}

