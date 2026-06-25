namespace Terrasoft.Configuration.FileImport
{
	using System.Collections.Generic;
	using Terrasoft.Core.Entities;

	public interface IPrimaryImportEntitiesGetter
	{
		/// <summary>
		/// Gets primary entities.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		/// <param name="keyColumns">Key columns.</param>
		/// <returns>Primary entities.</returns>
		IEnumerable<Entity> Get(ImportParameters parameters, IEnumerable<ImportColumn> keyColumns);
	}
}
