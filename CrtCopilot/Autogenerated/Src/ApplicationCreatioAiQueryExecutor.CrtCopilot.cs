namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.FeatureToggling;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.Applications.Abstractions;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: ApplicationCreatioAiQueryExecutor

	/// <summary>
	/// Query executor for creation ai of application.
	/// </summary>
	[DefaultBinding(typeof(IEntityQueryExecutor), Name = "ApplicationCreatioAiQueryExecutor")]
	public class ApplicationCreatioAiQueryExecutor : BaseApplicationSchemaQueryExecutor<CopilotIntentSchema>
	{

		#region Constants: Private

		private const string UIdColumnName = "UId";
		private const string StatusColumnName = "Status";
		private const string TypeColumnName = "Type";
		private const string ModelColumnName = "Model";
		private const string ApiTypeValue = "API";
		private const string ChatTypeValue = "Chat";

		#endregion

		#region Constructors: Public

		public ApplicationCreatioAiQueryExecutor(UserConnection userConnection, IAppManager appManager)
			: base(userConnection, appManager) {
		}

		#endregion

		#region Properties: Private

		private CopilotIntentSchemaManager _copilotIntentSchemaManager;
		private CopilotIntentSchemaManager CopilotIntentSchemaManager =>
			_copilotIntentSchemaManager ?? (_copilotIntentSchemaManager =
				(CopilotIntentSchemaManager)UserConnection.GetSchemaManager(nameof(CopilotIntentSchemaManager)));

		private Dictionary<string, string> _statusNames;
		private Dictionary<string, string> StatusNames =>
			_statusNames ?? (_statusNames = new Dictionary<string, string>());

		private Dictionary<string, string> _llmModelNames;
		private Dictionary<string, string> LlmModelNames =>
			_llmModelNames ?? (_llmModelNames = new Dictionary<string, string>());

		#endregion

		#region Methods: Private

		private string GetLookupName(string lookupSchemaName, string code, Dictionary<string, string> cache) {
			if (string.IsNullOrEmpty(code)) {
				return code;
			}
			if (!cache.ContainsKey(code)) {
				var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, lookupSchemaName) {
					PrimaryQueryColumn = {
						IsAlwaysSelect = true
					},
					UseAdminRights = false
				};
				esq.AddColumn("Name");
				esq.AddColumn("Code");
				var entity = esq.GetEntityCollection(UserConnection)
					.FirstOrDefault(e => e.GetTypedColumnValue<string>("Code") == code);
				cache[code] = entity?.GetTypedColumnValue<string>("Name") ?? code;
			}
			return cache[code];
		}

		private string GetTypeColumnValue(CopilotIntentSchema instance) {
			switch (instance.Type) {
				case CopilotIntentType.Agent:
				case CopilotIntentType.WorkflowAgent:
					return instance.Type.ToString();
				default:
					return instance.Behavior.SkipForChat ? ApiTypeValue : ChatTypeValue;
			}
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// <inheritdoc />
		/// </summary>
		protected override string ResultEntitySchemaName => "ApplicationCreatioAi";

		/// <summary>
		/// <inheritdoc />
		/// </summary>
		protected override SchemaManager<CopilotIntentSchema> GetSchemaManager() => CopilotIntentSchemaManager;

		/// <summary>
		/// <inheritdoc />
		/// </summary>
		protected override void MapAdditionalColumns(Entity entity,
				ISchemaManagerItem<CopilotIntentSchema> schemaManagerItem) {
			var instance = schemaManagerItem.Instance;
			entity.SetColumnValue(UIdColumnName, instance.UId);
			string statusName = GetLookupName("CopilotIntentStatus", instance.Status.ToString(), StatusNames);
			entity.SetColumnValue(StatusColumnName, statusName);
			entity.SetColumnValue(TypeColumnName, GetTypeColumnValue(instance));
			string llmModelName = GetLookupName("LlmModel", instance.LlmModel, LlmModelNames);
			entity.SetColumnValue(ModelColumnName, llmModelName);
		}

		protected override IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> GetSchemaManagerItems(
				IEnumerable<Guid> appIds, IEnumerable<Guid> pkgIds) {
			var items = base.GetSchemaManagerItems(appIds, pkgIds);
			if (Features.GetIsDisabled<GenAIFeatures.UseAgenticProcesses>()) {
				return items.Where(item => item.Instance.Type != CopilotIntentType.WorkflowAgent);
			}
			return items;
		}

		#endregion

	}

	#endregion

}

