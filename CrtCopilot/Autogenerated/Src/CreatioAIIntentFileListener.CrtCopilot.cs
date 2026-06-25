namespace Creatio.Copilot
{
	using System;
	using Common.Logging;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Core.Factories;
	using Terrasoft.File;
	using Terrasoft.File.Abstractions;
	using Terrasoft.File.Abstractions.TextExtraction;
	using SystemSettings = Terrasoft.Core.Configuration.SysSettings;

	#region Class: CreatioAIIntentFileListener

	/// <summary>
	/// Handles events for the <see cref="CreatioAIIntentFile"/> entity.
	/// </summary>
	[EntityEventListener(SchemaName = "CreatioAIIntentFile")]
	public class CreatioAIIntentFileListener : BaseEntityEventListener
	{

		#region Constants: Private

		private const string FileContentSizeLimitSettingName = "CreatioAIIntentFileContentSizeLimit";
		private const int DefaultContentLimit = 60000;

		#endregion

		#region Properties: Private

		private ILog _logger;
		private ILog Logger => _logger ?? (_logger = LogManager.GetLogger("CreatioAIIntentFile"));

		#endregion

		#region Methods: Private

		private static EntityFileLocator GetFileLocator(Entity entity) {
			var fileLocator = new EntityFileLocator("CreatioAIIntentFile", entity.PrimaryColumnValue);
			return fileLocator;
		}

		private static int GetTotalFilesContentSize(Guid intentUId, UserConnection userConnection) {
			var select =
				new Select(userConnection)
					.Column(Func.Sum("ContentSize")).As("TotalContentSize")
				.From("CreatioAIIntentFile").WithHints(Hints.NoLock)
				.Where("IntentUId")
					.IsEqual(Column.Parameter(intentUId)) as Select;
			return select.ExecuteScalar<int>();
		}

		private static int GetFileContentSize(IFileLocator fileLocator, UserConnection userConnection) {
			try {
				var contentExtractor = ClassFactory.Get<ITextContentExtractor>();
				var content = contentExtractor.ExtractText(fileLocator);
				return content.Length;
			} catch (Exception e) {
				string message = userConnection.GetLocalizableString("FileContentExtractionErrorMessage", nameof(CreatioAIIntentFileListener));
				throw new InvalidOperationException(message, e);
			}
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

		private void OnCreatioAIIntentFileSaved(Entity entity) {
			UserConnection userConnection = entity.UserConnection;
			string fileName = entity.GetTypedColumnValue<string>("Name");
			Guid intentUId = entity.GetTypedColumnValue<Guid>("IntentUId");
			var fileLocator = GetFileLocator(entity);
			try {
				int contentSize = GetFileContentSize(fileLocator, userConnection);
				int contentSizeLimit = SystemSettings.GetValue(userConnection, FileContentSizeLimitSettingName,
					DefaultContentLimit);
				if (contentSize > contentSizeLimit) {
					string message = userConnection.GetLocalizableString("SingleFileContentSizeLimitationMessage", nameof(CreatioAIIntentFileListener))
						.Format(contentSizeLimit);
					throw new InvalidOperationException(message);
				}
				int totalContentSize = GetTotalFilesContentSize(intentUId, userConnection);
				if (totalContentSize + contentSize > contentSizeLimit) {
					string message = userConnection.GetLocalizableString("TotalFilesContentSizeLimitationMessage", nameof(CreatioAIIntentFileListener))
						.Format(contentSizeLimit);
					throw new InvalidOperationException(message);
				}
				entity.SetColumnValue("ContentSize", contentSize);
				entity.Save(new EntitySaveConfig {
					SetColumnDefValue = false,
					ValidateRequired = false
				});
			} catch (Exception e) {
				Logger.Error($"Error extracting text content from file {fileName}", e);
				SafeRemoveFile(fileLocator, fileName, userConnection);
				throw;
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Handles entity Saved event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="EntityAfterEventArgs"/> instance containing the event data.</param>
		public override void OnSaved(object sender, EntityAfterEventArgs e) {
			OnCreatioAIIntentFileSaved((Entity)sender);
			base.OnSaved(sender, e);
		}

		#endregion

	}

	#endregion

}
