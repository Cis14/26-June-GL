namespace Terrasoft.Configuration.CESModels
{
	using System.Collections.Generic;

	#region Class: SenderValidationInfo

	/// <summary>
	/// Represents response for sender validation.
	/// </summary>
	public class SenderValidationInfo
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the wrong emails list.
		/// </summary>
		/// <value>
		/// The wrong emails list.
		/// </value>
		public List<string> WrongEmailsList { get; set; }

		/// <summary>
		/// Gets or sets the validated emails list.
		/// </summary>
		/// <value>
		/// The validated emails list.
		/// </value>
		public List<string> ValidatedEmailsList { get; set; }

		#endregion

	}

	#endregion
	
	#region Class: ProviderSenderEmail

	/// <summary>
	/// Represents sender emails for selected ESP.
	/// </summary>
	public class ProviderSenderEmail
	{

		#region Properties: Public
		
		/// <summary>
		/// Gets or sets the list of sender emails.
		/// </summary>
		public List<string> SenderEmails { get; set; }
		
		/// <summary>
		/// Gets or sets the ESP name.
		/// </summary>
		public string ProviderName { get; set; }

		#endregion

	}

	#endregion

}

