namespace Terrasoft.Configuration.FileImport
{
	public interface IImportLogger
	{
		#region Methods: Public

		/// <summary>
		/// Handles error.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		void HandleError(object sender, ColumnProcessErrorEventArgs eventArgs);

		/// <summary>
		/// Handles exception.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		void HandleException(object sender, ImportEntitySaveErrorEventArgs eventArgs);

		/// <summary>
		/// Handles information message.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		void HandleInfoMessage(object sender, InfoMessageEventArgs eventArgs);

		/// <summary>
		/// Saves log.
		/// </summary>
		void SaveLog();

		#endregion

	}
}
