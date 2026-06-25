namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion.Responses;

	#region Interface: ICopilotToolProcessor

	internal interface ICopilotToolProcessor
	{

		#region Methods: Public

		List<CopilotMessage> HandleToolCalls(ChatCompletionResponse completionResponse, CopilotSession session,
			CopilotToolContext toolContext);

		#endregion

	}

	#endregion

}

