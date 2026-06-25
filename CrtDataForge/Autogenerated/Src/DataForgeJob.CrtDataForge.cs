namespace Terrasoft.Configuration.DataForge
{
	using global::Common.Logging;
	using System;
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: DataForgeBaseJob

	/// <summary>
	/// Base class for DataForge jobs.
	/// </summary>
	public abstract class DataForgeBaseJob : IJobExecutor
	{
		#region Fields: Private

		private static readonly ILog _log = LogManager.GetLogger("DataForge");

		#endregion

		#region Methods: Abstract

		public abstract void RunJob(IDataForgeService service, IDictionary<string, object> parameters);

		#endregion

		#region Methods: Public

		public void Execute(UserConnection userConnection, IDictionary<string, object> parameters) {
			IDataForgeService dataForgeService = ClassFactory.Get<IDataForgeServiceFactory>(
				new ConstructorArgument("userConnection", userConnection))
				.Create();

			_log.Info($"Executing job: {this.GetType().Name}");

			RunJob(dataForgeService, parameters);
		}

		#endregion

	}

	#endregion

	#region Class:  DataForgeUploadPendingDataStructuresJob

	/// <summary>
	/// Job for uploading pending data structures in DataForge.
	/// </summary>
	public class DataForgeUploadPendingDataStructuresJob : DataForgeBaseJob
	{
		#region Methods: Public

		public override void RunJob(IDataForgeService service, IDictionary<string, object> parameters) {
			service.UploadPendingDataStructures();
		}

		#endregion
	}

	#endregion

	#region Class: DataForgeUploadPendingLookupsJob

	/// <summary>
	/// Job for uploading pending lookups in DataForge.
	/// </summary>
	public class DataForgeUploadPendingLookupsJob : DataForgeBaseJob
	{
		#region Methods: Public

		public override void RunJob(IDataForgeService service, IDictionary<string, object> parameters) {
			service.UploadPendingLookups();
		}

		#endregion
	}

	#endregion

	#region Class: DataForgeInitializeDataStructureJob

	/// <summary>
	/// Job for initializing data structure in DataForge.
	/// </summary>
	public class DataForgeInitializeDataStructureJob : DataForgeBaseJob
	{
		#region Methods: Public

		public override void RunJob(IDataForgeService service, IDictionary<string, object> parameters) {
			service.InitializeDataStructure();
		}

		#endregion
	}

	#endregion

	#region Class: DataForgeInitializeLookupsJob

	/// <summary>
	/// Job for initializing lookups in DataForge.
	/// </summary>
	public class DataForgeInitializeLookupsJob : DataForgeBaseJob
	{
		#region Methods: Public

		public override void RunJob(IDataForgeService service, IDictionary<string, object> parameters) {
			service.InitializeLookups();
		}

		#endregion
	}

	#endregion

	#region Class: DataForgeUploadTableDefinitionJob

	/// <summary>
	/// Job for uploading table definition data in DataForge.
	/// </summary>
	public class DataForgeUploadTableDefinitionJob : DataForgeBaseJob
	{
		#region Methods: Public

		public override void RunJob(IDataForgeService service, IDictionary<string, object> parameters) {
			if (parameters.TryGetValue("item", out object schemaItemObj) &&
					schemaItemObj is TableDefinition tableDefinition) {
				service.UploadTableDefinition(tableDefinition);
			}
		}

		#endregion
	}

	#endregion

	#region Class: DataForgeRemoveTableByNameJob

	/// <summary>
	/// Job for removing table by name in DataForge.
	/// </summary>
	public class DataForgeRemoveTableByNameJob : DataForgeBaseJob
	{
		#region Methods: Public

		public override void RunJob(IDataForgeService service, IDictionary<string, object> parameters) {
			if (parameters.TryGetValue("item", out object tableNameObj) &&
					tableNameObj is string tableName) {
				service.RemoveTableByName(tableName);
			}
		}

		#endregion
	}

	#endregion

	#region Class: DataForgeUploadLookupJob

	/// <summary>
	/// Job for uploading lookup data in DataForge.
	/// </summary>
	public class DataForgeUploadLookupJob : DataForgeBaseJob
	{
		#region Methods: Public

		public override void RunJob(IDataForgeService service, IDictionary<string, object> parameters) {
			if (parameters.TryGetValue("item", out object lookupDefinitionObj) &&
					lookupDefinitionObj is LookupDefinition lookupDefinition) {
				service.UploadLookup(lookupDefinition);
			}
		}

		#endregion
	}

	#endregion

	#region Class: DataForgeUpdateLookupsForValueJob

	/// <summary>
	/// Job for updating lookups for a specific value in DataForge.
	/// </summary>
	public class DataForgeUpdateLookupsForValueJob : DataForgeBaseJob
	{
		#region Methods: Public

		public override void RunJob(IDataForgeService service, IDictionary<string, object> parameters) {
			if (parameters.TryGetValue("item", out object lookupValueSchemaUIdObj) &&
					lookupValueSchemaUIdObj is Guid lookupValueSchemaUId) {
				service.UpdateLookupsForValue(lookupValueSchemaUId);
			}
		}

		#endregion
	}

	#endregion

	#region Class: DataForgeDeleteLookupJob

	/// <summary>
	/// Job for deleting lookups in DataForge.
	/// </summary>
	public class DataForgeDeleteLookupJob : DataForgeBaseJob
	{
		#region Methods: Public

		public override void RunJob(IDataForgeService service, IDictionary<string, object> parameters) {
			if (parameters.TryGetValue("item", out object lookupIdObj) &&
					lookupIdObj is Guid lookupId) {
				service.DeleteLookup(lookupId);
			}
		}

		#endregion
	}

	#endregion
}
