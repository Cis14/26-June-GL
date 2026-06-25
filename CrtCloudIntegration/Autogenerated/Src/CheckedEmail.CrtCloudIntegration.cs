namespace Terrasoft.Configuration.CESModels
{

	using System.Runtime.Serialization;

	#region Class: CheckedEmail

	/// <summary>
	/// Represents the email item state.
	/// </summary>
	[DataContract]
	public class CheckedEmail
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		[DataMember(Name = "email")]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the email state.
		/// </summary>
		[DataMember(Name = "is_checked")]
		public bool IsChecked { get; set; }

		#endregion

	}

	#endregion

}

