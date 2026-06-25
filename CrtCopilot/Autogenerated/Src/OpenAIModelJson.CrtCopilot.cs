namespace Terrasoft.Configuration
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    [DataContract]
    public class OpenAIModelJson : LlmModelJson
    {
        #region Properties: Public

        [DataMember(Name = "api_key")]
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        #endregion
    }
}

