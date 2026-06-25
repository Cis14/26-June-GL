namespace Terrasoft.Configuration.FileImport {
	using Core.Factories;
	using Core.Scheduler;
	using Quartz;
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using System.ServiceModel.Activation;
	using System.Web.SessionState;
	using Terrasoft.Common;
	using Web.Common;
	using global::Common.Logging;

	#region  Class: ColumnsMappingResponse

	[DataContract]
	public class ColumnsMappingResponse : ConfigurationServiceResponse {

		#region  Fields: Public

		[DataMember]
		public IEnumerable<ImportColumn> Columns;

		[DataMember]
		public string RootSchemaName;

		#endregion

	}

	#endregion

	#region  Class: FileImportService

	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class FileImportService : BaseService, IFileImportService, IReadOnlySessionState {

		#region Constants: Private

		private const string FileImportProcessName = "FileImportProcess";

		#endregion

		#region  Fields: Private

		private IBaseFileImporter _fileImporter;
		private IImportParametersRepository _importParametersRepository;
		private IFileImporterFactory _fileImporterFactory;

		#endregion

		#region Properties: Private

		private ConstructorArgument UserConstructorArgument =>
			new ConstructorArgument("userConnection", UserConnection);

		private IImportParametersRepository ImportParametersRepository => _importParametersRepository ??
			(_importParametersRepository = ClassFactory.Get<IImportParametersRepository>(UserConstructorArgument));

		private IFileImporterFactory FileImporterFactory => _fileImporterFactory ?? (_fileImporterFactory = ClassFactory.Get<IFileImporterFactory>());

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="FileImportService" />.
		/// </summary>
		public FileImportService() { }

		/// <summary>
		/// Creates instance of type <see cref="FileImportService" />.
		/// </summary>
		/// <param name="fileImporter">File importer.</param>
		public FileImportService(FileImporter fileImporter) => _fileImporter = fileImporter;

		#endregion

		#region Properties: Protected

		/// <summary>
		/// File importer.
		/// </summary>
		protected IBaseFileImporter FileImporter => _fileImporter ?? (_fileImporter = FileImporterFactory.GetFileImporter(UserConnection));

		#endregion

		#region Methods: Private

		/// <summary>
		/// Starts file import process.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		private void StartFileImportProcess(Guid importSessionId) {
			var parameters = new Dictionary<string, object> {
				{ "ImportSessionId", importSessionId }
			};
			JobOptions jobOptions;
			if (!UserConnection.GetIsFeatureEnabled("UseDefaultImportJobOptions")) {
				jobOptions = new JobOptions {
					RequestsRecovery = false
				};
			} else {
				jobOptions = JobOptions.Default;
			}
			UserConnection.RunProcess(FileImportProcessName, MisfireInstruction.SimpleTrigger.FireNow, parameters,
				jobOptions);
			WriteAddedFileImportJobLog(importSessionId);

		}

		private void WriteAddedFileImportJobLog(Guid importSessionId) {
			var logger = LogManager.GetLogger("FileImportAppender");
			var message =
				FileImportLogMessageExtensions.CreateFileImportLogMessage(FileImportLogMessageType.PlanedStartFileImportJob, importSessionId);
			logger.Info(message);
		}

		private void SaveImportParameters(ImportParameters parameters) {
			ImportParametersRepository.Update(parameters);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Gets columns mapping parameters.
		/// </summary>
		/// <param name="request">File import service request.</param>
		/// <returns>Columns mapping parameters.</returns>
		public ColumnsMappingResponse GetColumnsMappingParameters(FileImportServiceRequest request) {
			request.CheckArgumentNull(nameof(request));
			return FileImporter.GetColumnsMappingParameters(request.ImportSessionId);
		}

		/// <summary>
		/// Gets import session information.
		/// </summary>
		/// <returns>Import session information.</returns>
		public ImportSessionInfoResponse GetImportSessionInfo(FileImportServiceRequest request) {
			request.CheckArgumentNull(nameof(request));
			var response = new ImportSessionInfoResponse();
			try {
				var importSessionId = request.ImportSessionId;
				var parameters = FileImporter.FindImportParameters(importSessionId);
				if (parameters != null && parameters.RootSchemaUId != Guid.Empty) {
					var schema = UserConnection.EntitySchemaManager.GetInstanceByUId(parameters.RootSchemaUId);
					response.SetImportSessionInfo(parameters, schema.Name);
				}
			} catch (Exception e) {
				response.Exception = e;
			}
			return response;
		}

		/// <summary>
		/// Starts import of the file with the specified identifier.
		/// </summary>
		/// <param name="request"></param>
		public ConfigurationServiceResponse Import(FileImportServiceRequest request) {
			request.CheckArgumentNull(nameof(request));
			var response = new ConfigurationServiceResponse();
			try {
				UserConnection.DBSecurityEngine.CheckCanExecuteOperation("CanImportFromExcel");
				var parameters = FileImporter.FindImportParameters(request.ImportSessionId);
				parameters.NeedSendNotify = true;
				if (UserConnection.GetIsFeatureEnabled("UsePersistentFileImport")) {
					SaveImportParameters(parameters);
				} else {
					parameters.Save();
				}
				StartFileImportProcess(request.ImportSessionId);
			} catch (Exception e) {
				response.Exception = e;
			}
			return response;
		}

		/// <summary>
		/// Processes file and saves its information to cache.
		/// </summary>
		/// <returns>Operation result.</returns>
		public ConfigurationServiceResponse SaveFile() {
			var response = new ConfigurationServiceResponse();
			try {
				FileImporter.SaveFile();
			} catch (Exception e) {
				response.Exception = e;
			}
			return response;
		}

		/// <summary>
		/// Sets columns mapping parameters.
		/// </summary>
		/// <param name="request">File import service request.</param>
		public void SetColumnsMappingParameters(FileImportServiceRequest request) {
			request.CheckArgumentNull(nameof(request));
			FileImporter.SetColumnsMappingParameters(request.ImportSessionId, request.Columns);
		}

		/// <summary>
		/// Sets file information.
		/// </summary>
		/// <param name="request">File import service request.</param>
		/// <returns>Operation result.</returns>
		public ConfigurationServiceResponse SetFileInfo(FileImportServiceRequest request) {
			request.CheckArgumentNull(nameof(request));
			var response = new ConfigurationServiceResponse();
			try {
				FileImporter.SetFileInfo(request.ImportSessionId, request.FileName);
			} catch (Exception e) {
				response.Exception = e;
			}
			return response;
		}

		/// <summary>
		/// Sets import object.
		/// </summary>
		/// <param name="request">File import service request.</param>
		/// <returns>Operation result.</returns>
		public ConfigurationServiceResponse SetImportObject(FileImportServiceRequest request) {
			request.CheckArgumentNull(nameof(request));
			var response = new ConfigurationServiceResponse();
			try {
				FileImporter.SetImportObject(request.ImportSessionId, request.ImportObject);
			} catch (Exception e) {
				response.Exception = e;
			}
			return response;
		}

		#endregion

	}

	#endregion

	#region  Class: FileImportServiceRequest

	[DataContract]
	public class FileImportServiceRequest {

		#region  Fields: Public

		[DataMember(Name = "columns")]
		public List<ImportColumn> Columns;

		[DataMember(Name = "fileName")]
		public string FileName;

		[DataMember(Name = "importObject")]
		public ImportObject ImportObject;

		[DataMember(Name = "importSessionId")]
		public Guid ImportSessionId;

		#endregion

	}

	#endregion

	#region  Class: ImportSessionInfoResponse

	[DataContract]
	public class ImportSessionInfoResponse : ConfigurationServiceResponse {

		#region  Fields: Public

		[DataMember(Name = "fileName")]
		public string FileName;

		[DataMember(Name = "importedRowsCount")]
		public uint ImportedRowsCount;

		[DataMember(Name = "importObject")]
		public ImportObject ImportObject;

		[DataMember(Name = "importTags")]
		public List<ImportTag> ImportTags;

		[DataMember(Name = "notImportedRowsCount")]
		public uint NotImportedRowsCount;

		[DataMember(Name = "processedRowsCount")]
		public uint ProcessedRowsCount;

		[DataMember(Name = "rootSchemaName")]
		public string RootSchemaName;

		[DataMember(Name = "totalRowsCount")]
		public uint TotalRowsCount;

		[DataMember(Name = "hasLookupProcessingErrors")]
		public bool HasLookupProcessingErrors;

		#endregion

		#region Methods: Public

		/// <summary>
		/// Sets import session information.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		public void SetImportSessionInfo(ImportParameters parameters) {
			FileName = parameters.FileName;
			ImportObject = parameters.ImportObject;
			TotalRowsCount = parameters.TotalRowsCount;
			ProcessedRowsCount = parameters.ProcessedRowsCount;
			ImportedRowsCount = parameters.ImportedRowsCount;
			NotImportedRowsCount = parameters.TotalRowsCount - parameters.ImportedRowsCount;
			ImportTags = parameters.ImportTags;
			HasLookupProcessingErrors = parameters.HasLookupProcessingErrors;
		}

		/// <summary>
		/// Sets import session information.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		/// <param name="schemaName">Schema name.</param>
		public void SetImportSessionInfo(ImportParameters parameters, string schemaName) {
			SetImportSessionInfo(parameters);
			RootSchemaName = schemaName;
		}

		#endregion

	}

	#endregion

}

