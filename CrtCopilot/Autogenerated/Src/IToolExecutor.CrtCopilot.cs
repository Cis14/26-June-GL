namespace Creatio.Copilot
{
	using System.Collections.Generic;

	/// <summary>
	/// Interface for executing Copilot tools.
	/// </summary>
	public interface IToolExecutor
	{

		#region Properties: Public

		/// <summary>
		/// Gets a value indicating whether confirmation is required before proceeding with the action.
		/// </summary>
		bool IsConfirmationRequired { get; }

		#endregion

		#region Methods: Public

		/// <summary>
		/// Executes tool called by Copilot.
		/// </summary>
		/// <param name="toolCallId">Tool identifier.</param>
		/// <param name="arguments">Tool's arguments.</param>
		/// <param name="session">Copilot session.</param>
		/// <returns></returns>
		List<CopilotMessage> Execute(string toolCallId, Dictionary<string, object> arguments, CopilotSession session);

		#endregion

	}
} 
