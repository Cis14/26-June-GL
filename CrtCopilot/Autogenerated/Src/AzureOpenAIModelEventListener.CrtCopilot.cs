namespace Terrasoft.Configuration
{
    using System;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Entities.Events;

    #region Class: AzureOpenAIModelEventListener

    /// <summary>
    /// Entity event listener for AzureOpenAIModel virtual entity.
    /// </summary>
    [EntityEventListener(SchemaName = "AzureOpenAIModel")]
    public class AzureOpenAIModelEventListener : BaseLlmModelEventListener
    {
        #region Properties: Protected

        protected override string ModelPrefix => "azure/";

        #endregion

        #region Methods: Private

        /// <summary>
        /// Validates the resource name against basic Azure naming rules.
        /// </summary>
        /// <param name="name">The resource name to validate.</param>
        /// <returns>True if the name is valid, false otherwise.</returns>
		private bool IsValidResourceName(string name) {
		    if (name.Length < 2 || name.Length > 64) {
		        return false;
		    }
		    if (!char.IsLetterOrDigit(name[0]) || !char.IsLetterOrDigit(name[name.Length - 1])) {
		        return false;
		    }
		    foreach (char c in name) {
		        if (!char.IsLetterOrDigit(c) && c != '-') {
		            return false;
		        }
		    }
		    return true;
		}

        #endregion

        #region Methods: Protected

        protected override void SyncProviderConfig(Entity virtualEntity, Entity llmModel) {
            string model = virtualEntity.GetTypedColumnValue<string>("Model");
            if (string.IsNullOrWhiteSpace(model)) {
                throw new ArgumentException("Azure OpenAI Model (Deployment ID) is required.", "Model");
            }
            string apiKey = virtualEntity.GetTypedColumnValue<string>("ApiKey");
            if (string.IsNullOrWhiteSpace(apiKey)) {
                throw new ArgumentException("Azure OpenAI API Key is required for authentication.", "ApiKey");
            }
            string resourceName = virtualEntity.GetTypedColumnValue<string>("ResourceName");
            if (string.IsNullOrWhiteSpace(resourceName)) {
                throw new ArgumentException("Azure resource name is required to form the API endpoint URL.", "ResourceName");
            }
			if (!IsValidResourceName(resourceName)) {
			    throw new ArgumentException(
			        "Azure resource name is invalid. It must be 2-64 characters, contain only letters, numbers, and hyphens, and must start and end with a letter or number.",
			        "ResourceName");
			}
            string apiVersion = virtualEntity.GetTypedColumnValue<string>("ApiVersion");
            if (string.IsNullOrWhiteSpace(apiVersion)) {
                throw new ArgumentException("Azure API Version is required.", "ApiVersion");
            }
            var config = new AzureOpenAIModelJson();
            string normalizedModel = EnsureModelPrefix(model);
            config.Model = normalizedModel;
            config.ApiKey = apiKey;
            config.ResourceName = resourceName;
            config.ApiVersion = apiVersion;
            llmModel.SetColumnValue("EncryptedConfig", config.ToJson());
        }

        #endregion

    }

    #endregion
}

