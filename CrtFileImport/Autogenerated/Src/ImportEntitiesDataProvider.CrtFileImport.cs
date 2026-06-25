namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Common;
	using Core;
	using Core.DB;
	using Core.Entities;
	using Core.Factories;

	#region Class: ImportEntitiesDataProvider

	[DefaultBinding(typeof(IImportEntitiesDataProvider), Name = nameof(ImportEntitiesDataProvider))]
	public class ImportEntitiesDataProvider : IImportEntitiesDataProvider
	{
		#region Constants: Private

		private const int MaxParametersCountPerQueryChunk = 1900;
		private const int MaxRowForInsertPerQueryChunk = 1000;

		private const string BufferedImportEntitySchemaName = "BufferedImportEntity";

		#endregion

		#region Fields: Private

		private ImportLogger _logger;
		private EntitySchema _schema;
		private IColumnsAggregatorFactory _columnsAggregatorFactory;
		private IColumnsAggregatorAdapter _columnsProcessor;
		private readonly string _importExcelRowIndex = "ImportExcelRowIndex";

		#endregion

		#region Constructors: Public

		public ImportEntitiesDataProvider(UserConnection userConnection) {
			UserConnection = userConnection;
			InitializeLogger();
		}

		public ImportEntitiesDataProvider(UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor) : this(
				userConnection) {
			_columnsProcessor = columnsProcessor;
			InitializeLogger();
		}

		public ImportEntitiesDataProvider(UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor,
				IFileImportFindExistsRowColumnProviderFactory columnProviderFactory,
				IFileImportInfoLogger fileImportInfoLogger) : this(userConnection,
				columnsProcessor) {
			_columnProviderFactory = columnProviderFactory;
			_fileImportInfoLogger = fileImportInfoLogger;
		}

		#endregion

		#region Properties: Private

		private IColumnsAggregatorAdapter ColumnsProcessor => _columnsProcessor ??
				(_columnsProcessor = ColumnsAggregatorFactory?.GetColumnsAggregator(UserConnection));

		private IColumnsAggregatorFactory ColumnsAggregatorFactory => _columnsAggregatorFactory ??
				(_columnsAggregatorFactory = ClassFactory.Get<IColumnsAggregatorFactory>());

		private ImportLogger Logger => _logger ?? (_logger = new ImportLogger(UserConnection));
		private IFileImportInfoLogger _fileImportInfoLogger;
		private IFileImportInfoLogger FileImportInfoLogger =>
			_fileImportInfoLogger ?? (_fileImportInfoLogger = ClassFactory.Get<IFileImportInfoLogger>());

		private UserConnection UserConnection { get; }

		private IFileImportFindExistsRowColumnProviderFactory _columnProviderFactory;
		private IFileImportFindExistsRowColumnProviderFactory ColumnProviderFactory =>
			_columnProviderFactory ?? (_columnProviderFactory =
				ClassFactory.Get<IFileImportFindExistsRowColumnProviderFactory>());

		private IFileImportFindExistsRowColumnProvider _columnProvider;

		private IFileImportFindExistsRowColumnProvider ColumnProvider =>
			_columnProvider ?? (_columnProvider = ColumnProviderFactory.GetColumnProvider());

		#endregion

		#region Methods: Private

		private Delete GetBufferedImportEntityDeleteQuery(object importSessionId = null) {
			var delete = new Delete(UserConnection)
				.From(BufferedImportEntitySchemaName);
			if (importSessionId != null) {
				delete.Where("ImportSessionId").IsEqual(Column.Parameter(importSessionId));
			}
			return delete;
		}

		private Insert GetBufferedImportEntityInsertQuery() {
			var bufferedImportEntity = new Insert(UserConnection).Into(BufferedImportEntitySchemaName);
			return bufferedImportEntity;
		}

		private IEnumerable<IEnumerable<ImportEntity>> GetImportEntitiesChunks(IEnumerable<ImportEntity> entities,
				IEnumerable<ImportColumn> keyColumns) {
			var entitiesList = entities.ToList();
			var columnsList = keyColumns.ToList();
			var maxParamsPerChunk = Math.Abs(MaxParametersCountPerQueryChunk / (columnsList.Count + 2));
			maxParamsPerChunk = Math.Min(MaxRowForInsertPerQueryChunk, maxParamsPerChunk);
			var chunksCount = (int)Math.Ceiling(entitiesList.Count / (double)maxParamsPerChunk);
			return entitiesList.SplitOnParts(chunksCount);
		}

		private List<Guid> GetPersistedImportSessionIds() {
			var sessionIds = new List<Guid>();
			var select = new Select(UserConnection).Distinct()
				.Column("ImportSessionId")
				.From(BufferedImportEntitySchemaName);
			using (var dbExecutor = UserConnection.EnsureDBConnection()) {
				using (var dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						sessionIds.Add(dataReader.GetColumnValue<Guid>("ImportSessionId"));
					}
				}
			}
			return sessionIds;
		}

		private void InitializeLogger() {
			BufferedImportEntitySaveDBError += Logger.HandleErrorMessage;
		}

		private void InnerSaveImportEntitiesToDb(IEnumerable<ImportEntity> entities,
				IEnumerable<ImportColumn> keyColumns,
				Guid importSessionId) {
			var importColumns = keyColumns.ToList();
			if (importColumns.IsEmpty()) {
				return;
			}
			FileImportInfoLogger.WriteSessionActionStartMessage(importSessionId,
				"ImportEntitiesDataProvider.InnerSaveImportEntitiesToDB: saving entities batches list to DB");
			var entitiesList = GetImportEntitiesChunks(entities, importColumns);
			foreach (var entitiesBatch in entitiesList) {
				FileImportInfoLogger.WriteSessionActionStartMessage(importSessionId,
					"ImportEntitiesDataProvider.InnerSaveImportEntitiesToDB: saving concrete entities batch" +
					$"(batch entities count = {entitiesBatch.Count()}) to DB");
				InsertBatchToDb(importSessionId, importColumns, entitiesBatch);
				FileImportInfoLogger.WriteSessionActionEndMessage(importSessionId,
					"ImportEntitiesDataProvider.InnerSaveImportEntitiesToDB: saving concrete entities batch" +
					$"(batch entities count = {entitiesBatch.Count()}) to DB");
			}
			FileImportInfoLogger.WriteSessionActionEndMessage(importSessionId,
				"ImportEntitiesDataProvider.InnerSaveImportEntitiesToDB: saving entities batches list to DB");
		}


		private ICollection<ImportEntityColumnValueInfo> GetColumnsToBufferedEntities(ImportEntity importEntity,
				IEnumerable<ImportColumn> keyColumns) {
			EntitySchemaColumnCollection schemaColumns = _schema.Columns;
			List<ImportEntityColumnValueInfo> valueInfos = new List<ImportEntityColumnValueInfo>();
			foreach ((ImportColumn column, int index) in keyColumns.Select((c, i) => (Column: c, Index: i + 1))) {
				string columnName = GetColumnName(column, index);
				ImportColumnDestination destination = column.Destinations.FirstOrDefault();
				if (destination == null) {
					throw new ArgumentNullOrEmptyException(nameof(destination));
				}
				DataValueType dataValueType = schemaColumns.GetByName(destination.ColumnName).DataValueType;
				ImportColumnValue columnValue = importEntity.FindColumnValue(column);
				object valueForSave = columnValue == null
					? dataValueType.DefValue
					: ColumnsProcessor.FindValueForSave(destination, columnValue);
				valueInfos.Add(new ImportEntityColumnValueInfo() {
					SourceColumnName = column.Source,
					ColumnName = columnName,
					ColumnValue = valueForSave,
					ColumnDataType = dataValueType
				});
			}
			return valueInfos;
		}

		private void InsertBatchToDb(Guid importSessionId, List<ImportColumn> importColumns,
				IEnumerable<ImportEntity> entitiesBatch) {
			try {
				var insertQuery = GetBufferedImportEntityInsertQuery();
				foreach (var importEntity in entitiesBatch) {
					IEnumerable<ImportEntityColumnValueInfo> columnValues =
						GetColumnsToBufferedEntities(importEntity, importColumns);
					ICollection<ImportEntityColumnValueInfo> parseErrorColumnValues = columnValues.
						Where(cv => cv.ColumnValue == null).ToList();
					if (parseErrorColumnValues.Any()) {
						SetImportEntityException(parseErrorColumnValues, importEntity);
						continue;
					}
					if (columnValues.All(cv => cv.ColumnValue.ToString() == string.Empty)) {
						continue;
					}
					insertQuery.Values();
					foreach (ImportEntityColumnValueInfo columnValueInfo in columnValues) {
						insertQuery.Set(columnValueInfo.ColumnName,
							Column.Parameter(columnValueInfo.ColumnValue, columnValueInfo.ColumnDataType));
					}
					insertQuery.Set("ImportSessionId", Column.Const(importSessionId));
					insertQuery.Set(_importExcelRowIndex, Column.Parameter((int)importEntity.RowIndex));
				}
				insertQuery.Execute();
			} catch (Exception e) {
				OnBufferedImportEntitySaveDBError(new ErrorMessageEventArgs {
					ImportSessionId = importSessionId,
					Exception = e,
					Message = e.Message
				});
			}
		}

		private void SetImportEntityException(ICollection<ImportEntityColumnValueInfo> parseErrorColumnValues,
			ImportEntity importEntity) {
			var message = new LocalizableString(UserConnection.Workspace.ResourceStorage,
				"ImportEntitiesDataProvider", "LocalizableStrings.ParseErrorMessage.Value");
			string[] columnNames = parseErrorColumnValues.Select(columnValue =>
					$"{columnValue.SourceColumnName}").ToArray();
			importEntity.ImportEntityException = new InvalidCastException(string.Format(message,
				string.Join(", ", columnNames)));
		}

		private string GetColumnName(ImportColumn column, int index) {
			return ColumnProvider.GetDbColumnName(new FileImportFindExistsRowColumnInfo {
				ImportColumn = column,
				ColumnIndex = index
			});
		}

		#endregion

		#region Methods: Protected

		protected void OnBufferedImportEntitySaveDBError(ErrorMessageEventArgs eventArgs) {
			BufferedImportEntitySaveDBError?.Invoke(this, eventArgs);
		}

		protected virtual bool SaveImportEntitiesToDB(IEnumerable<ImportEntity> entities,
				IEnumerable<ImportColumn> keyColumns,
				Guid importSessionId) {
			var success = true;
			Logger.FileName = importSessionId.ToString();
			try {
				var importEntities = entities.ToList();
				CleanBufferedImportEntities(importSessionId);
				InnerSaveImportEntitiesToDb(importEntities, keyColumns, importSessionId);
			} catch (Exception e) {
				success = false;
				OnBufferedImportEntitySaveDBError(new ErrorMessageEventArgs {
						ImportSessionId = importSessionId,
						Exception = e,
						Message = e.Message
				});
			}
			Logger.SaveLog();
			return success;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Clears data from BufferedImportEntity table by provided <paramref name="importSessionId" />
		/// </summary>
		/// <param name="importSessionId"></param>
		public void CleanBufferedImportEntities(Guid importSessionId) {
			FileImportInfoLogger.WriteSessionActionStartMessage(importSessionId,
				"ImportEntitiesDataProvider: cleaning Buffered import entities before saving");
			Logger.FileName = importSessionId.ToString();
			try {
				var delete = GetBufferedImportEntityDeleteQuery(importSessionId);
				delete.Execute();
			} catch (Exception e) {
				OnBufferedImportEntitySaveDBError(new ErrorMessageEventArgs {
						ImportSessionId = importSessionId,
						Exception = e,
						Message = e.Message
				});
			}
			Logger.SaveLog();
		}

		/// <inheritdoc cref="IImportEntitiesDataProvider.CleanBufferedImportEntities()"/>
		public void CleanBufferedImportEntities() {
			FileImportInfoLogger.WriteActionMessage(
				"ImportEntitiesDataProvider: start cleaning Buffered import entities.");
			try {
				var delete = GetBufferedImportEntityDeleteQuery();
				delete.Execute();
			} catch (Exception e) {
				OnBufferedImportEntitySaveDBError(new ErrorMessageEventArgs {
					Exception = e,
					Message = e.Message
				});
			}
			FileImportInfoLogger.WriteActionMessage(
				"ImportEntitiesDataProvider: end cleaning Buffered import entities.");
		}

		public void CleanOldImportEntitiesRawData(Func<Guid, ImportParameters> findImportParametersFunc) {
			try {
				var savedSessionIds = GetPersistedImportSessionIds();
				var notActualIds = savedSessionIds.Where(id => findImportParametersFunc(id) == null)
					.Select(i => Column.Const(i));
				var queryColumnExpressions = notActualIds.ToList();
				if (queryColumnExpressions.Count > 0) {
					var delete = GetBufferedImportEntityDeleteQuery();
					delete.Where("ImportSessionId").In(queryColumnExpressions);
					delete.Execute();
				}
				FileImportInfoLogger.WriteActionMessage("ImportEntitiesDataProvider: finish CleanOldImportEntitiesRawData");
			} catch (Exception e) {
				OnBufferedImportEntitySaveDBError(new ErrorMessageEventArgs {
						ImportSessionId = Guid.Empty,
						Exception = e,
						Message = e.Message
				});
				throw;
			}
			Logger.SaveLog();
		}

		public bool SaveImportEntitiesToDB(ImportParameters parameters, IEnumerable<ImportColumn> keyColumns) {
			FileImportInfoLogger.WriteSessionActionStartMessage(parameters.ImportSessionId,
				"ImportEntitiesDataProvider.SaveImportEntitiesToDB");
			_schema = UserConnection.EntitySchemaManager.GetInstanceByUId(parameters.RootSchemaUId);
			var result = SaveImportEntitiesToDB(parameters.Entities, keyColumns, parameters.ImportSessionId);
			FileImportInfoLogger.WriteSessionActionEndMessage(parameters.ImportSessionId,
				"ImportEntitiesDataProvider.SaveImportEntitiesToDB");
			return result;
		}

		/// <summary>
		/// Check available key columns count.
		/// </summary>
		/// <param name="keyColumnsCount">Key columns count.</param>
		/// <returns>Result validate.</returns>
		public bool ValidateKeyColumnsCount(int keyColumnsCount) {
			var bufferedImportEntitySchema =
					UserConnection.EntitySchemaManager.GetInstanceByName(BufferedImportEntitySchemaName);
			var columns = bufferedImportEntitySchema.Columns;
			var lastKeyColumn = columns.Any(c => c.Name == $"Column{keyColumnsCount}");
			return lastKeyColumn;
		}

		#endregion

		public event EventHandler<ErrorMessageEventArgs> BufferedImportEntitySaveDBError;
	}

	#endregion
}

