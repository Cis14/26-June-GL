using System;

namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Model 
{

	#region Class: TranscriptMetadata

	/// <summary>
	/// Transcript metadata from Graph API.
	/// </summary>
	public class TranscriptMetadata {
		/// <summary>
		/// Transcript unique identifier from Graph API.
		/// </summary>
		public string TranscriptUId { get; set; }

		/// <summary>
		/// URL to download transcript content.
		/// </summary>
		public string TranscriptContentUrl { get; set; }

		/// <summary>
		/// Meeting iCalUId.
		/// </summary>
		public string MeetingICalUId { get; set; }

		/// <summary>
		/// Transcript creation date from Graph API.
		/// </summary>
		public DateTime CreatedDateTime { get; set; }	
	}

	#endregion

}