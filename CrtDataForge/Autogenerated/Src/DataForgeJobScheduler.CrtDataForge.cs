namespace Terrasoft.Configuration.DataForge
{
	using global::Common.Logging;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;


	#region Interface: IDataForgeJobScheduler
	/// <summary>
	/// Scheduler interface for creating and scheduling DataForge jobs.
	/// </summary>
	public interface IDataForgeJobScheduler
	{
		/// <summary>
		/// Schedules a DataForge job of the specified type.
		/// </summary>
		/// <typeparam name="T">The job type that inherits from DataForgeBaseJob.</typeparam>
		/// <param name="parameters">Job parameters (optional).</param>
		/// <param name="shouldRemovePreviousJobs">Whether to remove existing jobs of the same type.</param>
		void Schedule<T>(
			IDictionary<string, object> parameters = null,
			bool shouldRemovePreviousJobs = true) where T : DataForgeBaseJob;

		/// <summary>
		/// Schedules a job for schema item.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="schemaItem">Schema item to extract table definition from.</param>
		void ScheduleForTableDefinition<T>(
			ISchemaManagerItem<EntitySchema> schemaItem) where T : DataForgeBaseJob;

		/// <summary>
		/// Schedules a job for name.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="schemaItem">Schema item to extract table definition from.</param>
		void ScheduleForName<T>(
			ISchemaManagerItem<EntitySchema> schemaItem) where T : DataForgeBaseJob;

		/// <summary>
		/// Schedules a job for lookup definition.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">Entity to extract data from.</param>
		void ScheduleForLookupDefinition<T>(
			Entity entity) where T : DataForgeBaseJob;

		/// <summary>
		/// Schedules a job for lookup id.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">Entity to extract data from.</param>
		void ScheduleForLookupId<T>(
			Entity entity) where T : DataForgeBaseJob;

		/// <summary>
		/// Schedules a job for lookup value schema UId.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">Entity to extract data from.</param>
		void ScheduleForLookupValueSchemaUId<T>(
			Entity entity) where T : DataForgeBaseJob;
	}

	#endregion

	#region Class: DataForgeJobFactory

	/// <summary>
	/// Factory for creating and scheduling DataForge jobs.
	/// </summary>
	[DefaultBinding(typeof(IDataForgeJobScheduler))]
	public class DataForgeJobScheduler : IDataForgeJobScheduler
	{
		#region Fields: Private

		private static readonly ILog _log = LogManager.GetLogger("DataForge");
		private readonly UserConnection _userConnection;
		private readonly IAppSchedulerWraper _scheduler;
		private readonly IDataStructureHandler _dataStructureHandler;
		private readonly ILookupHandler _lookupHandler;
		private readonly IDataForgeService _dataForgeService;

		#endregion

		#region Constructors: Public

		public DataForgeJobScheduler(
			UserConnection userConnection,
			IAppSchedulerWraper scheduler,
			IDataStructureHandler dataStructureHandler,
			ILookupHandler lookupHandler,
			IDataForgeServiceFactory dataForgeServiceFactory) {
			_userConnection = userConnection;
			_scheduler = scheduler;
			_dataStructureHandler = dataStructureHandler;
			_lookupHandler = lookupHandler;
			_dataForgeService = dataForgeServiceFactory.Create();
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public void Schedule<T>(
				IDictionary<string, object> parameters = null,
				bool shouldRemovePreviousJobs = true) where T : DataForgeBaseJob {
			string jobGroupName = typeof(T).Name;

			_log.Info($"Scheduling {jobGroupName}");

			if (!_dataForgeService.IsDataForgeEnabled()) {
				_log.Info($"DataForge is disabled. Scheduling {jobGroupName} is aborted.");
				return;
			}

			if (_userConnection.Workspace.IsCommonNodeInWebFarm()) {
				_log.Info($"Current node is common for web farm. Scheduling {jobGroupName} is aborted.");
				return;
			}

			try {

				if (shouldRemovePreviousJobs) {
					SchedulerUtils.DeleteOldJobs(jobGroupName);
				}

				_scheduler.ScheduleImmediateJob<T>(
					jobGroupName,
					_userConnection.Workspace.Name,
					_userConnection.CurrentUser.Name,
					parameters,
					true);

				_log.Info($"Successfully scheduled {jobGroupName}");
			} catch (Exception ex) {
				_log.Error($"Error scheduling {jobGroupName}: {ex.Message}", ex);
				throw;
			}
		}

		/// <inheritdoc/>
		public void ScheduleForTableDefinition<T>(
				ISchemaManagerItem<EntitySchema> schemaItem) where T : DataForgeBaseJob {
			TableDefinition tableDefinition = _dataStructureHandler.GetTableDefinition(schemaItem, false, true);
			var parameters = new Dictionary<string, object> { ["item"] = tableDefinition };
			Schedule<T>(parameters, false);
		}

		/// <inheritdoc/>
		public void ScheduleForName<T>(
				ISchemaManagerItem<EntitySchema> schemaItem) where T : DataForgeBaseJob {
			var parameters = new Dictionary<string, object> { ["item"] = schemaItem.Name };
			Schedule<T>(parameters, false);
		}

		/// <inheritdoc/>
		public void ScheduleForLookupDefinition<T>(
				Entity entity) where T : DataForgeBaseJob {
			var id = entity.GetTypedColumnValue<Guid>("Id");
			List<LookupDefinition> lookupDefinitions = _lookupHandler.GetLookupDefinitions(new List<Guid>() { id });

			if (lookupDefinitions.Count == 0) {
				_log.Warn($"No lookup definition found for Id: {id}");
				return;
			}

			var parameters = new Dictionary<string, object> { ["item"] = lookupDefinitions.FirstOrDefault() };
			Schedule<T>(parameters, false);
		}

		/// <inheritdoc/>
		public void ScheduleForLookupId<T>(
				Entity entity) where T : DataForgeBaseJob {
			var id = entity.GetTypedColumnValue<Guid>("Id");
			var parameters = new Dictionary<string, object> { ["item"] = id };
			Schedule<T>(parameters, false);
		}

		/// <inheritdoc/>
		public void ScheduleForLookupValueSchemaUId<T>(
				Entity entity) where T : DataForgeBaseJob {
			var schemaUId = entity.Schema.UId;

			if (_lookupHandler.IsFromExcludedSchema(entity)) {
				_log.Info($"Schema with UId: {schemaUId} is excluded from processing. Scheduling aborted.");
				return;
			}

			var parameters = new Dictionary<string, object> { ["item"] = schemaUId };
			Schedule<T>(parameters, false);
		}

		#endregion
	}

	#endregion
}
