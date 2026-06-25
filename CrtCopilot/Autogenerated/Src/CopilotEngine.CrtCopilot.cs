namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using Creatio.Copilot.Actions;
	using Creatio.FeatureToggling;
	using global::Common.Logging;
	using Terrasoft.Common.Threading;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Common;

	[DefaultBinding(typeof(ICopilotEngine))]
	internal class CopilotEngine : ICopilotEngine
	{

		#region Constants: Private

		private const string CanRunCopilotOperation = "CanRunCreatioAI";
		private const string CanRunCopilotApiOperation = "CanRunCreatioAIApi";

		#endregion

		#region Fields: Private

		private readonly ILog _log = LogManager.GetLogger("Copilot");
		private readonly UserConnection _userConnection;
		private readonly ICopilotSessionManager _sessionManager;
		private readonly ICopilotMsgChannelSender _copilotMsgChannelSender;
		private readonly ICopilotApiSkillExecutor _apiSkillExecutor;
		private readonly ICopilotChatExecutor _chatExecutor;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotEngine"/>
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="copilotSessionManager">Copilot session manager.</param>
		/// <param name="copilotMsgChannelSender">Copilot message sender.</param>\
		/// <param name="apiSkillExecutor">Copilot api skill executor.</param>
		/// <param name="chatExecutor">Copilot chat executor.</param>
		public CopilotEngine(UserConnection userConnection, ICopilotSessionManager copilotSessionManager,
				ICopilotMsgChannelSender copilotMsgChannelSender, ICopilotApiSkillExecutor apiSkillExecutor,
				ICopilotChatExecutor chatExecutor) {
			_userConnection = userConnection;
			_sessionManager = copilotSessionManager;
			_copilotMsgChannelSender = copilotMsgChannelSender;
			_apiSkillExecutor = apiSkillExecutor;
			_chatExecutor = chatExecutor;
		}

		#endregion

		#region Properties: Internal

		private IIntentSchemaService _intentSchemaService;

		/// <summary>
		/// Gets or sets the instance of <see cref="IIntentSchemaService"/>.
		/// </summary>
		/// <value>
		/// The <see cref="IIntentSchemaService"/> instance used to interact with skill schema service.
		/// </value>
		internal IIntentSchemaService IntentSchemaService {
			get {
				if (_intentSchemaService != null) {
					return _intentSchemaService;
				}
				return _intentSchemaService = _userConnection.GetIntentSchemaService();
			}
			set => _intentSchemaService = value;
		}

		#endregion

		#region Methods: Private

		private static IEnumerable<string> FilterActiveIntents(string[] names,
				IEnumerable<CopilotIntentSchema> activeIntents) {
			HashSet<string> allActiveIntentNames = activeIntents.Select(intent => intent.Name.ToLower()).ToHashSet();
			return names.Where(name => allActiveIntentNames.Contains(name.ToLower()));
		}

		private bool CanRunCopilotApi() {
			if (Features.GetIsDisabled<GenAIFeatures.EnableStandaloneApi>()) {
				return false;
			}
			return _userConnection.DBSecurityEngine.GetCanExecuteOperation(CanRunCopilotApiOperation);
		}

		private bool CanUseCopilotChat() {
			return _userConnection.DBSecurityEngine.GetCanExecuteOperation(CanRunCopilotOperation);
		}

		private IEnumerable<CopilotIntentSchema> FindSkillsForChat(Guid? excludeIntentId) {
			IEnumerable<CopilotIntentSchema> items = IntentSchemaService.FindSkills()
				.Where(intent => !intent.Behavior.SkipForChat);
			if (excludeIntentId != null) {
				items = items.Where(x => x.UId != excludeIntentId);
			}
			return items;
		}

		private IEnumerable<CopilotIntentSchema> FindSkillsForApi() {
			return IntentSchemaService.FindSkills().Where(intent => intent.Behavior.SkipForChat);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public CopilotChatPart SendUserMessage(string content, Guid? copilotSessionId = null,
				CopilotContext copilotContext = null, CopilotSendMessageOptions options = null) {
			return _chatExecutor.SendSession(content, copilotSessionId, copilotContext, options);
		}

		/// <inheritdoc/>
		public void CompleteAction(Guid copilotSessionId, string actionInstanceUId,
				CopilotActionExecutionResult result) {
			CopilotSession session = _sessionManager.FindById(copilotSessionId);
			if (session?.State != CopilotSessionState.Active) {
				return;
			}
			bool hasPendingAsyncMessage = session.Messages.Any(m =>
				m.IsPendingAsyncAction() && m.ToolCallId == actionInstanceUId);
			if (hasPendingAsyncMessage) {
				_apiSkillExecutor.CompleteAction(session, actionInstanceUId, result);
				return;
			}
			if (session.IsTransient) {
				return;
			}
			try {
				string resultContent = result.Status == CopilotActionExecutionStatus.Completed
					? result.Response ?? "Ok"
					: result.ErrorMessage ?? "Unknown error occurred";
				List<CopilotMessage> toolMessages = session.CreateToolCallMessages(actionInstanceUId, resultContent);
				_chatExecutor.HandleToolCallsCompletedAndSendSession(toolMessages, session);
			} catch (Exception e) {
				_log.Error(e);
				throw;
			} finally {
				_copilotMsgChannelSender.SendSessionProgress(CopilotSessionProgress.Create(_userConnection, session,
					CopilotSessionProgressStates.WaitingForUserMessage), session.UserId);
			}
		}

		/// <inheritdoc/>
		public async Task<CopilotIntentCallResult> ExecuteIntentAsync(CopilotIntentCall call,
				CancellationToken token = default) {
			return await _apiSkillExecutor.ExecuteAsync(call, token);
		}

		/// <inheritdoc/>
		public async Task<CopilotIntentCallResult> CompleteExecutingIntentAsync(Guid sessionId,
				CancellationToken token = default) {
			var session = _sessionManager.GetById(sessionId);
			return await _apiSkillExecutor.CompleteExecutingIntentAsync(session, token);
		}

		[Obsolete]
		public IList<string> GetAvailableIntents(CopilotIntentMode mode, params string[] names) {
			return GetAvailableSkills(mode, names);
		}

		/// <inheritdoc/>
		public IList<string> GetAvailableSkills(CopilotIntentMode mode, params string[] names) {
			if ((mode == CopilotIntentMode.Api && !CanRunCopilotApi()) ||
					(mode == CopilotIntentMode.Chat && !CanUseCopilotChat())) {
				return new List<string>();
			}
			IEnumerable<CopilotIntentSchema> activeIntents = mode == CopilotIntentMode.Api ?
				FindSkillsForApi() :
				FindSkillsForChat(null);
			IList<string> availableIntentNames = names.Any() ?
				FilterActiveIntents(names, activeIntents).ToList() :
				activeIntents.Select(intent => intent.Name).ToList();
			return availableIntentNames;
		}

		/// <inheritdoc/>
		public CopilotIntentCallResult ExecuteIntent(CopilotIntentCall call) {
			return AsyncPump.Run(() => ExecuteIntentAsync(call, CancellationToken.None));
		}

		#endregion

	}

}

