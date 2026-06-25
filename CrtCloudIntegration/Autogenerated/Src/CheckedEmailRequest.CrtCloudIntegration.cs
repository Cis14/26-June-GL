namespace Terrasoft.Configuration.CESModels
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region Class: CheckedEmailRequest

	/// <summary>
	/// Represents request to check emails to be verified on provider side.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.CESModels.BaseServiceRequest" />
	[DataContract]
	public class CheckedEmailRequest : BaseServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the list of emails to check.
		/// </summary>
		/// <value>
		/// The emails.
		/// </value>
		[DataMember(Name = "emails")]
		public IEnumerable<string> Emails { get; set; }

		#endregion

	}

	#endregion

}

