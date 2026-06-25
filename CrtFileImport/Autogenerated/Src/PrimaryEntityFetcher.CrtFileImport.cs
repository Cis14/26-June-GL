namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: PrimaryEntityFetcher

	///<inheritdoc cref="IPrimaryEntityFinder"/>
	public class PrimaryEntityFetcher : IPrimaryEntityFinder
	{

		#region Properties: Private

		/// <summary>
		/// Gets columns processor.
		/// </summary>
		private IColumnsAggregatorAdapter ColumnsProcessor { get; }

		/// <summary>
		/// Gets user connection.
		/// </summary>
		private UserConnection UserConnection { get; }

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="PrimaryEntityFetcher" />.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="columnsProcessor">Columns processor.</param>
		public PrimaryEntityFetcher(UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor) {
			UserConnection = userConnection;
			ColumnsProcessor = columnsProcessor;
		}

		#endregion

		#region Methods: Private

		private EntitySchema GetEntitySchema(Guid entitySchemaUId) {
			return UserConnection.EntitySchemaManager.GetInstanceByUId(entitySchemaUId);
		}

		private KeyValuePair<string, object> GetFilterItemParameter(ImportColumnDestination destination, ImportColumnValue importColumnValue, EntitySchema entitySchema) {
			object value = null;
			if(importColumnValue != null) {
				value = ColumnsProcessor.FindValueForSave(destination, importColumnValue);
			}
			if(value == null) {
				var columns = entitySchema.Columns;
				value = columns.GetByName(destination.ColumnName).DataValueType.DefValue;
			}
			return new KeyValuePair<string, object>(destination.ColumnName, value);
		}

		private Dictionary<string, object> GetFiltersParameters(ImportEntity importEntity, IEnumerable<ImportColumn> keysImportColumns, EntitySchema entitySchema) {
			var filtersParameters = new Dictionary<string, object>();
			foreach(var importColumn in keysImportColumns) {
				var columnValue = importEntity.FindColumnValue(importColumn);
				var rootSchemaKeyColumns = importColumn.GetDestinationRootSchemaKeyColumns(entitySchema.UId);
				var filterItemParameter = rootSchemaKeyColumns.Select(d => GetFilterItemParameter(d, columnValue, entitySchema));
				filtersParameters.AddRange(filterItemParameter);
			}
			return filtersParameters;
		}

		#endregion

		#region Methods: Public

		///<inheritdoc cref="IPrimaryEntityFinder"/>
		public void LoadPrimaryEntity(ImportParameters parameters, IEnumerable<ImportColumn> keysImportColumns) {
			parameters.CheckArgumentNull(nameof(parameters));
			keysImportColumns.CheckArgumentNull(nameof(keysImportColumns));
			if(!keysImportColumns.Any()) {
				return;
			}
			var entitySchema = GetEntitySchema(parameters.RootSchemaUId);
			foreach(var importEntity in parameters.Entities) {
				var filtersParameters = GetFiltersParameters(importEntity, keysImportColumns, entitySchema);
				var entity = new Entity(UserConnection, parameters.RootSchemaUId);
				try {
					if(entity.FetchFromDB(filtersParameters, true)) {
						importEntity.PrimaryEntity = entity;
					}
				} catch(DublicateDataException e) {
					importEntity.ImportEntityException = e;
				}
			}
		}

		#endregion

	}

	#endregion
}
