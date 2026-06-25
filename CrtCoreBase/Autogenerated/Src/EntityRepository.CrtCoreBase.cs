namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Store;
	using Terrasoft.Common;
	using Terrasoft.Core.DB;

	#region Class: EntityData

	/// <summary>
	/// Entity data contract.
	/// </summary>
	[Serializable]
	public class EntityData
	{
		/// <summary>
		/// Gets or sets the entity identifier.
		/// </summary>
		/// <value>
		/// The entity identifier.
		/// </value>
		public Guid Id { get; set; }
	}

	#endregion

	#region Interface: IGetRepository 

	/// <summary>
	/// Provides methods to obtain data.
	/// </summary>
	/// <typeparam name="TEntityData">Data</typeparam>
	public interface IGetRepository<out TEntityData>
		where TEntityData : EntityData
	{

		/// <summary>
		/// Gets entity data by identifier.
		/// </summary>
		/// <param name="id">The item identifier.</param>
		/// <returns>The entity data.</returns>
		TEntityData Get(Guid id);

		/// <summary>
		/// Gets all entity data.
		/// </summary>
		/// <returns>Collection of entity data.</returns>
		IEnumerable<TEntityData> GetAll();
	}

	#endregion
	
	#region Interface: IEntityRepository

	public interface IEntityRepository<TEntityData> : IGetRepository<TEntityData>
		where TEntityData : EntityData
	{
		/// <summary>
		/// Adds the specified entity data.
		/// </summary>
		/// <param name="entity">The entity data.</param>
		void Add(TEntityData entity);

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity data.</param>
		void Update(TEntityData entity);

		/// <summary>
		/// Updates entity with the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="changedValues">The column values.</param>
		void Update(Guid id, IDictionary<string, object> changedValues);

		/// <summary>
		/// Updates entities with the specified identifiers.
		/// </summary>
		/// <param name="keyCollection">The key collection.</param>
		/// <param name="changedValues">The column values.</param>
		void BulkUpdate(IEnumerable<Guid> keyCollection, IDictionary<string, object> changedValues);
	}

	#endregion

	#region Class: CachedEntityRepository

	/// <summary>
	/// Repository of cache for entity data.
	/// </summary>
	/// <typeparam name="TEntityData">The type of the entity data.</typeparam>
	/// <seealso cref="Terrasoft.Configuration.EntityRepository{TEntityData}" />
	public class CachedEntityRepository<TEntityData> : EntityRepository<TEntityData>
		where TEntityData : EntityData, new()
	{
		
		#region Properties: Protected

		protected string EntityCacheGroupName { get; }

		#endregion

		#region Constructors: Public

		public CachedEntityRepository(UserConnection userConnection, string entitySchemaName,
				string entitycacheGroupName) : base(userConnection, entitySchemaName) {
			EntityCacheGroupName = entitycacheGroupName;
		}

		#endregion

		#region Methods: Protected

		protected override EntitySchemaQuery GetEntityCollectionEsq() {
			EntitySchemaQuery esq = base.GetEntityCollectionEsq();
			if (EntityCacheGroupName.IsNullOrWhiteSpace()) {
				return esq;
			}
			esq.Cache = UserConnection.ApplicationCache.WithLocalCaching(EntityCacheGroupName);
			esq.CacheItemName = $"{EntityCacheGroupName}Item";
			return esq;
		}

		protected override Entity GetEntity(Guid id) {
			var entities = GetEntityCollection();
			return entities.Find(entity => entity.PrimaryColumnValue.Equals(id));
		}

		#endregion
	}

	#endregion

	#region Class: EntityRepository

	/// <summary>
	/// The repository for entity data.
	/// </summary>
	/// <typeparam name="TEntityData">The type of the entity data.</typeparam>
	/// <seealso cref="Terrasoft.Configuration.IEntityRepository{TEntityData}" />
	public class EntityRepository<TEntityData> : IEntityRepository<TEntityData>
		where TEntityData : EntityData, new()
	{

		#region Fields: Protected

		protected readonly UserConnection UserConnection;

		protected readonly EntitySchema SchemaInstance;

		#endregion

		#region Constructors: Public

		public EntityRepository(UserConnection userConnection, string entitySchemaName) {
			UserConnection = userConnection;
			SchemaInstance = UserConnection.EntitySchemaManager.FindInstanceByName(entitySchemaName);
		}

		#endregion

		#region Methods: Private

		private void FillEntityFromValues(Entity entity, IDictionary<string, object> values) {
			foreach (var value in values) {
				entity.SetColumnValue(value.Key, value.Value);
			}
		}

		#endregion

		#region Methods: Protected

		protected Entity CreateEntityFromObject(TEntityData item) {
			Entity entity = CreateEntity();
			FillEntityFromObject(entity, item);
			return entity;
		}

		protected virtual Entity CreateEntity() {
			return SchemaInstance.CreateEntity(UserConnection);
		}

		protected virtual Entity GetEntity(Guid id) {
			var entity = CreateEntity();
			if (!entity.FetchFromDB(id)) {
				throw new Common.ItemNotFoundException($"{SchemaInstance.Name} with id {id}");
			}
			return entity;
		}

		protected virtual EntitySchemaQuery GetEntityCollectionEsq() {
			var esq = new EntitySchemaQuery(SchemaInstance);
			esq.PrimaryQueryColumn.IsAlwaysSelect = true;
			return esq;
		}

		protected virtual TEntityData CreateObjectFromEntity(Entity entity) {
			var instance = new TEntityData {
				Id = entity.GetTypedColumnValue<Guid>("Id")
			};
			return instance;
		}

		protected virtual void FillEntityFromObject(Entity entity, TEntityData item) {
			entity.SetColumnValue("Id", item.Id);
		}

		protected virtual EntityCollection GetEntityCollection() {
			var esq = GetEntityCollectionEsq();
			return esq.GetEntityCollection(UserConnection);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Gets entity data by identifier.
		/// </summary>
		/// <param name="id">The item identifier.</param>
		/// <returns>The entity data.</returns>
		public TEntityData Get(Guid id) {
			var entity = GetEntity(id);
			return CreateObjectFromEntity(entity);
		}

		/// <summary>
		/// Gets all entity data.
		/// </summary>
		/// <returns>Collection of entity data.</returns>
		public IEnumerable<TEntityData> GetAll() {
			var result = new List<TEntityData>();
			var entities = GetEntityCollection();
			entities.ForEach(entity => result.Add(CreateObjectFromEntity(entity)));
			return result;
		}

		/// <summary>
		/// Adds the specified entity data.
		/// </summary>
		/// <param name="item">The entity data.</param>
		public void Add(TEntityData item) {
			Entity entity = CreateEntityFromObject(item);
			entity.Save(false);
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="item">The entity data.</param>
		public void Update(TEntityData item) {
			var entity = GetEntity(item.Id);
			FillEntityFromObject(entity, item);
			entity.Save(false);
		}

		/// <summary>
		/// Updates entity with the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="columnValues">The column values.</param>
		public void Update(Guid id, IDictionary<string, object> columnValues) {
			var entity = GetEntity(id);
			FillEntityFromValues(entity, columnValues);
			entity.Save(false);
		}

		/// <summary>
		/// Updates entities with the specified identifiers.
		/// </summary>
		/// <param name="keyCollection">The key collection.</param>
		/// <param name="columnValues">The column values.</param>
		public void BulkUpdate(IEnumerable<Guid> keyCollection, IDictionary<string, object> columnValues) {
			if (!keyCollection.Any() || columnValues.Count == 0) {
				return;
			}
			var update = new Update(UserConnection, SchemaInstance.Name);
			foreach (var column in columnValues) {
				update.Set(column.Key, Column.Parameter(column.Value));
			}
			update.Where("Id").In(Column.Parameters(keyCollection));
			update.Execute();
		}

		#endregion
		
	}

	#endregion
}
