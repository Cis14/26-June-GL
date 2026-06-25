namespace Creatio.Copilot
{
    using System.Collections.Generic;
    using Terrasoft.Core.Factories;

    #region Interfaces: Public

    public interface ILlmColumnPathValidator
    {
        IList<string> Validate(string columnPath, string rootSchemaName);
    }

    #endregion

    #region Class: LlmColumnPathValidator

    [DefaultBinding(typeof(ILlmColumnPathValidator))]
    public class LlmColumnPathValidator : ILlmColumnPathValidator
    {

        #region Fields: Private

        private readonly ILlmColumnPathHelper _helper = ClassFactory.Get<ILlmColumnPathHelper>();
        
        #endregion

        #region Methods: Private
        
        private bool ValidateBackwardReference(string pathPart, string rootTableName, string fullColumnPath,
            IList<string> errors) {
            if (!_helper.TryParseBackwardReference(pathPart, out string tableName, out string columnName)) {
                errors.Add("Backward reference in column path '" + fullColumnPath +
                    "' has invalid format. Expected format: '[JoinedTable:JoinedTableRelationColumn]'. Got: '" +
                    pathPart + "'");
                return false;
            }
            if (!_helper.TableExists(tableName)) {
                errors.Add($"Error in column path '{fullColumnPath}'. Table '{tableName}' does not exist.");
                return false;
            }
            if (!_helper.ColumnExists(columnName, tableName)) {
                errors.Add(
                    $"Error in column path '{fullColumnPath}'. Table '{tableName}' does not have column '{columnName}'.");
                return false;
            }
            if (!_helper.IsLookupColumn(columnName, tableName)) {
                errors.Add(
                    $"Error in column path '{fullColumnPath}'. Column '{columnName}' in table '{tableName}' is not a lookup column.");
                return false;
            }
            string referencedTable = _helper.GetLookupColumnReferenceTable(columnName, tableName);
            if (referencedTable != rootTableName) {
                errors.Add(
                    $"Error in column path '{fullColumnPath}'. Table {tableName} cannot be joined to table {rootTableName} by column {columnName}', because column {columnName} refers to {referencedTable} table.");
                return false;
            }
            return true;
        }
        
        #endregion

        #region Methods: Public

        public IList<string> Validate(string columnPath, string rootTableName) {
            var errors = new List<string>();
            if (!_helper.TableExists(rootTableName)) {
                errors.Add($"Table '{rootTableName}' does not exist.");
                return errors;
            }
            string[] pathParts = columnPath.Split('.');
            string currentTableName = rootTableName;
            for (var i = 0; i < pathParts.Length; i++) {
                string currentPart = pathParts[i];
                bool isLastPart = i == pathParts.Length - 1;
                if (_helper.IsBackwardReferencePathPart(currentPart)) {
                    if (!ValidateBackwardReference(currentPart, currentTableName, columnPath, errors)) {
                        break;
                    }
                    if (isLastPart) {
                        errors.Add($"Column name was expected after '{currentPart}'.");
                    }
                    currentTableName = _helper.GetBackwardReferenceTable(currentPart);
                } else {
                    if (!_helper.ColumnExists(currentPart, currentTableName)) {
                        errors.Add($"Table '{currentTableName}' does not have column '{currentPart}'.");
                        break;
                    }
                    if (!isLastPart) {
                        if (!_helper.IsLookupColumn(currentPart, currentTableName)) {
                            errors.Add($"Column '{currentPart}' in table '{currentTableName}' is not a lookup column.");
                            break;
                        }
                        currentTableName = _helper.GetLookupColumnReferenceTable(currentPart, currentTableName);
                    }
                }
            }
            return errors;
        }

        #endregion

    }

    #endregion

}

