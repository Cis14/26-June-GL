namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;

	#region Interface: ICopilotMessageConfirmationHandler

	internal interface ICopilotMessageConfirmationHandler
	{
		/// <summary>
		/// Handles a user's confirmation response for tool calls in a Copilot session.
		/// Finds the last pending confirmation, determines if the response is approval or rejection,
		/// updates the tool calls' confirmation results, and executes approved tool calls.
		/// </summary>
		/// <param name="userMessage">The user's message containing the confirmation response.</param>
		/// <param name="session">The current Copilot session.</param>
		/// <param name="toolContext">The current Copilot tool context.</param>
		/// <returns>
		/// A list of Copilot messages resulting from executing approved tool calls, or an empty list if none were executed.
		/// </returns>
		List<CopilotMessage> HandleConfirmedToolCalls(CopilotMessage userMessage, CopilotSession session,
			CopilotToolContext toolContext);

		/// <summary>
		/// Processes the provided tool calls and generates a confirmation message if any tool calls require user confirmation.
		/// </summary>
		/// <param name="toolCalls">The collection of tool calls to evaluate.</param>
		/// <param name="toolContext">The context containing tool definitions and executors.</param>
		/// <param name="session">The chat session for preparing a context with additional information for message generation.</param>
		/// <returns>A list containing a confirmation message if required; otherwise, an empty list.</returns>
		List<CopilotMessage> GetConfirmationMessages(IEnumerable<ToolCall> toolCalls, CopilotToolContext toolContext, CopilotSession session);
	}

	#endregion

}
