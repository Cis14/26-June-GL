namespace CrtCloudIntegration.Dto
{
	using Newtonsoft.Json;

	#region Class: WebSocketDto

	public class WebSocketDto
	{

		#region Properties: Public

		[JsonProperty("command")]
		public string Command { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("errorCode")]
		public string ErrorCode { get; set; }

		[JsonProperty("platform")]
		public string Platform { get; set; }

		[JsonProperty("isSuccess")]
		public bool IsSuccess { get; set; }

		[JsonProperty("websocketSessionId")]
		public string WebsocketSessionId { get; set; }

		#endregion

	}

	#endregion

}
