namespace Terrasoft.Configuration.CESModels
{

	using System.Runtime.Serialization;

	#region Class: AccountInfoRequest

	[DataContract]
	public class AccountInfoRequest : BaseServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the authentication key.
		/// </summary>
		/// <value>
		/// The authentication key.
		/// </value>
		[DataMember(Name = "authKey")]
		public string AuthKey { get; set; }

		#endregion

	}

	#endregion

}

