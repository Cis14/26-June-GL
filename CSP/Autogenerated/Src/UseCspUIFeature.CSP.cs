namespace Terrasoft.Configuration
{
	using Creatio.FeatureToggling;

	public class UseCspUI : FeatureMetadata
	{
		public UseCspUI() {
			IsEnabled = true;
			Description = "Enables new UI editor for content security policy";
		}
	}
}
