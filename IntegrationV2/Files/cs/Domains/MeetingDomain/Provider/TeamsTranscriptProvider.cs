namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Provider
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using IntegrationApi.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Client;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Client.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Utils;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: TeamsTranscriptProvider

	/// <summary>
	/// Teams transcript provider.
	/// </summary>
	[DefaultBinding(typeof(ITranscriptProvider), Name = "teams")]
	public class TeamsTranscriptProvider : ITranscriptProvider
	{

		#region Fields: Private

		/// <summary>
		/// Maximum allowed time difference (in hours) between activity DueDate and transcript creation time.
		/// </summary>
		private const int MaxHoursDifference = 12;

		private readonly ICalendarLogger _log;
		private readonly ICalendarRepository _calendarRepository;
		private readonly IMeetingRepository _meetingRepository;
		private readonly IMeetingTranscriptRepository _transcriptRepository;
		private readonly UserConnection _uc;

		#endregion

		#region Constructors: Public

		public TeamsTranscriptProvider(UserConnection uc, ICalendarLogger log, ICalendarRepository calendarRepository, IMeetingRepository meetingRepository,
			IMeetingTranscriptRepository transcriptRepository) {
			_uc = uc ?? throw new ArgumentNullException(nameof(uc));
			_log = log ?? throw new ArgumentNullException(nameof(log));
			_calendarRepository = calendarRepository ?? throw new ArgumentNullException(nameof(calendarRepository));
			_meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
			_transcriptRepository = transcriptRepository ?? throw new ArgumentNullException(nameof(transcriptRepository));
		}

		#endregion

		#region Methods: Private

		private IGraphApiUserClient GetGraphApiClient() {
			if (ClassFactory.HasBinding(typeof(IGraphApiUserClient)))
				return ClassFactory.Get<IGraphApiUserClient>();
			var calendar = _calendarRepository.GetOwnerCalendar(_uc.CurrentUser.ContactId);
			if (calendar?.Settings == null)
				throw new InvalidOperationException($"Calendar settings not found for user {_uc.CurrentUser.ContactId}");
			var token = calendar.Settings.GetAccessToken(_uc, "Calendars.ReadWrite OnlineMeetingTranscript.Read.All OnlineMeetings.Read");
			return new GraphApiUserClient(token, _log);
		}

		/// <summary>
		/// Finds the matching Activity ID for a transcript based on ICalUId and creation date.
		/// </summary>
		/// <param name="iCalUId">Meeting ICalUId.</param>
		/// <param name="transcriptCreatedDateTime">Transcript creation date from Graph API (UTC).</param>
		/// <returns>Matched Activity ID or null if no match found.</returns>
		private Guid? FindMatchingActivityId(string iCalUId, DateTime transcriptCreatedDateTime) {
			var activities = _meetingRepository.GetActivitiesByBaseICalUId(iCalUId);
			if (!activities.Any()) {
				_log?.LogInfo($"No activities found for ICalUId {iCalUId}");
				return null;
			}
			var matchedActivity = activities
				.Select(a => new {
					a.ActivityId,
					a.DueDate,
					HoursDifference = Math.Abs((a.DueDate - transcriptCreatedDateTime).TotalHours)
				})
				.Where(a => a.HoursDifference <= MaxHoursDifference)
				.OrderBy(a => a.HoursDifference)
				.FirstOrDefault();
			if (matchedActivity == null) {
				_log?.LogInfo($"No activities found within {MaxHoursDifference} hours of transcript creation time {transcriptCreatedDateTime} for ICalUId {iCalUId}. ");
				return null;
			}
			_log?.LogDebug($"Matched transcript created at {transcriptCreatedDateTime} to activity with DueDate {matchedActivity.DueDate} " +
				$"(ActivityId: {matchedActivity.ActivityId}, difference: {matchedActivity.HoursDifference:F2} hours).");
			return matchedActivity.ActivityId;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="ITranscriptProvider.DownloadTranscripts(IList{ValueTuple{string, Guid}})"/>
		public void DownloadTranscripts(IList<(string ICalUId, Guid ActivityId)> meetings) {
			if (meetings == null || !meetings.Any()) {
				_log?.LogDebug("No meetings provided for transcript download.");
				return;
			}
			try {
				_log?.LogInfo($"Starting transcript download for {meetings.Count} meetings.");
				var iCalUIds = meetings.Select(m => m.ICalUId).ToList();
				var simplifiedICalUIds = ICalUIdHelper.SimplifyICalUIds(iCalUIds);
				var metadataList = GetGraphApiClient().FetchTranscriptMetadata(simplifiedICalUIds).GetAwaiter().GetResult().ToList();
				foreach ((string ICalUId, Guid ActivityId) meeting in meetings) {
					_transcriptRepository.SaveTranscriptSyncAttempt(meeting.ActivityId);
					_log?.LogDebug(
						$"Transcript sync attempt saved for meeting {meeting.ICalUId} (Activity: {meeting.ActivityId}).");
				}
				_log?.LogInfo($"Fetched metadata for {metadataList.Count} transcripts from Graph API.");
				foreach (var metadata in metadataList) {
					try {
						if (_transcriptRepository.TranscriptExists(metadata.TranscriptUId)) {
							_log?.LogDebug($"Transcript {metadata.TranscriptUId} already exists. Skipping.");
							continue;
						}
						var activityId = FindMatchingActivityId(metadata.MeetingICalUId, metadata.CreatedDateTime);
						if (!activityId.HasValue) {
							continue;
						}
						_log?.LogDebug($"Downloading transcript content for {metadata.TranscriptUId} from Graph API.");
						string transcriptContent = GetGraphApiClient()
							.DownloadTranscriptContent(metadata.TranscriptContentUrl)
							.GetAwaiter()
							.GetResult();
						var meetingTranscript = new MeetingTranscript {
							ActivityId = activityId.Value,
							MeetingICalUId = metadata.MeetingICalUId,
							TranscriptUId = metadata.TranscriptUId,
							Transcript = transcriptContent
						};
						_transcriptRepository.CreateTranscript(meetingTranscript);
					} catch (Exception ex) {
						_log?.LogError($"Error processing transcript for meeting {metadata.MeetingICalUId}.", ex);
					}
				}
				_log?.LogInfo("Transcript download completed successfully.");
			} catch (Exception ex) {
				_log?.LogError("Error during transcript download process.", ex);
				throw;
			}
		}

		#endregion

	}

	#endregion

}
