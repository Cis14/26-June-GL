namespace Terrasoft.Configuration
{
	using global::IntegrationV2.Files.cs.Domains.MeetingDomain.Failover;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Factories;

	#region Class: ListenerServiceFailJobFactory

	/// <summary>
	/// Provides methods for creating the exchange events listener service failure processing job.
	/// </summary>
	[DefaultBinding(typeof(IListenerServiceFailJobFactory))]
	public class ListenerServiceFailJobFactory: IListenerServiceFailJobFactory
	{

		#region Constants: Private

		/// <summary>
		/// Email job period (in minutes).
		/// </summary>
		private const int _emailJobPeriod = 1;

		/// <summary>
		/// Calendar job period (in minutes).
		/// </summary>
		private const int _calendarJobPeriod = 15;

		#endregion

		#region Constants: Public

		/// <summary>
		/// Scheduler email job group name.
		/// </summary>
		public const string EmailJobGroupName = "ExchangeListener";

		/// <summary>
		/// Scheduler calendar job group name.
		/// </summary>
		public const string CalendarJobGroupName = "Calendar";

		#endregion

		#region Methods: Private

		/// <summary>
		/// Creates failure processing job.
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/>.</param>
		/// <param name="jobGroupName">Job group name.</param>
		/// <param name="defPeriod">Default period of job executions, in minutes.</param>
		private void ScheduleFailJob<T>(UserConnection userConnection, string jobGroupName, int defPeriod) where T: IJobExecutor {
			var schedulerWraper = ClassFactory.Get<IAppSchedulerWraper>();
			var failoverType = typeof(T);
			if (schedulerWraper.DoesJobExist(failoverType.FullName, jobGroupName)) {
				var triggers = schedulerWraper.GetActiveTriggerKeysForJobGroup(jobGroupName);
				if (triggers != null && triggers.Contains(failoverType.FullName)) {
					return;
				}
			}
			schedulerWraper.RemoveGroupJobs(jobGroupName);
			int periodMin = SysSettings.GetValue(userConnection, failoverType.Name + "Period", defPeriod);
			if (periodMin == 0) {
				return;
			}
			schedulerWraper.ScheduleMinutelyJob<T>(jobGroupName, userConnection.Workspace.Name,
				userConnection.CurrentUser.Name, periodMin, null, true);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IListenerServiceFailJobFactory.ScheduleListenerServiceFailJob"/>
		public void ScheduleListenerServiceFailJob(UserConnection userConnection) {
			ScheduleFailJob<ListenerServiceFailJob>(userConnection, EmailJobGroupName, _emailJobPeriod);
		}

		/// <inheritdoc cref="IListenerServiceFailJobFactory.ScheduleCalendarFailJob"/>
		public void ScheduleCalendarFailJob(UserConnection userConnection) {
			ScheduleFailJob<CalendarFailoverJob>(userConnection, CalendarJobGroupName, _calendarJobPeriod);
		}

		#endregion

	}

	#endregion

}