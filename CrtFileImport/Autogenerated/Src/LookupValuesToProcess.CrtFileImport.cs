namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;
	using Common;
	using Core;
	using Newtonsoft.Json;

	#region  Class: LookupCell

	/// <summary>
	/// An instance of this class contains information about cell.
	/// </summary>
	public class LookupCell
	{
		#region Constants: Private

		[JsonProperty]
		private string _columnIndex;

		[JsonProperty]
		private readonly uint _rowIndex;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="LookupCell" />.
		/// </summary>
		public LookupCell(ImportEntity importEntity, ImportColumn importColumn) {
			_rowIndex = importEntity == null ? default(uint) : importEntity.RowIndex;
			_columnIndex = importColumn?.Index;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Gets cell number.
		/// </summary>
		/// <returns>Cell number.</returns>
		public string GetCellNumber() => string.Concat(_columnIndex, _rowIndex);

		#endregion
	}

	#endregion

	#region  Class: LookupValuesToProcess

	public class LookupValuesToProcess
	{
		#region  Fields: Private

		/// <summary>
		/// Information about all cells which contain lookup value.
		/// </summary>
		private LookupColumnValues<Dictionary<string, List<LookupCell>>> _lookupValueCells;

		private LookupColumnValues<List<string>> _valuesToProcess;

		#endregion

		#region Constructors: Public

		public LookupValuesToProcess(UserConnection userConnection) => UserConnection = userConnection;

		#endregion

		#region Properties: Private

		/// <summary>
		/// Values to process.
		/// </summary>
		private LookupColumnValues<List<string>> ValuesToProcess =>
				_valuesToProcess ?? (_valuesToProcess = new LookupColumnValues<List<string>>(GetKey));

		private LookupColumnValues<Dictionary<string, List<LookupCell>>> LookupValueCells => _lookupValueCells ??
				(_lookupValueCells = new LookupColumnValues<Dictionary<string, List<LookupCell>>>(GetKey));

		#endregion

		#region Properties: Protected

		protected UserConnection UserConnection { get; }

		#endregion

		#region Methods: Private

		/// <summary>
		/// Gets lookup entity import column name.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <returns>Lookup entity import column name</returns>
		private string GetDestinationImportColumnName(ImportColumnDestination destination) {
			var entitySchemaUId = GetReferenceSchemaUId(destination);
			var entitySchema = UserConnection.EntitySchemaManager.GetInstanceByUId(entitySchemaUId);
			var importColumnName = destination.FindImportColumnName();
			var selectedColumnName = string.IsNullOrEmpty(importColumnName)
					? entitySchema.GetPrimaryDisplayColumnName()
					: importColumnName;
			return selectedColumnName;
		}

		private string GetKey(Guid entitySchemaUId, string columnName) => $"{entitySchemaUId}_{columnName}";

		/// <summary>
		/// Gets column destination reference schema unique identifier.
		/// </summary>
		/// <param name="destination">Column destination.</param>
		/// <returns>Reference schema unique identifier.</returns>
		private Guid GetReferenceSchemaUId(ImportColumnDestination destination) {
			var schemaUId = destination.FindReferenceSchemaUId();
			if (!schemaUId.IsEmpty()) {
				return schemaUId;
			}
			var referenceSchemaName = destination.FindReferenceSchemaName();
			var schema =
					UserConnection.EntitySchemaManager.GetItemByName(referenceSchemaName);
			schemaUId = schema.UId;
			return schemaUId;
		}

		/// <summary>
		/// Creates lookup entity column <see cref="LookupValuesEntitySchemaColumn" /> using given key.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <returns>Lookup entity column.</returns>
		private LookupValuesEntitySchemaColumn SplitKey(string key) {
			var items = key.Split(new[] { '_' }, 2);
			return new LookupValuesEntitySchemaColumn(Guid.Parse(items[0]), items[1]);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Add lookup value in collection.
		/// </summary>
		/// <param name="importEntity">An instance imported entity.</param>
		/// <param name="column">Import column.</param>
		/// <param name="columnValue">Import column value.</param>
		/// <param name="destination">Import column destination.</param>
		public void Add(ImportEntity importEntity, ImportColumn column, ImportColumnValue columnValue,
				ImportColumnDestination destination) {
			var entitySchemaUId = GetReferenceSchemaUId(destination);
			List<string> selectedColumnValues;
			List<LookupCell> cells;
			Dictionary<string, List<LookupCell>> valueCells;
			var selectedColumnName = GetDestinationImportColumnName(destination);
			if (!ValuesToProcess.ContainsKey(entitySchemaUId, selectedColumnName)) {
				selectedColumnValues = new List<string>();
				ValuesToProcess.Add(entitySchemaUId, selectedColumnName, selectedColumnValues);
				valueCells = new Dictionary<string, List<LookupCell>>();
				LookupValueCells.Add(entitySchemaUId, selectedColumnName, valueCells);
			} else {
				selectedColumnValues = ValuesToProcess.GetValue(entitySchemaUId, selectedColumnName);
				valueCells = LookupValueCells.GetValue(entitySchemaUId, selectedColumnName);
			}
			var selectedColumnValue = columnValue.Value.Trim();
			if (!valueCells.ContainsKey(selectedColumnValue)) {
				cells = new List<LookupCell>();
				valueCells.Add(selectedColumnValue, cells);
			} else {
				cells = valueCells[selectedColumnValue];
			}
			cells.Add(new LookupCell(importEntity, column));
			if (selectedColumnValues.Contains(selectedColumnValue)) {
				return;
			}
			selectedColumnValues.Add(selectedColumnValue);
		}

		/// <summary>
		/// Add lookup value in collection.
		/// </summary>
		public void Add(ImportEntity importEntity, IEnumerable<ImportColumn> lookupColumns) {
			foreach (var column in lookupColumns) {
				var columnValue = importEntity.FindColumnValue(column);
				if (columnValue == null) {
					continue;
				}
				foreach (var destination in column.Destinations) {
					Add(importEntity, column, columnValue, destination);
				}
			}
		}

		/// <summary>
		/// Get cells coordinates in source file.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> GetCellsIndexes(Guid entitySchemaUId, string columnName, string values) {
			var cells = LookupValueCells.GetValue(entitySchemaUId, columnName);
			if (cells != null && cells.TryGetValue(values, out var lookupCells)) {
				return lookupCells.Select(cell => cell.GetCellNumber());
			}
			return null;
		}

		/// <summary>
		/// Converts key into lookup entity column and gets all lookup values for all lookup entity column.
		/// </summary>
		/// <returns>Lookup values for all lookup entity column</returns>
		public IEnumerable<KeyValuePair<LookupValuesEntitySchemaColumn, List<string>>> GetItems() {
			var items = new List<KeyValuePair<LookupValuesEntitySchemaColumn, List<string>>>();
			foreach (var keyValuePair in ValuesToProcess) {
				items.Add(new KeyValuePair<LookupValuesEntitySchemaColumn, List<string>>(SplitKey(keyValuePair.Key),
						keyValuePair.Value));
			}
			return items;
		}

		public void RestoreState(LookupValuesToProcessMemento memento) {
			CollectionUtilities.ForEach(memento.LookupValueCells, item => LookupValueCells[item.Key] = item.Value);
			CollectionUtilities.ForEach(memento.ValuesToProcess, item => ValuesToProcess[item.Key] = item.Value);
		}

		public LookupValuesToProcessMemento SaveState() => new LookupValuesToProcessMemento {
				LookupValueCells = LookupValueCells,
				ValuesToProcess = ValuesToProcess
		};

		#endregion
	}

	#endregion

	#region  Class: LookupValuesToProcessMemento

	[DataContract]
	[Serializable]
	public class LookupValuesToProcessMemento
	{
		#region Properties: Public

		[DataMember(Name = "lookupValueCells")]
		public Dictionary<string, Dictionary<string, List<LookupCell>>> LookupValueCells { get;set; } 
			= new Dictionary<string, Dictionary<string, List<LookupCell>>>();

		[DataMember(Name = "valuesToProcess")]
		public Dictionary<string, List<string>> ValuesToProcess { get; set; } = new Dictionary<string, List<string>>();

		#endregion
	}

	#endregion
}

