namespace Terrasoft.Configuration.BpmonlineCloudIntegration
{
	using System;

	#region Class: RequestTokenException

	[Serializable()]
	public class RequestTokenException : Exception
	{
		
		#region Constructors: Public

		public RequestTokenException() {
		}

		public RequestTokenException(string message) : base(message) {
		}

		public RequestTokenException(string message, Exception innerException) : base(message, innerException) {			
		}

		#endregion

		#region Properties: Public

		public override string Message => "Client token request was raised an error.";

		public string ErrorCode { get; set; } = string.Empty;

		#endregion
	}
	
	#endregion
}
