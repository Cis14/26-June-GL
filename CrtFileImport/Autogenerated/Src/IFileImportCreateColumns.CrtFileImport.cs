namespace Terrasoft.Configuration.FileImport
{
	using Terrasoft.Core.Entities;

	public interface IFileImportCreateColumns
	{
		ImportColumn CreateColumn(EntitySchema rootSchema, EntitySchemaColumn entitySchemaColumn, ImportColumnValue columnValue);
	}
}
