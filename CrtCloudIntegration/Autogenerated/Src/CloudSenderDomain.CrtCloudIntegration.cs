namespace Terrasoft.Configuration.CESModels
{
	using System;
	using System.Collections.Generic;

	#region Class: CloudSenderDomain

	/// <summary>
	/// Represents information about domain for sending the emails.
	/// </summary>
	public class CloudSenderDomain
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the domain address.
		/// </summary>
		public string Domain { get; set; }

		/// <summary>
		/// Gets or sets the verification error.
		/// </summary>
		public string Error { get; set; }

		/// <summary>
		/// Gets or sets the verification key.
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// Gets or sets the Mx key.
		/// </summary>
		public string Mx { get; set; }

		/// <summary>
		/// Gets or sets the SPF key.
		/// </summary>
		public string SpfKey { get; set; }

		/// <summary>
		/// Gets or sets the status of the domain.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the sender domains information identifier.
		/// </summary>
		public Guid SenderDomainsInfoId { get; set; }

		/// <summary>
		/// Gets or sets the modified on.
		/// </summary>
		public DateTime ModifiedOn { get; set; } = DateTime.Now;

		/// <summary>
		/// Gets or sets RecordDetails.
		/// </summary>
		public List<CloudSenderDomainRecordDetail> RecordDetails { get; set; } = new List<CloudSenderDomainRecordDetail>();

		#endregion

	}

	#endregion
}
