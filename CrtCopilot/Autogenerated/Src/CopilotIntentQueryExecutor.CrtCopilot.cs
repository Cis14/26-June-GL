namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.Copilot.Metadata;
	using Creatio.FeatureToggling;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	
	#region Class: CopilotIntentQueryExecutor

	[DefaultBinding(typeof(IEntityQueryExecutor), Name = "CopilotIntentQueryExecutor")]
	public class CopilotIntentQueryExecutor : BaseCopilotIntentQueryExecutor
	{
		#region Constructors: Public

		public CopilotIntentQueryExecutor(UserConnection userConnection)
			: base(userConnection) { }

		#endregion

		#region Methods: Private

		private Entity GetIntentMode(CopilotIntentBehavior behavior) {
			return GetCopilotIntentLookupESQ("CopilotIntentMode").GetEntityCollection(UserConnection)
				.FirstOrDefault(mode =>
					mode.GetTypedColumnValue<string>("Code") == (behavior.SkipForChat ? "API" : "Chat"));
		}

		#endregion

		#region Methods: Protected

		protected override Entity GetIntentEntityFromManagerItem(
			ISchemaManagerItem<CopilotIntentSchema> schemaManagerItem,
			Entity intentEntity) {
			CopilotIntentSchema intentSchema = schemaManagerItem.Instance;
			Entity entity = base.GetIntentEntityFromManagerItem(schemaManagerItem,
				intentEntity);
			Entity mode = GetIntentMode(intentSchema.Behavior);
			if (mode != null) {
				entity.LoadColumnValue("ModeId",
					mode.GetTypedColumnValue<Guid>("Id"));
				entity.LoadColumnValue("ModeName",
					mode.GetTypedColumnValue<string>("Name"));
			}
			return entity;
		}

		protected override IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> GetIntentManagerItems() {
			IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> items = base.GetIntentManagerItems();
			Func<ISchemaManagerItem<CopilotIntentSchema>, bool> isSkill = item =>
				item.Instance.Type == CopilotIntentType.Skill;
			return Features.GetIsEnabled<Terrasoft.Configuration.GenAI.GenAIFeatures.ShowSystemIntent>()
				? items.OrderBy(isSkill)
				: items.Where(isSkill);
		}

		#endregion

	}

	#endregion

}

