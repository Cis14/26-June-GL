namespace Terrasoft.Configuration.FileImport
{

	#region Class: FileImportFindExistsRowColumnInfo

	/// <summary>
	/// Provides information about imported column.
	/// </summary>
	public class FileImportFindExistsRowColumnInfo
	{

		/// <summary>
		/// The numeric value of the column index.
		/// </summary>
		public int ColumnIndex { get; set; }

		/// <summary>
		/// Import column <see cref="ImportColumn"/>.
		/// </summary>
		public ImportColumn ImportColumn { get; set; }
	}

	#endregion

	#region Interface: IFileImportFindExistsRowColumnProvider

	/// <summary>
	/// Provides functionality to manage column names for searching existing rows during file import.
	/// </summary>
	public interface IFileImportFindExistsRowColumnProvider
	{
		/// <summary>
		/// Determines database column name.
		/// </summary>
		/// <param name="columnInfo">Column info<see cref="FileImportFindExistsRowColumnInfo"/>.</param>
		/// <returns>Column name.</returns>
		string GetDbColumnName(FileImportFindExistsRowColumnInfo columnInfo);

		/// <summary>
		/// Inits columns names for search existing records.
		/// </summary>
		/// <exception cref="FileImportExceededAvailableKeyColumnsLimitException">Throws when the maximum limit of
		/// available key columns is exceeded.</exception>
		/// <param name="importParameters">Import parameters<see cref="ImportParameters"/></param>
		void InitDbColumnNameForKeyColumns(ImportParameters importParameters);

	}

	#endregion

}
