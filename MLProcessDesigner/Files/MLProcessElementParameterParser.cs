namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core.Process;

	public static class MLProcessElementParameterParser
	{
		private static readonly IDictionary<string, string> _elementEntityParameterDictionary =
			new Dictionary<string, string> {
				{ "ReadDataUserTask", "ResultEntity" },
				{ "AddDataUserTask", "EntitySchemaId" },
				{ "DeleteDataUserTask", "EntitySchemaId" },
				{ "ChangeDataUserTask", "EntitySchemaUId" },
				{ "ChangeAdminRightsUserTask", "EntitySchemaUId" },
				{ "LinkEntityToProcessUserTask", "EntitySchemaId" },
				{ "Terrasoft.Core.Process.ProcessSchemaStartSignalEvent", "EntitySchemaUId" }
			};

		public static bool TryGetEntityParameterName(string elementType, out string parameterName) {
			return _elementEntityParameterDictionary.TryGetValue(elementType, out parameterName);
		}

		public static Guid? GetElementEntitySchemaUId(string elementType,
				ICollection<ProcessSchemaParameter> parameters) {
			if (!TryGetEntityParameterName(elementType, out string parameterName)) {
				return null;
			}
			var parameter = parameters?.FirstOrDefault(e => e.Name == parameterName);
			if (parameter == null) {
				return null;
			}
			var value = parameter.SourceValue.Value;
			return Guid.TryParse(value, out var parsed) 
				? parsed
				: parameterName == "ResultEntity"
					? (Guid?)parameter.ReferenceSchemaUId
					: null;
		}
	}
}