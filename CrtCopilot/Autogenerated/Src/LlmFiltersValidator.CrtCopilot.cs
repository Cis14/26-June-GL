namespace Creatio.Copilot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json.Linq;
    using Terrasoft.Core;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Factories;

    #region Interfaces: Public

    public interface ILlmFiltersValidator
    {
        IList<string> Validate(LLMUnknownFilterResponseContract filterGroup, string rootSchemaName);
    }

    #endregion

    #region Class: LlmFiltersValidator

    [DefaultBinding(typeof(ILlmFiltersValidator))]
    public class LlmFiltersValidator : ILlmFiltersValidator
    {

        #region Constants: Private

        private const string RecommendationToGetValidIds =
            "Use [ActionGetLookupsValue] or [SearchEntities] actions to get valid IDs or use filter by other column (e.g. by Name).";

        private const string RecommendationToGetValidSchemaNames =
            "Try to use [ActionRetrieveSimilarTableNames] to get valid schema names.";

        private const string RecommendationToGetValidColumnNames =
            "Use [ActionRetrieveTableColumnsDetails] to get valid column names.";

        #endregion

        #region Fields: Private

        private readonly UserConnection _userConnection = ClassFactory.Get<UserConnection>();
        private readonly ILlmColumnPathValidator _columnPathValidator = ClassFactory.Get<ILlmColumnPathValidator>();
        private readonly ILlmColumnPathHelper _columnPathHelper = ClassFactory.Get<ILlmColumnPathHelper>();

        #endregion

        #region Methods: Private

        private bool IsFilterGroup(LLMUnknownFilterResponseContract filter) {
            return filter?.logicalOperation != null;
        }

        private void ValidateFilterGroup(LLMUnknownFilterResponseContract filterGroup, string rootSchemaName,
            IList<string> errors) {
            if (!_columnPathHelper.TableExists(rootSchemaName)) {
                errors.Add($"Table '{rootSchemaName}' does not exist.");
                return;
            }
            if (filterGroup.filters != null) {
                foreach (LLMUnknownFilterResponseContract t in filterGroup.filters) {
                    if (IsFilterGroup(t)) {
                        ValidateFilterGroup(t, rootSchemaName, errors);
                    } else {
                        ValidateComparisonFilter(t, rootSchemaName, errors);
                    }
                }
            }
            if (filterGroup.backwardReferenceFilters != null) {
                foreach (LLMUnknownFilterResponseContract t in filterGroup.backwardReferenceFilters) {
                    ValidateBackwardReferenceFilter(t, rootSchemaName, errors);
                }
            }
        }

        private void ValidateComparisonFilter(LLMUnknownFilterResponseContract filter, string rootSchemaName,
            IList<string> errors) {
            if (_columnPathHelper.ColumnPathHasBackwardReference(filter.columnPath)) {
                errors.Add(
                    $"Column path '{filter.columnPath}' should not contain backward references. Use backwardReferenceFilters or remove backward reference.");
                return;
            }
            if (!ValidateColumnPath(filter.columnPath, rootSchemaName, errors)) {
                return;
            }
            if (IsNullFilter(filter)) {
                return;
            }
            if (IsIdFilter(filter, rootSchemaName)) {
                ValidateIds(filter, rootSchemaName, errors);
            }
        }

        private bool IsNullFilter(LLMUnknownFilterResponseContract filter) {
            return filter.comparisonType == "IS_NULL" || filter.comparisonType == "IS_NOT_NULL";
        }

        private void ValidateBackwardReferenceFilterAggregationValue(LLMUnknownFilterResponseContract backwardFilter,
            IList<string> errors) {
            if (backwardFilter.aggregationType == "EXISTS" || backwardFilter.aggregationType == "NOT_EXISTS") {
                return;
            }
            if (string.IsNullOrEmpty(backwardFilter.aggregationValue)) {
                errors.Add($"Backward reference filter for '{backwardFilter.columnPath}' must have aggregationValue.");
            }
        }

        private void ValidateBackwardReferenceFilter(LLMUnknownFilterResponseContract backwardFilter,
            string rootTableName, IList<string> errors) {
            if (backwardFilter is null) {
                return;
            }
            if (!_columnPathHelper.ColumnPathHasBackwardReference(backwardFilter.columnPath)) {
                errors.Add(
                    $"Column path '{backwardFilter.columnPath}' in backward reference filter must contain a backward reference. Add a backward reference or use `filters` instead of `backwardReferenceFilters`.");
                return;
            }
            ValidateBackwardReferenceFilterAggregationValue(backwardFilter, errors);
            if (!ValidateColumnPath(backwardFilter.columnPath, rootTableName, errors)) {
                return;
            }
            if (backwardFilter.subFilters != null) {
                string backwardRefTable = _columnPathHelper.GetLastBackwardReferenceTable(backwardFilter.columnPath);
                ValidateFilterGroup(backwardFilter.subFilters, backwardRefTable, errors);
            }
        }

        private bool ValidateColumnPath(string columnPath, string entitySchemaName, IList<string> errors) {
            IList<string> columnPathErrors = _columnPathValidator.Validate(columnPath, entitySchemaName);
            foreach (var error in columnPathErrors) {
                errors.Add(error);
            }
            return !columnPathErrors.Any();
        }

        private bool IsIdColumn(EntitySchemaColumn schemaColumn) {
            var parentSchema = schemaColumn.ParentMetaSchema as EntitySchema;
            return schemaColumn.IsLookupType || parentSchema.PrimaryColumn.Name == schemaColumn.Name;
        }

        private bool IsIdFilter(LLMUnknownFilterResponseContract inputFilter, string entitySchemaName) {
            var column = _columnPathHelper.FindEntitySchemaColumnByPath(inputFilter.columnPath, entitySchemaName);
            return IsIdColumn(column);
        }

        private EntitySchema GetColumnReferenceSchema(EntitySchemaColumn schemaColumn) {
            var parentSchema = schemaColumn.ParentMetaSchema as EntitySchema;
            if (schemaColumn.IsLookupType) {
                return schemaColumn.ReferenceSchema;
            }
            if (parentSchema?.PrimaryColumn?.Name == schemaColumn.Name) {
                return parentSchema;
            }
            return null;
        }

        private void ValidateIds(LLMUnknownFilterResponseContract filter, string entitySchemaName,
            IList<string> errors) {
            EntitySchemaColumn column = _columnPathHelper.FindEntitySchemaColumnByPath(filter.columnPath, entitySchemaName);
            if (!TryExtractIds(filter.value, out IEnumerable<string> ids, errors)) {
                errors.Add($"Lookup filter value for '{filter.columnPath}' must be a GUID. Got: '{filter.value}'.\n" +
                    RecommendationToGetValidIds);
                return;
            }
            foreach (var idStr in ids) {
                if (!Guid.TryParse(idStr, out Guid id)) {
                    errors.Add($"Lookup filter value for '{filter.columnPath}' must be a GUID. Got: '{idStr}'.\n" +
                        RecommendationToGetValidIds);
                    continue;
                }
                EntitySchema referenceSchema = GetColumnReferenceSchema(column);
                if (!RecordExists(referenceSchema, id)) {
                    errors.Add(
                        $"Lookup record with Id '{id}' not found in '{referenceSchema.Name}' for column '{filter.columnPath}'.\n" +
                        RecommendationToGetValidIds);
                }
            }
        }

        private static bool TryExtractIds(object value, out IEnumerable<string> ids, IList<string> errors) {
            if (value is string s) {
                ids = new[] { s };
                return true;
            }
            if (value is string[] sa) {
                ids = sa;
                return true;
            }
            if (value is object[] oa && oa.All(x => x is string)) {
                ids = oa.Cast<string>();
                return true;
            }
            if (value is JArray ja && ja.All(x => x.Type == JTokenType.String)) {
                ids = ja.Select(x => x.Value<string>());
                return true;
            }
            ids = Array.Empty<string>();
            return false;
        }

        private bool RecordExists(EntitySchema schema, Guid id) {
            var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, schema.Name);
            esq.PrimaryQueryColumn.IsVisible = true;
            esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, esq.PrimaryQueryColumn.Name,
                id));
            var entities = esq.GetEntityCollection(_userConnection);
            return entities.Count > 0;
        }

        private string FixPath(string path) {
            var segments = (path ?? string.Empty).Split('.').Select(part =>
                part.Length > 2 && part.EndsWith("Id", StringComparison.Ordinal)
                    ? part.Substring(0, part.Length - 2)
                    : part);
            return string.Join(".", segments);
        }

        #endregion

        #region Methods: Public

        public IList<string> Validate(LLMUnknownFilterResponseContract filterGroup, string rootSchemaName) {
            var errors = new List<string>();
            ValidateFilterGroup(filterGroup, rootSchemaName, errors);
            return errors;
        }

        #endregion

    }

    #endregion

}

