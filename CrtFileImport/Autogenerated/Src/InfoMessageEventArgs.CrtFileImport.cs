namespace Terrasoft.Configuration.FileImport
{
	using System;

	#region  Class: ErrorMessageEventArgs

	public class ErrorMessageEventArgs : InfoMessageEventArgs
	{
		#region Properties: Public

		public Guid ImportSessionId { get; set; }

		public Exception Exception { get; set; }

		#endregion
	}

	#endregion

	#region  Class: InfoMessageEventArgs

	public class InfoMessageEventArgs : EventArgs
	{
		#region Properties: Public

		public string Message { get; set; }

		#endregion
	}

	#endregion
}

