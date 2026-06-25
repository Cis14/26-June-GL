namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: CopilotSysCapabilityQueryExecutor

	[DefaultBinding(typeof(IEntityQueryExecutor), Name = "CopilotSysCapabilityQueryExecutor")]
	public class CopilotSysCapabilityQueryExecutor : CopilotIntentQueryExecutor
	{

		#region Constructors: Public

		public CopilotSysCapabilityQueryExecutor(UserConnection userConnection)
			: base(userConnection) { }

		#endregion

		#region Methods: Protected

		protected override IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> GetIntentManagerItems() {
			IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> items = CopilotIntentSchemaManager.GetItems();
			return items.Where(item => item.Instance.Type == CopilotIntentType.System);
		}

		#endregion

	}

	#endregion

}

