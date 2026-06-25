namespace Creatio.Copilot
{
	using System.Threading;
	using System.Threading.Tasks;

	public interface ICopilotSessionResponseHandler
	{
		Task<bool> CanHandleAsync(CopilotSession session, CancellationToken cancellationToken = default);
		Task HandleAsync(CopilotSession session, CancellationToken cancellationToken = default);
	}
}

