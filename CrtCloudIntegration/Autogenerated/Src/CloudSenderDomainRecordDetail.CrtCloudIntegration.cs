namespace Terrasoft.Configuration.CESModels
{
	using System;

	#region Class: SenderDomainRecordDetail

	/// <summary>
	/// Represents information about domain record details.
	/// </summary>
	public class CloudSenderDomainRecordDetail
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets Hostname.
		/// </summary>
		public string Hostname { get; set; }

		/// <summary>
		/// Gets or Sets Type.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets Value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets SenderDomainRecordCategory.
		/// </summary>
		public string SenderDomainRecordCategory { get; set; }

		/// <summary>
		/// Status of the record: 'active', 'inactive'.
		/// </summary>
		public string Status { get; set; }

		#endregion

	}

	#endregion
}
