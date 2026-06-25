namespace Terrasoft.Core.Process
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Drawing;
	using System.Globalization;
	using System.Text;
	using System.Threading.Tasks;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Configuration;
	using Terrasoft.Web.Security.Csp;
	using Terrasoft.Web.Security.Csp.Extensions;

	#region Class: SysCspUserTrustedSrc_ActualizeCspHeaderMethodsWrapper

	/// <exclude/>
	public class SysCspUserTrustedSrc_ActualizeCspHeaderMethodsWrapper : ProcessModel
	{

		public SysCspUserTrustedSrc_ActualizeCspHeaderMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			var userConnection = this.Get<UserConnection>("UserConnection");
			var cspHeaderActualizer = userConnection.GetCspHeaderActualizer();
			cspHeaderActualizer.ActualizeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
			return true;
		}

		#endregion

	}

	#endregion

}

