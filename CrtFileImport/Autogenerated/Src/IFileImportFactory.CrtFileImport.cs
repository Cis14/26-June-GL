namespace Terrasoft.Configuration.FileImport
{
    using Terrasoft.Core;

	#region Interface: IFileImportFactory


	/// <summary>
	/// Interface for building entities to FileImport.
	/// </summary>
	public interface IFileImportFactory
	{
		/// <summary>
		/// Get primary entity finder.
		/// </summary>
		/// <param name="userConnection">Import user settings.</param>
		/// <param name="columnsProcessor">Columns values..</param>
		IPrimaryEntityFinder GetPrimaryEntityFinder(UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor);
	}

	#endregion

}
