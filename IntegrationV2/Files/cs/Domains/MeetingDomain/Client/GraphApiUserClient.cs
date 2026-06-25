namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Client
{
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Client.Interfaces;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Terrasoft.Common;

    #region Class: GraphApiUserClient

    /// <summary>
    /// PoC implementation of graph API usage.
    /// </summary>
    public class GraphApiUserClient : GraphApiAbstractClient, IGraphApiUserClient
    {

        #region Fields: Private

        /// <summary>
        /// User Access Token
        /// </summary>
        private readonly string _accessToken;

        #endregion

        #region Constructors: Public

        /// <summary>
        /// .ctror.
        /// </summary>
        /// <param name="accessToken">User delegated access token.</param>
        /// <param name="log"><see cref="ICalendarLogger"/> implementation instance.</param>
        public GraphApiUserClient(string accessToken, ICalendarLogger log) {
            _accessToken = accessToken;
            _log = log;
        }

        #endregion

        #region Methods: Private

        private async Task<JObject> GetOnlineMeetingByJoinUrlAsync(string joinUrl) {
            if (string.IsNullOrWhiteSpace(joinUrl))
                return null;
            string joinUrlEscaped = joinUrl.Replace("'", "''");
            var filter = $"joinWebUrl eq '{joinUrlEscaped}'";
            string serviceUri = "https://graph.microsoft.com/v1.0/me/onlineMeetings?$filter=" +
                HttpUtility.UrlEncode(filter);
            string json = await SendRequest(serviceUri, "GET", null, _accessToken, "application/json");
            JObject obj = JObject.Parse(json);
            var array = obj["value"] as JArray;
            if (array == null || array.Count == 0)
                return null;
            return (JObject)array[0];
        }

        /// <summary>
        /// Fetches events from Graph API filtered by iCalUIds.
        /// Handles pagination by following @odata.nextLink.
        /// </summary>
        /// <param name="iCalList">List of iCalUIds to filter by.</param>
        /// <returns>JArray of events.</returns>
        private async Task<JArray> GetEventsByICalUIds(List<string> iCalList) {
            string[] filterParts = iCalList.Select(id => $"iCalUId eq '{id.Replace("'", "''")}'").ToArray();
            string filter = string.Join(" or ", filterParts);
            string eventsUri = "https://graph.microsoft.com/v1.0/me/events" + "?$select=onlineMeeting,iCalUId" +
                "&$filter=" + HttpUtility.UrlEncode(filter);
            var allEvents = new JArray();
            string nextLink = eventsUri;
            while (!string.IsNullOrEmpty(nextLink)) {
                try {
                    string eventsJson = await SendRequest(nextLink, "GET", null, _accessToken, "application/json");
                    JObject eventsObj = JObject.Parse(eventsJson);
                    if (eventsObj["value"] is JArray pageEvents) {
                        foreach (var ev in pageEvents) {
                            allEvents.Add(ev);
                        }
                    }
                    nextLink = eventsObj["@odata.nextLink"]?.Value<string>();
                }
                catch (Exception ex) {
                    _log?.LogError($"Error fetching events page: {nextLink}", ex);
                    break;
                }
            }
            return allEvents;
        }

        /// <summary>
        /// Builds a map of joinUrl to list of iCalUIds from events array.
        /// </summary>
        /// <param name="eventsArray">Array of event objects from Graph API.</param>
        /// <returns>Dictionary mapping joinUrl to list of iCalUIds.</returns>
        private static Dictionary<string, List<string>> BuildJoinUrlMap(JArray eventsArray) {
            var joinUrlMap = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            foreach (JToken evToken in eventsArray) {
                if (!(evToken is JObject ev))
                    continue;
                var iCalUId = ev.Value<string>("iCalUId");
                if (string.IsNullOrWhiteSpace(iCalUId))
                    continue;
                var onlineMeeting = ev["onlineMeeting"] as JObject;
                var joinUrl = onlineMeeting?["joinUrl"]?.Value<string>();
                if (string.IsNullOrWhiteSpace(joinUrl))
                    continue;
                if (!joinUrlMap.TryGetValue(joinUrl, out var list)) {
                    list = new List<string>();
                    joinUrlMap[joinUrl] = list;
                }
                if (!list.Contains(iCalUId, StringComparer.OrdinalIgnoreCase))
                    list.Add(iCalUId);
            }
            return joinUrlMap;
        }

        /// <summary>
        /// Fetches all transcript metadata for a given online meeting.
        /// </summary>
        /// <param name="meetingId">Online meeting ID from Graph API.</param>
        /// <param name="iCalUIds"></param>
        /// <returns>List of transcript metadata.</returns>
        private async Task<List<TranscriptMetadata>> GetTranscriptsForMeeting(string meetingId, List<string> iCalUIds) {
            var results = new List<TranscriptMetadata>();
            var transcriptsUri =
                $"https://graph.microsoft.com/v1.0/me/onlineMeetings/{HttpUtility.UrlEncode(meetingId)}/transcripts";
            string transcriptsJson = await SendRequest(transcriptsUri, "GET", null, _accessToken, "application/json");
            JObject transcriptsObj = JObject.Parse(transcriptsJson);
            if (!(transcriptsObj["value"] is JArray transcriptsArray) || transcriptsArray.Count == 0)
                return results;
            foreach (JToken trToken in transcriptsArray) {
                if (!(trToken is JObject tr))
                    continue;
                var transcriptId = tr.Value<string>("id");
                var contentUrl = tr.Value<string>("transcriptContentUrl");
                if (string.IsNullOrWhiteSpace(contentUrl) || string.IsNullOrWhiteSpace(transcriptId))
                    continue;
                var createdDateTime = tr.Value<DateTime>("createdDateTime");
                results.AddRange(iCalUIds.Select(iCalUId => new TranscriptMetadata {
                    MeetingICalUId = iCalUId,
                    TranscriptUId = transcriptId,
                    TranscriptContentUrl = contentUrl,
                    CreatedDateTime = createdDateTime
                }));
            }
            return results;
        }

        /// <summary>
        /// Processes transcript metadata for a specific joinUrl and associated iCalUIds.
        /// </summary>
        /// <param name="joinUrl">Teams meeting join URL.</param>
        /// <param name="iCalUIds">List of iCalUIds associated with this joinUrl.</param>
        /// <returns>List of transcript metadata.</returns>
        private async Task<List<TranscriptMetadata>> 
            ProcessTranscriptsForJoinUrl(string joinUrl, List<string> iCalUIds) {
            JObject meeting = await GetOnlineMeetingByJoinUrlAsync(joinUrl);
            if (meeting == null)
                return new List<TranscriptMetadata>();
            var meetingId = meeting.Value<string>("id");
            if (string.IsNullOrWhiteSpace(meetingId))
                return new List<TranscriptMetadata>();
            return await GetTranscriptsForMeeting(meetingId, iCalUIds);
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Fetches transcript metadata for the given set of iCalUIds using delegated Graph calls:
        /// 1) me/events filtered by iCalUId to get onlineMeeting.joinUrl
        /// 2) me/onlineMeetings filtered by joinWebUrl to get meeting ID
        /// 3) me/onlineMeetings/{id}/transcripts to get transcript metadata (ID and content URL)
        /// Returns metadata without downloading actual transcript content.
        /// </summary>
        /// <param name="iCalUIds">Collection of iCalUIds to process.</param>
        /// <returns>Collection of <see cref="TranscriptMetadata"/> with transcript IDs and content URLs.</returns>
        public async Task<IEnumerable<TranscriptMetadata>> FetchTranscriptMetadata(IEnumerable<string> iCalUIds) {
            if (iCalUIds == null)
                throw new ArgumentNullException(nameof(iCalUIds));
            var iCalList = iCalUIds.Where(id => !string.IsNullOrWhiteSpace(id))
                .Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            if (!iCalList.Any())
                return new List<TranscriptMetadata>();
            JArray eventsArray = await GetEventsByICalUIds(iCalList);
            if (eventsArray == null || eventsArray.Count == 0)
                return new List<TranscriptMetadata>();
            var joinUrlMap = BuildJoinUrlMap(eventsArray);
            if (joinUrlMap.Count == 0)
                return new List<TranscriptMetadata>();
            var results = new List<TranscriptMetadata>();
            foreach (var kvp in joinUrlMap)
                try {
                    var transcripts = await ProcessTranscriptsForJoinUrl(kvp.Key, kvp.Value);
                    results.AddRange(transcripts);
                }
                catch (Exception ex) {
                    _log?.LogError($"Error processing transcripts for joinUrl '{kvp.Key}'.", ex);
                }
            return results;
        }

        /// <summary>
        /// Downloads the actual transcript content from the provided content URL.
        /// </summary>
        /// <param name="transcriptContentUrl">The transcript content URL from Graph API metadata.</param>
        /// <returns>The transcript text content as string.</returns>
        public async Task<string> DownloadTranscriptContent(string transcriptContentUrl) {
            if (string.IsNullOrWhiteSpace(transcriptContentUrl))
                throw new ArgumentNullException(nameof(transcriptContentUrl));
            return await SendRequest(transcriptContentUrl, "GET", null, _accessToken, "text/vtt");
        }

        #endregion

    }

    #endregion

}
