namespace Terrasoft.EmailDomain
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using System.Security;
	using EmailContract;
	using EmailContract.DTO;
	using IntegrationApi.Interfaces;
	using Terrasoft.Common;
	using global::Common.Logging;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.EmailDomain.Interfaces;
	using Terrasoft.EmailDomain.Model;
	using Terrasoft.IntegrationV2.Utils;
	using Terrasoft.Mail.Sender;
	using global::IntegrationV2.Files.cs.Utils;
	using Terrasoft.Core.Configuration;
	using System.Collections.Concurrent;

	#region Class: EmailRepository

	/// <summary>
	/// Email message model repository.
	/// </summary>
	[DefaultBinding(typeof(IEmailRepository))]
	internal class EmailRepository : IEmailRepository
	{

		#region Fields: Private

		/// <summary>
		/// <see cref="UserConnection"/> instance.
		/// </summary>
		private readonly UserConnection _userConnection;

		/// <summary>
		/// <see cref="IActivityUtils"/> implementation instance.
		/// </summary>
		private readonly IActivityUtils _activityUtils = ClassFactory.Get<IActivityUtils>();

		/// <summary>
		/// <see cref="IAttachmentRepository"/> implementation instance.
		/// </summary>
		private readonly IAttachmentRepository _attachmentRepository;

		/// <summary>
		/// <see cref="ILog"/> implementation instance.
		/// </summary>
		private readonly ILog _log = LogManager.GetLogger("EmailListener");

		/// <summary>
		/// <see cref="HashLocker"/> instance.
		/// </summary>
		private static readonly HashLocker _hashLocker = new HashLocker();

		private static readonly ConcurrentDictionary<string, Guid> _savingEmailToActivityMap = new ConcurrentDictionary<string, Guid>();

		#endregion

		#region Constructors: Public

		public EmailRepository(UserConnection uc) {
			_userConnection = uc;
			_attachmentRepository = ClassFactory.Get<IAttachmentRepository>(new ConstructorArgument("uc", uc));
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Creates activity instance for <paramref name="email"/>.
		/// </summary>
		/// <param name="email">Email model instance.</param>
		/// <param name="syncSessionId">Synchronization session identifier.</param>
		/// <returns>Saved activity instance.</returns>
		private Entity CreateActivity(EmailModel email, string syncSessionId) {
			var activity = GetActivityEntity();
			activity.SetDefColumnValues();
			activity.SetColumnValue("Sender", RemoveInvalidChars(email.From));
			activity.SetColumnValue("SendDate", email.SendDate);
			activity.SetColumnValue("Title", RemoveInvalidChars(email.Subject));
			activity.SetColumnValue("TypeId", IntegrationConsts.EmailTypeId);
			activity.SetColumnValue("Body", RemoveInvalidChars(email.Body));
			activity.SetColumnValue("IsHtmlBody", email.IsHtmlBody);
			activity.SetColumnValue("HeaderProperties", RemoveInvalidChars(string.Join("\n", email.Headers)));
			activity.SetColumnValue("OwnerId", _userConnection.CurrentUser.ContactId);
			activity.SetColumnValue("Recepient", RemoveInvalidChars(string.Join(" ", email.To)));
			activity.SetColumnValue("CopyRecepient", RemoveInvalidChars(string.Join(" ", email.Copy)));
			activity.SetColumnValue("BlindCopyRecepient", RemoveInvalidChars(string.Join(" ", email.BlindCopy)));
			activity.SetColumnValue("EmailSendStatusId", IntegrationConsts.EmailSentStatusId);
			activity.SetColumnValue("PriorityId", GetActivityPriority(email.Importance));
			activity.SetColumnValue("ActivityCategoryId", IntegrationConsts.EmailCategoryId);
			activity.SetColumnValue("StatusId", IntegrationConsts.ActivityCompletedStatusId);
			activity.SetColumnValue("DueDate", email.SendDate);
			activity.SetColumnValue("StartDate", email.SendDate);
			activity.SetColumnValue("MailHash", _activityUtils.GetEmailHash(_userConnection, email.SendDate, email.Subject,
				email.OriginalBody, _userConnection.CurrentUser.TimeZone));
			SaveActivity(activity, email, syncSessionId);
			return activity;
		}

		/// <summary>
		/// Saves <paramref name="activity"/> to database.
		/// </summary>
		/// <param name="activity">Activity entity instance.</param>
		/// <param name="email">Email model instance.</param>
		/// <param name="syncSessionId">Synchronization session identifier.</param>
		private void SaveActivity(Entity activity, EmailModel email, string syncSessionId) {
			var emailId = GetActivityIdByMessageId(email.MessageId);
			// If emailId from EMD was not found, then try to get it by hash. SkipMailHashCheck disabled by default.
			var lookingEmailIdByHash = emailId == Guid.Empty && ListenerUtils.GetIsFeatureDisabled("SkipMailHashCheck");
			// If emailId from EMD was found, but we need to looking it by hash. GetActivityByHash disabled by default.
			var forceGetEmailIdByHash = emailId != Guid.Empty && ListenerUtils.GetIsFeatureEnabled("GetActivityByHash");
			var useEmailHash = lookingEmailIdByHash || forceGetEmailIdByHash;
			if (useEmailHash) {
				emailId = GetActivityIdByHash(email);
			}
			var attachmentsCount = email.Attachments.Count;
			if (emailId != Guid.Empty) {
				activity.PrimaryColumnValue = emailId;
				email.Id = emailId;
				activity.UseAdminRights = false;
				activity.StoringState = StoringObjectState.Changed;
				attachmentsCount = ListenerUtils.GetIsFeatureEnabled("AllowEmailsReload")
					? _attachmentRepository.GetAttachmentsCount(emailId)
					: attachmentsCount;
			}
			if (emailId != Guid.Empty && attachmentsCount == email.Attachments.Count) {
				_log.Trace(string.Concat($"SessionId: {syncSessionId}. EmailRepository activity with id {activity.PrimaryColumnValue} ",
					$"and MessageId = {email.MessageId} not changed."));
				return;
			}
			if (ListenerUtils.GetIsFeatureDisabled("CreateActivitiesWithHashLocking")) {
				try {
					activity.Save();
				} catch(DbOperationException ex) {
					_log.Error($"SessionId: {syncSessionId}. DB error when executing {ex.SqlText}");
					throw;
				}
			} else {
				var lockText = useEmailHash ? activity.GetTypedColumnValue<string>("MailHash") : email.MessageId;
				using (_hashLocker.Lock(lockText)) {
					if (!_savingEmailToActivityMap.TryGetValue(email.MessageId, out emailId)) {
						emailId = useEmailHash ? GetActivityIdByHash(email) : GetActivityIdByMessageId(email.MessageId);
					}
					if (emailId == Guid.Empty) {
						activity.Save();
						_savingEmailToActivityMap[email.MessageId] = activity.PrimaryColumnValue;
					} else {
						email.Id = emailId;
						activity.PrimaryColumnValue = emailId;
					}
				}
			}
			_log.Trace(string.Concat($"SessionId: {syncSessionId}. EmailRepository save activity with id {activity.PrimaryColumnValue} ",
				$"with subject {activity.PrimaryDisplayColumnValue} and MessageId = {email.MessageId}"));
			email.Id = activity.PrimaryColumnValue;
			_attachmentRepository.SaveAttachments(email, emailId != Guid.Empty);
		}

		/// <summary>
		/// Creates EmailMessageData instance for <paramref name="activity"/>.
		/// </summary>
		/// <param name="activity">Activity entity instance.</param>
		/// <param name="email">Email model instance.</param>
		/// <param name="mailboxId">Mailbox identifier.</param>
		/// <param name="syncSessionId">Synchronization session identifier.</param>
		/// <returns>Instance <see cref="Entity"/> of EmailMessageData.</returns>
		private Entity CreateEmailMessageData(Entity activity, EmailModel email, Guid mailboxId, string syncSessionId) {
			var ticks = _activityUtils.GetSendDateTicks(_userConnection, activity);
			var userConnectionParam = new ConstructorArgument("userConnection", _userConnection);
			var helper = ClassFactory.Get<IEmailMessageHelper>(userConnectionParam);
			Dictionary<string, string> headers = new Dictionary<string, string>() {
				{ "MessageId", email.MessageId },
				{ "InReplyTo", email.InReplyTo },
				{ "SyncSessionId", syncSessionId },
				{ "References", email.References },
				{ "SendDateTicks", ticks.ToString() }
			};
			var emailMessage = helper.CreateEmailMessage(activity, mailboxId, headers, false);
			_log.Trace(string.Concat($"SessionId: {syncSessionId}. EmailRepository create email message ",
				$"for mailbox {mailboxId} for activity with id {activity.PrimaryColumnValue} ",
				$"with subject {activity.PrimaryDisplayColumnValue} ",
				$"with messageId = {email.MessageId} and email message data id = {emailMessage?.PrimaryColumnValue}"));
			if (ListenerUtils.GetIsFeatureEnabled("CreateActivitiesWithHashLocking")) {
				_savingEmailToActivityMap.TryRemove(email.MessageId, out _);
			}
			return emailMessage;
		}

		/// <summary>
		/// Returns <paramref name="importance"/> identifier.
		/// </summary>
		/// <param name="importance">Email message importance.</param>
		/// <returns>Message importance identifier.</returns>
		private Guid GetActivityPriority(EmailImportance importance) {
			return EmailPriorityConverter.GetActivityPriority((int)importance);
		}

		/// <summary>
		/// Returns activity identifier for <paramref name="messageId"/>.
		/// </summary>
		/// <param name="messageId">Email message identifier.</param>
		/// <returns>Activity identifier.</returns>
		private Guid GetActivityIdByMessageId(string messageId) {
			var headers = GetHeaders(messageId);
			return headers.Any() ? headers.First().Id : Guid.Empty;
		}

		/// <summary>
		/// Returns activity identifier using mail hash for <paramref name="email"/>.
		/// </summary>
		/// <param name="messageId"><see cref="EmailModel"/> instance.</param>
		/// <returns>Activity identifier.</returns>
		private Guid GetActivityIdByHash(EmailModel email) {
			List<Guid> emailIds = _activityUtils.GetExistingEmaisIds(_userConnection, email.SendDate, email.Subject,
				email.OriginalBody, _userConnection.CurrentUser.TimeZone);
			return emailIds.Any() ? emailIds.First() : Guid.Empty;
		}

		/// <summary>
		/// Creats email message headers select.
		/// </summary>
		/// <param name="messageId">Email message identifier.</param>
		/// <returns><see cref="Select"/> instance.</returns>
		private Select GetEmailHeaderSelect(string messageId) {
			return new Select(_userConnection)
				.Column("ActivityId")
				.Column("MessageId")
				.Column("InReplyTo")
				.Column("References")
			.From("EmailMessageData")
			.Where("MessageId").IsEqual(Column.Parameter(messageId)) as Select;
		}

		/// <summary>
		/// Create empty activity <see cref="Entity"/>.
		/// </summary>
		/// <returns>Activity<see cref="Entity"/>.</returns>
		private Entity GetActivityEntity() {
			var schema = _userConnection.EntitySchemaManager.GetInstanceByName("Activity");
			return schema.CreateEntity(_userConnection);
		}

		/// <summary>
		/// Replaces characters that break save query on DB level.
		/// </summary>
		/// <param name="rawString">Column value.</param>
		/// <returns><paramref name="rawString"/> without invalid characters.</returns>
		private string RemoveInvalidChars(string rawString) {
			return rawString.Replace('\0'.ToString(), string.Empty);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IEmailRepository.CreateEmail(Guid)"/>
		public Email CreateEmail(Guid activityId) {
			var email = new Email {
				Id = activityId.ToString()
			};
			var activityEntity = GetActivityEntity();
			if (activityEntity.FetchFromDB(activityId)) {
				email.Body = activityEntity.GetTypedColumnValue<string>("Body");
				email.IsHtmlBody = activityEntity.GetTypedColumnValue<bool>("IsHtmlBody");
			} else {
				throw new SecurityException(string.Format(
					new LocalizableString("Terrasoft.Core", "EntitySchema.Exception.NoRightForRead"),
					activityEntity.SchemaName));
			}
			return email;
		}

		/// <inheritdoc cref="IEmailRepository.Save(EmailModel, Guid, string)"/>
		public void Save(EmailModel email, Guid mailboxId = default(Guid), string syncSessionId = null) {
			var emd = GetEmailMessageData(email, mailboxId, syncSessionId);
			emd.Save();
		}

		/// <inheritdoc cref="IEmailRepository.GetEmailMessageData(EmailModel, Guid, string)"/>
		public Entity GetEmailMessageData(EmailModel email, Guid mailboxId = default(Guid), string syncSessionId = null) {
			var subject = email.Subject;
			_log.Trace($"SessionId: {syncSessionId}. Start creating Email Activity for mailbox: {mailboxId}. Email subject: {subject}");
			var activity = CreateActivity(email, syncSessionId);
			_log.Trace($"SessionId: {syncSessionId}. Finished creating Email Activity for mailbox: {mailboxId} AND start creating EmailMessageData. Email subject: {subject}");
			var emailMessage = CreateEmailMessageData(activity, email, mailboxId, syncSessionId);
			_log.Trace($"SessionId: {syncSessionId}. Finished creating EmailMessageData for mailbox: {mailboxId}. Email subject: {subject}");
			return emailMessage;
		}

		/// <inheritdoc cref="IEmailRepository.GetHeaders(string)"/>
		public IEnumerable<EmailModelHeader> GetHeaders(string messageId) {
			var result = new List<EmailModelHeader>();
			if (messageId.IsNullOrEmpty()) {
				return result;
			}
			var select = GetEmailHeaderSelect(messageId);
			using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection()) {
				using (IDataReader reader = select.ExecuteReader(dbExecutor)) {
					while (reader.Read()) {
						result.Add(new EmailModelHeader { 
							Id = reader.GetColumnValue<Guid>("ActivityId"),
							MessageId = reader.GetColumnValue<string>("MessageId"),
							InReplyTo = reader.GetColumnValue<string>("InReplyTo"),
							References = reader.GetColumnValue<string>("References")
						});
					}
				}
			}
			return result;
		}

		public bool CheckIsEmail(Guid emailId) {
			var select = new Select(_userConnection)
				.Column(Func.Count("Id"))
				.From("Activity")
				.Where("Id").IsEqual(Column.Parameter(emailId))
					.And("TypeId").IsEqual(Column.Parameter(IntegrationConsts.EmailTypeId)) as Select;
			return select.ExecuteScalar<int>() > 0;
		}

		#endregion

	}

	#endregion

}
