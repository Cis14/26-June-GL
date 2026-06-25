namespace Terrasoft.EmailDomain.Failover
{
	using System;
	using System.Collections.Generic;
	using IntegrationApi.Interfaces;
	using IntegrationApi.MailboxDomain.Interfaces;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.EmailDomain.Interfaces;
	using Terrasoft.IntegrationV2.Logging.Interfaces;

	#region Class: FetchEmailsJob

	/// <summary>
	/// <see cref="IFetchEmailsJob"/> implementation.
	/// </summary>
	[DefaultBinding(typeof(IFetchEmailsJob))]
	public class FetchEmailsJob : IFetchEmailsJob, IJobExecutor
	{
		#region Consts: Private

		private const string _jobGroupName = "FetchEmails";

		#endregion

		#region Fields: Private

		private readonly Lazy<ISynchronizationLogger> _log = new Lazy<ISynchronizationLogger>(() => ClassFactory.Get<ISynchronizationLogger>());
		private IEmailSyncQueueRepository _emailSyncQueueRepository;
		private IEmailProvider _emailProvider;
		private IMailboxService _mailboxService;

		#endregion

		#region Methods: Private

		private void InitDependencies(UserConnection uc) {
			_emailSyncQueueRepository = ClassFactory.Get<IEmailSyncQueueRepository>();
			var requestFactory = ClassFactory.Get<IHttpWebRequestFactory>();
			_emailProvider = ClassFactory.Get<IEmailProvider>(
				new ConstructorArgument("userConnection", uc),
				new ConstructorArgument("requestFactory", requestFactory));
			_mailboxService = ClassFactory.Get<IMailboxService>(new ConstructorArgument("uc", uc));
		}

		private void RunFetchCommand(Guid mailboxId, IEnumerable<string> messageIds, UserConnection uc) {
			try {
				var mailbox = _mailboxService.GetMailbox(mailboxId, false);
				if (mailbox == null) {
					_log.Value?.Warn($"Mailbox {mailboxId} not exists or not avaliable for {uc.CurrentUser.Name} user.");
					return;
				}
				_emailProvider.FetchEmails(mailbox.ConvertToSynchronizationCredentials(uc), messageIds);
				_log.Value?.Info($"Mailbox {mailboxId} fetch command for {string.Join(";", messageIds)} sent to Email listener.");
			} catch (Exception ex) {
				_log.Value?.Error($"Mailbox {mailboxId} fetch command error", ex);
			}
		}

		private bool GetIsFetchJobExists(IAppSchedulerWraper schedulerWraper) {
			var jobNames = schedulerWraper.GetJobKeysForJobGroup(_jobGroupName);
			return jobNames != null && jobNames.Count > 0;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IJobExecutor.Execute(UserConnection, IDictionary{string, object})"/>
		public void Execute(UserConnection userConnection, IDictionary<string, object> parameters = null) {
			_log.Value?.Info($"FetchEmailsJob started.");
			InitDependencies(userConnection);
			var messages = _emailSyncQueueRepository.GetAllEmailSyncQueueItems(userConnection);
			foreach (var item in messages) {
				RunFetchCommand(item.Key, item.Value, userConnection);
			}
			_log.Value?.Info($"FetchEmailsJob ended.");
		}

		/// <inheritdoc cref="IFetchEmailsJob.ScheduleJob(UserConnection)"/>
		public void ScheduleJob(UserConnection userConnection) {
			var schedulerWraper = ClassFactory.Get<IAppSchedulerWraper>();
			if (GetIsFetchJobExists(schedulerWraper)) {
				_log.Value?.Warn($"Job group {_jobGroupName} has running jobs, new job will not be created.");
				return;
			}
			_log.Value?.Info($"FetchEmailsJob task was added to scheduler job group {_jobGroupName}.");
			schedulerWraper.ScheduleImmediateJob<FetchEmailsJob>(_jobGroupName, userConnection.Workspace.Name,
					userConnection.CurrentUser.Name, isSystemUser: true);
		}

		#endregion

	}

	#endregion

}
