namespace Terrasoft.Configuration.CESModels
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region Class: EmailMessage

	/// <summary>
	/// The email message.
	/// </summary>
	[DataContract]
	public class EmailMessage
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the from_email.
		/// </summary>
		[DataMember(Name = "from_email")]
		public string from_email { get; set; }

		/// <summary>
		/// Gets or sets the from_name.
		/// </summary>
		[DataMember(Name = "from_name")]
		public string from_name { get; set; }

		/// <summary>
		/// Gets the global_merge_vars.
		/// </summary>
		[DataMember(Name = "global_merge_vars")]
		public List<merge_var> global_merge_vars { get; private set; }

		/// <summary>
		/// Gets or sets the html.
		/// </summary>
		[DataMember(Name = "html")]
		public string html { get; set; }

		/// <summary>
		/// Gets or sets the images.
		/// </summary>
		[DataMember(Name = "images")]
		public IEnumerable<image> images { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether important.
		/// </summary>
		[DataMember(Name = "important")]
		public bool important { get; set; }

		/// <summary>
		/// Gets the merge_vars.
		/// </summary>
		[DataMember(Name = "merge_vars")]
		public List<rcpt_merge_var> merge_vars { get; set; }

		/// <summary>
		/// Gets or sets the subject.
		/// </summary>
		[DataMember(Name = "subject")]
		public string subject { get; set; }

		/// <summary>
		/// Gets or sets the tags.
		/// </summary>
		[DataMember(Name = "tags")]
		public IEnumerable<string> tags { get; set; }

		/// <summary>
		/// Gets or sets the to.
		/// </summary>
		[DataMember(Name = "to")]
		public IEnumerable<EmailAddress> to { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether track_clicks.
		/// </summary>
		[DataMember(Name = "track_clicks")]
		public bool track_clicks { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether track_opens.
		/// </summary>
		[DataMember(Name = "track_opens")]
		public bool track_opens { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether inline_css.
		/// </summary>
		[DataMember(Name = "inline_css")]
		public bool inline_css { get; set; }

		/// <summary>
		/// Gets or sets a provider name to send message
		/// </summary>
		[DataMember(Name = "provider_name")]
		public string provider_name { get; set; }

		#endregion

		#region Methods: Public

		/// <summary>
		/// The add global variable.
		/// </summary>
		/// <param name="name">
		/// The name.
		/// </param>
		/// <param name="content">
		/// The content.
		/// </param>
		public void AddGlobalVariable(string name, string content) {
			if (global_merge_vars == null) {
				global_merge_vars = new List<merge_var>();
			}
			var mv = new merge_var {
				name = name,
				content = content
			};
			global_merge_vars.Add(mv);
		}

		/// <summary>
		/// Initialise global variable collection.
		/// </summary>
		public void InitGlobalVariable() {
			if (global_merge_vars == null) {
				global_merge_vars = new List<merge_var>();
			}
		}

		/// <summary>
		/// Initialise recipient variable.
		/// </summary>
		public void InitRecipientVariable() {
			if (merge_vars == null) {
				merge_vars = new List<rcpt_merge_var>();
			}
		}

		#endregion

	}

	#endregion

	#region Class: image

	/// <summary>
	/// The image.
	/// </summary>
	[DataContract]
	public class image
	{

		#region Properties: Public

		/// <summary>
		/// The content.
		/// </summary>
		[DataMember(Name = "content")]
		public string content { get; set; }

		/// <summary>
		/// The name.
		/// </summary>
		[DataMember(Name = "name")]
		public string name { get; set; }

		/// <summary>
		/// The type.
		/// </summary>
		[DataMember(Name = "type")]
		public string type { get; set; }

		#endregion

	}

	#endregion

	#region Class: merge_var

	/// <summary>
	/// The merge_var.
	/// </summary>
	[DataContract]
	public class merge_var
	{

		#region Properties: Public

		/// <summary>
		/// The content.
		/// </summary>
		[DataMember(Name = "content")]
		public string content { get; set; }

		/// <summary>
		/// The name.
		/// </summary>
		[DataMember(Name = "name")]
		public string name { get; set; }

		#endregion

	}

	#endregion

	#region Class: rcpt_merge_var

	/// <summary>
	/// The rcpt_merge_var.
	/// </summary>
	[DataContract]
	public class rcpt_merge_var
	{

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="rcpt_merge_var" /> class.
		/// </summary>
		public rcpt_merge_var() {
			vars = new List<merge_var>();
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// The rcpt.
		/// </summary>
		[DataMember(Name = "rcpt")]
		public string rcpt { get; set; }

		/// <summary>
		/// The vars.
		/// </summary>
		[DataMember(Name = "vars")]
		public List<merge_var> vars { get; set; }

		#endregion

	}

	#endregion

}

