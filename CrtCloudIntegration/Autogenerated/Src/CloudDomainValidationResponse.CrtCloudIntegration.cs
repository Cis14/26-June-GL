namespace Terrasoft.Configuration.CESModels
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region Class: ValidateDomainResponse

	/// <summary>
	/// Represents response of the service for retrieving domains for sending email.
	/// </summary>
	[DataContract]
	public class CloudDomainValidationResponse
	{

		#region Properties: Public

		/// <summary>
		/// Is Valid.
		/// </summary>
		[DataMember(Name = "isValid")]

		public bool IsValid { get; set; }

		/// <summary>
		/// Is success.
		/// </summary>
		[DataMember(Name = "isSuccess")]
		public bool IsSuccess { get; set; }

		/// <summary>
		/// Status.
		/// </summary>
		[DataMember(Name = "status")]
		public string Status { get; set; }

		/// <summary>
		/// Message.
		/// </summary>
		[DataMember(Name = "message")]
		public string Message { get; set; }

		/// <summary>
		/// Is success.
		/// </summary>
		[DataMember(Name = "recordsValidation")]
		public List<CloudRecordValidationResult> RecordsValidation { get; set; }

		#endregion

	}

	#endregion

	#region Class: RecordValidationResult

	/// <summary>
	/// Represents the result of a record validation, including its validity, a message, and the associated record category.
	/// </summary>
	[DataContract]
	public class CloudRecordValidationResult
	{

		#region Properties: Public

		/// <summary>
		/// Is Valid.
		/// </summary>
		[DataMember(Name = "isValid")]
		public bool IsValid { get; set; }

		/// <summary>
		/// Error message.
		/// </summary>
		[DataMember(Name = "message")]
		public string Message { get; set; }

		/// <summary>
		/// Record category.
		/// </summary>
		[DataMember(Name = "recordCategory")]
		public string RecordCategory { get; set; }

		#endregion

	}


	#endregion

}

