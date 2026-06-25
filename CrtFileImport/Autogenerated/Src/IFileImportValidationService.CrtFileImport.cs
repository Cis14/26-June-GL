namespace Terrasoft.Configuration.FileImport
{
	using System.ServiceModel;
	using System.ServiceModel.Web;

	#region Interface: IFileImportValidationService

	[ServiceContract(Name = "FileImportValidationService")]
	public interface IFileImportValidationService 
	{
		#region Methods: Public

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
				ResponseFormat = WebMessageFormat.Json)]
		FileImportValidationResponse Validate(FileImportServiceRequest request);

		#endregion
	}

	#endregion
}
