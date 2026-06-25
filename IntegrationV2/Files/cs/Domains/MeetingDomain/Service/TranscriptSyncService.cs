using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Service
{
    using System;
    using System.Linq;
    using Creatio.FeatureToggling;
    using IntegrationApi.Interfaces;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
    using Terrasoft.Core;
    using Terrasoft.Core.Factories;

    #region Interface: ITranscriptSyncService

    /// <summary>
    /// Service responsible for synchronizing meeting transcripts.
    /// </summary>
    public interface ITranscriptSyncService
    {

        #region Methods: Public

        /// <summary>
        /// Starts synchronization of recent completed meeting transcripts for specified contact.
        /// </summary>
        /// <param name="contactId">Contact identifier.</param>
        void SyncRecentCompletedMeetingTranscripts(Guid contactId);

        #endregion

    }

    #endregion

    #region Class: TranscriptSyncService

    /// <summary>
    /// Default implementation of <see cref="ITranscriptSyncService"/>.
    /// </summary>
    [DefaultBinding(typeof(ITranscriptSyncService))]
    public class TranscriptSyncService : ITranscriptSyncService
    {

        #region Fields: Private

        private readonly UserConnection _uc;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly ITranscriptProviderResolver _transcriptProviderResolver;
        private readonly IMeetingTranscriptRepository _meetingTranscriptRepository;
        private readonly ICalendarLogger _log;

        private const int DefaultSyncPeriodDays = 14;
        private const int ActivitiesBatchSize = 80;

        #endregion

        #region Constructors: Public

        /// <summary>
        /// .ctor.
        /// </summary>
        /// <param name="uc"><see cref="UserConnection"/> instance.</param>
        /// <param name="calendarRepository"><see cref="ICalendarRepository"/> instance.</param>
        /// <param name="meetingRepository"><see cref="IMeetingRepository"/> instance.</param>
        /// <param name="transcriptProviderResolver"><see cref="ITranscriptProviderResolver"/> instance.</param>
        /// <param name="meetingTranscriptRepository"></param>
        public TranscriptSyncService(UserConnection uc, ICalendarRepository calendarRepository,
            IMeetingRepository meetingRepository, ITranscriptProviderResolver transcriptProviderResolver,
            IMeetingTranscriptRepository meetingTranscriptRepository) {
            _uc = uc ?? throw new ArgumentNullException(nameof(uc));
            _calendarRepository = calendarRepository ?? throw new ArgumentNullException(nameof(calendarRepository));
            _meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
            _transcriptProviderResolver = transcriptProviderResolver ??
                throw new ArgumentNullException(nameof(transcriptProviderResolver));
            _meetingTranscriptRepository = meetingTranscriptRepository ??
                throw new ArgumentNullException(nameof(meetingTranscriptRepository));
            _log = ClassFactory.Get<ICalendarLogger>();
            _log.SetModelName(GetType().Name);
        }

        #endregion

        #region Methds: Private

        /// <summary>
        /// Gets iCalUIds and Activity IDs for meetings that need transcript synchronization based on sync intervals.
        /// </summary>
        /// <param name="contactId">Contact identifier.</param>
        /// <param name="syncPeriodDays">Number of days to look back for completed meetings.</param>
        /// <returns>Collection of tuples containing (ICalUId, ActivityId).</returns>
        private IEnumerable<(string ICalUId, Guid ActivityId)> GetRecentlyCompletedMeetingICalUIds(Guid contactId,
            int syncPeriodDays) {
            DateTime currentTime = DateTime.UtcNow;
            DateTime syncPeriodStartDate = currentTime.AddDays(-syncPeriodDays);
            _log?.LogDebug(
                $"Looking for completed meetings within last {syncPeriodDays} days (from {syncPeriodStartDate} to {currentTime}) for contact {contactId}.");
            var activityInfo = _meetingTranscriptRepository
                .GetTranscriptSyncInterval(contactId, syncPeriodStartDate, currentTime).ToList();
            if (!activityInfo.Any()) {
                _log?.LogDebug($"No activities passed sync intervals for contact {contactId}.");
                return Enumerable.Empty<(string, Guid)>();
            }
            var allActivityIds = activityInfo.Select(ai => ai.ActivityId).Distinct().ToList();
            DateTime minTargetSyncTime = activityInfo.Min(ai => ai.TargetSyncDateTime);
            var attemptsByActivity =
                _meetingTranscriptRepository.GetTranscriptSyncAttemptsByActivity(allActivityIds, minTargetSyncTime);
            var eligibleActivityIds = new HashSet<Guid>();
            foreach ((Guid activityId, DateTime targetSync) in activityInfo) {
                if (eligibleActivityIds.Count >= ActivitiesBatchSize)
                    break;
                if (!attemptsByActivity.TryGetValue(activityId, out var attemptsForActivity)) {
                    eligibleActivityIds.Add(activityId);
                    continue;
                }
                bool hasAttemptAfterTarget = attemptsForActivity.Any(a => a > targetSync);
                if (!hasAttemptAfterTarget)
                    eligibleActivityIds.Add(activityId);
            }
            if (!eligibleActivityIds.Any()) {
                _log?.LogDebug($"No activities eligible for transcript synchronization for contact {contactId}.");
                return Enumerable.Empty<(string, Guid)>();
            }
            return _meetingRepository.GetActivitiesMetadataByLocalIds(eligibleActivityIds, contactId);
        }

        #endregion

        #region Methods: Public

        /// <inheritdoc cref="ITranscriptSyncService.SyncRecentCompletedMeetingTranscripts(Guid)"/>
        public void SyncRecentCompletedMeetingTranscripts(Guid contactId) {
            try {
                if (!Features.GetIsEnabled("TranscriptSync"))
                    return;
                Calendar calendar = _calendarRepository.GetOwnerCalendar(contactId);
                if (calendar == null || !calendar.ImportTranscripts) {
                    return;
                }
                int syncPeriodDays = DefaultSyncPeriodDays;
                if (calendar != null && calendar.SyncSinceDate != DateTime.MinValue) {
                    var days = (int)Math.Round((DateTime.UtcNow.Date - calendar.SyncSinceDate.Date).TotalDays,
                        MidpointRounding.AwayFromZero);
                    syncPeriodDays = days > 0 ? days : 1;
                }
                var meetings = GetRecentlyCompletedMeetingICalUIds(contactId, syncPeriodDays).ToList();
                if (!meetings.Any()) {
                    _log?.LogDebug($"No recently completed meetings found for contact {contactId}.");
                    return;
                }
                _log?.LogInfo(
                    $"Processing transcripts for {meetings.Count} recently completed meetings for contact {contactId}.");
                ITranscriptProvider transcriptProvider = _transcriptProviderResolver.Resolve("teams", _uc);
                transcriptProvider.DownloadTranscripts(meetings);
                _log?.LogInfo($"Successfully processed transcripts for contact {contactId}.");
            } catch (Exception ex) {
                _log?.LogError($"Error while processing transcripts for contact {contactId}.", ex);
            }
        }

        #endregion

    }

    #endregion

}
