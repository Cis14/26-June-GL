namespace Terrasoft.EmailDomain.Interfaces
{
	using EmailContract.DTO;
	using System;
	using System.Collections.Generic;
	using Terrasoft.EmailDomain.Model;

	#region Interface: IAttachmentRepository

	/// <summary>
	/// Attachment model repository interface.
	/// </summary>
	internal interface IAttachmentRepository
	{

		#region Methods: Internal

		/// <summary>
		/// Saves attachments from <paramref name="email"/> to storage.
		/// </summary>
		/// <param name="email"><see cref="EmailModel"/> instance.</param>
		/// <param name="replaceExisting">Flag that indicates that previous set of <paramref name="email"/>
		/// should be replaced with attachments from <paramref name="email"/>.</param>
		void SaveAttachments(EmailModel email, bool replaceExisting = false);

		/// <summary>
		/// Returns file service link for <paramref name="attachmentId"/>.
		/// </summary>
		/// <param name="attachmentId">Attachment identifier.</param>
		/// <returns>Attachment file service link.</returns>
		string GetAttachmentLink(Guid attachmentId);

		/// <summary>
		/// Set inline flag.
		/// </summary>
		/// <param name="attachmentId">Attachment identifier.</param>
		/// <param name="ignoreRights">Use admin rights.</param>
		void SetInline(Guid attachmentId, bool ignoreRights = false);

		/// <summary>
		/// Get email attachments for <paramref name="activityId"/>.
		/// </summary>
		/// <param name="activityId"></param>
		/// <returns>Email attachment collection.</returns>
		List<Attachment> GetAttachments(Guid activityId);

		/// <summary>
		/// Returns <paramref name="activityId"/> attachments count.
		/// </summary>
		/// <param name="activityId">Activity record identifier.</param>
		/// <returns>Count of Activity File records linked to <paramref name="activityId"/>.</returns>
		int GetAttachmentsCount(Guid activityId);

		#endregion

	}

	#endregion

}
