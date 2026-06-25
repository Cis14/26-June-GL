namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;

	#region Class: CopilotToolContext

	internal class CopilotToolContext
	{

		#region Constructors: Public

		public CopilotToolContext(
				IList<ToolDefinition> tools,
				Dictionary<string, IToolExecutor> mapping,
				IEnumerable<CopilotIntentSchema> intents) {
			Tools = tools;
			Mapping = mapping;
			Intents = intents ?? Enumerable.Empty<CopilotIntentSchema>();
		}

		public CopilotToolContext(IList<ToolDefinition> tools, Dictionary<string, IToolExecutor> mapping) 
				: this(tools, mapping, null) {
		}

		#endregion

		#region Properties: Public

		public IList<ToolDefinition> Tools { get; }
		public Dictionary<string, IToolExecutor> Mapping { get; }
		public IEnumerable<CopilotIntentSchema> Intents { get; }

		#endregion

	}

	#endregion

}
