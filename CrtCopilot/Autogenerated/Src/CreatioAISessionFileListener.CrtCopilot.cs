namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Common.Logging;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Core.Factories;
	using Terrasoft.File;
	using Terrasoft.File.Abstractions;

	#region Class: CreatioAISessionFileListener

	/// <summary>
	/// Handles events for the <see cref="CreatioAISessionFile"/> entity.
	/// </summary>
	[EntityEventListener(SchemaName = "CreatioAISessionFile")]
	public class CreatioAISessionFileListener : BaseEntityEventListener
	{

		#region Constants: Private

		private const string FileSchemaName = "CreatioAISessionFile";
		private const int ErrorMessageToChatSymbolsLimit = 200;

		#endregion

		#region Fields: Private

		private readonly IDocumentValidator _validator;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CreatioAISessionFileListener"/> class.
		/// </summary>
		public CreatioAISessionFileListener()
			: this(new DocumentValidator()) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CreatioAISessionFileListener"/> class
		/// with a default <see cref="DocumentValidator"/>.
		/// </summary>
		public CreatioAISessionFileListener(IDocumentValidator validator) {
			_validator = validator ?? throw new ArgumentNullOrEmptyException(nameof(validator));
		}

		#endregion

		#region Properties: Private

		private ILog _logger;

		private ILog Logger => _logger ?? (_logger = LogManager.GetLogger("CreatioAISessionFile"));

		#endregion

		#region Methods: Private

		private static EntityFileLocator GetFileLocator(Entity entity) {
			return new EntityFileLocator(entity.SchemaName, entity.PrimaryColumnValue);
		}

		private static CopilotSession GetCopilotSession(ICopilotSessionManager manager, Guid sessionId) {
			CopilotSession session = manager.FindById(sessionId);
			if (session == null) {
				session = manager.CreateSession(CopilotSessionType.Chat, sessionId);
				manager.Add(session);
			}
			return session;
		}

		private static void UpdateSessionOnClient(CopilotSession session) {
			ICopilotMsgChannelSender sender = ClassFactory.Get<ICopilotMsgChannelSender>();
			sender.SendMessages(new CopilotChatPart(((IEnumerable<BaseCopilotMessage>)session.Messages).ToList(),
				session));
		}

		private static void AddDocumentToSession(CopilotSession session, Guid fileId, string fileName) {
			session.Documents.Add(GetDocument(session.Id, fileId, fileName));
			CopilotMessage copilotMessage = CopilotMessage.FromSystem(
				$"File was uploaded {fileName}" + Environment.NewLine + $"#FileId {fileId}; #FileName {fileName}; " +
				$"#SessionId {session.Id};");
			session.AddMessage(copilotMessage);
		}

		private static void UpdateFileEntity(Entity entity, int contentSize) {
			entity.SetColumnValue("ContentSize", contentSize);
			entity.Save(new EntitySaveConfig {
				SetColumnDefValue = false,
				ValidateRequired = false
			});
		}

		private static void UpdateSessionStorage(ICopilotSessionManager manager, CopilotSession session) {
			manager.Update(session, null);
		}

		private static CreatioAIDocument GetDocument(Guid sessionId, Guid fileId, string fileName) {
			return new CreatioAIDocument {
				FileId = fileId,
				FileName = fileName,
				SessionId = sessionId,
				FileSchemaName = FileSchemaName
			};
		}

		private void SafeRemoveFile(EntityFileLocator fileLocator, string fileName, UserConnection userConnection) {
			try {
				IFileFactory fileFactory = userConnection.GetFileFactory();
				IFile file = fileFactory.Get(fileLocator);
				file.Delete();
			} catch (Exception e) {
				Logger.Error($"Error deleting file {fileName}", e);
			}
		}

		private void OnCreatioAISessionFileSaved(Entity entity) {
			UserConnection userConnection = entity.UserConnection;
			string fileName = entity.GetTypedColumnValue<string>("Name");
			Guid sessionId = entity.GetTypedColumnValue<Guid>("SessionId");
			CopilotSession session = null;
			EntityFileLocator fileLocator = GetFileLocator(entity);
			ICopilotSessionManager manager = ClassFactory.Get<ICopilotSessionManager>();
			try {
				session = GetCopilotSession(manager, sessionId);
				int contentSize = _validator.ValidateContent(userConnection, fileLocator);
				int existingContentSize = _validator.GetTotalSessionFilesContentSize(userConnection, sessionId);
				_validator.ValidateContentSize(userConnection, existingContentSize + contentSize);
				UpdateFileEntity(entity, contentSize);
				AddDocumentToSession(session, entity.PrimaryColumnValue, fileName);
				UpdateSessionStorage(manager, session);
				UpdateSessionOnClient(session);
			} catch (Exception e) {
				Logger.Error($"Error adding file {fileName} to session {session?.Id ?? sessionId}," +
					$" actual error: {e.Message}", e);
				SafeRemoveFile(fileLocator, fileName, userConnection);
				string errorMessage = userConnection.GetLocalizableString("FileUploadFailedGeneric", nameof(CreatioAISessionFileListener))
					.Format(fileName, TrimMessage(e.Message));
				if (!TrySendErrorMessageToChat(manager, sessionId, errorMessage)) {
					throw;
				}
			}
		}

		private bool TrySendErrorMessageToChat(ICopilotSessionManager manager, Guid sessionId, string message) {
			try {
				CopilotSession session = GetCopilotSession(manager, sessionId);
				AddErrorMessage(session, message);
				UpdateSessionStorage(manager, session);
				UpdateSessionOnClient(session);
				return true;
			} catch (Exception e) {
				Logger.Error($"Failed to send error message to session {sessionId}," +
					$" actual error: {e.Message}", e);
				return false;
			}
		}

		private string TrimMessage(string message) {
			if (message.Length > ErrorMessageToChatSymbolsLimit) {
				return message.Substring(0, ErrorMessageToChatSymbolsLimit) + "...";
			}
			return message;
		}

		private void AddErrorMessage(CopilotSession session, string message) {
			CopilotMessage copilotMessage = CopilotMessage.FromAssistant(message);
			session.AddMessage(copilotMessage);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Handles entity Saved event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="EntityAfterEventArgs"/> instance containing the event data.</param>
		public override void OnSaved(object sender, EntityAfterEventArgs e) {
			OnCreatioAISessionFileSaved((Entity)sender);
			base.OnSaved(sender, e);
		}

		#endregion

	}

	#endregion

}

