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

    public interface ILlmColumnPathHelper
    {
        EntitySchema FindEntitySchemaByName(string tableName);
        EntitySchemaColumn FindEntitySchemaColumnByPath(string columnPath, string tableName);
        bool ColumnExists(string columnName, string tableName);
        bool IsBackwardReferencePathPart(string pathPart);
        bool TableExists(string tableName);
        bool IsLookupColumn(string columnName, string tableName);
        string GetLookupColumnReferenceTable(string columnName, string tableName);

        bool TryParseBackwardReference(string pathPart, out string tableName, out string columnName);
        string GetBackwardReferenceTable(string pathPart);
        string GetLastBackwardReferenceTable(string columnPath);

        bool ColumnPathHasBackwardReference(string columnPath);
        string NormalizeColumnPath(string columnPath);
    }

    #endregion

    #region Class: LlmColumnPathHelper

    [DefaultBinding(typeof(ILlmColumnPathHelper))]
    public class LlmColumnPathHelper : ILlmColumnPathHelper
    {
        
        #region Fields: Private

        private readonly UserConnection _userConnection = ClassFactory.Get<UserConnection>();

        #endregion

        #region Methods: Public

        public EntitySchema FindEntitySchemaByName(string tableName) {
            return _userConnection.EntitySchemaManager.FindInstanceByName(tableName);
        }

        public EntitySchemaColumn FindEntitySchemaColumnByPath(string columnPath, string tableName) {
            EntitySchema entitySchema = FindEntitySchemaByName(tableName);
            string normalizedColumnPath = NormalizeColumnPath(columnPath);
            return entitySchema?.FindSchemaColumnByPath(normalizedColumnPath);
        }

        public bool ColumnExists(string columnName, string tableName) {
            return FindEntitySchemaColumnByPath(columnName, tableName) != null;
        }

        public bool IsBackwardReferencePathPart(string pathPart) {
            return pathPart.StartsWith("[") && pathPart.EndsWith("]");
        }
        
        public bool TableExists(string tableName) {
            return FindEntitySchemaByName(tableName) != null;
        }
        
        public bool IsLookupColumn(string columnName, string tableName) {
            EntitySchemaColumn schemaColumn = FindEntitySchemaColumnByPath(columnName, tableName);
            return schemaColumn != null && schemaColumn.DataValueType.IsLookup;
        }
        
        public string GetLookupColumnReferenceTable(string columnName, string tableName) {
            EntitySchemaColumn schemaColumn = FindEntitySchemaColumnByPath(columnName, tableName);
            return schemaColumn?.ReferenceSchema?.Name;
        }

        public bool TryParseBackwardReference(string pathPart, out string tableName, out string columnName) {
            string pattern = @"^\[(?<table>[A-Za-z_][A-Za-z0-9_]*)\:(?<column>[A-Za-z_][A-Za-z0-9_]*)\]$";
            Match match = Regex.Match(pathPart, pattern);
            if (match.Success) {
                tableName = match.Groups["table"].Value;
                columnName = match.Groups["column"].Value;
                return true;
            }
            tableName = null;
            columnName = null;
            return false;
        }

        public string GetBackwardReferenceTable(string pathPart) {
            return TryParseBackwardReference(pathPart, out string tableName, out string columnName) ? tableName : null;
        }
        
        public string GetLastBackwardReferenceTable(string columnPath) {
            string[] parts = columnPath.Split('.');
            for (int i = parts.Length - 1; i >= 0; i--) {
                if (IsBackwardReferencePathPart(parts[i])) {
                    return GetBackwardReferenceTable(parts[i]);
                }
            }
            return null;
        }
        
        public bool ColumnPathHasBackwardReference(string columnPath) {
            return columnPath.Split('.').Any(IsBackwardReferencePathPart);
        }
        
        public string NormalizeColumnPath(string columnPath) {
            IEnumerable<string> parts = columnPath.Split('.').Select(part => {
                if (part.Length > 2 && part.EndsWith("Id", StringComparison.Ordinal)) {
                    return part.Substring(0, part.Length - 2);
                }
                if (IsBackwardReferencePathPart(part) &&
                    TryParseBackwardReference(part, out string tableName, out string columnName)) {
                    if (columnName.EndsWith("Id", StringComparison.Ordinal)) {
                        columnName = columnName.Substring(0, columnName.Length - 2);
                    }
                    return $"[{tableName}:{columnName}]";
                }
                return part;
            });
            return string.Join(".", parts);
        }
        
        #endregion

    }

    #endregion

}

