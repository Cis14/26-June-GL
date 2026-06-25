namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;

	#region Class: CreateSessionOptions

	public class CreateSessionOptions
	{

		#region Properties: Public

		public bool HasInputParameters { get; set; } = true;

		public bool UseDefaultSystemPrompt { get; set; } = true;

		/// <summary>
		/// Optional dictionary of prompt macro overrides.
		/// Key format: "Namespace.Name" (e.g., "Prompt.AiAssistant").
		/// Null = no overrides, inline defaults are used.
		/// </summary>
		public IDictionary<string, string> PromptOverrides { get; set; }

		/// <summary>
		/// Optional session-scoped tool filters applied before tool definitions are constructed.
		/// All filters must allow an action or intent for it to be exposed to the LLM.
		/// Filters that implement <see cref="IDescribableToolFilter"/> are persisted as descriptors on the session.
		/// </summary>
		public IList<IAIToolFilter> ToolFilters { get; set; }

		/// <summary>
		/// Optional maximum number of tokens that the model may generate in a single completion.
		/// Null means provider default behavior.
		/// </summary>
		public int? MaxOutputTokens { get; set; }

		#endregion

	}

	#endregion

	#region Interface: ICopilotSessionManager

	/// <summary>
	/// Interface for managing Copilot sessions.
	/// </summary>
	public interface ICopilotSessionManager
	{

		#region Methods: Public

		/// <summary>
		/// Creates a new session instance. Does not add it to cache.
		/// </summary>
		/// <param name="sessionType">The session mode. Affects which system prompt is chosen.</param>
		/// <param name="sessionId">Optional session identifier. If null, a new identifier will be generated.</param>
		/// <param name="options">Session creation options.</param>
		/// <returns>A new instance of <see cref="CopilotSession"/>.</returns>
		CopilotSession CreateSession(CopilotSessionType sessionType, Guid? sessionId = null,
			CreateSessionOptions options = null);

		/// <summary>
		/// Attach session to active session list.
		/// </summary>
		CopilotSession Add(CopilotSession copilotSession);

		/// <summary>
		/// Updates copilot session.
		/// </summary>
		/// <param name="copilotSession">Copilot session to update.</param>
		/// <param name="requestId">Optional Completion API request identifier.</param>
		void Update(CopilotSession copilotSession, Guid? requestId);

		/// <summary>
		/// Finds copilot session by identifier.
		/// </summary>
		CopilotSession FindById(Guid copilotSessionId);

		/// <summary>
		/// Gets copilot session by identifier.
		/// </summary>
		CopilotSession GetById(Guid copilotSessionId);

		/// <summary>
		/// Gets active copilot sessions for the user.
		/// </summary>
		IEnumerable<CopilotSession> GetActiveSessions(Guid userId);

		/// <summary>
		/// Gets a root session for the given session.
		/// </summary>
		/// <param name="session">A <see cref="CopilotSession"/> instance.</param>
		/// <returns>The root <see cref="CopilotSession"/> instance if set for the given session;
		/// otherwise, <c>null</c>.</returns>
		CopilotSession GetRootSession(CopilotSession session);

		/// <summary>
		/// Closes copilot session.
		/// </summary>
		void CloseSession(CopilotSession copilotSession, Guid? requestId);

		/// <summary>
		/// Renames copilot session.
		/// </summary>
		/// <param name="copilotSession">Session to rename.</param>
		/// <param name="sessionName">New session name.</param>
		void RenameSession(CopilotSession copilotSession, string sessionName);

		#endregion

	}

	#endregion

} 

