namespace Terrasoft.Core.Process
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Drawing;
	using System.Globalization;
	using System.Text;
	using Terrasoft.Common;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Configuration;

	#region Class: SaveNewsInsightPreferencesMethodsWrapper

	/// <exclude/>
	public class SaveNewsInsightPreferencesMethodsWrapper : ProcessModel
	{

		public SaveNewsInsightPreferencesMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			var constructorArgument = new ConstructorArgument("userConnection", UserConnection);
			var insightsJobDispatcher = ClassFactory.Get<InsightsJobDispatcher>(constructorArgument);
			var frequency = Get<string>("Frequency");
			insightsJobDispatcher.ScheduleJob(frequency);
			return true;
		}

		#endregion

	}

	#endregion

}

