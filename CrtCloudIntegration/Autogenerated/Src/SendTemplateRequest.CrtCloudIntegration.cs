namespace Terrasoft.Configuration.CESModels
{
	using System;
	using System.Runtime.Serialization;

	#region Class: SendTemplateRequest

	/// <summary>
	/// Class for request to send batch of recipients by template.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.CESModels.BaseServiceRequest" />
	[DataContract]
	public class SendTemplateRequest : BaseServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the email message.
		/// </summary>
		/// <value>
		/// The <see cref="EmailMessage"/>.
		/// </value>
		[DataMember(Name = "message")]
		public EmailMessage Message { get; set; }

		/// <summary>
		/// Gets or sets the name of the template.
		/// </summary>
		/// <value>
		/// The name of the template.
		/// </value>
		[DataMember(Name = "template_name")]
		public string TemplateName { get; set; }

		/// <summary>
		/// Gets or sets the email identifier.
		/// </summary>
		/// <value>
		/// The email identifier.
		/// </value>
		[DataMember(Name = "email_id")]
		public Guid EmailId { get; set; }

		/// <summary>
		/// Gets or sets the time of sending.
		/// </summary>
		/// <value>
		/// The date and time template was sent at.
		/// </value>
		[DataMember(Name = "send_at")]
		public string SendAt { get; set; }

		#endregion

	}

	#endregion

}

