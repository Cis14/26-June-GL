namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Failover
{
	using System;
	using System.Collections.Generic;
	using IntegrationApi.Calendar;
	using IntegrationApi.Interfaces;
	using IntegrationApi.MailboxDomain.Model;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
	using IntegrationV2.Files.cs.Failover;
	using IntegrationV2.Files.cs.Warnings;
	using IntegrationV2.Files.cs.Warnings.Exceptions;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Factories;

	#region Class: CalendarFailoverJob

	/// <summary>
	/// Checks the calendar job every <see cref="BaseFailoverJob{T}.Period"/> minutes and creates new ones if they are missing.
	/// </summary>
	public class CalendarFailoverJob: BaseFailoverJob<Calendar>
	{

		#region Constants: Private

		/// <summary>
		/// Calendar failure handlers job group name.
		/// </summary>
		private const string _handlerJobGroupName = "CalendarHandler";

		#endregion

		#region Fields: Private

		/// <summary>
		/// <see cref="ICalendarRepository"/> instance.
		/// </summary>
		private ICalendarRepository _calendarRepository;

		/// <summary>
		/// <see cref="ICalendarLogger"/> instance.
		/// </summary
		private readonly ICalendarLogger _log = ClassFactory.Get<ICalendarLogger>();

		/// <summary>
		/// Job period (in minutes).
		/// </summary>
		private int _period;

		#endregion

		#region Constructors: Public

		public CalendarFailoverJob() {
			_log.SetAction(SyncAction.Job);
		}

		#endregion

		#region Methods: Private

		private string GetProcessName(Calendar calendar) {
			return calendar.Type == CalendarType.Exchange || calendar.Type == CalendarType.Graph
				? ExchangeConsts.ActivitySyncProcessName
				: "GActivitySynchronizationModuleProcess";
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseFailoverJob{T}.JobGroupName"/>
		protected override string JobGroupName => "Calendar";

		/// <inheritdoc cref="BaseFailoverJob{T}.Period"/>
		protected override int Period => _period;

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="BaseFailoverJob{T}.GetFeatureName()"/>
		protected override string GetFeatureName() {
			return "NewMeetingIntegration";
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.HasLicense(UserConnection)()"/>
		protected override bool HasLicense(UserConnection userConnection) {
			return userConnection.LicHelper.GetHasExplicitlyLicensedOperationLicense(LicenseConsts.CalendarSynchronization);
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.InitDependencies(UserConnection)"/>
		protected override void InitDependencies(UserConnection uc) {
			_period = SysSettings.GetValue(uc, "MailboxSyncInterval", 1);
			_calendarRepository = ClassFactory.Get<ICalendarRepository>("CalendarRepository",
				new ConstructorArgument("uc", uc),
				new ConstructorArgument("sessionId", _log?.SessionId));
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.GetAccountsWithoutSync(UserConnection)"/>
		protected override List<Calendar> GetAccountsWithoutSync(UserConnection uc) {
			var calendarsWithoutSync = new List<Calendar>();
			var allEnabledCalendars = _calendarRepository.GetEnabledCalendars();
			var syncJobScheduler = ClassFactory.Get<ISyncJobScheduler>();
			foreach (var calendar in allEnabledCalendars) {
				var parameters = new Dictionary<string, object> {
					{ "SenderEmailAddress", calendar.Settings.SenderEmailAddress },
					{ "SysAdminUnitId", calendar.Settings.UserId },
					{ "PeriodInMinutes", Period },
					{ "ProccessName", GetProcessName(calendar) }
				};
				if (!syncJobScheduler.DoesSyncJobExist(uc, parameters)) {
					calendarsWithoutSync.Add(calendar);
				}
			}
			return calendarsWithoutSync;
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.EnableAccountsSync(UserConnection, List{T}))"/>
		protected override void EnableAccountsSync(UserConnection uc, List<Calendar> calendars) {
			var schedulerWraper = ClassFactory.Get<IAppSchedulerWraper>();
			foreach (var calendar in calendars) {
				var parameters = new Dictionary<string, object> {
					{ "PeriodInMinutes", Period },
					{ "SenderEmailAddress", calendar.Settings.SenderEmailAddress },
					{ "SysAdminUnitId", calendar.Settings.UserId },
					{ "ProccessName", GetProcessName(calendar) },
					{ "MailboxType", calendar.Type == CalendarType.Exchange || calendar.Type == CalendarType.Graph
						? MailServer.ExchangeTypeId
						: MailServer.ImapTypeId }
				};
				schedulerWraper.ScheduleImmediateJob<CalendarFailoverHandler>(_handlerJobGroupName, uc.Workspace.Name,
					calendar.Settings.UserName, parameters);
			}
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.LogInfo(string)"/>
		protected override void LogInfo(string message) {
			_log.LogInfo(message);
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.LogInfo(string)"/>
		protected override void LogDebug(string message) {
			_log.LogDebug(message);
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.LogWarn(string, Exception)"/>
		protected override void LogWarn(string message, Exception e = null) {
			_log.LogWarn(message, e);
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.LogError(string, Exception)"/>
		protected override void LogError(string message, Exception e) {
			_log.LogError(message, e);
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.ProcessAccountsWarning(UserConnection)"/>
		protected override void ProcessAccountsWarning(UserConnection userConnection) {
			if (GetIsFeatureEnabledForAnyUser(userConnection, "DisableMailboxSyncWarnings")) {
				LogDebug("Skip processing account warnings.");
				return;
			}
			var helper = ClassFactory.Get<ISynchronizationErrorHelper>(new ConstructorArgument("userConnection", userConnection));
			var enabledAccounts = _calendarRepository.GetEnabledCalendars();
			foreach (var syncAccount in enabledAccounts) {
				if (syncAccount.Type != CalendarType.Exchange && syncAccount.Type != CalendarType.Graph) {
					continue;
				}
				if (syncAccount.IsOffice365Account() && !syncAccount.Settings.UseOAuth && syncAccount.Settings.WarningCodeId == Guid.Empty) {
					var warningException = new BasicAuthWarningException();
					helper.ProcessSynchronizationError(syncAccount.Settings.SenderEmailAddress, warningException);
				} else if (syncAccount.Settings.WarningCodeId != Guid.Empty && 
					(!syncAccount.IsOffice365Account() || syncAccount.Settings.UseOAuth)) {
					helper.CleanUpSynchronizationWarning(syncAccount.Settings.SenderEmailAddress);
				}
			}
		}

		#endregion

	}

	#endregion

}
