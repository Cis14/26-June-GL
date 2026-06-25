namespace Terrasoft.Core.Process.Configuration
{
	using global::Common.Logging;
	using System;
	using Terrasoft.Configuration.Translation;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;

	#region Class: GenerateStaticContentForChangedSchemasUserTask
	/// <exclude/>
	public partial class GenerateStaticContentForChangedSchemasUserTask
	{

		#region Fields: Private

		private readonly ILog _log = LogManager.GetLogger("Translation");
		private readonly ApplyTranslationActivityTracker _activityTracker = new ApplyTranslationActivityTracker();

		#endregion

		#region Methods: Private

		private void BuildConfiguration() {
			if (!GlobalAppSettings.UseStaticFileContent) {
				return;
			}
			var configurationBuilder = ClassFactory.Get<ConfigurationBuild.IAppConfigurationBuilder>();
			try {
				configurationBuilder.BuildChanged();
			} catch (AggregateException e) {
				if (ContainsFileAccessIOException(e)) {
					_log.WarnFormat("[GenerateStaticContentForChangedSchemasUserTask] File access error during BuildChanged: {0}", e.Message);
				} else {
					throw;
				}
			}
		}

		private bool ContainsFileAccessIOException(AggregateException exception) {
			if (exception == null) {
				return false;
			}
			foreach (Exception innnerException in exception.InnerExceptions) {
				var iterator = innnerException;
				while (iterator != null) {
					if (iterator is System.IO.IOException) {
						return true;
					}
					iterator = iterator.InnerException;
				}
			}
			return false;
		}

		#endregion

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {
			ApplyTranslationProcessExtension.UpdateApplyProcessStage(context, ApplySessionId,
				ApplyTranslationsStagesEnum.GenerateStaticContent);
			BuildConfiguration();
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
			return true;
		}

		#endregion

		#region Methods: Public

		public override bool CompleteExecuting(params object[] parameters) {
			return base.CompleteExecuting(parameters);
		}

		public override void CancelExecuting(params object[] parameters) {
			ApplyTranslationProcessExtension.CancelApplySession(UserConnection, ApplySessionId);
			base.CancelExecuting(parameters);
		}

		public override string GetExecutionData() {
			return string.Empty;
		}

		public override ProcessElementNotification GetNotificationData() {
			return base.GetNotificationData();
		}

		#endregion

	}

	#endregion

}