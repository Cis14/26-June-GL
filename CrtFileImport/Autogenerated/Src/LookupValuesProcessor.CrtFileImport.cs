namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Core;
	using Core.Entities;

	#region  Class: LookupValuesProcessor

	public class LookupValuesProcessor
	{
		#region  Fields: Private

		private readonly bool _validateRequiredColumns;

		#endregion

		#region Constructors: Public

		public LookupValuesProcessor(UserConnection userConnection, bool validateRequiredColumns) {
			_validateRequiredColumns = validateRequiredColumns;
			UserConnection = userConnection;
		}

		#endregion

		#region Properties: Protected

		protected UserConnection UserConnection { get; }

		#endregion

		#region Events: Public

		public event EventHandler<LookupValuesProcessorErrorEventArgs> ProcessError;

		#endregion

		#region Methods: Private

		/// <summary>
		/// Adds CreatedOn column.
		/// </summary>
		/// <param name="esq">Entity schema query.</param>
		private void AddCreatedOnColumn(EntitySchemaQuery esq) {
			var createdOnColumn = esq.RootSchema.CreatedOnColumn;
			if (createdOnColumn != null) {
				var queryColumn = esq.AddColumn(createdOnColumn.Name);
				queryColumn.OrderByAsc();
			}
		}

		/// <summary>
		/// Adds existing lookup column values.
		/// </summary>
		/// <param name="columnValues">Column values.</param>
		/// <param name="entitySchemaUId">Entity schema unique identifier.</param>
		/// <param name="selectedColumnName">Selected lookup entity schema column name.</param>
		/// <param name="selectedColumnValues">Selected lookup entity schema column values.</param>
		private void AddExistingLookupValues(Dictionary<string, Guid> columnValues, Guid entitySchemaUId,
				string selectedColumnName, IEnumerable<string> selectedColumnValues) {
			var primaryColumnValues = GetExistingLookupPrimaryColumnValues(entitySchemaUId,
					selectedColumnName, selectedColumnValues);
			foreach (var item in primaryColumnValues) {
				columnValues.Add(item.Key, item.Value);
			}
		}

		/// <summary>
		/// Adds new lookup column values.
		/// </summary>
		/// <param name="columnValues">Column values.</param>
		/// <param name="entitySchemaUId">Entity schema unique identifier.</param>
		/// <param name="selectedColumnName">Selected lookup entity schema column name.</param>
		/// <param name="selectedColumnValues">Selected lookup entity schema column values.</param>
		private void AddNewLookupValues(Dictionary<string, Guid> columnValues, Guid entitySchemaUId,
				string selectedColumnName, IEnumerable<string> selectedColumnValues) {
			var primaryColumnValues = GetNewLookupValues(columnValues, entitySchemaUId,
					selectedColumnName, selectedColumnValues);
			foreach (var item in primaryColumnValues) {
				columnValues.Add(item.Key, item.Value);
			}
		}

		/// <summary>
		/// Gets existing lookup entity primary column values.
		/// </summary>
		/// <param name="entitySchemaUId">Entity schema unique identifier.</param>
		/// <param name="selectedColumnName">Selected lookup entity schema column name.</param>
		/// <param name="selectedColumnValues">Selected lookup entity schema column values.</param>
		/// <returns>Lookup entity primary column values.</returns>
		private Dictionary<string, Guid> GetExistingLookupPrimaryColumnValues(Guid entitySchemaUId,
				string selectedColumnName, IEnumerable<string> selectedColumnValues) {
			var primaryColumnValues = new Dictionary<string, Guid>();
			var entitySchema = UserConnection.EntitySchemaManager.GetInstanceByUId(entitySchemaUId);
			var esq = new EntitySchemaQuery(entitySchema) {
					RowCount = UserConnection.AppConnection.MaxEntityRowCount
			};
			AddCreatedOnColumn(esq);
			var queryPrimaryColumn = esq.AddColumn(entitySchema.GetPrimaryColumnName());
			var querySelectedColumn = esq.AddColumn(selectedColumnName);
			foreach (var valuesToProcessGroup in GetValuesToProcessGroups(selectedColumnValues)) {
				esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, selectedColumnName,
						valuesToProcessGroup));
				var entityCollection = esq.GetEntityCollection(UserConnection);
				foreach (var entity in entityCollection) {
					var primaryColumnValue = entity.GetTypedColumnValue<Guid>(queryPrimaryColumn.Name);
					var selectedColumnValue =
							entity.GetTypedColumnValue<string>(querySelectedColumn.Name);
					selectedColumnValue = selectedColumnValue.ToLower();
					if (!primaryColumnValues.ContainsKey(selectedColumnValue)) {
						primaryColumnValues.Add(selectedColumnValue, primaryColumnValue);
					}
				}
				esq.ResetSelectQuery();
				esq.Filters.Clear();
			}
			return primaryColumnValues;
		}

		/// <summary>
		/// Gets missing lookup column values.
		/// </summary>
		/// <param name="existingColumnValues">Existing lookup column values.</param>
		/// <param name="allColumnValues">All column values.</param>
		/// <returns>Missing lookup column values.</returns>
		private IEnumerable<string> GetMissingLookupValues(Dictionary<string, Guid> existingColumnValues,
				IEnumerable<string> allColumnValues) {
			foreach (var columnValue in allColumnValues) {
				if (!existingColumnValues.ContainsKey(columnValue.ToLower())) {
					yield return columnValue;
				}
			}
		}

		/// <summary>
		/// Gets new lookup values.
		/// </summary>
		/// <param name="columnValues">Column values.</param>
		/// <param name="entitySchemaUId">Entity schema unique identifier.</param>
		/// <param name="selectedColumnName">Selected lookup entity schema column name.</param>
		/// <param name="selectedColumnValues">Selected lookup entity schema column values.</param>
		/// <returns>New lookup values.</returns>
		private Dictionary<string, Guid> GetNewLookupValues(Dictionary<string, Guid> columnValues, Guid entitySchemaUId,
				string selectedColumnName, IEnumerable<string> selectedColumnValues) {
			var missingLookupValues = GetMissingLookupValues(columnValues, selectedColumnValues);
			var lookupValues = new Dictionary<string, Guid>();
			foreach (var messingValue in missingLookupValues) {
				if (lookupValues.ContainsKey(messingValue.ToLower())) {
					continue;
				}
				var primaryColumnValue = ProcessMissingLookupValues(entitySchemaUId, selectedColumnName, messingValue);
				if (primaryColumnValue.HasValue) {
					lookupValues.Add(messingValue.ToLower(), primaryColumnValue.Value);
				}
			}
			return lookupValues;
		}

		/// <summary>
		/// Gets values to process groups.
		/// </summary>
		/// <param name="values">Values to process.</param>
		/// <returns>Values to process groups.</returns>
		private IEnumerable<IEnumerable<string>> GetValuesToProcessGroups(IEnumerable<string> values) {
			var valuesCount = values.Count();
			var valuesToProcessGroups = new List<IEnumerable<string>>();
			for (var processed = 0; processed < valuesCount; processed += FileImporterConstants.MaxQueryFiltersCount) {
				valuesToProcessGroups.Add(values.Skip(processed).Take(FileImporterConstants.MaxQueryFiltersCount));
			}
			return valuesToProcessGroups;
		}

		private void OnProcessError(LookupValuesProcessorErrorEventArgs eventArgs) {
			ProcessError?.Invoke(this, eventArgs);
		}

		private Guid? ProcessMissingLookupValues(Guid entitySchemaUId, string columnName, string missingValue) {
			var entityId = TryCreateEntity(entitySchemaUId, columnName, missingValue);
			return entityId;
		}

		/// <summary>
		/// Creates entity.
		/// </summary>
		/// <param name="entitySchemaUId">Entity schema unique identifier.</param>
		/// <param name="selectedColumnName">Selected lookup entity schema column name.</param>
		/// <param name="selectedColumnValue">Selected lookup entity schema column value.</param>
		/// <returns>Entity identifier.</returns>
		private Guid? TryCreateEntity(Guid entitySchemaUId, string selectedColumnName, string selectedColumnValue) {
			var primaryColumnValue = Guid.NewGuid();
			var entitySchema = UserConnection.EntitySchemaManager.GetInstanceByUId(entitySchemaUId);
			var selectedColumn = entitySchema.GetSchemaColumnByPath(selectedColumnName);
			if (selectedColumn.DataValueType is TextDataValueType) {
				selectedColumnValue = TextColumnHelper.PrepareTextColumnValue(
						(TextDataValueType)selectedColumn.DataValueType,
						selectedColumnValue);
			}
			try {
				var entity = entitySchema.CreateEntity(UserConnection);
				entity.SetColumnValue(entitySchema.GetPrimaryColumnName(), primaryColumnValue);
				entity.SetColumnValue(selectedColumnName, selectedColumnValue);
				entity.SetDefColumnValues();
				entity.Save(_validateRequiredColumns);
				return primaryColumnValue;
			} catch(Exception e) {
				OnProcessError(new LookupValuesProcessorErrorEventArgs(entitySchemaUId, selectedColumnName,
						selectedColumnValue, e));
				return null;
			}
		}

		#endregion

		#region Methods: Public

		public LookupProcessedValues ProcessValues(
				IEnumerable<KeyValuePair<LookupValuesEntitySchemaColumn, List<string>>> valuesToProcess) {
			var lookupProcessedValues = new LookupProcessedValues(UserConnection);
			foreach (var valueToProcess in valuesToProcess) {
				var entitySchemaUId = valueToProcess.Key.EntitySchemaUId;
				var selectedColumnName = valueToProcess.Key.Name;
				IEnumerable<string> selectedColumnValues = valueToProcess.Value;
				var columnValues = new Dictionary<string, Guid>();
				AddExistingLookupValues(columnValues, entitySchemaUId, selectedColumnName, selectedColumnValues);
				AddNewLookupValues(columnValues, entitySchemaUId, selectedColumnName, selectedColumnValues);
				lookupProcessedValues.Add(entitySchemaUId, selectedColumnName, columnValues);
			}
			return lookupProcessedValues;
		}

		#endregion

	}

	#endregion
}

