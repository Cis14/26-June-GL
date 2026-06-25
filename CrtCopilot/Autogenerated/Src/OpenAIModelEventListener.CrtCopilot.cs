namespace Terrasoft.Configuration
{
    using System;
    using System.Text.RegularExpressions;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Entities.Events;

    #region Class: OpenAIModelEventListener

    /// <summary>
    /// Entity event listener for OpenAIModel virtual entity.
    /// </summary>
    [EntityEventListener(SchemaName = "OpenAIModel")]
    public class OpenAIModelEventListener : BaseLlmModelEventListener
    {
        #region Properties: Protected

        protected override string ModelPrefix => "openai/";

        #endregion

        #region Methods: Protected

        protected override void SyncProviderConfig(Entity virtualEntity, Entity llmModel) {
            string model = virtualEntity.GetTypedColumnValue<string>("Model");
            if (string.IsNullOrWhiteSpace(model)) {
                throw new ArgumentException("OpenAI Model is required.", "Model");
            }
            string apiKey = virtualEntity.GetTypedColumnValue<string>("ApiKey");
            if (string.IsNullOrWhiteSpace(apiKey)) {
                throw new ArgumentException("OpenAI API Key is required for authentication.", "ApiKey");
            }
            var config = new OpenAIModelJson();
            string normalizedModel = EnsureModelPrefix(model);
            config.Model = normalizedModel;
            config.ApiKey = apiKey;
            llmModel.SetColumnValue("EncryptedConfig", config.ToJson());
        }

        #endregion
    }

    #endregion
}

