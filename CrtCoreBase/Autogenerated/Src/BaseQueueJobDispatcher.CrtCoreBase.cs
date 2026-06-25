namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Quartz;
	using Quartz.Impl.Matchers;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Web.Common;
	using global::Common.Logging;

	#region Class: BaseQueueJobDispatcher

	/// <summary>
	/// Provides logic to process queue job and handle its broken state.
	/// </summary>
	public abstract class BaseQueueJobDispatcher : IJobExecutor
	{

		#region Properties: Protected

		private IAppSchedulerWraper _appSchedulerWraper;
		/// <summary>
		/// Returns instance of <see cref="IAppSchedulerWraper"/>.
		/// </summary>
		protected IAppSchedulerWraper DefaultScheduler =>
			_appSchedulerWraper ?? (_appSchedulerWraper = ClassFactory.Get<IAppSchedulerWraper>());

		#endregion

		#region Properties: Public

		/// <summary>
		/// Name of job group.
		/// </summary>
		public abstract string JobGroupName { get; }

		/// <summary>
		/// An instance of the <see cref="UserConnection"/> class.
		/// </summary>
		public UserConnection UserConnection { get; set; }

		/// <summary>
		/// Logger.
		/// </summary>
		private ILog _logger;
		public ILog Logger {
			get => _logger ?? (_logger = GetLogger());
			set => _logger = value;
		}

		/// <summary>
		/// An instance of <see cref="IScheduler"/>.
		/// </summary>
		private IScheduler _scheduler;
		public IScheduler Scheduler {
			get => _scheduler ?? (_scheduler = GetScheduler());
			set => _scheduler = value;
		}

		#endregion

		#region Methods: Private

		private IEnumerable<JobKey> GetMonitoringJobs(IScheduler scheduler) {
			var jobKeys = GetMonitoringJobKeys(scheduler);
			return jobKeys.Where(x => x.Name.Contains(GetType().Name));
		}

		protected void TryUnscheduleJob(IScheduler scheduler, TriggerKey triggerKey) {
			try {
				scheduler.PauseTrigger(triggerKey);
				scheduler.UnscheduleJob(triggerKey);
			} catch (Exception ex) {
				var message = $"{GetType().Name} failed to unschedule job.";
				Logger.Error(message, ex);
			}
		}

		private void TryScheduleJob() {
			try {
				if (GetMonitoringJobs(Scheduler).Any()) {
					return;
				}
				var userName = UserConnection.CurrentUser.Name;
				var workspaceName = UserConnection.Workspace.Name;
				ScheduleJob(workspaceName, userName);
			} catch (Exception ex) {
				var message = $"{GetType().Name} failed to schedule job.";
				Logger.Error(message, ex);
			}
		}

		protected void ProcessSchedulerJobTriggers(IScheduler scheduler, Action<ITrigger> action) {
			foreach (var jobKey in GetMonitoringJobs(scheduler)) {
				var jobTriggers = scheduler.GetTriggersOfJob(jobKey);
				jobTriggers.ForEach(trigger => action.Invoke(trigger));
			}
		}

		private void ProcessBrokenTriggers() {
			ProcessSchedulerJobTriggers(Scheduler, (trigger) => {
				var state = Scheduler.GetTriggerState(trigger.Key);
				if (state == TriggerState.Blocked || state == TriggerState.Error) {
					TryUnscheduleJob(Scheduler, trigger.Key);
				}
				if (state == TriggerState.Paused) {
					Scheduler.ResumeTrigger(trigger.Key);
				}
			});
		}

		private void TryProcessBrokenTriggers() {
			try {
				UnscheduleUnactualJobs();
				ProcessBrokenTriggers();
			} catch (Exception ex) {
				var message = $"{GetType().Name} failed to process broken triggers.";
				Logger.Error(message, ex);
			}
		}

		private void RemoveJobForLostTriggers() {
			try {
				var count = GetJobTriggersCount(GetType().Name, out var key);
				if (count == 0) {
					Scheduler.DeleteJob(key);
				}
			} catch (Exception ex) {
				var message = $"{GetType().Name} failed to remove job.";
				Logger.Error(message, ex);
			}
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Returns specific instance of <see cref="ILog"/>.
		/// </summary>
		/// <returns>Logger instance.</returns>
		protected abstract ILog GetLogger();

		/// <summary>
		/// Returns specific instance of <see cref="IScheduler"/>.
		/// </summary>
		/// <returns>Instance of Scheduler.</returns>
		protected abstract IScheduler GetScheduler();

		/// <summary>
		/// Unschedules all jobj of DefaultScheduler in case of migration from <see cref="DefaultScheduler"/> to
		/// <see cref="Scheduler"/>.
		/// </summary>
		protected virtual void UnscheduleUnactualJobs() {
			if (Scheduler != DefaultScheduler) {
				ProcessSchedulerJobTriggers(DefaultScheduler.Instance, (trigger) => {
					TryUnscheduleJob(DefaultScheduler.Instance, trigger.Key);
				});
			}
		}

		/// <summary>
		/// Returns list of monitoring job keys by specified job group.
		/// </summary>
		/// <returns>Collection of monitoring job keys by specified job group.</returns>
		protected virtual IEnumerable<JobKey> GetMonitoringJobKeys(IScheduler scheduler) {
			var groupMatcher = GroupMatcher<JobKey>.GroupContains(JobGroupName);
			return scheduler.GetJobKeys(groupMatcher);
		}

		/// <summary>
		/// Returns count of trigger per monitoring job that matches <paramref name="jobNamePart"/>.
		/// </summary>
		/// <param name="jobNamePart">Part of the JOB_NAME to filter the monitoring job.</param>
		/// <returns>Number of triggers or -1 if job was not found.</returns>
		protected int GetJobTriggersCount(string jobNamePart, out JobKey key) {
			key = GetMonitoringJobKeys(Scheduler)
				.Where(x => x.Name.Contains(jobNamePart))
				.FirstOrDefault();
			if (key == null) {
				return -1;
			}
			return Scheduler.GetTriggersOfJob(key).Count;
		}

		/// <summary>
		/// Schedules job.
		/// </summary>
		/// <param name="workspaceName">Name of current workspace.</param>
		/// <param name="userName">Current user name.</param>
		protected abstract void ScheduleJob(string workspaceName, string userName);

		/// <summary>
		/// Provides logic to be executed in job thread.
		/// </summary>
		protected abstract void InternalExecute();

		/// <summary>
		/// Returns instance of <see cref="UserConnection"/> class.
		/// </summary>
		/// <param name="context">Application event context.</param>
		/// <returns>Instance of <see cref="UserConnection"/>.</returns>
		protected virtual UserConnection GetUserConnection(AppEventContext context) {
			if (UserConnection == null) {
				var appConection = context.Application["AppConnection"] as AppConnection;
				UserConnection = appConection.SystemUserConnection;
			}
			return UserConnection;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Checks blocked triggers for queue job and tries to reschedule job if those triggers are found.
		/// </summary>
		public virtual void TryRescheduleJob() {
			TryProcessBrokenTriggers();
			RemoveJobForLostTriggers();
			TryScheduleJob();
		}

		/// <summary>
		/// Processes queue job.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="parameters">Job parameters.</param>
		public virtual void Execute(UserConnection userConnection, IDictionary<string, object> parameters) {
			userConnection.CheckArgumentNull("userConnection");
			UserConnection = userConnection;
			try {
				InternalExecute();
			} catch (Exception ex) {
				string message = $"{GetType().Name} failed to execute job.";
				Logger.Error(message, ex);
				throw;
			}
		}

		#endregion

	}

	#endregion

}

