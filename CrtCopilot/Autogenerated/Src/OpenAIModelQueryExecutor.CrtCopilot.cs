namespace Terrasoft.Configuration
{
    using Terrasoft.Core;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Factories;

    [DefaultBinding(typeof(IEntityQueryExecutor), Name = "OpenAIModelQueryExecutor")]
    public class OpenAIModelQueryExecutor : LlmModelQueryExecutor<OpenAIModelJson>
    {
        #region Constructors: Public

        public OpenAIModelQueryExecutor(UserConnection userConnection) 
            : base(userConnection, "OpenAIModel") {
        }

        #endregion

        #region Methods: Protected

        protected override void PopulateProviderSpecificFields(Entity entity, OpenAIModelJson config) {
            entity.SetColumnValue("Model", config.Model ?? "");
            entity.SetColumnValue("ApiKey", config.ApiKey ?? "");
        }

        #endregion
    }
}
