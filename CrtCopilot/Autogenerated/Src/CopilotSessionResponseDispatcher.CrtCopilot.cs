namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using Terrasoft.Core.Factories;

	[DefaultBinding(typeof(ICopilotSessionResponseDispatcher))]
	public class CopilotSessionResponseDispatcher : ICopilotSessionResponseDispatcher
	{

		#region Fields: Private

		private readonly IEnumerable<ICopilotSessionResponseHandler> _handlers;

		#endregion

		#region Constructors: Public

		public CopilotSessionResponseDispatcher(IEnumerable<ICopilotSessionResponseHandler> handlers) {
			_handlers = handlers;
		}

		#endregion

		#region Methods: Public

		public async Task DispatchAsync(CopilotSession session, CancellationToken ct = default) {
			foreach (var handler in _handlers) {
				if (await handler.CanHandleAsync(session, ct)) {
					await handler.HandleAsync(session, ct);
				}
			}
		}

		#endregion

	}
}

