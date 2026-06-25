namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Synchronization
{
    using Creatio.FeatureToggling;
    using IntegrationApi.Interfaces;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Client;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Client.Interfaces;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.DTO;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Utils;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Service;
    using IntegrationV2.Files.cs.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using Terrasoft.Common;
    using Terrasoft.Configuration;
    using Terrasoft.Core;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Factories;
    using Terrasoft.Core.Tasks;
    using Terrasoft.IntegrationV2.Utils;
    using SyncState = Terrasoft.Sync.SyncState;

    #region Class: CalendarSyncSession

    /// <summary>
    /// Calendar <see cref="ISyncSession"/> implementation.
    /// </summary>
    [DefaultBinding(typeof(ISyncSession), Name = "Calendar")]
    public class CalendarSyncSession : IBackgroundTask<SyncSessionArguments>, IUserConnectionRequired, ISyncSession
    {

        #region Fields: Private

        /// <summary>
        /// <see cref="ICalendarLogger"/> instance.
        /// </summary
        private ICalendarLogger _log;

        /// <summary>
        /// <see cref="UserConnection"/> instance.
        /// </summary
        private UserConnection _uc;

        /// <summary>
        /// <see cref="ICalendarRepository"/> implementation instance.
        /// </summary>
        private ICalendarRepository _calendarRepository;

        /// <summary>
        /// <see cref="IMeetingRepository"/> implementation instance.
        /// </summary>
        private IMeetingRepository _meetingRepository;

        /// <summary>
        /// <see cref="IParticipantRepository"/> implementation instance.
        /// </summary>
        private IParticipantRepository _participantRepository;

        /// <summary>
        /// <see cref="IEntitySynchronizerHelper"/> implementation instance.
        /// </summary>
        private IEntitySynchronizerHelper _syncHelper;

        /// <summary>
        /// <see cref="IActivityUtils"/> implementation instance.
        /// </summary>
        private IActivityUtils _utils;

        /// <summary>
        /// <see cref="IRecordOperationsNotificator"/> implementation instance.
        /// </summary>
        private IRecordOperationsNotificator _recordOperationsNotificator;

        /// <summary>
        /// <see cref="ITranscriptSyncService"/> implementation instance.
        /// </summary>
        private ITranscriptSyncService _transcriptSyncService;

        /// <summary>
        /// IntegrationSystem column value of EntitySynchronizer object. 
        /// </summary>
        private const string _integrationSystemName = "NewCalendarSync";

        /// <summary>
        /// Number of retries for locking.
        /// </summary>
        private readonly int _lockRetryCount = 5;

        /// <summary>
        /// Lock duration.
        /// </summary>
        private readonly int _lockRetryDelay = 10;

        #endregion

        #region Constructors: Internal

        /// <summary>
        /// .ctor.
        /// </summary>
        /// <param name="uc"><see cref="UserConnection"/> instance.</param>
        /// <param name="lockRetryCount">Number of retries for locking.</param>
        /// <param name="lockRetryDelay">Lock duration.</param>
        internal CalendarSyncSession(UserConnection uc, int lockRetryCount, int lockRetryDelay)
            : this(uc) {
            _lockRetryCount = lockRetryCount;
            _lockRetryDelay = lockRetryDelay;
        }

        #endregion

        #region Constructors: Public

        /// <summary>
        /// .ctor.
        /// </summary>
        public CalendarSyncSession() { }

        /// <summary>
        /// .ctor.
        /// </summary>
        /// <param name="uc"><see cref="UserConnection"/> instance.</param>
        public CalendarSyncSession(UserConnection uc) {
            SetUserConnection(uc);
        }

        #endregion

        #region Methods: Private

        /// <summary>
        /// Set dependendcies.
        /// </summary>
        /// <param name="uc"><see cref="UserConnection"/> instance.</param>
        private void SetDependencies(UserConnection uc, SyncSessionArguments runOptions) {
            _uc = uc;
            _syncHelper = _syncHelper ?? ClassFactory.Get<IEntitySynchronizerHelper>();
            _utils = _utils ?? ClassFactory.Get<IActivityUtils>();
            _recordOperationsNotificator = _recordOperationsNotificator ??
                ClassFactory.Get<IRecordOperationsNotificator>(new ConstructorArgument("userConnection", uc));
            _log = _log ?? (runOptions == null
                ? ClassFactory.Get<ICalendarLogger>()
                : ClassFactory.Get<ICalendarLogger>(new ConstructorArgument("sessionId", runOptions.SessionId),
                    new ConstructorArgument("modelName", GetType().Name),
                    new ConstructorArgument("syncAction", runOptions.SyncAction)));
            _calendarRepository = _calendarRepository ?? ClassFactory.Get<ICalendarRepository>(
                "CalendarInMemoryRepository", new ConstructorArgument("uc", _uc),
                new ConstructorArgument("sessionId", _log?.SessionId));
            _meetingRepository = _meetingRepository ?? ClassFactory.Get<IMeetingRepository>(
                new ConstructorArgument("uc", _uc), new ConstructorArgument("calendarRepository", _calendarRepository),
                new ConstructorArgument("sessionId", _log.SessionId));
            _participantRepository = _participantRepository ??
                ClassFactory.Get<IParticipantRepository>(new ConstructorArgument("uc", _uc),
                    new ConstructorArgument("sessionId", _log.SessionId),
                    new ConstructorArgument("calendarRepository", _calendarRepository));
            _transcriptSyncService = _transcriptSyncService ?? ClassFactory.Get<ITranscriptSyncService>(
                new ConstructorArgument("uc", _uc), new ConstructorArgument("calendarRepository", _calendarRepository),
                new ConstructorArgument("meetingRepository", _meetingRepository),
                new ConstructorArgument("transcriptProviderResolver",
                    ClassFactory.Get<ITranscriptProviderResolver>(new ConstructorArgument("log", _log),
                        new ConstructorArgument("calendarRepository", _calendarRepository),
                        new ConstructorArgument("meetingRepository", _meetingRepository),
                        new ConstructorArgument("transcriptRepository",
                            ClassFactory.Get<IMeetingTranscriptRepository>(new ConstructorArgument("uc", _uc))))));
        }

        /// <summary>
        /// Export meeting to external calendar.
        /// </summary>
        /// <param name="options"><see cref="SyncSessionArguments"/> instance.</param>
        private void ExportMeeting(SyncSessionArguments options) {
            var meetings = GetMeetingsForExport(options).Where(m => m.State != SyncState.Deleted);
            foreach (Meeting meeting in meetings) {
                if (GetIsItemLockedForSync(meeting)) {
                    _log?.LogInfo($"Meeting item is locked, item skipped {meeting} in calendar '{meeting.Calendar}'.");
                    continue;
                }
                if (options.SyncAction != SyncAction.Delete && !CanCreateEntityInRemoteStore(meeting)) {
                    _log?.LogInfo(
                        $"Meeting item is locked for export, item skipped {meeting} in calendar '{meeting.Calendar}'.");
                    continue;
                }
                try {
                    switch (options.SyncAction) {
                        case SyncAction.UpdateWithInvite:
                        case SyncAction.ExportPeriod:
                        case SyncAction.CreateOrUpdate:
                            if (meeting.Calendar.Type == IntegrationApi.Calendar.CalendarType.Google)
                                continue;
                            CreateOrUpdateMeeting(options, meeting);
                            break;
                        case SyncAction.Delete:
                        case SyncAction.DeleteWithInvite:
                            DeleteMeeting(options, meeting);
                            break;
                    }
                } finally {
                    UnlockItem(meeting);
                }
            }
        }

        /// <summary>
        /// Create or update <paramref name="meeting"/>.
        /// </summary>
        /// <param name="options"><see cref="SyncSessionArguments"/> instance.</param>
        /// <param name="meeting"><see cref="Meeting"/> instance.</param>
        private void CreateOrUpdateMeeting(SyncSessionArguments options, Meeting meeting) {
            _meetingRepository.Load(meeting);
            if (meeting.IsChanged(_uc.CurrentUser.TimeZone) || meeting.NeedInitialExport(meeting.Calendar)) {
                if (!meeting.InSyncPeriod()) {
                    _log?.LogInfo($"Meeting being processed outside the sync period, item skipped {meeting} " +
                        $"in calendar '{meeting.Calendar}'.");
                    return;
                }
                if (meeting.Calendar.OldSyncEnabled) {
                    _log?.LogInfo($"Meeting is synced for user with old calendar sync, " +
                        $"item skipped {meeting} in calendar '{meeting.Calendar}'.");
                    return;
                }
                meeting.IsNeedSendInvitations = options.SyncAction == SyncAction.UpdateWithInvite;
                meeting.AddToCalendar(_uc, _meetingRepository);
            } else {
                _log?.LogInfo($"Meeting is not changed, item skipped {meeting} in calendar '{meeting.Calendar}'.");
            }
        }

        /// <summary>
        /// Delete <paramref name="meeting"/>.
        /// </summary>
        /// <param name="meeting"><see cref="Meeting"/> instance.</param>
        private void DeleteMeeting(Meeting meeting) {
            var reason = "New export event for deleting meeting";
            _log?.LogInfo($"Delete meeting from external repository started. Reason '{reason}'. {meeting}");
            RemoveMeetingFromCalendar(meeting);
            _log?.LogInfo($"Delete meeting from external repository ended. Reason '{reason}'. {meeting}");
            SendRecordChange(meeting, EntityChangeType.Deleted, reason);
        }

        /// <summary>
        /// Delete <paramref name="meeting"/>.
        /// </summary>
        /// <param name="options"><see cref="SyncSessionArguments"/> instance.</param>
        /// <param name="meeting"><see cref="Meeting"/> instance.</param>
        private void DeleteMeeting(SyncSessionArguments options, Meeting meeting) {
            meeting.IsNeedSendInvitations = options.SyncAction == SyncAction.DeleteWithInvite;
            if (meeting.InSyncPeriod()) {
                DeleteMeeting(meeting);
            } else {
                if (options.IsBackgroundProcess) {
                    _log?.LogInfo($"Delete meeting is out of sync period from background process," +
                        $" item skipped {meeting} in calendar '{meeting.Calendar}'.");
                    return;
                }
                DeleteMeeting(meeting);
            }
        }

        /// <summary>
        /// Send record change notification for participants.
        /// </summary>
        /// <param name="meeting"><see cref="Meeting"/> instance.</param>
        /// <param name="state"><see cref="EntityChangeType"/> record change type.</param>
        /// <param name="reason">Record notification reason.</param>
        private void SendRecordChange(Meeting meeting, EntityChangeType state, string reason) {
            var participants = meeting.Participants.Where(p => p.ContactId.IsNotEmpty());
            if (!participants.Any())
                return;
            Guid ownerId = meeting.Calendar != null ? meeting.Calendar.OwnerId : Guid.Empty;
            var contactIds = state != EntityChangeType.Deleted || meeting.InvitesSent
                ? participants.Select(p => p.ContactId).ToList()
                : new List<Guid> {
                    ownerId
                };
            _recordOperationsNotificator.SendRecordChange(contactIds, "Activity", meeting.Id, state);
            _log?.LogDebug($"Message to update client UI sent '{state}' to participants contacts: " +
                $"'{string.Join(" ,", participants.Select(p => p.ContactId))}' for {meeting} with calendar owner " +
                $"{meeting.Calendar?.OwnerId}. Reason '{reason}'.");
        }

        /// <summary>
        /// Adds the current user as the participant in the meeting from internal calendar if the user is not
        /// in the participants of this meeting, but it (the meeting) is in the external calendar of this user
        /// (because in external calendar user was not added directly to the meeting like participant
        /// but was added like memmber of the added group).
        /// </summary>
        /// <param name="meetings">Synchronized <see cref="Meeting"/> instance collection.</param>
        private void CreateCurrentUserParticipant(IEnumerable<Meeting> meetings) {
            foreach (Meeting meeting in meetings.Where(m => m.InvitesSent || !m.IsOrganizerMeeting()))
                _participantRepository.CreateCurrentUserParticipant(meeting.Id);
        }

        /// <summary>
        /// Import meeting to internal calendar.
        /// </summary>
        /// <param name="options"><see cref="SyncSessionArguments"/> instance.</param>
        private void ImportPeriod(SyncSessionArguments options) {
            foreach (Guid calendarOwnerId in options.ContactIds) {
                Calendar calendar = _calendarRepository.GetOwnerCalendar(calendarOwnerId);
                if (calendar == null || !calendar.Settings.SyncEnabled ||
                    calendar.Type == IntegrationApi.Calendar.CalendarType.Google)
                    continue;
                _log?.LogInfo($"Start import meetings from {calendar}.");
                calendar.Settings.RefreshAccessToken(_uc);
                _syncHelper.ClearEntitySynchronizer(calendar.OwnerId, _uc);
                DateTime sessionStartedDateUtc = DateTime.UtcNow;
                ICalendarClient calendarClient = CalendarClientFactory.GetCalendarClient(calendar, _log.SessionId, _uc);
                var rawData = calendarClient.GetSyncPeriodMeetings(calendar, out bool isAllExternalMeetingsLoaded)
                    .Distinct(ComparerUtils.GetComparer<MeetingDto>()).ToList();
                bool isAllInternalMeetingsLoaded =
                    TrySyncImportedMeetings(calendar, rawData, out var allInternalMeetings);
                DeleteExternalMeetingsDuplicate(allInternalMeetings);
                if (!isAllExternalMeetingsLoaded)
                    continue;
                DeleteMeetingsDifference(calendar, calendarClient, rawData, sessionStartedDateUtc);
                if (isAllInternalMeetingsLoaded) {
                    calendar.LastSyncDateUtc = sessionStartedDateUtc;
                    _calendarRepository.Save(calendar);
                }
            }
        }

        /// <summary>
        /// Syncs meetings from external calendar to internal calendar.
        /// </summary>
        /// <param name="calendar"><see cref="Calendar"/> intance.</param>
        /// <param name="rawData">External mettings.</param>
        /// <param name="allMeetings">Internal meetings.</param>
        /// <returns><c>True</c>, if all loaded all external meetings, otherwise <c>false</c>.</returns>
        private bool TrySyncImportedMeetings(Calendar calendar, List<MeetingDto> rawData,
            out HashSet<Meeting> allMeetings) {
            var isLoadedAllImportItems = true;
            allMeetings = new HashSet<Meeting>();
            var invitesSentMeetings = new HashSet<Meeting>();
            foreach (MeetingDto rawMeeting in rawData.OrderByDescending(rd => rd.InvitesSent)) {
                var itemLocked = false;
                try {
                    itemLocked = LockItemForSync(rawMeeting, calendar.OwnerId);
                    if (itemLocked) {
                        var meetings = GetMeetingForImport(rawMeeting, calendar);
                        allMeetings.UnionWith(meetings);
                        invitesSentMeetings.UnionWith(meetings.Where(m => m.InvitesSent));
                        var notLocalCopyMeetings = meetings.Where(m => !IsLocalCopy(m, invitesSentMeetings));
                        foreach (Meeting meeting in notLocalCopyMeetings.Where(m => m.CanBeImported())) {
                            _log.LogInfo($"Start import meeting {meeting}.");
                            SaveMeetingToInternalRepository(meeting);
                            _log.LogInfo($"Meeting imported successfully {meeting}.");
                        }
                        if (ListenerUtils.GetIsFeatureEnabled("CreateCurrentUserParticipant"))
                            CreateCurrentUserParticipant(notLocalCopyMeetings);
                    } else {
                        isLoadedAllImportItems = false;
                        _log.LogInfo($"Meeting already synced, metting import skipped {rawMeeting}.");
                    }
                } catch (Exception e) {
                    _log.LogError($"Failed import for meeting {rawMeeting}.", e);
                } finally {
                    if (itemLocked)
                        UnlockItem(rawMeeting, calendar.OwnerId);
                }
            }
            return isLoadedAllImportItems;
        }

        /// <summary>
        /// Delete difference between external and internal calendar.
        /// </summary>
        /// <param name="calendar"><see cref="Calendar"/> instance.</param>
        /// <param name="calendarClient"><see cref="ICalendarClient"/> instance.</param>
        /// <param name="rawData">List of external meetings.</param>
        /// <param name="syncStartDate">Synchronization session start date.</param>
        private void DeleteMeetingsDifference(Calendar calendar, ICalendarClient calendarClient,
            List<MeetingDto> rawData, DateTime syncStartDate) {
            if (ListenerUtils.GetIsFeatureDisabled("InternalMeetingsDelete"))
                return;
            var meetingsForDelete = GetImportedMeetingsForDelete(rawData, calendar, calendarClient, syncStartDate);
            _log.LogDebug($"Found {meetingsForDelete.Count} meeting from import to deleting.");
            foreach (Meeting deletedMeeting in meetingsForDelete)
                try {
                    if (deletedMeeting.IsOrganizerMeeting() && !deletedMeeting.IsLocalCopy())
                        DeleteAllMetingParts(deletedMeeting);
                    else
                        HideInternalMeeting(deletedMeeting);
                } catch (SecurityException e) {
                    _log?.LogError($"Meeting deletion failed. {deletedMeeting}", e);
                }
        }

        /// <summary>
        /// Delete the remaining parts of the meeting in the internal and external calendars of participants.
        /// </summary>
        /// <param name="deletedMeeting">Meeting for deleteion.</param>
        private void DeleteAllMetingParts(Meeting deletedMeeting) {
            var deletedMeetings = _meetingRepository.GetDeletedMeetings(deletedMeeting.Id);
            foreach (Meeting item in deletedMeetings.Where(m => m.Calendar.Id != deletedMeeting?.Calendar.Id))
                RemoveMeetingFromCalendar(item);
            var deletedParticipants = deletedMeetings.SelectMany(dm => dm.Participants, (dm, p) => p);
            bool hasExtraParticipants =
                deletedMeeting.Participants.Any(p => !deletedParticipants.Any(dp => dp.ContactId == p.ContactId));
            if (deletedMeeting.InvitesSent && deletedMeeting.IsOrganizerMeeting() && hasExtraParticipants)
                deletedMeetings.Add(deletedMeeting);
            foreach (Meeting meeting in deletedMeetings)
                HideInternalMeeting(meeting);
        }

        /// <summary>
        /// Hide meeting in internal calendar.
        /// </summary>
        /// <param name="hiddenMeeting"></param>
        private void HideInternalMeeting(Meeting hiddenMeeting) {
            var reason = "Delete internal meeting from import";
            _meetingRepository.DeclineMeeting(hiddenMeeting);
            _log?.LogInfo($"Deleted meeting from internal repository. Reason '{reason}'. {hiddenMeeting}");
            SendRecordChange(hiddenMeeting, EntityChangeType.Deleted, reason);
        }

        /// <summary>
        /// Delete meetings duplicates from external calendar.
        /// </summary>
        /// <param name="meetings">All import <see cref="Meeting"/> collection.</param>
        private void DeleteExternalMeetingsDuplicate(HashSet<Meeting> meetings) {
            var localCopies = FilterLocalCopies(meetings);
            _log.LogDebug($"Found {localCopies.Count} duplication to deleting.");
            foreach (Meeting localCopy in localCopies) {
                localCopy.InvitesSent = true;
                var reason = "External meeting duplicate (local copy)";
                _log?.LogInfo($"Delete meeting from external repository started. Reason '{reason}'. {localCopy}");
                RemoveMeetingFromCalendar(localCopy);
                _log?.LogInfo($"Delete meeting from external repository ended. Reason '{reason}'. {localCopy}");
            }
        }

        /// <summary>
        /// Get meetings for export for the period for contact.
        /// </summary>
        /// <param name="contact">Contact identifier.</param>
        /// <returns><see cref="Meeting"/> instance collection.</returns>
        private List<Meeting> GetMeetingsForPeriodExport(Guid contactId) {
            var meetings = new List<Meeting>();
            if (contactId.IsNotEmpty()) {
                Calendar calendar = _calendarRepository.GetOwnerCalendar(contactId);
                if (calendar != null)
                    meetings = _meetingRepository.GetMeetings(contactId, calendar.SyncSinceDate);
            }
            return meetings;
        }

        /// <summary>
        /// Get meetings for export.
        /// </summary>
        /// <param name="options"><see cref="SyncSessionArguments"/> instance.</param>
        /// <returns><see cref="Meeting"/> instance collection.</returns>
        private List<Meeting> GetMeetingsForExport(SyncSessionArguments options) {
            List<Meeting> meetings;
            switch (options.SyncAction) {
                case SyncAction.Delete:
                case SyncAction.DeleteWithInvite:
                    meetings = _meetingRepository.GetDeletedMeetings(options.MeetingId);
                    break;
                case SyncAction.ExportPeriod:
                    meetings = GetMeetingsForPeriodExport(options.ContactIds.FirstOrDefault());
                    break;
                default:
                    meetings = _meetingRepository.GetMeetings(options.MeetingId);
                    meetings.ForEach(item => item.OldColumnsValues = options.OldColumnsValues);
                    break;
            }
            _log?.LogDebug($"Loaded '{meetings.Count}' meetings for export  =>");
            _log?.LogTrace($"\r\n{meetings.GetString()}");
            if (options.ContactIds.Any())
                meetings = meetings
                    .Where(m => m.Calendar != null && options.ContactIds.Any(c => c == m.Calendar.OwnerId)).ToList();
            else
                meetings = meetings.Where(m => m.Calendar != null).ToList();
            _log?.LogDebug($"Result meetings count '{meetings.Count}' for export processing." +
                $"\r\nLoaded meetings => \r\n{meetings.GetString()}");
            return meetings;
        }

        /// <summary>
        /// Gets all meetings for import.
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="calendar"></param>
        /// <returns>All meetings for import.</returns>
        private HashSet<Meeting> GetMeetingsForImport(List<MeetingDto> rawData, Calendar calendar) {
            var result = new HashSet<Meeting>();
            foreach (MeetingDto rawMeeting in rawData)
                result.UnionWith(GetMeetingForImport(rawMeeting, calendar));
            return result;
        }

        /// <summary>
        /// Get instances of one meeting across all participants.
        /// </summary>
        /// <param name="rawMeeting">External meeting instance.</param>
        /// <param name="calendar"><see cref="Calendar"/> instance.</param>
        /// <returns>List instances of one meeting across all participants.</returns>
        private List<Meeting> GetMeetingForImport(MeetingDto rawMeeting, Calendar calendar) {
            var result = new List<Meeting>();
            var allMeetings = _meetingRepository.GetMeetings(rawMeeting.ICalUid);
            var currentCalendarMeetings = allMeetings.Where(m =>
                (m.ICalUid == rawMeeting.ICalUid || m.ICalUid.IsNullOrEmpty()) &&
                m.Calendar?.OwnerId == calendar.OwnerId).ToList();
            if (allMeetings.Any() && currentCalendarMeetings.IsEmpty())
                result.AddRange(allMeetings.Where(m => m.Calendar?.OwnerId == calendar.OwnerId));
            if (currentCalendarMeetings.Any()) {
                foreach (Meeting existingMeeting in currentCalendarMeetings) {
                    FillMeeting(existingMeeting, rawMeeting);
                    if (existingMeeting.ICalUid.IsNullOrEmpty())
                        existingMeeting.SetIntegrationsId(new IntegrationId(rawMeeting.RemoteId, rawMeeting.ICalUid));
                    _log?.LogDebug($"Add exist meeting to raw import collection {existingMeeting}.");
                    if (rawMeeting.IsChangedFromAnotherApp) {
                        ICalendarClient calendarClient =
                            CalendarClientFactory.GetCalendarClient(calendar, _log.SessionId, _uc);
                        calendarClient.SetExtendedProperty(existingMeeting, calendar, "ApplicationPath",
                            _uc?.AppConnection?.AppSettings?.AppBinDirectory);
                        _log.LogWarn(
                            $"Synchronization of meetings for this calendar account is enabled in another application." +
                            $" Current application path '{_uc?.AppConnection?.AppSettings?.AppBinDirectory}'.");
                    }
                    result.Add(existingMeeting);
                }
            } else {
                Guid meetingId = allMeetings.Any() ? allMeetings.First().Id : rawMeeting.Id;
                var newMeeting = new Meeting(meetingId, _log?.SessionId);
                newMeeting.Calendar = calendar;
                newMeeting.SetIntegrationsId(new IntegrationId(rawMeeting.RemoteId, rawMeeting.ICalUid));
                FillMeeting(newMeeting, rawMeeting);
                _log?.LogDebug($"Add new meeting to raw import collection {newMeeting}.");
                result.Add(newMeeting);
            }
            return result;
        }

        private List<Meeting> GetImportedMeetingsForDelete(List<MeetingDto> rawData, Calendar calendar,
            ICalendarClient calendarClient, DateTime syncStartDate) {
            bool meetingCondition(Meeting m) {
                return m.Calendar?.Id == calendar.Id && m.State != SyncState.Deleted && m.CanBeImported();
            }
            if (calendar.LastSyncDateUtc > DateTime.MinValue && !rawData.Any(dto => dto.IsRecurent)) {
                var data = calendarClient.GetDeletedMeetings(calendar)
                    .Where(dm => IsMeetingMissingInCalendar(calendar, dm)).ToList();
                if (!data.Any())
                    return new List<Meeting>();
                return GetMeetingsForImport(data, calendar).Where(meetingCondition).ToList();
            }
            return _meetingRepository
                .GetDeletedMeetings(calendar.OwnerId, calendar.SyncSinceDate, calendar.SyncTillDate,
                    rawData.Select(rd => rd.ICalUid), syncStartDate).Where(meetingCondition).ToList();
        }

        private bool IsMeetingMissingInCalendar(Calendar calendar, MeetingDto meeting) {
            string calendarEmail = calendar.Settings.SenderEmailAddress;
            bool isParticipantExist =
                meeting.Participants.TryGetValue(calendarEmail, out InvitationState participantResponseType);
            bool isOrganizer =
                meeting.OrganizerEmail.Equals(calendarEmail, StringComparison.InvariantCultureIgnoreCase);
            if (isOrganizer)
                isParticipantExist &= meeting.Participants.Count > 1;
            _log.LogDebug($"Loaded deleting meeting '{meeting.Title}' with ICalUid {meeting.ICalUid}, " +
                $"IsCancelled: '{meeting.IsCancelled}', WithoutParticipant: '{!isParticipantExist}', IsDecline: " +
                $"{participantResponseType == InvitationState.Declined}, IsOrganizer: {isOrganizer}.");
            return meeting.IsCancelled || !isParticipantExist || participantResponseType == InvitationState.Declined ||
                (isOrganizer && ListenerUtils.GetIsFeatureDisabled("NotDeleteDesktopMeetings"));
        }

        private bool IsLocalCopy(Meeting meeting, IEnumerable<Meeting> invitesSentMeetings) {
            return !meeting.RemoteItemInvitesSent && invitesSentMeetings.Any(ism =>
                meeting.Id == ism.Id && meeting.ICalUid != ism.ICalUid && !ism.ICalUid.Contains(meeting.ICalUid));
        }

        private HashSet<Meeting> FilterLocalCopies(HashSet<Meeting> meetings) {
            var invitesSentMeetings = meetings.Where(m => m.InvitesSent);
            var copiesToDelete = new HashSet<Meeting>();
            foreach (Meeting invitesSentMeeting in invitesSentMeetings)
                copiesToDelete.UnionWith(meetings.Where(m => IsLocalCopy(m, invitesSentMeetings)));
            return new HashSet<Meeting>(copiesToDelete.Distinct(ComparerUtils.GetComparer<Meeting>()));
        }

        private void FillMeeting(Meeting meeting, MeetingDto meetingDto) {
            bool isMeetingCancelledInDb = meeting.IsCancelled &&
                Features.GetIsEnabled("FeatureDeclineParticipantIfActivityCanceled");
            meetingDto.Title = _utils.FixActivityTitle(meetingDto.Title, _uc);
            meeting.LoadData(meetingDto, _uc);
            meeting.ClearParticipants();
            meeting.OrganizerId = GetMeetingOrganizer(meetingDto);
            if (meetingDto.Participants.IsEmpty())
                return;
            var participants =
                _participantRepository.GetParticipants(meeting.Id, meetingDto.Participants.Keys.ToList());
            bool isFeatureCancel =
                meeting.IsCancelled && Features.GetIsEnabled("FeatureCancelActivityBySynchronization");
            foreach (Participant participant in participants) {
                meeting.AddParticipant(participant);
                if (!meetingDto.Participants.ContainsKey(participant.EmailAddress))
                    continue;
                if (isMeetingCancelledInDb) {
                    participant.SetInvitationState(InvitationState.Declined);
                    continue;
                }
                InvitationState invitationState = meetingDto.Participants[participant.EmailAddress];
                bool canChangeParticipantState = meeting.OrganizerId == meeting.Calendar?.OwnerId ||
                    participant.ContactId == meeting.Calendar?.OwnerId ||
                    participant.EmailAddress.EqualsIgnoreCase(meetingDto.OrganizerEmail);
                if (participant.ContactId == meeting.OrganizerId)
                    participant.SetInvitationState(isFeatureCancel
                        ? InvitationState.Declined
                        : InvitationState.Confirmed);
                else if (canChangeParticipantState || Features.GetIsEnabled("CanChangeParticipanStates"))
                    participant.SetInvitationState(isFeatureCancel ? InvitationState.Declined : invitationState);
            }
        }

        private Guid GetMeetingOrganizer(MeetingDto meetingDto) {
            var organizer = _participantRepository.GetParticipantContacts(new List<string> {
                meetingDto.OrganizerEmail
            }).FirstOrDefault();
            if (organizer.Key == Guid.Empty)
                return Guid.Empty;
            Calendar organizerCalendar = _calendarRepository.GetOwnerCalendar(organizer.Value);
            if (organizerCalendar == null)
                return Guid.Empty;
            return organizer.Key;
        }

        private void SaveMeetingToInternalRepository(Meeting meeting) {
            _log?.LogDebug($"Save meeting to internal repository {meeting} in calendar '{meeting.Calendar}'.");
            _meetingRepository.Save(meeting);
            _log?.LogDebug($"Meeting saved to internal repository {meeting} in calendar '{meeting.Calendar}'.");
            if (meeting.InvitesSent && (meeting.OrganizerId.IsEmpty() || meeting.IsOrganizerMeeting()) &&
                meeting.Participants.Any())
                _participantRepository.UpdateMeetingParticipants(meeting.Participants, meeting.IsOrganizerMeeting());
        }

        private bool LockItemForSync(MeetingDto meeting, Guid ownerId) {
            var policy = new Terrasoft.Sync.SyncRetryPolicy<Exception>();
            bool lockResult = policy.Execute(_lockRetryCount, TimeSpan.FromSeconds(_lockRetryDelay),
                (locked) => !locked, () => {
                    return _syncHelper.LockItemForSync(meeting.ICalUid, ownerId, _integrationSystemName, _uc);
                });
            if (lockResult)
                _log?.LogDebug($"Lock meeting for sync {meeting}.");
            else
                _log?.LogInfo($"Meeting already synced, lock skipped {meeting}.");
            return lockResult;
        }

        private void UnlockItem(MeetingDto meeting, Guid ownerId) {
            _log?.LogDebug($"Unlock meeting for sync {meeting} for {ownerId}.");
            var ids = new List<string> {
                meeting.ICalUid
            };
            _syncHelper.UnlockEntities(ids, ownerId, _integrationSystemName, _uc);
            _syncHelper.UnlockEntities(ids, _uc.CurrentUser.ContactId, _integrationSystemName, _uc);
        }

        private void UnlockItem(Meeting meeting) {
            _log?.LogDebug($"Unlock meeting for sync {meeting}.");
            _syncHelper.UnlockEntity(meeting.Id, meeting.Calendar.OwnerId, _integrationSystemName, _uc);
        }

        private bool GetIsItemLockedForSync(Meeting meeting) {
            if (meeting.ICalUid == null)
                return false;
            return _syncHelper.GetEntityLockedForSync(meeting.ICalUid, _integrationSystemName, _uc);
        }

        private bool CanCreateEntityInRemoteStore(Meeting meeting) {
            var policy = new Terrasoft.Sync.SyncRetryPolicy<Exception>();
            var lockResult = policy.Execute<bool>(_lockRetryCount, TimeSpan.FromSeconds(_lockRetryDelay),
                (locked) => !locked, () => {
                    return _syncHelper.CanCreateEntityInRemoteStore(meeting.Id, meeting.Calendar.OwnerId, _uc,
                        _integrationSystemName);
                });
            return lockResult;
        }

        private void RemoveMeetingFromCalendar(Meeting meeting) {
            meeting.Calendar?.RemoveMeeting(meeting, _uc);
            _meetingRepository.Delete(meeting);
        }

        #endregion

        #region Methods: Public

        /// <inheritdoc cref="IBackgroundTask.Run(TParameters)"/>.
        public void Run(SyncSessionArguments runOptions) {
            SetDependencies(_uc, runOptions);
            if (!_uc.LicHelper.GetHasExplicitlyLicensedOperationLicense(LicenseConsts.CalendarSynchronization)) {
                _log?.LogWarn(
                    $"Calendar synchronization process is not licensed. Please request {LicenseConsts.CalendarSynchronization} license.");
                return;
            }
            try {
                switch (runOptions.SyncAction) {
                    case SyncAction.UpdateWithInvite:
                    case SyncAction.DeleteWithInvite:
                    case SyncAction.Delete:
                    case SyncAction.ExportPeriod:
                    case SyncAction.CreateOrUpdate:
                        _log?.LogInfo($"Calendar meetings export started.");
                        ExportMeeting(runOptions);
                        _log?.LogInfo($"Calendar meetings export ended.");
                        break;
                    case SyncAction.ImportPeriod:
                        _log?.LogInfo($"Calendar meetings import started.");
                        ImportPeriod(runOptions);
                        _log?.LogInfo($"Calendar meetings import ended.");
                        _transcriptSyncService?.SyncRecentCompletedMeetingTranscripts(_uc.CurrentUser.ContactId);
                        break;
                }
            } catch (Exception e) {
                _log.LogError($"Calendar synchronization session executing failed.", e);
            }
        }

        /// <inheritdoc cref="IUserConnectionRequired.SetUserConnection(UserConnection)"/>.
        public void SetUserConnection(UserConnection uc) {
            _uc = uc;
        }

        /// <inheritdoc cref="ISyncSession.Start"/>
        public void Start() {
            var syncAction = SyncAction.ImportPeriod;
            var options = new SyncSessionArguments {
                ContactIds = new List<Guid> {
                    _uc.CurrentUser.ContactId
                },
                SyncAction = syncAction
            };
            SetDependencies(_uc, options);
            Calendar calendar = _calendarRepository.GetOwnerCalendar(_uc.CurrentUser.ContactId);
            if (calendar == null || !calendar.Settings.SyncEnabled)
                return;
            if (calendar.LastSyncDateUtc == DateTime.MinValue) {
                options.SyncAction = SyncAction.ExportPeriod;
                _log.SetAction(SyncAction.ExportPeriod);
                Run(options);
            }
            options.SyncAction = syncAction;
            _log.SetAction(syncAction);
            Run(options);
        }

        /// <inheritdoc cref="ISyncSession.StartFailover"/>
        public void StartFailover() {
            throw new NotImplementedException();
        }

        #endregion

    }

    #endregion

}
