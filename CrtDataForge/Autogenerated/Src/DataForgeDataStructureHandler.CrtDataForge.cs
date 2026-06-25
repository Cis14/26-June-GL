namespace Terrasoft.Configuration.DataForge
{
	using global::Common.Logging;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Interface: IDataStructureHandler

	/// <summary>
	/// Provides functionality for retrieving structured and summarized metadata representations of entity schemas.
	/// </summary>
	public interface IDataStructureHandler
	{
		/// <summary>
		/// Retrieves detailed metadata definitions for the specified entity schemas.
		/// </summary>
		/// <param name="skipSimilarToName">If true, omits captions or descriptions that match column or table names.</param>
		/// <param name="tryMapSqlTypes">If true, attempts to map internal types to SQL-compatible types.</param>
		/// <param name="items">The entity schema items to process.</param>
		/// <returns>A list of <see cref="TableDefinition"/> objects representing table metadata.</returns>
		List<TableDefinition> GetTableDefinitions(
			bool skipSimilarToName = true,
			bool tryMapSqlTypes = false,
			params ISchemaManagerItem<EntitySchema>[] items);

		/// <summary>
		/// Retrieves detailed metadata definitions for the specified entity schemas.
		/// </summary>
		/// <param name="skipSimilarToName">If true, omits captions or descriptions that match column or table names.</param>
		/// <param name="tryMapSqlTypes">If true, attempts to map internal types to SQL-compatible types.</param>
		/// <param name="items">A collection of schema manager items to process. If null, all schemas will be used.</param>
		/// <returns>A list of <see cref="TableDefinition"/> objects representing table metadata.</returns>
		List<TableDefinition> GetTableDefinitions(
			bool skipSimilarToName = true,
			bool tryMapSqlTypes = false,
			IEnumerable<ISchemaManagerItem<EntitySchema>> items = null);

		/// <summary>
		/// Retrieves simplified summaries for the specified entity schemas.
		/// </summary>
		/// <param name="items">The schema manager items to summarize.</param>
		/// <returns>A list of <see cref="TableSummary"/> objects representing schema summaries.</returns>
		List<TableSummary> GetTableSummaries(params ISchemaManagerItem<EntitySchema>[] items);

		/// <summary>
		/// Retrieves simplified summaries for the specified entity schemas.
		/// </summary>
		/// <param name="items">A collection of schema manager items to summarize. If null, all schemas will be used.</param>
		/// <returns>A list of <see cref="TableSummary"/> objects representing schema summaries.</returns>
		List<TableSummary> GetTableSummaries(IEnumerable<ISchemaManagerItem<EntitySchema>> items = null);

		/// <summary>
		/// Retrieves detailed metadata definition for a single entity schema.
		/// </summary>
		/// <param name="item">The entity schema item to process.</param>
		/// <param name="skipSimilarToName">If true, omits captions or descriptions that match column or table names.</param>
		/// <param name="tryMapSqlTypes">If true, attempts to map internal types to SQL-compatible types.</param>
		/// <returns>A <see cref="TableDefinition"/> object representing table metadata, or null if the schema is invalid or not processed.</returns>
		TableDefinition GetTableDefinition(
			ISchemaManagerItem<EntitySchema> item,
			bool skipSimilarToName = true,
			bool tryMapSqlTypes = false);

	}

	#endregion

	#region Class: DataStructureHandler

	[DefaultBinding(typeof(IDataStructureHandler))]
	internal class DataStructureHandler : IDataStructureHandler
	{

		#region Constants: Private

		private const string NameColumnAlias = "Name";
		private const string ModifiedOnColumnAlias = "ModifiedOn";

		#endregion

		#region Fields: Private

		private static readonly ILog _log = LogManager.GetLogger("DataForge");
		private readonly ISchemaChecksumProvider _checksumProvider;
		private readonly ITableMetadataBuilder _tableMetadataBuilder;
		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="DataStructureHandler"/> class with the specified dependencies.
		/// </summary>
		/// <param name="userConnection">The user connection context for database and user-specific operations.</param>
		/// <param name="checksumProvider">The provider responsible for retrieving schema checksums.</param>
		/// <param name="tableMetadataBuilder">The builder used to construct table metadata structures.</param>
		public DataStructureHandler(
			UserConnection userConnection,
			ISchemaChecksumProvider checksumProvider,
			ITableMetadataBuilder tableMetadataBuilder) {
			_userConnection = userConnection;
			_checksumProvider = checksumProvider;
			_tableMetadataBuilder = tableMetadataBuilder;
		}

		#endregion

		#region Methods: Private

		private List<T> ProcessSchemas<T>(
			IEnumerable<ISchemaManagerItem<EntitySchema>> items,
			Func<EntitySchema, string, DateTime, T> builderFunc,
			string errorPrefix) {

			List<EntitySchema> schemas = (items?.Any() == true
					? items
					: _userConnection.EntitySchemaManager.GetItems())
				.Select(i => i.SafeInstance)
				.Where(s => s != null && !s.IsVirtual)
				.ToList();

			var modifiedOnSelect = (Select)new Select(_userConnection)
				.Column("SysSchema", "Name").As(NameColumnAlias)
				.Column(Func.Max("SysSchema", "ModifiedOn")).As(ModifiedOnColumnAlias)
				.From("SysSchema")
				.GroupBy("SysSchema", "Name");
			modifiedOnSelect.SpecifyNoLockHints();
			var modifiedOnMap = new Dictionary<string, DateTime>();
			modifiedOnSelect.ExecuteReader(reader => {
				string name = reader.GetColumnValue<string>(NameColumnAlias);
				DateTime modifiedOn = DateTime.SpecifyKind(reader.GetColumnValue<DateTime>(ModifiedOnColumnAlias), DateTimeKind.Utc);
				modifiedOnMap[name] = modifiedOn;
			});

			Dictionary<string, string> checksums = _checksumProvider.GetChecksums();
			var result = new List<T>();

			foreach (EntitySchema schema in schemas) {
				try {
					if (!checksums.TryGetValue(schema.Name, out string checksum)) {
						_log.WarnFormat("No checksum found for schema: {0}.", schema.Name);
					}
					if (!modifiedOnMap.TryGetValue(schema.Name, out DateTime modifiedOn)) {
						_log.WarnFormat("No 'modifiedOn' timestamp found for schema: {0}.", schema.Name);
					}
					result.Add(builderFunc(schema, checksum, modifiedOn));
				} catch (Exception ex) {
					_log.Error($"{errorPrefix} {schema.Name}", ex);
				}
			}

			return result;
		}

		private List<TableDefinition> GetTableDefinitionsCore(
			bool skipSimilarToName,
			bool tryMapSqlTypes,
			IEnumerable<ISchemaManagerItem<EntitySchema>> items) {
			return ProcessSchemas(items,
				(schema, checksum, modifiedOn) => _tableMetadataBuilder.BuildDefinition(schema, checksum, modifiedOn,
					skipSimilarToName, tryMapSqlTypes), "Error processing table");
		}

		private List<TableSummary> GetTableSummariesCore(
			IEnumerable<ISchemaManagerItem<EntitySchema>> items) {

			return ProcessSchemas(items,
				(schema, checksum, modifiedOn) => _tableMetadataBuilder.BuildSummary(schema, checksum, modifiedOn),
				"Error building summary for");
		}


		#endregion

		#region Methods: Public

		/// <summary>
		/// Retrieves detailed table definitions for the given entity schemas.
		/// </summary>
		/// <param name="skipSimilarToName">If true, omits redundant captions or descriptions.</param>
		/// <param name="tryMapSqlTypes">If true, maps internal types to SQL-compatible types.</param>
		/// <param name="items">The schema items to process.</param>
		/// <returns>A list of <see cref="TableDefinition"/> objects with complete metadata.</returns>
		public List<TableDefinition> GetTableDefinitions(
				bool skipSimilarToName = true,
				bool tryMapSqlTypes = false,
				params ISchemaManagerItem<EntitySchema>[] items) =>
			GetTableDefinitionsCore(skipSimilarToName, tryMapSqlTypes, items);

		/// <summary>
		/// Retrieves detailed table definitions for the given entity schemas.
		/// </summary>
		/// <param name="skipSimilarToName">If true, omits redundant captions or descriptions.</param>
		/// <param name="tryMapSqlTypes">If true, maps internal types to SQL-compatible types.</param>
		/// <param name="items">A collection of schema manager items to process. If null, all schemas will be included.</param>
		/// <returns>A list of <see cref="TableDefinition"/> objects with complete metadata.</returns>
		public List<TableDefinition> GetTableDefinitions(
				bool skipSimilarToName = true,
				bool tryMapSqlTypes = false,
				IEnumerable<ISchemaManagerItem<EntitySchema>> items = null) =>
			GetTableDefinitionsCore(skipSimilarToName, tryMapSqlTypes, items);

		/// <summary>
		/// Retrieves summarized metadata for the given entity schemas.
		/// </summary>
		/// <param name="items">The schema items to summarize.</param>
		/// <returns>A list of <see cref="TableSummary"/> objects representing schema summaries.</returns>
		public List<TableSummary> GetTableSummaries(params ISchemaManagerItem<EntitySchema>[] items) =>
			GetTableSummariesCore(items);

		/// <summary>
		/// Retrieves summarized metadata for the given entity schemas.
		/// </summary>
		/// <param name="items">A collection of schema manager items to summarize. If null, all schemas will be used.</param>
		/// <returns>A list of <see cref="TableSummary"/> objects representing schema summaries.</returns>
		public List<TableSummary> GetTableSummaries(IEnumerable<ISchemaManagerItem<EntitySchema>> items = null) =>
			GetTableSummariesCore(items);

		/// <summary>
		/// Retrieves detailed metadata definition for a single entity schema.
		/// </summary>
		/// <param name="item">The entity schema item to process.</param>
		/// <param name="skipSimilarToName">If true, omits captions or descriptions that match column or table names.</param>
		/// <param name="tryMapSqlTypes">If true, attempts to map internal types to SQL-compatible types.</param>
		/// <returns>A <see cref="TableDefinition"/> object representing table metadata, or null if the schema is invalid or not processed.</returns>
		public TableDefinition GetTableDefinition(
			ISchemaManagerItem<EntitySchema> item,
			bool skipSimilarToName = true,
			bool tryMapSqlTypes = false) =>
			 item == null
				? null
				: GetTableDefinitions(skipSimilarToName, tryMapSqlTypes, item).FirstOrDefault();

		#endregion
	}

	#endregion
}

