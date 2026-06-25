namespace CrtCloudIntegration.Utilities
{
	using System.Diagnostics.CodeAnalysis;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Interface: IMsgChannelUtilities

	/// <summary>
	/// Proxy for <see cref="Terrasoft.Configuration.MsgChannelUtilities"/>
	/// </summary>
	/// <remarks>
	/// Required for testing purposes
	/// </remarks>
	public interface IMsgChannelUtilities
	{
		#region Methods: Public

		/// <summary>
		/// Sends a WebSocket message to a user identified by UserConnection
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/></param>
		/// <param name="senderName">Any string</param>
		/// <param name="messageText"><b>MUST</b> be a valid Json string</param>
		/// <remarks>
		/// To correctly encode message text use <see cref="Terrasoft.Common.Json"/> class
		///	<example>
		///	<code>
		/// string validString = Json.FormatJsonString(Json.Serialize(messageText), Formatting.Indented)
		/// </code>
		/// </example>
		/// </remarks>
		void PostMessage(UserConnection userConnection, string senderName, string messageText);

		/// <summary>
		/// Sends a WebSocket message to a all users
		/// </summary>
		/// <param name="senderName">Any string</param>
		/// <param name="messageText"><inheritdoc cref="PostMessage"/></param>
		/// <remarks><inheritdoc cref="PostMessage"/></remarks>
		void PostMessageToAll(string senderName, string messageText);

		#endregion
	}

	#endregion

	#region Class: MicroservicesMsgChannelUtilities

	/// <inheritdoc cref="IMsgChannelUtilities"/>
	[ExcludeFromCodeCoverage]
	[DefaultBinding(typeof(IMsgChannelUtilities))]
	public class MicroservicesMsgChannelUtilities : IMsgChannelUtilities
	{

		#region Methods: Public

		public void PostMessage(UserConnection userConnection, string senderName, string messageText) {
			Terrasoft.Configuration.MsgChannelUtilities.PostMessage(userConnection, senderName, messageText);
		}

		public void PostMessageToAll(string senderName, string messageText) {
			Terrasoft.Configuration.MsgChannelUtilities.PostMessageToAll(senderName, messageText);
		}

		#endregion

	}

	#endregion

}
