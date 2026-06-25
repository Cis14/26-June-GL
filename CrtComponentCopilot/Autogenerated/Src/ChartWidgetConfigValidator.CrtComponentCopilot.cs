namespace Creatio.ComponentCopilot
{
    using System;
    using System.Collections.Generic;
    using Creatio.Copilot;
    using Terrasoft.Core.Factories;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Terrasoft.Common;

    #region Class: ChartWidgetConfigValidator

    [DefaultBinding(typeof(IViewElementConfigValidator), Name = "ChartWidgetConfigJsonSchemaValidator")]
    public class ChartWidgetConfigValidator : IViewElementConfigValidator
    {

        #region Methods: Private

        private void ValidateGrouping(JObject config, IList<string> errors) {
            JToken groupingNode = config["data"]["grouping"];
            if (groupingNode == null || String.IsNullOrEmpty(groupingNode.Value<string>())) {
                errors.Add(
                    "Series item does not contain data.grouping. If user didn't specify grouping, system can't build chart. Ask user to specify grouping.");
            }
        }

        private void ValidateFilters(JObject config, IList<string> errors) {
            var filtersNode = config["data"]["filters"];
            var fromNode = config["data"]["from"];
            var rootSchemaName = fromNode.Value<string>();
            var filtersJson = filtersNode.ToString();
            LLMUnknownFilterResponseContract filtersObject;
            try {
                filtersObject = JsonConvert.DeserializeObject<LLMUnknownFilterResponseContract>(filtersJson);
            } catch (Exception ex) {
                errors.Add("Filters section has invalid structure. " + ex.Message);
                return;
            }
            var filtersValidator = ClassFactory.Get<ILlmFiltersValidator>();
            errors.AddRange(filtersValidator.Validate(filtersObject, rootSchemaName));
        }

        private void ForEachSeries(JObject config, Action<JObject> action) {
            var seriesNode = config["element"]["series"];
            if (seriesNode is JArray seriesArray) {
                foreach (var item in seriesArray) {
                    if (item is JObject itemObj) {
                        action(itemObj);
                    }
                }
            }
        }

        #endregion

        #region Methods: Public

        public IList<string> Validate(string configJson) {
            var errors = new List<string>();
            try {
                JObject config = JObject.Parse(configJson);
                ForEachSeries(config, (item) => {
                    ValidateGrouping(item, errors);
                    ValidateFilters(item, errors);
                });
            } catch (Exception ex) {
                errors.Add("Chart widget config is not valid: " + ex.Message);
            }
            return errors;
        }

        #endregion

    }

    #endregion

}

