
namespace Terrasoft.Configuration
{
	using System.Collections.Generic;
	using Core.Scheduler;
	using Terrasoft.Core;

	#region Class: UserConnectionUtilities

	/// <summary>
	/// Contains user connection utility methods.
	/// </summary>
	public static class UserConnectionUtilities
	{

		#region Methods: Public

		/// <summary>
		/// Runs process in current workspace using current user credentials.
		/// </summary>
		/// <param name="source">User connection.</param>
		/// <param name="processName">Process name.</param>
		/// <param name="parameters">Process parameters.</param>
		/// <remarks>Quartz job group is the process name concatenated with static suffix.
		/// Job name is auto-generated.</remarks>
		public static void RunProcess(this UserConnection source, string processName,
				IDictionary<string, object> parameters) {
			var jobConfig = GetJobConfig(processName, source.Workspace.Name,
				source.CurrentUser.Name, parameters);
			RunTriggerJob(jobConfig);
		}

		/// <summary>
		/// Runs process in current workspace using current user credentials.
		/// </summary>
		/// <param name="source">User connection.</param>
		/// <param name="processName">Process name.</param>
		/// <remarks>Quartz job group is the process name concatenated with static suffix.
		/// Job name is auto-generated.</remarks>
		public static void RunProcess(this UserConnection source, string processName) {
			RunProcess(source, processName, null);
		}

		/// <summary>
		/// Runs process in current workspace using current user credentials and providing <paramref name="options"/> for running job.
		/// </summary>
		/// <param name="source">User connection.</param>
		/// <param name="processName">Process name.</param>
		/// <param name="misfireInstruction">Trigger policy.</param>
		/// <param name="parameters">Process parameters.</param>
		/// <param name="options">Instance of the <see cref="JobOptions"/> parameters.</param>
		/// <remarks>Quartz job group is the process name concatenated with static suffix.
		/// Job name is auto-generated.</remarks>
		public static void RunProcess(this UserConnection source, string processName, int misfireInstruction,
				IDictionary<string, object> parameters, JobOptions options) {
			var jobConfig = GetJobConfig(processName, source.Workspace.Name,
					source.CurrentUser.Name, parameters);
			jobConfig.JobOptions = options;
			RunScheduleJob(jobConfig, misfireInstruction);
		}

		/// <summary>
		/// Runs process in current workspace using current user credentials.
		/// </summary>
		/// <param name="source">User connection.</param>
		/// <param name="processName">Process name.</param>
		/// <param name="misfireInstruction">Trigger policy.</param>
		/// <param name="parameters">Process parameters.</param>
		/// <remarks>Quartz job group is the process name concatenated with static suffix.
		/// Job name is auto-generated.</remarks>
		public static void RunProcess(this UserConnection source, string processName, int misfireInstruction,
				IDictionary<string, object> parameters) {
			var jobConfig = GetJobConfig(processName, source.Workspace.Name,
				source.CurrentUser.Name, parameters);
			RunScheduleJob(jobConfig, misfireInstruction);
		}

		/// <summary>
		/// Runs process in current workspace using current user credentials.
		/// </summary>
		/// <param name="source">User connection.</param>
		/// <param name="processName">Process name.</param>
		/// <param name="misfireInstruction">Trigger policy.</param>
		/// <remarks>Quartz job group is the process name concatenated with static suffix.
		/// Job name is auto-generated.</remarks>
		public static void RunProcess(this UserConnection source, string processName, int misfireInstruction) {
			var jobConfig = GetJobConfig(processName, source.Workspace.Name,
				source.CurrentUser.Name, null);
			RunScheduleJob(jobConfig, misfireInstruction);
		}

		#endregion

		#region Methods: Private

		private static JobConfig GetJobConfig(string processName, string workspaceName, string userName,
				IDictionary<string, object> parameters) {
			return new JobConfig(processName, workspaceName, userName, parameters);
		}

		private static void RunScheduleJob(JobConfig jobConfig, int misfireInstruction) {
			QuartzJobTriggerManager.Instance.RunScheduleJob(jobConfig, misfireInstruction);
		}

		private static void RunTriggerJob(JobConfig jobConfig) {
			QuartzJobTriggerManager.Instance.RunTriggerJob(jobConfig);
		}

		#endregion

	}


	#endregion

}
