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

	#region Class: GetEntitiesFromSearchMethodsWrapper

	/// <exclude/>
	public class GetEntitiesFromSearchMethodsWrapper : ProcessModel
	{

		public GetEntitiesFromSearchMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			IEnumerable<CompositeObject> collection = Get<List<CompositeObject>>("ResultCollection");
			Guid entityId = Guid.Empty;
			string searchEntityName = Get<string>("SearchEntityName");
			collection.ForEach((item) => {
				string entityName = item.GetTypedValue<string>("EntityName");
				if (entityName == searchEntityName && entityId == Guid.Empty) {
					entityId = Guid.Parse(item.GetTypedValue<string>("Id"));
				}
			});
			Set("RecordId", entityId);
			return true;
		}

		#endregion

	}

	#endregion

}

