namespace Terrasoft.Configuration.BpmonlineCloudIntegration
{
	using System;

    #region Class: GetCreatioUrlException

    /// <summary>
    /// The exception that is thrown when there is an error getting the Creatio url.
    /// </summary>
    [Serializable]
	public class GetCreatioUrlException : Exception
	{

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="GetCreatioUrlException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public GetCreatioUrlException(string message)
			: base(message) { }

		#endregion

	}

	#endregion

}

