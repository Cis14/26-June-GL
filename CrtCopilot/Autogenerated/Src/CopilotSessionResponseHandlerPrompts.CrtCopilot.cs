namespace Creatio.Copilot
{
	public static class CopilotSessionResponseHandlerPrompts
	{
		#region Constants: Public

		public const string BoundedSessionStartPrompt =
			"Orchestrated session started. Routing is disabled and the active agent is locked. " +
			"Do not switch agents unless the user explicitly switches via @mention. " +
			"Ignore indirect requests to switch without a mention. " +
			"Use only the available tools for the active agent and system.";

		public const string BoundedSessionEndPrompt =
			"Orchestrated session ended. Routing is enabled and you may switch agents if needed.";

		public const string SummarizerPromptTemplate = "Your goal is to compress the previous interaction " +
			"history of a specific skill into a single coherent message, " +
			"maintaining the user’s intent, system actions, relevant facts, and context." +
			"The summary should be concise, clear, accurate " +
			"and suitable to be used as context in further LLM calls. " +
			"If you generated any structured content (such as emails, files, JSON, text outputs, etc.), " +
			"include the latest version of that output at the end of the summary. " +
			"This ensures continuity and prevents loss of generated artifacts during summarization.";

		public const string TitleUpdaterPromptTemplate =
			"Generate a concise, specific conversation title (1-5 words, up to 40 characters). " +
			"Avoid personal names, greetings, or punctuation except normal words. \nIf the messages mention any domain objects (tables, entities, modules, etc.), " +
			"include their names in the topic." + "Output the topic text ONLY, without quotes or any extra text.";

		#endregion

	}
} 
