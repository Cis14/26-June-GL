 namespace Terrasoft.Configuration.CESModels
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents response with account's license information.
    /// </summary>
    [DataContract]
    public class EmailLicenseInformationResponse: BaseCloudResponse
    {

        /// <summary>
        /// Account's limit of emails to send per day. If -1 - unlimited.
        /// </summary>
        [DataMember(Name = "dailyLimitCount")]
        public int DailyLimitCount { get; set; }

        /// <summary>
        /// Already send emails count for today.
        /// </summary>
        [DataMember(Name = "todayUsage")]
        public int TodayUsage { get; set; }
    }
}
