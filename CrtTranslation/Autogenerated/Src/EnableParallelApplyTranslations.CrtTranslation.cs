namespace Terrasoft.Configuration.Translation
{
	using Creatio.FeatureToggling;

	#region Class: EnableParallelApplyTranslations

	internal class EnableParallelApplyTranslations : FeatureMetadata
	{

		#region Constructors: Public

		public EnableParallelApplyTranslations() {
			IsEnabled = true;
			Description = "Enables parallel apply translation for improved performance.";
		}

		#endregion

	}

	#endregion

}

