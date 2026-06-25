namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using Quartz;
	using Quartz.Impl.Triggers;
	using Terrasoft.Common;
	using Terrasoft.Core.Scheduler;

	#region Class: JobConfig

	public class JobConfig
	{

		#region Constants: Private

		private const string JobGroupSuffix = "Group";

		#endregion

		#region Contructors: Public

		public JobConfig() {
		}

		public JobConfig(string processName, string workspaceName, string userName, IDictionary<string, object> parameters) {
			processName.CheckArgumentNullOrEmpty("processName");
			workspaceName.CheckArgumentNullOrEmpty("workspaceName");
			userName.CheckArgumentNullOrEmpty("userName");
			ProcessName = processName;
			SetJobGroup();
			WorkspaceName = workspaceName;
			UserName = userName;
			Parameters = parameters;
		}

		#endregion

		#region Properties: Public

		/// <summary>Process name.</summary>
		public string ProcessName { get; set; }
		/// <summary>The name of the group to which the Job belongs.</summary>
		public string JobGroup { get; set; }
		/// <summary>Job name.</summary>
		public string JobName { get; set; } = Guid.NewGuid().ToString();
		/// <summary>Workspace in which process must be started.</summary>
		public string WorkspaceName { get; set; }
		/// <summary>The user on whose behalf process will be started.</summary>
		public string UserName { get; set; }
		/// <summary>If true, the Job executed from name current user.</summary>
		public bool IsSystemUser { get; set; }
		/// <summary>The parameters that will be passed to the process.</summary>
		public IDictionary<string, object> Parameters { get; set; }

		/// <summary>
		/// <see cref="JobOptions"/> class with job creation parameters.
		/// </summary>
		private JobOptions _jobOptions;
		public JobOptions JobOptions {
			get => _jobOptions ?? (_jobOptions = JobOptions.Default);
			set => _jobOptions = value;
		}

		#endregion

		#region Methods: Private

		private void SetJobGroup() {
			JobGroup = string.Concat(ProcessName, JobGroupSuffix);
		}

		#endregion

	}

	#endregion

	#region Class: QuartzJobTriggerManager

	public class QuartzJobTriggerManager
	{
		
		#region Fields: Private

		private static readonly object lockObject = new object();
		private static QuartzJobTriggerManager _instance;

		#endregion

		#region Properties: Public

		/// <summary>Creating instance in singleton pattern and thread-safe mode.</summary>
		public static QuartzJobTriggerManager Instance {
			get {
				if (_instance == null) {
					lock (lockObject) {
						if (_instance == null) {
							_instance = new QuartzJobTriggerManager();
							return _instance;
						}
					}
				}
				return _instance;
			}
		}

		#endregion

		#region Methods: Protected
		
		protected IJobDetail GetProcesJob(JobConfig jobConfig) {
			return AppScheduler.CreateProcessJob(jobConfig.JobName, jobConfig.JobGroup, jobConfig.ProcessName,
				jobConfig.WorkspaceName, jobConfig.UserName, jobConfig.Parameters, jobConfig.IsSystemUser, jobConfig.JobOptions);
		}

		protected ITrigger GetTrigger(string triggerName, string groupName, int misfireInstruction) {
			return new SimpleTriggerImpl(triggerName, groupName) { MisfireInstruction = misfireInstruction };
		}

		#endregion

		#region Methods: Public

		/// <summary>Runs a task for immediate one-time start of process.</summary>
		/// <param name="jobConfig">Properties for configure the Job.</param>
		public virtual void RunTriggerJob(JobConfig jobConfig) {
			AppScheduler.TriggerJob(jobConfig.JobName, jobConfig.JobGroup, jobConfig.ProcessName,
				jobConfig.WorkspaceName, jobConfig.UserName, jobConfig.Parameters);
		}

		/// <summary>Runs the ScheduleJob for process execution.</summary>
		/// <param name="jobConfig">Properties for configure the Job.</param>
		public virtual void RunScheduleJob(JobConfig jobConfig, int misfireInstruction) {
			var job = Instance.GetProcesJob(jobConfig);
			var triggerName = Guid.NewGuid().ToString();
			var triggerGroup = jobConfig.JobGroup;
			var trigger = Instance.GetTrigger(triggerName, triggerGroup, misfireInstruction);
			AppScheduler.Instance.ScheduleJob(job, trigger);
		}

		#endregion

	}

	#endregion

}

