namespace Terrasoft.Configuration.GeneratedWebFormService
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Common.Json;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: ObjectDefaultValueManager

	/// <summary>
	/// Returns default values for given landing and entity.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.GeneratedWebFormService.IDefaultValueManager" />
	public class ObjectDefaultValueManager : IDefaultValueManager
	{

		#region Properties: Protected

		protected virtual string SourceDefaultsSchemaName {
			get { return "LandingObjectDefaults"; }
		}

		protected EntitySchema SourceEntitySchema { get; set; }

		#endregion

		#region Methods: Protected

		protected virtual void InitSourceSchema(Guid webFormId, UserConnection userConnection) {
			SourceEntitySchema = WebFormHelper.GetWebFormEntitySchema(webFormId, userConnection);
		}

		protected virtual EntitySchemaQuery GetEntityESQ(EntitySchemaManager entitySchemaManager) {
			var formESQ = new EntitySchemaQuery(entitySchemaManager, "GeneratedWebForm");
			formESQ.PrimaryQueryColumn.IsAlwaysSelect = true;
			formESQ.AddColumn("EntityDefaultValues");
			return formESQ;
		}

		protected virtual Dictionary<string, object> GetColumnsFromEntityDefaultValues(Guid webFormId, UserConnection userConnection) {
			var result = new Dictionary<string, object>();
			EntitySchemaManager entitySchemaManager = userConnection.EntitySchemaManager;
			EntitySchema entitySchema = SourceEntitySchema;
			EntitySchemaQuery formESQ = GetEntityESQ(entitySchemaManager);
			Entity generatedWebFormEntity = formESQ.GetEntity(userConnection, webFormId);
			EntitySchemaColumn defaultFieldsColumn
				= generatedWebFormEntity.Schema.Columns.GetByName("EntityDefaultValues");
			string jsonData = generatedWebFormEntity.GetTypedColumnValue<string>(defaultFieldsColumn);
			List<FormDefaultValuesData> defaultFieldsArray
				= Json.Deserialize<List<FormDefaultValuesData>>(jsonData);
			if (defaultFieldsArray != null) {
				foreach (FormDefaultValuesData defaultFieldItem in defaultFieldsArray) {
					try {
						EntitySchemaColumn entityColumn
							= entitySchema.Columns.GetByName(defaultFieldItem.columnName);
						Type columnType = entityColumn.ValueType;
						if (columnType.Name == "Guid") {
							FormDefaultGuidValue defaultValueObject
								= Json.Deserialize<FormDefaultGuidValue>(defaultFieldItem.value);
							object defaultValue = Json.Deserialize(defaultValueObject.value, columnType);
							result.Add(defaultFieldItem.columnName + "Id", defaultValue);
						} else {
							object defaultValue = Json.Deserialize(defaultFieldItem.value, columnType);
							result.Add(defaultFieldItem.columnName, defaultValue);
						}
					} catch (Exception) {
						continue;
					}
				}
			}
			return result;
		}

		protected virtual Dictionary<string, object> GetColumnsFromLandingDefaultValues(Guid webFormId, UserConnection userConnection) {
			var result = new Dictionary<string, object>();
			EntitySchemaManager entitySchemaManager = userConnection.EntitySchemaManager;
			EntitySchema entitySchema = SourceEntitySchema;
			EntitySchemaQuery landingDefaultsEsq = GetLandingDefaultsESQ(entitySchemaManager, webFormId);
			EntityCollection defaultEntityCollection
				= landingDefaultsEsq.GetEntityCollection(userConnection);
			foreach (Entity entity in defaultEntityCollection) {
				EntitySchemaColumn columnUIdColumn = entity.Schema.Columns.GetByName("ColumnUId");
				EntitySchemaColumn guidValueColumn = entity.Schema.Columns.GetByName("GuidValue");
				EntitySchemaColumn textValueColumn = entity.Schema.Columns.GetByName("TextValue");
				EntitySchemaColumn floatValueColumn = entity.Schema.Columns.GetByName("FloatValue");
				EntitySchemaColumn integerValueColumn = entity.Schema.Columns.GetByName("IntegerValue");
				EntitySchemaColumn booleanValueColumn = entity.Schema.Columns.GetByName("BooleanValue");
				EntitySchemaColumn dateTimeValueColumn = entity.Schema.Columns.GetByName("DateTimeValue");
				Guid columnUId = entity.GetTypedColumnValue<Guid>(columnUIdColumn.ColumnValueName);
				EntitySchemaColumn column = entitySchema.Columns.GetByUId(columnUId);
				object columnValue = null;
				TypeCode typeCode = Type.GetTypeCode(column.DataValueType.ValueType);
				switch (typeCode) {
					case TypeCode.String:
						columnValue = entity.GetTypedColumnValue<String>(textValueColumn.ColumnValueName);
						break;
					case TypeCode.Object:
						if (column.DataValueType.ValueType == typeof(Guid)) {
							if (column.IsLookupType) {
								columnValue = entity.GetTypedColumnValue<Guid>(guidValueColumn.ColumnValueName);
							} else {
								columnValue = entity.GetTypedColumnValue<Guid>(textValueColumn.ColumnValueName);
							}
						}
						break;
					case TypeCode.Int32:
						columnValue = entity.GetTypedColumnValue<Int32>(integerValueColumn.ColumnValueName);
						break;
					case TypeCode.Decimal:
						columnValue = entity.GetTypedColumnValue<Decimal>(floatValueColumn.ColumnValueName);
						break;
					case TypeCode.Boolean:
						columnValue = entity.GetTypedColumnValue<Boolean>(booleanValueColumn.ColumnValueName);
						break;
					case TypeCode.DateTime:
						columnValue = entity.GetTypedColumnValue<DateTime>(dateTimeValueColumn.ColumnValueName);
						break;
				}
				result.Add(column.ColumnValueName, columnValue);
			}
			return result;
		}

		protected virtual EntitySchemaQuery GetLandingDefaultsESQ(EntitySchemaManager entitySchemaManager, Guid webFormId) {
			var landingDefaultsEsq = new EntitySchemaQuery(entitySchemaManager, SourceDefaultsSchemaName);
			landingDefaultsEsq.PrimaryQueryColumn.IsAlwaysSelect = true;
			landingDefaultsEsq.AddColumn("Landing");
			landingDefaultsEsq.AddColumn("ColumnUId");
			landingDefaultsEsq.AddColumn("GuidValue");
			landingDefaultsEsq.AddColumn("TextValue");
			landingDefaultsEsq.AddColumn("FloatValue");
			landingDefaultsEsq.AddColumn("IntegerValue");
			landingDefaultsEsq.AddColumn("BooleanValue");
			landingDefaultsEsq.AddColumn("DateTimeValue");
			landingDefaultsEsq.Filters.Add(landingDefaultsEsq.CreateFilterWithParameters(
				FilterComparisonType.Equal,
				"Landing",
				webFormId
				));
			return landingDefaultsEsq;
		}

		protected void MergeColumnValuesWithOverwrite(
				Dictionary<string, object> mergeValues, Dictionary<string, object> mergeIntoValues) {
			foreach (string columnName in mergeValues.Keys) {
				mergeIntoValues[columnName] = mergeValues[columnName];
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Returns default values for given landing.
		/// </summary>
		/// <param name="userConnection">Instance of user connection.</param>
		/// <param name="webFormId">Identifier of landing.</param>
		/// <returns>Dictionary with column name as key and column value as value.</returns>
		public virtual Dictionary<string, object> GetValues(Guid webFormId, UserConnection userConnection) {
			var result = new Dictionary<string, object>();
			InitSourceSchema(webFormId, userConnection);
			MergeColumnValuesWithOverwrite(GetColumnsFromEntityDefaultValues(webFormId, userConnection), result);
			MergeColumnValuesWithOverwrite(GetColumnsFromLandingDefaultValues(webFormId, userConnection), result);
			return result;
		}

		#endregion
	}

	#endregion

}

