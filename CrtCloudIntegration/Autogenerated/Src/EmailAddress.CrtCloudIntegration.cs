namespace Terrasoft.Configuration.CESModels
{
	using System;
	using System.Runtime.Serialization;

	#region Class: EmailAddress

	/// <summary>
	/// The email address.
	/// </summary>
	[DataContract]
	public class EmailAddress
	{

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="EmailAddress" /> class.
		/// </summary>
		public EmailAddress() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="EmailAddress" /> class.
		/// </summary>
		/// <param name="email">
		/// The email.
		/// </param>
		public EmailAddress(string email) {
			this.email = email;
			name = string.Empty;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EmailAddress" /> class.
		/// </summary>
		/// <param name="id">
		/// Id of recipient.
		/// </param>
		/// <param name="email">
		/// The email.
		/// </param>
		public EmailAddress(Guid id, string email) {
			this.id = id;
			this.email = email;
			name = string.Empty;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EmailAddress" /> class.
		/// </summary>
		/// <param name="email">
		/// The email.
		/// </param>
		/// <param name="name">
		/// The name.
		/// </param>
		public EmailAddress(string email, string name) {
			this.email = email;
			this.name = name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EmailAddress" /> class.
		/// </summary>
		/// <param name="email">
		/// The email.
		/// </param>
		/// <param name="name">
		/// The name.
		/// </param>
		/// <param name="type">
		/// The type.
		/// </param>
		public EmailAddress(string email, string name, string type) {
			this.email = email;
			this.name = name;
			this.type = type;
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Gets or sets id of recipient.
		/// </summary>
		[DataMember(Name = "id")]
		public Guid id { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		[DataMember(Name = "email")]
		public string email { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		[DataMember(Name = "name")]
		public string name { get; set; }

		/// <summary>
		/// The header type to use for the recipient, defaults to "to" if not provided
		/// oneof(to, cc, bcc)
		/// </summary>
		[DataMember(Name = "type")]
		public string type { get; set; }

		/// <summary>
		/// Gets or sets the expiration date of the message.
		/// </summary>
		[DataMember(Name = "expiration_date")]
		[Obsolete("7.17.3 | Use AddTemplateRequest.ExpirationDate instead.")]
		public DateTime? expiration_date { get; set; }

		/// <summary>
		/// Replica id.
		/// </summary>
		[DataMember(Name = "replica_id")]
		public Guid replica_id { get; set; }

		/// <summary>
		/// Gets or sets the recipient merge var.
		/// </summary>
		[DataMember(Name = "rcpt_merge_var")]
		public rcpt_merge_var rcpt_merge_var { get; set; }

		#endregion

	}

	#endregion

}

