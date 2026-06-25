namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security;
	using Creatio.Copilot.Metadata;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Security;

	#region Class: CopilotIntentsStorage

	/// <summary>
	/// Storage for Copilot intents.
	/// </summary>
	[DefaultBinding(typeof(ICopilotIntentsStorage))]
	internal class CopilotIntentsStorage : ICopilotIntentsStorage
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private IEnumerable<CopilotIntentSchema> _systemIntents;
		private IIntentSchemaService _intentSchemaService;

		#endregion

		#region Constructors: Public

		public CopilotIntentsStorage(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Gets or sets the instance of <see cref="IIntentSchemaService"/>.
		/// </summary>
		/// <value>
		/// The <see cref="IIntentSchemaService"/> instance used to interact with skill schema service.
		/// </value>
		public IIntentSchemaService IntentSchemaService {
			get {
				if (_intentSchemaService != null) {
					return _intentSchemaService;
				}
				return _intentSchemaService = _userConnection.GetIntentSchemaService();
			}
			set => _intentSchemaService = value;
		}

		public IEnumerable<CopilotIntentSchema> SystemIntents => _systemIntents ??
			(_systemIntents = IntentSchemaService.FindSystemIntents()
				.Where(x => x.Status != CopilotIntentStatus.Deactivated && !x.Behavior.WorkflowOnly)
				.ToList());

		#endregion

		#region Methods: Private

		private IEnumerable<CopilotActionMetaItem> GetCurrentIntentActionsMetaItems(Guid? intentId) {
			if (intentId.IsNullOrEmpty()) {
				return new List<CopilotActionMetaItem>();
			}
			CopilotIntentSchema intent = FindSchemaByUId(intentId.Value);
			if (intent != null && intent?.Type != CopilotIntentType.System && !HasExecutionPermitted(intentId)) {
				LocalizableString ls = _userConnection.GetLocalizableString("NoIntentExecutionRight", nameof(CopilotIntentsStorage));
				throw new SecurityException(ls.Format(intent.Name));
			}
			if (intent != null && intent.Behavior.SkipForChat) {
				return new List<CopilotActionMetaItem>();
			}
			List<CopilotActionMetaItem> actions = intent?.Actions?.ToList();
			return actions ?? new List<CopilotActionMetaItem>();
		}

		#endregion

		#region Methods: Public

		public IEnumerable<CopilotIntentSchema> FindAgents() {
			return IntentSchemaService.FindAgents();
		}

		public CopilotIntentSchema FindSchemaByName(string name) {
			return IntentSchemaService.FindSchemaByName(name);
		}

		public bool HasExecutionPermitted(Guid? intentId) {
			if (!intentId.HasValue) {
				return false;
			}
			return IntentSchemaService.HasOperationPermitted(UserSchemaOperationRights.Execute, intentId.Value);
		}

		public string GetIntentCaptionByIntentId(Guid? rootIntentId) {
			if (!rootIntentId.HasValue || rootIntentId == Guid.Empty) {
				return string.Empty;
			}
			return IntentSchemaService.FindSchemaByUId(rootIntentId.Value)?.Caption ?? string.Empty;
		}

		public IEnumerable<CopilotIntentSchema> GetSubIntents(Guid intentUid) {
			return IntentSchemaService.GetSubIntents(intentUid);
		}

		public IEnumerable<CopilotActionMetaItem> GetSystemIntentActionsMetaItems() {
			var actionMetaItems = new List<CopilotActionMetaItem>();
			foreach (CopilotIntentSchema intent in SystemIntents) {
				if (intent.Actions != null && intent.Actions.IsNotNullOrEmpty()) {
					actionMetaItems.AddRange(intent.Actions);
				}
			}
			return actionMetaItems;
		}

		public CopilotIntentSchema FindSchemaByUId(Guid schemaUid) {
			return IntentSchemaService.FindSchemaByUId(schemaUid);
		}

		public IEnumerable<CopilotIntentSchema> GetSystemSubIntents(Guid? intentId) {
			if (intentId.IsNullOrEmpty()) {
				return Enumerable.Empty<CopilotIntentSchema>();
			}
			IEnumerable<CopilotIntentSchema> systemSubIntents = IntentSchemaService.GetSystemSubIntents(intentId.Value)
				.Where(intent => !intent.Behavior.SkipForChat && intent.Status != CopilotIntentStatus.Deactivated);
			return systemSubIntents;
		}

		public IEnumerable<CopilotActionMetaItem> GetActionsFromIntentSystemSubIntents(Guid? intentId) {
			var actions = new List<CopilotActionMetaItem>();
			IEnumerable<CopilotIntentSchema> systemSubIntents = GetSystemSubIntents(intentId);
			foreach (CopilotIntentSchema systemSubIntent in systemSubIntents) {
				if (systemSubIntent.Actions != null && systemSubIntent.Actions.IsNotNullOrEmpty()) {
					actions.AddRange(systemSubIntent.Actions);
				}
			}
			return actions;
		}

		public IEnumerable<CopilotActionMetaItem> GetActionsMetaItemsByIntent(CopilotIntentSchema intent) {
			if (intent != null && intent.Type != CopilotIntentType.System && !HasExecutionPermitted(intent.UId)) {
				LocalizableString ls = _userConnection.GetLocalizableString("NoIntentExecutionRight", nameof(CopilotIntentsStorage));
				throw new SecurityException(ls.Format(intent.Name));
			}
			List<CopilotActionMetaItem> actions = intent?.Actions?.ToList();
			return actions ?? new List<CopilotActionMetaItem>();
		}

		public IEnumerable<CopilotActionMetaItem> GetSystemActionsMetaItemsForUsePageContext() {
			var actionsForContext = new [] { "RetrieveEntityData", "GetContextConstant" };
			List<CopilotActionMetaItem> actions = GetSystemIntentActionsMetaItems()
				.Where(a => actionsForContext.Contains(a.Name))
				.ToList();
			return actions;
		}

		public IEnumerable<CopilotIntentSchema> FindAgentsForChat(Guid? excludeIntentId) {
			IEnumerable<CopilotIntentSchema> items = FindAgents()
				.Where(agent => !agent.Behavior.SkipForChat);
			if (excludeIntentId != null) {
				items = items.Where(x => x.UId != excludeIntentId);
			}
			return items;
		}

		public IEnumerable<CopilotActionMetaItem> GetActionsMetaItemsByIntent(Guid? intentId) {
			return intentId.HasValue ? GetCurrentIntentActionsMetaItems(intentId) : Enumerable.Empty<CopilotActionMetaItem>();
		}

		public IEnumerable<CopilotIntentSchema> GetSubIntentsForChat(Guid? intentId, Guid? excludeIntentId) {
			if (!intentId.HasValue) {
				return Enumerable.Empty<CopilotIntentSchema>();
			}
			IEnumerable<CopilotIntentSchema> items = IntentSchemaService.GetSubIntents(intentId.Value)
				.Where(intent => !intent.Behavior.SkipForChat);
			if (excludeIntentId != null) {
				items = items.Where(x => x.UId != excludeIntentId);
			}
			return items;
		}
		#endregion

	}

	#endregion

}
