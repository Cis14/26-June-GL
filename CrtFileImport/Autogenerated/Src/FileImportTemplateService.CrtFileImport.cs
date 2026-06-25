namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using System.ServiceModel.Activation;
	using Terrasoft.Common;
	using Web.Common;
	using Terrasoft.Core.Store;
	using System.Linq;
	using Terrasoft.Core.DB;
	using Newtonsoft.Json;
	using System.ServiceModel;
	using System.ServiceModel.Web;
	using System.Text;
	using Terrasoft.Core.Factories;
	using Terrasoft.Web.Common.ServiceRouting;

	#region  Class: FileImportTemplateService

	[ServiceContract]
	[DefaultServiceRoute]
	[SspServiceRoute]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class FileImportTemplateService : BaseService
	{

		#region  Fields: Private

		private IFileImporterFactory _fileImporterFactory;

		private IBaseFileImporter _fileImporter;

		private readonly string _importTemplateSchemaName = "FileImportTemplate";

		#endregion

		#region Properties: Protected

		private IFileImporterFactory FileImporterFactory => _fileImporterFactory ?? (_fileImporterFactory = ClassFactory.Get<IFileImporterFactory>());

		private IBaseFileImporter FileImporter => _fileImporter ?? (_fileImporter = FileImporterFactory.GetFileImporter(UserConnection));

		#endregion

		#region Methods: Private

		/// <summary>
		/// Validate request parameters
		/// </summary>
		/// <param name="request">File import template service request</param>
		/// <param name="isNeedValidateTemplateId">is need validate template id by empty guid</param>
		private void ValidateRequest(FileImportTemplateServiceRequest request, bool isNeedValidateTemplateId = true) {
			request.CheckArgumentNull(nameof(request));
			if (request.ImportSessionId == Guid.Empty) {
				throw new ArgumentNullException(nameof(request.ImportSessionId));
			}
			if (request.ImportTemplateId == Guid.Empty && isNeedValidateTemplateId) {
				throw new ArgumentNullException(nameof(request.ImportTemplateId));
			}
		}

		/// <summary>
		/// Apply template to import column mapping and save it to import column mapping parameters
		/// </summary>
		/// <param name="importSessionId">Import session id</param>
		/// <param name="importTemplateId">Import template id</param>
		private void InnerApplyImportTemplate(Guid importSessionId, Guid importTemplateId) {
			if (importTemplateId == GetAppliedImportTemplateIdFromCache(importSessionId)) {
				return;
			}
			if (importTemplateId == Guid.Empty) {
				FileImporter.RefreshColumnMapping(importSessionId);
			} else {
				var templateColumns = GetTemplateColumnMapping(importTemplateId);
				var columnMappingParameters = FileImporter.GetColumnsMappingParameters(importSessionId);
				var appliedColumns = GetAppliedColumns(columnMappingParameters.Columns, templateColumns);
				FileImporter.SetColumnsMappingParameters(importSessionId, appliedColumns.ToList());	
			}
			SetAppliedImportTemplateIdToCache(importSessionId, importTemplateId);
		}

		/// <summary>
		/// Get a collection of import columns
		/// </summary>
		/// <param name="importTemplateId">Import template Id</param>
		/// <returns>A collection of import columns</returns>
		private IEnumerable<ImportColumn> GetTemplateColumnMapping(Guid importTemplateId) {
			var select = GetTemplateDataSelect(importTemplateId);
			var templateData = select.ExecuteScalar<byte[]>();
			return templateData != null
				? JsonConvert.DeserializeObject<IEnumerable<ImportColumn>>(Encoding.UTF8.GetString(templateData))
				: new List<ImportColumn>();
		}

		/// <summary>
		/// Get import file template select by template id
		/// </summary>
		/// <param name="importTemplateId">Import template Id</param>
		/// <returns>Select</returns>
		private Select GetTemplateDataSelect(Guid importTemplateId) {
			var select = new Select(UserConnection).Top(1)
				.Column("TemplateData")
				.From(_importTemplateSchemaName)
				.Where("Id").IsEqual(Column.Parameter(importTemplateId)) as Select;
			return select;
		}

		/// <summary>
		/// Get collection of columns mapping with applied import template
		/// </summary>
		/// <param name="columns">Columns mapping from import parameters</param>
		/// <param name="templateColumns">Column mapping from import template</param>
		/// <returns>A collection of columns mapping with applied import template</returns>
		private IEnumerable<ImportColumn> GetAppliedColumns(IEnumerable<ImportColumn> columns, IEnumerable<ImportColumn> templateColumns) {
			foreach (var column in columns) {
				var templateColumn = templateColumns.FirstOrDefault(c => c.Source == column.Source);
				column.Destinations = templateColumn != null
					? templateColumn.Destinations
					: new List<ImportColumnDestination>();
			}
			return columns;
		}

		/// <summary>
		/// Get collection of columns with destinations 
		/// </summary>
		/// <param name="columns">Columns mapping from import parameters</param>
		/// <returns>A collection of columns with destinations</returns>
		private IEnumerable<ImportColumn> GetColumnsWithDestination(IEnumerable<ImportColumn> columns) {
			return columns.Where(column => column.Destinations.Any()).ToList();
		}

		/// <summary>
		/// Update template data by serialized column mapping
		/// </summary>
		/// <param name="importTemplateId">Import template id</param>
		/// <param name="columns">Columns mapping from import parameters</param>
		private void SaveColumnsMappingAsTemplate(Guid importTemplateId, IEnumerable<ImportColumn> columns) {
			var update = new Update(base.UserConnection, _importTemplateSchemaName)
				.Set("TemplateData", GetSerializedTemplateDataColumnExpression(columns))
				.Where("Id").IsEqual(Column.Parameter(importTemplateId)) as Update;
			update.Execute();
		}

		/// <summary>
		/// Serialize column mapping and create column expression from it
		/// </summary>
		/// <param name="columns">Template columns mapping</param>
		/// <returns>Query column expression</returns>
		private static QueryColumnExpression GetSerializedTemplateDataColumnExpression(IEnumerable<ImportColumn> columns) {
			return Column.Parameter(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(columns)));
		}

		/// <summary>
		/// Get import template id from cache.
		/// </summary>
		/// <param name="importSessionId">Import session id</param>
		/// <returns>Import template id</returns>
		private Guid GetImportTemplateIdFromCache(Guid importSessionId) {
			var key = GetImportTemplateCacheKey(importSessionId);
			return UserConnection.SessionData.GetValue(key, Guid.Empty);
		}
		
		/// <summary>
		/// Set import template id to cache.
		/// </summary>
		/// <param name="importSessionId">Import session id</param>
		/// <param name="importTemplateId">Import template id</param>
		private void SetImportTemplateIdToCache(Guid importSessionId, Guid importTemplateId) {
			var key = GetImportTemplateCacheKey(importSessionId);
			UserConnection.SessionData[key] = importTemplateId;
		}

		/// <summary>
		/// Get import template id cache key.
		/// </summary>
		/// <param name="importSessionId">Import session id</param>
		/// <returns>Import template id cache key</returns>
		private string GetImportTemplateCacheKey(Guid importSessionId) {
			return $"{importSessionId}_TemplateKey";
		}
		
		/// <summary>
		/// Get applied import template id from cache.
		/// </summary>
		/// <param name="importSessionId">Import session id</param>
		/// <returns>Applied import template id</returns>
		private Guid GetAppliedImportTemplateIdFromCache(Guid importSessionId) {
			var key = GetAppliedImportTemplateCacheKey(importSessionId);
			return UserConnection.SessionData.GetValue(key, Guid.Empty);
		}
		
		/// <summary>
		/// Set applied import template id to cache.
		/// </summary>
		/// <param name="importSessionId">Import session id</param>
		/// <param name="importTemplateId">Applied import template id</param>
		private void SetAppliedImportTemplateIdToCache(Guid importSessionId, Guid importTemplateId) {
			var key = GetAppliedImportTemplateCacheKey(importSessionId);
			UserConnection.SessionData[key] = importTemplateId;
		}
		
		/// <summary>
		/// Get applied import template id cache key.
		/// </summary>
		/// <param name="importSessionId">Import session id</param>
		/// <returns>Applied import template id cache key</returns>
		private string GetAppliedImportTemplateCacheKey(Guid importSessionId) {
			return $"{importSessionId}_AppliedTemplateKey";
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Set import template id in session data with import session id key
		/// </summary>
		/// <param name="request">File import template service request</param>
		/// <returns>Configuration service response</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
			ResponseFormat = WebMessageFormat.Json)]
		public ConfigurationServiceResponse SetImportTemplateId(FileImportTemplateServiceRequest request) {
			ValidateRequest(request, false);
			var response = new ConfigurationServiceResponse();
			try {
				SetImportTemplateIdToCache(request.ImportSessionId, request.ImportTemplateId);
			} catch (Exception e) {
				response.Exception = e;
			}
			return response;
		}

		/// <summary>
		/// Get import template id from session data by import session id key
		/// </summary>
		/// <param name="importSessionId">Import session id</param>
		/// <returns>Import template response</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
			ResponseFormat = WebMessageFormat.Json)]
		public ImportTemplateResponse GetImportTemplateId(Guid importSessionId) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));
			var response = new ImportTemplateResponse();
			try {
				response.ImportTemplateId = GetImportTemplateIdFromCache(importSessionId);
			} catch (Exception e) {
				response.Exception = e;
			}
			return response;
		}

		/// <summary>
		/// Get template id from session data and apply it to current import column mapping
		/// </summary>
		/// <param name="importSessionId">Import session id</param>
		/// <returns>Configuration service response</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
			ResponseFormat = WebMessageFormat.Json)]
		public ConfigurationServiceResponse ApplyImportTemplate(Guid importSessionId) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));
			var response = new ConfigurationServiceResponse();
			try {
				var importTemplateId = GetImportTemplateIdFromCache(importSessionId);
				InnerApplyImportTemplate(importSessionId, importTemplateId);
			} catch (Exception e) {
				response.Exception = e;
			}
			return response;
		}

		/// <summary>
		/// Clean applied import template Id
		/// </summary>
		/// <param name="importSessionId">Import session id</param>
		/// <returns>Configuration service response</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
			ResponseFormat = WebMessageFormat.Json)]
		public ConfigurationServiceResponse ForceCleanAppliedImportTemplateId(Guid importSessionId) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));
			var response = new ConfigurationServiceResponse();
			try {
				SetAppliedImportTemplateIdToCache(importSessionId, Guid.NewGuid());
			} catch (Exception e) {
				response.Exception = e;
			}
			return response;
		}

		/// <summary>
		/// Get current import column mapping and update by it import template data
		/// </summary>
		/// <param name="importSessionId">Import session id</param>
		/// <param name="importTemplateId">Import template id</param>
		/// <returns>Configuration service response</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
			ResponseFormat = WebMessageFormat.Json)]
		public ConfigurationServiceResponse SaveImportTemplate(FileImportTemplateServiceRequest request) {
			ValidateRequest(request);
			var response = new ConfigurationServiceResponse();
			try {
				var columnMappingParameters = FileImporter.GetColumnsMappingParameters(request.ImportSessionId);
				var columnsWithDestination = GetColumnsWithDestination(columnMappingParameters.Columns);
				SaveColumnsMappingAsTemplate(request.ImportTemplateId, columnsWithDestination);
			} catch (Exception e) {
				response.Exception = e;
			}
			return response;
		}

		#endregion

	}

	#endregion

	#region  Class: FileImportTemplateServiceRequest

	[DataContract]
	public class FileImportTemplateServiceRequest
	{

		#region  Fields: Public

		[DataMember(Name = "importTemplateId")]
		public Guid ImportTemplateId;

		[DataMember(Name = "importSessionId")]
		public Guid ImportSessionId;

		#endregion

	}

	#endregion

	#region  Class: ImportSessionInfoResponse

	[DataContract]
	public class ImportTemplateResponse : ConfigurationServiceResponse
	{

		#region  Fields: Public

		[DataMember(Name = "importTemplateId")]
		public Guid ImportTemplateId;

		#endregion

	}

	#endregion

}

