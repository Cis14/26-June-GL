namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: BaseColumnProcessor

	/// <summary>
	/// Column processor base class.
	/// </summary>
	public abstract class BaseColumnProcessor : IFileImportColumnProcessError, IFileImportCreateColumns
	{

		#region Constants: Protected

		protected const string TypeUIdPropertyName = "TypeUId";
		protected const string ReferenceSchemaUIdPropertyName = "ReferenceSchemaUId";

		#endregion

		#region  Fields: Private

		private string _columnProcessErrorMessage;

		#endregion

		#region Constructors: Protected

		/// <summary>
		/// Creates instance of type <see cref="BaseColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		protected BaseColumnProcessor(UserConnection userConnection) => UserConnection = userConnection;

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Column process error message.
		/// </summary>
		protected string ColumnProcessErrorMessage {
			get {
				if (_columnProcessErrorMessage != null) {
					return _columnProcessErrorMessage;
				}
				IResourceStorage storage = UserConnection.Workspace.ResourceStorage;
				_columnProcessErrorMessage = new LocalizableString(storage, "BaseColumnProcessor",
						"LocalizableStrings.ColumnProcessErrorMessage.Value");
				return _columnProcessErrorMessage;
			}
		}

		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected abstract Guid DataValueTypeUId { get; }

		/// <summary>
		/// User connection.
		/// </summary>
		protected UserConnection UserConnection { get; }

		#endregion

		#region Events: Public

		public event EventHandler<ColumnProcessErrorEventArgs> ProcessError;

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Creates column.
		/// </summary>
		/// <param name="columnValue">Column value information.</param>
		/// <returns>Import column.</returns>
		protected ImportColumn CreateColumn(ImportColumnValue columnValue) => new ImportColumn {
				Index = columnValue.ColumnIndex,
				Source = columnValue.Value
		};

		/// <summary>
		/// Creates column destination.
		/// </summary>
		/// <param name="entitySchema">Entity schema.</param>
		/// <param name="entitySchemaColumn">Entity schema column.</param>
		/// <param name="columnValue">Column value information.</param>
		/// <returns>Import column destination.</returns>
		protected ImportColumnDestination CreateColumnDestination(EntitySchema entitySchema,
				EntitySchemaColumn entitySchemaColumn, ImportColumnValue columnValue) => new ImportColumnDestination {
				IsKey = GetIsPrimaryDisplayColumn(entitySchema, entitySchemaColumn),
				SchemaUId = entitySchema.UId,
				ColumnName = entitySchemaColumn.Name,
				ColumnValueName = entitySchemaColumn.ColumnValueName,
				Properties = GetColumnDestinationProperties(entitySchemaColumn, columnValue)
		};

		/// <summary>
		/// Finds column destination type unique identifier.
		/// </summary>
		/// <param name="destination">Column destination.</param>
		/// <returns>Type unique identifier.</returns>
		protected Guid FindColumnTypeUId(ImportColumnDestination destination) => destination.FindColumnTypeUId();

		/// <summary>
		/// Gets column destination default properties.
		/// </summary>
		/// <param name="entitySchemaColumn">Entity schema column.</param>
		/// <param name="columnValue">Column value information.</param>
		/// <returns>Column destination default properties.</returns>
		protected virtual Dictionary<string, object> GetColumnDestinationProperties(
				EntitySchemaColumn entitySchemaColumn, ImportColumnValue columnValue) =>
				new Dictionary<string, object> {
						{ TypeUIdPropertyName, DataValueTypeUId }
				};

		/// <summary>
		/// Determines if entity schema column is primary display column.
		/// </summary>
		/// <param name="entitySchema">Entity schema.</param>
		/// <param name="entitySchemaColumn">Entity schema column.</param>
		/// <returns><c>true</c>, if entity schema column is primary display column.</returns>
		protected bool GetIsPrimaryDisplayColumn(EntitySchema entitySchema, EntitySchemaColumn entitySchemaColumn) {
			var isPrimaryDisplayColumn = false;
			string primaryDisplayColumnName = entitySchema.FindPrimaryDisplayColumnName();
			if (primaryDisplayColumnName.IsNotNullOrEmpty()) {
				isPrimaryDisplayColumn = primaryDisplayColumnName.Equals(entitySchemaColumn.Name);
			}
			return isPrimaryDisplayColumn;
		}

		/// <summary>
		/// Sends event if converted value is null
		/// </summary>
		/// <param name="cellReference"></param>
		/// <param name="message"></param>
		protected void SendProcessError(string cellReference, string message) {
			var eventArgs = new ColumnProcessErrorEventArgs {
					CellReference = cellReference,
					Message = message
			};
			ProcessError?.Invoke(this, eventArgs);
		}

		/// <summary>
		/// Sends event if converted value is null
		/// </summary>
		/// <param name="columnValue"></param>
		protected void SendProcessError(ImportColumnValue columnValue) {
			SendProcessError($"{columnValue.ColumnIndex}{columnValue.RowIndex}", ColumnProcessErrorMessage);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Determines if entity schema column can be processed.
		/// </summary>
		/// <param name="entitySchemaColumn">Entity schema column</param>
		/// <returns><c>true</c>, if processing is possible.</returns>
		public virtual bool CanProcess(EntitySchemaColumn entitySchemaColumn) =>
				DataValueTypeUId.Equals(entitySchemaColumn.DataValueType.UId);

		/// <summary>
		/// Determines if column destination can be processed.
		/// </summary>
		/// <param name="destination">Column destination.</param>
		/// <returns><c>true</c>, if processing is possible.</returns>
		public bool CanProcess(ImportColumnDestination destination) {
			Guid columnTypeUId = FindColumnTypeUId(destination);
			return columnTypeUId.Equals(DataValueTypeUId);
		}

		/// <summary>
		/// Creates column.
		/// </summary>
		/// <param name="entitySchema">Entity schema.</param>
		/// <param name="entitySchemaColumn">Entity schema column.</param>
		/// <param name="columnValue">Column value information.</param>
		/// <returns>Import column.</returns>
		public ImportColumn CreateColumn(EntitySchema entitySchema, EntitySchemaColumn entitySchemaColumn,
				ImportColumnValue columnValue) {
			ImportColumn column = CreateColumn(columnValue);
			ImportColumnDestination destination = CreateColumnDestination(entitySchema, entitySchemaColumn,
					columnValue);
			column.Destinations.Add(destination);
			return column;
		}

		#endregion

	}

	#endregion

}

