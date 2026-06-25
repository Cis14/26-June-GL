namespace Terrasoft.Configuration.CESModels
{
	using System.Runtime.Serialization;

	#region Class: ValidateSenderDomainResponse

	/// <summary>
	/// Represents response of domain validation result.
	/// </summary>
	public class ValidateDomainResponse
	{

		#region Properties: Public

		/// <summary>
		/// Is Success.
		/// </summary>
		[DataMember(Name = "isSuccess")]
		public bool IsSuccess { get; set; }

		/// <summary>
		/// Is Success.
		/// </summary>
		[DataMember(Name = "isValid")]
		public bool IsValid { get; set; }

		#endregion

	}

	#endregion

}

