namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using Common.Logging;
	using Creatio.Copilot.Actions;
	using Creatio.FeatureToggling;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.File;
	using SystemSettings = Terrasoft.Core.Configuration.SysSettings;

	#region Class: AttachToConversationAction

	/// <summary>
	/// Represents an action that attaches a file or document to a Copilot conversation in Creatio.
	/// </summary>
	public class AttachToConversationAction : BaseExecutableCodeAction, IUserConnectionRequired
	{

		#region Fields: Private

		private readonly IDocumentValidator _validator;
		private readonly char _delimiter = ';';
		private UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Create a new instance of the <see cref="AttachToConversationAction"/> class.
		/// </summary>
		public AttachToConversationAction()
			: this(new DocumentValidator()) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AttachToConversationAction"/> class with the specified
		/// document content validator.
		/// </summary>
		/// <param name="validator">The <see cref="IDocumentValidator"/> instance used to validate
		/// document content.</param>
		/// <exception cref="ArgumentNullOrEmptyException">
		/// Thrown when the <paramref name="validator"/> parameter is <c>null</c>.
		/// </exception>
		public AttachToConversationAction(IDocumentValidator validator) {
			_validator = validator ?? throw new ArgumentNullOrEmptyException(nameof(validator));
			Parameters = new List<SourceCodeActionParameter> {
				new SourceCodeActionParameter {
					Name = "schemaName",
					Caption = new LocalizableString("File schema name"),
					Description = new LocalizableString(
						"Schema in which file information is kept. Should be provided by user."),
					IsRequired = true,
					DataValueTypeUId = DataValueType.MediumTextDataValueTypeUId
				},
				new SourceCodeActionParameter {
					Name = "filesList",
					Caption = new LocalizableString("File List of Identifier"),
					Description = new LocalizableString($"A '{_delimiter}' separated list of file identifiers " +
						"(GUIDs), each entry representing file id in Creatio."),
					IsRequired = true,
					DataValueTypeUId = DataValueType.LongTextDataValueTypeUId
				}
			};
		}

		#endregion

		#region Properties: Private

		private ILog _logger;
		private ILog Logger => _logger ?? (_logger = LogManager.GetLogger(nameof(AttachToConversationAction)));

		#endregion

		#region Methods: Private

		private static void UpdateSession(ICopilotSessionManager sessionManager, CopilotSession session) {
			sessionManager.Update(session, null);
		}

		private (List<CreatioAIDocument> documents, int targetContentSize, List<string> errors) GetDocuments(
				Dictionary<string, string> parameterValues, string fileSchemaName, Guid sessionId) {
			if (!parameterValues.TryGetValue("filesList", out string fileIds) || string.IsNullOrWhiteSpace(fileIds)) {
				return (null, 0, new List<string> {
					"File identifiers are not provided or empty."
				});
			}
			var errors = new List<string>();
			var documents = new List<CreatioAIDocument>();
			int targetContentSize = _validator.GetTotalSessionFilesContentSize(_userConnection, sessionId);
			foreach (string fileIdAsString in fileIds.Split(_delimiter)) {
				if (!Guid.TryParse(fileIdAsString, out Guid fileId) || fileId.IsEmpty()) {
					errors.Add("Invalid file identifier: " + fileIdAsString);
					continue;
				}
				(CreatioAIDocument document, int size) = GetDocumentById(fileId, fileSchemaName, sessionId);
				int documentContentSize;
				try {
					documentContentSize = _validator.ValidateDocument(_userConnection, document, size);
				} catch (Exception e) {
					errors.Add($"File {document.FileName} with id {document.FileId} is not valid: {e.Message}");
					continue;
				}
				targetContentSize += documentContentSize;
				documents.Add(document);
			}
			return (documents, targetContentSize, errors);
		}

		private (CreatioAIDocument document, int size) GetDocumentById(Guid fileId, string fileSchemaName, Guid sessionId) {
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, fileSchemaName) {
				PrimaryQueryColumn = {
					IsAlwaysSelect = true
				},
				UseAdminRights = true
			};
			esq.AddColumn("Name");
			esq.AddColumn("TotalSize");
			Entity file = esq.GetEntity(_userConnection, fileId);
			if (file == null) {
				return (null, 0);
			}
			string fileName = file.GetTypedColumnValue<string>("Name");
			int totalSize = file.GetTypedColumnValue<int>("TotalSize");
			var document = new CreatioAIDocument {
				FileName = fileName,
				FileId = fileId,
				FileSchemaName = fileSchemaName,
				SessionId = sessionId
			};
			return (document, totalSize);
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc />
		protected override bool GetIsEnabled() {
			return Features.GetIsEnabled<GenAIFeatures.EnableAttachDocumentToConversationAction>();
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public override LocalizableString GetCaption() {
			return new LocalizableString("Add File or Document to Conversation");
		}

		/// <inheritdoc/>
		public override LocalizableString GetDescription() {
			return new LocalizableString("Adds specified files/documents to the conversation.");
		}

		/// <inheritdoc/>
		public override CopilotActionExecutionResult Execute(ActionExecutionOptions options) {
			Dictionary<string, string> parameterValues = options.ParameterValues;
			if (!parameterValues.TryGetValue("schemaName", out string fileSchemaName) ||
					string.IsNullOrWhiteSpace(fileSchemaName)) {
				throw new ArgumentNullOrEmptyException("schemaName");
			}
			var sessionManager = ClassFactory.Get<ICopilotSessionManager>();
			CopilotSession session = sessionManager.FindById(options.CopilotSessionUId);
			if (session == null) {
				Logger.Error($"Session {options.CopilotSessionUId} not found.");
				return new CopilotActionExecutionResult {
					Status = CopilotActionExecutionStatus.Failed,
					ErrorMessage = "Session not found."
				};
			}
			(List<CreatioAIDocument> documents, int targetContentSize, List<string> errors) = GetDocuments(
				parameterValues, fileSchemaName, options.CopilotSessionUId);
			try {
				_validator.ValidateContentSize(_userConnection, targetContentSize);
			} catch {
				errors.Add("Total file content size exceeded the limit. Files will not be attached.");
				return new CopilotActionExecutionResult {
					Status = CopilotActionExecutionStatus.Failed,
					ErrorMessage = string.Join(Environment.NewLine, errors)
				};
			}
			if (documents == null) {
				return new CopilotActionExecutionResult {
					Status = CopilotActionExecutionStatus.Failed,
					ErrorMessage = string.Join(Environment.NewLine, errors)
				};
			}
			var attachedFilesBuilder = new StringBuilder();
			foreach (CreatioAIDocument document in documents) {
				session.Documents.Add(document);
				if (attachedFilesBuilder.Length > 0) {
					attachedFilesBuilder.Append(_delimiter);
				}
				attachedFilesBuilder.Append($"{document.FileName}");
			}
			UpdateSession(sessionManager, session);
			string documentsAttached = attachedFilesBuilder.ToString();
			string responseMessage = string.Empty;
			if (!string.IsNullOrWhiteSpace(documentsAttached)) {
				responseMessage = $"File(s) {attachedFilesBuilder} attached to conversation.";
			}
			if (errors.Any()) {
				string errorMessage = "Errors: " + string.Join(Environment.NewLine, errors);
				responseMessage = !string.IsNullOrWhiteSpace(responseMessage)
					? responseMessage + Environment.NewLine + errorMessage
					: errorMessage;
			}
			return new CopilotActionExecutionResult {
				Status = CopilotActionExecutionStatus.Completed,
				Response = responseMessage
			};
		}

		/// <inheritdoc />
		public void SetUserConnection(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

	}

	#endregion

}

