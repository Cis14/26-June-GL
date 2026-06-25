namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Creatio.FeatureToggling;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: IntentPromptBuilder

	[DefaultBinding(typeof(IIntentPromptBuilder))]
	internal class IntentPromptBuilder : IIntentPromptBuilder
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="IntentPromptBuilder"/> class.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public IntentPromptBuilder(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		private static string FormatParameters(Dictionary<string, object> inputParameterValues,
				CopilotIntentSchema intent, bool includeOutputParametersInPrompt) {
			var sections = new List<string>();
			var parameterSectionFormatter = new IntentParametersSectionFormatter();
			if (inputParameterValues.Count > 0) {
				sections.Add(
					parameterSectionFormatter.FormatInputParameters(intent.InputParameters, inputParameterValues));
			}
			if (intent.OutputParameters.Count > 0 && includeOutputParametersInPrompt) {
				sections.Add(parameterSectionFormatter.FormatOutputParameters(intent.OutputParameters));
			}
			return string.Join(Environment.NewLine, sections);
		}

		private void GetExtraParameterNames(IDictionary<string, object> inputParameters,
				IList<string> warnings, List<string> inputParameterNames) {
			HashSet<string> extraParameterNames = inputParameters.Keys.ToHashSet();
			extraParameterNames.ExceptWith(inputParameterNames);
			if (extraParameterNames.Any()) {
				string warning = _userConnection.GetLocalizableString("WarningParameterNotExist", nameof(IntentPromptBuilder))
					.Format(string.Join(",", extraParameterNames));
				warnings.Add(warning);
			}
		}

		private HashSet<string> GetNotSpecifiedParameters(IDictionary<string, object> inputParameters,
				List<string> inputParameterNames, IList<string> warnings) {
			var nonSpecified = new HashSet<string>(inputParameterNames);
			nonSpecified.ExceptWith(inputParameters.Keys);
			if (nonSpecified.Any()) {
				string warning = _userConnection.GetLocalizableString("WarningParameterValueNotSpecified", nameof(IntentPromptBuilder))
					.Format(string.Join(",", nonSpecified));
				warnings.Add(warning);
			}
			return nonSpecified;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public List<CopilotMessage> GenerateIntentPromptMessages(IDictionary<string, object> parameterValues, string additionalPromptText,
				CopilotIntentSchema intent, IList<string> warnings, bool includeOutputParametersInPrompt) {
			var result = new List<CopilotMessage>();
			if (parameterValues == null) {
				parameterValues = new Dictionary<string, object>();
			}
			List<string> intentInputParameters = intent.InputParameters.Select(x => x.Name).ToList();
			HashSet<string> notSpecifiedKeys = GetNotSpecifiedParameters(parameterValues,
				intentInputParameters, warnings);
			GetExtraParameterNames(parameterValues, warnings, intentInputParameters);
			var inputParameters = new Dictionary<string, object>(parameterValues);
			var prompt = new StringBuilder();
			prompt.Append(intent.FullPrompt);
			foreach (string key in notSpecifiedKeys) {
				inputParameters[key] = null;
			}
			if (!string.IsNullOrWhiteSpace(additionalPromptText)) {
				prompt.AppendLine().Append(additionalPromptText);
			}
			string userParametersMessage = FormatParameters(inputParameters, intent, includeOutputParametersInPrompt);
			if (Features.GetIsEnabled<Terrasoft.Configuration.GenAI.GenAIFeatures.UseSystemRoleForWorkflowSkillPrompt>()) {
				result.Add(CopilotMessage.FromSystem(prompt.ToString()));
				if (userParametersMessage.IsNotNullOrWhiteSpace()) {
					result.Add(CopilotMessage.FromUser(userParametersMessage));
				}
			} else {
				if (userParametersMessage.IsNotNullOrWhiteSpace()) {
					prompt.AppendLine().Append(userParametersMessage);
				}
				result.Add(CopilotMessage.FromUser(prompt.ToString()));
			}
			return result;
		}

		/// <inheritdoc/>
		public string GenerateIntentPrompt(CopilotIntentSchema intent, IDictionary<string, object> parameterValues) {
			var formattingContext = new PromptTemplateFormattingContext(_userConnection) {
				VariableValues = parameterValues
			};
			return intent.PromptTemplate.Format(formattingContext);
		}

		#endregion

	}

	#endregion

}

