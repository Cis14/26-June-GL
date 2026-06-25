 namespace Creatio.Copilot
{
	using System;
	using Creatio.Copilot.Metadata;

	#region Class: AIIntentFilterContext

	public class AIIntentFilterContext
	{

		#region Constructors: Public

		public AIIntentFilterContext(CopilotIntentSchema intent) {
			Intent = intent;
			Name = intent?.Name;
			UId = intent?.UId ?? System.Guid.Empty;
			Type = intent?.Type;
		}

		#endregion

		#region Properties: Public

		public string Name { get; }

		public Guid UId { get; }

		public CopilotIntentType? Type { get; }

		public CopilotIntentSchema Intent { get; }

		#endregion
	}

	#endregion

}

