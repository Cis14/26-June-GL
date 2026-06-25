namespace Terrasoft.Configuration.CESModels
{
	using System.Runtime.Serialization;

	#region Class: ServiceInfo

	/// <summary>
	/// Definition for cloud service
	/// </summary>
	[DataContract]
	public class ServiceInfo
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the name of the service.
		/// </summary>
		[DataMember(Name = "serviceName")]
		public string ServiceName { get; set; }

		/// <summary>
		/// Gets or sets the settings of the service. <see cref="MailingServiceSettings"/>.
		/// </summary>
		[DataMember(Name = "settings")]
		public MailingServiceSettings Settings { get; set; }

		#endregion

	}

	#endregion

}

