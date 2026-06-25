namespace IntegrationV2.Files.cs.Failover
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Factories;
	using Terrasoft.IntegrationV2.Utils;

	#region Class: BaseFailoverJob<T>

	public abstract class BaseFailoverJob<T>: IJobExecutor
	{

		#region Constants: Private

		private const int _minFailoverJobSynchronizationPeriod = 0;

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Job group name.
		/// </summary>
		protected abstract string JobGroupName { get; }

		/// <summary>
		/// Job period (in minutes).
		/// </summary>
		protected abstract int Period { get; }


		private readonly IFeatureUtilities _featureUtilities = ClassFactory.Get<IFeatureUtilities>();
		protected IFeatureUtilities FeatureUtilities {
			get {
				return _featureUtilities;
			}
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Gets feature name for toggle failover execution.
		/// </summary>
		/// <returns>Feature name for toggle failover execution.</returns>
		protected abstract string GetFeatureName();

		/// <summary>
		/// Indicates if job has license
		/// </summary>
		/// <returns>Feature name for toggle failover execution.</returns>
		protected abstract bool HasLicense(UserConnection userConnection);

		/// <summary>
		/// Initialize failover dependencies .
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		protected abstract void InitDependencies(UserConnection userConnection);

		/// <summary>
		/// Gets calendar accounts without synchronization.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <returns></returns>
		protected abstract List<T> GetAccountsWithoutSync(UserConnection uc);

		/// <summary>
		/// Enable account for synchronization.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="accounts">Accounts collection for enabling sunchronization.</param>
		protected abstract void EnableAccountsSync(UserConnection uc, List<T> accounts);

		/// <summary>
		/// Log <paramref name="message"/> with info level.
		/// </summary>
		/// <param name="message">Log message.</param>
		protected abstract void LogInfo(string message);

		/// <summary>
		/// Log <paramref name="message"/> with debug level.
		/// </summary>
		/// <param name="message">Log message.</param>
		protected abstract void LogDebug(string message);

		/// <summary>
		/// Log <paramref name="message"/> with warning level.
		/// </summary>
		/// <param name="message">Log message.</param>
		/// <param name="e"><see cref="Exception"/> instance.</param>
		protected abstract void LogWarn(string message, Exception e = null);

		/// <summary>
		/// Log <paramref name="message"/> with error level.
		/// </summary>
		/// <param name="message">Log message.</param>
		/// <param name="e"><see cref="Exception"/> instance.</param>
		protected abstract void LogError(string message, Exception e);

		/// <summary>
		/// Checking whether to skip failover execution.
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/> instance.</param>
		/// <returns>True, if need to skip failover execution, otherwise false.</returns>
		protected virtual bool SkipFailoverExecution(UserConnection userConnection) {
			return !HasLicense(userConnection) || !GetIsFeatureEnabledForAnyUser(userConnection, GetFeatureName());
		}

		/// <summary>
		/// Checks is feature enabled for any user.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="code">Feature code.</param>
		/// <returns><c>True</c> if feature enabled for any user. Returns <c>false</c> otherwise.</returns>
		protected bool GetIsFeatureEnabledForAnyUser(UserConnection uc,string code) {
			return FeatureUtilities.GetIsFeatureEnabledForAnyUser(uc, code);
		}

		/// <summary>
		/// Processes accounts warnings.
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/> instance.</param>
		protected abstract void ProcessAccountsWarning(UserConnection userConnection);

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IJobExecutor.Execute(UserConnection, IDictionary{string, object})"/>
		public void Execute(UserConnection userConnection, IDictionary<string, object> parameters) {
			var failoverName = GetType().Name;
			try {
				LogInfo($"[{failoverName}] started.");
				if (SkipFailoverExecution(userConnection)) {
					LogInfo($"[{failoverName}] Execution is skipped.");
					return;
				}
				InitDependencies(userConnection);
				LogDebug($"[{failoverName}] dependencies initiated.");
				var accounts = GetAccountsWithoutSync(userConnection);
				LogInfo($"[{failoverName}] {accounts.Count} accounts received for enabling synchronization.");
				EnableAccountsSync(userConnection, accounts);
				LogInfo($"[{failoverName}] Synchronization of {accounts.Count} accounts is enabled.");
				ProcessAccountsWarning(userConnection);
			} catch (Exception e) {
				LogError($"[{failoverName}] error {e}", e);
			} finally {
				int periodMin = SysSettings.GetValue(userConnection, $"{failoverName}Period", Period);
				if (periodMin == _minFailoverJobSynchronizationPeriod) {
					var schedulerWraper = ClassFactory.Get<IAppSchedulerWraper>();
					schedulerWraper.RemoveGroupJobs(JobGroupName);
					LogWarn($"[{failoverName}] {failoverName}Period is 0, {failoverName} stopped.");
				}
				LogInfo($"[{failoverName}] ended.");
			}
		}

		#endregion

	}

	#endregion

}
