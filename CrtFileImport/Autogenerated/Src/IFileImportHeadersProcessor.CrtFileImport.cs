namespace Terrasoft.Configuration.FileImport {
	using System.Collections.Generic;
	using Terrasoft.Core.Entities;
	
	public interface IFileImportHeadersCreator {
		 /// <summary>
		 /// Creates import columns for processed header's values.
		 /// </summary>
		 /// <param name="rootSchema"><see cref="EntitySchema"/>source schema for create columns.</param>
		 /// <param name="columnValues">Values from file.</param>
		 /// <returns>Collection of <see cref="ImportColumn"/></returns>
		IEnumerable<ImportColumn> CreateHeaderColumns(EntitySchema rootSchema,
			IEnumerable<ImportColumnValue> columnValues);
	}
}

