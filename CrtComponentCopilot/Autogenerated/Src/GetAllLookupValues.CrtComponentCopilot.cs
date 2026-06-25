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

	#region Class: GetAllLookupValuesMethodsWrapper

	/// <exclude/>
	public class GetAllLookupValuesMethodsWrapper : ProcessModel
	{

		public GetAllLookupValuesMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			var entitySchemaName = Get<string>("EntitySchemaName");
			var userConnection = Get<UserConnection>("UserConnection");
			var entitySchema = userConnection.EntitySchemaManager.GetInstanceByName(entitySchemaName);
			var esq = new EntitySchemaQuery(entitySchema);
			esq.RowCount = 100;
			esq.PrimaryQueryColumn.IsVisible = true;
			esq.AddColumn(entitySchema.PrimaryDisplayColumn.Name);
			var collection = esq.GetEntityCollection(userConnection);
			var result = new CompositeObjectList<CompositeObject>();
			foreach (Entity entity in collection) {
				var compositeObject = new CompositeObject();
				IEnumerable<string> columnValueNames = entity.GetColumnValueNames();
				foreach (string columnValueName in columnValueNames) {
					var columnValue = entity.FindEntityColumnValue(columnValueName);
					if (columnValue.IsLoaded) {
						compositeObject[columnValueName] = columnValue.Value;
					}
				}
				result.Add(compositeObject);
			}
			Set("LookupRecords", new CompositeObjectList<CompositeObject>(result));
			return true;
		}

		#endregion

	}

	#endregion

}

