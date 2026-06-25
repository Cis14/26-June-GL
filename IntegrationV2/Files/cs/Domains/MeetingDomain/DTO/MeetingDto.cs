namespace IntegrationV2.Files.cs.Domains.MeetingDomain.DTO {
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;

	#region Class: MeetingDto

	[ExcludeFromCodeCoverage]
	public class MeetingDto {

		#region Properties: Public

		public Guid Id { get; } = Guid.NewGuid();

		public string Title { get; set; }

		public DateTime StartDateUtc { get; set; }

		public DateTime DueDateUtc { get; set; }

		public string Location { get; set; }

		public string Body { get; set; }

		public Guid PriorityId { get; set; }

		public bool InvitesSent { get; set; }

		public Dictionary<string, InvitationState> Participants { get; set; } = 
			new Dictionary<string, InvitationState>(StringComparer.InvariantCultureIgnoreCase);

		public bool RemindToOwner { get; set; }

		public DateTime RemindToOwnerDateUtc { get; set; }

		public string RemoteId { get; set; }

		public string ICalUid { get; set; }

		public string OrganizerEmail { get; set; }

		public bool IsPrivate { get; set; }

		public bool IsRecurent { get; set; }

		public DateTime RemoteCreatedOn { get; set; }

		public bool IsCancelled { get; set; }

		internal bool IsChangedFromAnotherApp { get; set; } 

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="object.ToString()"/>
		public override string ToString() {
			return $"[Id: \"{Id}\", Title: \"{Title}\", StartDate: \"{StartDateUtc} (UTC)\", DueDate: \"{DueDateUtc} (UTC)\", " +
				$"InvitesSent: \"{InvitesSent}\", Location: \"{Location}\", PriorityId: \"{PriorityId}\", ICalUid: \"{ICalUid}\"]";
		}

		#endregion

	}

	#endregion

}
