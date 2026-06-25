namespace Terrasoft.Configuration.CESModels
{
	using System;
	using System.Runtime.Serialization;

	#region Class: ValidateSenderDomainResponse

	/// <summary>
	/// Represents request for single domain validation.
	/// </summary>
	[DataContract]
	public class ValidateDomainRequest
	{

		#region Properties: Public

		/// <summary>
		/// Is Success.
		/// </summary>
		[DataMember(Name = "domainId")]
		public Guid DomainId { get; set; }

		#endregion

	}

	#endregion

}

