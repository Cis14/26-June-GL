namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using IntegrationApi.Calendar;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Client;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Client.Interfaces;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
    using IntegrationV2.Files.cs.Utils;
    using Terrasoft.Common;
    using Terrasoft.Configuration;
    using Terrasoft.Core;
    using Terrasoft.Core.Configuration;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Factories;
    using Terrasoft.IntegrationV2.Utils;

    #region Class: Calendar

    /// <summary>
    /// Calendar domain model.
    /// </summary>
    [Serializable]
    public class Calendar
    {

        #region Fields: Private

        /// <summary>
        /// <see cref="ICalendarLogger"/> instance.
        /// </summary
        [NonSerialized]
        private readonly ICalendarLogger _log;

        private readonly List<string> _emails;

        private Guid _syncPeriodId;

        #endregion

        #region Properties: Public

        public Guid Id { get; }

        public CalendarType Type { get; } = CalendarType.Exchange;

        public CalendarSettings Settings { get; private set; }

        public Guid OwnerId { get; private set; }

        public DateTime SyncSinceDate { get; private set; }

        public DateTime SyncTillDate { get; private set; }

        public DateTime LastSyncDateUtc { get; set; }

        public bool OldSyncEnabled { get; set; }

        public bool ImportTranscripts { get; set; }

        #endregion

        #region Constructor: Public

        /// <summary>
        /// .ctor.
        /// </summary>
        /// <param name="entity">Calendar <see cref="Entity"/>.</param>
        /// <param name="type">Calendar type.</param>
        public Calendar(Entity entity, CalendarType type = CalendarType.Exchange) {
            Id = entity.PrimaryColumnValue;
            Type = type;
            SetSyncDates(entity);
        }

        /// <summary>
        /// .ctor.
        /// </summary>
        /// <param name="entity">Calendar <see cref="Entity"/>.</param>
        /// <param name="sessionId">Synchronization session identifier.</param>
        /// <param name="emails">Current culendar emails with doman.</param>
        /// <param name="type">Calendar type.</param>
        public Calendar(Entity entity, string sessionId, List<string> emails, CalendarType type = CalendarType.Exchange)
            : this(entity, type) {
            _log = ClassFactory.Get<ICalendarLogger>(new ConstructorArgument("sessionId", sessionId),
                new ConstructorArgument("modelName", GetType().Name));
            _emails = emails;
        }

        #endregion

        #region Methods: Private

        private void SetSyncDates(Entity entity) {
            TimeZoneInfo timeZone = entity.UserConnection.CurrentUser.TimeZone;
            if (Type == CalendarType.Google) {
                var date = (DateTime)SysSettings.GetValue(entity.UserConnection,
                    entity.GetTypedColumnValue<Guid>("UserId"), "GoogleCalendarLastSynchronization");
                LastSyncDateUtc = date;
                SyncSinceDate = DateTime.UtcNow.AddDays(-7);
                SyncTillDate = DateTime.UtcNow.AddDays(7);
                return;
            }
            LastSyncDateUtc =
                DateTimeUtils.ConvertTimeToUtc(entity.GetTypedColumnValue<DateTime>("AppointmentLastSyncDate"),
                    timeZone);
            _syncPeriodId = entity.GetTypedColumnValue<Guid>("ActivitySyncPeriodId");
            ImportTranscripts = entity.GetTypedColumnValue<bool>("ImportTranscripts");
            UpdateSyncPeriodDates(entity.UserConnection);
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Add <see cref="Meeting"/> to calendar.
        /// </summary>
        /// <param name="meeting"><see cref="Meeting"/> model.</param>
        /// <param name="meetingRepository"><see cref="IMeetingRepository"/> instance.</param>
        /// <param name="uc"><see cref="UserConnection"/> instance.</param>
        public void SaveMeeting(Meeting meeting, IMeetingRepository meetingRepository, UserConnection uc = null) {
            if (Type == CalendarType.Google)
                return;
            _log?.LogInfo($"Create or update meeting '{meeting}' to '{this}'.");
            if (Settings.SyncEnabled) {
                Settings.RefreshAccessToken(uc);
                ICalendarClient calendarClient = CalendarClientFactory.GetCalendarClient(this, _log?.SessionId, uc);
                IntegrationId integrationsId = calendarClient.SaveMeeting(meeting, this);
                if (integrationsId != null) {
                    if (meeting.RemoteId.IsNullOrEmpty())
                        meeting.RemoteCreatedOn = DateTime.Now;
                    meeting.SetIntegrationsId(integrationsId);
                    meetingRepository.SaveMetadata(meeting);
                    _log?.LogInfo($"Update meeting '{meeting.Id}' RemoteId in the internal repository.");
                }
            }
            _log?.LogInfo($"Meeting '{meeting.Id}' created or updated in '{this}'.");
        }

        /// <summary>
        /// Removes <paramref name="meeting"/> from current calendar.
        /// </summary>
        /// <param name="meeting"><see cref="Meeting"/> instance.</param>
        /// <param name="uc"><see cref="UserConnection"/> instance.</param>
        public void RemoveMeeting(Meeting meeting, UserConnection uc = null) {
            if (Type == CalendarType.Google)
                return;
            if (Settings.SyncEnabled && !string.IsNullOrEmpty(meeting.RemoteId)) {
                Settings.RefreshAccessToken(uc);
                ICalendarClient calendarClient = CalendarClientFactory.GetCalendarClient(this, _log?.SessionId, uc);
                calendarClient.RemoveMeeting(meeting, this);
            }
        }

        /// <summary>
        /// Sends <see cref="Meeting"/> invites inexternal calendar.
        /// </summary>
        /// <param name="meeting"><see cref="Meeting"/> model.</param>
        /// <param name="meetingRepository"><see cref="IMeetingRepository"/> instance.</param>
        /// <param name="uc"><see cref="UserConnection"/> instance.</param>
        public void SendInvitations(Meeting meeting, IMeetingRepository meetingRepository, UserConnection uc = null) {
            if (Settings.SyncEnabled) {
                Settings.RefreshAccessToken(uc);
				if (Type == CalendarType.Exchange || Type == CalendarType.Graph)
					try {
						Settings.LoadOauthAppParams(uc);
					} catch (Exception e) {
                        _log?.LogWarn($"OAuth app params load failed for {this}. {e}.");
                    }
                ICalendarClient calendarClient = CalendarClientFactory.GetCalendarClient(this, _log?.SessionId, uc);
                if (Type == CalendarType.Google)
                    ((GoogleCalendarClient)calendarClient).SetUserConnection(uc);
                meeting.IsNeedSendInvitations = ListenerUtils.GetIsFeatureEnabled("MeetingInvitation");
                calendarClient.SendInvitations(meeting, this,
                    ListenerUtils.GetIsFeatureEnabled("SyncMeetingAsTeamsMeeting"));
                meetingRepository.SaveMetadata(meeting);
            }
        }

        /// <summary>
        /// Set settings property.
        /// </summary>
        /// <param name="calendarSettings"><see cref="CalendarSettings"/> instance.</param>
        public void SetCalendarSettings(CalendarSettings calendarSettings) {
            Settings = calendarSettings;
        }

        /// <summary>
        /// Set owner identifier.
        /// </summary>
        /// <param name="ownerId">Owner identifier.</param>
        public void SetOwner(Guid ownerId) {
            OwnerId = ownerId;
        }

        /// <summary>
        /// Update sync period dates.
        /// </summary>
        /// <param name="userConnection">User connection.</param>
        public void UpdateSyncPeriodDates(UserConnection userConnection) {
            if (_syncPeriodId != Guid.Empty) {
                var dateType = LoadFromDateType.GetInstance(userConnection);
                SyncSinceDate = dateType.GetLoadFromDate(_syncPeriodId);
                SyncTillDate = dateType.GetLoadFromDate(_syncPeriodId, true);
            }
        }

        /// <summary>
        /// Checks if current calendar is owner's calendar (by email address).
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <returns><c>True</c> if current calendar is owner's calendar (by email address), <c>false</c> otherwise.</returns>
        public bool IsOwnerCalendar(string email) {
            string emailLower = email.ToLower();
            if (_emails != null)
                return _emails.Any(e => e.Equals(emailLower, StringComparison.CurrentCultureIgnoreCase));
            return false;
        }

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString() {
            return $"[\"Calendar ({Type})\" => \"{Id}\" \"{OwnerId}\"]";
        }

        #endregion

    }

    #endregion

}
