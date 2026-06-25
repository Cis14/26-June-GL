namespace IntegrationV2
{
	using System;
	using System.Collections.Generic;
	using Common.Logging;
	using IntegrationApi.Interfaces;
	using IntegrationApi.MailboxDomain.Model;
	using IntegrationV2.Files.cs.Domains.MeetingDomain;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Synchronization;
	using Terrasoft.Common;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Tasks;
	using Terrasoft.IntegrationV2.Utils;

	#region Class: SyncJobScheduler

	[DefaultBinding(typeof(ISyncJobScheduler))]
	public class SyncJobScheduler : ISyncJobScheduler
	{

		#region Fields: Private

		/// <summary>
		/// Synchronization jobs name.
		/// </summary>
		private const string SyncJobGroupName = "Exchange";

		private const string GoogleSyncJobGroupName = "GoogleIntegration";

		private const string GoogleSyncProcessName = "GActivitySynchronizationModuleProcess";

		/// <summary>
		/// Exchange integration logger.
		/// </summary>
		private ILog _log { get; } = LogManager.GetLogger("Exchange");

		/// <summary>
		/// <see cref="IAppSchedulerWraper"/> implementation.
		/// </summary>
		private readonly IAppSchedulerWraper _appSchedulerWraper = ClassFactory.Get<IAppSchedulerWraper>();

		/// <summary>
		/// Default synchronization job suffix.
		/// </summary>
		private readonly string _syncJobSuffix = "ImmediateProcessJob";

		/// <summary>
		/// Allowed syncronization job parameters for <see cref="IAppSchedulerWraper"/>.
		/// </summary>
		private readonly List<string> _syncJobParameters = new List<string>{
			"SenderEmailAddress",
			"LoadEmailsFromDate",
			"MailboxId"
		};

		#endregion

		#region Methods: Private

		/// <summary>
		/// Get simply synchronization job name.
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/> instance.</param>
		/// <param name="processName">Sync process name.</param>
		/// <returns>Synchronization job name.</returns>
		private string GetSyncJobName(UserConnection userConnection, string processName) {
			if (processName == GoogleSyncProcessName) {
				return $"SyncGoogleCalendar{userConnection.CurrentUser.Id}";
			}
			return $"{processName}_{userConnection.CurrentUser.Id}";
		}

		/// <summary>
		/// Get synchronization job name.
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/> instance.</param>
		/// <param name="processName">Process name.</param>
		/// <param name="senderEmailAddress">Sender email address.</param>
		/// <param name="suffix">Process name suffix.</param>
		/// <param name="sysAdminUnitId">Mailbox owner identifier.</param>
		/// <returns>Synchronization job name.</returns>
		private string GetSyncJobName(UserConnection userConnection, string processName,
				string senderEmailAddress, string suffix = null, string sysAdminUnitId = null) {
			if (sysAdminUnitId.IsNullOrEmpty()) {
				sysAdminUnitId = userConnection.CurrentUser.Id.ToString();
			}
			if (processName == GoogleSyncProcessName) {
				return $"SyncGoogleCalendar{sysAdminUnitId}";
			}
			string result = $"{senderEmailAddress}_{processName}_{sysAdminUnitId}";
			return string.IsNullOrEmpty(suffix) ? result : $"{result}_{suffix}";
		}

		/// <summary>
		/// Returns scheduler name for <paramref name="parameters"/>.
		/// </summary>
		/// <param name="parameters">Scheduler task parameters collection.</param>
		/// <returns>Scheduler name.</returns>
		private string GetSchedulerName(IDictionary<string, object> parameters = null) {
			var processName = GetSyncProcessName(parameters);
			return GetSchedulerName(processName);
		}

		/// <summary>
		/// Returns scheduler name for <paramref name="processName"/>.
		/// </summary>
		/// <param name="processName">Process name that will be scheduled.</param>
		/// <returns>Scheduler name.</returns>
		private string GetSchedulerName(string processName) {
			if (processName == null || processName != ExchangeConsts.ActivitySyncProcessName) {
				return null;
			}
			return "CalendarSyncScheduler";
		}

		/// <summary>
		/// Get synchronization job name.
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/> instance.</param>
		/// <param name="parameters">Synchronization process parameters.</param>
		/// <returns>Synchronization job name.</returns>
		private string GetSyncJobName(UserConnection userConnection, IDictionary<string, object> parameters = null) {
			var processName = GetSyncProcessName(parameters);
			var sysAdminUnitId = GetParametersValue(parameters, "SysAdminUnitId", userConnection.CurrentUser.Id).ToString();
			var senderEmailAddress = GetParametersValue<string>(parameters, "SenderEmailAddress");
			var periodInMinutes = GetParametersValue(parameters, "PeriodInMinutes", 0);
			return GetSyncJobName(userConnection, processName, senderEmailAddress, GetSyncJobSuffix(periodInMinutes), sysAdminUnitId);
		}

		/// <summary>
		/// Get synchronization process name.
		/// </summary>
		/// <param name="parameters">Synchronization process parameters.</param>
		/// <param name="defValue">Default value.</param>
		/// <returns>Synchronization process name.</returns>
		private string GetSyncProcessName(IDictionary<string, object> parameters, string defValue = null) {
			if (parameters.ContainsKey("ProccessName")) {
				return parameters["ProccessName"].ToString();
			}
			if (!string.IsNullOrEmpty(defValue)) {
				return defValue;
			}
			return GetEmailProcessName(parameters);
		}

		/// <summary>
		/// Get synchronization process name.
		/// </summary>
		/// <param name="parameters">Synchronization process parameters.</param>
		/// <returns>Synchronization process name.</returns>
		private string GetEmailProcessName(IDictionary<string, object> parameters) {
			var mailboxType = GetParametersValue<Guid>(parameters, "MailboxType");
			if(mailboxType == Guid.Empty) {
				throw new ArgumentNullException("MailboxType");
			}
			return mailboxType == MailServer.ImapTypeId ? "SyncImapMail" : ExchangeConsts.MailSyncProcessName;
		}

		/// <summary>
		/// Get value of <paramref name="parameters"/> by <paramref name="key"/>, or <paramref name="defValue"/>.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parameters"></param>
		/// <param name="key"></param>
		/// <param name="defValue">Default parameter value.</param>
		/// <returns>Value of <paramref name="parameters"/></returns>
		private T GetParametersValue<T>(IDictionary<string, object> parameters, string key, T defValue = default) {
			if (!parameters.ContainsKey(key) && defValue == null) {
				throw new ArgumentNullException(key);
			}
			return parameters.ContainsKey(key) ? (T)parameters[key] : defValue;
		}

		/// <summary>
		/// Removes <paramref name="syncJobName"/> job from scheduled jobs.
		/// </summary>
		/// <param name="syncJobName">Synchronization job name.</param>
		/// <param name="userConnection"><see cref="UserConnection"/> instance.</param>
		private void RemoveProcessJob(string syncJobName,string processName, UserConnection userConnection) {
			var stackTrace = new System.Diagnostics.StackTrace(false);
			_log.ErrorFormat("RemoveJob called: CurrentUser {0}, SyncJobName {1}. Trace: {2}",
				userConnection.CurrentUser.Name, syncJobName, stackTrace.ToString());
			var schedulerName = GetSchedulerName(processName);
			_appSchedulerWraper.RemoveJob(syncJobName, GetSyncJobGroupName(processName), schedulerName);
		}

		/// <summary>
		/// Creates exchange synchronization process job.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="syncJobName">Synchronization job name.</param>
		/// <param name="processName">Synchronization process name.</param>
		/// <param name="period">Synchronization job run perion in minutes.</param>
		/// <param name="parameters">Synchronization process parameters.</param>
		private void CreateProcessJob(UserConnection uc, string syncJobName, string processName,
				int period, IDictionary<string, object> parameters) {
			RemoveProcessJob(syncJobName, processName, uc);
			var syncJobParameters = GetSyncJobParameters(parameters);
			var useNewMeetingIntegration = ListenerUtils.GetIsFeatureEnabled("NewMeetingIntegration");
			var currentUser = uc.CurrentUser;
			if (period == 0) {
				syncJobParameters.Add("CreateReminding", true);
				if (useNewMeetingIntegration && processName == ExchangeConsts.ActivitySyncProcessName) {
					StartCalendarSynchronization(SyncAction.ImportPeriod, currentUser.ContactId, currentUser.Name);
				} else {
					_log.Error($"ScheduleImmediateProcessJob called:  CurrentUser {currentUser.Name}, SyncJobName {syncJobName}");
					_appSchedulerWraper.ScheduleImmediateProcessJob(syncJobName, GetSyncJobGroupName(processName),
						processName, uc.Workspace.Name, currentUser.Name, syncJobParameters);
				}
			} else {
				var schedulerName = GetSchedulerName(processName);
				_log.Error($"ScheduleMinutelyJob called: CurrentUser {currentUser.Name}, SyncJobName {syncJobName}, schedulerName {schedulerName}");
				_appSchedulerWraper.ScheduleMinutelyJob(syncJobName, GetSyncJobGroupName(processName), processName,
					uc.Workspace.Name, currentUser.Name, period, schedulerName, syncJobParameters);
			}
		}

		/// <summary>
		/// Get parameters for <see cref="IAppSchedulerWraper"/> scheduler.
		/// </summary>
		/// <param name="parameters">Synchronization process parameters.</param>
		/// <returns>Parameters for <see cref="IAppSchedulerWraper"/> scheduler.</returns>
		private IDictionary<string, object> GetSyncJobParameters(IDictionary<string, object> parameters) {
			var result = new Dictionary<string, object>();
			foreach (var parametr in parameters) {
				if (_syncJobParameters.Contains(parametr.Key)) {
					result.Add(parametr.Key, parametr.Value);
				}
			}
			return result;
		}

		/// <summary>
		/// Gets synchronization job suffix.
		/// </summary>
		/// <param name="periodInMinutes">Synchronization period in minutes.</param>
		/// <returns>Synchronization job suffix.</returns>
		private string GetSyncJobSuffix(int periodInMinutes) {
			return periodInMinutes == 0 ? _syncJobSuffix : string.Empty;
		}

		/// <summary>
		/// Gets synchronization job waiting priod in minutes.
		/// </summary>
		/// <param name="parameters">Synchronization process parameters.</param>
		/// <param name="periodInMinutes">Default period.</param>
		/// <returns>Synchronization job waiting priod.</returns>
		private int GetSyncPeriod(IDictionary<string, object> parameters, int periodInMinutes = 0) {
			var syncPeriodInMinutes = GetParametersValue(parameters, "PeriodInMinutes", periodInMinutes);
			if (syncPeriodInMinutes < 0) {
				throw new ArgumentOutOfRangeException("PeriodInMinutes");
			}
			return syncPeriodInMinutes;
		}

		/// <summary>
		/// Start syncing a user's calendar.
		/// </summary>
		/// <param name="action">Synchronization direction.</param>
		/// <param name="contactId">User contact id.</param>
		/// <param name="contactName">User contact name.</param>
		private void StartCalendarSynchronization(SyncAction action, Guid contactId, string contactName) {
			var logger = ClassFactory.Get<ICalendarLogger>(new ConstructorArgument("sessionId", string.Empty),
				new ConstructorArgument("modelName", GetType().Name),
				new ConstructorArgument("syncAction", action));
			logger.LogInfo($"Start calendar synchronization for user: {contactName}");
			try {
				Task.StartNewWithUserConnection<CalendarSyncSession, SyncSessionArguments>(
					new SyncSessionArguments {
						ContactIds = new List<Guid> { contactId },
						SyncAction = action,
						SessionId = logger.SessionId
					});
			} catch (Exception e) {
				logger.LogError($"Calendar synchronization for user: {contactName} failed.", e);
			}
		}

		/// <summary>
		/// Gets sync job group name.
		/// </summary>
		/// <param name="parameters">Sync job parameters.</param>
		/// <returns>Sync job group name./returns>
		private string GetSyncJobGroupName(IDictionary<string, object> parameters) {
			var processName = GetSyncProcessName(parameters);
			return GetSyncJobGroupName(processName);
		}

		/// <summary>
		///  Gets sync job group name.
		/// </summary>
		/// <param name="processName">Sync job process name.</param>
		/// <returns>Sync job group name./returns>
		private string GetSyncJobGroupName(string processName) {
			return processName == GoogleSyncProcessName ? GoogleSyncJobGroupName : SyncJobGroupName;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="ISyncJobScheduler.CreateSyncJob(UserConnection, int, string, IDictionary{string, object})"/>
		public void CreateSyncJob(UserConnection userConnection, int periodInMinutes, string processName,
				IDictionary<string, object> parameters = null) {
			int syncPeriodInMinutes = GetSyncPeriod(parameters, periodInMinutes);
			var suffix = GetSyncJobSuffix(syncPeriodInMinutes);
			var senderEmailAddress = GetParametersValue(parameters, "SenderEmailAddress", string.Empty);
			var sysAdminUnitId = GetParametersValue(parameters, "SysAdminUnitId", userConnection.CurrentUser.Id).ToString();
			var syncProcessName = GetSyncProcessName(parameters, processName);
			var syncJobName = string.IsNullOrEmpty(senderEmailAddress)
				? GetSyncJobName(userConnection, syncProcessName)
				: GetSyncJobName(userConnection, syncProcessName, senderEmailAddress, suffix, sysAdminUnitId);
			CreateProcessJob(userConnection, syncJobName, processName, periodInMinutes, parameters);
		}

		/// <inheritdoc cref="ISyncJobScheduler.CreateSyncJob(UserConnection, IDictionary{string, object})"/>
		public void CreateSyncJob(UserConnection userConnection, IDictionary<string, object> parameters) {
			var emailProcessName = GetEmailProcessName(parameters);
			int syncPeriodInMinutes = GetSyncPeriod(parameters);
			CreateSyncJob(userConnection, syncPeriodInMinutes, emailProcessName, parameters);
		}

		/// <inheritdoc cref="ISyncJobScheduler.RemoveSyncJob(UserConnection, string, string)"/>
		public void RemoveSyncJob(UserConnection userConnection, string senderEmailAddress, string processName) {
			string syncJobName = GetSyncJobName(userConnection, processName, senderEmailAddress);
			RemoveProcessJob(syncJobName, processName, userConnection);
		}

		/// <inheritdoc cref="ISyncJobScheduler.DoesSyncJobExist(UserConnection, IDictionary{string, object})"/>
		public bool DoesSyncJobExist(UserConnection userConnection, IDictionary<string, object> parameters = null) {
			var syncJobName = GetSyncJobName(userConnection, parameters);
			var schedulerName = GetSchedulerName(parameters);
			return _appSchedulerWraper.DoesJobExist(syncJobName, GetSyncJobGroupName(parameters), schedulerName);
		}

		#endregion

	}

	#endregion

}
