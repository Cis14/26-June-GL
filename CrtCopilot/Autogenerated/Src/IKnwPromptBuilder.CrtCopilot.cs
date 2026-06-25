namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;

	#region Interface: IKnwPromptBuilder

	/// <summary>
	/// Builds knowledge source related prompt messages for a Copilot session.
	/// </summary>
	internal interface IKnwPromptBuilder
	{

		#region Methods: Public

		/// <summary>
		/// Adds a knowledge source prompt (system/user messages) into the provided message collection.
		/// </summary>
		/// <param name="session">Copilot session context.</param>
		/// <param name="messages">Collection to append generated messages to.</param>
		void AddKnwSourcePrompt(CopilotSession session, List<ChatMessage> messages);

		/// <summary>
		/// Adds a single knowledge source message to the session's internal pipeline.
		/// </summary>
		/// <param name="session">Copilot session context.</param>
		void AddKnwSourceMsg(CopilotSession session);

		#endregion

	}

	#endregion

}
