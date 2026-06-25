namespace Terrasoft.Configuration
{
	using Creatio.FeatureToggling;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Feature;
	using Terrasoft.Core.RecordPermissionExtension;

	#region Class: PermissionExtensionsListener

	/// <summary>
	/// Controls the flow of creating and modifying PermissionExtensions.
	/// </summary>
	/// <seealso cref="BaseEntityEventListener" />
	[EntityEventListener(SchemaName = "SysRecordPermissionExtension")]
	public class SysRecordPermissionExtensionListener : BaseEntityEventListener
	{
		#region Methods: Private

		private void ClearPermissionExtensionsCache() {
			if (!Features.GetIsEnabled<UseRecordPermissionExtensions>()) {
				return;
			}
			var permissionExtensionsProvider = ClassFactory.Get<IPermissionExtensionsProvider>();
			permissionExtensionsProvider.Clear();
		}

		private void CheckAdminOperationRights(UserConnection userConnection) {
			var dbSecurityEngine = userConnection.DBSecurityEngine;
			if (!dbSecurityEngine.GetCanExecuteOperation("CanManageSolution")) {
				dbSecurityEngine.CheckCanExecuteOperation("CanManageAdministration");
			}
		}	

		#endregion

		#region Methods: Public

		/// <summary>
		/// Handles PermissionExtensions OnInserting event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="T:Terrasoft.Core.Entities.EntityAfterEventArgs" /> instance containing the
		/// event data.</param>
		public override void OnInserting(object sender, EntityBeforeEventArgs e) {
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			CheckAdminOperationRights(userConnection);
			base.OnInserting(sender, e);
		}

		/// <summary>
		/// Handles PermissionExtensions OnInserted event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="T:Terrasoft.Core.Entities.EntityAfterEventArgs" /> instance containing the
		/// event data.</param>
		public override void OnInserted(object sender, EntityAfterEventArgs e) {
			base.OnInserted(sender, e);
			ClearPermissionExtensionsCache();
		}

		/// <summary>
		/// Handles PermissionExtensions OnUpdating event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="T:Terrasoft.Core.Entities.EntityBeforeEventArgs" /> instance containing
		/// the event data.</param>
		public override void OnUpdating(object sender, EntityBeforeEventArgs e) {
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			CheckAdminOperationRights(userConnection);
			base.OnUpdating(sender, e);
		}

		/// <summary>
		/// Handles PermissionExtensions OnUpdated event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="T:Terrasoft.Core.Entities.EntityBeforeEventArgs" /> instance containing
		/// the event data.</param>
		public override void OnUpdated(object sender, EntityAfterEventArgs e) {
			base.OnUpdated(sender, e);
			ClearPermissionExtensionsCache();
		}

		/// <summary>
		/// Handles PermissionExtensions OnDeleting event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">
		/// The <see cref="T:Terrasoft.Core.Entities.EntityBeforeEventArgs"/> instance containing the
		/// event data.
		/// </param>
		public override void OnDeleting(object sender, EntityBeforeEventArgs e) {
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			CheckAdminOperationRights(userConnection);
			base.OnDeleting(sender, e);
		}

		/// <summary>
		/// Handles PermissionExtensions OnDeleted event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="T:Terrasoft.Core.Entities.EntityBeforeEventArgs" /> instance containing
		/// the event data.</param>
		public override void OnDeleted(object sender, EntityAfterEventArgs e) {
			base.OnDeleted(sender, e);
			ClearPermissionExtensionsCache();
		}

		#endregion
	}

	#endregion
}

