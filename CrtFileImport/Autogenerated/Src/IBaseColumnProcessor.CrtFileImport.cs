namespace Terrasoft.Configuration.FileImport
{
	/// <summary>
	/// Represents base functionality for column's processor
	/// </summary>
	public interface IBaseColumnProcessor : IFileImportCanProcess, IFileImportCreateColumns, IFileImportColumnProcessError, IProcessedValuesProvider
	{ }
}

