namespace Terrasoft.Configuration.CESModels
{

	#region Class: BaseCloudResponse

	/// <summary>
	/// Class for base response from CES with common properties.
	/// </summary>
	public class BaseCloudResponse
	{

		#region Properties: Public

		/// <summary>
		/// Gets a value indicating whether this response is success.
		/// </summary>
		/// <value>
		///   <c>true</c> if Status equals "ok"; otherwise, <c>false</c>.
		/// </value>
		public bool IsSuccess {
			get { return Status == "ok"; }
		}

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// "ok" if request succeed; otherwise "error"
		/// </value>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>
		/// The message.
		/// </value>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the response code.
		/// </summary>
		/// <value>
		/// The response code.
		/// </value>
		public int Code { get; set; }

		#endregion

	}

	#endregion

}

