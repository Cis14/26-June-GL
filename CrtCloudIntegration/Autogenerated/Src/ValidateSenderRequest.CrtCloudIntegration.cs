namespace Terrasoft.Configuration.CESModels
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region Class: ValidateSenderRequest

	/// <summary>
	/// Class for request to validate sender's emails.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.CESModels.BaseServiceRequest" />
	[DataContract]
	public class ValidateSenderRequest : BaseServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the email list.
		/// </summary>
		/// <value>
		/// The email list.
		/// </value>
		[DataMember(Name = "emailList")]
		public IEnumerable<string> EmailList { get; set; }
		
		/// <summary>
		/// Gets or sets the ESP name.
		/// </summary>
		/// <value>
		/// The ESP name.
		/// </value>
		[DataMember(Name = "providerName")]
		public string ProviderName { get; set; }

		#endregion

	}

	#endregion

}

