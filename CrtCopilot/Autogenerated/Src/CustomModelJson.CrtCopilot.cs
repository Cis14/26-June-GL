namespace Terrasoft.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [DataContract]
    public class CustomModelJson : LlmModelJson
    {
        #region Properties: Public

        /// <summary>
        /// Stores all additional JSON properties at the root level beyond the base "model" field.
        /// This allows CustomModelJson to support dynamic fields like aws_access_key_id, aws_region_name, etc.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JToken> AdditionalData { get; set; } = new Dictionary<string, JToken>();

        #endregion

        #region Methods: Public

        public new static CustomModelJson FromJson(string json) {
            return FromJson<CustomModelJson>(json);
        }

        public new string ToJson() {
            var settings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            return JsonConvert.SerializeObject(this, settings);
        }

        #endregion
    }
}

