namespace Terrasoft.Core.Process
{

	using Creatio.FeatureToggling;
	using global::Common.Logging;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Drawing;
	using System.Globalization;
	using System.Linq;
	using System.Text;
	using Terrasoft.Common;
	using Terrasoft.Configuration.Deduplication;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Configuration;

	#region Class: FindContactDuplicatesMethodsWrapper

	/// <exclude/>
	public class FindContactDuplicatesMethodsWrapper : ProcessModel
	{

		public FindContactDuplicatesMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("FindDuplicatesExecute", FindDuplicatesExecute);
		}

		#region Methods: Private

		private bool FindDuplicatesExecute(ProcessExecutingContext context) {
			var duplicatesSearchUtilities = GetDuplicatesSearchUtilities();
			var contacts = Get<CompositeObjectList<CompositeObject>>("Contacts");
			
			if (!ValidateFeatureAndRules(duplicatesSearchUtilities)) {
				Set("InsertedRecordsCount", contacts.Count);
				return true;
			}
			
			var accountId = Get<Guid>("AccountId");
			var entitySchema = UserConnection.EntitySchemaManager.GetInstanceByName("Contact");
			var rulesList = duplicatesSearchUtilities.GetActiveRules(entitySchema.Name);
			var columns = duplicatesSearchUtilities.GetColumnsFromRules(rulesList);
			if (accountId == Guid.Empty || contacts.Count == 0) {
				return true;
			}
			var searchColumns = contacts[0].Keys.ToList();
			
			foreach (var record in contacts.ToList()) {
			    var mappedColumns = MapColumns(record, searchColumns, columns);
			    try {
			    	var duplicatesCollection = duplicatesSearchUtilities.FindRecordDuplicates(mappedColumns, rulesList);
				    if (duplicatesCollection?.Any() == true) {
				        HandleDuplicates(duplicatesCollection, entitySchema, accountId);
				        contacts.Remove(record);
				    }
				} catch (Exception e) {
					_log.ErrorFormat("[FindContactDuplicates.FindDuplicatesExecute] exception: {0}", e);
				}
			}
			Set("InsertedRecordsCount", contacts.Count);
			Set("Contacts", new CompositeObjectList<CompositeObject>(contacts));
			return true;
		}

			private static readonly ILog _log = LogManager.GetLogger("Process");
			
			private DuplicatesSearchUtilities GetDuplicatesSearchUtilities() {
			    return ClassFactory.Get<DuplicatesSearchUtilities>(new[] {
			        new ConstructorArgument("userConnection", UserConnection),
			        new ConstructorArgument("entitySchemaName", "Contact")
			    });
			}
			
			private bool ValidateFeatureAndRules(DuplicatesSearchUtilities duplicatesSearchUtilities) {
				if (!Features.GetIsEnabled("ESDeduplication")) {
					_log.Info("[FindContactDuplicates.ValidateFeatureAndRules]: feature disabled");
				    return false;
				}
				var entitySchema = UserConnection.EntitySchemaManager.GetInstanceByName("Contact");
				if (!duplicatesSearchUtilities.GetActiveRules(entitySchema.Name).Any()) {
					_log.Info("[FindContactDuplicates.ValidateFeatureAndRules]: no active rules");
				    return false;
				}
				return true;
			}
			
			private List<KeyValuePair<string, object>> MapColumns(
			    CompositeObject record, List<string> searchColumns, IEnumerable<KeyValuePair<string, string>> columns) {
				var mappedColumns = searchColumns
				    .Where(column => record.ContainsKey(column) && columns.Any(c => c.Value == column))
				    .Select(column => new KeyValuePair<string, object>(column, record[column]))
				    .ToList();
				if (!mappedColumns.Exists(kv => kv.Key == "Id")) {
					mappedColumns.Add(new KeyValuePair<string, object>("Id", Guid.Empty));
				}
				return mappedColumns;
			}
			
			private void HandleDuplicates(
			    IEnumerable<Dictionary<string, string>> duplicatesCollection, EntitySchema entitySchema, Guid accountId) {
			    var entity = entitySchema.CreateEntity(UserConnection);
			    foreach (var item in duplicatesCollection) {
			        var primaryColumnValue = item["Id"];
			        if (!string.IsNullOrEmpty(primaryColumnValue)) {
			            entity.FetchFromDB(primaryColumnValue);
			            entity.SetDefColumnValues();
			            entity.SetColumnValue("AccountId", accountId);
			            entity.Save();
			        }
			    }
			}

		#endregion

	}

	#endregion

}

