namespace Creatio.Copilot
{
	using System.Collections.Generic;

	#region Interface: IIntentPromptBuilder

	/// <summary>
	/// Provides an abstraction for generating prompt strings for one or many copilot intents.
	/// </summary>
	internal interface IIntentPromptBuilder
	{

		#region Methods: Internal

		/// <summary>
		/// Generates prompt messages for the specified <paramref name="intent"/> using supplied
		/// <paramref name="parameterValues"/>, optional <paramref name="additionalPromptText"/>
		/// and parameter inclusion options.
		/// </summary>
		/// <param name="parameterValues">Dictionary of input parameter names to values. May be <c>null</c>
		/// or empty.</param>
		/// <param name="additionalPromptText">Optional extra user / system instruction appended after the base
		/// intent prompt.</param>
		/// <param name="intent">The intent schema whose template and parameter metadata define
		/// the prompt structure.</param>
		/// <param name="warnings">A collection that will receive warning messages (e.g. missing or extra parameters).
		/// Never <c>null</c>.</param>
		/// <param name="includeOutputParametersInPrompt">When true, output parameter definitions
		/// are appended to guide model output.</param>
		/// <returns>The fully composed list of prompt messages.</returns>
		List<CopilotMessage> GenerateIntentPromptMessages(IDictionary<string, object> parameterValues, string additionalPromptText,
			CopilotIntentSchema intent, IList<string> warnings, bool includeOutputParametersInPrompt);

		/// <summary>
		/// Generates a prompt for the specified <paramref name="intent"/> using the provided
		/// <paramref name="parameterValues"/> dictionary.
		/// </summary>
		/// <param name="intent">Intent schema to format.</param>
		/// <param name="parameterValues">Variable values dictionary reused for the intent template.</param>
		/// <returns>The formatted prompt text.</returns>
		string GenerateIntentPrompt(CopilotIntentSchema intent, IDictionary<string, object> parameterValues);

		#endregion

	}

	#endregion

}

