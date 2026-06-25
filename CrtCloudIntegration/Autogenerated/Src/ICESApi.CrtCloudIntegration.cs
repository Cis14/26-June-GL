namespace Terrasoft.Configuration.CES
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Configuration.CESModels;
	using Terrasoft.Configuration.CESResponses;

	public interface ICESApi
	{

		#region Properties: Public

		/// <summary>
		/// The Api Key for the project received from the CESApp website
		/// </summary>
		string ApiKey { get; }

		/// <summary>
		/// Serivce base url
		/// </summary>
		string BaseUrl { get; set; }

		/// <summary>
		/// Serivce base secure url
		/// </summary>
		string BaseSecureUrl { get; set; }

		#endregion

		#region Methods: Public

		/// <summary>
		/// Sends a new transactional message through CES.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		/// <param name="emailId">
		/// Email id.
		/// </param>
		/// <param name="send_at">
		/// The send_at.
		/// </param>
		BaseCloudResponse SendMessage(EmailMessage message, Guid emailId, DateTime send_at = new DateTime());

		/// <summary>
		/// Send a new transactional message through CES using a template
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		/// <param name="templateName">
		/// </param>
		/// <param name="templateContents">
		/// </param>
		/// <param name="emailId">
		///		Identifier of email.
		/// </param>
		/// <param name="send_at">
		/// The send_at.
		/// </param>
		BaseCloudResponse SendTemplate(EmailMessage message, string templateName,
			IEnumerable<TemplateContent> templateContents, Guid emailId, DateTime send_at = new DateTime());

		/// <summary>
		/// Add a new template.
		/// </summary>
		/// <param name="name">The name for the new template - must be unique.</param>
		/// <param name="fromEmail">A default sending address for emails sent using this template.</param>
		/// <param name="fromName">A default from name to be used.</param>
		/// <param name="subject">A default subject line to be used.</param>
		/// <param name="code">
		/// The HTML code for the template with <c>mc:edit</c> attributes for
		/// the editable elements.
		/// </param>
		/// <param name="text">A default text part to be used when sending with this template.</param>
		/// <param name="emailId">Unique id of email.</param>
		/// <param name="images">List of inline attachments.</param>
		/// <param name="checksum">Current template checksum.</param>
		/// <param name="replicaId">The replica identifier.</param>
		/// <returns>
		/// A <see cref="BaseCloudResponse"/> object.
		/// </returns>
		BaseCloudResponse AddTemplate(string name, string fromEmail, string fromName, string subject, string code,
			string text, Guid emailId, IEnumerable<image> images = null, string checksum = null,
			string replicaId = null);

		/// <summary>
		/// Validate an API key and respond to a ping
		/// </summary>
		/// <returns>
		/// The <see cref="string" />.
		/// </returns>
		string Ping(ActualLicenseInfoContract licInfo);

		/// <summary>
		/// Updates the user setttings.
		/// </summary>
		/// <returns>
		/// The <see cref="AccountInfo" />.
		/// </returns>
		AccountInfo UpdateUserSettings(string webHookAppDomain, string authKey);

		/// <summary>
		/// Returns the user account info.
		/// </summary>
		/// <returns>
		/// The <see cref="AccountInfo" />.
		/// </returns>
		AccountInfo AccountInfo(string authKey);

		/// <summary>
		/// Validates sender's emails and send validation emails if needed.
		/// </summary>
		/// <param name="emails">List of sender's emails for validation</param>
		/// <returns>Lists of incorrect and validated emails</returns>
		[Obsolete("7.18.3 | Use ValidateSenderForProvider(IEnumerable<string> emails, string providerName) instead")]
		SenderValidationInfo ValidateSender(IEnumerable<string> emails);

		/// <summary>
		/// The list senders.
		/// </summary>
		/// <returns>
		/// The <see cref="List{T}" />.
		/// </returns>
		CloudSenderDomainsInfo SenderDomainsInfo();

		/// <summary>
		/// Adds the sender domains information.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <returns></returns>
		CloudSenderDomainsInfo AddSenderDomainsInfo(string domain);

		/// <summary>
		/// Gets the checked emails by api key.
		/// </summary>
		/// <param name="emails">Enumerable of emails.</param>
		/// <returns></returns>
		CheckedEmailResponse GetCheckedEmails(IEnumerable<string> emails);

		#endregion

	}
}

