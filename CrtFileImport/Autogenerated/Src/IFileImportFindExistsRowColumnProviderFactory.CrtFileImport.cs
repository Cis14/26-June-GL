namespace Terrasoft.Configuration.FileImport
{

	#region Interface: IFileImportFindExistsRowColumnProviderFactory

	/// <summary>
	/// Represents a factory for creating instances of <see cref="IFileImportFindExistsRowColumnProvider"/>.
	/// </summary>
	public interface IFileImportFindExistsRowColumnProviderFactory
	{

		#region Methods: Public

		/// <summary>
		/// Retrieves an instance of <see cref="IFileImportFindExistsRowColumnProvider"/> for managing column names
		/// during file import operations.
		/// </summary>
		/// <returns>An instance of <see cref="IFileImportFindExistsRowColumnProvider"/>.</returns>
		IFileImportFindExistsRowColumnProvider GetColumnProvider();

		#endregion

	}

	#endregion

}
