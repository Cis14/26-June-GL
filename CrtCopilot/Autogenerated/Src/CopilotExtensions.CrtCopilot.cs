namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;

	internal static class CopilotExtensions
	{

		#region Fields: Private

		private static readonly Guid _sendMessagesToUserIntentUId = new Guid("51c693e3-2c94-b0ee-6b1a-0fdcc63c82ec");

		#endregion

		#region Methods: Private

		private static bool IsTool(this CopilotMessage message) {
			return message != null && message.Role == CopilotMessageRole.Tool &&
				!string.IsNullOrWhiteSpace(message.ToolCallId);
		}

		#endregion

		#region Methods: Public

		public static CopilotIntentSchemaManager GetIntentSchemaManager(this UserConnection userConnection) {
			return userConnection.Workspace.SchemaManagerProvider.GetManager<CopilotIntentSchemaManager>();
		}

		public static CopilotActionTypeSchemaManager GetActionTypeSchemaManager(this UserConnection userConnection) {
			return userConnection.Workspace.SchemaManagerProvider.GetManager<CopilotActionTypeSchemaManager>();
		}

		public static bool IsToolCall(this CopilotMessage message) {
			return message != null && message.Role == CopilotMessageRole.Assistant &&
				message.ContentType != CopilotContentType.Confirmation && message.ToolCalls.Any() &&
				!string.IsNullOrWhiteSpace(message.ToolCallId);
		}

		public static bool IsConfirmation(this CopilotMessage message) {
			return message != null && message.Role == CopilotMessageRole.Assistant &&
				message.ContentType == CopilotContentType.Confirmation && message.Buttons.Any();
		}

		public static bool IsPendingConfirmation(this CopilotMessage message) {
			return message.IsConfirmation() && string.IsNullOrWhiteSpace(message.ConfirmationResult);
		}

		public static bool IsRejectedConfirmation(this CopilotMessage message) {
			return message.IsConfirmation() && message.ConfirmationResult == CopilotToolConfirmationResult.Reject;
		}

		public static bool IsPendingClarification(this CopilotMessage message) {
			return message != null && message.ContentType == CopilotContentType.Clarification &&
				string.IsNullOrWhiteSpace(message.ConfirmationResult);
		}

		/// <summary>
		/// Determines whether the message is pending async action completion.
		/// </summary>
		/// <param name="message">The copilot message.</param>
		/// <returns>True if message is pending async action; otherwise, false.</returns>
		public static bool IsPendingAsyncAction(this CopilotMessage message) {
			return message.Role == CopilotMessageRole.Tool &&
				message.ContentType == CopilotContentType.AsyncAction &&
				string.IsNullOrWhiteSpace(message.ConfirmationResult);
		}

		public static bool IsFromWorkflow(this CopilotMessage message) {
			return message != null && message.Role == CopilotMessageRole.System &&
			CopilotWorkflowService.WorkflowSystemMessages.Contains(message.Content);
		}

		public static bool IsNotification(this CopilotMessage message) {
			return message != null && message.ContentType == CopilotContentType.Notification;
		}

		public static bool IsCancellation(this CopilotMessage message) {
			return message != null && message.ContentType == CopilotContentType.Cancellation;
		}

		public static bool IsFromUser(this CopilotMessage message) {
			return message != null && message.Role == CopilotMessageRole.User;
		}

		public static bool IsFromAssistant(this CopilotMessage message) {
			return message != null && message.Role == CopilotMessageRole.Assistant;
		}

		public static bool IsToolById(this CopilotMessage message, string toolCallId) {
			return message.IsTool() && message.ToolCallId == toolCallId;
		}

		public static IEnumerable<CopilotMessage> GetMergedChatHistoryMessages(this CopilotSession session) {
			return session.GetMergedMessages().Where(message =>
				message.Role != CopilotMessageRole.Tool &&
				(message.Role != CopilotMessageRole.System || message.IsSummary) &&
				message.Content != null);
		}

		public static string GenerateToolCallId() {
			return "call_" + Guid.NewGuid().ToString("N").Substring(0, 24);
		}

		/// <summary>
		/// Returns localizable string from specified resource group.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="localizableStringName">Localizable string name.</param>
		/// <param name="resourceGroupName">Resource group name</param>
		/// <returns>Localizable string.</returns>
		public static LocalizableString GetLocalizableString(this UserConnection userConnection,
				string localizableStringName, string resourceGroupName) {
			string lsv = "LocalizableStrings." + localizableStringName + ".Value";
			return new LocalizableString(userConnection.Workspace.ResourceStorage, resourceGroupName, lsv);
		}

		/// <summary>
		/// Checks whether the intent has SendMessagesToUser capability.
		/// </summary>
		/// <param name="intent">Intent to check.</param>
		/// <returns><c>true</c> if intent has added SendMessagesToUser capability; otherwise <c>false</c>.</returns>
		public static bool HasSendMessagesToUserCapability(this CopilotIntentSchema intent) {
			return intent != null && intent.SubIntents.Any(subIntent => subIntent.UId == _sendMessagesToUserIntentUId);
		}

		/// <summary>
		/// Checks whether all tools should omit assistant response.
		/// </summary>
		/// <param name="toolMessages"></param>
		/// <returns></returns>
		public static bool IsAllToolsShouldOmitAssistantResponse(this List<CopilotMessage> toolMessages) {
			return toolMessages
				.Where(msg => msg.Role == CopilotMessageRole.Tool)
				.All(msg => msg.OmitAssistantResponse);
		}
	}

	#endregion

}

