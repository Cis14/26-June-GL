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

	#region Class: SaveNewApiKeyMethodsWrapper

	/// <exclude/>
	public class SaveNewApiKeyMethodsWrapper : ProcessModel
	{

		public SaveNewApiKeyMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("SaveNewAPIkeyTaskExecute", SaveNewAPIkeyTaskExecute);
		}

		#region Methods: Private

		private bool SaveNewAPIkeyTaskExecute(ProcessExecutingContext context) {
			var bingApiKey = Get<string>("BingApiKey");
			SysSettings.SetDefValue(UserConnection, "BingSearchApiKey", bingApiKey);
			return true;
		}

		#endregion

	}

	#endregion

}

