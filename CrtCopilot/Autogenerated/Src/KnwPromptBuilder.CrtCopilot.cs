namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.FeatureToggling;
	using Terrasoft.Core.Factories;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;

	#region Class: KnwPromptBuilder

	/// <summary>
	/// Builds and injects a system prompt enumerating knowledge sources (if any) retrieved for the current
	/// <see cref="CopilotSession"/>. Resolution of the underlying <see cref="IKnwSourceProvider"/> is optional: the
	/// builder degrades gracefully and returns no prompt when the provider is not registered, the feature flag is
	/// disabled, or an error occurs during retrieval.
	/// </summary>
	[DefaultBinding(typeof(IKnwPromptBuilder))]
	internal class KnwPromptBuilder : IKnwPromptBuilder {

		#region Constants: Private

		private const string NoDescriptionPlaceholder = "(no description)";

		private const string KnwPromptFormat = "You are provided with a list of external Knowledge Sources (RAG).\n" +
			"Each entry includes a unique UID and short description (purpose, domain, type of data).\n" +
			"Use this list to decide which sources to query when generating an answer.\n" +
			"Your goal is to find the most relevant and factual information from these sources and integrate it " +
			"naturally into the response.\n\n" + "RAG Sources:\n" + "{0}\n\n";

		#endregion

		#region Methods: Private

		private string GetKnwSourcePrompt(CopilotSession session) {
			if (!Features.GetIsEnabled<GenAIFeatures.EnableKnwRetrieval>() || session == null) {
				return null;
			}
			IKnwSourceProvider provider;
			try {
				provider = ClassFactory.Get<IKnwSourceProvider>();
			} catch {
				return null;
			}
			if (provider == null) {
				return null;
			}
			IEnumerable<KnwSourceDto> sources = provider.GetSourcesInSession(session);
			var sourceList = sources?
				.Where(s => s != null && s.Id != Guid.Empty)
				.ToList();
			if (sourceList == null || sourceList.Count == 0) {
				return null;
			}
			string sourcesBlock = string.Join("\n", sourceList.Select(item => "SourceUId: " +
				item.Id + ", SourceDescription: " +
				(string.IsNullOrWhiteSpace(item.Description) ? NoDescriptionPlaceholder : item.Description)));
			return string.Format(KnwPromptFormat, sourcesBlock).TrimEnd();
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public void AddKnwSourcePrompt(CopilotSession session, List<ChatMessage> messages) {
			string knwPrompt = GetKnwSourcePrompt(session);
			if (!string.IsNullOrEmpty(knwPrompt)) {
				int lastUserMessageIndex = messages.FindLastIndex(m => m.Role == CopilotMessageRole.User);
				messages.Insert(lastUserMessageIndex == -1 ? 0 : lastUserMessageIndex,
					new ChatMessage(CopilotMessageRole.System, knwPrompt));
			}
		}

		/// <inheritdoc/>
		public void AddKnwSourceMsg(CopilotSession session) {
			string knwPrompt = GetKnwSourcePrompt(session);
			if (!string.IsNullOrEmpty(knwPrompt)) {
				session.AddMessage(new CopilotMessage(knwPrompt, CopilotMessageRole.System));
			}
		}

		#endregion

	}

	#endregion

}
