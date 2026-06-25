namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using IntegrationApi.Calendar.DTO;
    using IntegrationApi.Interfaces;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Client;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.DTO;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
    using IntegrationV2.Files.cs.Utils;
    using Terrasoft.Common;
    using Terrasoft.Core;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Factories;
    using Terrasoft.Sync;

    #region Class: Meeting

    /// <summary>
    /// Meeting domain model.
    /// </summary>
    public class Meeting
    {

        #region Constants: Private

        /// <summary>
        /// "Russian Standard Time" time zone identifier. 
        /// </summary>
        private const string RussianStandardTimeTimeZoneId = "Russian Standard Time";

        /// <summary>
        /// Custom "Russian Standard Time" time zone identifier. 
        /// </summary>
        private const string CustomRussianStandardTimeTimeZoneId = "Custom Russian Standard Time";

        /// <summary>
        /// Custom "Russian Standard Time" time zone offset. 
        /// </summary>
        private const int CustomRussianStandardTimeTotalHoursOffset = 3;

        #endregion

        #region Fields: Private

        /// <summary>
        /// <see cref="IActivityUtils"/> instance.
        /// </summary>
        private readonly IActivityUtils _utils = ClassFactory.Get<IActivityUtils>();

        /// <summary>
        /// Hash value.
        /// </summary>
        private string _hash = string.Empty;

        /// <summary>
        /// Number of participants.
        /// </summary>
        private int _numberOfParticipants;

        #endregion

        #region Constructors: Public

        /// <summary>
        /// .ctor.
        /// </summary>
        public Meeting()
            : this(Guid.Empty, string.Empty) { }

        /// <summary>
        /// .ctor.
        /// </summary>
        /// <param name="meeetingId">Meeting identifier.</param>
        /// <param name="uc"><see cref="UserConnection"/> instance.</param>
        public Meeting(Guid meeetingId, string sessionId) {
            Id = meeetingId;
        }

        /// <summary>
        /// .ctor.
        /// </summary>
        /// <param name="entity">Meeting <see cref="Entity"/>.</param>
        public Meeting(Entity entity, string sessionId)
            : this(entity.PrimaryColumnValue, sessionId) {
            UserConnection userConnection = entity.UserConnection;
            Title = entity.GetTypedColumnValue<string>("Title");
            StartDate = entity.GetTypedColumnValue<DateTime>("StartDate");
            DueDate = entity.GetTypedColumnValue<DateTime>("DueDate");
            Location = entity.GetTypedColumnValue<string>("Location");
            Body = entity.GetTypedColumnValue<string>("Notes");
            PriorityId = entity.GetTypedColumnValue<Guid>("PriorityId");
            StartTimeZone = GetExchangeAppointmentTimeZoneInfo(userConnection, StartDate);
            EndTimeZone = GetExchangeAppointmentTimeZoneInfo(userConnection, StartDate);
            RemindToOwner = entity.GetTypedColumnValue<bool>("RemindToOwner");
            RemindToOwnerDate = entity.GetTypedColumnValue<DateTime>("RemindToOwnerDate");
            ModifiedOn = entity.GetTypedColumnValue<DateTime>("ModifiedOn");
            StatusId = entity.GetTypedColumnValue<Guid>("StatusId");
            OrganizerId = entity.GetTypedColumnValue<Guid>("OrganizerId");
            Transcript = new MeetingTranscript();
        }

        #endregion

        #region Properties: Public

        public Guid Id { get; }

        public string Title { get; private set; }

        public DateTime StartDate { get; internal set; }

        public TimeZoneInfo StartTimeZone { get; private set; }

        public DateTime DueDate { get; private set; }

        public TimeZoneInfo EndTimeZone { get; private set; }

        public string Location { get; private set; }

        public string Body { get; private set; }

        public Guid PriorityId { get; private set; }

        public bool RemoteItemInvitesSent { get; set; }

        public string SourceCalendarName { get; set; } = string.Empty;

        private bool _invitesSent = false;

        public bool InvitesSent {
            get => _invitesSent;
            set {
                if (_invitesSent)
                    return;
                _invitesSent = value;
            }
        }

        public List<Participant> Participants { get; } = new List<Participant>();

        public bool RemindToOwner { get; private set; }

        public DateTime RemindToOwnerDate { get; private set; }

        public string RemoteId { get; private set; }

        public string ICalUid { get; private set; }

        public Guid OrganizerId { get; set; }

        public Guid StatusId { get; }

        public DateTime ModifiedOn { get; }

        public DateTime RemoteCreatedOn { get; set; }

        public Calendar Calendar { get; internal set; }

        public bool IsNeedSendInvitations { get; internal set; }

        public bool IsCancelled { get; internal set; }

        public SyncState State { get; set; } = SyncState.New;

        public Dictionary<string, object> OldColumnsValues { get; set; } = new Dictionary<string, object>();

        public MeetingTranscript Transcript { get; set; }

        #endregion

        #region Methods: Private

        /// <summary>
        /// Create a custom time zone.
        /// </summary>
        /// <param name="offset">TimeZone offset.</param>
        /// <param name="timeZoneCode">Time zone code.</param>
        /// <returns><see cref="TimeZoneInfo"/> instance.</returns>
        private TimeZoneInfo GetCustomTimeZone(TimeSpan offset, string timeZoneCode) {
            return TimeZoneInfo.CreateCustomTimeZone(timeZoneCode, offset, timeZoneCode, timeZoneCode);
        }

        /// <summary>
        /// Check the platform of the current system is Unix.
        /// </summary>
        /// <returns>True if current system is Unix, otherwise - false.</returns>
        private bool IsUnixOS() {
            OperatingSystem os = Environment.OSVersion;
            PlatformID platform = os.Platform;
            return platform == PlatformID.Unix || platform == PlatformID.MacOSX;
        }

        /// <summary>
        /// Gets exchange appointment <see cref="TimeZoneInfo"/>.
        /// </summary>
        /// <param name="userConnection"><see cref="UserConnection"/> instance.</param>
        /// <param name="time"><see cref="DateTime"/> timezone search time.</param>
        /// <returns>Exchange appointment <see cref="TimeZoneInfo"/>.</returns>
        private TimeZoneInfo GetExchangeAppointmentTimeZoneInfo(UserConnection userConnection, DateTime time) {
            string userTimeZoneId = userConnection.CurrentUser.TimeZoneId;
            TimeZoneInfo timeZoneInfo = userTimeZoneId == RussianStandardTimeTimeZoneId
                ? GetCustomTimeZone(new TimeSpan(CustomRussianStandardTimeTotalHoursOffset, 0, 0),
                    CustomRussianStandardTimeTimeZoneId)
                : TimeZoneUtilities.GetTimeZoneInfo(userTimeZoneId);
            if (IsUnixOS())
                timeZoneInfo =
                    GetCustomTimeZone(userConnection.CurrentUser.TimeZone.GetUtcOffset(time), userTimeZoneId);
            var adjustmentRules = timeZoneInfo.GetAdjustmentRules();
            if (adjustmentRules.Any() && !adjustmentRules.Any(ar => ar.DateEnd == DateTime.MaxValue.Date)) {
                TimeZoneInfo tz = timeZoneInfo;
                return TimeZoneInfo.CreateCustomTimeZone(tz.Id, tz.BaseUtcOffset, tz.DisplayName, tz.StandardName);
            }
            return timeZoneInfo;
        }

        /// <summary>
        /// Gets is meeting date in sync period.
        /// </summary>
        /// <param name="date">Date time of meeting.</param>
        /// <returns><c>True</c>, meeting in sync period, otherwise false.</returns>
        private bool IsDateInSyncPeriod(DateTime date) {
            return date >= Calendar?.SyncSinceDate;
        }

        /// <summary>
        /// Validating old start date.
        /// </summary>
        /// <returns><c>True</c> if in the period, otherwise - <c>False</c>.</returns>
        private bool IsOldStartDateInSyncPerion() {
            return OldColumnsValues.ContainsKey("StartDate") &&
                IsDateInSyncPeriod((DateTime)OldColumnsValues["StartDate"]);
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Adds <see cref="Participant"/>`s to <see cref="Meeting"/>.
        /// </summary>
        /// <param name="participants">Participants list.</param>
        public void AddParticipants(List<Participant> participants) {
            foreach (Participant participant in participants)
                AddParticipant(participant);
        }

        /// <summary>
        /// Adds <see cref="Participant"/>`s to <see cref="Meeting"/>.
        /// </summary>
        /// <param name="participant">Participant instance.</param>
        public void AddParticipant(Participant participant) {
            if (!Participants.Any(p => p.ContactId == participant.ContactId))
                Participants.Add(participant);
        }

        /// <summary>
        /// Delete <paramref name="participants"/> from meeting.
        /// </summary>
        /// <param name="participants">List of removal <see cref="Participant"/>.</param>
        public void DeleteParticipants(List<Participant> participants) {
            foreach (Participant participant in participants)
                Participants.RemoveAll(p => p.ContactId == participant.ContactId);
        }

        /// <summary>
        /// Clears participant list.
        /// </summary>
        public void ClearParticipants() {
            Participants.Clear();
        }

        /// <summary>
        /// Saves synchronization metadata.
        /// </summary>
        /// <param name="integrationId"><see cref="IntegrationId"/> instance.</param>
        public void SetIntegrationsId(IntegrationId integrationId) {
            RemoteId = integrationId.RemoteId;
            ICalUid = integrationId.ICalUid;
        }

        /// <summary>
        /// Setting the hash value for current meeting.
        /// </summary>
        /// <param name="hash">Hash value.</param>
        public void SetHash(string hash) {
            _hash = hash;
        }

        /// <summary>
        /// Set number of participants.
        /// </summary>
        /// <param name="numberOfParticipants">Participants count.</param>
        public void SetNumberOfParticipants(int numberOfParticipants) {
            _numberOfParticipants = numberOfParticipants;
        }

        /// <summary>
        /// Calculate and get the current value of hash.
        /// </summary>
        /// <param name="timeZone">Current user timezone.</param>
        /// <param name="useCompatibleDateFormat">Use compatible date format.</param>
        public string GetActualHash(TimeZoneInfo timeZone, bool useCompatibleDateFormat = true) {
            return _utils.GetActivityHash(new ActivityHashDataSet {
                Title = Title,
                Location = Location,
                StartDate = StartDate,
                DueDate = DueDate,
                PriorityId = PriorityId,
                Notes = Body,
                TimeZoneInfo = timeZone,
                UseCompatibleDateFormat = useCompatibleDateFormat
            });
        }

        /// <summary>
        /// #hecks if the actual meeting matches the last one.
        /// </summary>
        /// <param name="timeZone">Current user timezone.</param>
        /// <param name="ignoreParticipants">Ignore participants count for change detection flag.</param>
        /// <returns><c>True</c> if current activity not matches with latest, otherwise - <c>False</c>.</returns>
        public bool IsChanged(TimeZoneInfo timeZone, bool ignoreParticipants = false) {
            string actualHash = GetActualHash(timeZone);
            int actualParticipantCount = Participants.Count;
            return ignoreParticipants
                ? actualHash != _hash
                : actualHash != _hash || actualParticipantCount != _numberOfParticipants;
        }

        /// <summary>
        /// Checks is current meeting can be exported to external calendar.
        /// </summary>
        /// <returns><c>True</c> if meeting can be synced, <c>false</c> otherwise.</returns>
        public bool CanBeExported() {
            return !InvitesSent || IsOrganizerMeeting();
        }

        /// <summary>
        /// Checks is current meeting can be imported from external calendar.
        /// </summary>
        /// <returns><c>True</c> if meeting can be synced, <c>false</c> otherwise.</returns>
        public bool CanBeImported() {
            return !InvitesSent || OrganizerId.IsEmpty() || OrganizerId.Equals(Calendar?.OwnerId);
        }

        /// <summary>
        /// Adds this meeting to calendar.
        /// </summary>
        /// <param name="uc"><see cref="UserConnection"/> instance.</param>
        /// <param name="uc"><see cref="IMeetingRepository"/> instance.</param>
        public void AddToCalendar(UserConnection uc = null, IMeetingRepository meetingRepository = null) {
            if (CanBeExported())
                Calendar?.SaveMeeting(this, meetingRepository, uc);
        }

        /// <summary>
        /// Loads data from <paramref name="sourceMeeting"/> to current instance properties.
        /// </summary>
        /// <param name="sourceMeeting"><see cref="MeetingDto"/> instance.</param>
        public void LoadData(MeetingDto sourceMeeting, UserConnection uc) {
            TimeZoneInfo timezone = uc.CurrentUser.TimeZone;
            Title = sourceMeeting.IsPrivate ? _utils.GetLczPrivateMeeting(uc) : sourceMeeting.Title;
            Location = sourceMeeting.IsPrivate ? string.Empty : sourceMeeting.Location;
            Body = sourceMeeting.IsPrivate ? string.Empty : sourceMeeting.Body;
            StartDate = DateTimeUtils.GetUserDateTime(sourceMeeting.StartDateUtc, timezone);
            DueDate = DateTimeUtils.GetUserDateTime(sourceMeeting.DueDateUtc, timezone);
            PriorityId = sourceMeeting.PriorityId;
            _invitesSent = sourceMeeting.InvitesSent;
            RemoteItemInvitesSent = sourceMeeting.InvitesSent;
            RemindToOwner = sourceMeeting.RemindToOwner;
            RemindToOwnerDate = sourceMeeting.RemindToOwner
                ? DateTimeUtils.GetUserDateTime(sourceMeeting.RemindToOwnerDateUtc, timezone)
                : DateTime.MinValue;
            RemoteCreatedOn = sourceMeeting.RemoteCreatedOn;
            IsCancelled = sourceMeeting.IsCancelled;
        }

        /// <summary>
        /// Gets is meeting in sync period.
        /// </summary>
        /// <returns><c>True</c>, meeting in sync period, otherwise <c>false</c>.</returns>
        public bool InSyncPeriod() {
            return IsDateInSyncPeriod(StartDate) || IsOldStartDateInSyncPerion();
        }

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString() {
            return $"[Id: \"{Id}\", Title: \"{Title}\", start {StartDate}, due {DueDate}, " +
                $"CalendarSettingsId: \"{Calendar?.Settings.Id}\", CalRecordUId: \"{ICalUid}\", " +
                $"InvitationSent: \"{InvitesSent}\", ParticipantCount: \"{Participants.Count}\"]";
        }

        /// <summary>
        /// Checks is current meeting need initial export to <paramref name="calendar"/>.
        /// </summary>
        /// <param name="calendar"><see cref="Calendar"/> instance.</param>
        /// <returns><c>True</c> if current meeting requires initial export to <paramref name="calendar"/>.
        /// Returns <c>false</c> otherwise.</returns>
        public bool NeedInitialExport(Calendar calendar) {
            return SourceCalendarName == string.Empty ||
                !calendar.Settings.SenderEmailAddress.EqualsIgnoreCase(SourceCalendarName);
        }

        /// <summary>
        /// Check if a given instance of a meeting is a meeting of the organizer.
        /// </summary>
        /// <returns><c>True</c>, if this meeting is a meeting of the organizer, otherwise <c>false</c>.</returns>
        public bool IsOrganizerMeeting() {
            return OrganizerId.IsNotEmpty() && OrganizerId.Equals(Calendar?.OwnerId);
        }

        /// <summary>
        /// Check if a given instance of a meeting is local copy.
        /// </summary>
        /// <returns></returns>
        public bool IsLocalCopy() {
            return !InvitesSent && Participants.Count == 1 && Participants[0].ContactId == OrganizerId;
        }

        #endregion

    }

    #endregion

}
