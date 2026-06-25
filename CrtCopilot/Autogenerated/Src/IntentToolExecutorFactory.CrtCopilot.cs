namespace Creatio.Copilot
{
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: IntentToolExecutorFactory

	/// <summary>
	/// Intent tool executor factory.
	/// </summary>
	/// <inheritdoc cref="Creatio.Copilot.IIntentToolExecutorFactory"/>
	[DefaultBinding(typeof(IIntentToolExecutorFactory))]
	public class IntentToolExecutorFactory : IIntentToolExecutorFactory
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private readonly ICopilotMsgChannelSender _copilotMsgChannelSender;
		private readonly IDocumentTool _documentTool;
		private readonly ICopilotWorkflowService _workflowService;
		private readonly ICopilotSessionManager _sessionManager;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="IntentToolExecutorFactory"/> type.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="copilotSessionManager">Copilot session manager.</param>
		/// <param name="copilotMsgChannelSender">Copilot msg channel sender.</param>
		/// <param name="documentTool">Document tool.</param>
		/// <param name="workflowService"></param>
		public IntentToolExecutorFactory(UserConnection userConnection, ICopilotSessionManager copilotSessionManager,
			ICopilotMsgChannelSender copilotMsgChannelSender, IDocumentTool documentTool,
			ICopilotWorkflowService workflowService) {
			_userConnection = userConnection;
			_copilotMsgChannelSender = copilotMsgChannelSender;
			_documentTool = documentTool;
			_workflowService = workflowService;
			_sessionManager = copilotSessionManager;
		}

		#endregion

		#region Methods: Private

		private WorkflowToolExecutor GetWorkflowExecutor(CopilotIntentSchema copilotIntentSchema) {
			return new WorkflowToolExecutor(copilotIntentSchema, _userConnection, _workflowService);
		}

		private IntentToolExecutor GetIntentExecutor(CopilotIntentSchema copilotIntentSchema) {
			return new IntentToolExecutor(copilotIntentSchema, _userConnection, _copilotMsgChannelSender,
				_documentTool, _sessionManager);
		}

		private bool IsWorkflowSchema(CopilotIntentSchema copilotIntentSchema) {
			return copilotIntentSchema.Type == CopilotIntentType.WorkflowAgent;
		}

		#endregion

		#region Mehods: Public

		/// <inheritdoc/>
		public IntentToolExecutor Create(CopilotIntentSchema copilotIntentSchema) {
			return GetIntentExecutor(copilotIntentSchema);
		}

		/// <inheritdoc/>
		public IToolExecutionTrigger CreateTrigger(CopilotIntentSchema copilotIntentSchema) {
			return IsWorkflowSchema(copilotIntentSchema) ?
				(IToolExecutionTrigger) GetWorkflowExecutor(copilotIntentSchema)
				: GetIntentExecutor(copilotIntentSchema);
		}

		/// <inheritdoc/>
		public IToolExecutor CreateExecutor(CopilotIntentSchema copilotIntentSchema) {
			return IsWorkflowSchema(copilotIntentSchema) ?
				(IToolExecutor) GetWorkflowExecutor(copilotIntentSchema)
				: GetIntentExecutor(copilotIntentSchema);
		}

		#endregion

	}

	#endregion

}
