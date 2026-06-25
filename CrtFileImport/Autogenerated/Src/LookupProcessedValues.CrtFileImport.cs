namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using Common;
	using Core;

	#region  Class: LookupProcessedValues

	public class LookupProcessedValues : IProcessedValuesProvider
	{
		#region  Fields: Private

		private LookupColumnValues<Dictionary<string, Guid>> _results;

		#endregion

		#region Constructors: Public

		public LookupProcessedValues(UserConnection userConnection) => UserConnection = userConnection;

		#endregion

		#region Properties: Private

		/// <summary>
		/// Results.
		/// </summary>
		private LookupColumnValues<Dictionary<string, Guid>> Results =>
				_results ?? (_results = new LookupColumnValues<Dictionary<string, Guid>>(GetKey));

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

		#endregion

		#region Methods: Public

		public void Add(Guid entitySchemaUId, string columnName, Dictionary<string, Guid> columnValues) {
			Results.Add(entitySchemaUId, columnName, columnValues);
		}

		/// <summary>
		/// Finds processed value.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <param name="columnValue">Import column value.</param>
		/// <returns>Processed value.</returns>
		public object FindValueForSave(ImportColumnDestination destination, ImportColumnValue columnValue) {
			var entitySchemaUId = GetReferenceSchemaUId(destination);
			var importColumnName = GetDestinationImportColumnName(destination);
			var values = Results.GetValue(entitySchemaUId, importColumnName);
			var key = columnValue.Value.ToLower().Trim();
			if (!values.TryGetValue(key, out var id)) {
				return null;
			}
			return id;
		}

		public void RestoreState(LookupProcessedValuesMemento memento) {
			if (memento.Results == null) {
				return;
			}
			foreach (var item in memento.Results) {
				Results[item.Key] = item.Value;
			}
		}

		public LookupProcessedValuesMemento SaveState() => new LookupProcessedValuesMemento {
				Results = Results
		};

		#endregion
	}

	#endregion

	#region  Class: LookupProcessedValuesMemento

	[DataContract]
	[Serializable]
	public class LookupProcessedValuesMemento
	{
		#region Properties: Public

		[DataMember(Name = "Results")]
		public Dictionary<string, Dictionary<string, Guid>> Results {get; set; } 
			= new Dictionary<string, Dictionary<string, Guid>> ();

		#endregion
	}

	#endregion
}

