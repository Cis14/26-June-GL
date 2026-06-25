namespace Terrasoft.Configuration.CESModels
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region Class: UpdateCloudAccountServiceRequest

	/// <summary>
	/// Represents request to update services of the account.
	/// </summary>
	[DataContract]
	public class UpdateCloudAccountServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the API key.
		/// </summary>
		[DataMember(Name = "apiKey")]
		public string ApiKey { get; set; }

		/// <summary>
		/// Gets or sets the cloud services settings for account.
		/// </summary>
		[DataMember(Name = "services")]
		public List<ServiceInfo> Services { get; set; }

		#endregion

	}

	#endregion

}

