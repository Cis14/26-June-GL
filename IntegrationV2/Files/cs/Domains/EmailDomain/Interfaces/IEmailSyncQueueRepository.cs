namespace Terrasoft.EmailDomain.Interfaces
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Core;

	#region Interface: IEmailSyncQueueRepository

	/// <summary>
	/// Provides api for email sync task queue persistence storage. 
	/// </summary>
	internal interface IEmailSyncQueueRepository
	{

		#region Methods: Internal
		
		/// <summary>
		/// Adds email sync tacks to queue.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="mailboxId">Mailbox identifier.</param>
		/// <param name="messageIds">Email messageId header values.</param>
		void AddEmailSyncQueueItems(UserConnection uc, Guid mailboxId, IEnumerable<string> messageIds);

		/// <summary>
		/// Removes email sync tack from queue.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="mailboxId">Mailbox identifier.</param>
		/// <param name="messageId">Email messageId header.</param>
		void RemoveEmailSyncQueueItem(UserConnection uc, Guid mailboxId, string messageId);

		/// <summary>
		/// Returns existing email sync tasks grouped by mailbox Id.
		/// </summary>
		/// <param name="uc">UserConnection uc</param>
		/// <returns>Existing email sync tasks grouped by mailbox Id.</returns>
		Dictionary<Guid, IEnumerable<string>> GetAllEmailSyncQueueItems(UserConnection uc);

		#endregion

	}

	#endregion

}