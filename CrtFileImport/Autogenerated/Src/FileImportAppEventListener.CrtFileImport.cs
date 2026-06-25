namespace Terrasoft.Configuration.FileImport
{
	using Creatio.FeatureToggling;
	using Quartz;
	using Quartz.Impl.Triggers;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Web.Common;

	#region Class: FileImportAppEventListener

	/// <summary>
	///     An application event listener that restart not completed import from excel.
	/// </summary>
	/// <seealso cref="Terrasoft.Web.Common.AppEventListenerBase"/>
	public class FileImportAppEventListener : AppEventListenerBase
	{

		#region Constants: Private

		private const string FileImportTriggerName = "RestarFileImportTrigger";
		private const string JobGroupName = "RestartFileImport";

		#endregion

		#region Fields: Private

		private readonly IAppSchedulerWraper _schedulerWrapper = ClassFactory.Get<IAppSchedulerWraper>();

		#endregion

		#region Methods: Private

		private static AppConnection GetAppConnection(AppEventContext context) {
			return (AppConnection)context.Application["AppConnection"];
		}

		private void RestartUncompletedImport(UserConnection userConnection) {
			const string className = JobGroupName;
			if (_schedulerWrapper.DoesJobExist(className, JobGroupName)) {
				_schedulerWrapper.RemoveJob(className, JobGroupName);
			}
			IJobDetail job = _schedulerWrapper.CreateClassJob<FileImportBackgroundProcessor>(className, JobGroupName,
				userConnection, isSystemUser: true);
			ITrigger trigger = new SimpleTriggerImpl(FileImportTriggerName, JobGroupName);
			_schedulerWrapper.Instance.ScheduleJob(job, trigger);
		}

		private void ClearBufferedImportEntities(UserConnection userConnection) {
			var importEntitiesDataProvider =  ClassFactory.Get<IImportEntitiesDataProvider>(
				new ConstructorArgument("userConnection", userConnection));
			importEntitiesDataProvider.CleanBufferedImportEntities();
		}

		#endregion

		#region Methods: Public

		/// <summary>
		///     Start import who not been ended.
		/// </summary>
		/// <param name="context">Application event context.</param>
		public override void OnAppStart(AppEventContext context) {
			AppConnection appConnection = GetAppConnection(context);
			if (appConnection.IsFailOverProcessCompletionEnabled) {
				return;
			}
			UserConnection userConnection = appConnection.SystemUserConnection;
			if (Features.GetIsEnabled<FileImportFeatures.CleanBufferedImportTableOnAppStart>()) {
				ClearBufferedImportEntities(userConnection);
			}
			if (userConnection.GetIsFeatureEnabled("UsePersistentFileImport")) {
				RestartUncompletedImport(userConnection);
			}
		}

		#endregion

	}

	#endregion

}

