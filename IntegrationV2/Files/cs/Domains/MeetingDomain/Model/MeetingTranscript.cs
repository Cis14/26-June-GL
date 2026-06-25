using System;

namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Model
{

    #region Class: MeetingTranscript

    /// <summary>
    /// Meeting transcript domain model.
    /// </summary>
    public class MeetingTranscript {

        #region Properties: Public

        /// <summary>
        /// Transcript identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Transcript text.
        /// </summary>
        public string Transcript { get; set; }

        /// <summary>
        /// Meeting summary.
        /// </summary>
        public string MeetingSummary { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Modification date.
        /// </summary>
        /// 
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Meeting iCalUId from Graph API.
        /// </summary>
        public string MeetingICalUId { get; set; }

        /// <summary>
        /// Unique transcript identifier from Graph API.
        /// </summary>
        public string TranscriptUId { get; set; }

        /// <summary>
        /// Activity identifier.
        /// </summary>
        public Guid ActivityId { get; set; }

        #endregion

    }

    #endregion

}
