namespace IntegrationV2.MailboxDomain.Interfaces
{
	using System;
	using System.Collections.Generic;
	using IntegrationApi.MailboxDomain.Model;

	#region Interface: IMailboxRepository

	/// <summary>
	/// Mailbox storage repository interface.
	/// </summary>
	internal interface IMailboxRepository
	{

		#region Methods: Internal

		/// <summary>
		/// Returns all mailboxes list.
		/// </summary>
		/// <param name="isUserMailboxesOnly">Select current user mailboxes only or all mailboxes flag.</param>
		/// <param name="useForSynchronization">Sign is synchronization mode or not.</param>
		/// <returns><see cref="Mailbox"/> collection.</returns>
		IEnumerable<Mailbox> GetAll(bool isUserMailboxesOnly = true, bool useForSynchronization = true);

		/// <summary>
		/// Returns concrete mailbox.
		/// </summary>
		/// <param name="mailboxId">Mailbox Id.</param>
		/// <param name="isUserMailboxesOnly">Select current user mailboxes only flag.<</param>
		/// <returns><see cref="Mailbox"/> instance.</returns>
		Mailbox GetById(Guid mailboxId, bool isUserMailboxesOnly = true);

		/// <summary>
		/// Returns mailbox email address collection.
		/// </summary>
		/// <param name="mailboxId"><see cref="MailboxSyncSettings"/> instance identifier.</param>
		/// <returns>Mailbox email address collection.</returns>
		IEnumerable<string> GetEmails(Guid mailboxId);

		#endregion

	}

	#endregion

}
