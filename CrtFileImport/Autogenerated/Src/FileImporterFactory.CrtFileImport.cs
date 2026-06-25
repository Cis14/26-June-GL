namespace Terrasoft.Configuration.FileImport
{
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	/// <inheritdoc cref="IFileImporterFactory"/>
	/// <summary>
	/// Implements interface for create file importer
	/// </summary>
	[DefaultBinding(typeof(IFileImporterFactory), Name = nameof(FileImporterFactory))]
	public class FileImporterFactory : IFileImporterFactory
	{
		/// <inheritdoc cref="IFileImporterFactory"/>
		public IBaseFileImporter GetFileImporter(UserConnection userConnection) {
			var userConnectionArgs = new ConstructorArgument("userConnection", userConnection);
			if (userConnection.GetIsFeatureEnabled("UsePersistentFileImport")) {
				return ClassFactory.Get<IPersistentFileImporter>(userConnectionArgs);
			}
			return ClassFactory.Get<IFileImporter>(userConnectionArgs);
		}
	}
}
