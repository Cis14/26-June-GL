namespace Terrasoft.Configuration.FileImport
{
	/// <summary>
	/// Represents base functionality for columns aggregator
	/// </summary>
	public interface IBaseColumnsAggregator : IProcessedValuesProvider, IFileImportColumnProcessError, IFileImportCreateColumns
	{
		/// <summary>
		/// Creates import column.
		/// </summary>
		/// <param name="columnValue">Column value.</param>
		/// <returns>Import column.</returns>
		ImportColumn CreateColumn(ImportColumnValue columnValue);
	}
}
