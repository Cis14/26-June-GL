namespace Terrasoft.Configuration
{
    using System;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    [DataContract]
    public class LlmModelJson
    {
        #region Properties: Public

        [DataMember(Name = "model")]
        [JsonProperty("model")]
        public string Model { get; set; }

        #endregion

        #region Methods: Public

        public string ToJson() {
            var settings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
            return JsonConvert.SerializeObject(this, Formatting.None, settings);
        }

        public static T FromJson<T>(string json) where T : LlmModelJson, new() {
            if (string.IsNullOrWhiteSpace(json)) {
                return new T();
            }
            try {
                return JsonConvert.DeserializeObject<T>(json) ?? new T();
            } catch {
                return new T();
            }
        }

        public static LlmModelJson FromJson(string json) {
            return FromJson<LlmModelJson>(json);
        }

        #endregion
    }
}

