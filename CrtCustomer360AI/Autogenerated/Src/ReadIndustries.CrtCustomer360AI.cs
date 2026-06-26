namespace Terrasoft.Core.Process
{

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

	#region Class: ReadIndustriesMethodsWrapper

	/// <exclude/>
	public class ReadIndustriesMethodsWrapper : ProcessModel
	{

		public ReadIndustriesMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			var compositeObject = Get<CompositeObjectList<CompositeObject>>("ReadDataUserTask1.ResultCompositeObjectList");
			var namesCollection = compositeObject.Select(s => s["Name"]).ToList();
			var resultingString = string.Join(", ", namesCollection);
			Set<string>("ConcatenatedIndustries", resultingString);
			return true;
		}

		#endregion

	}

	#endregion

}

