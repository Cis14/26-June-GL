namespace Creatio.Copilot
{
	using System.Threading;
	using System.Threading.Tasks;
	using Creatio.Copilot;

	public interface ICopilotSessionResponseDispatcher
	{
		Task DispatchAsync(CopilotSession session, CancellationToken ct = default);
	}
}

