namespace Terrasoft.Core.Process
{

	using Creatio.Copilot;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;
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

	#region Class: Process_3d0825bMethodsWrapper

	/// <exclude/>
	public class Process_3d0825bMethodsWrapper : ProcessModel
	{

		public Process_3d0825bMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
			AddScriptTaskMethod("ScriptTask3Execute", ScriptTask3Execute);
			AddScriptTaskMethod("ScriptTask5Execute", ScriptTask5Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
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
			//   sqlToRawExamples (Text)
			
			var selectedIdsJson = Get<string>("SelectedIdsJson");
			var examplesJson = Get<string>("Examples");
			
			if (string.IsNullOrWhiteSpace(selectedIdsJson)) {
				Set("sqlToRawExamples", "SelectedIdsJson is empty.");
				return true;
			}
			if (string.IsNullOrWhiteSpace(examplesJson)) {
				Set("sqlToRawExamples", "Examples JSON is empty.");
				return true;
			}
			
			try {
				// Deserialize simple list of IDs
				var selectedIds = JsonConvert.DeserializeObject<List<string>>(selectedIdsJson)
					?? new List<string>();
			
				// Deserialize the examples list (array of objects)
				var examples = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(examplesJson)
					?? new List<Dictionary<string, object>>();
			
				var sbSqlRaw = new StringBuilder();
			
				foreach (var id in selectedIds) {
					var record = examples.FirstOrDefault(e =>
						e.ContainsKey("ID") &&
						string.Equals(e["ID"]?.ToString(), id, StringComparison.OrdinalIgnoreCase));
			
					if (record == null) {
						continue;
					}
			
					string GetVal(string key) =>
						record.ContainsKey(key) && record[key] != null ? record[key].ToString() : string.Empty;
			
					var sql = GetVal("SQL Query");
					var raw = GetVal("Raw JSON Filter");
			
					sbSqlRaw.AppendLine($"SQL Query: {sql}");
					sbSqlRaw.AppendLine($"Raw JSON: {raw}");
					sbSqlRaw.AppendLine();
				}
			
				Set("sqlToRawExamples", sbSqlRaw.ToString());
			}
			catch (Exception ex) {
				Set("sqlToRawExamples", $"Error: {ex.Message}");
			}
			
			return true;
		}

		private bool ScriptTask5Execute(ProcessExecutingContext context) {
			var llmEsqConverter = ClassFactory.Get<ILlmEsqConverter>();
			var filterJson = Get<string>("rawJsonFilter");
			var llmFilterRequest = JsonConvert.DeserializeObject<LlmFilterRequest>(filterJson);
			var serializedFilter = llmEsqConverter.ConvertToEsqFilter(llmFilterRequest.filter, llmFilterRequest.rootSchemaName);
			
			// Serialize while ignoring nulls
			var filter = JsonConvert.SerializeObject(serializedFilter, new JsonSerializerSettings {
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
			    NullValueHandling = NullValueHandling.Ignore
			});
			
			Set("esqJsonFilter", filter);
			return true;
			 
		}

		#endregion

	}

	#endregion

}

