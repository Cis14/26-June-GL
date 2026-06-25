namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using Creatio.Copilot.Metadata;

	#region Interface: ICopilotToolContextBuilder

	internal interface ICopilotToolContextBuilder
	{

		#region Methods: Public

		CopilotToolContext BuildChatSessionToolContext(CopilotSession session);

		CopilotToolContext BuildApiSkillToolContext(CopilotSession session);

		CopilotToolContext GetToolContext(IEnumerable<CopilotActionMetaItem> actionItems, IEnumerable<CopilotIntentSchema> intents = null);

		#endregion

	}

	#endregion

}
