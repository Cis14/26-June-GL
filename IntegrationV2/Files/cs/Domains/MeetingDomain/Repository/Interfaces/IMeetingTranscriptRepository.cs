using System.Collections.Generic;
using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;

namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces
{
    using System;

    #region Interface: IMeetingTranscriptRepository

    /// <summary>
    /// Meeting transcript repository interface.
    /// </summary>
    public interface IMeetingTranscriptRepository
    {

        #region Methods: Public

        /// <summary>
        /// Saves a transcript synchronization attempt for the specified activity.
        /// </summary>
        /// <param name="activityId">Activity identifier.</param>
        void SaveTranscriptSyncAttempt(Guid activityId);

        /// <summary>
        /// Calculates transcript synchronization intervals for activities of the specified contact
        /// within the given period.
        /// </summary>
        /// <param name="contactId">Contact identifier.</param>
        /// <param name="syncPeriodStartDate">Sync period start date (UTC).</param>
        /// <param name="currentTime">Current time (UTC).</param>
        /// <returns>Collection of tuples (ActivityId, TargetSyncDateTime).</returns>
        IEnumerable<(Guid ActivityId, DateTime TargetSyncDateTime)> GetTranscriptSyncInterval(Guid contactId,
            DateTime syncPeriodStartDate, DateTime currentTime);

        /// <summary>
        /// Returns transcript sync attempts grouped by activity.
        /// </summary>
        /// <param name="activityIds">Activity identifiers.</param>
        /// <param name="minTargetSyncTime">Minimal target sync time.</param>
        /// <returns>Dictionary ActivityId -> list of attempt CreatedOn dates (UTC).</returns>
        IDictionary<Guid, List<DateTime>> GetTranscriptSyncAttemptsByActivity(IEnumerable<Guid> activityIds,
            DateTime minTargetSyncTime);

        /// <summary>
        /// Checks if a transcript exists by TranscriptUId (regardless of Activity).
        /// </summary>
        /// <param name="transcriptUId">Transcript unique identifier from Graph API.</param>
        /// <returns>True if exists, false otherwise.</returns>
        bool TranscriptExists(string transcriptUId);

        /// <summary>
        /// Creates a new meeting transcript record.
        /// </summary>
        /// <param name="meetingTranscript"><see cref="MeetingTranscript"/> instance to insert.</param>
        /// <returns>Created <see cref="MeetingTranscript"/> instance with populated Id.</returns>
        MeetingTranscript CreateTranscript(MeetingTranscript meetingTranscript);

        #endregion

    }

    #endregion

}
