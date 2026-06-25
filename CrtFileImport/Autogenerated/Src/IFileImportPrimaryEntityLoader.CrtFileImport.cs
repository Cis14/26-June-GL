namespace Terrasoft.Configuration.FileImport
{

	#region Interface: IFileImportPrimaryEntityLoader

	/// <summary>
	/// Loads data for primary entities.
	/// </summary>
	public interface IFileImportPrimaryEntityLoader
	{

		#region Methods: Public

		/// <summary>
		/// Loads column values for the primary entities.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		void LoadEntities(ImportParameters parameters);

		#endregion

	}

	#endregion

}


