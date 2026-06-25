namespace Terrasoft.Configuration.FileImport 
{
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region  Class: FileImportFactory

	///<inheritdoc cref="IFileImportFactory"/>
	[DefaultBinding(typeof(IFileImportFactory), Name = nameof(FileImportFactory))]
    public class FileImportFactory: IFileImportFactory
	{
		#region Methods: Public

		///<inheritdoc cref="IFileImportFactory"/>	
		public IPrimaryEntityFinder GetPrimaryEntityFinder(UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor) {
		
			if (userConnection.GetIsFeatureEnabled("UseFetchForSearchExistEntityInFileImport"))
			{
				return new PrimaryEntityFetcher(userConnection, columnsProcessor);
			}
			
			return new PrimaryEntityFinder(userConnection, columnsProcessor);
		}

		#endregion
	}

	#endregion
}
