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

	#region Class: UpdateConfigurationActivityLogRetentionPeriodMethodsWrapper

	/// <exclude/>
	public class UpdateConfigurationActivityLogRetentionPeriodMethodsWrapper : ProcessModel
	{

		public UpdateConfigurationActivityLogRetentionPeriodMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			// Read process parameter
			int inputValue = Get<int>("UpdatedConfigurationActivityLogRetentionPeriodValue");
			
			// Clamp value to allowed minimal value - 0
			int safeValue;
			if (inputValue < 0) {
				safeValue = 0;
			} else {
				safeValue = inputValue;
			}
			
			// Update default value of the system setting
			const string sysSettingCode = "ConfigurationActivityLogRetentionPeriod";
			Terrasoft.Core.Configuration.SysSettings.SetDefValue(
				UserConnection,
				sysSettingCode,
				safeValue
			);
			
			return true;
		}

		#endregion

	}

	#endregion

}

