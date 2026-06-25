namespace Terrasoft.Configuration
{
    using System;
    using Terrasoft.Core;
    using Terrasoft.Core.Entities;
	using Terrasoft.AppFeatures;

    public abstract class LlmModelQueryExecutor<TConfig> : BaseQueryExecutor, IEntityQueryExecutor
        where TConfig : LlmModelJson, new()
    {
        #region Fields: Private

        private readonly UserConnection _userConnection;
        private readonly string _entitySchemaName;

        #endregion

        #region Constructors: Protected

        protected LlmModelQueryExecutor(UserConnection userConnection, string entitySchemaName)
            : base(userConnection, entitySchemaName) {
            _userConnection = userConnection;
            _entitySchemaName = entitySchemaName;
        }

        #endregion

        #region Properties: Protected

		protected new UserConnection UserConnection => _userConnection;
		protected new EntitySchema EntitySchema => UserConnection.EntitySchemaManager.GetInstanceByName(_entitySchemaName);

        #endregion

        #region Methods: Private

        private void PopulateBaseEntity(Entity entity, Entity llmModel) {
            entity.SetColumnValue("Id", llmModel.GetTypedColumnValue<Guid>("Id"));
            entity.SetColumnValue("ModifiedOn", llmModel.GetTypedColumnValue<DateTime>("ModifiedOn"));
            entity.SetColumnValue("Name", llmModel.GetTypedColumnValue<string>("Name"));
            entity.SetColumnValue("Code", llmModel.GetTypedColumnValue<string>("Code"));
            entity.SetColumnValue("Description", llmModel.GetTypedColumnValue<string>("Description"));
            entity.SetColumnValue("CanUseStructuredOutput", llmModel.GetTypedColumnValue<bool>("CanUseStructuredOutput"));
            entity.SetColumnValue("CanUseFunctionCalling", llmModel.GetTypedColumnValue<bool>("CanUseFunctionCalling"));
            entity.SetColumnValue("EncryptedConfig", llmModel.GetTypedColumnValue<string>("EncryptedConfig"));
            entity.SetColumnValue("LlmProviderId", llmModel.GetTypedColumnValue<Guid>("LlmProviderId"));
            entity.SetColumnValue("LlmProviderName", llmModel.GetTypedColumnValue<string>("LlmProviderName"));
        }

        private EntitySchemaQuery BuildLlmModelQuery(EntitySchemaQuery sourceQuery) {
            var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "LlmModel");
            esq.PrimaryQueryColumn.IsAlwaysSelect = true;
            esq.UnmaskColumnValues = true;
            var llmModelSchema = UserConnection.EntitySchemaManager.GetInstanceByName("LlmModel");
            foreach(var column in llmModelSchema.Columns) {
                esq.AddColumn(column.Name);
            }
            if (sourceQuery.Filters.Count > 0) {
                esq.Filters.Add(sourceQuery.Filters);
            }
            if (sourceQuery.RowCount > 0) {
                esq.RowCount = sourceQuery.RowCount;
            }
            return esq;
        }

        #endregion

        #region Methods: Protected

        protected abstract void PopulateProviderSpecificFields(Entity entity, TConfig config);

        #endregion

        #region Methods: Public

        public EntityCollection GetEntityCollection(EntitySchemaQuery esq) {
            var collection = new EntityCollection(UserConnection, EntitySchema);
            var llmModelQuery = BuildLlmModelQuery(esq);
            var llmModelCollection = llmModelQuery.GetEntityCollection(UserConnection);
            foreach (var llmModel in llmModelCollection) {
                var virtualEntity = EntitySchema.CreateEntity(UserConnection);
                PopulateBaseEntity(virtualEntity, llmModel);
                var encryptedConfig = llmModel.GetTypedColumnValue<string>("EncryptedConfig");
                var config = LlmModelJson.FromJson<TConfig>(encryptedConfig);
                PopulateProviderSpecificFields(virtualEntity, config);
                collection.Add(virtualEntity);
            }
            return collection;
        }

        #endregion
    }
}

