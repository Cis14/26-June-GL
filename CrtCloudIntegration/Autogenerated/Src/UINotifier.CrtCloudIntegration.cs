namespace CrtCloudIntegration.Utilities
{
	using CrtCloudIntegration.Dto;
	using CrtCloudIntegration.Utilities.Errors;
	using Newtonsoft.Json;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Interface: IUINotifier

	/// <summary>
	/// Interface for the notification on UI.
	/// </summary>
	public interface IUINotifier
	{

		#region Methods: Public

		/// <summary>
		/// Send notification with the specified command and description.
		/// </summary>
		/// <param name="commandName">Name of the command.</param>
		/// <param name="description">The description.</param>
		/// <param name="platform">Ad platform name</param>
		/// <param name="customSenderName">Custom name of the sender instead of SenderName property.</param>
		/// <param name="websocketSessionId">The websocket session identifier.</param>
		void Notify(string commandName, string platform = "", string description = "", string customSenderName = "",
			string websocketSessionId = "");

		/// <summary>
		/// Reports the error.
		/// </summary>
		/// <param name="error">The error.</param>
		/// <param name="customSenderName">Custom name of the sender instead of SenderName property.</param>
		/// <param name="websocketSessionId">The websocket session identifier.</param>
		void ReportError(IError error, string customSenderName = "", string websocketSessionId = "");

		#endregion

	}

	#endregion


	#region Class: UINotifierBase

	/// <summary>
	/// Represents the implementation of UI notifier.
	/// </summary>
	/// <seealso cref="CrtCloudIntegration.Utilities.IUINotifier" />
	public abstract class UINotifierBase: IUINotifier
	{
		#region Fields: Private

		private readonly IMsgChannelUtilities _msgChannelUtilities;
		private readonly UserConnection _userConnection;

		#endregion

		#region Properties: Protected

		protected abstract string SenderName { get; }

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="UINotifierBase"/> class.
		/// </summary>
		public UINotifierBase() {
			_msgChannelUtilities = ClassFactory.Get<IMsgChannelUtilities>();
			_userConnection = ClassFactory.Get<UserConnection>();
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Posts the message.
		/// </summary>
		/// <param name="webSocketDto">The web socket dto.</param>
		/// <param name="senderName">Name of the sender.</param>
		private void PostMessage(WebSocketDto webSocketDto, string senderName) {
			senderName = senderName.IsNullOrEmpty() ? SenderName : senderName;

			_msgChannelUtilities.PostMessage(
				_userConnection, senderName,
				Json.FormatJsonString(Json.Serialize(webSocketDto), Formatting.Indented)
			);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IUINotifier.Notify"/>
		public virtual void Notify(string commandName, string platform = "", string description = "", string customSenderName = "",
			string websocketSessionId = "") {
			var webSocketDto = new WebSocketDto {
				IsSuccess = true,
				Command = commandName,
				Description = description,
				Platform = platform,
				WebsocketSessionId = websocketSessionId
			};
			PostMessage(webSocketDto, customSenderName);
		}

		/// <inheritdoc cref="IUINotifier.ReportError"/>
		public virtual void ReportError(IError error, string customSenderName = "", string websocketSessionId = "") {
			var translation = new ErrorToNotificationTranslator(_userConnection);
			var webSocketDto = translation.TranslateError(error);
			webSocketDto.WebsocketSessionId = websocketSessionId;
			var sender = customSenderName.IsNullOrEmpty() ? SenderName : customSenderName;
			PostMessage(webSocketDto, sender);
		}

		#endregion

	}

	#endregion

}

