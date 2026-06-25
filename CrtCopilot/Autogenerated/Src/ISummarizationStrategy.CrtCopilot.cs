namespace Creatio.Copilot
{
	using System.Threading;
	using System.Threading.Tasks;

	#region Interface: ISummarizationStrategy

	/// <summary>
	/// Defines the contract for a summarization strategy.
	/// </summary>
	public interface ISummarizationStrategy {
		Task<bool> NeedSummarizeAsync(CopilotSession session, CancellationToken cancellationToken);
		Task SummarizeAsync(CopilotSession session, CancellationToken cancellationToken);
	}

	#endregion

}
