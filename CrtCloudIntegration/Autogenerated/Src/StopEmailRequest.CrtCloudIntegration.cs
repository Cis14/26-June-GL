 namespace Terrasoft.Configuration.CESModels
{
	using System;
	using System.Runtime.Serialization;

	#region Class: AddTemplateRequest

	/// <summary>
	/// Class for request to stop email.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.CESModels.BaseServiceRequest" />
	[DataContract]
	public class StopEmailRequest : BaseServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the email identifier.
		/// </summary>
		/// <value>
		/// The email identifier.
		/// </value>
		[DataMember(Name = "email_id")]
		public Guid EmailId { get; set; }

		#endregion

	}

	#endregion

}
