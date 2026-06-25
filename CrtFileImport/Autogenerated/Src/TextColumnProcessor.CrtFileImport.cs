namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: TextColumnProcessor

	/// <inheritdoc cref="BaseColumnProcessor"/>
	/// <summary>
	/// An instance of this class is responsible for processing Text column values.
	/// </summary>
	[DefaultBinding(typeof(IColumnProcessor), Name = nameof(TextColumnProcessor))]
	public class TextColumnProcessor : BaseColumnProcessor, IColumnProcessor
	{

		#region Constructors: Public

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Creates instance of type <see cref="TextColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public TextColumnProcessor(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.TextDataValueTypeUId;

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="BaseColumnProcessor"/>
		/// <summary>
		/// Finds processed value.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <param name="columnValue">Import column value.</param>
		/// <returns>Processed value.</returns>
		public object FindValueForSave(ImportColumnDestination destination, ImportColumnValue columnValue) {
			EntitySchema entitySchema = UserConnection.EntitySchemaManager.GetInstanceByUId(destination.SchemaUId);
			EntitySchemaColumn column = entitySchema.Columns.GetByName(destination.ColumnName);
			return TextColumnHelper.PrepareTextColumnValue((TextDataValueType)column.DataValueType, columnValue.Value);
		}

		/// <inheritdoc cref="IColumnProcessor"/>
		public void Process() { }

		/// <inheritdoc cref="IColumnProcessor"/>
		public void Add(ImportEntity importEntity, ImportColumn column, ImportColumnValue columnValue, ImportColumnDestination destination) { }


		#endregion

	}

	#endregion

}
