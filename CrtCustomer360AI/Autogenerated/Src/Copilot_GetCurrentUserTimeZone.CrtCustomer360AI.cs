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

	#region Class: Copilot_GetCurrentUserTimeZoneMethodsWrapper

	/// <exclude/>
	public class Copilot_GetCurrentUserTimeZoneMethodsWrapper : ProcessModel
	{

		public Copilot_GetCurrentUserTimeZoneMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			Set<string>("TimeZone", UserConnection.CurrentUser.TimeZoneId);
			return true;
		}

		#endregion

	}

	#endregion

}

