namespace IntegrationV2.Files.cs.Domains.MeetingDomain.MeetingService
{
	using Creatio.FeatureToggling;
	using IntegrationApi.Calendar;
	using IntegrationApi.Calendar.DTO;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Provider;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
    using IntegrationV2.Files.cs.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Terrasoft.Common;
    using Terrasoft.Configuration;
    using Terrasoft.Core;
    using Terrasoft.Core.Factories;
    using static Terrasoft.EmailDomain.IntegrationConsts.Calendar;
    using Calendar = IntegrationV2.Files.cs.Domains.MeetingDomain.Model.Calendar;

    /// <summary>
    /// Meeting service implementation.
    /// </summary>
    [DefaultBinding(typeof(IMeetingService))]
    public class MeetingService: IMeetingService {

        #region Fields: Private

        /// <summary>
        /// <see cref="IMeetingRepository"/> instance.
        /// </summary>
        private readonly IMeetingRepository _meetingRepository;

        /// <summary>
        /// <see cref="ICalendarRepository"/> instance.
        /// </summary>
        private readonly ICalendarRepository _calendarRepository;

        /// <summary>
        /// <see cref="ICalendarLogger"/> instance.
        /// </summary>
        private ICalendarLogger Log { get; } = ClassFactory.Get<ICalendarLogger>();

        /// <summary>
        /// <see cref="IParticipantRepository"/> instance.
        /// </summary>
        private readonly IParticipantRepository _participantRepository;

        /// <summary>
        /// <see cref="IMeetingTranscriptRepository"/> instance.
        /// </summary>
        private readonly IMeetingTranscriptRepository _transcriptRepository;

        /// <summary>
        /// <see cref="UserConnection"/> instance.
        /// </summary>
        private readonly UserConnection _uc;

        #endregion

        #region Constructors: Public

        /// <summary>
        /// .ctor.
        /// </summary>
        /// <param name="uc"><see cref="UserConnection"/> instance.</param>
        public MeetingService(UserConnection uc) {
            _uc = uc;
            Log.SetModelName(GetType().Name);
            _calendarRepository = ClassFactory.Get<ICalendarRepository>("CalendarInMemoryRepository",
                new ConstructorArgument("uc", uc), new ConstructorArgument("sessionId", Log.SessionId));
            _meetingRepository = ClassFactory.Get<IMeetingRepository>(new ConstructorArgument("uc", uc),
                new ConstructorArgument("calendarRepository", _calendarRepository),
                new ConstructorArgument("sessionId", Log.SessionId));
            _participantRepository = ClassFactory.Get<IParticipantRepository>(new ConstructorArgument("uc", uc),
                new ConstructorArgument("sessionId", Log.SessionId),
                new ConstructorArgument("calendarRepository", _calendarRepository));
            _transcriptRepository = ClassFactory.Get<IMeetingTranscriptRepository>(new ConstructorArgument("uc", uc));
        }

        #endregion

        #region Methods: Private

        /// <summary>
        /// Checking if the meeting start date is outdated.
        /// </summary>
        /// <param name="meeting"><see cref="Meeting"/> instance.</param>
        /// <returns><c>True</c> if meeting is outdated, <c>false</c> otherwise.</returns>
        private bool IsOutdatedMeeting(Meeting meeting) {
            if (meeting == null)
                return false;
            DateTime utcStartDate = DateTimeUtils.ConvertTimeToUtc(meeting.StartDate, _uc.CurrentUser.TimeZone);
            return utcStartDate < DateTime.UtcNow;
        }

        /// <summary>
        /// Checks if invitations can be sent to the current meeting.
        /// </summary>
        /// <param name="meeting"><see cref="Meeting"/> instance.</param>
        /// <param name="userContactId">User contact identifier.</param>
        /// <returns><c>True</c> if can be send invitations, <c>false</c> otherwise.</returns>
        private bool CanSendInvitations(Meeting meeting, Guid userContactId) {
            bool isExportedMeeting = meeting.RemoteId.IsNotNullOrEmpty();
            if (!isExportedMeeting)
                Log?.LogInfo(
                    $"Failed to send invitations to contact {meeting.Calendar?.OwnerId} for a non-existent meeting {meeting}.");
            bool isOrganizerSent = meeting.IsOrganizerMeeting() && userContactId.Equals(meeting.Calendar?.OwnerId);
            if (!isOrganizerSent)
                Log?.LogInfo($"Invitations can be sent only meeting organizer {meeting.OrganizerId}," +
                    $" but tried to send {meeting.Calendar?.OwnerId}. {meeting}");
            return isExportedMeeting && isOrganizerSent;
        }

        /// <summary>
        /// Send invitations to participants.
        /// </summary>
        /// <param name="meeting"><see cref="Meeting"/> instance.</param>
        /// <param name="userContactId">User contact identifier.</param>
        private void SendMeetingInvitation(Meeting meeting, Guid userContactId) {
            if (CanSendInvitations(meeting, userContactId)) {
                Log?.LogInfo($"Send invitations for meeting {meeting}.");
                meeting.Calendar.SendInvitations(meeting, _meetingRepository, _uc);
                foreach (Participant participant in meeting.Participants) {
                    participant.SetInvited();
                    if (participant.ContactId == meeting.OrganizerId)
                        participant.SetInvitationState(InvitationState.Confirmed);
                    _participantRepository.UpdateParticipantInvitation(participant);
                    Log?.LogInfo($"Invitation was sent {meeting.OrganizerId} for participant {participant}.");
                }
                Log?.LogInfo($"Invitations was sent for meeting {meeting}.");
            }
        }

        #endregion

        #region Methods: Public

        /// <inheritdoc cref="IMeetingService.SendInvitations(Guid, Guid)"/>
        public void SendInvitations(Guid meetingId, Guid userContactId) {
            Log.SetAction(SyncAction.SendInvite);
            var meetings = _meetingRepository.GetMeetings(meetingId);
            meetings.ForEach(meeting => SendMeetingInvitation(meeting, userContactId));
        }

        /// <inheritdoc cref="IMeetingService.GetMeetingInvitationInfo(Guid, Guid)"/>
        public MeetingInvitationInfo GetMeetingInvitationInfo(Guid meetingId, Guid userContactId) {
            var meetingInvitationInfo = new MeetingInvitationInfo();
            var meetings = _meetingRepository.GetMeetings(meetingId);
            meetingInvitationInfo.IsSynchronized = meetings.Any(m => m.RemoteId.IsNotNullOrEmpty());
            meetingInvitationInfo.IsParticipantsInvited = meetings.Any(m => m.InvitesSent);
            meetingInvitationInfo.IsParticipantsExist = meetings.Any(m =>
                m.Participants.Count > 1 && m.Participants.Any(p =>
                    p.EmailAddress.IsNotNullOrEmpty() && p.ContactId != m.OrganizerId));
            meetingInvitationInfo.HasCalendarIntegration = meetings.Any(m =>
                m.Calendar != null && m.Calendar.OwnerId == userContactId && m.Calendar.Settings.SyncEnabled);
            meetingInvitationInfo.IsOutdatedMeeting = IsOutdatedMeeting(meetings.FirstOrDefault());
            Meeting meeting = meetings.FirstOrDefault(m => m.Calendar?.OwnerId == userContactId);
            if (meeting != null)
                meetingInvitationInfo.CalendarSyncSinceDate =
                    meeting.Calendar.SyncSinceDate.ToString(CultureInfo.InvariantCulture);
            return meetingInvitationInfo;
        }

        /// <inheritdoc cref="IMeetingService.CanUserChangeMeeting(Guid, Guid)"/>
        public ChangeMeetingResponse CanUserChangeMeeting(Guid meetingId, Guid userContactId) {
            Meeting organizerMeeting = _meetingRepository.GetMeetings(meetingId)
                .FirstOrDefault(m => m.Participants.Any(p => p.ContactId == m.OrganizerId));
            if (organizerMeeting == null || organizerMeeting.RemoteId.IsNullOrEmpty() || !organizerMeeting.InvitesSent)
                return ChangeMeetingResponse.Yes;
            if (organizerMeeting.OrganizerId == userContactId)
                return IsOutdatedMeeting(organizerMeeting)
                    ? ChangeMeetingResponse.YesWithObsoleteNotification
                    : ChangeMeetingResponse.YesWithNotification;
            return ChangeMeetingResponse.No;
        }

        /// <inheritdoc cref="IMeetingService.CanUserChangeCalendar(string, Guid)"/>
        public bool CanUserChangeCalendar(string senderEmailAddress, Guid userContactId) {
            bool hasActiveCalendar = HasActiveCalendar(userContactId);
            bool isCurrentActiveCalendar = _calendarRepository.GetOwnerCalendar(senderEmailAddress) != null;
            return !hasActiveCalendar || isCurrentActiveCalendar;
        }

        /// <inheritdoc cref="IMeetingService.HasActiveCalendar(Guid)"/>
        public bool HasActiveCalendar(Guid userContactId) {
            return _calendarRepository.GetOwnerCalendar(userContactId) != null;
        }

        /// <inheritdoc cref="IMeetingService.GetUserCalendarsInfo(Guid)"/>
        public List<CalendarAccountInfo> GetUserCalendarsInfo(Guid userContactId) {
            var ownerCalendars = _calendarRepository.GetOwnerCalendars(userContactId);
            return ownerCalendars
                .Where(c => c.Type == CalendarType.Exchange
                    || c.Type == CalendarType.Google
                    || (c.Type == CalendarType.Graph && CanUseGraphCalendarSync(c)))
                .Select(c => new CalendarAccountInfo {
                    Id = c.Id,
                    MailBoxId = c.Settings.Id,
                    Email = c.Settings.SenderEmailAddress,
                    Type = c.Type
                }).ToList();
        }

        /// <inheritdoc cref="IMeetingService.GetCalendarOwnerId(string, bool)"/>
        public Guid GetCalendarOwnerId(string senderEmailAddress, bool active = true) {
            Calendar calendar = _calendarRepository.GetOwnerCalendar(senderEmailAddress);
            if (calendar == null)
                return Guid.Empty;
            if (active && !calendar.Settings.SyncEnabled)
                return Guid.Empty;
            return calendar.OwnerId;
        }

        /// <summary>
        /// Returns whether Graph calendar should be available in user calendar lists.
        /// </summary>
        /// <param name="calendar"><see cref="Calendar"/> instance.</param>
        /// <returns><c>True</c> when the Graph calendar can participate in calendar synchronization.</returns>
        private static bool CanUseGraphCalendarSync(Calendar calendar) {
            return Features.GetIsEnabled("UseEwsSyncForGraphMailbox") &&
                !(calendar.Settings.UseImpersonation && Features.GetIsEnabled("ApplicationAsServiceAccount"));
        }

        #endregion

    }
}
