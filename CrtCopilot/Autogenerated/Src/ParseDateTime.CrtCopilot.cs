namespace Terrasoft.Core.Process
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Drawing;
	using System.Globalization;
	using System.Text;
	using Terrasoft.Common;
	using Terrasoft.Configuration.Enrichment;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Configuration;

	#region Class: ParseDateTimeMethodsWrapper

	/// <exclude/>
	public class ParseDateTimeMethodsWrapper : ProcessModel
	{

		public ParseDateTimeMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("RunDateTimeRecognizerUserTaskExecute", RunDateTimeRecognizerUserTaskExecute);
		}

		#region Methods: Private

		private bool RunDateTimeRecognizerUserTaskExecute(ProcessExecutingContext context) {
			var service = new EnrichmentService();
			var utterance = Get<string>("Utterance");
			var result = service.ParseDateTime(utterance);
			Set("ResultType", result.ResultType);
			Set("ClarificationMessage", result.ClarificationMessage);
			Set("UserDateTimeFormat", result.UserDateTimeFormat);
			var list = new CompositeObjectList<CompositeObject>();
			if (result.Interpretations != null) {
				foreach (var i in result.Interpretations) {
					var obj = new CompositeObject {
						["NeedsConfirmation"] = i.NeedsConfirmation,
						["NormalizedFrom"] = i.NormalizedFrom,
						["LocalEnd"] = i.LocalEnd,
						["UtcEnd"] = i.UtcEnd,
						["Origin"] = i.Origin,
						["Local"] = i.Local,
						["Type"] = i.Type,
						["Utc"] = i.Utc
					};
					list.Add(obj);
				}
			}
			Set("Interpretations", list);
			return true;
		}

		#endregion

	}

	#endregion

}

