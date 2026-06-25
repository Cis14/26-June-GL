namespace IntegrationV2.Files.cs.Domains.MeetingDomain.EventListener
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Core.Factories;
	using Terrasoft.EmailDomain.Interfaces;

	#region Class: ParticipantEventListener

	[ExcludeFromCodeCoverage]
	[EntityEventListener(SchemaName = "ActivityParticipant")]
	class ParticipantEventListener: BaseCalendarEventListener
	{

		#region Properties: Protected

		protected override string MeetingColumnName { get => "ActivityId"; }

		#endregion

		#region Methods: Private

		/// <summary>
		/// Gets meeting organizer identifier.
		/// </summary>
		/// <param name="entity"><see cref="Entity"/> instance.</param>
		/// <returns>Meeting organizer identifier.</returns>
		private Guid GetMeetingOrganizer(Entity entity) {
			var meetingId = entity.GetTypedColumnValue<Guid>("ActivityId");
			var userConnection = entity.UserConnection;
			var calendarRepository = ClassFactory.Get<ICalendarRepository>("CalendarInMemoryRepository",
				new ConstructorArgument("uc", userConnection),
				new ConstructorArgument("sessionId", Logger.SessionId));
			var meetingRepository = ClassFactory.Get<IMeetingRepository>(
				new ConstructorArgument("uc", userConnection),
				new ConstructorArgument("calendarRepository", calendarRepository),
				new ConstructorArgument("sessionId", Logger.SessionId));
			var meeting = meetingRepository.GetMeetings(meetingId).FirstOrDefault();
			var organizerId = Guid.Empty;
			if (meeting != null) {
				organizerId = meeting.OrganizerId;
			}
			if (organizerId != Guid.Empty) {
				Logger?.LogDebug($"Meeting with participant '{entity.PrimaryColumnValue}' has organizer {organizerId}.");
			}
			return organizerId;
		}

		/// <summary>
		/// Delete <see cref="Participant"/> from meeting.
		/// </summary>
		/// <param name="entity"><see cref="Entity"/> instance.</param>
		private void DeleteParticipantFromMeeting(Entity entity) {
			var meetingId = entity.GetTypedColumnValue<Guid>("ActivityId");
			var participantId = entity.GetTypedColumnValue<Guid>("ParticipantId");
			var organizerId = GetMeetingOrganizer(entity);
			var organizerSyncAction = GetIsBackgroundProcess() ? SyncAction.CreateOrUpdate : SyncAction.UpdateWithInvite;
			StartMeetingSynchronization(meetingId, new List<Guid> { organizerId }, organizerSyncAction);
			StartMeetingSynchronization(meetingId, new List<Guid> { participantId }, SyncAction.Delete);
		}

		/// <summary>
		/// Add <see cref="Participant"/> to meeting.
		/// </summary>
		/// <param name="entity"><see cref="Entity"/> instance.</param>
		private void AddParticipantToMeeting(Entity entity) {
			var meetingId = entity.GetTypedColumnValue<Guid>("ActivityId");
			var participantContactId = entity.GetTypedColumnValue<Guid>("ParticipantId");
			var organizerId = GetMeetingOrganizer(entity);
			StartMeetingSynchronization(meetingId, new List<Guid> { participantContactId, organizerId });
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// <see cref="BaseEntityEventListener.OnInserted(object, EntityAfterEventArgs)"/>
		/// </summary>
		public override void OnInserted(object sender, EntityAfterEventArgs e) {
			var entity = (Entity)sender;
			var participantId = entity.GetTypedColumnValue<Guid>("ParticipantId");
			Logger?.LogInfo($"Add new participant (id '{entity.PrimaryColumnValue}', participantId '{participantId}') to meeting.");
			base.OnInserted(sender, e);
			if (IsNeedDoAction(entity)) {
				AddParticipantToMeeting(entity);
			} else {
				Logger?.LogInfo($"Add new participant '{entity.PrimaryColumnValue}' to meeting no need to action, action skipped.");
			}
		}

		/// <summary>
		/// <see cref="BaseEntityEventListener.OnDeleted(object, EntityAfterEventArgs)"/>
		/// </summary>
		public override void OnDeleted(object sender, EntityAfterEventArgs e) {
			var entity = (Entity)sender;
			var participantId = entity.GetTypedColumnValue<Guid>("ParticipantId");
			Logger?.LogDebug($"Delete participant (id '{entity.PrimaryColumnValue}', participantId '{participantId}') from meeting.");
			base.OnDeleted(sender, e);
			if (IsNeedDoAction(entity)) {
				DeleteParticipantFromMeeting(entity);
			} else {
				Logger?.LogInfo($"Delete participant '{entity.PrimaryColumnValue}' from meeting no need to action, action skipped.");
			}
		}
		
		#endregion

	}

	#endregion

}
