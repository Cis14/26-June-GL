namespace Terrasoft.Core.Process
{

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

	#region Class: ClearEntityDeleteEventLogMethodsWrapper

	/// <exclude/>
	public class ClearEntityDeleteEventLogMethodsWrapper : ProcessModel
	{

		public ClearEntityDeleteEventLogMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			var cleaner = new EntityDeleteEventLogCleaner(UserConnection);
			cleaner.Clear();
			return true;
		}

		#endregion

	}

	#endregion

}

