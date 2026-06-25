namespace Creatio.Copilot
{
    /// <summary>
    /// Interface for mentioning agents in chat.
    /// </summary>
    public interface IToolExecutionTrigger
    {
        /// <summary>
        /// Executes tool called by User.
        /// </summary>
        /// <param name="session">Copilot session.</param>
        /// <returns></returns>
        CopilotSession TriggerExecution(CopilotSession session);
    }
}

