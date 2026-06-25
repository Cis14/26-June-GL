namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces
{
	using System;
	using System.Collections.Generic;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;

	#region Interface: IParticipantRepository

	/// <summary>
	/// Participant repository interface.
	/// </summary>
	public interface IParticipantRepository
	{

		#region Methods: public

		/// <summary>
		/// Gets <see cref="Participant"/> collection by <paramref name="meetingId"/>.
		/// </summary>
		/// <param name="meetingId"><see cref="Meeting"/> instance identifier.</param>
		/// <returns><see cref="Participant"/> collection.</returns>
		List<Participant> GetMeetingParticipants(Guid meetingId);

		/// <summary>
		/// Creates <see cref="Participant"/> instance for participant record.
		/// </summary>
		/// <param name="meetingId">Meeting identifier.</param>
		/// <param name="contactId">Contact identifier.</param>
		/// <returns><see cref="Participant"/> instance.</returns>
		Participant GetParticipant(Guid meetingId, Guid contactId);

		/// <summary>
		/// Returns list of <see cref="Participant"/> by email.
		/// </summary>
		/// <param name="meetingId">Meeting identifier.</param>
		/// <param name="emails">Participant emails.</param>
		/// <returns>List of <see cref="Participant"/>.</returns>
		List<Participant> GetParticipants(Guid meetingId, List<string> emails);

		/// <summary>
		/// Returns list of contact identifiers and emails that are related to <paramref name="emails"/>.
		/// </summary>
		/// <param name="emails">Emails list.</param>
		/// <returns>List of contact identifiers and emails.</returns>
		Dictionary<Guid, string> GetParticipantContacts(List<string> emails);

		/// <summary>
		/// Updates meeting participants list in DB.
		/// Deletes existing participants that are not in <paramref name="actualParticipants"/>,
		/// craetes not existing participants from <paramref name="actualParticipants"/>.
		/// </summary>
		/// <param name="actualParticipants">Actual participants list.</param>
		/// <param name="isOrganizer">Flag meaning whether the update of the participants 
		/// is performed by the organizer or not.</param>
		void UpdateMeetingParticipants(List<Participant> actualParticipants, bool isOrganizer);

		/// <summary>
		/// Update invitation status of participant in the meeting.
		/// </summary>
		/// <param name="participant"><see cref="Participant"/> instance.</param>
		void UpdateParticipantInvitation(Participant participant);

		/// <summary>
		/// Sets invite response "Declined" for <paramref name="participants"/>.
		/// </summary>
		/// <param name="participants">List of <see cref="Participant"/> instances.</param>
		void DeclineMeeting(IEnumerable<Participant> participants);

		/// <summary>
		/// Removes <paramref name="participant"/> synchronization metadata.
		/// </summary>
		/// <param name="participant"><see cref="Participant"/> instance.</param>
		void Delete(Participant participant);

		/// <summary>
		/// Save participant
		/// </summary>
		/// <param name="participantId">Participant unique identifier.</param>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		void Save(Meeting meeting);

		/// <summary>
		/// Creates new participant for meeting for current user contact if it is necessary.
		/// </summary>
		/// <param name="meetingId"><see cref="Meeting"/> instance identifier.</param>
		void CreateCurrentUserParticipant(Guid meetingId);

		/// <summary>
		/// Creates new contacts by emails.
		/// </summary>
		/// <param name="emails">Emails list.</param>
		/// <returns>List of contact identifiers and emails.</returns>
		Dictionary<Guid, string> CreateContactsByEmails(List<string> emails);

		#endregion

	}

	#endregion

}
