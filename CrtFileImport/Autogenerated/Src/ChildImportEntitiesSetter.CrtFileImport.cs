namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: ChildImportEntitiesSetter

	/// <summary>
	/// An instance of this class is responsible for setting primary import entities.
	/// </summary>
	[DefaultBinding(typeof(IChildImportEntitiesSetter), Name = nameof(ChildImportEntitiesSetter))]
	public class ChildImportEntitiesSetter: IChildImportEntitiesSetter
	{

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="ChildImportEntitiesSetter"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="columnsProcessor">Columns processor.</param>
		public ChildImportEntitiesSetter(UserConnection userConnection, INonPersistentColumnsAggregator columnsProcessor) {
			UserConnection = userConnection;
			ColumnsProcessor = columnsProcessor;
		}

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Columns processor.
		/// </summary>
		protected INonPersistentColumnsAggregator ColumnsProcessor { get; }

		/// <summary>
		/// User connection.
		/// </summary>
		protected UserConnection UserConnection { get; }

		#endregion

		#region Methods: Private

		/// <summary>
		/// Gets entity key.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <returns>Entity key.</returns>
		private string GetEntityKey(Entity entity, IEnumerable<string> columnsNames) {
			StringBuilder sb = new StringBuilder();
			EntitySchemaColumnCollection schemaColumns = entity.Schema.Columns;
			foreach (string columnName in columnsNames) {
				EntitySchemaColumn schemaColumn = schemaColumns.GetByName(columnName);
				object columnValue = entity.GetColumnValue(schemaColumn);
				sb.Append(columnValue);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Gets entity key columns names.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <param name="schema">Entity schema.</param>
		/// <param name="importEntity">Import entity.</param>
		/// <param name="destinationValues">Destination values.</param>
		/// <returns>Entity key columns names.</returns>
		private IEnumerable<string> GetEntityKeyColumnsNames(ImportColumnDestination destination, EntitySchema schema,
				ImportEntity importEntity, Dictionary<ImportColumnDestination, object> destinationValues) {
			List<string> entityKeyColumnsNames = new List<string> {
				GetDestinationAttributeColumnName(destination, schema)
			};
			EntitySchemaColumn connectionColumn = importEntity.GetReferenceColumn(schema);
			entityKeyColumnsNames.Add(connectionColumn.Name);
			IEnumerable<string> destinationColumnsNames = GetDestinationColumnsNames(destinationValues, destination);
			entityKeyColumnsNames.AddRange(destinationColumnsNames);
			return entityKeyColumnsNames;
		}

		/// <summary>
		/// Gets destination columns names.
		/// </summary>
		/// <param name="destinationValues">Destination values.</param>
		/// <param name="destination">Import column destination.</param>
		/// <returns>Destination columns names.</returns>
		private IEnumerable<string> GetDestinationColumnsNames(
				Dictionary<ImportColumnDestination, object> destinationValues, ImportColumnDestination destination) {
			return destinationValues
				.Where(destinationValue => destinationValue.Key.GetKey() == destination.GetKey())
				.Select(destinationValue => destinationValue.Key.ColumnName);
		}

		/// <summary>
		/// Gets import entity key.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <param name="schema">Entity schema.</param>
		/// <param name="importEntity">Import entity.</param>
		/// <param name="destinationValues">Destination values.</param>
		/// <returns>Import entity key.</returns>
		private string GetImportEntityKey(ImportColumnDestination destination, EntitySchema schema,
				ImportEntity importEntity, Dictionary<ImportColumnDestination, object> destinationValues) {
			StringBuilder sb = new StringBuilder();
			object attributeColumnValue = destination.FindAttributeColumnValue();
			sb.Append(attributeColumnValue);
			Guid connectionColumnValue = importEntity.PrimaryEntity.PrimaryColumnValue;
			sb.Append(connectionColumnValue);
			IEnumerable<object> destinationValuesForSave = GetDestinationValuesForSave(destinationValues, destination);
			foreach (object destinationValueForSave in destinationValuesForSave) {
				sb.Append(destinationValueForSave);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Gets destination values for save.
		/// </summary>
		/// <param name="destinationValues">Destination values.</param>
		/// <param name="destination">Import column destination.</param>
		/// <returns>Destination columns names.</returns>
		private IEnumerable<object> GetDestinationValuesForSave(
				Dictionary<ImportColumnDestination, object> destinationValues, ImportColumnDestination destination) {
			return destinationValues
				.Where(destinationValue => destinationValue.Key.GetKey() == destination.GetKey())
				.Select(destinationValue => destinationValue.Value);
		}

		/// <summary>
		/// Gets destination attribute column name.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <param name="schema">EntitySchema.</param>
		/// <returns>Destination attribute column name.</returns>
		private string GetDestinationAttributeColumnName(ImportColumnDestination destination, EntitySchema schema) {
			string attributeColumnValueName = destination.FindAttributeColumnValueName();
			return schema.Columns
				.Where(column => column.ColumnValueName.Equals(attributeColumnValueName))
				.Select(column => column.Name)
				.First();
		}

		/// <summary>
		/// Gets schemas key columns values.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		/// <param name="importEntity">Import entity.</param>
		/// <returns>Schemas key columns values.</returns>
		private Dictionary<EntitySchema, Dictionary<ImportColumnDestination, object>> GetSchemasKeyDestinations(
				ImportParameters parameters, ImportEntity importEntity) {
			var schemasKeyDestinations = new Dictionary<EntitySchema, Dictionary<ImportColumnDestination, object>>();
			foreach (ImportColumn column in parameters.Columns) {
				ImportColumnValue columnValue = importEntity.FindColumnValue(column);
				if (columnValue == null) {
					continue;
				}
				foreach (ImportColumnDestination destination in column.Destinations) {
					Guid schemaUId = destination.SchemaUId;
					if (schemaUId.Equals(parameters.RootSchemaUId)) {
						continue;
					}
					object valueForSave = ColumnsProcessor.FindValueForSave(destination, columnValue);
					if (valueForSave == null) {
						continue;
					}
					EntitySchema schema = UserConnection.EntitySchemaManager.GetInstanceByUId(schemaUId);
					Dictionary<ImportColumnDestination, object> keyDestinations;
					if (!schemasKeyDestinations.TryGetValue(schema, out keyDestinations)) {
						keyDestinations = new Dictionary<ImportColumnDestination, object>();
						schemasKeyDestinations.Add(schema, keyDestinations);
					}
					keyDestinations.Add(destination, valueForSave);
				}
			}
			return schemasKeyDestinations;
		}

		/// <summary>
		/// Adds child entities.
		/// </summary>
		/// <param name="entities">Entities.</param>
		/// <param name="importEntity">Import entity.</param>
		/// <param name="schemasKeyDestinations">Schemas key destinations.</param>
		private void AddChildEntities(IEnumerable<Entity> entities, ImportEntity importEntity,
				Dictionary<EntitySchema, Dictionary<ImportColumnDestination, object>> schemasKeyDestinations) {
			foreach (var schemaKeyDestinations in schemasKeyDestinations) {
				EntitySchema schema = schemaKeyDestinations.Key;
				Dictionary<ImportColumnDestination, object> destinationValues = schemaKeyDestinations.Value;
				foreach (var destinationValue in destinationValues) {
					ImportColumnDestination destination = destinationValue.Key;
					Entity childEntity = importEntity.FindChildEntity(destination);
					if (childEntity != null) {
						continue;
					}
					AddChildEntity(destination, schema, importEntity, destinationValues, entities);
				}
			}
		}

		/// <summary>
		/// Adds child entity.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <param name="schema">Entity schema.</param>
		/// <param name="importEntity">Import entity.</param>
		/// <param name="destinationValues">Destination values.</param>
		/// <param name="entities">Entities.</param>
		private void AddChildEntity(ImportColumnDestination destination, EntitySchema schema,
				ImportEntity importEntity, Dictionary<ImportColumnDestination, object> destinationValues,
				IEnumerable<Entity> entities) {
			string importEntityKey = GetImportEntityKey(destination, schema, importEntity, destinationValues);
			IEnumerable<string> entityKeyColumnsNames = GetEntityKeyColumnsNames(destination, schema, importEntity,
				destinationValues);
			foreach (Entity entity in entities) {
				if (!entity.Schema.UId.Equals(schema.UId)) {
					continue;
				}
				string entityKey = GetEntityKey(entity, entityKeyColumnsNames);
				if (importEntityKey.Equals(entityKey)) {
					importEntity.AddChildEntity(destination, entity);
					break;
				}
			}
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IChildImportEntitiesSetter"/>
		public void Set(ImportParameters parameters, IEnumerable<Entity> entities) {
			if (entities.Count() == 0) {
				return;
			}
			foreach (ImportEntity importEntity in parameters.Entities) {
				if (importEntity.PrimaryEntity == null) {
					continue;
				}
				Dictionary<EntitySchema, Dictionary<ImportColumnDestination, object>> schemasKeyDestinations =
					GetSchemasKeyDestinations(parameters, importEntity);
				AddChildEntities(entities, importEntity, schemasKeyDestinations);
			}
		}

		#endregion

	}

	#endregion

}
