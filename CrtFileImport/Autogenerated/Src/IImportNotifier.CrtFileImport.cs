namespace Terrasoft.Configuration.FileImport
{

	public interface IImportNotifier
	{
		/// <summary>
		/// Notifies client that import entity is being saved.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		void Notify(object sender, ImportEntitySavingEventArgs eventArgs);

		/// <summary>
		/// Notifies client that import is completed.
		/// </summary>
		void NotifyEnd();
	}
}
