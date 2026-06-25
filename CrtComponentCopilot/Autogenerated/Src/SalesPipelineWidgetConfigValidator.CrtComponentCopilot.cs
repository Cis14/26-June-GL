  namespace Creatio.ComponentCopilot
{
    using System;
    using System.Collections.Generic;
    using Creatio.Copilot;
    using Terrasoft.Core.Factories;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Terrasoft.Common;

    #region Class: FullPipelineWidgetConfigValidator

    [DefaultBinding(typeof(IViewElementConfigValidator), Name = "SalesPipelineWidgetConfigJsonSchemaValidator")]
    public class SalesPipelineWidgetConfigValidator : IViewElementConfigValidator
    {

        #region Methods: Private

        private void ValidateFilters(string rootSchemaName, JToken filters, IList<string> errors) {
            var filtersJson = filters.ToString();
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

        #endregion

        #region Methods: Public

        public IList<string> Validate(string configJson) {
            var errors = new List<string>();
            try {
				JObject config = JObject.Parse(configJson);
				ValidateFilters("Opportunity", config["element"]["opportunitiesFilters"], errors);
            } catch (Exception ex) {
                errors.Add("Full pipeline widget config is not valid: " + ex.Message);
            }
            return errors;
        }

        #endregion

    }

    #endregion

}

