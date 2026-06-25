namespace CrtCloudIntegration.Models.Requests
{
	using System.Runtime.Serialization;

	/// <summary>
	/// Represents a request to start the authentication flow.
	/// </summary>
	[DataContract]
	public class BaseRequest
	{

		#region Properties: Public

		/// <summary>
		/// The application identifier.
		/// </summary>
		[DataMember(Name = "application")]
		public string Application { get; set; }

		/// <summary>
		/// The platform name.
		/// </summary>
		[DataMember(Name = "platform")]
		public string Platform { get; set; }

		/// <summary>
		/// Gets or sets the websocket session identifier.
		/// </summary>
		[DataMember(Name = "websocketSessionId")]
		public string WebsocketSessionId { get; set; }

		#endregion

	}
}


