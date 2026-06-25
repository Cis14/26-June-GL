namespace Terrasoft.Configuration.FileImport
{
	using System;

	#region Interface: IFileImportInfoLogger

	public interface IFileImportInfoLogger
	{

		#region Methods: Public

		/// <summary>
		/// Write session action start message.
		/// </summary>
		/// <param name="importSessionId"> Import session id.</param>
		/// <param name="message">Log message.</param>
		void WriteSessionActionStartMessage(Guid importSessionId, string message);

		/// <summary>
		/// Write session action message.
		/// </summary>
		/// <param name="importSessionId"> Import session id.</param>
		/// <param name="message">Log message.</param>
		void WriteSessionActionMessage(Guid importSessionId, string message);

		/// <summary>
		/// Write action message.
		/// </summary>
		/// <param name="message">Log message.</param>
		void WriteActionMessage(string message);

		/// <summary>
		/// Write session action end message.
		/// </summary>
		/// <param name="importSessionId"> Import session id.</param>
		/// <param name="message">Log message.</param>
		void WriteSessionActionEndMessage(Guid importSessionId, string message);

		#endregion

	}

	#endregion

}
