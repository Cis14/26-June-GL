namespace CrtCloudIntegration.Utilities
{
	using Terrasoft.Core.Factories;


	#region Class: UINotifierDigitalAds

	/// <summary>
	/// Represents the implementation of UI notifier.
	/// </summary>
	/// <seealso cref="CrtCloudIntegration.Utilities.IUINotifier" />
	[DefaultBinding(typeof(IUINotifier), Name = "CloudIntegration")]
	public class UINotifierCloudIntegration: UINotifierBase
	{

		#region Properties: Protected

		protected override string SenderName => "CloudIntegration";

		#endregion

	}

	#endregion

}

