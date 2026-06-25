namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Configuration.GenAI;
	using Terrasoft.Common;
	using System.Linq;
	using Creatio.FeatureToggling;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;

	/// <summary>
	/// Implementation of <see cref="ICopilotDocumentManager"/>.
	/// </summary>
	[DefaultBinding(typeof(ICopilotDocumentManager))]
	internal class CopilotDocumentManager : ICopilotDocumentManager
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private readonly IDocumentTool _documentTool;

		#endregion

		#region Constructors: Public

		public CopilotDocumentManager(UserConnection userConnection, IDocumentTool documentTool) {
			_userConnection = userConnection;
			_documentTool = documentTool;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public void AddDocumentsCompletionMessages(CopilotSession session, List<ChatMessage> messages) {
			if (!Features.GetIsEnabled<GenAIFeatures.UseFileHandling>()) {
				return;
			}
			var completionMessages = _documentTool.GetDocumentMessagesForCompletion(
					session.Documents, session.IsTransient)
				.Select(msg => msg.ToCompletionApiMessage())
				.ToArray();
			if (completionMessages.Length > 0) {
				int lastUserMessageIndex = messages.FindLastIndex(m => m.Role == CopilotMessageRole.User);
				messages.InsertRange(lastUserMessageIndex == -1 ? 0 : lastUserMessageIndex, completionMessages);
			}
		}

		/// <inheritdoc/>
		public void AddIntentDocuments(CopilotSession session) {
			if (Features.GetIsDisabled<GenAIFeatures.UseFileHandling>()) {
				return;
			}
			IList<CreatioAIDocument> documents = _documentTool.GetDocuments(_userConnection,
				session.CurrentIntentId, session.RootIntentId);
			if (documents.IsNotNullOrEmpty()) {
				session.Documents.AddRange(documents);
			}
		}

		/// <inheritdoc/>
		public void AddCallDocuments(CopilotSession session, CopilotIntentCall call) {
			IList<ICreatioAIDocument> documents = call.Documents;
			if (documents.IsNullOrEmpty()) {
				return;
			}
			Exception[] errors = _documentTool.ValidateDocuments(_userConnection, session.Id, documents).ToArray();
			if (errors.Length > 0) {
				throw new AggregateException(errors);
			}
			session.Documents.AddRange(documents);
		}

		#endregion

	}
}

