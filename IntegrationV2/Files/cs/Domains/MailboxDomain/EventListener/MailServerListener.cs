namespace IntegrationV2.Files.cs.Domains.MailboxDomain.EventListener
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
    using Terrasoft.Core.Configuration;
    using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;

	#region Class: MailServerListener

	/// <summary>
	/// <see cref="BaseEntityEventListener"/> implementation. Mail server change logic.
	/// </summary>
	[EntityEventListener(SchemaName = "MailServer")]
    public class MailServerListener : BaseEntityEventListener
    {

		#region Methods: Protected

		/// <summary>
		/// Returns error message identifier for <paramref name="code"/>.
		/// </summary>
		/// <param name="code">Error message code.</param>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <returns><see cref="SyncErrorMessage"/> identifier.</returns>
		protected Guid GetErrorCodeId(string code, UserConnection uc) {
			var select = new Select(uc).Top(1)
				.Column("Id")
				.From("SyncErrorMessage")
				.Where("Code").IsEqual(Column.Parameter(code)) as Select;
			return select.ExecuteScalar<Guid>();
		}

		/// <summary>
		/// Returns current server <see cref="MailboxSyncSettings"/> identifiers.
		/// </summary>
		/// <param name="mailServerId"><see cref="MailServer"/> identifier.</param>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <returns>Mail server <see cref="MailboxSyncSettings"/> identifiers.</returns>
		protected List<Guid> GetMailServerMailboxIds(Guid mailServerId, UserConnection uc) {
			var select = (Select)new Select(uc)
				.Column("Id")
			.From("MailboxSyncSettings")
			.Where("MailServerId").IsEqual(Column.Parameter(mailServerId));
			var mailboxIds = new List<Guid>();
			using (DBExecutor dbExecutor = uc.EnsureDBConnection()) {
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						mailboxIds.Add(dataReader.GetColumnValue<Guid>("Id"));
					}
				}
			}
			return mailboxIds;
		}

		/// <summary>
		/// Clears current mail server mailboxes passwords.
		/// </summary>
		/// <param name="mailboxIds"><see cref="MailboxSyncSettings"/> identifiers.</param>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		protected void ClearMailboxSyncSettingsPasswords(IEnumerable<Guid> mailboxIds, UserConnection uc) {
			var entitySchema = uc.EntitySchemaManager.GetInstanceByName("MailboxSyncSettings");
			var errorCodeId = GetErrorCodeId("OAuthUnauthorized", uc);
			foreach (var mailboxId in mailboxIds) {
				Entity mailbox = entitySchema.CreateEntity(uc);
				if (mailbox.FetchFromDB(mailboxId)) {
					mailbox.SetColumnValue("UserPassword", string.Empty);
					mailbox.SetColumnValue("SynchronizationStopped", true);
					mailbox.SetColumnValue("ErrorCodeId", errorCodeId);
					mailbox.Save();
				}
			}
		}

		/// <summary>
		/// Clears current mail server mailboxes OAuth token storages.
		/// </summary>
		/// <param name="mailboxIds"><see cref="MailboxSyncSettings"/> identifiers.</param>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		protected void ClearMailboxSyncSettingsOAuthTokenStorages(IEnumerable<Guid> mailboxIds, UserConnection uc) {
			var entitySchema = uc.EntitySchemaManager.GetInstanceByName("MailboxSyncSettings");
			var errorCodeId = GetErrorCodeId("Unauthorized", uc);
			foreach (var mailboxId in mailboxIds) {
				Entity mailbox = entitySchema.CreateEntity(uc);
				if (mailbox.FetchFromDB(mailboxId)) {
					mailbox.SetColumnValue("OAuthTokenStorageId", null);
					mailbox.SetColumnValue("SynchronizationStopped", true);
					mailbox.SetColumnValue("ErrorCodeId", errorCodeId);
					mailbox.Save();
				}
			}
		}

		/// <summary>
		/// Deletes unused OAuth application.
		/// </summary>
		/// <param name="mailServer"><see cref="Entity"/> instance.</param>
		protected void UpdateMailboxes(Entity mailServer) {
			var changedColumnValues = mailServer.GetChangedColumnValues();
			var value = changedColumnValues.FirstOrDefault(x => x.Name == "OAuthApplicationId"); 
			if (value == null || value.Value == value.OldValue) {
				return;
			}
			var uc = mailServer.UserConnection;
			var mailboxIds = GetMailServerMailboxIds(mailServer.PrimaryColumnValue, uc);
			if (value.OldValue == null && value.Value != null) {
				ClearMailboxSyncSettingsPasswords(mailboxIds, uc);
			} else {
				ClearMailboxSyncSettingsOAuthTokenStorages(mailboxIds, uc);
			}
		}

		/// <summary>
		/// Deletes unused OAuth application.
		/// </summary>
		/// <param name="mailServer"><see cref="Entity"/> instance.</param>
		protected void DeleteUnusedOAuthApplication(Entity mailServer) {
			var oldValue = mailServer.GetColumnOldValue("OAuthApplicationId");
			var value = mailServer.GetColumnValue("OAuthApplicationId");
			if (oldValue == null || (mailServer.StoringState != StoringObjectState.Deleted && oldValue == value)) {
				return;
			}
			var uc = mailServer.UserConnection;
			new Delete(uc).From("OAuthApplications")
				.Where("Id").IsEqual(Column.Parameter(oldValue)).Execute();
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Handles entity Updated event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e"><see cref="EntityAfterEventArgs"/> instance containing  event data.
		/// </param>
		public override void OnUpdated(object sender, EntityAfterEventArgs e) {
			base.OnUpdated(sender, e);
			var mailServer = (Entity)sender;
			UpdateMailboxes(mailServer);
			DeleteUnusedOAuthApplication(mailServer);
		}

		/// <summary>
		/// Handles entity Deleted event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e"><see cref="EntityAfterEventArgs"/> instance containing event data.
		/// </param>
		public override void OnDeleted(object sender, EntityAfterEventArgs e) {
			base.OnDeleted(sender, e);
			DeleteUnusedOAuthApplication((Entity)sender);
		}

		#endregion

	}

	#endregion

}
