namespace Terrasoft.Configuration.EntityEventLog
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Entities.EntityEventLog;

	#region Class: SysEntityDeleteEventLogConfigListenerListener

	/// <summary>
	/// Controls the flow of creating and modifying SysEntityDeleteEventLogConfig.
	/// </summary>
	/// <seealso cref="Terrasoft.Core.Entities.Events.BaseEntityEventListener" />
	[EntityEventListener(SchemaName = "SysEntityDeleteEventLogConfig")]
	public class SysEntityDeleteEventLogConfigListener : BaseEntityEventListener
	{		
		#region Methods: Private

		private void ClearEntityDeleteEventLogConfigProviderCache(UserConnection userConnection) {
			var entityDeleteEventLogConfigProvider = ClassFactory.Get<IEntityDeleteEventLogConfigProvider>();
			entityDeleteEventLogConfigProvider.Clear();
		}

		private void CheckAdminOperationRights(UserConnection userConnection) {
			var dbSecurityEngine = userConnection.DBSecurityEngine;
			if (!dbSecurityEngine.GetCanExecuteOperation("CanManageSolution")) {
				dbSecurityEngine.CheckCanExecuteOperation("CanManageChangeLog");
			}				
		}

		private void CheckInsertAdminOperationRight(UserConnection userConnection) {
			var dbSecurityEngine = userConnection.DBSecurityEngine;
			if (!dbSecurityEngine.GetCanExecuteOperation("CanInsertEverything")) {
				CheckAdminOperationRights(userConnection);
			}
		}

		private void CheckUpdateAdminOperationRight(UserConnection userConnection) {
			var dbSecurityEngine = userConnection.DBSecurityEngine;
			if (!dbSecurityEngine.GetCanExecuteOperation("CanUpdateEverything")) {
				CheckAdminOperationRights(userConnection);
			}
		}

		private void CheckDeleteAdminOperationRight(UserConnection userConnection) {
			var dbSecurityEngine = userConnection.DBSecurityEngine;
			if (!dbSecurityEngine.GetCanExecuteOperation("CanDeleteEverything")) {
				CheckAdminOperationRights(userConnection);
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Handles SysEntityDeleteEventLogConfig OnInserting gevent.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="T:Terrasoft.Core.Entities.EntityAfterEventArgs" /> instance containing the
		/// event data.</param>
		public override void OnInserting(object sender, EntityBeforeEventArgs e) {			
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			CheckInsertAdminOperationRight(userConnection);
			base.OnInserting(sender, e);			
		}

		/// <summary>
		/// Handles SysEntityDeleteEventLogConfig OnInserted event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="T:Terrasoft.Core.Entities.EntityAfterEventArgs" /> instance containing the
		/// event data.</param>
		public override void OnInserted(object sender, EntityAfterEventArgs e) {
			base.OnInserted(sender, e);
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			ClearEntityDeleteEventLogConfigProviderCache(userConnection);
		}

		/// <summary>
		/// Handles SysEntityDeleteEventLogConfig OnUpdating event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="T:Terrasoft.Core.Entities.EntityBeforeEventArgs" /> instance containing
		/// the event data.</param>
		public override void OnUpdating(object sender, EntityBeforeEventArgs e) {
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			CheckUpdateAdminOperationRight(userConnection);
			base.OnUpdating(sender, e);
		}

		/// <summary>
		/// Handles SysEntityDeleteEventLogConfig OnUpdated event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="T:Terrasoft.Core.Entities.EntityBeforeEventArgs" /> instance containing
		/// the event data.</param>
		public override void OnUpdated(object sender, EntityAfterEventArgs e) {
			base.OnUpdated(sender, e);
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			ClearEntityDeleteEventLogConfigProviderCache(userConnection);
		}

		/// <summary>
		/// Handles SysEntityDeleteEventLogConfig OnDeleting event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">
		/// The <see cref="T:Terrasoft.Core.Entities.EntityBeforeEventArgs"/> instance containing the
		/// event data.
		/// </param>
		public override void OnDeleting(object sender, EntityBeforeEventArgs e) {
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			CheckDeleteAdminOperationRight(userConnection);
			base.OnDeleting(sender, e);
		}
		
		/// <summary>
		/// Handles SysEntityDeleteEventLogConfig OnDeleted event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="T:Terrasoft.Core.Entities.EntityBeforeEventArgs" /> instance containing
		/// the event data.</param>
		public override void OnDeleted(object sender, EntityAfterEventArgs e){
			base.OnDeleted(sender, e);
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			ClearEntityDeleteEventLogConfigProviderCache(userConnection);
		}

		#endregion

	}

	#endregion

}
