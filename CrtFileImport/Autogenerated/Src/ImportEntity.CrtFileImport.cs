namespace Terrasoft.Configuration.FileImport
{
	using Common;
	using Core;
	using Core.Entities;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;
	using System.Text;

	#region  Class: ImportEntityMemento

	[Serializable]
	public class ImportEntityMemento
	{

		#region  Fields: Public

		[DataMember(Name = "columnValues")]
		public IEnumerable<ImportColumnValue> ColumnValues;
		[DataMember(Name = "rowIndex")]
		public uint RowIndex;

		#endregion

	}

	#endregion
	

	#region  Class: ImportEntity

	/// <summary>
	/// An instance of this class represents entity that is being imported.
	/// </summary>
	[Serializable]
	public class ImportEntity
	{
		#region  Fields: Private

		[NonSerialized]
		private Dictionary<string, Entity> _childEntities;

		private IEnumerable<ImportColumnValue> _columnValues;

		/// <summary>
		/// Key.
		/// </summary>
		[NonSerialized]
		private string _key;

		[NonSerialized]
		private Entity _primaryEntity;

		/// <summary>
		/// Raw key.
		/// </summary>
		[NonSerialized]
		private string _rawKey;

		#endregion

		#region  Fields: Public

		/// <summary>
		/// File row index.
		/// </summary>
		public uint RowIndex;

		#endregion

		#region Properties: Public

		/// <summary>
		/// Column values.
		/// </summary>
		public IEnumerable<ImportColumnValue> ColumnValues {
			get => _columnValues;
			set => _columnValues = value;
		}

		/// <summary>
		/// Primary entity.
		/// </summary>
		public Entity PrimaryEntity {
			get => _primaryEntity;
			set => _primaryEntity = value;
		}

		/// <summary>
		/// Child entities.
		/// </summary>
		public Dictionary<string, Entity> ChildEntities {
			get => _childEntities ?? (_childEntities = new Dictionary<string, Entity>());
			set => _childEntities = value;
		}

		/// <summary>
		/// Import entity exception.
		/// </summary>
		public Exception ImportEntityException { get; set; }

		#endregion

		#region Methods: Private

		/// <summary>
		/// Creates child entity.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="entitySchema">Entity schema.</param>
		/// <param name="destination">Import column destination.</param>
		/// <returns>Child entity.</returns>
		private Entity CreateChildEntity(UserConnection userConnection, EntitySchema entitySchema,
				ImportColumnDestination destination) {
			var entity = CreateEntity(userConnection, entitySchema);
			foreach (var attribute in destination.Attributes) {
				entity.SetColumnValue(attribute.Key, attribute.Value);
			}
			var connectionColumn = GetReferenceColumn(entitySchema);
			entity.SetColumnValue(connectionColumn.ColumnValueName, PrimaryEntity.PrimaryColumnValue);
			return entity;
		}

		/// <summary>
		/// Creates entity.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="entitySchema">Entity schema.</param>
		/// <returns>Entity.</returns>
		private Entity CreateEntity(UserConnection userConnection, EntitySchema entitySchema) {
			var entity = entitySchema.CreateEntity(userConnection);
			entity.SetDefColumnValues();
			return entity;
		}

		/// <summary>
		/// Gets child entity.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="destination">Import column destination.</param>
		/// <returns>Child entity.</returns>
		private Entity ForceGetChildEntity(UserConnection userConnection, ImportColumnDestination destination) {
			var childEntity = FindChildEntity(destination);
			if (childEntity == null) {
				var destinationKey = destination.GetKey();
				var entitySchema = userConnection.EntitySchemaManager.GetInstanceByUId(destination.SchemaUId);
				childEntity = CreateChildEntity(userConnection, entitySchema, destination);
				ChildEntities.Add(destinationKey, childEntity);
			}
			return childEntity;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Adds child entity.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <param name="entity">Entity.</param>
		public void AddChildEntity(ImportColumnDestination destination, Entity entity) {
			var destinationKey = destination.GetKey();
			ChildEntities.Add(destinationKey, entity);
		}

		/// <summary>
		/// Finds child entity.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <returns>Child entity.</returns>
		public Entity FindChildEntity(ImportColumnDestination destination) {
			var destinationKey = destination.GetKey();
			Entity childEntity;
			ChildEntities.TryGetValue(destinationKey, out childEntity);
			return childEntity;
		}

		/// <summary>
		/// Finds import entity column value.
		/// </summary>
		/// <param name="column">Import column.</param>
		/// <returns>Import entity column value.</returns>
		public ImportColumnValue FindColumnValue(ImportColumn column) {
			return ColumnValues
				.FirstOrDefault(columnValue => columnValue.ColumnIndex == column.Index);
		}

		/// <summary>
		/// Gets entity that will be saved.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="destination">Import column destination.</param>
		/// <returns>Entity that will be saved.</returns>
		public Entity GetEntityForSave(UserConnection userConnection, ImportColumnDestination destination) {
			if (destination.SchemaUId.Equals(PrimaryEntity.Schema.UId)) {
				return PrimaryEntity;
			}
			return ForceGetChildEntity(userConnection, destination);
		}

		/// <summary>
		/// Gets key.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		/// <param name="columns">Import columns.</param>
		/// <param name="columnsProcessor">Columns processor.</param>
		/// <param name="postProcessing">Postprocessing function.</param>
		/// <returns>Key.</returns>
		public string GetKey(ImportParameters parameters, IEnumerable<ImportColumn> columns,
				INonPersistentColumnsAggregator columnsProcessor, Func<string, object, object> postProcessing = null) {
			var sb = new StringBuilder();
			var schemaUId = parameters.RootSchemaUId;
			foreach (var column in columns) {
				var columnValue = FindColumnValue(column);
				if (columnValue == null) {
					continue;
				}
				foreach (var destination in column.Destinations) {
					if (!destination.SchemaUId.Equals(schemaUId) || !destination.IsKey) {
						continue;
					}
					var valueForSave = columnsProcessor.FindValueForSave(destination, columnValue);
					if (valueForSave == null) {
						continue;
					}
					if (postProcessing != null) {
						valueForSave = postProcessing(destination.ColumnName, valueForSave);
					}
					sb.Append(valueForSave);
				}
			}
			_key = sb.ToString().ToLower();
			return _key;
		}

		/// <summary>
		/// Gets raw key.
		/// </summary>
		/// <param name="columns">Key columns.</param>
		/// <returns>Raw key.</returns>
		public string GetRawKey(IEnumerable<ImportColumn> columns) {
			if (_rawKey.IsNullOrEmpty()) {
				var sb = new StringBuilder();
				foreach (var column in columns) {
					var columnValue = FindColumnValue(column);
					if (columnValue == null) {
						continue;
					}
					sb.Append(columnValue.Value.Trim());
				}
				_rawKey = sb.ToString().ToLower();
			}
			return _rawKey;
		}

		/// <summary>
		/// Gets column that connects child entities to parent entity.
		/// </summary>
		/// <param name="entitySchema">Child entity schema.</param>
		/// <returns>Column that connects child entities to parent entity.</returns>
		public EntitySchemaColumn GetReferenceColumn(EntitySchema entitySchema) {
			foreach (var column in entitySchema.Columns) {
				if (!column.UsageType.Equals(EntitySchemaColumnUsageType.General)) {
					continue;
				}
				if (column.ReferenceSchemaUId.Equals(PrimaryEntity.Schema.UId)) {
					return column;
				}
			}
			throw new ItemNotFoundException(entitySchema.Name);
		}

		/// <summary>
		/// Initializes primary entity.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="parameters">Import parameters.</param>
		public void InitPrimaryEntity(UserConnection userConnection, ImportParameters parameters) {
			if (PrimaryEntity != null) {
				return;
			}
			var entitySchema = userConnection.EntitySchemaManager.GetInstanceByUId(parameters.RootSchemaUId);
			PrimaryEntity = CreateEntity(userConnection, entitySchema);
			PrimaryEntity.UseAdminRights = parameters.UseAdminRights;
		}

		/// <summary>
		/// Saves import entity.
		/// </summary>
		public virtual void Save(bool saveInBackground = false) {
			var entitySaveconfig = new EntitySaveConfig {
				SetForceBackgroundMode = saveInBackground,
				SkipLiveEditing = true
			};
			PrimaryEntity.Save(entitySaveconfig);
			ChildEntities.Values.ForEach(item => item.Save(entitySaveconfig));
		}

		/// <summary>
		/// Returns state for save
		/// </summary>
		/// <returns></returns>
		public ImportEntityMemento SaveState() => new ImportEntityMemento() {
				ColumnValues = new List<ImportColumnValue>(ColumnValues),
				RowIndex = RowIndex
		};

		/// <summary>
		/// Restore saved state 
		/// </summary>
		/// <param name="memento"></param>
		public void RestoreState(ImportEntityMemento memento) {
			RowIndex = memento.RowIndex;
			ColumnValues = new List<ImportColumnValue>(memento.ColumnValues);
		}

		/// <summary>
		/// Create an instance from <see cref="ImportEntityMemento"/> object.
		/// </summary>
		public static ImportEntity CreateFromMemento(ImportEntityMemento memento) {
			var entity = new ImportEntity();
			entity.RestoreState(memento);
			return entity;
		}

		#endregion
	}

	#endregion
}

