namespace Creatio.Copilot
{
	using System;
	using Common.Logging;
	using Creatio.Copilot.Metadata;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: BaseCopilotSubSkillQueryExecutor

	public abstract class
		BaseCopilotSubSkillQueryExecutor : BaseCopilotSchemaManagerQueryExecutor<CopilotSubIntentMetaItem>
	{
		private readonly ILog _log = LogManager.GetLogger("Copilot");
		private readonly string _sourceSchemaName;

		#region Constructors: Public

		protected BaseCopilotSubSkillQueryExecutor(UserConnection userConnection, string schemaName, string cacheKey,
			string sourceSchemaName)
			: base(userConnection, schemaName, cacheKey, "SubSkill") {
			_sourceSchemaName = sourceSchemaName;
		}

		#endregion

		#region Methods: Protected

		protected override MetaItemCollection<CopilotSubIntentMetaItem> GetMetaItemCollectionFromSchema(
			CopilotIntentSchema schema) {
			return schema.SubIntents;
		}

		protected override Entity GetMetaItemEntityFromSchema(CopilotIntentSchema intentSchema,
			CopilotSubIntentMetaItem metaItem) {
			ISchemaManagerItem<CopilotIntentSchema> intent = CopilotIntentSchemaManager.FindItemByUId(metaItem.UId);
			if (intent == null) {
				_log.Error($"Intent '{metaItem.Name}' with UId {metaItem.UId} was not found.");
				return null;
			}
			var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, _sourceSchemaName);
			esq.AddAllSchemaColumns();
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Id", intent.UId));
			var result = esq.GetEntityCollection(UserConnection);
			Entity entity = EntitySchema.CreateEntity(UserConnection);
			entity.LoadColumnValue("Id", result[0].GetTypedColumnValue<Guid>("Id"));
			entity.LoadColumnValue("Code", result[0].GetTypedColumnValue<string>("Code"));
			entity.LoadColumnValue("Name", result[0].GetTypedColumnValue<string>("Name"));
			entity.LoadColumnValue("Prompt", result[0].GetTypedColumnValue<string>("Prompt"));
			entity.LoadColumnValue("Description", result[0].GetTypedColumnValue<string>("Description"));
			entity.LoadColumnValue("CreatedById", result[0].GetTypedColumnValue<Guid>("CreatedById"));
			entity.LoadColumnValue("CreatedByName", result[0].GetTypedColumnValue<string>("CreatedByName"));
			entity.LoadColumnValue("ModifiedOn", result[0].GetTypedColumnValue<DateTime>("ModifiedOn"));
			entity.LoadColumnValue("StatusId", result[0].GetTypedColumnValue<Guid>("StatusId"));
			entity.LoadColumnValue("StatusName", result[0].GetTypedColumnValue<string>("StatusName"));
			entity.LoadColumnValue("TypeId", result[0].GetTypedColumnValue<Guid>("TypeId"));
			entity.LoadColumnValue("TypePrimaryColor", result[0].GetTypedColumnValue<string>("TypePrimaryColor"));
			entity.LoadColumnValue("TypeName", result[0].GetTypedColumnValue<string>("TypeName"));
			entity.LoadColumnValue("ModeId", result[0].GetTypedColumnValue<Guid>("ModeId"));
			entity.LoadColumnValue("ModeName", result[0].GetTypedColumnValue<string>("ModeName"));
			return entity;
		}

		#endregion

	}

	#endregion

}

