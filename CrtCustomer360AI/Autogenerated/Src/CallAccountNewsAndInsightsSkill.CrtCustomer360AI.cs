namespace Terrasoft.Core.Process
{

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
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Configuration;

	#region Class: CallAccountNewsAndInsightsSkillMethodsWrapper

	/// <exclude/>
	public class CallAccountNewsAndInsightsSkillMethodsWrapper : ProcessModel
	{

		public CallAccountNewsAndInsightsSkillMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			var candidates = Get<ICompositeObjectList<ICompositeObject>>("Candidates");
			var sb = new StringBuilder();
			
			foreach (ICompositeObject cont in candidates) {
				if (cont.TryGetValue<ICompositeObjectList<ICompositeObject>>("ContentParts", out ICompositeObjectList<ICompositeObject> contentParts)) {
					foreach (ICompositeObject part in contentParts) {
						if (part.TryGetValue<string>("Text", out string text)) {
							sb.AppendLine(text);
						}
					}
				}
			} 
			
			Set<string>("GeminiResponse", string.Join(" ", sb.ToString()));
			return true;
		}

		#endregion

	}

	#endregion

}

