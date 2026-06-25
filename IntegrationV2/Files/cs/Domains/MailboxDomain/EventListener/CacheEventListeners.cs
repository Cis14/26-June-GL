namespace IntegrationV2.Files.cs.Domains.MailboxDomain.EventListener
{
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Core.Store;
	using Terrasoft.IntegrationV2.Utils;

	/// <summary>
	/// Class starts object updating for three entities using <see cref="BaseEntityEventListener"/> implementation.
	/// </summary>
	[EntityEventListener(SchemaName = "MailboxSyncSettings")]
	[EntityEventListener(SchemaName = "MailServer")]
	[EntityEventListener(SchemaName = "MailboxFoldersCorrespondence")]
	[EntityEventListener(SchemaName = "VwSysAdminUnit")]
	class CacheEventListeners : BaseEntityEventListener
	{

		#region Methods: Private

		private void ProcessEntity(object sender, EntityAfterEventArgs e) {
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			if (ListenerUtils.GetIsFeatureDisabled("IsMailboxSyncSettingsCached")) {
				return;
			}
			ICacheStore applicationCache = userConnection.ApplicationCache;
			switch (entity.SchemaName) {
				case "MailboxSyncSettings":
					ProcessMailboxSyncSettings(entity);
					break;
				case "MailServer":
					applicationCache.Remove("MailServerList");
					applicationCache.Remove("MailboxList");
					break;
				case "MailboxFoldersCorrespondence":
					applicationCache.Remove("MailboxFolderList");
					applicationCache.Remove("MailboxList");
					break;
				case "VwSysAdminUnit":
					ProcessSysAdminUnit(entity);
					break;
				default:
					break;
			}
		}

		private void ProcessMailboxSyncSettings(Entity entity) {
			var userConnection = entity.UserConnection;
			var applicationCache = userConnection.ApplicationCache;
			var changedColumnValues = entity.GetChangedColumnValues();
			if (entity.ChangeType == EntityChangeType.Inserted) {
				applicationCache.Remove("MailboxList");
			} else {
				if (!changedColumnValues.Any(x => x.Name == "LastSyncDate"
						|| x.Name == "RetryCounter") ||
					changedColumnValues.Any(x => x.Name == "UserPassword"
					|| x.Name == "SynchronizationStopped"
					|| x.Name == "ErrorCodeId")) {
					applicationCache.Remove("MailboxList");
				}
			}
		}

		private void ProcessSysAdminUnit(Entity entity) {
			var userConnection = entity.UserConnection;
			var changedColumnValues = entity.GetChangedColumnValues();
			if (entity.ChangeType != EntityChangeType.Updated || !changedColumnValues.Any(x => x.Name == "Active")) {
				return;
			}
			var applicationCache = userConnection.ApplicationCache;
			applicationCache.Remove("MailboxList");
		}

		#endregion

		#region Methods: Public

			/// <summary>
			/// Handles entity Inserted event.
			/// </summary>
			/// <param name="sender">Event sender.</param>
			/// <param name="e"><see cref="EntityAfterEventArgs"/> instance containing event data.
			/// </param>
		public override void OnInserted(object sender, EntityAfterEventArgs e) {
			base.OnInserted(sender, e);
			ProcessEntity(sender, e);
		}

		/// <summary>
		/// Handles entity Deleted event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e"><see cref="EntityAfterEventArgs"/> instance containing event data.
		/// </param>
		public override void OnDeleted(object sender, EntityAfterEventArgs e) {
			base.OnDeleted(sender, e);
			ProcessEntity(sender, e);
		}

		/// <summary>
		/// Handles entity Updated event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e"><see cref="EntityAfterEventArgs"/> instance containing  event data.
		/// </param>
		public override void OnUpdated(object sender, EntityAfterEventArgs e) {
			base.OnUpdated(sender, e);
			ProcessEntity(sender, e);
		}

		#endregion

	}
}
