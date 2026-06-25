namespace Terrasoft.Configuration.FileImport
{
	using Common;
	using Core;
	using Core.Entities;
	using Core.Factories;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core.Store;
	using Terrasoft.Web.Http.Abstractions;

	public abstract class BaseFileImporter : IBaseFileImporter
	{

		#region Constants: Private

		private const string EntitySchemaUIdPropertyName = "entitySchemaUId";
		private const string ImportSessionIdPropertyName = "importSessionId";
		private const string TypeUIdPropertyName = "TypeUId";
		private const string ReferenceSchemaUIdPropertyName = "ReferenceSchemaUId";
		private IImportEntitiesDataProvider _importEntitiesDataProvider;

		#endregion

		#region  Fields: Private

		private IColumnsAggregatorAdapter _columnsProcessor;
		private ICacheStore _cacheStore;

		#endregion

		#region Properties: Protected

		/// <summary>
		/// User connection.
		/// </summary>
		protected UserConnection UserConnection { get; }

		/// <summary>
		/// Columns processor.
		/// </summary>
		protected IColumnsAggregatorAdapter ColumnsProcessor {
			get {
				if(_columnsProcessor != null) {
					return _columnsProcessor;
				}
				_columnsProcessor = CreateColumnsAggregator();
				return _columnsProcessor;
			}
		}

		protected ICacheStore CacheStore => _cacheStore ?? (_cacheStore = Store.Cache[CacheLevel.Application]);

		protected IImportEntitiesDataProvider ImportEntitiesDataProvider => _importEntitiesDataProvider ??
		(_importEntitiesDataProvider = ClassFactory.Get<IImportEntitiesDataProvider>(
				new ConstructorArgument("userConnection", UserConnection),
				new ConstructorArgument("columnsProcessor", ColumnsProcessor)));

		#endregion

		#region Constructor: Protected

		/// <summary>
		/// Creates instance of type <see cref="BaseFileImporter" />.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		protected BaseFileImporter(UserConnection userConnection) => UserConnection = userConnection;

		/// <summary>
		/// Creates instance of type <see cref="BaseFileImporter" />.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		protected BaseFileImporter(UserConnection userConnection, ICacheStore cacheStore, IColumnsAggregatorAdapter columnsProcessor) {
			UserConnection = userConnection;
			_cacheStore = cacheStore;
			_columnsProcessor = columnsProcessor;

		}
		#endregion

		#region Methods: Private

		/// <summary>
		/// Creates file processor.
		/// </summary>
		/// <param name="parameters">File import parameters.</param>
		/// <returns>File processor.</returns>
		private IFileProcessor CreateFileProcessor(ImportParameters parameters) => ClassFactory.Get<ExcelFileProcessor>(
				new ConstructorArgument("userConnection", UserConnection),
				new ConstructorArgument("parameters", parameters));

		/// <summary>
		/// Finds entity schema column.
		/// </summary>
		/// <param name="columnName">Column name.</param>
		/// <param name="schemaUId">Entity schema unique identifier.</param>
		/// <returns>Entity schema column.</returns>
		private EntitySchemaColumn FindSchemaColumnByName(string columnName, Guid schemaUId) {
			var schema = UserConnection.EntitySchemaManager.GetInstanceByUId(schemaUId);
			foreach(var column in schema.Columns) {
				if(string.Equals(column.Name, columnName, StringComparison.OrdinalIgnoreCase)) {
					return column;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets column value name.
		/// </summary>
		/// <param name="columnName">Column name.</param>
		/// <param name="entitySchemaUId">Entity schema unique identifier.</param>
		/// <returns>Column value name.</returns>
		private string GetColumnValueName(string columnName, Guid entitySchemaUId) {
			var entitySchemaColumn = FindSchemaColumnByName(columnName, entitySchemaUId);
			return entitySchemaColumn.ColumnValueName;
		}

		/// <summary>
		/// Updates columns mapping.
		/// </summary>
		/// <param name="parameters">File import parameters.</param>
		/// <param name="columns">Import columns.</param>
		private void UpdateColumnsMapping(ImportParameters parameters, IEnumerable<ImportColumn> columns) {
			var parametersColumns = columns.ToList();
			foreach(var column in parametersColumns) {
				foreach(var destination in column.Destinations) {
					IEnumerable<string> propertyNames = new List<string>(destination.Properties.Keys);
					foreach(var propertyName in propertyNames) {
						var propertyValue = destination.Properties[propertyName];
						if(propertyName.Equals(TypeUIdPropertyName) ||
								propertyName.Equals(ReferenceSchemaUIdPropertyName)) {
							destination.Properties[propertyName] = propertyValue is Guid
								? propertyValue
								: Guid.Parse((string)propertyValue);
						}
					}
					if(destination.ColumnValueName.IsNotNullOrEmpty()) {
						continue;
					}
					destination.ColumnValueName = GetColumnValueName(destination.ColumnName, destination.SchemaUId);
				}
			}
			parameters.Columns = parametersColumns;
		}

		private void ClearOldRawImportEntities() {
			if(UserConnection.GetIsFeatureEnabled("HighestSpeedFileImport")) {
				ImportEntitiesDataProvider.CleanOldImportEntitiesRawData(FindImportParameters);
			}
		}
		private bool CheckCanUseImportEntitiesStorage(ImportParameters parameters) {
			var keyColumns = GetKeyColumns(parameters);
			var keyColumnsCount = keyColumns.Count();
			var isValidColumnsCount = ImportEntitiesDataProvider.ValidateKeyColumnsCount(keyColumnsCount);
			parameters.CanUseImportEntitiesStorage = isValidColumnsCount;
			return isValidColumnsCount;
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Saves file import parameters.
		/// </summary>
		/// <param name="parameters"></param>
		protected virtual void SaveImportParameters(ImportParameters parameters) => parameters.Save();

		/// <summary>
		/// Create instance of <see cref="IColumnsAggregatorAdapter"/>
		/// </summary>
		/// <returns></returns>
		protected abstract IColumnsAggregatorAdapter CreateColumnsAggregator();

		/// <summary>
		/// Execute import by parameters
		/// </summary>
		/// <param name="parameters"></param>
		protected abstract void Import(ImportParameters parameters);

		/// <summary>
		/// Gets import parameters.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <returns>Import parameters.</returns>
		protected virtual ImportParameters ForceGetImportParameters(Guid importSessionId) {
			var parameters = FindImportParameters(importSessionId) ?? new ImportParameters(importSessionId);
			return parameters;
		}

		protected IEnumerable<ImportColumn> GetKeyColumns(ImportParameters parameters) {
			var keyColumns = new List<ImportColumn>();
			foreach(var column in parameters.Columns) {
				foreach(var destination in column.Destinations) {
					if(!destination.IsKey) {
						continue;
					}
					keyColumns.Add(column);
					break;
				}
			}
			return keyColumns;
		}

		protected virtual void ImportInternal(Guid importSessionId) {
		}

		protected void SetImportParametersHeaderColumns(ImportParameters parameters, Guid importObjectUId) {
			if (parameters.HeaderColumnsValues != null) {
				var fileProcessor = CreateFileProcessor(parameters);
				fileProcessor.ProcessObject(importObjectUId);
			}
			SaveImportParameters(parameters);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		/// <summary>
		/// Finds file import parameters.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <returns>File import parameters.</returns>
		public virtual ImportParameters FindImportParameters(Guid importSessionId) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));

			var parameters = (ImportParameters)CacheStore[importSessionId.ToString()];
			return parameters;
		}

		/// <inheritdoc />
		/// <summary>
		/// Returns columns mapping parameters.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <returns>Columns mapping parameters.</returns>
		public virtual ColumnsMappingResponse GetColumnsMappingParameters(Guid importSessionId) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));

			var parameters = GetImportParameters(importSessionId);
			var rootSchemaUId = parameters.RootSchemaUId;
			var rootSchemaName = UserConnection.EntitySchemaManager.GetItemByUId(rootSchemaUId).Name;
			return new ColumnsMappingResponse {
				Columns = parameters.Columns,
				RootSchemaName = rootSchemaName
			};
		}

		/// <inheritdoc />
		/// <summary>
		/// Refresh column mapping.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		public virtual void RefreshColumnMapping(Guid importSessionId) {
			var parameters = ForceGetImportParameters(importSessionId);
			parameters.Columns = null;
			var fileProcessor = CreateFileProcessor(parameters);
			fileProcessor.ProcessObject(parameters.ImportObject.UId);
			SaveImportParameters(parameters);
		}

		/// <inheritdoc />
		/// <summary>
		/// Gets file import parameters.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <returns>File import parameters.</returns>
		public ImportParameters GetImportParameters(Guid importSessionId) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));

			var parameters = FindImportParameters(importSessionId);
			if(parameters != null) {
				return parameters;
			}
			throw new InvalidOperationException();
		}

		/// <inheritdoc />
		/// <summary>
		/// Starts import.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		public void Import(Guid importSessionId) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));

			ClearOldRawImportEntities();
			ImportInternal(importSessionId);
		}

		/// <inheritdoc />
		/// <summary>
		/// Starts import.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		public void ImportWithParams(ImportParameters parameters) {
			parameters.CheckArgumentNull(nameof(parameters));

			Import(parameters);
		}

		/// <inheritdoc />
		/// <summary>
		/// Processes file and saves its information to cache.
		/// </summary>
		public void SaveFile() {
			var request = HttpContext.Current?.Request;
			var form = request?.Form;
			form.CheckArgumentNull(nameof(form));
			Guid.TryParse(form[EntitySchemaUIdPropertyName], out var rootSchemaUId);
			var importSessionId = Guid.Parse(form[ImportSessionIdPropertyName]);
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));
			var file = request.Files["files"];
			file.CheckArgumentNull(nameof(file));
			var parameters = ForceGetImportParameters(importSessionId);
			var fileProcessor = CreateFileProcessor(parameters);
			fileProcessor.ProcessFile(file);
			if(rootSchemaUId.IsNotEmpty()) {
				fileProcessor.ProcessObject(rootSchemaUId);
			}
			SaveImportParameters(parameters);
		}

		/// <inheritdoc />
		/// <summary>
		/// Sets tags.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <param name="tags">Import tag names.</param>
		public void SaveImportTags(Guid importSessionId, List<ImportTag> tags) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));
			tags.CheckArgumentNull(nameof(tags));

			var parameters = GetImportParameters(importSessionId);
			parameters.ImportTags = tags;
			SaveImportParameters(parameters);
		}

		/// <inheritdoc />
		public void SetColumnsMappingParameters(Guid importSessionId, List<ImportColumn> columns) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));
			columns.CheckArgumentNull(nameof(columns));

			var parameters = GetImportParameters(importSessionId);
			UpdateColumnsMapping(parameters, columns);
			SaveImportParameters(parameters);
		}

		/// <inheritdoc />
		public void SetFileInfo(Guid importSessionId, string fileName) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));

			var parameters = ForceGetImportParameters(importSessionId);
			parameters.FileName = fileName;
			if(fileName.IsNullOrEmpty()) {
				parameters.ResetFileData();
			}
			SaveImportParameters(parameters);
		}

		/// <inheritdoc />
		public virtual void SetImportObject(Guid importSessionId, ImportObject importObject) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));
			importObject.CheckArgumentNull(nameof(importObject));
			var parameters = ForceGetImportParameters(importSessionId);
			parameters.ImportObject = importObject;
			SetImportParametersHeaderColumns(parameters, importObject.UId);
		}

		/// <inheritdoc />
		public bool Validate(Guid importSessionId) {
			importSessionId.CheckArgumentEmpty(nameof(importSessionId));

			var parameters = GetImportParameters(importSessionId);
			CheckCanUseImportEntitiesStorage(parameters);
			SaveImportParameters(parameters);
			return parameters.CanUseImportEntitiesStorage;
		}

		#endregion

	}
}

