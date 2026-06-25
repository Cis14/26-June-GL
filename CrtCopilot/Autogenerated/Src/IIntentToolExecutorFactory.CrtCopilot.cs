namespace Creatio.Copilot
{
	using System;

	/// <summary>
	/// Defines a factory interface for creating instances of IntentToolExecutor.
	/// </summary>
	public interface IIntentToolExecutorFactory
	{
		/// <summary>
		/// Creates a new instance of IntentToolExecutor using the specified CopilotIntentSchema.
		/// </summary>
		/// <param name="copilotIntentSchema">Copilot intent schema.</param>
		/// <returns></returns>
		[Obsolete]
		IntentToolExecutor Create(CopilotIntentSchema copilotIntentSchema);

		/// <summary>
		/// Creates trigger.
		/// </summary>
		/// <param name="copilotIntentSchema">Copilot intent schema.</param>
		/// <returns>Tool execution trigger.</returns>
		IToolExecutionTrigger CreateTrigger(CopilotIntentSchema copilotIntentSchema);

		/// <summary>
		/// Creates executor.
		/// </summary>
		/// <param name="copilotIntentSchema">Copilot intent schema.</param>
		/// <returns>Tool executor.</returns>
		IToolExecutor CreateExecutor(CopilotIntentSchema copilotIntentSchema);
	}
}

