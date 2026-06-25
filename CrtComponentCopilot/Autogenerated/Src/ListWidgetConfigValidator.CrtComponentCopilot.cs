namespace Creatio.ComponentCopilot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Creatio.Copilot;
    using Terrasoft.Core;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Factories;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Terrasoft.Common;

    #region Class: ListWidgetConfigValidator

    [DefaultBinding(typeof(IViewElementConfigValidator), Name = "ListWidgetConfigJsonSchemaValidator")]
    public class ListWidgetConfigValidator : IViewElementConfigValidator
    {

        #region Fields: Private

        private readonly ILlmColumnPathValidator _columnPathValidator = ClassFactory.Get<ILlmColumnPathValidator>();
        private readonly ILlmColumnPathHelper _columnPathHelper = ClassFactory.Get<ILlmColumnPathHelper>();
        private readonly ILlmFiltersValidator _filtersValidator = ClassFactory.Get<ILlmFiltersValidator>();
        
        #endregion

        #region Methods: Private

        private void ValidateFilters(JObject filterGroup, string rootSchemaName, IList<string> errors) {
            var filtersJson = filterGroup.ToString();
            LLMUnknownFilterResponseContract filtersObject;
            try {
                filtersObject = JsonConvert.DeserializeObject<LLMUnknownFilterResponseContract>(filtersJson);
            } catch (Exception ex) {
                errors.Add("Filters section has invalid structure. " + ex.Message);
                return;
            }
            var filterErrors = _filtersValidator.Validate(filtersObject, rootSchemaName);
            foreach (var error in filterErrors) {
                errors.Add(error);
            }
        }
        
        private void ValidateRootFilters(JObject config, IList<string> errors) {
            var filtersNode = config["data"]["filters"];
            var fromNode = config["data"]["from"];
            var rootSchemaName = fromNode?.Value<string>();
            if (filtersNode == null || string.IsNullOrEmpty(rootSchemaName)) {
                return;
            }
            ValidateFilters(filtersNode as JObject, rootSchemaName, errors);
        }
        
        private bool ValidateColumnPath(string columnPath, string rootTableName,
            IList<string> errors) {
            IList<string> columnPathErrors = _columnPathValidator.Validate(columnPath, rootTableName);
            foreach (string error in columnPathErrors) {
                errors.Add(error);
            }
            return !columnPathErrors.Any();
        }

        private void ValidateColumns(JObject config, IList<string> errors) {
            var columns = config["data"]["select"] as JArray;
            var fromNode = config["data"]["from"];
            var rootSchemaName = fromNode.Value<string>();
            foreach (var columnToken in columns) {
                var column = (JObject)columnToken;
                var columnPath = column["columnPath"].Value<string>();
                ValidateColumnPath(columnPath, rootSchemaName, errors);
                if (_columnPathHelper.ColumnPathHasBackwardReference(columnPath)) {
                    ValidateBackwardReferenceColumn(column, errors);
                } else if (column["aggregationFunction"] != null) {
                    errors.Add($@"""{columnPath}"" is not a backward reference path, but `aggregationFunction` is specified.");
                }
            }
        }
        
        private void ValidateBackwardReferenceColumn(JObject columnConfig, IList<string> errors) {
            var columnPath = columnConfig["columnPath"].Value<string>();
            if (columnConfig["aggregationFunction"] == null) {
                errors.Add(
                    $@"""{columnPath}"" is a backward reference path, but `aggregationFunction` is not specified.");
            }
            if (columnConfig["filter"] != null) {
                string backwardRefTable = _columnPathHelper.GetLastBackwardReferenceTable(columnPath);
                ValidateFilters(columnConfig["filter"] as JObject, backwardRefTable, errors);
            }
        }

        private void ValidateOrderBy(JObject config, IList<string> errors) {
            var orderBy = config["data"]["orderBy"] as JObject;
            var availableColumnPaths = (config["data"]["select"] as JArray)
                .Select(col => col["columnPath"].Value<string>()).ToArray();
            var columnPath = orderBy["columnPath"].Value<string>();
            if (!availableColumnPaths.Contains(columnPath)) {
                errors.Add($"Column by path '{columnPath}' used in orderBy is not present in select columns. " +
                    "Available columns: " + string.Join(", ", availableColumnPaths));
            }
        }

        #endregion

        #region Methods: Public

        public IList<string> Validate(string configJson) {
            var errors = new List<string>();
            try {
                JObject config = JObject.Parse(configJson);
                JObject elementConfig = (JObject)config["element"];
                ValidateColumns(elementConfig, errors);
                ValidateOrderBy(elementConfig, errors);
                ValidateRootFilters(elementConfig, errors);
            } catch (Exception ex) {
                errors.Add("List widget config is not valid: " + ex.Message);
            }
            return errors;
        }

        #endregion

    }

    #endregion

}

