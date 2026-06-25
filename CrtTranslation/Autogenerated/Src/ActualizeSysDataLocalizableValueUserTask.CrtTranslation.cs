namespace Terrasoft.Core.Process.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Packages;
	using Terrasoft.Core.Process;

	#region Class: ActualizeSysDataLocalizableValueUserTask

	/// <exclude/>
	public partial class ActualizeSysDataLocalizableValueUserTask
	{

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {
			var utility = new PackageDataLocalizationUtilities(UserConnection);
			var result = utility.ActualizeAllSchemas(continueIfError: true);
			ProcessedSchemas = result.ProcessedSchemas;
			SchemasWithErrors = result.SchemasWithErrors;
			UpdatedSchemaNames = result.UpdatedSchemaNames;
			UpdatedColumnNames = result.UpdatedColumnNames;
			DeletedRecords = result.DeletedRecords;
			HasErrors = result.HasErrors;
			ResultMessage = result.HasErrors ? FailCase : SuccessfulCase;
			return true;
		}

		#endregion

		#region Methods: Public

		public override bool CompleteExecuting(params object[] parameters) {
			return base.CompleteExecuting(parameters);
		}

		public override void CancelExecuting(params object[] parameters) {
			base.CancelExecuting(parameters);
		}

		public override string GetExecutionData() {
			return string.Empty;
		}

		public override ProcessElementNotification GetNotificationData() {
			return base.GetNotificationData();
		}

		#endregion

	}

	#endregion

}

