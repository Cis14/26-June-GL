namespace Terrasoft.Configuration.FileImport
{
	public interface IFileImportTagManager
	{
		/// <summary>
		/// Creates tags.
		/// </summary>
		/// <param name="sender">Event sender object.</param>
		/// <param name="args">Event arguments.</param>
		void CreateTags(object sender, BeforeImportEntitiesSaveEventArgs args);

		/// <summary>
		/// Saves all tags for each imported entity.
		/// </summary>
		/// <param name="sender">Event sender object.</param>
		/// <param name="args">Event arguments.</param>
		void SaveEntityTags(object sender, ImportEntitySavedEventArgs args);

		/// <summary>
		/// Deletes tags if entities doesn't import.
		/// </summary>
		/// <param name="sender">Event sender object.</param>
		/// <param name="args">Event arguments.</param>
		void DeleteNotUsedTags(object sender, AfterImportEntitiesSaveEventArgs args);

	}
}
