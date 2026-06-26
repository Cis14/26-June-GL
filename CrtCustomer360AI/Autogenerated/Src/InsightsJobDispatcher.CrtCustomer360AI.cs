namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using global::Common.Logging;
	using Quartz;
	using Quartz.Impl.Matchers;
	using Quartz.Impl.Triggers;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: InsightsJobDispatcher

	/// <summary>
	/// Provides methods for Insights schema job management.
	/// </summary>
	public class InsightsJobDispatcher
	{

		#region Constants: Private
		
		private const string _insightsSchedulerName = "NewsAndInsightsQuartzScheduler";
		private const string ScheduledNewsAndInsightsJobGroup = "NewsAndInsights";
		private const string ScheduledNewsAndInsightsJobProcessName = "AccountNewsAndInsightsScheduleExecutor";

		#endregion

		#region Fields: Private

		private UserConnection _userConnection;
		private IAppSchedulerWraper _appSchedulerWraper;

		#endregion

		#region Constructors: Public

		public InsightsJobDispatcher(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		public InsightsJobDispatcher(UserConnection userConnection, IAppSchedulerWraper schedulerWraper)
			: this(userConnection) {
			_appSchedulerWraper = schedulerWraper;
		}

		#endregion

		#region Properties: Private

		/// <summary>
		/// Gets or sets the process service provider.
		/// </summary>
		private IAppSchedulerWraper AppScheduler {
			get {
				return _appSchedulerWraper ?? (_appSchedulerWraper = ClassFactory.Get<IAppSchedulerWraper>());
			}
		}

		/// <summary>
		/// Instance of <see cref="IScheduler"/> with name <see cref="_insightsSchedulerName"/> or default.
		/// </summary>
		public IScheduler InsightsScheduler => AppScheduler.FindScheduler(_insightsSchedulerName)
			?? AppScheduler.Instance;

		#endregion

		#region Methods: Public

		/// <summary>
		/// Schedules a job for the specified Insights schema.
		/// </summary>
		/// <param name="frequency">Job execution frequency.</param>
		/// <param name="isImmediateJob">Whether to run the job immediately.</param>
		public void ScheduleJob(string frequency) {
			try {
				var insightsJobName = $"NewsAndInsights{frequency}Job";
				ScheduledIntervalJob(frequency, insightsJobName);
			} catch (Exception ex) {
				Console.Write(ex);
			}
		}

		#endregion

		#region Methods: Private

		private void ScheduledIntervalJob (string frequency, string insightsJobName) {
			if (AppScheduler.DoesJobExist(insightsJobName, ScheduledNewsAndInsightsJobGroup)) {
				return;
			}
			var jobParameters = new Dictionary<string, object> {
				{ "Frequency", frequency },
			};
			var cronExpression = GetCronExpression(frequency);
			var jobDetail = AppScheduler.CreateProcessJob(
				insightsJobName,
				ScheduledNewsAndInsightsJobGroup,
				ScheduledNewsAndInsightsJobProcessName,
				_userConnection.Workspace.Name,
				_userConnection.CurrentUser.Name,
				jobParameters);
			var cronTrigger = new CronTriggerImpl {
				Name = insightsJobName,
				Group = ScheduledNewsAndInsightsJobGroup,
				TimeZone = TimeZoneInfo.Local,
				CronExpression = new CronExpression(cronExpression),
				MisfireInstruction = MisfireInstruction.CronTrigger.DoNothing
			};
			InsightsScheduler.ScheduleJob(jobDetail, cronTrigger);
		}
		
		private string GetCronExpression(string frequency)
		{
			switch (frequency.ToLower())
			{
				case "weekly":
					return "0 0 9 ? * MON *";
				case "monthly":
					return "0 0 9 1 * ? *";
				default:
					throw new ArgumentException($"Unsupported frequency: {frequency}");
			}
		}
		
		#endregion
	}

	#endregion

}
