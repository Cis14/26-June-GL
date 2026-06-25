namespace Terrasoft.Configuration.FileImport {
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	/// <summary>
	/// <inheritdoc cref="IFileImportHeadersProcessorFactory"/>
	/// Provide class for create header processor
	/// </summary>
	[DefaultBinding(typeof(IFileImportHeadersProcessorFactory), Name = nameof(FileImportHeadersProcessorFactory))]
	public class FileImportHeadersProcessorFactory: IFileImportHeadersProcessorFactory {
	
		/// <summary>
		/// <inheritdoc cref="IFileImportHeadersProcessorFactory"/>
		/// </summary>
		public IFileImportHeadersCreator GetProcessor(UserConnection userConnection, EntitySchema rootSchema) {
			var userConnectionArgs = new ConstructorArgument("userConnection", userConnection);
			if (rootSchema.Name == "SysTranslation") {
				return ClassFactory.Get<SysTranslationFileImportHeadersProcessor>(userConnectionArgs);
			}
			return ClassFactory.Get<DefaultFileImportHeadersProcessor>(userConnectionArgs);
		}
	}

}
