namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Runtime.Serialization;
	using System.ServiceModel.Activation;
	using System.Web.SessionState;
	using Core.Factories;
	using Web.Common;

	#region  Class: FileImportValidationResponse

	[DataContract]
	public class FileImportValidationResponse : ConfigurationServiceResponse
	{
		#region  Properties: Public

		[DataMember]
		public bool CanUseImportEntitiesStorage { get; set; }

		#endregion
	}

	#endregion

	#region  Class: FileImportValidationService

	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class FileImportValidationService : BaseService, IFileImportValidationService, IReadOnlySessionState
	{
		#region  Fields: Private

		private IBaseFileImporter _fileImporter;

		#endregion

		#region Properties: Private

		private ConstructorArgument UserConstructorArgument =>
			new ConstructorArgument("userConnection", UserConnection);

		#endregion

		#region Properties: Protected

		/// <summary>
		/// File importer.
		/// </summary>
		protected IBaseFileImporter FileImporter => _fileImporter ?? (_fileImporter = CreateFileImporter());

		#endregion

		#region Methods: Public

		private IBaseFileImporter CreateFileImporter() {
			if (UserConnection.GetIsFeatureEnabled("UsePersistentFileImport")) {
				return ClassFactory.Get<IPersistentFileImporter>(UserConstructorArgument);
			}
			return ClassFactory.Get<IFileImporter>(UserConstructorArgument);
		}

		/// <summary>
		/// Validate imports settings.
		/// </summary>
		/// <param name="request">File import service request.</param>
		/// <returns>Operation result.</returns>
		public FileImportValidationResponse Validate(FileImportServiceRequest request) {
			var response = new FileImportValidationResponse();
			try {
				response.CanUseImportEntitiesStorage = FileImporter.Validate(request.ImportSessionId);
			} catch (Exception e) {
				response.Exception = e;
			}
			return response;
		}

		#endregion
	}

	#endregion
}

