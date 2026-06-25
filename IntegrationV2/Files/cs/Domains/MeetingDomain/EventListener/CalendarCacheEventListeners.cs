namespace IntegrationV2.Files.cs.Domains.MailboxDomain.EventListener
{
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Core.Store;
	using Terrasoft.EmailDomain;

	/// <summary>
	/// Class starts object updating for three entities using <see cref="BaseEntityEventListener"/> implementation.
	/// </summary>
	[EntityEventListener(SchemaName = "MailboxSyncSettings")]
	[EntityEventListener(SchemaName = "ActivitySyncSettings")]
	[EntityEventListener(SchemaName = "MailServer")]
	[EntityEventListener(SchemaName = "SocialAccount")]
	class CalendarCacheEventListeners : BaseEntityEventListener
	{

		#region Methods: Private

		private void ProcessEntity(object sender, EntityAfterEventArgs e) {
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			ICacheStore applicationCache = userConnection.ApplicationCache;
			applicationCache.Remove(IntegrationConsts.Calendar.CacheItemName);
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
