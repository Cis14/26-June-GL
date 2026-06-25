namespace Terrasoft.Configuration.FileImport
{
	using System.Collections.Generic;

	#region Interface: IPrimaryEntityFinder


	/// <summary>
	/// Find exist items and set for ImportEntity.
	/// </summary>
	public interface IPrimaryEntityFinder
	{
		/// <summary>
		/// Find primary entity.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		/// <param name="keysImportColumns">Key columns.</param>
		void LoadPrimaryEntity(ImportParameters parameters, IEnumerable<ImportColumn> keysImportColumns);
	}

	#endregion

}
