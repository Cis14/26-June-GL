namespace Terrasoft.EmailDomain
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;
	using Terrasoft.EmailDomain.Interfaces;
	using Terrasoft.IntegrationV2.Logging.Interfaces;
	using Terrasoft.IntegrationV2.Utils;

	#region Class: EmailSyncQueueRepository
	/// <summary>
	/// <see cref="IEmailSyncQueueRepository"/> implementation with database used as storage.
	/// </summary>
	[DefaultBinding(typeof(IEmailSyncQueueRepository))]
	internal class EmailSyncQueueRepository : IEmailSyncQueueRepository
	{
		#region Fields: Private

		private readonly Lazy<ISynchronizationLogger> _log = new Lazy<ISynchronizationLogger>(() => ClassFactory.Get<ISynchronizationLogger>());

		#endregion

		#region Methods: Private

		private bool CanUseEmailSyncQueue() {
			var result = ListenerUtils.GetIsFeatureEnabled("UseEmailSyncQueue");
			if (!result) {
				_log.Value?.Info($"Feature UseEmailSyncQueue disabled, emails tasks DB storage skipped.");
			}
			return result;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IEmailSyncQueueRepository.AddEmailSyncQueueItems(UserConnection, Guid, IEnumerable{string})"/>
		public void AddEmailSyncQueueItems(UserConnection uc, Guid mailboxId, IEnumerable<string> messageIds) {
			if (!CanUseEmailSyncQueue()) {
				return;
			}
			var nowParameter = new QueryParameter("now", DateTime.UtcNow, "DateTime");
			messageIds?.ForEach(messageId => {
				try {
					new Insert(uc)
						.Into("EmailSyncQueue")
						.Set("Id", Column.Parameter(Guid.NewGuid()))
						.Set("CreatedOn", nowParameter)
						.Set("MessageId", Column.Parameter(messageId))
						.Set("MailboxId", Column.Parameter(mailboxId))
						.Execute();
				} catch (Exception ex) {
					_log.Value?.Error($"error on adding email {messageId} from mailbox {mailboxId} to EmailSyncQueue", ex);
				}
			});
		}

		/// <inheritdoc cref="IEmailSyncQueueRepository.RemoveEmailSyncQueueItem(UserConnection, Guid, string)"/>
		public void RemoveEmailSyncQueueItem(UserConnection uc, Guid mailboxId, string messageId) {
			if (!CanUseEmailSyncQueue()) {
				return;
			}
			try {
				new Delete(uc).From("EmailSyncQueue")
					.Where("MailboxId").IsEqual(Column.Parameter(mailboxId))
					.And("MessageId").IsEqual(Column.Parameter(messageId))
					.Execute();
			} catch (Exception ex) {
				_log.Value?.Error($"error on removing email {messageId} from mailbox {mailboxId} to EmailSyncQueue", ex);
			}
		}

		/// <inheritdoc cref="IEmailSyncQueueRepository.GetAllEmailSyncQueueItems(UserConnection)"/>
		public Dictionary<Guid, IEnumerable<string>> GetAllEmailSyncQueueItems(UserConnection uc) {
			var result = new Dictionary<Guid, IEnumerable<string>>();
			if (!CanUseEmailSyncQueue()) {
				return result;
			}
			var select = new Select(uc)
				.Top(uc.AppConnection.MaxEntityRowCount)
				.Column("MailboxId")
				.Column("MessageId")
				.From("EmailSyncQueue")
				.Where("MailboxId").Not().IsNull()
				.And("MessageId").IsNotEqual(Column.Const(string.Empty)) as Select;
			using (DBExecutor dbExecutor = uc.EnsureDBConnection()) {
				using (var reader = select.ExecuteReader(dbExecutor)) {
					while (reader.Read()) {
						var mailboxId = reader.GetColumnValue<Guid>("MailboxId");
						var list = mailboxId.IsEmpty() || !result.ContainsKey(mailboxId)
							? new HashSet<string>()
							: result[mailboxId] as HashSet<string>;
						var messageId = reader.GetColumnValue<string>("MessageId");
						list.Add(messageId);
						result[mailboxId] = list;
					}
				}
			}
			return result;
		}

		#endregion

	}

	#endregion

}
