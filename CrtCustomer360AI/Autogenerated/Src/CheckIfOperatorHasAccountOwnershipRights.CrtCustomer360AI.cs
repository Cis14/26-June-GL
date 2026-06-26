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

	#region Class: CheckIfOperatorHasAccountOwnershipRightsMethodsWrapper

	/// <exclude/>
	public class CheckIfOperatorHasAccountOwnershipRightsMethodsWrapper : ProcessModel
	{

		public CheckIfOperatorHasAccountOwnershipRightsMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask2Execute", ScriptTask2Execute);
		}

		#region Methods: Private

		private bool ScriptTask2Execute(ProcessExecutingContext context) {
			Set<Guid>("CurrentUserContactId", UserConnection.CurrentUser.ContactId);
			return true;
		}

		#endregion

	}

	#endregion

}

