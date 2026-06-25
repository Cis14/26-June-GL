namespace Terrasoft.Configuration
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Entities.Events;

    #region Class: CustomModelEventListener

    /// <summary>
    /// Entity event listener for CustomLlmModel virtual entity.
    /// </summary>
    [EntityEventListener(SchemaName = "CustomLlmModel")]
    public class CustomModelEventListener : BaseLlmModelEventListener
    {
        #region Constants: Private

        private const string JsonConfigColumn = "JsonConfig";
        private const string EncryptedConfigColumn = "EncryptedConfig";

        #endregion

        #region Properties: Protected

        protected override string ModelPrefix => string.Empty;

        #endregion

        #region Methods: Private - JSON Parsing

        private void ValidateJsonConfig(string jsonConfig) {
            if (string.IsNullOrWhiteSpace(jsonConfig)) {
                return;
            }
            try {
                JToken.Parse(jsonConfig);
            } catch (JsonReaderException ex) {
                throw new ArgumentException(
                    "Model configuration (JSON) is not valid. Please check the JSON syntax.", ex);
            }
        }

        #endregion

        #region Methods: Protected

        protected override void SyncProviderConfig(Entity virtualEntity, Entity llmModel) {
            if (!virtualEntity.IsColumnValueLoaded(JsonConfigColumn)) {
                return;
            }
            string jsonConfig = virtualEntity.GetTypedColumnValue<string>(JsonConfigColumn);
            ValidateJsonConfig(jsonConfig);
            var config = CustomModelJson.FromJson(jsonConfig);
            llmModel.SetColumnValue(EncryptedConfigColumn, config.ToJson());
        }

        #endregion

    }

    #endregion
}

