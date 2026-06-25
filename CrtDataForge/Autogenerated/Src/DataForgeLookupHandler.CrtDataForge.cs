namespace Terrasoft.Configuration.DataForge
{
	using Common;
	using Core.Configuration;
	using Core.DB;
	using global::Common.Logging;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Interface: ILookupHandler

	/// <summary>
	/// Provides functionality to identify and extract lookup definitions and values.
	/// </summary>
	public interface ILookupHandler
	{
		/// <summary>
		/// Determines whether the given entity represents a lookup schema.
		/// </summary>
		/// <param name="entity">The entity to evaluate.</param>
		/// <returns><c>true</c> if the entity is a lookup schema; otherwise, <c>false</c>.</returns>
		bool IsLookup(Entity entity);

		/// <summary>
		/// Determines whether the given entity is a lookup value.
		/// </summary>
		/// <param name="entity">The entity to evaluate.</param>
		/// <returns><c>true</c> if the entity is a lookup value; otherwise, <c>false</c>.</returns>
		bool IsLookupValue(Entity entity);

		/// <summary>
		/// Retrieves a collection of full lookup definitions for the specified lookup schema identifiers.
		/// </summary>
		/// <param name="ids">The unique identifiers of the lookup schemas.</param>
		/// <returns>A list of <see cref="LookupDefinition"/> objects.</returns>
		List<LookupDefinition> GetLookupDefinitions(IReadOnlyList<Guid> ids = null);

		/// <summary>
		/// Maps a <see cref="LookupDefinition"/> to a <see cref="LookupSummary"/>.
		/// </summary>
		/// <param name="def">The lookup definition to map.</param>
		/// <returns>A <see cref="LookupSummary"/> object.</returns>
		LookupSummary MapToLookupSummary(LookupDefinition def);

		/// <summary>
		/// Maps a <see cref="LookupValueDefinition"/> to a <see cref="LookupValueSummary"/>.
		/// </summary>
		/// <param name="def">The lookup value definition to map.</param>
		/// <returns>A <see cref="LookupValueSummary"/> object.</returns>
		LookupValueSummary MapToLookupValueSummary(LookupValueDefinition def);

		/// <summary>
		/// Retrieves all lookup definitions that include the specified lookup value schema UId.
		/// </summary>
		/// <param name="valuesSchemaUId">The UId of values schema</param>
		/// <returns>A list of <see cref="LookupDefinition"/> objects that reference the given value.</returns>
		List<LookupDefinition> GetLookupDefinitionsForValue(Guid valuesSchemaUId);

		/// <summary>
		/// Retrieves lookup value definitions for the specified collection of lookup definitions.
		/// </summary>
		/// <param name="lookupDefinitions">The collection of lookup definitions.</param>
		/// <returns>A list of <see cref="LookupValueDefinition"/> objects associated with the provided lookups.</returns>
		List<LookupValueDefinition> GetLookupValueDefinitionsForLookups(IReadOnlyList<LookupDefinition> lookupDefinitions);

		/// <summary>
		/// Checks if the entity is from an excluded schema.
		/// </summary>
		/// <param name="entity">The entity to evaluate.</param>
		/// <returns><c>true</c> if the entity is from an excluded schema; otherwise, <c>false</c>.</returns>
		bool IsFromExcludedSchema(Entity entity);
	}

	#endregion

	#region Class: LookupDataRow

	internal class LookupDataRow
	{
		public Guid Id { get; set; }
		public Guid? UId { get; set; }
		public Guid? SysEntitySchemaUId { get; set; }
		public string Name { get; set; }
		public string Caption { get; set; }
		public string Description { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string SchemaName { get; set; }
	}

	#endregion

	#region Enum: LookupQueryContext

	/// <summary>
	/// Defines the context for building lookup queries with specific JOIN and filter conditions.
	/// </summary>
	internal enum LookupQueryContext
	{
		/// <summary>
		/// Default query without special filtering or joins.
		/// </summary>
		Default,

		/// <summary>
		/// Query against Lookup table with exclusion filters (excluding DFSyncExceptions).
		/// </summary>
		LookupAllowed,

		/// <summary>
		/// Query against SysSchema table for lookup schemas with exclusion filters.
		/// </summary>
		SysSchemaLookupAllowed,

		/// <summary>
		/// Query against Lookup table for unlimited sync lookups (including DFSyncUnlimited).
		/// </summary>
		LookupUnlimited,

		/// <summary>
		/// Query against SysSchema table for unlimited sync lookup schemas.
		/// </summary>
		SysSchemaLookupUnlimited
	}

	#endregion

	#region Class: LookupHandler

	/// <summary>
	/// Resolves lookup metadata and value definitions.
	/// </summary>
	[DefaultBinding(typeof(ILookupHandler))]
	internal class LookupHandler : ILookupHandler
	{

		#region Constants: Private

		private const string LookupSchemaName = "Lookup";
		private const string BaseLookupSchemaName = "BaseLookup";
		private const string BaseHierarchicalLookupSchemaName = "BaseHierarchicalLookup";
		private const string BaseCodeLookupSchemaName = "BaseCodeLookup";
		private const string BaseImageLookupSchemaName = "BaseImageLookup";
		private const string BaseCodeImageLookupSchemaName = "BaseCodeImageLookup";
		private const string SysSchemaName = "SysSchema";
		private const string DFSyncExceptionsSchemaName = "DFSyncExceptions";
		private const string DFSyncUnlimitedSchemaName = "DFSyncUnlimited";

		private const int DataForgeLookupChunkSizeDefault = 1000;
		private const int DataForgeLookupRecordSoftLimit = 3000;
		private const int DataForgeLookupMaxFieldLengthDefault = 250;

		private static class EntityFields
		{
			public const string Id = nameof(Id);
			public const string ParentId = nameof(ParentId);
			public const string UId = nameof(UId);
			public const string Name = nameof(Name);
			public const string Caption = nameof(Caption);
			public const string Description = nameof(Description);
			public const string ModifiedOn = nameof(ModifiedOn);
			public const string CreatedOn = nameof(CreatedOn);
			public const string SysEntitySchemaUId = nameof(SysEntitySchemaUId);
			public const string RecordInactive = nameof(RecordInactive);
		}

		private static class DataValueTypeFields
		{
			public const string Size = nameof(Size);
		}

		private static readonly string[] SysSchemaLookupFields = {
			EntityFields.Id,
			EntityFields.UId,
			EntityFields.Name,
			EntityFields.Caption,
			EntityFields.Description,
			EntityFields.ModifiedOn,
			EntityFields.CreatedOn,
			EntityFields.RecordInactive
		};

		private static readonly string[] LookupSchemaFields = {
			EntityFields.Id,
			EntityFields.Name,
			EntityFields.Description,
			EntityFields.ModifiedOn,
			EntityFields.CreatedOn,
			EntityFields.SysEntitySchemaUId,
			EntityFields.RecordInactive
		};

		private static readonly string[] LookupValueFields = {
			EntityFields.Id,
			EntityFields.Name,
			EntityFields.Description,
			EntityFields.ModifiedOn,
			EntityFields.CreatedOn,
			EntityFields.RecordInactive
		};

		private static readonly HashSet<string> LookupSchemas = new HashSet<string>() {
			LookupSchemaName,
			BaseLookupSchemaName,
			BaseHierarchicalLookupSchemaName,
			BaseCodeLookupSchemaName,
			BaseImageLookupSchemaName,
			BaseCodeImageLookupSchemaName,
		};

		/// <summary>
		/// SysSchema.ParentId values that indicate a lookup-family schema.
		/// </summary>
		private static readonly Guid[] LookupBaseParentUIds = new[] {
			new Guid("D8D9C657-DF07-48DD-9B83-FC1FF01D1939"), // Lookup
			new Guid("50C4D8F3-45C4-4989-A68E-2EDCEE2B0B53"), // BaseHierarchicalLookup
			new Guid("A8A295CB-B02A-4D82-896B-66EBDB378E98"), // BaseCodeLookup
			new Guid("A23AE877-EF9E-412B-BB96-A1D328122B37"), // BaseImageLookup
			new Guid("D7307337-6F57-43DB-B710-E72114A8CEE7"), // BaseCodeImageLookup
			new Guid("19125E51-A235-4540-A4C7-5D9CAFF66621")  // BaseLookup
		};

		#endregion

		#region Fields: Private

		private static readonly ILog _log = LogManager.GetLogger("DataForge");
		private readonly UserConnection _userConnection;
		private readonly IChecksumProvider _checksumProvider;
		private int? _effectiveLimit = null;
		private int? _chunkSize = null;
		private int? _maxFieldLength = null;
		private HashSet<Guid> _virtualSchemaUIds = null;

		#endregion

		#region Properties: Private

		private int EffectiveLimit {
			get {
				if (!_effectiveLimit.HasValue) {
					int softLimit = SysSettings.GetValue(_userConnection, "DataForgeLookupRecordLimit", DataForgeLookupRecordSoftLimit);
					_effectiveLimit = softLimit;
				}
				return _effectiveLimit.Value;
			}
		}

		private int ChunkSize {
			get {
				if (!_chunkSize.HasValue) {
					_chunkSize = SysSettings.GetValue(_userConnection, "DataForgeLookupChunkSize", DataForgeLookupChunkSizeDefault);
				}
				return _chunkSize.Value;
			}
		}

		private int MaxFieldLength {
			get {
				if (!_maxFieldLength.HasValue) {
					_maxFieldLength = SysSettings.GetValue(_userConnection, "DataForgeLookupMaxFieldLength", DataForgeLookupMaxFieldLengthDefault);
				}
				return _maxFieldLength.Value;
			}
		}

		private HashSet<Guid> VirtualSchemaUIds {
			get {
				if (_virtualSchemaUIds == null) {
					_virtualSchemaUIds = GetVirtualSchemaUIds();
				}
				return _virtualSchemaUIds;
			}
		}

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Creates a new instance of <see cref="LookupHandler"/>.
		/// </summary>
		public LookupHandler(UserConnection userConnection, IChecksumProvider checksumProvider) {
			_userConnection = userConnection;
			_checksumProvider = checksumProvider;
		}

		#endregion

		#region Methods: Private

		private bool IsTextFieldToTruncate(string fieldName) {
			return fieldName == EntityFields.Name
				|| fieldName == EntityFields.Description
				|| fieldName == EntityFields.Caption;
		}

		private void FilterActiveRecords(Select select, string schemaName, EntitySchema schema, LookupQueryContext queryContext) {
			var recordInactiveColumn = schema.Columns.FindByName(EntityFields.RecordInactive);
			if (recordInactiveColumn == null) {
				return;
			}

			_log.Debug($"Filtering inactive records for schema '{schemaName}'");
			
			// filtering out all inactive records
			select.Where(schemaName, EntityFields.RecordInactive).IsEqual(Column.Parameter(false));
		}

		private Select BuildSelectQuery(string schemaName, IReadOnlyList<string> fields, EntitySchema schema, LookupQueryContext queryContext = LookupQueryContext.Default) {
			var select = new Select(_userConnection).From(schemaName);

			if (fields != null && fields.Count > 0) {
				int addedColumns = 0;
				foreach (string field in fields) {
					var column = schema.Columns.FindByName(field);
					if (column == null) {
						_log.Debug($"Field '{field}' not found in schema '{schemaName}', skipping");
						continue;
					}

					if (IsTextFieldToTruncate(field)) {
						var columnExpression = new QueryColumnExpression(schemaName, field);
						var substringFunc = new SubstringQueryFunction(columnExpression, 1, MaxFieldLength);
						select.Column(substringFunc).As(field);
					} else {
						select.Column(schemaName, field);
					}
					addedColumns++;
				}

				if (addedColumns == 0) {
					_log.Debug($"No valid fields found for schema '{schemaName}', adding Id column as fallback");
					select.Column(schemaName, EntityFields.Id);
				}
			} else {
				select.Column(schemaName, "*");
			}

			switch (queryContext) {
			case LookupQueryContext.SysSchemaLookupAllowed:
			select.LeftOuterJoin(DFSyncExceptionsSchemaName).As("ex")
				.On(schemaName, EntityFields.Name).IsEqual("ex", EntityFields.Name);
			select.Where(schemaName, EntityFields.ParentId)
				.In(Column.Parameters(LookupBaseParentUIds.Cast<object>().ToArray()))
				.And("ex", EntityFields.Name).IsNull();
			break;

			case LookupQueryContext.SysSchemaLookupUnlimited:
			select.InnerJoin(DFSyncUnlimitedSchemaName).As("inc")
				.On(schemaName, EntityFields.Name).IsEqual("inc", EntityFields.Name);
			select.LeftOuterJoin(DFSyncExceptionsSchemaName).As("ex")
				.On(schemaName, EntityFields.Name).IsEqual("ex", EntityFields.Name);
			select.Where(schemaName, EntityFields.ParentId)
				.In(Column.Parameters(LookupBaseParentUIds.Cast<object>().ToArray()))
				.And("ex", EntityFields.Name).IsNull();
			break;

			case LookupQueryContext.LookupAllowed:
			select.InnerJoin(SysSchemaName).As("s")
				.On(schemaName, EntityFields.SysEntitySchemaUId).IsEqual("s", EntityFields.UId);
			select.LeftOuterJoin(DFSyncExceptionsSchemaName).As("ex")
				.On("s", EntityFields.Name).IsEqual("ex", EntityFields.Name);
			select.Where("ex", EntityFields.Name).IsNull();
			break;

			case LookupQueryContext.LookupUnlimited:
			select.InnerJoin(SysSchemaName).As("s")
				.On(schemaName, EntityFields.SysEntitySchemaUId).IsEqual("s", EntityFields.UId);
			select.InnerJoin(DFSyncUnlimitedSchemaName).As("inc")
				.On("s", EntityFields.Name).IsEqual("inc", EntityFields.Name);
			select.LeftOuterJoin(DFSyncExceptionsSchemaName).As("ex")
				.On("s", EntityFields.Name).IsEqual("ex", EntityFields.Name);
			select.Where("ex", EntityFields.Name).IsNull();
			break;

			case LookupQueryContext.Default:
			default:
			break;
			}

			FilterActiveRecords(select, schemaName, schema, queryContext);

			select.OrderByAsc(schemaName, EntityFields.Id);

			return select;
		}

		private void ApplyIdFilters(Select query, string schemaName, IReadOnlyList<Guid> ids) {
			if (ids != null && ids.Count > 0) {
				query.And(schemaName, EntityFields.Id).In(Column.Parameters(ids.Cast<object>().ToArray()));
			}
		}

		private void ApplyEqualityFilters(Select query, string schemaName, Dictionary<string, Guid> filters) {
			if (filters == null || filters.Count == 0) {
				return;
			}

			foreach (var filter in filters) {
				if (!filter.Value.IsEmpty()) {
					query.And(schemaName, filter.Key).IsEqual(Column.Parameter(filter.Value));
				}
			}
		}

		private LookupDataRow ReadDataRowFromReader(IDataReader reader, string schemaName, Dictionary<string, int> readerColumnMap) {
			var dataRow = new LookupDataRow {
				SchemaName = schemaName
			};

			int loadedColumns = 0;

			try {
				if (readerColumnMap.ContainsKey(EntityFields.Id)) {
					dataRow.Id = reader.GetColumnValue<Guid>(EntityFields.Id);
					loadedColumns++;
				}
			} catch (Exception ex) {
				_log.Debug($"Failed to load Id from reader: {ex.Message}");
			}

			try {
				if (readerColumnMap.ContainsKey(EntityFields.UId)) {
					var uid = reader.GetColumnValue<Guid>(EntityFields.UId);
					if (uid != Guid.Empty) {
						dataRow.UId = uid;
						loadedColumns++;
					}
				}
			} catch (Exception ex) {
				_log.Debug($"Failed to load UId from reader: {ex.Message}");
			}

			try {
				if (readerColumnMap.ContainsKey(EntityFields.SysEntitySchemaUId)) {
					var schemaUId = reader.GetColumnValue<Guid>(EntityFields.SysEntitySchemaUId);
					if (schemaUId != Guid.Empty) {
						dataRow.SysEntitySchemaUId = schemaUId;
						loadedColumns++;
					}
				}
			} catch (Exception ex) {
				_log.Debug($"Failed to load SysEntitySchemaUId from reader: {ex.Message}");
			}

			try {
				if (readerColumnMap.ContainsKey(EntityFields.Name)) {
					dataRow.Name = reader.GetColumnValue<string>(EntityFields.Name);
					loadedColumns++;
				}
			} catch (Exception ex) {
				_log.Debug($"Failed to load Name from reader: {ex.Message}");
			}

			try {
				if (readerColumnMap.ContainsKey(EntityFields.Caption)) {
					dataRow.Caption = reader.GetColumnValue<string>(EntityFields.Caption);
					loadedColumns++;
				}
			} catch (Exception ex) {
				_log.Debug($"Failed to load Caption from reader: {ex.Message}");
			}

			try {
				if (readerColumnMap.ContainsKey(EntityFields.Description)) {
					dataRow.Description = reader.GetColumnValue<string>(EntityFields.Description);
					loadedColumns++;
				}
			} catch (Exception ex) {
				_log.Debug($"Failed to load Description from reader: {ex.Message}");
			}

			try {
				if (readerColumnMap.ContainsKey(EntityFields.ModifiedOn)) {
					var modifiedOn = reader.GetColumnValue<DateTime>(EntityFields.ModifiedOn);
					if (modifiedOn != default && modifiedOn != DateTime.MinValue) {
						dataRow.ModifiedOn = modifiedOn;
						loadedColumns++;
					}
				}
			} catch (Exception ex) {
				_log.Debug($"Failed to load ModifiedOn from reader: {ex.Message}");
			}

			try {
				if (readerColumnMap.ContainsKey(EntityFields.CreatedOn)) {
					var createdOn = reader.GetColumnValue<DateTime>(EntityFields.CreatedOn);
					if (createdOn != default && createdOn != DateTime.MinValue) {
						dataRow.CreatedOn = createdOn;
						loadedColumns++;
					}
				}
			} catch (Exception ex) {
				_log.Debug($"Failed to load CreatedOn from reader: {ex.Message}");
			}

			if (loadedColumns == 0) {
				_log.Warn($"No columns loaded for data row in schema '{schemaName}'");
				return null;
			}

			return dataRow;
		}

		private List<LookupDataRow> ExecuteQueryAndBuildDataRows(
				Select query,
				string schemaName,
				int chunkSize,
				int? maxRecords) {
			var resultList = new List<LookupDataRow>();
			int offset = 0;
			int totalFetched = 0;
			int recordsToFetch = maxRecords ?? int.MaxValue;
			int consecutiveEmptyBatches = 0;
			const int maxConsecutiveEmptyBatches = 3;

			_log.Debug($"ExecuteQueryAndBuildDataRows START - Schema: {schemaName}, ChunkSize: {chunkSize}, MaxRecords: {maxRecords?.ToString() ?? "unlimited"}");

			while (totalFetched < recordsToFetch) {
				int batchSize = Math.Min(chunkSize, recordsToFetch - totalFetched);

				var batchQuery = (Select)query.Clone();
				batchQuery.OffsetFetch(offset, batchSize);

				_log.Debug($"Executing batch - Offset: {offset}, BatchSize: {batchSize}, TotalFetched: {totalFetched}");

				int rowsRead = 0;
				int validRows = 0;
				bool batchFailed = false;

				try {
					using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection()) {
						using (IDataReader reader = batchQuery.ExecuteReader(dbExecutor)) {
							var readerColumnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
							try {
								for (int i = 0; i < reader.FieldCount; i++) {
									string columnName = reader.GetName(i);
									if (!string.IsNullOrEmpty(columnName)) {
										readerColumnMap[columnName] = i;
									}
								}
							} catch (Exception ex) {
								_log.Error($"Failed to build reader column map for schema '{schemaName}' at offset {offset}: {ex.Message}", ex);
								batchFailed = true;
							}

							if (!batchFailed) {
								while (reader.Read()) {
									rowsRead++;
									try {
										LookupDataRow dataRow = ReadDataRowFromReader(reader, schemaName, readerColumnMap);
										if (dataRow != null) {
											resultList.Add(dataRow);
											validRows++;
											totalFetched++;

											if (totalFetched >= recordsToFetch) {
												_log.Debug($"Reached max records limit: {recordsToFetch}");
												break;
											}
										}
									} catch (Exception ex) {
										_log.Warn($"Failed to create data row from reader in schema '{schemaName}': {ex.Message}");
									}
								}
							}
						}
					}
				} catch (Exception ex) {
					_log.Error($"Failed to execute batch query for schema '{schemaName}' at offset {offset}: {ex.Message}", ex);
					batchFailed = true;
				}

				_log.Debug($"Batch complete - RowsRead: {rowsRead}, ValidRows: {validRows}, TotalFetched: {totalFetched}");

				if (batchFailed) {
					_log.Warn($"Batch failed, stopping execution");
					break;
				}

				if (validRows == 0) {
					consecutiveEmptyBatches++;
					if (consecutiveEmptyBatches >= maxConsecutiveEmptyBatches) {
						_log.Warn($"Stopping query execution after {consecutiveEmptyBatches} consecutive empty batches for schema '{schemaName}'");
						break;
					}
				} else {
					consecutiveEmptyBatches = 0;
				}

				if (rowsRead < batchSize) {
					_log.Debug($"Read fewer rows ({rowsRead}) than batch size ({batchSize}), stopping");
					break;
				}

				if (totalFetched >= recordsToFetch) {
					_log.Debug($"Reached total records limit: {recordsToFetch}");
					break;
				}

				offset += batchSize;
			}

			_log.Debug($"ExecuteQueryAndBuildDataRows COMPLETE - TotalFetched: {totalFetched}, ResultListCount: {resultList.Count}");
			return resultList;
		}

		private List<LookupDataRow> QueryDataInBatches(
				string schemaName,
				IReadOnlyList<Guid> ids = null,
				int? maxRecords = null,
				IReadOnlyList<string> fields = null,
				LookupQueryContext queryContext = LookupQueryContext.Default,
				Dictionary<string, Guid> equalityFilters = null) {
			// Fetch schema only for field validation in BuildSelectQuery
			EntitySchema schema = _userConnection.EntitySchemaManager.GetInstanceByName(schemaName);
			Select query = BuildSelectQuery(schemaName, fields, schema, queryContext);
			ApplyIdFilters(query, schemaName, ids);
			ApplyEqualityFilters(query, schemaName, equalityFilters);
			query.SpecifyNoLockHints();

			_log.Debug($"QueryDataInBatches - Schema: {schemaName}, ChunkSize: {ChunkSize}, MaxRecords: {maxRecords?.ToString() ?? "unlimited"}");

			return ExecuteQueryAndBuildDataRows(query, schemaName, ChunkSize, maxRecords);
		}

		private EntitySchema GetAssociatedSchema(LookupDataRow dataRow) {
			Guid schemaUId = Guid.Empty;

			if (dataRow.SchemaName == SysSchemaName) {
				schemaUId = dataRow.UId ?? Guid.Empty;
			} else {
				schemaUId = dataRow.SysEntitySchemaUId ?? Guid.Empty;
			}

			if (schemaUId.IsEmpty()) {
				schemaUId = dataRow.UId ?? Guid.Empty;
			}
			if (schemaUId.IsEmpty()) {
				schemaUId = dataRow.SysEntitySchemaUId ?? Guid.Empty;
			}

			EntitySchema schema = schemaUId.IsEmpty()
				? null
				: _userConnection.EntitySchemaManager.FindInstanceByUId(schemaUId);
			if (schema == null) {
				_log.Warn($"Associated schema not found for UId: {schemaUId}");
			}
			return schema;
		}

		private string TryGetName(LookupDataRow dataRow) {
			if (!string.IsNullOrWhiteSpace(dataRow.Caption)) {
				return dataRow.Caption;
			}
			if (!string.IsNullOrWhiteSpace(dataRow.Name)) {
				return dataRow.Name;
			}
			return null;
		}

		private string TryGetDescription(LookupDataRow dataRow, string name) {
			var desc = dataRow.Description;
			return string.IsNullOrWhiteSpace(desc) || string.Equals(name, desc.Trim(), StringComparison.InvariantCultureIgnoreCase)
				? null
				: desc.Trim();
		}

		private static bool IsSaneDate(DateTime dt) =>
			dt != default && dt != DateTime.MinValue && dt.Year >= 2000;

		private string TryGetModifiedOn(LookupDataRow dataRow) {
			if (dataRow.ModifiedOn.HasValue && IsSaneDate(dataRow.ModifiedOn.Value)) {
				return dataRow.ModifiedOn.Value.ToString("o");
			}
			if (dataRow.CreatedOn.HasValue && IsSaneDate(dataRow.CreatedOn.Value)) {
				return dataRow.CreatedOn.Value.ToString("o");
			}
			return null;
		}

		private (Guid Id, string Name, string Description, string ModifiedOn, string Checksum) ExtractData(LookupDataRow dataRow) {
			var id = dataRow.Id;
			var name = TryGetName(dataRow);
			var description = TryGetDescription(dataRow, name);
			var modifiedOn = TryGetModifiedOn(dataRow);
			var checksum = _checksumProvider.GetChecksum(name ?? string.Empty, description ?? string.Empty);
			return (id, name, description, modifiedOn, checksum);
		}

		private List<T> SelectValid<T>(IEnumerable<LookupDataRow> dataRows, Func<LookupDataRow, T> extractor)
			where T : class {
			return dataRows.Select(extractor).Where(x => x != null).ToList();
		}

		private bool TryExtractValid(LookupDataRow dataRow, out (Guid Id, string Name, string Description, string ModifiedOn, string Checksum) data) {
			data = ExtractData(dataRow);
			if (data.Id == Guid.Empty) {
				return false;
			}
			if (string.IsNullOrWhiteSpace(data.Name)) {
				return false;
			}
			if (string.IsNullOrWhiteSpace(data.ModifiedOn)) {
				return false;
			}
			return true;
		}

		private HashSet<Guid> GetVirtualSchemaUIds() {
			var result = new HashSet<Guid>();

			IEnumerable<ISchemaManagerItem<EntitySchema>> items;
			try {
				items = _userConnection.EntitySchemaManager.GetItems()
					?? Enumerable.Empty<ISchemaManagerItem<EntitySchema>>();
			} catch (Exception ex) {
				_log.Warn("failed to enumerate schema manager items.", ex);
				return result;
			}

			int skipped = 0;
			foreach (var mgrItem in items) {
				if (mgrItem == null) {
					continue;
				}
				try {
					var schema = mgrItem.Instance;
					if (schema != null && schema.IsVirtual && !schema.UId.IsEmpty()) {
						result.Add(schema.UId);
					}
				} catch (Exception ex) {
					skipped++;
					_log.Debug("skipped one schema item due to error.", ex);
				}
			}

			if (skipped > 0) {
				_log.Warn($"completed with {skipped} skipped items; collected {result.Count} virtual schema UIds.");
			}
			return result;
		}

		private List<LookupDataRow> ApplyVirtualSchemaFilter(List<LookupDataRow> dataRows) {
			if (dataRows == null || dataRows.Count == 0) {
				return dataRows;
			}

			if (dataRows[0].SchemaName == SysSchemaName) {
				return dataRows.Where(row => row.UId.HasValue && !VirtualSchemaUIds.Contains(row.UId.Value)).ToList();
			}

			return dataRows;
		}

		private HashSet<Guid> GetUnlimitedLookupIdsSet() {
			var result = new HashSet<Guid>();

			var sysSchemaRows = QueryDataInBatches(
				SysSchemaName,
				fields: new[] { EntityFields.Id },
				queryContext: LookupQueryContext.SysSchemaLookupUnlimited);

			foreach (var row in sysSchemaRows) {
				if (row.Id != Guid.Empty && (row.UId == null || !VirtualSchemaUIds.Contains(row.UId.Value))) {
					result.Add(row.Id);
				}
			}

			var lookupRows = QueryDataInBatches(
				LookupSchemaName,
				fields: new[] { EntityFields.Id },
				queryContext: LookupQueryContext.LookupUnlimited);

			foreach (var row in lookupRows) {
				if (row.Id != Guid.Empty) {
					result.Add(row.Id);
				}
			}

			return result;
		}

		private static bool IsDerivedFromLookupFamily(EntitySchema schema) {
			var s = schema;
			while (s != null) {
				if (LookupSchemas.Contains(s.Name, StringComparer.InvariantCultureIgnoreCase)) {
					return true;
				}
				s = s.ParentSchema;
			}
			return false;
		}

		private LookupDefinition GetLookupDefinition(LookupDataRow dataRow) {
			if (!TryExtractValid(dataRow, out var d)) {
				return null;
			}

			var definition = new LookupDefinition {
				Id = d.Id,
				Name = d.Name,
				Description = d.Description,
				ModifiedOn = d.ModifiedOn,
				Checksum = d.Checksum
			};

			EntitySchema associated = GetAssociatedSchema(dataRow);
			if (associated != null && !associated.IsVirtual) {
				definition.ValuesSchemaName = associated.Name;
				definition.ValuesSchemaUId = associated.UId;
			}

			return definition;
		}

		private LookupValueDefinition GetLookupValueDefinition(LookupDataRow dataRow, Guid schemaUId) {
			if (!TryExtractValid(dataRow, out var d)) {
				return null;
			}
			return new LookupValueDefinition {
				Id = d.Id,
				Name = d.Name,
				Description = d.Description,
				ModifiedOn = d.ModifiedOn,
				Checksum = d.Checksum,
				SchemaUId = schemaUId
			};
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public bool IsFromExcludedSchema(Entity entity) {
			return !string.IsNullOrWhiteSpace(entity?.SchemaName)
				&& entity.SchemaName == DFSyncExceptionsSchemaName;
		}

		/// <inheritdoc/>
		public bool IsLookup(Entity entity) {
			if (entity?.Schema?.Name == LookupSchemaName) {
				return true;
			}
			if (entity?.Schema?.Name == SysSchemaName) {
				try {
					var uId = entity.GetTypedColumnValue<Guid>(EntityFields.UId);
					var schema = _userConnection.EntitySchemaManager.FindInstanceByUId(uId);
					return IsDerivedFromLookupFamily(schema);
				} catch {
					return false;
				}
			}
			return false;
		}

		/// <inheritdoc/>
		public bool IsLookupValue(Entity entity) => entity != null
			&& entity.Schema?.ParentSchema != null
			&& LookupSchemas.Contains(entity.Schema.ParentSchema.Name);



		/// <inheritdoc/>
		public LookupSummary MapToLookupSummary(LookupDefinition def) {
			if (def == null) {
				return null;
			}
			return new LookupSummary {
				Id = def.Id,
				ModifiedOn = def.ModifiedOn,
				Checksum = def.Checksum,
				ValuesSchemaUId = def.ValuesSchemaUId
			};
		}

		/// <inheritdoc/>
		public LookupValueSummary MapToLookupValueSummary(LookupValueDefinition def) {
			if (def == null) {
				return null;
			}
			return new LookupValueSummary {
				Id = def.Id,
				SchemaUId = def.SchemaUId,
				ModifiedOn = def.ModifiedOn,
				Checksum = def.Checksum
			};
		}

		/// <inheritdoc/>
		public List<LookupDefinition> GetLookupDefinitions(IReadOnlyList<Guid> ids = null) {
			List<LookupDataRow> sysSchemas = QueryDataInBatches(
				SysSchemaName,
				ids: ids,
				fields: SysSchemaLookupFields,
				queryContext: LookupQueryContext.SysSchemaLookupAllowed);

			sysSchemas = ApplyVirtualSchemaFilter(sysSchemas);

			List<LookupDataRow> lookupRows = QueryDataInBatches(
				LookupSchemaName,
				ids: ids,
				fields: LookupSchemaFields,
				queryContext: LookupQueryContext.LookupAllowed);

			List<LookupDefinition> defs = SelectValid(sysSchemas.Concat(lookupRows), GetLookupDefinition);

			return defs;
		}

		/// <inheritdoc/>
		public List<LookupDefinition> GetLookupDefinitionsForValue(Guid valuesSchemaUId) {
			List<LookupDataRow> sysSchemas = QueryDataInBatches(
				SysSchemaName,
				fields: SysSchemaLookupFields,
				queryContext: LookupQueryContext.SysSchemaLookupAllowed,
				equalityFilters: new Dictionary<string, Guid> { { EntityFields.UId, valuesSchemaUId } });

			sysSchemas = ApplyVirtualSchemaFilter(sysSchemas);

			List<LookupDataRow> lookupRows = QueryDataInBatches(
				LookupSchemaName,
				fields: LookupSchemaFields,
				queryContext: LookupQueryContext.LookupAllowed,
				equalityFilters: new Dictionary<string, Guid> { { EntityFields.SysEntitySchemaUId, valuesSchemaUId } });

			var all = sysSchemas.Concat(lookupRows);
			return SelectValid(all, GetLookupDefinition);
		}

		/// <inheritdoc/>
		public List<LookupValueDefinition> GetLookupValueDefinitionsForLookups(IReadOnlyList<LookupDefinition> lookupDefinitions) {
			var lookupValueDefinitions = new List<LookupValueDefinition>();
			var processedSchemas = new HashSet<Guid>();

			var unlimitedLookupIds = GetUnlimitedLookupIdsSet();

			foreach (LookupDefinition lookupDefinition in lookupDefinitions) {
				if (string.IsNullOrWhiteSpace(lookupDefinition.ValuesSchemaName) || lookupDefinition.ValuesSchemaUId.IsEmpty() || processedSchemas.Contains(lookupDefinition.ValuesSchemaUId)) {
					continue;
				}

				bool isUnlimitedLookup = unlimitedLookupIds.Contains(lookupDefinition.Id);
				int? rowCountLimit = isUnlimitedLookup ? (int?)null : EffectiveLimit;

				try {
					List<LookupDataRow> dataRows = QueryDataInBatches(lookupDefinition.ValuesSchemaName, maxRecords: rowCountLimit, fields: LookupValueFields);
					Guid schemaUId = lookupDefinition.ValuesSchemaUId;
					var values = SelectValid(dataRows, dataRow => GetLookupValueDefinition(dataRow, schemaUId));
					lookupValueDefinitions.AddRange(values);
					processedSchemas.Add(lookupDefinition.ValuesSchemaUId);
				} catch (Exception e) {
					_log.Error("Failed to retrieve lookup values for schema: " + lookupDefinition.ValuesSchemaName, e);
				}
			}

			return lookupValueDefinitions;
		}

		#endregion

	}

	#endregion

}

