namespace Terrasoft.Core.Process.Configuration
{

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using System.Linq;
	using Terrasoft.Core.BusinessRules.Appliers.Extensions;
	using Terrasoft.Core.BusinessRules.Models;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Entities.Extensions;
	using JsonSerializer = Terrasoft.Common.Json.Json;
	using ErrorMessageBuilder = Terrasoft.Configuration.ErrorMessageBuilder;

	#region Class: RecordCreationUserTask

	/// <exclude/>
	public partial class RecordCreationUserTask
	{

		#region Methods: Private

		private bool TryValidateAndHandleLookup(ref string columnName, EntitySchema schema) {
			var columnNamesToSearch = new List<string> {
				columnName
			};
			if (columnName.EndsWith("Id") && columnName.Length > 2) {
				var baseColumnName = columnName.Substring(0, columnName.Length - 2);
				columnNamesToSearch.Add(baseColumnName);
			}
			var foundColumns = schema.Columns.FindByNames(columnNamesToSearch);
			var firstFoundColumn = foundColumns.FirstOrDefault(x => x != null);
			if (foundColumns.IsEmpty() || firstFoundColumn == null) {
				string error = $"Column '{columnName}' not found in schema '{schema.Name}'." + Environment.NewLine;
				OutputError = ErrorMessageBuilder.AddError(OutputError, error);
				return false;
			}
			columnName = firstFoundColumn.Name;
			return true;
		}

		private IEnumerable<string> SetColumnValues(Entity entity, EntitySchema schema) {
			var columns = string.IsNullOrWhiteSpace(ColumnValueMappingString)
				? new Dictionary<string, object>()
				: JsonSerializer.Deserialize<Dictionary<string, object>>(ColumnValueMappingString);
			var modifiedColumns = new List<string>();
			foreach (var pair in columns) {
				var columnName = pair.Key;
				var value = pair.Value;
				if (!TryValidateAndHandleLookup(ref columnName, schema)) {
					continue;
				}
				object columnValue = ParseColumnValue(value);
				if (entity.Schema.Columns.FindByName(columnName) == null) {
					var existingColumns = entity.Schema.Columns.Select(c => c.Name).Aggregate((a, b) => a + ", " + b);
					var error = $"Column '{columnName}' not found in schema '{ObjectCode}'." +
						"Please retrieve entity column details and try again." +
						$"Existing columns: {existingColumns}.";
					OutputError = ErrorMessageBuilder.AddError(OutputError, error);
				} else {
					entity.SetColumnValue(columnName, columnValue);
					modifiedColumns.Add(columnName);
				}
			}
			return modifiedColumns;
		}

		private object ParseColumnValue(object value)
		{
			if (value == null) {
				return null;
			}
			if (value is string str)
			{
				if (Guid.TryParse(str, out Guid guid)) {
					return guid == Guid.Empty ? null : (object)guid;
				}
				if (DateTime.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime date)) {
					return date;
				}
			}
			return value;
		}
		
		private void SetIdAndSave(Entity entity) {
			try {
				var createdEntityId = Guid.NewGuid();
				entity.SetColumnValue("Id", createdEntityId);
				var saved = entity.Save();
				if (saved) {
					CreatedRecordId = entity.GetTypedColumnValue<Guid>("Id");
				}
			} catch (Exception e) {
				OutputError = ErrorMessageBuilder.AddError(OutputError, e.Message);
			}
		}

		private void PrepareEntityForBusinessRules(Entity entity, EntitySchema schema) {
			entity.LoadLookupDisplayValues();
			foreach (var column in schema.Columns) {
				if (!column.IsLookupType || column.ReferenceSchema == null) {
					continue;
				}
				if (!entity.GetIsColumnValueLoaded(column.ColumnValueName)) {
					continue;
				}
				var lookupId = entity.GetTypedColumnValue<Guid>(column.ColumnValueName);
				if (lookupId.IsEmpty()) {
					continue;
				}
				var primaryImageColumnName = column.PrimaryImageColumnValueName;
				if (column.ReferenceSchema.PrimaryImageColumn != null
						&& !entity.GetIsColumnValueLoaded(primaryImageColumnName)) {
					entity.SetColumnValue(primaryImageColumnName, null);
				}
				var primaryColorColumnName = column.PrimaryColorColumnValueName;
				if (column.ReferenceSchema.PrimaryColorColumn != null
						&& !entity.GetIsColumnValueLoaded(primaryColorColumnName)) {
					entity.SetColumnValue(primaryColorColumnName, null);
				}
			}
		}

		#endregion

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {
			var userConnection = context.UserConnection;
			var schema = userConnection.EntitySchemaManager.GetInstanceByName(ObjectCode);
			var entity = schema.CreateEntity(userConnection);
			entity.SetDefColumnValues();
			try {
				var modifiedColumns = SetColumnValues(entity, schema);
				PrepareEntityForBusinessRules(entity, schema);
				var result = entity.ApplyBusinessRules(BusinessRuleContextStateType.Create, modifiedColumns);
				OutputError = ErrorMessageBuilder.AddBusinessRulesErrors(OutputError, result.Errors);
				if (DryRun) {
					entity.ValidateRequiredColumns();
				} else if (OutputError.IsNullOrEmpty() && !result.Errors.Any()) {
					SetIdAndSave(entity);
				}
				HasError = !OutputError.IsNullOrEmpty() || result.Errors.Any();
			} catch (Exception e) {
				OutputError = ErrorMessageBuilder.AddError(OutputError, e.Message);
				HasError = true;
			}
			return true;
		}

		#endregion

		#region Methods: Public

		public override bool CompleteExecuting(params object[] parameters) {
			return base.CompleteExecuting(parameters);
		}

		public override void CancelExecuting(params object[] parameters) {
			base.CancelExecuting(parameters);
		}

		public override string GetExecutionData() {
			return string.Empty;
		}

		public override ProcessElementNotification GetNotificationData() {
			return base.GetNotificationData();
		}

		#endregion

	}

	#endregion

}
