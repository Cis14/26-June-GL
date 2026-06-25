namespace Terrasoft.Configuration
{

	using System;
	using Core;
	using Messaging.Common;
	using global::Common.Logging;

	#region Class: MsgChannelUtilities

	public static class MsgChannelUtilities
	{

		#region Constants: Public

		public const string ProcessEngineSenderName = "ProcessEngine";

		[Obsolete("7.15.1 | Constant is not in use and will be removed in upcoming releases")]
		public const string ProcessEngineBackHistoryStateSenderName = "ProcessEngineBackHistoryState";

		#endregion

		#region Fields: Private

		private static readonly ILog _log = LogManager.GetLogger("WebSocket");

		#endregion

		#region Methods: Private

		private static bool CheckCanPostMessage(string userName, string senderName) {
			if (!MsgChannelManager.IsRunning) {
				_log.WarnFormat("Can't post message to {0} from {1} while MsgChannelManager is not running",
					userName ?? "All", senderName);
				return false;
			}
			return true;
		}

		private static IMsg CreateMessage(string sender, string msg) {
			IMsg simpleMessage = new SimpleMessage() {
				Id = Guid.NewGuid(),
				Body = msg
			};
			simpleMessage.Header.Sender = sender;
			return simpleMessage;
		}

		private static void PostMessageInternal(IMsgChannel channel, string sender, string msg) {
			IMsg simpleMessage = CreateMessage(sender, msg);
			_log.Debug($"Channel {channel.Name} post for {sender} msg: {msg}");
			channel.PostMessage(simpleMessage);
		}

		private static void PostMessageToAllInternal(string sender, string msg) {
			IMsg simpleMessage = CreateMessage(sender, msg);
			_log.Debug($"Post to all for {sender} msg: {msg}");
			MsgChannelManager.Instance.PostToAll(simpleMessage);
		}

		#endregion

		#region Methods: Public

		public static void PostMessage(UserConnection userConnection, string senderName, string messageText) {
			if (!CheckCanPostMessage(userConnection.CurrentUser.Name, senderName)) {
				return;
			}
			MsgChannelManager channelManager = MsgChannelManager.Instance;
			IMsgChannel userChannel = channelManager.FindItemByUId(userConnection.CurrentUser.Id);
			if (userChannel != null) {
				PostMessageInternal(userChannel, senderName, messageText);
			} else {
				_log.Info(string.Format("Channel not found for user {0} from {1}",
					userConnection.CurrentUser.Name, senderName));
			}
		}

		public static void PostMessageToAll(string senderName, string messageText) {
			if (!CheckCanPostMessage(null, senderName)) {
				return;
			}
			PostMessageToAllInternal(senderName, messageText);
		}

		#endregion
	}

	#endregion
}

