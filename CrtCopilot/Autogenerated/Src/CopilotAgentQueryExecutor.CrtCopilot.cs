 namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.FeatureToggling;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	
	#region Class: CopilotAgentQueryExecutor

	/// <summary>
	/// Query executor that returns only Copilot intents of type <see cref="CopilotIntentType.Agent"/> and,
	/// when the <see cref="GenAIFeatures.UseAgenticProcesses"/> feature is enabled, also
	/// <see cref="CopilotIntentType.WorkflowAgent"/>.
	/// </summary>
	[DefaultBinding(typeof(IEntityQueryExecutor), Name = "CopilotAgentQueryExecutor")]
	public class CopilotAgentQueryExecutor : BaseCopilotIntentQueryExecutor
	{

		#region Constructors: Public

		/// <summary>
		/// Creates a new instance of the <see cref="CopilotAgentQueryExecutor"/> class.
		/// </summary>
		/// <param name="userConnection"></param>
		public CopilotAgentQueryExecutor(UserConnection userConnection)
			: base(userConnection) {
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc/>
		protected override IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> GetIntentManagerItems() {
			IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> items = base.GetIntentManagerItems();
			bool isEnabled = Features.GetIsEnabled<GenAIFeatures.UseAgenticProcesses>();
			return items.Where(item => item.Instance.Type == CopilotIntentType.Agent ||
				(item.Instance.Type == CopilotIntentType.WorkflowAgent && isEnabled));
		}

		#endregion

	}

	#endregion

}

