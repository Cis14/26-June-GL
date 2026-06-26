namespace Terrasoft.Core.Process
{

	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Drawing;
	using System.Globalization;
	using System.Text;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Configuration;

	#region Class: ParseInsightsMethodsWrapper

	/// <exclude/>
	public class ParseInsightsMethodsWrapper : ProcessModel
	{

		public ParseInsightsMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			var json = Get<string>("FormattedResult");
			var items = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
			var insights = new CompositeObjectList<CompositeObject>();
			foreach (var item in items) {
				item.TryGetValue("title", out var title);
				item.TryGetValue("description", out var description);
				var insight = new CompositeObject {
					{ "Title", title },
					{ "Description", description }
				};
				insights.Add(insight);
			} 
			Set("Insights", insights);
			return true;
		}

		#endregion

	}

	#endregion

}

