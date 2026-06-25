namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Common.Logging;
	using Creatio.Copilot.Metadata;
	using Creatio.FeatureToggling;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Store;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;

	#region Class: CopilotToolContextBuilder

	/// <summary>
	/// Builder for tool context.
	/// </summary>
	[DefaultBinding(typeof(ICopilotToolContextBuilder))]
	internal class CopilotToolContextBuilder : ICopilotToolContextBuilder
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private readonly ICopilotIntentsStorage _intentsStorage;
		private readonly IIntentToolExecutorFactory _intentToolExecutorFactory;
		private readonly ICopilotMsgChannelSender _msgChannelSender;
		private readonly ICopilotBoundedAgentUtils _boundedAgentUtils;
		private readonly IToolFilterResolver _toolFilterResolver;
		private IIntentSchemaService _intentSchemaService;
		private readonly ILog _log = LogManager.GetLogger("Copilot");

		#endregion

		#region Constructors: Public

		public CopilotToolContextBuilder(UserConnection userConnection, ICopilotIntentsStorage intentsStorage,
				IIntentToolExecutorFactory intentToolExecutorFactory, ICopilotMsgChannelSender msgChannelSender,
				ICopilotBoundedAgentUtils boundedAgentUtils, IToolFilterResolver toolFilterResolver) {
			_userConnection = userConnection;
			_intentsStorage = intentsStorage;
			_intentToolExecutorFactory = intentToolExecutorFactory;
			_msgChannelSender = msgChannelSender;
			_boundedAgentUtils = boundedAgentUtils;
			_toolFilterResolver = toolFilterResolver;
		}

		#endregion

		#region Properties: Public

		public IIntentSchemaService IntentSchemaService {
			get => _intentSchemaService ?? (_intentSchemaService = _userConnection.GetIntentSchemaService());
			set => _intentSchemaService = value; // Used for tests to inject mocked IIntentSchemaService
		}

		#endregion

		#region Properties: Private

		private ICacheStore _cacheStore;
		private ICacheStore CacheStore =>
			_cacheStore ?? (_cacheStore = _userConnection.SessionCache.WithLocalCaching(nameof(CopilotToolContextBuilder)));

		private HashSet<string> _systemActionNames;
		private HashSet<string> SystemActionNames {
			get {
				if (_systemActionNames != null) {
					return _systemActionNames;
				}
				_systemActionNames = CacheStore.GetValue<HashSet<string>>(nameof(SystemActionNames));
				if (_systemActionNames != null) {
					return _systemActionNames;
				}
				_systemActionNames = new HashSet<string>(LoadSystemActionNames());
				CacheStore[nameof(SystemActionNames)] = _systemActionNames;
				return _systemActionNames;
			}
		}

		#endregion

		#region Methods: Private

		private static string AppendAlternativeName(LocalizableString description, string name, string alternativeName) {
			if (Features.GetIsDisabled<Terrasoft.Configuration.GenAI.GenAIFeatures.AddCaptionToDescription>() ||
					string.IsNullOrWhiteSpace(alternativeName) ||
					string.Equals(name, alternativeName, StringComparison.InvariantCultureIgnoreCase)) {
				return description?.Value;
			}
			string separator = string.Empty;
			string value = description == null ? string.Empty : description.Value;
			if (!string.IsNullOrWhiteSpace(value)) {
				separator = value.EndsWith(".") ? " " : ". ";
			}
			return $"{value}{separator}Alternative name: [{alternativeName}]";
		}

		private static ToolDefinition GetToolDefinition(CopilotIntentSchema intentSchema) {
			string intentTypeCaption = intentSchema.Type == CopilotIntentType.Agent ? "agent" : "skill";
			var toolName = $"{intentSchema.Name}_{intentTypeCaption}";
			string description;
			if (!string.IsNullOrWhiteSpace(description = intentSchema.IntentDescription) ||
				!string.IsNullOrWhiteSpace(description = intentSchema.Description)) {
				description = AppendAlternativeName(description, intentSchema.Name, intentSchema.Caption);
			} else {
				description = intentSchema.Caption;
			}
			var functionDefinitionBuilder = new FunctionDefinitionBuilder(toolName, description);
			if (Features.GetIsEnabled<Terrasoft.Configuration.GenAI.GenAIFeatures.GenerateIntentSelectionReason>()) {
				functionDefinitionBuilder.AddParameter("Reason", PropertyDefinition.DefineString(
					$"Specify the reason(s) why exactly this {intentTypeCaption} was chosen - not the others"));
			}
			FunctionDefinition functionDefinition = functionDefinitionBuilder.Validate().Build();
			var tool = new ToolDefinition {
				Function = functionDefinition
			};
			return tool;
		}

		private IEnumerable<string> LoadSystemActionNames() {
			IEnumerable<CopilotIntentSchema> systemIntents = IntentSchemaService.FindSystemIntents()
				.Where(x => x.Status != CopilotIntentStatus.Deactivated);
			return systemIntents?.SelectMany(intent => intent.Actions)
				.Select(systemAction => systemAction.Name);
		}

		private bool TryGetToolDefinition(CopilotActionMetaItem actionMetaItem, out ToolDefinition toolDefinition) {
			CopilotActionDescriptor actionDescriptor = actionMetaItem.Descriptor;
			toolDefinition = null;
			if (!actionDescriptor.IsEnabled) {
				_log.Warn(
					$"Action descriptor is disabled. Action name: {actionMetaItem.Name}, UId: {actionMetaItem.UId}");
				return false;
			}
			string toolName = actionDescriptor.Name;
			if (!SystemActionNames.Contains(toolName)) {
				toolName = $"{toolName}_action";
			}
			string description = AppendAlternativeName(actionDescriptor.Description, actionDescriptor.Name,
				actionDescriptor.Caption);
			var functionDefinitionBuilder = new FunctionDefinitionBuilder(toolName, description);
			IEnumerable<ICopilotParameterMetaInfo> parameters = actionDescriptor.Parameters
				.Where(param => param.Direction == ParameterDirection.Input);
			foreach (ICopilotParameterMetaInfo parameterMetaInfo in parameters) {
				functionDefinitionBuilder = functionDefinitionBuilder.AddParameter(parameterMetaInfo.Name,
					CopilotToolParamHelper.GetToolParam(parameterMetaInfo), parameterMetaInfo.IsRequired);
			}
			FunctionDefinition functionDefinition = functionDefinitionBuilder.Validate().Build();
			toolDefinition = new ToolDefinition {
				Function = functionDefinition
			};
			return true;
		}

		private void AddPageContextActions(List<CopilotActionMetaItem> actions) {
			var existingNames = new HashSet<string>(
				actions.Select(a => a.Name),
				StringComparer.OrdinalIgnoreCase);
			foreach (CopilotActionMetaItem action in _intentsStorage.GetSystemActionsMetaItemsForUsePageContext()) {
				if (existingNames.Add(action.Name)) {
					actions.Add(action);
				}
			}
		}

		private List<CopilotActionMetaItem> ExcludeConfirmationRequiredActionsWhenNoRootSession (
				CopilotSession session, List<CopilotActionMetaItem> actions) {
			if (session?.RootSessionId.HasValue == true) {
				return actions;
			}
			return actions.Where(action => !action.IsConfirmRequired).ToList();
		}

		private IEnumerable<IAIToolFilter> GetResolvedFilters(CopilotSession session) {
			if (session?.FilterDescriptors == null || !session.FilterDescriptors.Any()) {
				return Enumerable.Empty<IAIToolFilter>();
			}
			return _toolFilterResolver.Resolve(session.FilterDescriptors);
		}

		private bool PassesAllIntentFilters(CopilotSession session, CopilotIntentSchema intent) {
			IEnumerable<IAIToolFilter> filters = GetResolvedFilters(session);
			var context = new AIIntentFilterContext(intent);
			return filters.All(filter => filter?.ShouldIncludeIntent(context) != false);
		}

		private IEnumerable<CopilotActionMetaItem> GetFilteredSystemIntentActions(CopilotSession session) {
			if (Features.GetIsDisabled("GenAIFeatures.EnableIntentFiltering")) {
				return _intentsStorage.GetSystemIntentActionsMetaItems();
			}
			IEnumerable<CopilotIntentSchema> systemIntents = _intentsStorage.SystemIntents
				.Where(intent => PassesAllIntentFilters(session, intent));
			return systemIntents.SelectMany(intent => _intentsStorage.GetActionsMetaItemsByIntent(intent));
		}

		#endregion

		#region Methods: Public

		public CopilotToolContext BuildChatSessionToolContext(CopilotSession session) {
			Guid? rootIntentId = session.RootIntentId;
			Guid? currentIntentId = session.CurrentIntentId;
			var intents = new List<CopilotIntentSchema>();
			IEnumerable<CopilotIntentSchema> agents = _boundedAgentUtils.IsAgentBounded(session) && rootIntentId.HasValue
				? IntentSchemaService.FindAgents().Where(a => a.UId == rootIntentId)
				: _intentsStorage.FindAgentsForChat(rootIntentId);
			intents.AddRange(agents);
			var actionMetaItems = new HashSet<CopilotActionMetaItem>();
			if (rootIntentId.HasValue) {
				IEnumerable<CopilotIntentSchema> skills = _intentsStorage.GetSubIntentsForChat(rootIntentId, currentIntentId);
				intents.AddRange(skills);
				IEnumerable<CopilotActionMetaItem> agentActions = _intentsStorage.GetActionsMetaItemsByIntent(rootIntentId);
				IEnumerable<CopilotActionMetaItem> skillActions = currentIntentId != rootIntentId
					? _intentsStorage.GetActionsMetaItemsByIntent(currentIntentId)
					: new List<CopilotActionMetaItem>();
				IEnumerable<CopilotActionMetaItem> systemActions = GetFilteredSystemIntentActions(session);
				actionMetaItems.AddRange(agentActions);
				actionMetaItems.AddRange(skillActions);
				actionMetaItems.AddRange(systemActions);
			}
			intents = intents.Where(intent => PassesAllIntentFilters(session, intent)).ToList();
			CopilotToolContext toolContext = GetToolContext(actionMetaItems, intents);
			return toolContext;
		}

		public CopilotToolContext BuildApiSkillToolContext(CopilotSession session) {
			var actions = new List<CopilotActionMetaItem>();
			if (Features.GetIsEnabled<GenAIFeatures.UseAgenticProcesses>() && session.CurrentIntentId.HasValue) {
				CopilotIntentSchema intent = _intentsStorage.FindSchemaByUId(session.CurrentIntentId.Value);
				actions.AddRange(_intentsStorage.GetActionsMetaItemsByIntent(intent));
				actions.AddRange(_intentsStorage.GetActionsFromIntentSystemSubIntents(session.CurrentIntentId));
				if (intent.Behavior.UsePageContext) {
					AddPageContextActions(actions);
				}
			} else {
				actions.AddRange(GetFilteredSystemIntentActions(session));
			}
			actions = ExcludeConfirmationRequiredActionsWhenNoRootSession(session, actions);
			CopilotToolContext toolContext = GetToolContext(actions);
			return toolContext;
		}

		public CopilotToolContext GetToolContext(
			IEnumerable<CopilotActionMetaItem> actionItems,
			IEnumerable<CopilotIntentSchema> intents = null) {
			var tools = new List<ToolDefinition>();
			var mapping = new Dictionary<string, IToolExecutor>();
			foreach (CopilotIntentSchema intent in intents ?? Enumerable.Empty<CopilotIntentSchema>()) {
				ToolDefinition toolDefinition = GetToolDefinition(intent);
				mapping[toolDefinition.Function.Name] = _intentToolExecutorFactory.CreateExecutor(intent);
				tools.Add(toolDefinition);
			}
			foreach (CopilotActionMetaItem actionItem in actionItems) {
				if (TryGetToolDefinition(actionItem, out ToolDefinition toolDefinition) == false) {
					continue;
				}
				mapping[toolDefinition.Function.Name] = new CopilotActionToolExecutor(actionItem, _userConnection,
					_msgChannelSender);
				tools.Add(toolDefinition);
			}
			return new CopilotToolContext(tools, mapping, intents);
		}

		#endregion

	}

	#endregion

}

