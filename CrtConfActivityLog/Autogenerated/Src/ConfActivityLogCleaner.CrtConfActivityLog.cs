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

	#region Class: ConfActivityLogCleanerMethodsWrapper

	/// <exclude/>
	public class ConfActivityLogCleanerMethodsWrapper : ProcessModel
	{

		public ConfActivityLogCleanerMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask2Execute", ScriptTask2Execute);
		}

		#region Methods: Private

		private bool ScriptTask2Execute(ProcessExecutingContext context) {
			// Get required process parameters
			var userConnection = Get<UserConnection>("UserConnection");
			var dateForCleaner = Get<DateTime>("DateForCleaner");
			
			// Convert Date parameter to start of the day (00:00:00)
			DateTime cutoff = dateForCleaner.Date;
			
			// Batch size to avoid timeouts and large transactions
			const int batchSize = 1000;
			
			/* ============================================================
			 * Phase A:
			 * Delete ConfActivityLog records in leaf-first order
			 * (children first, then parents)
			 * ============================================================ */
			
			while (true)
			{
				// 1) Select leaf ConfActivityLog IDs older than cutoff
				// Leaf = no child rows where Child.ParentId = This.Id
				var leafSelect = new Select(userConnection)
					.Column("l", "Id")
					.From("ConfActivityLog").As("l")
					.Where("l", "CreatedOn").IsLess(Column.Parameter(cutoff))
					.And()
					.Not()
					.Exists(
						new Select(userConnection)
							.Column(Column.Const(1))   // SELECT 1
							.From("ConfActivityLog").As("c")
							.Where("c", "ParentId").IsEqual(Column.SourceColumn("l", "Id"))
					)
					.OrderByAsc("l", "CreatedOn")
					.OrderByAsc("l", "Id") as Select;
			
				var ids = new List<Guid>();
			
				using (var dbExecutor = userConnection.EnsureDBConnection())
				using (var reader = leafSelect.ExecuteReader(dbExecutor))
				{
					int counter = 0;
			
					while (reader.Read())
					{
						ids.Add(reader.GetColumnValue<Guid>("Id"));
						counter++;
			
						// Manual batch limit (replacement for TOP)
						if (counter >= batchSize)
						{
							break;
						}
					}
				}
			
				if (ids.Count == 0)
				{
					break; // no more deletable leaf records
				}
			
				// 2) Delete dependent records from CompilationHistory first
				new Delete(userConnection)
					.From("CompilationHistory")
					.Where("ConfActivityLog")
					.In(Column.Parameters(ids.Cast<object>().ToArray()))
					.Execute();
			
				// 3) Delete SysFile records linked by RecordId
				new Delete(userConnection)
					.From("SysFile")
					.Where("RecordId")
					.In(Column.Parameters(ids.Cast<object>().ToArray()))
					.Execute();
			
				// 4) Delete leaf records from ConfActivityLog
				int affected = new Delete(userConnection)
					.From("ConfActivityLog")
					.Where("Id")
					.In(Column.Parameters(ids.Cast<object>().ToArray()))
					.Execute();
			
				if (affected <= 0)
				{
					break;
				}
			}
			
			/* ============================================================
			 * Phase B:
			 * Delete orphan CompilationHistory rows older than cutoff
			 * ============================================================ */
			
			while (true)
			{
				var esqHistory = new EntitySchemaQuery(userConnection.EntitySchemaManager, "CompilationHistory")
				{
					RowCount = batchSize
				};
			
				esqHistory.PrimaryQueryColumn.IsVisible = true;
				esqHistory.AddColumn("Id");
			
				var historyCreatedOn = esqHistory.AddColumn("CreatedOn");
				historyCreatedOn.OrderByAsc();
			
				esqHistory.Filters.Add(esqHistory.CreateFilterWithParameters(
					FilterComparisonType.Less, "CreatedOn", cutoff
				));
			
				esqHistory.Filters.Add(esqHistory.CreateIsNullFilter("ConfActivityLog"));
			
				var historyEntities = esqHistory.GetEntityCollection(userConnection);
				if (historyEntities.Count == 0)
				{
					break;
				}
			
				var historyIds = historyEntities
					.Select(e => e.PrimaryColumnValue)
					.Where(id => id != Guid.Empty)
					.ToList();
			
				if (historyIds.Count == 0)
				{
					break;
				}
			
				int affectedHistory = new Delete(userConnection)
					.From("CompilationHistory")
					.Where("Id")
					.In(Column.Parameters(historyIds.Cast<object>().ToArray()))
					.Execute();
			
				if (affectedHistory <= 0)
				{
					break;
				}
			}
			
			return true;
		}

		#endregion

	}

	#endregion

}

