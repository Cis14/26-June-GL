namespace Creatio.Copilot
{
	using System.Threading;
	using System.Threading.Tasks;
	using Common.Logging;
	using Terrasoft.Core.Factories;

	#region Class: CopilotSessionMessageSummarizer

	/// <summary>
	/// Handles the response by delegating to a summarization strategy.
	/// It depends only on the ISummarizationStrategy interface, making it fully decoupled.
	/// </summary>
	[DefaultBinding(typeof(ICopilotSessionResponseHandler))]
	public class CopilotSessionMessageSummarizer : ICopilotSessionResponseHandler {

		#region Fields: Private

		private readonly ISummarizationStrategy _strategy;
		private static readonly ILog _log = LogManager.GetLogger("Copilot");

		#endregion

		#region Constructors: Public

		public CopilotSessionMessageSummarizer(ISummarizationStrategy strategy) {
			_strategy = strategy;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Determines whether the specified Copilot session can be handled by this response handler.
		/// </summary>
		/// <param name="session">The session containing messages and context to evaluate for handling.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the session can be handled.</returns>
		public Task<bool> CanHandleAsync(CopilotSession session, CancellationToken cancellationToken = default) {
			_log.Info($"Checking if summarization is needed for session {session.Id}");
			return _strategy.NeedSummarizeAsync(session, cancellationToken);
		}

		/// <summary>
		/// Handles the given session asynchronously by delegating to the summarization strategy.
		/// </summary>
		/// <param name="session">The Copilot session containing messages and context to process.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public Task HandleAsync(CopilotSession session, CancellationToken cancellationToken = default) {
			return _strategy.SummarizeAsync(session, cancellationToken);
		}

		#endregion

	}

	#endregion

}

