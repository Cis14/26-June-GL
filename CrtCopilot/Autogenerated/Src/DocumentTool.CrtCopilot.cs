namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;

	#region Class: DocumentTool

	/// <inheritdoc />
	[DefaultBinding(typeof(IDocumentTool))]
	public class DocumentTool : IDocumentTool
	{

		#region Constants: Private

		private const string FilePromptFormat = "A list of documents/files has been added to the conversation.\n" +
			"Instructions:\n" +
			"- If the content of a document is needed to answer the user's query, use the 'GetDocumentsContent' " +
			"action to retrieve the content.\n" +
			"- If a retrieved document starts with a '[Truncated]' marker, it means the full content " +
			"was not loaded and must be retrieved again if it is required.\n" +
			"- Only content of documents listed can be retrieved using this method.\n" +
			"Output Constraints: Only provide names of files when listing them.\n" +
			"List of added documents: {0}";
		private const string UseSkillPrompt =
			"- Make sure an agent is active before providing answer to user. Use *Creatio.AI Agent* if no " +
			"specialized agent is available.\n" +
			"- Added documents belong to conversation and do not belong to specific entity. You need to make sure " +
			"that available files belong to the user query.\n" +
			"- When the user references a document or file that is not added into the conversation " +
			"trigger skill with name SkillAttachFileToConversation.\n";
		private const string NoDocumentChatPrompt =
			"No documents or files currently added to the conversation.\nInstructions:\n" +
			UseSkillPrompt +
			"Output Constraints: Only provide names of files when listing them.";

		#endregion

		#region Fields: Private

		private readonly IDocumentValidator _documentValidator;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentTool"/> class using the default <see cref="DocumentValidator"/>.
		/// </summary>
		public DocumentTool()
			: this(new DocumentValidator()) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentTool"/> class with a specified <see cref="IDocumentValidator"/>.
		/// </summary>
		/// <param name="documentValidator">The document content validator to use.</param>
		public DocumentTool(IDocumentValidator documentValidator) {
			_documentValidator = documentValidator;
		}

		#endregion

		#region Methods: Private

		private IList<CreatioAIDocument> GetSessionDocuments(UserConnection userConnection, Guid sessionId) {
			var selectQuery = (Select)new Select(userConnection)
					.Column("Id")
					.Column("Name")
				.From("CreatioAISessionFile").WithHints(Hints.NoLock)
				.Where("SessionId").IsEqual(Column.Parameter(sessionId));
			var result = new List<CreatioAIDocument>();
			using (DBExecutor dbExecutor = userConnection.EnsureDBConnection()) {
				using (IDataReader reader = selectQuery.ExecuteReader(dbExecutor)) {
					while (reader.Read()) {
						Guid id = reader.GetColumnValue<Guid>("Id");
						string fileName = reader.GetColumnValue<string>("Name");
						var locator = new CreatioAIDocument {
							FileId = id,
							FileName = fileName,
							SessionId = sessionId,
							FileSchemaName = "CreatioAISessionFile"
						};
						result.Add(locator);
					}
				}
			}
			return result;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public IEnumerable<CopilotMessage> GetDocumentMessagesForCompletion(
				IEnumerable<ICreatioAIDocument> documents, bool isTransient = false) {
			documents = documents == null ? Array.Empty<CreatioAIDocument>() : documents.ToArray();
			if (!documents.Any()) {
				if (!isTransient) {
					yield return new CopilotMessage(NoDocumentChatPrompt, CopilotMessageRole.System);
				}
				yield break;
			}
			string filePromptFormat = isTransient ? FilePromptFormat : FilePromptFormat + UseSkillPrompt;
			string filePrompt = string.Format(filePromptFormat, string.Join(", ", documents.Select(doc =>
				$"{{schemaName:\"{doc.FileSchemaName}\",fileName:\"{doc.FileName}\",fileId:\"{doc.FileId}\"}}")));
			yield return new CopilotMessage(filePrompt, CopilotMessageRole.System);
		}

		/// <inheritdoc />
		public void RemoveIntentDocumentsFromSession(CopilotSession session) {
			if (session.Documents.IsEmpty()) {
				return;
			}
			List<ICreatioAIDocument> documentsToRemove = session.Documents.Where(doc => !doc.SessionId.HasValue).ToList();
			foreach (ICreatioAIDocument document in documentsToRemove) {
				session.Documents.Remove(document);
			}
		}

		/// <inheritdoc />
		public IList<CreatioAIDocument> GetDocuments(UserConnection userConnection, Guid? currentIntentId,
				Guid? rootIntentId) {
			var result = new List<CreatioAIDocument>();
			if (currentIntentId.IsNullOrEmpty() && rootIntentId.IsNullOrEmpty()) {
				return result;
			}
			var selectQuery = (Select)new Select(userConnection)
					.Column("Id")
					.Column("Name")
					.Column("IntentUId")
				.From("CreatioAIIntentFile").WithHints(Hints.NoLock)
				.Where("IntentUId")
					.In(Column.Parameters(currentIntentId ?? Guid.Empty, rootIntentId ?? Guid.Empty));
			using (DBExecutor dbExecutor = userConnection.EnsureDBConnection()) {
				using (IDataReader reader = selectQuery.ExecuteReader(dbExecutor)) {
					while (reader.Read()) {
						Guid id = reader.GetColumnValue<Guid>("Id");
						string fileName = reader.GetColumnValue<string>("Name");
						string actualIntentId = reader.GetColumnValue<string>("IntentUId");
						var locator = new CreatioAIDocument {
							FileId = id,
							FileName = fileName,
							IntentId = Guid.Parse(actualIntentId),
							FileSchemaName = "CreatioAIIntentFile"
						};
						result.Add(locator);
					}
				}
			}
			return result;
		}

		/// <inheritdoc />
		public void LoadSessionDocuments(UserConnection userConnection, CopilotSession session) {
			if (session.Documents == null) {
				session.Documents = new List<ICreatioAIDocument>();
			}
			IList<CreatioAIDocument> documents = GetSessionDocuments(userConnection, session.Id);
			if (documents.IsNotNullOrEmpty()) {
				session.Documents.AddRange(documents);
			}
			IList<CreatioAIDocument> skillDocuments = GetDocuments(userConnection,
				session.CurrentIntentId, session.RootIntentId);
			if (skillDocuments.IsNotNullOrEmpty()) {
				session.Documents.AddRange(skillDocuments);
			}
		}

		/// <inheritdoc />
		public IEnumerable<Exception> ValidateDocuments(UserConnection userConnection, Guid sessionId,
				IList<ICreatioAIDocument> documents) {
			var errors = new List<Exception>();
			if (documents == null || documents.Count == 0) {
				return Array.Empty<Exception>();
			}
			int targetContentSize = _documentValidator.GetTotalSessionFilesContentSize(userConnection,
				sessionId);
			foreach (ICreatioAIDocument document in documents) {
				try {
					int documentContentSize = _documentValidator.ValidateDocument(userConnection, document);
					targetContentSize += documentContentSize;
					_documentValidator.ValidateContentSize(userConnection, targetContentSize);
				} catch (Exception e) {
					errors.Add(e);
				}
			}
			return errors;
		}

		#endregion

	}

	#endregion

}

