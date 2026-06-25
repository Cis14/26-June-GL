namespace Terrasoft.Core.Process
{

	using Creatio.Copilot;
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Drawing;
	using System.Globalization;
	using System.Linq;
	using System.Text;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Configuration;

	#region Class: GenerateFilterJsonConfiguration_5180a35MethodsWrapper

	/// <exclude/>
	public class GenerateFilterJsonConfiguration_5180a35MethodsWrapper : ProcessModel
	{

		public GenerateFilterJsonConfiguration_5180a35MethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
			AddScriptTaskMethod("ScriptTask2Execute", ScriptTask2Execute);
			AddScriptTaskMethod("ScriptTask3Execute", ScriptTask3Execute);
			AddScriptTaskMethod("ScriptTask4Execute", ScriptTask4Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			var columns = Get<object>("tableColumnDetails");
			var result = JsonConvert.SerializeObject(columns);
			Set("tableColumnDetailsString", result);
			return true;
		}

		private bool ScriptTask2Execute(ProcessExecutingContext context) {
			// Read JSON input from process parameter
			var jsonText = Get<string>("Examples");
			
			// Validate input
			if (string.IsNullOrWhiteSpace(jsonText))
			{
				Set("examplesStr", "Input is empty or whitespace.");
				return true;
			}
			
			try
			{
				// Parse the JSON array
				var items = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonText);
			
				// Build formatted output
				var sb = new System.Text.StringBuilder();
			
				foreach (var item in items)
				{
					// Extract fields (if missing, mark as empty)
					var id = item.ContainsKey("ID") ? item["ID"]?.ToString() : string.Empty;
					var userQuery = item.ContainsKey("User Query") ? item["User Query"]?.ToString() : string.Empty;
			
					// Add formatted lines
					sb.AppendLine($"ID: {id}");
					sb.AppendLine($"User Query: {userQuery}");
					sb.AppendLine(); // blank line between records
				}
			
				// Save formatted text to process parameter
				Set("examplesStr", sb.ToString());
			}
			catch (Exception ex)
			{
				Set("examplesStr", $"Error parsing JSON: {ex.Message}");
			}
			
			return true;
		}

		private bool ScriptTask3Execute(ProcessExecutingContext context) {
			// Inputs:
			//   SelectedIdsJson (Text) -> e.g. ["SA1","DA1"]
			//   Examples (Text) -> full list JSON
			// Outputs:
			//   nlToSqlExamples (Text)
			
			var selectedIdsJson = Get<string>("SelectedIdsJson");
			var examplesJson = Get<string>("Examples");
			
			if (string.IsNullOrWhiteSpace(selectedIdsJson)) {
				Set("nlToSqlExamples", "SelectedIdsJson is empty.");
				return true;
			}
			if (string.IsNullOrWhiteSpace(examplesJson)) {
				Set("nlToSqlExamples", "Examples JSON is empty.");
				return true;
			}
			
			try {
				// Deserialize simple list of IDs
				var selectedIds = JsonConvert.DeserializeObject<List<string>>(selectedIdsJson)
					?? new List<string>();
			
				// Deserialize the examples list (array of objects)
				var examples = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(examplesJson)
					?? new List<Dictionary<string, object>>();
			
				var nlToSql = new StringBuilder();
			
				foreach (var id in selectedIds) {
					var record = examples.FirstOrDefault(e =>
						e.ContainsKey("ID") &&
						string.Equals(e["ID"]?.ToString(), id, StringComparison.OrdinalIgnoreCase));
			
					if (record == null) {
						continue;
					}
			
					string GetVal(string key) =>
						record.ContainsKey(key) && record[key] != null ? record[key].ToString() : string.Empty;
					
					var nl = GetVal("User Query");
					var sql = GetVal("SQL Query");
					var guidance = GetVal("Guidance");
			
					nlToSql.AppendLine($"User Query: {nl}");
					nlToSql.AppendLine($"SQL Query: {sql}");
					nlToSql.AppendLine($"Guidance: {guidance}");
					nlToSql.AppendLine();
				}
			
				Set("nlToSqlExamples", nlToSql.ToString());
			}
			catch (Exception ex) {
				Set("nlToSqlExamples", $"Error: {ex.Message}");
			}
			
			return true;
		}

		private bool ScriptTask4Execute(ProcessExecutingContext context) {
			var columns = Get<object>("SearchEntitiesResult");
			var result = JsonConvert.SerializeObject(columns);
			Set("SearchEntitiesResultStr", result);
			return true;
		}

		#endregion

	}

	#endregion

}

