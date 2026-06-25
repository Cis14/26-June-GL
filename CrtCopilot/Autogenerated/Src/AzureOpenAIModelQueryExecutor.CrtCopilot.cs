namespace Terrasoft.Configuration
{
    using Terrasoft.Core;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Factories;

    [DefaultBinding(typeof(IEntityQueryExecutor), Name = "AzureOpenAIModelQueryExecutor")]
    public class AzureOpenAIModelQueryExecutor : LlmModelQueryExecutor<AzureOpenAIModelJson>
    {
        #region Constructors: Public

        public AzureOpenAIModelQueryExecutor(UserConnection userConnection)
            : base(userConnection, "AzureOpenAIModel") {
        }

        #endregion

        #region Methods: Protected

        protected override void PopulateProviderSpecificFields(Entity entity, AzureOpenAIModelJson config) {
            entity.SetColumnValue("Model", config.Model ?? "");
            entity.SetColumnValue("ApiKey", config.ApiKey ?? "");
			entity.SetColumnValue("ResourceName", config.ResourceName ?? "");
			entity.SetColumnValue("ApiVersion", config.ApiVersion ?? "");
        }

        #endregion
    }
}

