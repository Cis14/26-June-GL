namespace Terrasoft.Configuration
{
    using System;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents Azure OpenAI-specific configuration extending base LLM Model configuration.
    /// Based on LiteLLM Azure OpenAI provider configuration.
    /// </summary>
    [DataContract]
    public class AzureOpenAIModelJson : LlmModelJson
    {
        #region Properties: Public

        /// <summary>
        /// Azure OpenAI API key for authentication.
        /// </summary>
        [DataMember(Name = "api_key")]
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        /// <summary>
        /// Azure OpenAI endpoint URL (e.g., https://your-resource.openai.azure.com/).
        /// This property is calculated automatically based on ResourceName.
        /// </summary>
        [DataMember(Name = "api_base")]
        [JsonProperty("api_base")]
        public string ApiBase {
            get {
                if (string.IsNullOrWhiteSpace(ResourceName)) {
                    return null; 
                }
                return $"https://{ResourceName}.openai.azure.com/";
            }
            set {
                ResourceName = null; 
                if (string.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (Uri.TryCreate(value, UriKind.Absolute, out Uri uri) && uri.Host.EndsWith(".openai.azure.com", StringComparison.OrdinalIgnoreCase)) {
                    string host = uri.Host;
                    int index = host.IndexOf('.');
                    if (index > 0) {
                        ResourceName = host.Substring(0, index);
                    }
                }
            }
        }
        
        /// <summary>
        /// The unique name of the Azure resource provided by the user (e.g., my-openai-instance).
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Azure OpenAI API version (e.g., 2024-02-15-preview).
        /// Required for Azure OpenAI.
        /// </summary>
        [DataMember(Name = "api_version")]
        [JsonProperty("api_version")]
        public string ApiVersion { get; set; }

        #endregion

        #region Methods: Public

        public new static AzureOpenAIModelJson FromJson(string json) {
            return FromJson<AzureOpenAIModelJson>(json);
        }

        #endregion
    }
}
