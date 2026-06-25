namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: PrimaryImportEntitiesSetter

	/// <summary>
	/// An instance of this class is responsible for setting primary import entities.
	/// </summary>
	[DefaultBinding(typeof(IPrimaryImportEntitiesSetter), Name = nameof(PrimaryImportEntitiesSetter))]
	public class PrimaryImportEntitiesSetter : IPrimaryImportEntitiesSetter
	{

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="PrimaryImportEntitiesSetter"/>.
		/// </summary>
		/// <param name="columnsProcessor">Columns processor.</param>
		public PrimaryImportEntitiesSetter(INonPersistentColumnsAggregator columnsProcessor) {
			ColumnsProcessor = columnsProcessor;
		}

		/// <summary>
		/// Creates instance of type <see cref="PrimaryImportEntitiesSetter"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="columnsProcessor">Columns processor.</param>
		public PrimaryImportEntitiesSetter(UserConnection userConnection,
			INonPersistentColumnsAggregator columnsProcessor) : this(columnsProcessor) {
			_userConnection = userConnection;
		}

		#endregion

		#region Properties: Protected

		private UserConnection _userConnection;

		/// <summary>
		/// User connection.
		/// </summary>
		protected UserConnection UserConnection => _userConnection ??
			(_userConnection = ClassFactory.Get<UserConnection>());

		/// <summary>
		/// Columns processor.
		/// </summary>
		protected INonPersistentColumnsAggregator ColumnsProcessor { get; }

		#endregion

		#region Properties: Private

		private IFileImportPrimaryEntityLoader _fileImportPrimaryEntityLoader;
		private IFileImportPrimaryEntityLoader FileImportPrimaryEntityLoader => _fileImportPrimaryEntityLoader ??
			(_fileImportPrimaryEntityLoader = ClassFactory.Get<IFileImportPrimaryEntityLoader>(
			new ConstructorArgument("userConnection", UserConnection)));

		#endregion

		#region Methods: Private

		/// <summary>
		/// Gets entity key.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <returns>Entity key.</returns>
		private string GetEntityKey(Entity entity, IEnumerable<string> columnsNames,
			Func<string, object, object> columnValuePostProcessingAction = null) {
			StringBuilder sb = new StringBuilder();
			EntitySchemaColumnCollection schemaColumns = entity.Schema.Columns;
			foreach (string columnName in columnsNames) {
				EntitySchemaColumn schemaColumn = schemaColumns.GetByName(columnName);
				object columnValue;
				if (columnValuePostProcessingAction == null) {
					columnValue = entity.GetColumnValue(schemaColumn);
				} else {
					columnValue = columnValuePostProcessingAction(columnName, entity.GetColumnValue(schemaColumn));
				}
				sb.Append(columnValue);
			}
			return sb.ToString().ToLower();
		}

		/// <summary>
		/// Gets key entity columns names.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		/// <param name="keyColumns">Key columns.</param>
		/// <returns>Key entity columns names.</returns>
		private IEnumerable<string> GetKeyEntityColumnsNames(ImportParameters parameters,
			IEnumerable<ImportColumn> keyColumns) {
			List<string> columnsNames = new List<string>();
			foreach (ImportColumn column in keyColumns) {
				foreach (ImportColumnDestination destination in column.Destinations) {
					if (!destination.SchemaUId.Equals(parameters.RootSchemaUId) || !destination.IsKey) {
						continue;
					}
					columnsNames.Add(destination.ColumnName);
				}
			}
			return columnsNames;
		}

		/// <summary>
		/// Sets existing entities to import entities.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		/// <param name="entities">Existing entities.</param>
		/// <param name="keyColumns">Key columns.</param>
		/// <param name="columnValuePostProcessingAction">Column value post processing action.</param>
		private void SetPrimaryEntities(ImportParameters parameters, IEnumerable<Entity> entities,
				IEnumerable<ImportColumn> keyColumns, Func<string, object, object> columnValuePostProcessingAction = null) {
			List<string> processedEntitiesKeys = new List<string>();
			IEnumerable<string> keyEntityColumnsNames = GetKeyEntityColumnsNames(parameters, keyColumns);
			foreach (Entity entity in entities) {
				string entityKey = GetEntityKey(entity, keyEntityColumnsNames, columnValuePostProcessingAction);
				if (processedEntitiesKeys.Contains(entityKey)) {
					continue;
				}
				foreach (ImportEntity importEntity in parameters.Entities) {
					string importEntityKey = importEntity.GetKey(parameters, keyColumns, ColumnsProcessor,
						columnValuePostProcessingAction);
					if (importEntityKey.Equals(entityKey) && importEntity.PrimaryEntity == null) {
						importEntity.PrimaryEntity = entity;
						break;
					}
				}
				processedEntitiesKeys.Add(entityKey);
			}
		}

		private KeyValuePair<string, DataValueType>[] GetColumnsDataValueType(Entity entity) {
			return entity?.Schema.Columns.Select(col => new KeyValuePair<string, DataValueType>(col.Name, col.DataValueType)).ToArray();
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IPrimaryImportEntitiesSetter"/>
		public void Set(ImportParameters parameters, IEnumerable<Entity> entities,
				IEnumerable<ImportColumn> keyColumns) {
			var importValueFormatting = ClassFactory.Get<ImportValueFormatting>(new ConstructorArgument[] {
				new ConstructorArgument("columnsType", GetColumnsDataValueType(entities.FirstOrDefault()))
			});
			SetPrimaryEntities(parameters, entities, keyColumns, importValueFormatting.GetPostProcessingValueForSave);
			SetPrimaryEntities(parameters, entities, keyColumns,
				(columnName, columnValue) => importValueFormatting.GetPostProcessingValueForSave(columnName, columnValue)?.ToString().Trim());
			if (parameters.LoadOnlyPrimaryColumnValuesInDuplicateSearch) {
				FileImportPrimaryEntityLoader.LoadEntities(parameters);
			}
		}

		#endregion
	}

	#endregion

}
