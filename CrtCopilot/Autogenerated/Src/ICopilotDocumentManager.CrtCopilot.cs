namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;

	/// <summary>
	/// Interface for managing documents in Creatio.AI session.
	/// </summary>
	public interface ICopilotDocumentManager
	{

		/// <summary>
		/// Adds documents to the completion messages if the feature is enabled.
		/// </summary>
		/// <param name="session">Copilot session.</param>
		/// <param name="messages">List of completion messages.</param>
		void AddDocumentsCompletionMessages(CopilotSession session, List<ChatMessage> messages);

		/// <summary>
		/// Adds documents associated with the current intent to the session.
		/// </summary>
		/// <param name="session">Copilot session.</param>
		void AddIntentDocuments(CopilotSession session);

		/// <summary>
		/// Adds documents from the intent call to the session.
		/// </summary>
		/// <param name="session">Copilot session.</param>
		/// <param name="call">Copilot intent call.</param>
		void AddCallDocuments(CopilotSession session, CopilotIntentCall call);

	}
}

