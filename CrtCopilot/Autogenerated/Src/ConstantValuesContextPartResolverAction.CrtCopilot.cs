namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using System.Linq;
	using Common.Logging;
	using Creatio.Copilot.Actions;
	using Terrasoft.Common;
    using Terrasoft.Core;
    using Terrasoft.Core.Factories;

	/// <summary>
	/// Constant values context resolver action.
	/// </summary>
	public class ConstantValuesContextPartResolverAction : BaseExecutableCodeAction {

		#region Constants: Private

		private const string CaptionValue = "Get values from the constant values context.";
		private const string ConstantKeyParameter = "constantKey";
		private const string DescriptionValue = "Provides values from the constant values context.";
		private const string ContextPartMissingErrorMessage = "Context part with constant values is missing or empty.";
		private const string KeyNotFoundErrorMessage = "Key not found or value is null in context.";
		private const string MissingOrInvalidParameterErrorMessage = "Missing or invalid 'constantKey' parameter.";
		private const string SessionNotFoundErrorMessage = "Session not found.";

		#endregion

		#region Constructors: Internal

		/// <summary>
		/// Initializes a new instance of the <see cref="ConstantValuesContextPartResolverAction"/> class.
		/// </summary>
		/// <param name="logger">Logger.</param>
		internal ConstantValuesContextPartResolverAction(ILog logger) : this() {
			_logger = logger;
		}

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="ConstantValuesContextPartResolverAction"/> class.
		/// </summary>
		public ConstantValuesContextPartResolverAction() {
			Parameters = new List<SourceCodeActionParameter> {
				new SourceCodeActionParameter() {
					Caption = ConstantKeyParameter,
					DataValueTypeUId = DataValueType.MediumTextDataValueTypeUId,
					Name = ConstantKeyParameter,
					IsRequired = true,
					Direction = 0,
					Description = "Key of the constant" }
			};
		}

		#endregion

		#region Properties: Private

		private ILog _logger;
		private ILog Logger => _logger ?? (_logger = LogManager.GetLogger(nameof(ConstantValuesContextPartResolverAction)));

		#endregion

		#region Methods: Public

		/// <summary>
		/// Gets the caption of the executable code action.
		/// </summary>
		public override LocalizableString GetCaption() {
			return new LocalizableString(CaptionValue);
		}

		/// <summary>
		/// Gets the description of the executable code action.
		/// </summary>
		public override LocalizableString GetDescription() {
			return new LocalizableString(DescriptionValue);
		}

		/// <summary>
		/// Executes the code action using the specified execution options.
		/// </summary>
		public override CopilotActionExecutionResult Execute(ActionExecutionOptions options) {
			var sessionManager = ClassFactory.Get<ICopilotSessionManager>();
			CopilotSession session = sessionManager.FindById(options.CopilotSessionUId);
			if (session == null) {
				Logger.Error($"Session '{options.CopilotSessionUId}' not found.");
				return GetFailedResult(SessionNotFoundErrorMessage);
			}
			if (!options.ParameterValues.TryGetValue(ConstantKeyParameter, out var parameterValue) || parameterValue == null) {
				Logger.Error("Parameter 'constantKey' not provided or null.");
				return GetFailedResult(MissingOrInvalidParameterErrorMessage);
			}
			if (session.CurrentContext == null) {
				Logger.Error("Session CurrentContext is not initialized.");
				return GetFailedResult(ContextPartMissingErrorMessage);
			}
			var part = session.CurrentContext.Parts
				.OfType<ConstantValuesContextPart>()
				.FirstOrDefault();
			if (part?.Values == null) {
				Logger.Error("Context part with constant values is missing or empty.");
				return GetFailedResult(ContextPartMissingErrorMessage);
			}
			if (!part.Values.TryGetValue(parameterValue, out var foundValue) || foundValue == null) {
				Logger.Error($"Key '{parameterValue}' not found or value is null in context part values.");
				return GetFailedResult(KeyNotFoundErrorMessage);
			}
			return new CopilotActionExecutionResult {
				Status = CopilotActionExecutionStatus.Completed,
				Response = foundValue.ToString(),
			};
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Creates a failed <see cref="CopilotActionExecutionResult"/> with the specified error message.
		/// </summary>
		private CopilotActionExecutionResult GetFailedResult(string errorMessage) {
			return new CopilotActionExecutionResult {
				Status = CopilotActionExecutionStatus.Failed,
				ErrorMessage = errorMessage
			};
		}

		#endregion

	}
}

