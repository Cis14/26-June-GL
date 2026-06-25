namespace Terrasoft.Configuration.FileImport
{
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: BaseFileImportHeadersProcessor

	/// <summary>
	/// Provides base class for process headers column.
	/// </summary>
	public abstract class BaseFileImportHeadersCreator : IFileImportHeadersCreator
	{

		#region Fields: Private

		private IColumnsAggregatorFactory _columnsAggregatorFactory;
		private IBaseColumnsAggregator _columnsProcessor;

		#endregion

		#region Properties: Private

		private IBaseColumnsAggregator ColumnsProcessor =>
			_columnsProcessor ?? (_columnsProcessor = ColumnsAggregatorFactory?.GetColumnsAggregator(UserConnection));

		private IColumnsAggregatorFactory ColumnsAggregatorFactory =>
			_columnsAggregatorFactory ?? (_columnsAggregatorFactory = ClassFactory.Get<IColumnsAggregatorFactory>());

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Instance of <see cref="UserConnection"/>
		/// </summary>
		protected UserConnection UserConnection { get; }

		#endregion

		#region Constructors: Protected

		protected BaseFileImportHeadersCreator(UserConnection userConnection) => UserConnection = userConnection;

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Find 
		/// </summary>
		/// <param name="entityColumns"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		protected abstract EntitySchemaColumn FindColumn(IEnumerable<EntitySchemaColumn> entityColumns, string value);

		/// <summary>
		/// Create column
		/// </summary>
		/// <param name="rootSchema">Source schema <see cref="EntitySchema"/>.</param>
		/// <param name="entitySchemaColumn">Column of source schema<see cref="EntitySchemaColumn"/>.</param>
		/// <param name="columnValue">Processed value from file <see cref="ImportColumnValue"/>.</param>
		/// <returns></returns>
		protected virtual ImportColumn CreateImportColumn(EntitySchema rootSchema, ImportColumnValue columnValue) {
			var entitySchemaColumn = FindColumn(rootSchema.Columns, columnValue.Value);
			return ColumnsProcessor.CreateColumn(rootSchema, entitySchemaColumn, columnValue);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// <inheritdoc cref="IFileImportHeadersCreator"/>
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ImportColumn> CreateHeaderColumns(EntitySchema rootSchema,
			IEnumerable<ImportColumnValue> columnValues) {
			ICollection<ImportColumn> columns = new List<ImportColumn>(columnValues.Count());
			foreach(ImportColumnValue columnValue in columnValues) {
				ImportColumn column = CreateImportColumn(rootSchema, columnValue);
				columns.Add(column);
			}
			return columns;
		}

		#endregion

	}

	#endregion

}

