 namespace Terrasoft.Configuration.FileImport
{
	using Creatio.FeatureToggling;

	#region Class: FileImportFeatures

	public class FileImportFeatures
	{

		#region Class: CleanBufferedImportTableOnAppStart

		public class CleanBufferedImportTableOnAppStart : FeatureMetadata
		{

			#region Constructors: Public

			public CleanBufferedImportTableOnAppStart() {
				IsEnabled = true;
				Description = "Determines whether to clean BufferedImport table on application start";
			}

			#endregion

		}

		#endregion

	}

	#endregion

}
