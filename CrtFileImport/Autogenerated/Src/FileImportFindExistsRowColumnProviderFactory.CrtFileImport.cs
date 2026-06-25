namespace Terrasoft.Configuration.FileImport
{
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: FileImportFindExistsRowColumnProviderFactory

	/// <inheritdoc cref="FileImportFindExistsRowColumnProviderFactory"/>
	[DefaultBinding(typeof(IFileImportFindExistsRowColumnProviderFactory),
		Name = nameof(FileImportFindExistsRowColumnProviderFactory))]
	public class FileImportFindExistsRowColumnProviderFactory : IFileImportFindExistsRowColumnProviderFactory
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		public FileImportFindExistsRowColumnProviderFactory(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="FileImportFindExistsRowColumnProviderFactory.GetColumnProvider"/>
		public IFileImportFindExistsRowColumnProvider GetColumnProvider() {
			return _userConnection.GetIsFeatureEnabled("UseTextColumnsToSearchExistsRecordsInFileImport")
				? ClassFactory.Get<IFileImportFindExistsRowByTextColumnProvider>() as
					IFileImportFindExistsRowColumnProvider
				: ClassFactory.Get<IFileImportFindExistsRowByTypedColumnsProvider>() as
					IFileImportFindExistsRowColumnProvider;
		}

		#endregion

	}

	#endregion

}
