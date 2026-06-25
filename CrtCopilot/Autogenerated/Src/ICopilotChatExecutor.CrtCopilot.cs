namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;

	#region Class: AsyncChatWorkflowResult

	/// <summary>
	/// Async chat workflow result.
	/// </summary>
	public class AsyncChatWorkflowResult
	{

		#region Properties: Public

		/// <summary>
		/// Completion messages.
		/// </summary>
		public List<CopilotMessage> CompletionMessages { get; set; }

		#endregion

	}

	#endregion

	#region Interface: ICopilotChatExecutor

	/// <summary>
	/// Interface for executing copilot engine chat session.
	/// </summary>
	public interface ICopilotChatExecutor
	{

		#region Methods: Public

		/// <summary>
		/// Sends message from the user to the Copilot.
		/// </summary>
		/// <param name="content">Content of the message.</param>
		/// <param name="copilotSessionId">Copilot session identifier. If empty, creates new session.</param>
		/// <param name="copilotContext">Context of the message.</param>
		/// <param name="options">Options of the message.</param>
		/// <returns><see cref="CopilotChatPart"/>, the information of the conversation part.</returns>
		CopilotChatPart SendSession(string content, Guid? copilotSessionId = null,
			CopilotContext copilotContext = null, CopilotSendMessageOptions options = null);

		/// <summary>
		/// Handles tool calls and send messages to Copilot.
		/// </summary>
		/// <param name="toolMessages">Tool messages.</param>
		/// <param name="session">Copilot session.</param>
		/// <param name="copilotContext">Context of the message.</param>
		void HandleToolCallsCompletedAndSendSession(List<CopilotMessage> toolMessages, CopilotSession session,
			CopilotContext copilotContext = null);

		/// <summary>
		/// Completes workflow.
		/// </summary>
		/// <param name="copilotSessionId">Copilot session identifier.</param>
		/// <param name="workflowResult">Copilot workflow result.</param>
		void CompleteAsyncChatWorkflow(Guid copilotSessionId, AsyncChatWorkflowResult workflowResult);

		#endregion

	}

	#endregion

}

