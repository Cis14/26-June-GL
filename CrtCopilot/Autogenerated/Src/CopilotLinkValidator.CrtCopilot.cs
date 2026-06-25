namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::Common.Logging;
	using Terrasoft.Common;
	using Terrasoft.Configuration.GenAI;
	using Terrasoft.Core.Factories;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion.Requests;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion.Responses;

	/// <summary>
	/// Implementation of <see cref="ICopilotLinkValidator"/>.
	/// </summary>
	[DefaultBinding(typeof(ICopilotLinkValidator))]
	internal class CopilotLinkValidator : ICopilotLinkValidator
	{

		#region Fields: Private

		private readonly ILog _log = LogManager.GetLogger("Copilot");
		private readonly ICopilotHyperlinkUtils _hyperlinkUtils;
		private readonly IGenAICompletionServiceProxy _completionService;

		#endregion

		#region Properties: Private

		private string PromptToRemoveInvalidLinks => "Rewrite the provided text, removing invalid links that " +
			"literally and strictly look like \"" + _hyperlinkUtils.InvalidLinkMarker + "\". If the message " +
			"does not contain other links except \"" + _hyperlinkUtils.InvalidLinkMarker + "\" please tell " +
			"that without mentioning the \"" + _hyperlinkUtils.InvalidLinkMarker + "\". All other links are " +
			"valid. Remove invalid links naturally, without additional comments, but save the idea and structure. " +
			"If the invalid link contains alternative text or markdown format remove them as well.";

		private string ApiPromptToRemoveInvalidLinks => "Rewrite the provided text, removing invalid links that " +
			"literally and strictly look like \"" + _hyperlinkUtils.InvalidLinkMarker + "\". Keep the formatting and " +
			"structure when possible. Remove any alternative text or markdown format related to invalid link. When" +
			" working with JSON, keep the name-value structure without modifying the names. Consider other links " +
			"as valid. If the value contains only \"" + _hyperlinkUtils.InvalidLinkMarker + "\", replace it with an" +
			" empty string.";

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotLinkValidator"/>
		/// </summary>
		/// <param name="hyperlinkUtils">An instance of <see cref="ICopilotHyperlinkUtils"/>.</param>
		/// <param name="completionService">An instance of <see cref="IGenAICompletionServiceProxy"/>.</param>
		public CopilotLinkValidator(ICopilotHyperlinkUtils hyperlinkUtils,
				IGenAICompletionServiceProxy completionService) {
			_hyperlinkUtils = hyperlinkUtils;
			_completionService = completionService;
		}

		#endregion

		#region Methods: Private

		private string RemoveInvalidLinks(string content, bool isApi) {
			try {
				string prompt = isApi ? ApiPromptToRemoveInvalidLinks : PromptToRemoveInvalidLinks;
				ChatCompletionRequest rewriteRequest = CreateSimpleCompletionRequest(prompt, content);
				ChatCompletionResponse response = _completionService.ChatCompletion(rewriteRequest);
				return response.Choices
					.Where(choice => choice.Message.Content.IsNotNullOrEmpty())
					.Select(choice => choice.Message.Content)
					.ConcatIfNotEmpty(string.Empty);
			} catch (Exception e) {
				_log.Error("Error occurred while removing invalid links", e);
				return content;
			}
		}

		private ChatCompletionRequest CreateSimpleCompletionRequest(string prompt, string content) {
			CopilotMessage systemMessage = CopilotMessage.FromSystem(prompt);
			CopilotMessage userMessage = CopilotMessage.FromUser(content);
			return new ChatCompletionRequest {
				Messages = new List<ChatMessage> {
					systemMessage.ToCompletionApiMessage(),
					userMessage.ToCompletionApiMessage()
				}
			};
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public string ValidateLinks(CopilotSession session, string content, bool isApi) {
			if (!_hyperlinkUtils.TryMarkInvalidLinks(session, content, out string markedContent)) {
				return content;
			}
			return RemoveInvalidLinks(markedContent, isApi);
		}

		#endregion

	}
}

