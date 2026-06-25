namespace Terrasoft.Configuration.DataForge
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using Common;
	using Core;
	using Core.Entities;
	using Core.Factories;
	using Core.ServiceModelContract;

	#region Interface: ITableColumnsDetailsProvider

	/// <summary>
	/// Provides methods for retrieving table column details from local entity schema metadata.
	/// </summary>
	public interface ITableColumnsDetailsProvider
	{
		/// <summary>
		/// Retrieves column details for the specified table.
		/// </summary>
		/// <param name="tableName">The name of the table to retrieve column details for.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetTableColumnsDetailsResponse"/> containing column details.</returns>
		DataForgeGetTableColumnsDetailsResponse GetTableColumnsDetails(string tableName, CancellationToken cancellationToken = default);
	}

	#endregion

	#region Class: TableColumnsDetailsProvider

	/// <summary>
	/// Provides table column details from local entity schema metadata.
	/// </summary>
	[DefaultBinding(typeof(ITableColumnsDetailsProvider))]
	public class TableColumnsDetailsProvider : ITableColumnsDetailsProvider
	{
		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="TableColumnsDetailsProvider"/> class.
		/// </summary>
		/// <param name="userConnection">User connection instance.</param>
		public TableColumnsDetailsProvider(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Builds the list of column details from an entity schema.
		/// </summary>
		/// <param name="entitySchema">The entity schema to extract column details from.</param>
		/// <returns>A list of <see cref="TableColumnDetails"/>.</returns>
		private List<TableColumnDetails> BuildColumnsList(EntitySchema entitySchema) {
			var columnsList = new List<TableColumnDetails>();
			foreach (var column in entitySchema.Columns) {
				var columnDetails = new TableColumnDetails {
					ColumnName = column.ColumnValueName,
					ColumnType = column.DataValueType.Name,
					ColumnRequired = column.RequirementType != EntitySchemaColumnRequirementType.None && !column.HasDefValue
				};
				if (IsMeaningful(column.Caption, column.ColumnValueName)) {
					columnDetails.ColumnCaption = column.Caption.ToString();
				}
				if (!string.IsNullOrWhiteSpace(column.Description) && !string.Equals(column.ColumnValueName,
						column.Description.ToString().Replace(" ", string.Empty),
						StringComparison.InvariantCultureIgnoreCase)) {
					columnDetails.ColumnDescription = column.Description.ToString();
				}
				if (column.ReferenceSchema != null && !string.IsNullOrWhiteSpace(column.ReferenceSchema.Name)) {
					columnDetails.ColumnRefersToTable = column.ReferenceSchema.Name;
				}
				columnsList.Add(columnDetails);
			}
			return columnsList;
		}

		/// <summary>
		/// Determines if a property value is meaningful compared to a reference string.
		/// </summary>
		/// <param name="propertyValue">The property value to check.</param>
		/// <param name="reference">The reference string to compare against.</param>
		/// <returns>True if the property value is meaningful; otherwise, false.</returns>
		private bool IsMeaningful(object propertyValue, string reference) {
			if (propertyValue == null) {
				return false;
			}
			string valueStr = propertyValue.ToString();
			if (string.IsNullOrWhiteSpace(valueStr)) {
				return false;
			}
			return !string.Equals(valueStr, reference, StringComparison.InvariantCultureIgnoreCase) &&
				!string.Equals(reference, valueStr.Replace(" ", string.Empty),
					StringComparison.InvariantCultureIgnoreCase);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Retrieves column details for the specified table.
		/// </summary>
		/// <param name="tableName">The name of the table to retrieve column details for.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetTableColumnsDetailsResponse"/> containing column details.</returns>
		public DataForgeGetTableColumnsDetailsResponse GetTableColumnsDetails(string tableName, CancellationToken cancellationToken = default) {
			try {
				var entitySchema = _userConnection.EntitySchemaManager.GetInstanceByName(tableName);
				var columns = BuildColumnsList(entitySchema);
				return new DataForgeGetTableColumnsDetailsResponse {
					Success = true,
					Data = new TableColumnsDetailsData {
						TableName = tableName,
						Columns = columns
					}
				};
			} catch (ItemNotFoundException) {
				return new DataForgeGetTableColumnsDetailsResponse {
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = "EntitySchemaNotFound",
						Message = $"Entity schema '{tableName}' was not found."
					}
				};
			} catch (Exception ex) {
				return new DataForgeGetTableColumnsDetailsResponse {
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = "UnexpectedError",
						Message = $"Unexpected error: {ex.Message}"
					}
				};
			}
		}

		#endregion
	}

	#endregion
}

