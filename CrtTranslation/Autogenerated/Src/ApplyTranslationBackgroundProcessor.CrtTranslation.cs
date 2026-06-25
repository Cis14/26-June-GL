namespace Terrasoft.Configuration.Translation
{
	using global::Common.Logging;
	using Quartz;
	using Quartz.Impl.Triggers;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Creatio.FeatureToggling;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;

	#region  Class: ApplyTranslationBackgroundProcessor

	public class ApplyTranslationBackgroundProcessor : IJobExecutor {

		#region Constants: Public

		private const string RestartApplyTranslationTriggerName = "RestartApplyTranslationTrigger";
		private const string JobGroupName = "RestartApplyTranslation";

		#endregion

		#region Field: Private

		private static readonly ILog Logger = LogManager.GetLogger("Translation");

		#endregion

		#region Methods: Private

		private static void WriteRestartingApplyErrorLog(
				KeyValuePair<Guid, ApplyTranslationParameters> processParameter, Exception e) {
			Logger.Error($"An error occured while restarting apply translation with ApplySessionId " +
				 $"{processParameter.Value.ApplySessionId}", e);
		}

		private static Guid GetProcessElementUId(ApplyTranslationsStagesEnum applyStage) {
			switch (applyStage) {
				case ApplyTranslationsStagesEnum.Initializing:
					return Guid.Parse("F5E60CD7-5A4C-4FB3-95CB-CFDA95576DD3");
				case ApplyTranslationsStagesEnum.CleanUnusedReferences:
					return Guid.Parse("98C7D5D5-0EFF-4137-BC09-67119D35D025");
				case ApplyTranslationsStagesEnum.ProcessForceApply:
					return Guid.Parse("237F7EE6-337E-400B-A6A4-9B7C184D2002");
				case ApplyTranslationsStagesEnum.ApplyTranslations:
					return Guid.Parse("920DA4C4-C29D-45B6-8E29-DF45A12DB90F");
				case ApplyTranslationsStagesEnum.GenerateStaticContent:
					return Guid.Parse("5AB77049-CE65-4CE5-ABC6-D11F46870A14");
				case ApplyTranslationsStagesEnum.VerifyTranslations:
					return Guid.Parse("BE9FD035-FCCA-4E30-91C3-BFCE0CC51AD0");
				case ApplyTranslationsStagesEnum.CorrectInvalidTranslations:
					return Guid.Parse("21E7C4DF-F33D-42B8-85B5-F44B55992C58");
				case ApplyTranslationsStagesEnum.Completed:
				default:
					return Guid.Empty;
			}
		} 

		private static void UpdateStatus(ProcessActivity activity, UserConnection userConnection, Process owner, ProcessStatus status, string error = null) {
			var updateStatusInLog = activity.GetType()
				.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
				.FirstOrDefault(m => m.Name == "UpdateStatusInLog" && m.GetParameters().Length == 3);
			updateStatusInLog?.Invoke(activity, new object[] { userConnection, status, error });
			var updateOwnerStatus = activity.GetType()
				.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
				.FirstOrDefault(m => m.Name == "UpdateOwnerStatus" && (
					m.GetParameters().Length == 4 ||
					m.GetParameters().Length == 3
				));
			if (updateOwnerStatus != null) {
				var parameters = updateOwnerStatus.GetParameters();
				if (parameters.Length == 4) {
					updateOwnerStatus.Invoke(activity, new object[] { userConnection, owner, status, error });
				}
				else if (parameters.Length == 3) {
					updateOwnerStatus.Invoke(activity, new object[] { userConnection, owner, status });
				}
			}
		}

		private static void RestartUncompletedApply(UserConnection userConnection, ApplyTranslationParameters applyParameter) {
			var applySessionId = applyParameter.ApplySessionId;
			var processEngine = userConnection.ProcessEngine;
			var processId = applyParameter.ApplyTranslationProcessId;
			var stage = applyParameter.ApplyStage;
			var taskUId = GetProcessElementUId(stage);
			if (taskUId == Guid.Empty) {
				Logger.InfoFormat("Translation apply stage is not defined for ApplySessionId: {0}", applyParameter.ApplySessionId);
				return;
			}
			var process = processEngine.FindProcessByUId(processId.ToString(), true);
			if (process == null) {
				Logger.InfoFormat("Translation apply process with id: {0} not found.", processId);
				return;
			}
			Logger.InfoFormat("Translation apply process with id: {0} has status: {1}.", processId, process.Status);
			if (process.Status == ProcessStatus.Inactive) {
				return;
			}
			Logger.InfoFormat("Translation apply process with id: {0} try to find element with uid: {1}", processId, taskUId);
			var element = process.FindFlowElementBySchemaElementUId(taskUId) as ProcessActivity;
			Logger.InfoFormat("Translation apply process with id: {0} element with uid: {1} found: {2}", processId, taskUId, element != null);
			if (element == null) {
				return;
			}
			if (CheckIsApplySessionActive(userConnection, applySessionId)) {
				RescheduleJob(userConnection, applySessionId);
				return;
			}
			try {
				Logger.InfoFormat("Translation apply process with id: {0} element with uid: {1} try to status update", processId, element.UId);
				UpdateStatus(element, userConnection, element.Owner, ProcessStatus.Running);
				Logger.InfoFormat("Translation apply process with id: {0} element with uid: {1} status update is success", processId, element.UId);
			} catch(Exception e) {
				Logger.ErrorFormat("Translation apply process with id: {0} element with uid: {1} status update failed", e);
			}
			Logger.InfoFormat("Translation apply process with id: {0} element with uid: {1} has status: {2}", processId, taskUId, element?.Status);
			Logger.InfoFormat("Translation apply process with id: {0} try to execute element with uid: {1}", processId, element.UId);
			var result = processEngine.ExecuteProcessElementByUId(element.UId, applyParameter);
			Logger.InfoFormat("Translation apply process with id: {0} try to execute element with uid: {1} is success: {2}", processId, element.UId, result);
		}

		private static void RescheduleJob(UserConnection userConnection, Guid applySessionId) {
			const string jobName = JobGroupName;
			Logger.InfoFormat("Uncompleted apply translation process is currently active. Try rescheduling to restart with job name {0}", jobName);
			var schedulerWrapper = ClassFactory.Get<IAppSchedulerWraper>();
			if (schedulerWrapper.DoesJobExist(jobName, JobGroupName)) {
				schedulerWrapper.RemoveJob(jobName, JobGroupName);
			}
			IJobDetail job = schedulerWrapper.CreateClassJob<ApplyTranslationBackgroundProcessor>(jobName,
				JobGroupName, userConnection, isSystemUser: true);
			var startDelay = TimeSpan.FromMinutes(5);
			var startTime = DateTimeOffset.UtcNow.Add(startDelay);
			ITrigger trigger = new SimpleTriggerImpl(RestartApplyTranslationTriggerName, JobGroupName, startTime);
			var date = schedulerWrapper.Instance.ScheduleJob(job, trigger);
			Logger.InfoFormat("Rescheduled to restart uncompleted apply translation process with job name {0} at {1:yyyy-MM-ddTHH:mm:ssZ} UTC",
				jobName, date.UtcDateTime);
		}

		private static bool CheckIsApplySessionActive(UserConnection userConnection, Guid applySessionId) {
			if (Features.GetIsDisabled("UseApplyTranslationSessionActivityTracking")) {
				return false;
			}
			var key = $"ApplyTranslations_{applySessionId}";
			var lastActivityTime = userConnection.ApplicationCache[key] as DateTime?;
			return lastActivityTime.HasValue && (DateTime.UtcNow - lastActivityTime.Value).TotalMinutes <= 1;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IJobExecutor"/>
		public void Execute(UserConnection userConnection, IDictionary<string, object> parameters) {
			var repository = new ApplyTranslationsParametersRepository(userConnection);
			var processesParameters = repository.GetWithProcessIncomplete();
			foreach (var processParameter in processesParameters) {
				try {
					RestartUncompletedApply(userConnection, processParameter.Value);
				} catch (Exception e) {
					WriteRestartingApplyErrorLog(processParameter, e);
				}
			}
		}

		#endregion

	}

	#endregion

}

