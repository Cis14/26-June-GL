namespace Creatio.Copilot
{
	using System.Threading;
	using System.Threading.Tasks;
	using Creatio.Copilot.Actions;

	/// <summary>
	/// Interface for executing copilot engine chat session.
	/// </summary>
	public interface ICopilotApiSkillExecutor
	{

		/// <summary>
		/// API to execute a given intent asynchronously and returns the result of the intent call.
		/// </summary>
		/// <param name="call">The copilot intent call object that contains details of the intent to execute.</param>
		/// <param name="token">A cancellation token that can be used to cancel the asynchronous operation.</param>
		/// <returns>/// A task that represents the asynchronous operation. The task result contains a
		/// <see cref="Creatio.Copilot.CopilotIntentCallResult"/> object that holds the
		/// outcome of the intent execution.</returns>
		Task<CopilotIntentCallResult> ExecuteAsync(CopilotIntentCall call, CancellationToken token = default);

		/// <summary>
		/// Completes async action that was executed by Copilot.
		/// <param name="session">The copilot session.</param>
		/// <param name="token">A cancellation token that can be used to cancel the asynchronous operation.</param>
		/// </summary>
		Task<CopilotIntentCallResult> CompleteExecutingIntentAsync(CopilotSession session,
			CancellationToken token = default);

		/// <summary>
		/// Completes async action with ProcessElementId.
		/// </summary>
		/// <param name="session">The copilot session.</param>
		/// <param name="actionInstanceUId">Action instance identifier.</param>
		/// <param name="result">Action execution result.</param>
		void CompleteAction(CopilotSession session, string actionInstanceUId, CopilotActionExecutionResult result);
	}
}

