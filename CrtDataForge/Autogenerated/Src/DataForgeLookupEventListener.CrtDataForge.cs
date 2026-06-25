using Common.Logging;
using Creatio.FeatureToggling;
using System;
using Terrasoft.Core.Entities;
using Terrasoft.Core.Entities.Events;
using Terrasoft.Core.Factories;
using static Terrasoft.Configuration.DataForge.DataForgeFeatures;

namespace Terrasoft.Configuration.DataForge
{

	/// <summary>
	/// Listens to entity lifecycle events (insert, update, delete) 
	/// and synchronizes lookup and lookup value data with the DataForge service.
	/// </summary>
	[EntityEventListener(IsGlobal = true)]
	public class DataForgeLookupEventListener : BaseEntityEventListener
	{
		#region Fields: Private

		private static readonly ILog _log = LogManager.GetLogger("DataForge");

		#endregion

		#region Methods: Private

		private static void ProcessEntity(object sender, EntityAfterEventArgs e) {
			if (!Features.GetIsEnabled<RealtimeLookupSync>()) {
				return;
			}

			var entity = (Entity)sender;
			var userConnection = entity.UserConnection;

			var checksumProvider = ClassFactory.Get<IChecksumProvider>(
				new ConstructorArgument("userConnection", userConnection)
			);
			var lookupHandler = ClassFactory.Get<ILookupHandler>(
				new ConstructorArgument("userConnection", userConnection),
				new ConstructorArgument("checksumProvider", checksumProvider)
			);

			var dataForgeJobScheduler = ClassFactory.Get<IDataForgeJobFactory>(
				new ConstructorArgument("userConnection", userConnection)
			).Create();

			try {
				if (lookupHandler.IsLookup(entity)) {
					dataForgeJobScheduler.ScheduleForLookupDefinition<DataForgeUploadLookupJob>(entity);
				} else if (lookupHandler.IsLookupValue(entity)) {
					dataForgeJobScheduler.ScheduleForLookupValueSchemaUId<DataForgeUpdateLookupsForValueJob>(entity);
				}
			} catch (Exception ex) {
				_log.Error($"Error processing entity {entity.Schema.Name}: {ex.Message}", ex);
			}
		}

		private static void ProcessDeletedEntity(object sender, EntityAfterEventArgs e) {
			if (!Features.GetIsEnabled<RealtimeLookupSync>()) {
				return;
			}

			var entity = (Entity)sender;
			var userConnection = entity.UserConnection;

			var checksumProvider = ClassFactory.Get<IChecksumProvider>(
				new ConstructorArgument("userConnection", userConnection)
			);
			var lookupHandler = ClassFactory.Get<ILookupHandler>(
				new ConstructorArgument("userConnection", userConnection),
				new ConstructorArgument("checksumProvider", checksumProvider)
			);

			var dataForgeJobScheduler = ClassFactory.Get<IDataForgeJobFactory>(
				new ConstructorArgument("userConnection", userConnection)
			).Create();

			try {
				if (lookupHandler.IsLookup(entity)) {
					dataForgeJobScheduler.ScheduleForLookupId<DataForgeDeleteLookupJob>(entity);
				} else if (lookupHandler.IsLookupValue(entity)) {
					dataForgeJobScheduler.ScheduleForLookupValueSchemaUId<DataForgeUpdateLookupsForValueJob>(entity);
				}
			} catch (Exception ex) {
				_log.Error($"Error processing deleted entity {entity.Schema.Name}: {ex.Message}", ex);
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Handles the logic to execute after an entity is inserted.
		/// Creates a lookup or lookup value in the DataForge service based on the entity type.
		/// </summary>
		public override void OnInserted(object sender, EntityAfterEventArgs e) {
			base.OnInserted(sender, e);
			ProcessEntity(sender, e);
		}

		/// <summary>
		/// Handles the logic to execute after an entity is updated.
		/// Updates the corresponding lookup or lookup value in the DataForge service.
		/// </summary>
		public override void OnUpdated(object sender, EntityAfterEventArgs e) {
			base.OnUpdated(sender, e);
			ProcessEntity(sender, e);
		}

		/// <summary>
		/// Handles the logic to execute after an entity is deleted.
		/// Deletes the corresponding lookup or lookup value from the DataForge service.
		/// </summary>
		public override void OnDeleted(object sender, EntityAfterEventArgs e) {
			base.OnDeleted(sender, e);
			ProcessDeletedEntity(sender, e);
		}

		#endregion
	}
}
