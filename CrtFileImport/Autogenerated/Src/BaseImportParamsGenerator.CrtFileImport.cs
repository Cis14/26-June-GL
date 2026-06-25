namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Common;
	using FileImport;
	using Core.Entities;

	#region Class: BaseImportParamsGenerator

	/// <summary>
	/// Base import params generator.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.IImportParamsGenerator" />
	public class BaseImportParamsGenerator : IImportParamsGenerator
	{
		#region Fields: Private

		private EntitySchema _rootSchema;

		#endregion

		#region Struct: Private

		protected struct ColumnScores
		{
			public List<ImportColumnValue> HeaderColumnValues;
			public List<ImportColumnValue> EntityColumns;
			public List<ImportColumn> Columns;
		}

		#endregion

		#region Fields: Protected

		protected const string TypeUIdPropertyName = "TypeUId";
		protected const string ReferenceSchemaPropertyName = "ReferenceSchemaUId";
		protected const string ImportColumnPropertyName = "ImportColumnName";
		protected const string ImportColumnIdPropertyValue = "Id";

		#endregion

		#region Constructors

		public BaseImportParamsGenerator(Dictionary<string, string> columnValues) {
			ColumnValues = columnValues;
		}

		public BaseImportParamsGenerator() {
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Gets the column values.
		/// </summary>
		/// <value>
		/// The column values.
		/// </value>
		public Dictionary<string, string> ColumnValues {
			get;
			protected set;
		}

		#endregion

		#region Methods: Private

		private ColumnScores GetEmptyColumnScore() {
			var columnScore = new ColumnScores {
				Columns = new List<ImportColumn>(),
				EntityColumns = new List<ImportColumnValue>(),
				HeaderColumnValues = new List<ImportColumnValue>()
			};
			return columnScore;
		}

		#endregion

		#region Methods: Protected

		protected void ApplyColumns(ImportParameters importParameters, ColumnScores columnScores) {
			importParameters.Columns = columnScores.Columns;
			importParameters.HeaderColumnsValues = columnScores.HeaderColumnValues;
			ImportEntity entity = importParameters.Entities.First();
			entity.ColumnValues = columnScores.EntityColumns;
		}

		protected void AddColumns(ColumnScores columnScores, string columnName, string columnValue) {
			columnScores.HeaderColumnValues.Add(CreateImportColumnValue(columnName, columnName));
			columnScores.EntityColumns.Add(CreateImportColumnValue(columnName, columnValue));
			columnScores.Columns.Add(CreateImportColumn(columnName, columnValue));
		}

		protected EntitySchemaColumn GetColumnByName(string columnName) {
			EntitySchemaColumnCollection columns = _rootSchema.Columns;
			return columns.FindByName(columnName) ?? columns.FindByColumnValueName(columnName);
		}

		protected virtual ImportParameters GetImportParams(EntitySchema entitySchema) {
			var importParameters = new ImportParameters();
			importParameters.Entities = new List<ImportEntity>();
			var entity = new ImportEntity();
			importParameters.Entities.Add(entity);
			importParameters.RootSchemaUId = entitySchema.UId;
			return importParameters;
		}

		protected virtual ImportColumnValue CreateImportColumnValue(string columnName, string value) {
			return new ImportColumnValue(value, columnName);
		}

		protected virtual ImportColumn CreateImportColumn(string columnName, string columnValue) {
			var column = new ImportColumn() {
				Index = columnName,
				Source = columnName
			};
			column.Destinations = new List<ImportColumnDestination>();
			column.Destinations.Add(CreateDestinationColumn(columnName, columnValue));
			return column;
		}

		protected virtual ImportColumnDestination CreateDestinationColumn(string columnName, string columnValue) {
			EntitySchemaColumn destColumn = GetColumnByName(columnName);
			return new ImportColumnDestination() {
				SchemaUId = _rootSchema.UId,
				ColumnName = destColumn.Name,
				ColumnValueName = destColumn.ColumnValueName,
				Properties = GetColumnDestinationProperties(destColumn, columnValue),
				IsKey = _rootSchema.PrimaryColumn == destColumn
			};
		}

		protected virtual Dictionary<string, object> GetColumnDestinationProperties(EntitySchemaColumn entitySchemaColumn, string columnValue) {
			var values = new Dictionary<string, object> {
				{ TypeUIdPropertyName, entitySchemaColumn.DataValueTypeUId }
			};
			if (entitySchemaColumn.IsLookupType) {
				values[ReferenceSchemaPropertyName] = entitySchemaColumn.ReferenceSchemaUId;
				values[ImportColumnPropertyName] = columnValue.IsValidGuid() ? ImportColumnIdPropertyValue : null;
			}
			return values;
		}

		protected virtual bool CheckColumnExists(string key) {
			EntitySchemaColumn destColumn = GetColumnByName(key);
			return destColumn != null;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Generates the parameters.
		/// </summary>
		/// <param name="entitySchema">The entity schema.</param>
		/// <param name="values">The values.</param>
		/// <returns>The import parameters.</returns>
		/// <exception cref="ArgumentNullException">
		/// values
		/// or
		/// Root entity schema
		/// </exception>
		public virtual ImportParameters GenerateParameters(EntitySchema entitySchema, Dictionary<string, string> values) {
			if (values == null) {
				throw new ArgumentNullException("values");
			}
			if (entitySchema == null) {
				throw new ArgumentNullException("Root entity schema");
			}
			_rootSchema = entitySchema;
			var importParams = GetImportParams(entitySchema);
			var columns = GetEmptyColumnScore();
			foreach (string key in values.Keys) {
				string value = values[key];
				if (!CheckColumnExists(key) || value.IsNullOrEmpty()) {
					continue;
				}
				AddColumns(columns, key, values[key]);
			}
			ApplyColumns(importParams, columns);
			return importParams;
		}

		/// <summary>
		/// Generates the parameters.
		/// </summary>
		/// <param name="entitySchema">The entity schema.</param>
		/// <returns>The import parameters.</returns>
		public virtual ImportParameters GenerateParameters(EntitySchema entitySchema) {
			return GenerateParameters(entitySchema, ColumnValues);
		}

		#endregion
	}

	#endregion

}

