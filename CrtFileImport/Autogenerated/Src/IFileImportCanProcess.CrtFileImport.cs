namespace Terrasoft.Configuration.FileImport
{
	using Terrasoft.Core.Entities;

	public interface IFileImportCanProcess
	{
		bool CanProcess(EntitySchemaColumn entitySchemaColumn);

		bool CanProcess(ImportColumnDestination destination);

	}
}

