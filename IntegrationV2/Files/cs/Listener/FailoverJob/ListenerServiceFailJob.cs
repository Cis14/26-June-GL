namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::IntegrationV2.Files.cs.Failover;
	using global::IntegrationV2.Files.cs.Warnings;
	using global::IntegrationV2.Files.cs.Warnings.Exceptions;
	using IntegrationApi.Interfaces;
	using IntegrationApi.MailboxDomain.Interfaces;
	using IntegrationApi.MailboxDomain.Model;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;
	using Terrasoft.EmailDomain;
	using Terrasoft.IntegrationV2.Logging.Interfaces;
	using Terrasoft.IntegrationV2.Utils;

	#region Class: ListenerServiceFailJob

	/// <summary>
	/// Minutely cheks the subscription statuses on the microservice, and creates immediate job <see cref="ListenerServiceFailHandler"/> 
	/// for mailboxes that does not have subscription.
	/// </summary>
	public class ListenerServiceFailJob: BaseFailoverJob<Mailbox>
	{

		#region Constants: Private

		/// <summary>
		/// Mailbox failure handlers job group name.
		/// </summary>
		private const string _handlerJobGroupName = "ExchangeListenerHandler";

		#endregion

		#region Fields: Private

		/// <summary>
		/// <see cref="ISynchronizationLogger"/> instance.
		/// </summary>
		private readonly ISynchronizationLogger _log;

		/// <summary>
		/// Mailbox subscription exists on litener service state code.
		/// </summary>
		private readonly string _subscriptionExistsState = "exists";

		/// <summary>
		/// <see cref="IExchangeListenerManager"/> instance.
		/// </summary>
		private IExchangeListenerManager _listenerManager;

		/// <summary>
		/// <see cref="IMailboxService"/> instance.
		/// </summary>
		private IMailboxService _mailboxService;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctor.
		/// </summary>
		public ListenerServiceFailJob() {
			_log = ClassFactory.Get<ISynchronizationLogger>();
		}

		#endregion

		#region Properties: Protected

		/// <inheritdoc cref="BaseFailoverJob{T}.JobGroupName"/>
		protected override string JobGroupName => ListenerServiceFailJobFactory.EmailJobGroupName;

		/// <inheritdoc cref="BaseFailoverJob{T}.Period"/>
		protected override int Period => 1;

		#endregion

		#region Methods: Private

		/// <summary>
		/// Returns mailboxes with enabled email synchronization.
		/// </summary>
		/// <returns>Collection of mailboxes with enabled email synchronization.</returns>
		private List<Mailbox> GetSynchronizableMailboxes() {
			var result = _mailboxService.GetAllSynchronizableMailboxes();
			LogDebug("GetAllSynchronizableMailboxes ended");
			return result;
		}

		/// <summary>
		/// Checks that exchange listeners service avaliable.
		/// </summary>
		/// <returns><c>True</c> if exchange listeners service avaliable. Otherwise returns <c>false</c>.</returns>
		private bool GetIsListenerServiceAvaliable() {
			return _listenerManager.GetIsServiceAvaliable();
		}

		/// <summary>
		/// Removes mailboxes with active subscriptions from <paramref name="mailboxes"/>.
		/// </summary>
		/// <param name="mailboxes">Collection of exchange mailboxes with enabled email synchronization.</param>
		private List<Mailbox> FilterActiveMailboxes(List<Mailbox> mailboxes) {
			var subscriptions = _listenerManager.GetSubscriptionsStatuses(mailboxes.Select(kvp => kvp.Id).ToArray());
			LogDebug($"FilterActiveMailboxes ended. Recived {subscriptions.Count} subscriptions from listener service");
			var existingSubscriptions = subscriptions.Where(kvp => kvp.Value == _subscriptionExistsState).Select(kvp => kvp.Key);
			return mailboxes.Where(m => !existingSubscriptions.Contains(m.Id)).ToList();
		}

		/// <summary>
		/// Creates <see cref="ExchangeListenerManager"/> instance.
		/// </summary>
		/// <param name="uc">Collection of exchange mailboxes with enabled email synchronization.</param>
		/// <returns>Implementation of <see cref="IExchangeListenerManager"/> instance.</returns>
		private IExchangeListenerManager GetExchangeListenerManager(UserConnection uc) {
			var managerFactory = ClassFactory.Get<IListenerManagerFactory>();
			return managerFactory.GetExchangeListenerManager(uc);
		}

		/// <summary>
		/// Returns is fail handling steel needed for <paramref name="mailboxes"/>.
		/// </summary>
		/// <param name="mailboxes">Collection of exchange mailboxes with enabled email synchronization.</param>
		/// <returns><c>True</c> if fail handling steel needed for <paramref name="mailboxes"/>.
		/// Otherwise returns <c>false</c>.</returns>
		private bool NeedProceed(List<Mailbox> mailboxes) {
			return mailboxes.Count > 0;
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Checks is feature enabled for all users.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="code">Feature code.</param>
		/// <returns><c>True</c> if feature enabled for any user. Returns <c>false</c> otherwise.</returns>
		protected bool GetIsFeatureEnabledForAll(UserConnection uc, string code) {
			return FeatureUtilities.GetIsFeatureEnabledForAll(uc, code);
		}

		/// <summary>
		/// Checks is warning is manual dismiss warning.
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/> instance.</param>
		/// <param name="warningId">Warning unique identifier.</param>
		/// <returns><c>True</c> if warning is manual dismiss warning. Returns <c>false</c> otherwise.</returns>
		protected bool GetIsManualDismissWarning(UserConnection userConnection, Guid warningId) {
			var select = new Select(userConnection)
				.Top(1)
				.Column("Id")
				.From("SyncErrorMessage")
				.Where("Id").IsEqual(Column.Parameter(warningId))
				.And("Action").IsEqual(Column.Parameter("ManualDismiss")) as Select;
			return select.ExecuteScalar<Guid>().IsNotEmpty();
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.SkipFailoverExecution(UserConnection)"/>
		protected override bool SkipFailoverExecution(UserConnection uc) {
			return GetIsFeatureEnabledForAll(uc, GetFeatureName());
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.GetFeatureName()"/>
		protected override string GetFeatureName() {
			return "OldEmailIntegration";
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.HasLicense(UserConnection)()"/>
		protected override bool HasLicense(UserConnection userConnection) {
			return true;
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.InitDependencies(UserConnection)"/>
		protected override void InitDependencies(UserConnection uc) {
			_listenerManager = GetExchangeListenerManager(uc);
			_mailboxService = ClassFactory.Get<IMailboxService>(new ConstructorArgument("uc", uc));
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.GetAccountsWithoutSync(UserConnection)"/>
		protected override List<Mailbox> GetAccountsWithoutSync(UserConnection uc) {
			LogDebug("GetMailboxesWithoutSubscriptions method started");
			var mailboxes = GetSynchronizableMailboxes();
			LogInfo($"GetMailboxesWithoutSubscriptions: selected {mailboxes.Count} mailboxes");
			if (!NeedProceed(mailboxes)) {
				LogDebug("GetMailboxesWithoutSubscriptions method ended");
				return new List<Mailbox>();
			}
			if (GetIsListenerServiceAvaliable()) {
				LogDebug("GetMailboxesWithoutSubscriptions: listener service avaliable, mailboxes subscriptions check started");
				mailboxes = FilterActiveMailboxes(mailboxes);
				LogInfo($"GetMailboxesWithoutSubscriptions: filtered to {mailboxes.Count} mailboxes");
			} else if (!GetIsFeatureEnabledForAll(uc, "DoNotSkipFailover")) {
				LogWarn($"Listener unavailable, skips failover for {mailboxes.Count} mailboxes");
				return new List<Mailbox>();
			}
			if (!NeedProceed(mailboxes)) {
				LogDebug("GetMailboxesWithoutSubscriptions method ended");
				return new List<Mailbox>();
			}
			LogDebug("GetMailboxesWithoutSubscriptions method ended");
			return mailboxes;
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.EnableAccountsSync(UserConnection, List{T}))"/>
		protected override void EnableAccountsSync(UserConnection uc, List<Mailbox> mailboxes) {
			LogDebug("StartFailoverHandlers started");
			var schedulerWraper = ClassFactory.Get<IAppSchedulerWraper>();
			schedulerWraper.RemoveGroupJobs(_handlerJobGroupName);
			LogDebug($"All jobs from {_handlerJobGroupName} job group deleted.");
			foreach (var mailbox in mailboxes) {
				var parameters = new Dictionary<string, object> {
					{ "SenderEmailAddress", mailbox.SenderEmailAddress },
					{ "MailboxType", mailbox.TypeId },
					{ "MailboxId", mailbox.Id },
					{ "PeriodInMinutes", 0 }
				};
				var syncJobScheduler = ClassFactory.Get<ISyncJobScheduler>();
				if (!syncJobScheduler.DoesSyncJobExist(uc, parameters)) {
					schedulerWraper.ScheduleImmediateJob<ListenerServiceFailHandler>(_handlerJobGroupName, uc.Workspace.Name,
						mailbox.OwnerUserName, parameters);
					LogDebug($"ListenerServiceFailHandler for {mailbox.Id} mailbox started using {mailbox.OwnerUserName} user.");
				} else {
					LogDebug($"ListenerServiceFailHandler for {mailbox.Id} mailbox skipped using {mailbox.OwnerUserName} user.");
				}
			}
			LogDebug("StartFailoverHandlers ended");
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.ProcessAccountsWarning(UserConnection)"/>
		protected override void ProcessAccountsWarning(UserConnection userConnection) {
			if (GetIsFeatureEnabledForAnyUser(userConnection, "DisableMailboxSyncWarnings")) {
				LogDebug("Skip processing account warnings.");
				return;
			}
			var helper = ClassFactory.Get<ISynchronizationErrorHelper>(new ConstructorArgument("userConnection", userConnection));
			var enabledAccounts = GetSynchronizableMailboxes();
			foreach (var syncAccount in enabledAccounts) {
			if (!syncAccount.CanUseExchangeActivitySync()) {
				continue;
			}
				if (syncAccount.IsOffice365Account() && !syncAccount.GetUseOAuth() && syncAccount.WarningCodeId == Guid.Empty) {
					var warningException = new BasicAuthWarningException();
					helper.ProcessSynchronizationError(syncAccount.SenderEmailAddress, warningException);
				} else if (syncAccount.WarningCodeId != Guid.Empty && !GetIsManualDismissWarning(userConnection, syncAccount.WarningCodeId) &&
						(!syncAccount.IsOffice365Account() || syncAccount.GetUseOAuth())) {
					helper.CleanUpSynchronizationWarning(syncAccount.SenderEmailAddress);
				}
			}
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.LogInfo(string)"/>
		protected override void LogInfo(string message) {
			_log.Info(message);
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.LogInfo(string)"/>
		protected override void LogDebug(string message) {
			_log.DebugFormat(message);
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.LogWarn(string, Exception)"/>
		protected override void LogWarn(string message, Exception e = null) {
			_log.Warn(message);
		}

		/// <inheritdoc cref="BaseFailoverJob{T}.LogError(string, Exception)"/>
		protected override void LogError(string message, Exception e) {
			_log.Error(message, e);
		}

		#endregion

	}

	#endregion

}
