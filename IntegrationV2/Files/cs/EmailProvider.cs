namespace IntegrationV2
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Text;
	using EmailContract.Commands;
	using EmailContract.DTO;
	using IntegrationApi.Interfaces;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: EmailProvider

	/// <summary>
	/// Provides methods for email provider service interaction.
	/// </summary>
	[DefaultBinding(typeof(IEmailProvider))]
	public class EmailProvider : IEmailProvider
	{

		#region Fields: Private

		/// <summary>
		/// <see cref="UserConnection"/> instance.
		/// </summary>
		private readonly UserConnection _userConnection;

		/// <summary>
		/// <see cref="IHttpWebRequestFactory"/> implementation instance.
		/// </summary>
		private readonly IHttpWebRequestFactory _requestFactory;

		#endregion

		#region Constructors: Public

		public EmailProvider(UserConnection userConnection, IHttpWebRequestFactory requestFactory) {
			_userConnection = userConnection;
			_requestFactory = requestFactory;
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Serializes <paramref name="command"/> for web request.
		/// </summary>
		/// <param name="command">Email command instance.</param>
		/// <returns>Serialized command.</returns>
		private byte[] Serialize(object command) {
			var json = Json.Serialize(command, true);
			return Encoding.UTF8.GetBytes(json);
		}

		/// <summary>
		/// Sends <paramref name="command"/> to <paramref name="commandHandlerUri"/>.
		/// </summary>
		/// <param name="command">Email command instance.</param>
		/// <param name="commandHandlerUri">Email provider service send method uri.</param>
		/// <returns>Email provider response.</returns>
		private string SendCommand(object command, string commandHandlerUri) {
			var data = Serialize(command);
			return SendCommand(data, commandHandlerUri);
		}

		/// <summary>
		/// Sends <paramref name="data"/> to <paramref name="commandHandlerUri"/>.
		/// </summary>
		/// <param name="data">Serialized command data.</param>
		/// <param name="commandHandlerUri">Email provider service send method uri.</param>
		/// <returns>Email provider response.</returns>
		private string SendCommand(byte[] data, string commandHandlerUri) {
			var request = _requestFactory.Create(commandHandlerUri);
			request.Method = "POST";
			request.ContentType = "application/json; charset=utf-8";
			request.ContentLength = data.Length;
			request.Timeout = 5 * 60 * 1000;
			request.ConnectionGroupName = "EmailProvider";
			using (Stream stream = request.GetRequestStream()) {
				stream.Write(data, 0, data.Length);
			}
			using (WebResponse response = request.GetResponse()) {
				using (Stream dataStream = response.GetResponseStream()) {
					using(StreamReader reader = new StreamReader(dataStream)) {
						return reader.ReadToEnd();
					}
				}
			}
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IEmailProvider.Send(Email, IEnumerable{Attachment}, Credentials, int)"/>
		public string Send(Email message, IEnumerable<Attachment> attachments, Credentials credentials, int version = 2) {
			string serviceUri = ExchangeListenerActions.GetSendEmailUrl(_userConnection, version);
			var command = new SendCommand() {
				Email = message,
				Credentials = credentials
			};
			return SendCommand(command, serviceUri);
		}

		/// <inheritdoc cref="IEmailProvider.StartSynchronization(SynchronizationCredentials, string)"/>
		public void StartSynchronization(SynchronizationCredentials credentials, string filters) {
			string serviceUri = ExchangeListenerActions.GetSynchronizeEmailsUrl(_userConnection);
			SendCommand(new SyncEmailsCommand() {
				Credentials = credentials,
				Filters = filters
			}, serviceUri);
		}

		/// <inheritdoc cref="IEmailProvider.FetchEmails(SynchronizationCredentials, IEnumerable{string})"/>
		public void FetchEmails(SynchronizationCredentials credentials, IEnumerable<string> messageIds) {
			string serviceUri = ExchangeListenerActions.GetFetchEmailsUrl(_userConnection);
			SendCommand(new SyncEmailsCommand() {
				Credentials = credentials,
				Filters = string.Join(";", messageIds)
			}, serviceUri);
		}

		#endregion

	}

	#endregion

}
