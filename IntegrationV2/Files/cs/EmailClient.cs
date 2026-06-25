namespace IntegrationV2
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using Common.Logging;
	using EmailContract.DTO;
	using IntegrationApi.MailboxDomain.Interfaces;
	using IntegrationApi.MailboxDomain.Model;
	using Terrasoft.Common;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;
	using Terrasoft.EmailDomain;
	using Terrasoft.EmailDomain.Interfaces;
	using Terrasoft.IntegrationV2.Utils;
	using Terrasoft.Mail;
	using Terrasoft.Mail.Sender;
	using Credentials = EmailContract.DTO.Credentials;
	using Mail = Terrasoft.Mail;

	#region Class: EmailClient

	/// <summary>Represents email client class.</summary>
	[DefaultBinding(typeof(IEmailClient), Name = "EmailClient")]
	public class EmailClient : IEmailClient
	{

		#region Fields: Private

		protected readonly UserConnection _userConnection;
		private readonly Credentials _credentials;
		private readonly IEmailService _emailService;
		private Mailbox _mailbox;
		private readonly ILog _emailSenderLogger;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// <see cref="EmailClient"/> ctor.
		/// </summary>
		/// <param name="userConnection">An instance of the user connection.</param>
		public EmailClient(UserConnection userConnection) {
			_userConnection = userConnection;
			_emailService = ClassFactory.Get<IEmailService>(
				new ConstructorArgument("uc", _userConnection));
			_emailSenderLogger = LogManager.GetLogger("EmailSender");
		}

		/// <summary>
		/// <see cref="EmailClient"/> ctor.
		/// </summary>
		/// <param name="userConnection">An instance of the user connection.</param>
		/// <param name="credentials">Short email connection credentials.</param>
		public EmailClient(UserConnection userConnection, Mail.Credentials credentials)
			: this(userConnection) {
			var mailServer = GetMailServer(credentials.ServerId);
			_credentials = new Credentials {
				UserName = credentials.UserName,
				Password = credentials.UserPassword,
				UseOAuth = credentials.UseOAuth,
				ServiceUrl = mailServer.OutgoingServerAddress,
				ServerTypeId = mailServer.TypeId,
				Port = mailServer.OutgoingPort,
				UseSsl = mailServer.OutgoingUseSsl,
				SecureOption = mailServer.GetSecureOption(false)
			};
		}

		/// <summary>
		/// <see cref="EmailClient"/> ctor.
		/// </summary>
		/// <param name="userConnection">An instance of the user connection.</param>
		/// <param name="emailCredentials">Full email connection credentials.</param>
		public EmailClient(UserConnection userConnection, Credentials emailCredentials)
			: this(userConnection) {
			_credentials = emailCredentials;
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Set email headers.
		/// </summary>
		/// <param name="email"><see cref="Email"/> instance.</param>
		/// <param name="headerProperties">List of <see cref="EmailMessageHeader"/>.</param>
		private void SetEmailHeaders(Email email, List<EmailMessageHeader> headerProperties) {
			var headers = new List<string>();
			if (headerProperties != null) {
				foreach (var property in headerProperties) {
					headers.Add(string.Concat(property.Name, "=", property.Value));
				}
			}
			email.Headers = headers;
		}

		/// <summary>
		/// Fills recipients from <see cref="EmailMessage"/> to <see cref="Email"/>.
		/// </summary>
		/// <param name="email"><see cref="Email"/> instance.</param>
		/// <param name="emailMessage"><see cref="EmailMessage"/> instance.</param>
		private void SetEmailRecipients(Email email, EmailMessage emailMessage) {
			FillMessageRecipientsCollection(email.Recepients, emailMessage.To);
			FillMessageRecipientsCollection(email.CopyRecepients, emailMessage.Cc);
			FillMessageRecipientsCollection(email.BlindCopyRecepients, emailMessage.Bcc);
		}

		/// <summary>
		/// Fills <paramref name="collection"/> recipients collection with <paramref name="values"/>.
		/// </summary>
		/// <param name="collection">Exchange email recipients collection.</param>
		/// <param name="values">Recipients values.</param>
		private void FillMessageRecipientsCollection(List<string> collection, List<string> values) {
			foreach (var address in values) {
				collection.Add(address.ExtractEmailAddress());
			}
		}

		/// <summary>
		/// Returns <see cref="Email"/> instance.</summary>
		/// <param name="emailMessage"><see cref="EmailMessage"/> instance.</param>
		/// <param name="ignoreRights">Flag that indicates whether to ignore rights.</param> 
		/// <returns><see cref="Email"/> instance.</returns>
		private Email GetEmail(EmailMessage emailMessage, bool ignoreRights = false) {
			var email = new Email {
				Id = emailMessage.GetMessageId(),
				Subject = emailMessage.Subject,
				Body = emailMessage.Body,
				Sender = GetSender(emailMessage, ignoreRights),
				Importance = (EmailContract.EmailImportance)emailMessage.Priority,
				IsHtmlBody = emailMessage.IsHtmlBody
			};
			SetEmailRecipients(email, emailMessage);
			SetEmailHeaders(email, emailMessage.HeaderProperties);
			SetAttachments(email, emailMessage.Attachments);
			return email;
		}

		/// <summary>
		/// Returns email sender value.</summary>
		/// <param name="emailMessage"><see cref="EmailMessage"/> instance.</param>
		/// <param name="ignoreRights">Flag that indicates whether to ignore rights.</param> 
		/// <returns>Email sender value.</returns>
		private string GetSender(EmailMessage emailMessage, bool ignoreRights = false) {
			if (_credentials != null) {
				return emailMessage.From;
			}
			var mailbox = GetMailbox(emailMessage.From, ignoreRights);
			return mailbox.GetSender();
		}

		/// <summary>
		/// Fill <see cref="Email.Attachments"/> collection.</summary>
		/// <param name="emailMessage"><see cref="Email"/> instance.</param>
		/// <param name="emailMessage"><see cref="EmailAttachment"/> collection.</param>
		private void SetAttachments(Email email, List<EmailAttachment> attachments) {
			var attachmentsDto = new List<Attachment>();
			foreach (EmailAttachment attachment in attachments) {
				var attachmentDto = new Attachment {
					Id = attachment.Id.ToString(),
					Name = attachment.Name,
					IsInline = attachment.IsContent
				};
				attachmentDto.SetData(attachment.Data);
				attachmentsDto.Add(attachmentDto);
			}
			email.Attachments = attachmentsDto;
		}

		/// <summary>
		/// Get send email credentials.
		/// </summary>
		/// <param name="from">Sender email address.</param>
		/// <param name="ignoreRights">Ignore mailbox rights flag.</param>
		/// <param name="useForSynchronization">Sign is synchronization mode or not.</param>
		/// <returns><see cref="Credentials"/> instance.</returns>
		private Credentials GetCredentials(string from, bool ignoreRights, bool useForSynchronization = true) {
			if (_credentials != null && (!_credentials.UseOAuth || _credentials.AccessToken.IsNotNullOrEmpty())) {
				return GetEmailCredentials(from);
			}
			return GetEmailCredentialsFromMailbox(from, ignoreRights, useForSynchronization);
		}

		/// <summary>
		/// Returns <see cref="Credentials"/> instance.</summary>
		/// <param name="from">Sender email.</param>
		/// <param name="ignoreRights">Ignore mailbox rights flag.</param>
		/// <param name="useForSynchronization">Sign is synchronization mode or not.</param>
		/// <returns><see cref="Credentials"/> instance.</returns>
		private Credentials GetEmailCredentialsFromMailbox(string from, bool ignoreRights,
				bool useForSynchronization = true) {
			var mailbox = GetMailbox(from, ignoreRights);
			return mailbox.ConvertToSynchronizationCredentials(_userConnection, useForSynchronization);
		}

		/// <summary>
		/// Returns <see cref="Mailbox"/> instance.</summary>
		/// <param name="from">Sender email.</param>
		/// <param name="ignoreRights">Ignore mailbox rights flag.</param>
		/// <returns><see cref="Mailbox"/> instance.</returns>
		private Mailbox GetMailbox(string from, bool ignoreRights) {
			if (_mailbox != null) {
				return _mailbox;
			}
			var mailboxService = ClassFactory.Get<IMailboxService>(new ConstructorArgument("uc", _userConnection));
			_mailbox = mailboxService.GetMailboxBySenderEmailAddress(from, !ignoreRights, false);
			if (!_mailbox.AllowEmailSend) {
				throw new Exception($"Mailbox {_mailbox.Id} does not allow sending.");
			}
			return _mailbox;
		}

		/// <summary>
		/// Returns <see cref="Credentials"/> instance from <see cref="Mail.Credentials"/> instance.</summary>
		/// <param name="from">Sender email.</param>
		/// <returns><see cref="Credentials"/> instance.</returns>
		private Credentials GetEmailCredentials(string from) {
			var credentials = _credentials;
			credentials.SenderEmailAddress = from;
			return credentials;
		}

		/// <summary>
		/// Returns mail server instance.</summary>
		/// <param name="serverId">Mail server identifier.</param>
		/// <returns>Mail server instance.</returns>
		private MailServer GetMailServer(Guid serverId) {
			var service = ClassFactory.Get<IMailServerService>(new ConstructorArgument("uc", _userConnection));
			return service.GetServer(serverId, false);
		}

		private void InternalSend(EmailMessage emailMessage, Email emailDto, Credentials credentials, string messageId, bool ignoreRights) {
			var sendResult = _emailService.Send(emailDto, credentials, ignoreRights);
			_emailSenderLogger.Debug($"[EmailClient] Email was sent with result {sendResult}, Subject: {emailMessage.Subject}, MessageId: {messageId}, Sender: {emailMessage.From}");
			if (sendResult.IsNotNullOrEmpty()) {
				throw new EmailException("ErrorOnSend", sendResult);
			}
		}

		private void InternalSendV3(EmailMessage emailMessage, Email emailDto, Credentials credentials, string messageId, bool ignoreRights) {
			try {
				var sendResult = _emailService.Send(emailDto, credentials, ignoreRights, 3);
				_emailSenderLogger.Debug($"[EmailClient] Email was sent to v3 endpoint with result {sendResult}, Subject: {emailMessage.Subject}, MessageId: {messageId}, Sender: {emailMessage.From}");
				var result = Terrasoft.Common.Json.Json.Deserialize<SendEmailResult>(sendResult);
				if (result.MessageId.IsNotNullOrEmpty() && result.MessageId != messageId) {
					_emailSenderLogger.Debug($"[EmailClient] MessageId was updated from {messageId} to {result.MessageId}, Subject: {emailMessage.Subject}, Sender: {emailMessage.From}");
					emailMessage.SetMessageId(result.MessageId);
				}
			}
			catch(WebException ex) {
				string message = null;
				if (ex.Response is HttpWebResponse response) {
					using (response)
					using (var stream = response.GetResponseStream())
					using (var reader = stream != null ? new StreamReader(stream) : null) {
						message = reader?.ReadToEnd();
					}
				}
				message = message ?? ex.Message;
				_emailSenderLogger.Debug($"[EmailClient] Email was not sent to v3 endpoint with result {message}, Subject: {emailMessage.Subject}, MessageId: {messageId}, Sender: {emailMessage.From}");
				throw new EmailException("ErrorOnSend", message);
			}
			catch (Exception ex) {
				_emailSenderLogger.Debug($"[EmailClient] Email was not sent to v3 endpoint with result {ex.Message}, Subject: {emailMessage.Subject}, MessageId: {messageId}, Sender: {emailMessage.From}");
				throw new EmailException("ErrorOnSend", ex.Message);
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Preparing email by <paramref name="emailMessage"/> data and send it.</summary>
		/// <param name="emailMessage">Email message data.</param>
		/// <param name="ignoreRights">Flag that indicates whether to ignore rights.</param>
		public void Send(EmailMessage emailMessage, bool ignoreRights = false) {
			var messageId = emailMessage.GetMessageId();
			_emailSenderLogger.Debug($"[EmailClient] Start sending email, PreparedEmailDTO, Subject: {emailMessage.Subject}, MessageId: {messageId}, Sender: {emailMessage.From}");
			var emailDto = GetEmail(emailMessage, ignoreRights);
			_emailSenderLogger.Debug($"[EmailClient] Receiving of credentials, Subject: {emailMessage.Subject}, MessageId: {messageId}, Sender: {emailMessage.From}");
			var credentials = GetCredentials(emailMessage.From, ignoreRights, false);
			if(credentials.ServerTypeId == IntegrationConsts.GraphMailServerTypeId) {
				InternalSendV3(emailMessage, emailDto, credentials, messageId, ignoreRights);
			}
			else {
				InternalSend(emailMessage, emailDto, credentials, messageId, ignoreRights);
			}
		}

		#endregion

	}

	#endregion

}
