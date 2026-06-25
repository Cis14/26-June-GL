namespace Terrasoft.Configuration
{
    using System;
    using Terrasoft.Core;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Entities.Events;

    #region Class: BaseLlmModelEventListener

    /// <summary>
    /// Base entity event listener for LLM model virtual entities.
    /// Handles CRUD operations by proxying to LlmModel physical entity.
    /// </summary>
    public abstract class BaseLlmModelEventListener : BaseEntityEventListener
    {
        #region Constants: Protected

        protected const string LlmModelSchemaName = "LlmModel";

        #endregion

        #region Properties: Protected Abstract

        /// <summary>
        /// Gets the error message prefix for the specific model prefix.
        /// </summary>
        protected abstract string ModelPrefix { get; }

        #endregion

        #region Methods: Private

        private Entity CreateLlmModelEntity(UserConnection userConnection) {
            var llmModelSchema = userConnection.EntitySchemaManager.GetInstanceByName(LlmModelSchemaName);
            return llmModelSchema.CreateEntity(userConnection);
        }

        private void ProcessSave(Entity virtualEntity) {
            var userConnection = virtualEntity.UserConnection;
            var id = virtualEntity.GetTypedColumnValue<Guid>("Id");
            if (id == Guid.Empty) {
                id = Guid.NewGuid();
                virtualEntity.SetColumnValue("Id", id);
            }
            var llmModel = CreateLlmModelEntity(userConnection);
            bool isNew = !llmModel.FetchFromDB(id);
            if (isNew) {
                llmModel.SetColumnValue("Id", id);
                llmModel.SetDefColumnValues();
            }
            SyncFromVirtualToLlmModel(virtualEntity, llmModel);
            llmModel.Save(false);
        }

        private void ProcessDelete(Entity virtualEntity) {
            var userConnection = virtualEntity.UserConnection;
            var id = virtualEntity.GetTypedColumnValue<Guid>("Id");
            if (id == Guid.Empty) {
                return;
            }
            var llmModel = CreateLlmModelEntity(userConnection);
            if (llmModel.FetchFromDB(id)) {
                llmModel.Delete();
            }
        }

        #endregion

        #region Methods: Protected

		protected string EnsureModelPrefix(string modelName) {
            if (string.IsNullOrWhiteSpace(modelName)) {
                return ModelPrefix;
            }
            string normalizedModel = modelName.StartsWith(ModelPrefix, StringComparison.OrdinalIgnoreCase)
                ? modelName
                : $"{ModelPrefix}{modelName}";
            return normalizedModel;
        }

        /// <summary>
        /// Synchronizes common properties from virtual entity to LlmModel entity.
        /// </summary>
        protected void SyncCommonProperties(Entity virtualEntity, Entity llmModel) {
            if (virtualEntity.IsColumnValueLoaded("Name")) {
                llmModel.SetColumnValue("Name", virtualEntity.GetTypedColumnValue<string>("Name"));
            }
            if (virtualEntity.IsColumnValueLoaded("Code")) {
                llmModel.SetColumnValue("Code", virtualEntity.GetTypedColumnValue<string>("Code"));
            }
            if (virtualEntity.IsColumnValueLoaded("Description")) {
                llmModel.SetColumnValue("Description", virtualEntity.GetTypedColumnValue<string>("Description"));
            }
            if (virtualEntity.IsColumnValueLoaded("CanUseStructuredOutput")) {
                llmModel.SetColumnValue("CanUseStructuredOutput",
                    virtualEntity.GetTypedColumnValue<bool>("CanUseStructuredOutput"));
            }
            if (virtualEntity.IsColumnValueLoaded("CanUseFunctionCalling")) {
                llmModel.SetColumnValue("CanUseFunctionCalling",
                    virtualEntity.GetTypedColumnValue<bool>("CanUseFunctionCalling"));
            }
            if (virtualEntity.IsColumnValueLoaded("LlmProviderId")) {
                llmModel.SetColumnValue("LlmProviderId",
                    virtualEntity.GetTypedColumnValue<Guid>("LlmProviderId"));
            }
        }

        /// <summary>
        /// Synchronizes provider-specific configuration from virtual entity to LlmModel entity.
        /// </summary>
        protected abstract void SyncProviderConfig(Entity virtualEntity, Entity llmModel);

        /// <summary>
        /// Synchronizes all properties from virtual entity to LlmModel entity.
        /// </summary>
        protected void SyncFromVirtualToLlmModel(Entity virtualEntity, Entity llmModel) {
            SyncCommonProperties(virtualEntity, llmModel);
            SyncProviderConfig(virtualEntity, llmModel);
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Handles entity Saving event.
        /// </summary>
        public override void OnSaving(object sender, EntityBeforeEventArgs e) {
            base.OnSaving(sender, e);
            var virtualEntity = (Entity)sender;
            try {
                ProcessSave(virtualEntity);
                e.IsCanceled = true;
            }
            catch (Exception ex) {
                e.IsCanceled = true;
                throw new InvalidOperationException($"Error saving {ModelPrefix} model: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Handles entity Deleting event.
        /// </summary>
        public override void OnDeleting(object sender, EntityBeforeEventArgs e) {
            base.OnDeleting(sender, e);
            var virtualEntity = (Entity)sender;
            try {
                ProcessDelete(virtualEntity);
                e.IsCanceled = true;
            }
            catch (Exception ex) {
                e.IsCanceled = true;
                throw new InvalidOperationException($"Error deleting {ModelPrefix} model: {ex.Message}", ex);
            }
        }

        #endregion
    }

    #endregion
}

