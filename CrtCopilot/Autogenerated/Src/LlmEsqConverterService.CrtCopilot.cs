namespace Creatio.Copilot
{
    using System.Linq;
    using System.Reflection;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using System.Web.SessionState;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Terrasoft.Core.Factories;
    using Terrasoft.Web.Common;
    using Terrasoft.Nui.ServiceModel.DataContract;

    #region Class: LlmEsqConverterService

    /// <summary>
    /// Provides web-service for conversion LLM query format to ESQ and vice versa.
    /// </summary>
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class LlmEsqConverterService : BaseService, IReadOnlySessionState
    {

        #region Methods: Private

        /// <summary>
        /// Converts to JSON
        /// </summary>
        /// <returns>JSON string.</returns>
        private string ConvertToJson(object data) {
            var settings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver() {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
            };
            string jsonResponse = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented, settings);
            return jsonResponse;
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Converts LLM filter to ESQ filter.
        /// </summary>
        /// <returns>Filter.</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ConvertToEsqFilters", BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string ConvertToEsqFilters(LlmFilterRequest[] filterRequests) {
            var llmEsqConverter = ClassFactory.Get<ILlmEsqConverter>();
            SerializableFilters[] filters = filterRequests.Select((req) =>
                llmEsqConverter.ConvertToEsqFilter(req.filter, req.rootSchemaName)).ToArray();
            return ConvertToJson(filters);
        }

        /// <summary>
        /// Converts LLM column expression to ESQ column.
        /// </summary>
        /// <returns>Column.</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ConvertToEsqColumns", BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string ConvertToEsqColumns(string[] columnExpressions) {
            var llmEsqConverter = ClassFactory.Get<ILlmEsqConverter>();
            SerializableSelectQueryColumn[] columns = columnExpressions.Select(llmEsqConverter.ConvertToEsqColumn).ToArray();
            return ConvertToJson(columns);
        }

        #endregion

    }

    #endregion

    public class LlmFilterRequest
    {
        public LLMUnknownFilterResponseContract filter { get; set; }
        public string rootSchemaName { get; set; }
    }
}

