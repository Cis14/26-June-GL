namespace Terrasoft.Configuration.CESModels
{
	using System;
	using System.Runtime.Serialization;

	#region Class: SendMessageRequest

	/// <summary>
	/// Class for request to send immediate message.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.CESModels.BaseServiceRequest" />
	[DataContract]
	public class SendMessageRequest : BaseServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the emails message.
		/// </summary>
		/// <value>
		/// The message DTO.
		/// </value>
		[DataMember(Name = "message")]
		public EmailMessage Message { get; set; }

		/// <summary>
		/// Gets or sets the date and time of sending in string format.
		/// </summary>
		/// <value>
		/// The send at.
		/// </value>
		[DataMember(Name = "send_at")]
		public string SendAt { get; set; }

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

