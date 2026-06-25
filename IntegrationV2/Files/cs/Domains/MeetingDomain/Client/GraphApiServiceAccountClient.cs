namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Client
{
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Client.Interfaces;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
    using Terrasoft.Common;

    #region Class: GraphApiServiceAccountClient

    /// <summary>
    /// PoC implementation of graph API usage.
    /// </summary>
    public class GraphApiServiceAccountClient : GraphApiAbstractClient, IGraphApiServiceAccountClient
    {

        #region Fields: Private

        /// <summary>
        /// Service account token.
        /// </summary>
        private readonly string _serviceAccountToken;

        /// <summary>
        /// Current Office 365 username.
        /// </summary>
        private readonly string _userName;

        #endregion

        #region Constructors: Public

        /// <summary>
        /// .ctror.
        /// </summary>
        /// <param name="serviceAccountToken">Application access token.</param>
        /// <param name="userName">Current user email address.</param>
        /// <param name="log"><see cref="ICalendarLogger"/> implementation instance.</param>
        public GraphApiServiceAccountClient(string serviceAccountToken, string userName, ICalendarLogger log) {
            _serviceAccountToken = serviceAccountToken;
            _userName = userName;
            _log = log;
            ValidateInstance();
        }

        #endregion

        #region Methods: Private

        private void ValidateInstance() {
            if (_serviceAccountToken.IsNotNullOrEmpty() && _userName.IsNotNullOrEmpty())
                return;
            throw new InvalidObjectStateException("OAuth parameters are not filled for Graph API. " +
                "Check OAuthApplication ClientId, SecretKey and TenantId columns.");
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Converts meeting with <paramref name="meetingId"/> to Teems meeting in current user calendar.
        /// </summary>
        /// <param name="meetingId">Exchange appointment unique identifier.</param>
        /// <returns>Updated meeting in JSON format.</returns>
        public async Task<string> ConvertToTeamsMeeting(string meetingId) {
            string serviceUri = $"https://graph.microsoft.com/v1.0/users/{_userName}/calendar/events/" +
                $"{HttpUtility.UrlEncode(HttpUtility.UrlEncode(meetingId))}";
            byte[] data = Encoding.UTF8.GetBytes("{\"isOnlineMeeting\": true}");
            return await SendRequest(serviceUri, "PATCH", data, _serviceAccountToken);
        }

        #endregion

    }

    #endregion

}
