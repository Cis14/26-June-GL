namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Linq;
	using Newtonsoft.Json;
	using Common.Logging;
	using Creatio.Copilot.Actions;
	using Creatio.FeatureToggling;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.File;
	using Terrasoft.File.Abstractions.TextExtraction;

	#region Class: ContextualDocumentProviderAction

	/// <summary>
	/// Contextual document provider code action.
	/// </summary>
	public class ContextualDocumentProviderAction : BaseExecutableCodeAction
	{

		#region Class: EnableDocumentListRetrieval

		/// <summary>
		/// Enable specific documents retrieval.
		/// </summary>
		/// <inheritdoc cref="Creatio.FeatureToggling.FeatureMetadata"/>
		public class EnableDocumentListRetrieval : FeatureMetadata
		{

			#region Constructors: Public

			/// <summary>
			/// Initializes a new instance of the <see cref="EnableDocumentListRetrieval"/> type.
			/// </summary>
			public EnableDocumentListRetrieval() {
				IsEnabled = false;
			}

			#endregion

		}

		#endregion


		#region Constants: Private

		private const string CaptionValue = "Get documents content.";
		private const string DescriptionValue = "Provides content of the documents that have been added to the conversation.";
		private const string FileMarkerFormatValue = "#FileId: {0}, #FileName: {1}";
		private const string ContentMarkerValue = "#Content:";
		private const string ContentMarkerErrorValue = "#Content: Error retrieving document content.";
		private const string SessionNotFoundErrorMessage = "Session not found";
		private const string DocumentIdsParameter = "DocumentIds";

		#endregion

		#region Constructors: Internal

		/// <summary>
		/// Initializes a new instance of the <see cref="ContextualDocumentProviderAction"/> class.
		/// </summary>
		/// <param name="logger">An instance of the <see cref="ILog"/> implementing type to initialize the current
		/// instance with.</param>
		internal ContextualDocumentProviderAction(ILog logger): this() {
			_logger = logger;
		}

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="ContextualDocumentProviderAction"/> class.
		/// </summary>
		public ContextualDocumentProviderAction() {
			if (IsDocumentListRetrievalEnabled()) {
				Parameters = new List<SourceCodeActionParameter> {
					new SourceCodeActionParameter {
						Name = DocumentIdsParameter,
						Caption = new LocalizableString("Document IDs"),
						Description = new LocalizableString(
							"Generate array of document identifiers (GUIDs) to filter available documents for those " +
							"that should be used as context. If all documents are required or no documents available send an empty array."),
						IsRequired = true,
						DataValueTypeUId = DataValueType.TextDataValueTypeUId
					}
				};
			}
		}

		#endregion

		#region Properties: Private

		private ILog _logger;
		private ILog Logger => _logger ?? (_logger = LogManager.GetLogger("Copilot"));

		#endregion

		#region Methods: Private

		private static bool IsDocumentListRetrievalEnabled() {
			return Features.GetIsEnabled<EnableDocumentListRetrieval>();
		}

		private void RenderDocument(StringBuilder contentBuilder, ICreatioAIDocument document,
				ITextContentExtractor contentExtractor) {
			contentBuilder.AppendFormat(FileMarkerFormatValue, document.FileId, document.FileName);
			contentBuilder.AppendLine();
			var entityFileLocator = new EntityFileLocator(document.FileSchemaName, document.FileId);
			try {
				string content = contentExtractor.ExtractText(entityFileLocator);
				contentBuilder.AppendLine(ContentMarkerValue);
				contentBuilder.AppendLine(content);
			} catch (Exception e) {
				Logger.Error($"Error retrieving document content for {document.FileName}", e);
				contentBuilder.AppendLine(ContentMarkerErrorValue);
			}
			contentBuilder.AppendLine();
		}

		private void HandleDocumentRetrieval(CopilotSession session, StringBuilder sb, ActionExecutionOptions options) {
			var contentExtractor = ClassFactory.Get<ITextContentExtractor>();
			IList<ICreatioAIDocument> documents = session.Documents;
			if (IsDocumentListRetrievalEnabled()) {
				List<Guid> documentIds = null;
				if (options.ParameterValues.TryGetValue(DocumentIdsParameter, out string documentIdsRaw)
					&& !string.IsNullOrEmpty(documentIdsRaw)) {
					try {
						documentIds = JsonConvert.DeserializeObject<List<Guid>>(documentIdsRaw);
					} catch {
						Logger.Warn("Failed to parse DocumentIds parameter as List<Guid>.");
					}
				}
				if (documentIds != null && documentIds.Count > 0) {
					documents = documents.Where(d => documentIds.Contains(d.FileId)).ToList();
				}
			}
			if (documents.Count > 0) {
				foreach (ICreatioAIDocument document in documents) {
					RenderDocument(sb, document, contentExtractor);
				}
			} else {
				sb.AppendLine("No documents have been uploaded or attached to the conversation, " +
					"or none matched the provided DocumentIds.");
				sb.AppendLine(
					"Upload a file to session or use skill SkillAttachFileToConversation if attaching existing files.");
			}
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public override LocalizableString GetCaption() {
			return new LocalizableString(CaptionValue);
		}

		/// <inheritdoc />
		public override LocalizableString GetDescription() {
			return new LocalizableString(DescriptionValue);
		}

		/// <inheritdoc />
		public override CopilotActionExecutionResult Execute(ActionExecutionOptions options) {
			var sessionManager = ClassFactory.Get<ICopilotSessionManager>();
			CopilotSession session = sessionManager.FindById(options.CopilotSessionUId);
			if (session == null) {
				Logger.Error($"Session {options.CopilotSessionUId} not found.");
				return new CopilotActionExecutionResult {
					Status = CopilotActionExecutionStatus.Failed,
					ErrorMessage = SessionNotFoundErrorMessage
				};
			}
			var sb = new StringBuilder();
			HandleDocumentRetrieval(session, sb, options);
			return new CopilotActionExecutionResult {
				Status = CopilotActionExecutionStatus.Completed,
				Response = sb.ToString(),
				ResponseOptions = new ActionResponseOptions {
					TruncateContentOnSave = true
				}
			};
		}

		#endregion

	}

	#endregion

}
