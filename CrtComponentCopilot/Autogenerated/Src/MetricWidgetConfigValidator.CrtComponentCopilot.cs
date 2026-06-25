namespace Creatio.ComponentCopilot
{
    using System;
    using System.Collections.Generic;
    using Creatio.Copilot;
    using Terrasoft.Core.Factories;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    #region Class: MetricWidgetConfigValidator

    [DefaultBinding(typeof(IViewElementConfigValidator), Name = "MetricWidgetConfigJsonSchemaValidator")]
    public class MetricWidgetConfigValidator : IViewElementConfigValidator
    {

        #region Methods: Private

        private void ValidateFilters(JObject config, List<string> errors) {
            var filtersNode = config["element"]["data"]["filters"];
            var fromNode = config["element"]["data"]["from"];
            var rootSchemaName = fromNode.Value<string>();
            var filtersJson = filtersNode.ToString();
            LLMUnknownFilterResponseContract filtersObject;
            try {
                filtersObject = JsonConvert.DeserializeObject<LLMUnknownFilterResponseContract>(filtersJson);
            } catch (Exception ex) {
                errors.Add("Filters section has invalid structure. " + ex);
                return;
            }
            var filtersValidator = ClassFactory.Get<ILlmFiltersValidator>();
            errors.AddRange(filtersValidator.Validate(filtersObject, rootSchemaName));
        }

        #endregion

        #region Methods: Public

        public IList<string> Validate(string configJson) {
            var errors = new List<string>();
            try {
                JObject config = JObject.Parse(configJson);
                ValidateFilters(config, errors);
            } catch (Exception ex) {
                errors.Add("Metric widget config is not valid: " + ex.Message);
            }
            return errors;
        }

        #endregion

    }

    #endregion

}

