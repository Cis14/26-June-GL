namespace Terrasoft.Configuration
{
    using Terrasoft.Core;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Factories;

    [DefaultBinding(typeof(IEntityQueryExecutor), Name = "CustomLlmModelQueryExecutor")]
    public class CustomLlmModelQueryExecutor : LlmModelQueryExecutor<CustomModelJson>
    {
        #region Constructors: Public

        public CustomLlmModelQueryExecutor(UserConnection userConnection)
            : base(userConnection, "CustomLlmModel") {
        }

        #endregion

        #region Methods: Protected

        protected override void PopulateProviderSpecificFields(Entity entity, CustomModelJson config) {
            entity.SetColumnValue("JsonConfig", config.ToJson());
        }

        #endregion
    }
}

