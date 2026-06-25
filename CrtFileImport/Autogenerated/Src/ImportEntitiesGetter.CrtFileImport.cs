namespace Terrasoft.Configuration.FileImport
{
	using Common;
	using Core;
	using Core.Configuration;
	using Core.Entities;

	#region  Class: ImportEntitiesGetter

	/// <summary>
	/// Import entities getter base class.
	/// </summary>
	public abstract class ImportEntitiesGetter
	{
		#region Constants: Private

		private const string MaxQueryParametersCountSysSettingsName = "FileImportMaxQueryParametersCount";

		#endregion

		#region  Fields: Private

		private bool _isMaxQueryParametersCountInitialized;

		private int _maxQueryParametersCount;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="ImportEntitiesGetter" />.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="columnsProcessor">Columns processor.</param>
		public ImportEntitiesGetter(UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor) {
			UserConnection = userConnection;
			ColumnsProcessor = columnsProcessor;
		}

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Columns processor.
		/// </summary>
		protected IColumnsAggregatorAdapter ColumnsProcessor { get; }

		/// <summary>
		/// Max query parameters count.
		/// </summary>
		protected int MaxQueryParametersCount {
			get {
				if (_isMaxQueryParametersCountInitialized) {
					return _maxQueryParametersCount;
				}
				_maxQueryParametersCount = SysSettings.GetValue(UserConnection,
						MaxQueryParametersCountSysSettingsName, FileImporterConstants.MaxQueryParametersCount);
				_isMaxQueryParametersCountInitialized = true;
				return _maxQueryParametersCount;
			}
		}

		/// <summary>
		/// User connection.
		/// </summary>
		protected UserConnection UserConnection { get; }

		protected virtual int MaxEntityRowCount => UserConnection.AppConnection.MaxEntityRowCount;

		#endregion

		#region Methods: Private

		private EntitySchemaQuery CreateEntitiesSearchQueryInternal(EntitySchema rootSchema) {
			var esq = new EntitySchemaQuery(rootSchema);
			ApplyEntitiesSearchQueryOptions(esq);
			esq.UseAdminRights = false;
			return esq;
		}

		private void AddEntitiesSearchQueryColumnsByParameters(EntitySchemaQuery esq, ImportParameters importParameters) {
			AddCreatedOnColumn(esq);
			AddEntitySchemaQueryColumns(esq, importParameters);
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Adds CreatedOn column.
		/// </summary>
		/// <param name="esq">Entity schema query.</param>
		protected void AddCreatedOnColumn(EntitySchemaQuery esq) {
			var createdOnColumn = esq.RootSchema.CreatedOnColumn;
			if (createdOnColumn != null) {
				var queryColumn = esq.AddColumn(createdOnColumn.Name);
				queryColumn.OrderByAsc();
			}
		}

		/// <summary>
		/// Creates existing entities search query.
		/// </summary>
		/// <param name="rootSchema">Root schema.</param>
		/// <returns>Existing entities search query.</returns>
		protected EntitySchemaQuery CreateEntitiesSearchQuery(EntitySchema rootSchema) {
			var esq = CreateEntitiesSearchQueryInternal(rootSchema);
			AddEntitiesSearchQueryColumns(esq);
			return esq;
		}

		protected EntitySchemaQuery CreateEntitiesSearchQuery(EntitySchema rootSchema, ImportParameters importParameters) {
			var esq = CreateEntitiesSearchQueryInternal(rootSchema);
			AddEntitiesSearchQueryColumnsByParameters(esq, importParameters);
			return esq;
		}

		protected virtual void AddEntitiesSearchQueryColumns(EntitySchemaQuery esq) {
			AddCreatedOnColumn(esq);
			AddAllSchemaColumns(esq);
		}

		protected virtual void AddEntitySchemaQueryColumns(EntitySchemaQuery esq, ImportParameters importParameters) {
		}

		protected void AddAllSchemaColumns(EntitySchemaQuery esq) {
			esq.AddAllSchemaColumns();
		}

		protected virtual void ApplyEntitiesSearchQueryOptions(EntitySchemaQuery esq) {
			esq.RowCount = MaxEntityRowCount;
			esq.Filters.LogicalOperation = LogicalOperationStrict.Or;
		}

		#endregion
	}

	#endregion
}

