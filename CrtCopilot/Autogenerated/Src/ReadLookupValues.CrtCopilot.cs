namespace Terrasoft.Core.Process
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Drawing;
	using System.Globalization;
	using System.Linq;
	using System.Text;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Configuration;

	#region Class: ReadLookupValuesMethodsWrapper

	/// <exclude/>
	public class ReadLookupValuesMethodsWrapper : ProcessModel
	{

		public ReadLookupValuesMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
			AddScriptTaskMethod("ScriptTask2Execute", ScriptTask2Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			var entitySchemaUId = Get<Guid>("LookupSchema");
			var userConnection = Get<UserConnection>("UserConnection");
			var entitySchema = userConnection.EntitySchemaManager.FindInstanceByUId(entitySchemaUId);
			var esq = new EntitySchemaQuery(entitySchema);
			esq.RowCount = 100;
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

		private bool ScriptTask2Execute(ProcessExecutingContext context) {
			var entitySchemaUId = Get<Guid>("LookupSchema");
			var userConnection = Get<UserConnection>("UserConnection");
			var entitySchema = userConnection.EntitySchemaManager.FindInstanceByUId(entitySchemaUId);
			var displayColumnName = entitySchema.PrimaryDisplayColumn.Name;
			var compositeObject = Get<CompositeObjectList<CompositeObject>>("LookupRecords");
			var namesCollection = compositeObject.Select(s => s[displayColumnName]).ToList();
			var resultingString = string.Join(", ", namesCollection);
			Set("ConcatenatedRecords", resultingString);
			return true;
		}

		#endregion

	}

	#endregion

}

