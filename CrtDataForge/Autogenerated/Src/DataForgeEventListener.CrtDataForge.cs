namespace Terrasoft.Configuration.DataForge
{
	using Creatio.FeatureToggling;
	using global::Common.Logging;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Web.Common;
	using static Terrasoft.Configuration.DataForge.DataForgeFeatures;

	#region Class: DataForgeEventListener

	/// <summary>
	/// Application event listener for DataForge service.
	/// Handles application lifecycle events and subscribes to schema changes when real-time sync is enabled.
	/// </summary>
	public class DataForgeEventListener : IAppEventListener
	{
		#region Fields: Private

		private static readonly ILog _log = LogManager.GetLogger("DataForge");
		private IDataForgeJobScheduler _dataForgeJobScheduler;
		private AppEventContext _appEventContext;
		private AppConnection _appConnection;
		private UserConnection _userConnection;

		#endregion

		#region Properties: Private

		private AppConnection AppConnection {
			get {
				if (_appConnection == null) {
					_appConnection = _appEventContext.Application["AppConnection"] as AppConnection;
				}
				return _appConnection;
			}
		}

		private UserConnection UserConnection {
			get {
				if (_userConnection == null) {
					_userConnection = AppConnection.SystemUserConnection;
				}
				return _userConnection;
			}
		}

		#endregion

		#region Methods: Private

		private void OnItemSaved(object sender, SchemaManagerItemAfterSaveEventArgs e) {
			_log.Info($"On Item Saved: {e.Item.Name}");

			if (!Features.GetIsEnabled<RealtimeSchemaSync>()) {
				_log.Info("Realtime schema sync is disabled. Skipping upload.");
				return;
			}

			_dataForgeJobScheduler.ScheduleForTableDefinition<DataForgeUploadTableDefinitionJob>(e.Item as ISchemaManagerItem<EntitySchema>);
		}

		private void OnItemRemoved(object sender, SchemaManagerItemAfterRemoveEventArgs e) {
			_log.Info($"On Item Removed: {e.Item.Name}");

			if (!Features.GetIsEnabled<RealtimeSchemaSync>()) {
				_log.Info("Realtime schema sync is disabled. Skipping removal.");
				return;
			}

			_dataForgeJobScheduler.ScheduleForName<DataForgeRemoveTableByNameJob>(e.Item as ISchemaManagerItem<EntitySchema>);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Handles application start event.
		/// Initializes dependencies and subscribes to schema modification events if real-time sync is enabled.
		/// </summary>
		/// <param name="context">Application event context.</param>
		public void OnAppStart(AppEventContext context) {
			_appEventContext = context;
			_dataForgeJobScheduler = ClassFactory.Get<IDataForgeJobFactory>(
				new ConstructorArgument("userConnection", UserConnection)
			).Create();

			UserConnection.EntitySchemaManager.ItemSaved += OnItemSaved;
			UserConnection.EntitySchemaManager.ItemRemoved += OnItemRemoved;

			if (Features.GetIsEnabled<BulkSchemaSync>()) {
				_log.Info("Bulk schema sync is enabled");
				_dataForgeJobScheduler.Schedule<DataForgeUploadPendingDataStructuresJob>();
			} else {
				_log.Info("Bulk schema sync is disabled. Skipping bulk schema sync.");
			}

			if (Features.GetIsEnabled<BulkLookupSync>()) {
				_log.Info("Bulk lookup sync is enabled");
				_dataForgeJobScheduler.Schedule<DataForgeUploadPendingLookupsJob>();

			} else {
				_log.Info("Bulk lookup sync is disabled. Skipping bulk lookup sync.");
			}
		}

		/// <summary>
		/// Handles application end event.
		/// Unsubscribes from schema modification events to clean up resources.
		/// </summary>
		/// <param name="context">Application event context.</param>
		public void OnAppEnd(AppEventContext context) {
			UserConnection.EntitySchemaManager.ItemSaved -= OnItemSaved;
			UserConnection.EntitySchemaManager.ItemRemoved -= OnItemRemoved;
		}

		/// <summary>
		/// Handles session start event.
		/// Currently no operation.
		/// </summary>
		/// <param name="context">Application event context.</param>
		public void OnSessionStart(AppEventContext context) {
			// No operation (reserved for future use).
		}

		/// <summary>
		/// Handles session end event.
		/// Currently no operation.
		/// </summary>
		/// <param name="context">Application event context.</param>
		public void OnSessionEnd(AppEventContext context) {
			// No operation (reserved for future use).
		}

		#endregion
	}

	#endregion
}

