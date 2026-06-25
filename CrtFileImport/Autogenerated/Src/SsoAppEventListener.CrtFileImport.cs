namespace Terrasoft.Configuration
{
	using Terrasoft.Core;
	using Terrasoft.Web.Common;

	#region Class: SsoAppEventListener

	/// <summary>
	/// Class provides entry point for SSO actions on user session start.
	/// </summary>
	public class SsoAppEventListener : AppEventListenerBase
	{

		#region Methods: Public

		/// <summary>
		/// Starts update sso contact task.
		/// </summary>
		/// <param name="context"><see cref="AppEventContext"/> instance.</param>
		public override void OnSessionStart(AppEventContext context) {
			if (GlobalAppSettings.FeatureSsoJitWithoutScheduler) {
				Core.Tasks.Task.StartNewWithUserConnection<UpdateSsoContactBackgroundTask, bool>(true);
			}
		}

		#endregion

	}

	#endregion

}
