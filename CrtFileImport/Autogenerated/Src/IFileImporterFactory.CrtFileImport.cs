namespace Terrasoft.Configuration.FileImport
{
	using Terrasoft.Core;

	/// <summary>
	/// Represents method for create file importer
	/// </summary>
	public interface IFileImporterFactory
	{
		/// <summary>
		/// Create file importer
		/// </summary>
		/// <param name="userConnection"></param>
		/// <returns><see cref="IBaseFileImporter"/></returns>
		IBaseFileImporter GetFileImporter(UserConnection userConnection);
	}
}
