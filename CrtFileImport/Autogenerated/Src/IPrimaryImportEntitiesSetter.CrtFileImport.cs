namespace Terrasoft.Configuration.FileImport
{
	using System.Collections.Generic;
	using Terrasoft.Core.Entities;

	public interface IPrimaryImportEntitiesSetter
	{
		/// <summary>
		/// Sets existing entities to import entities.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		/// <param name="entities">Existing entities.</param>
		/// <param name="keyColumns">Key columns.</param>
		void Set(ImportParameters parameters, IEnumerable<Entity> entities, IEnumerable<ImportColumn> keyColumns);
	}
}
