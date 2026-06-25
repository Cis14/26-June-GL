namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Factories;
	using Terrasoft.File;
	using Terrasoft.File.Abstractions;
	using Terrasoft.File.Abstractions.TextExtraction;

	#region Class: IDocumentValidator

	/// <summary>
	/// Provides methods to validate document content, extension, and size for Creatio AI documents.
	/// </summary>
	public interface IDocumentValidator
	{
		#region Methods: Public

		/// <summary>
		/// Validates the content size of a file.
		/// Throws an exception if the size exceeds the configured limits or if extraction fails.
		/// </summary>
		/// <param name="userConnection">The user connection instance.</param>
		/// <param name="fileLocator">The locator for the file to validate.</param>
		/// <returns>The size of the file content.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the file content size exceeds the allowed limit or if file content extraction fails.
		/// </exception>
		int ValidateContent(UserConnection userConnection, IFileLocator fileLocator);

		/// <summary>
		/// Gets the total content size of all files in a session.
		/// </summary>
		/// <param name="userConnection">The user connection instance.</param>
		/// <param name="sessionId">The session identifier.</param>
		/// <returns>Total content size in symbols.</returns>
		int GetTotalSessionFilesContentSize(UserConnection userConnection, Guid sessionId);

		/// <summary>
		/// Validates the target content size for a session against system limits.
		/// Throws an exception if the target size exceeds the allowed limit.
		/// </summary>
		/// <param name="userConnection">The user connection instance.</param>
		/// <param name="targetContentSize">Target content size in symbols. Usually is the sum of content of documents
		/// existing in session together with the content of newly added document/documents</param>
		void ValidateContentSize(UserConnection userConnection, int targetContentSize);

		/// <summary>
		/// Validates a document for allowed extension, file size, and content size.
		/// Throws an exception if any validation fails.
		/// </summary>
		/// <param name="userConnection">The user connection instance.</param>
		/// <param name="document">The document to validate.</param>
		/// <param name="totalDocumentSizeInBytes">Optional: total document size in bytes.
		/// Read from database if value not provided.</param>
		/// <returns>Content size in bytes.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown if extension, file size, or content size validation fails.
		/// </exception>
		int ValidateDocument(UserConnection userConnection, ICreatioAIDocument document,
			int? totalDocumentSizeInBytes = null);

		#endregion

	}

	#endregion

	#region Class: DocumentValidator

	/// <inheritdoc />
	public class DocumentValidator : IDocumentValidator
	{

		#region Constants: Private

		private const string SessionFileContentSizeLimitSettingName = "CreatioAISessionFileContentSizeLimit";
		private const int DefaultContentLimit = 60000;
		private const string CreatioAiMaxSessionFileSizeSettingName = "CreatioAIMaxSessionFileSize";
		private const int DefaultMaxSessionFileSizeInMb = 5;
		private const string CreatioAiAllowedFileExtensionsSettingName = "CreatioAIAllowedFileExtensions";
		private const string DefaultAllowedFileExtensions =
			"txt,docx,pdf,htm,css,scss,json,xhtml,md,tex,csv,tsv,log,sql,cs,ts,yaml,yml";

		#endregion

		#region Methods: Private

		private static int GetTotalFilesContentSizeInternal(UserConnection userConnection, Guid sessionId) {
			var select = (Select)new Select(userConnection)
					.Column(Func.Sum("ContentSize")).As("TotalContentSize")
				.From("CreatioAISessionFile").WithHints(Hints.NoLock)
				.Where("SessionId")
					.IsEqual(Column.Parameter(sessionId));
			return select.ExecuteScalar<int>();
		}

		private static int GetFileContentSize(IFileLocator fileLocator, UserConnection userConnection) {
			try {
				var contentExtractor = ClassFactory.Get<ITextContentExtractor>();
				string content = contentExtractor.ExtractText(fileLocator);
				return content.Length;
			} catch (Exception e) {
				string message = userConnection.GetLocalizableString("FileContentExtractionErrorMessage", nameof(DocumentValidator));
				throw new InvalidOperationException(message, e);
			}
		}

		private static void ValidateExtension(UserConnection userConnection, string fileName) {
			string allowedExtensions = SysSettings.GetValue(userConnection, CreatioAiAllowedFileExtensionsSettingName,
				DefaultAllowedFileExtensions);
			HashSet<string> allowedList = allowedExtensions.Split(',')
				.Select(e => e.Trim().ToLowerInvariant())
				.ToHashSet();
			string fileExtension = System.IO.Path.GetExtension(fileName);
			if (string.IsNullOrEmpty(fileExtension) || !allowedList.Contains(
					fileExtension.TrimStart('.').ToLowerInvariant())) {
				string message = userConnection.GetLocalizableString("DocumentNotSupportedExtension", nameof(DocumentValidator))
					.Format(fileName);
				throw new InvalidOperationException(message);
			}
		}

		private static int? GetTotalSize(ICreatioAIDocument document, UserConnection userConnection) {
			int? totalSize = null;
			var esq = new EntitySchemaQuery(userConnection.EntitySchemaManager, document.FileSchemaName);
			esq.AddColumn("TotalSize");
			var entity = esq.GetEntity(userConnection, document.FileId);
			if (entity != null) {
				totalSize = entity.GetTypedColumnValue<int>("TotalSize");
			}
			return totalSize;
		}

		private static void ValidateTotalSize(UserConnection userConnection, ICreatioAIDocument document,
			int? totalDocumentSize) {
			if (totalDocumentSize == null) {
				totalDocumentSize = GetTotalSize(document, userConnection);
			}
			int fileSizeLimitInBytes = SysSettings.GetValue(userConnection, CreatioAiMaxSessionFileSizeSettingName,
				DefaultMaxSessionFileSizeInMb) * 1024 * 1024;
			if (totalDocumentSize.HasValue && totalDocumentSize.Value > fileSizeLimitInBytes) {
				string message = userConnection.GetLocalizableString("DocumentMaxAllowedSize", nameof(DocumentValidator))
					.Format(document.FileName, fileSizeLimitInBytes);
				throw new InvalidOperationException(message);
			}
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public int ValidateContent(UserConnection userConnection, IFileLocator fileLocator) {
			int contentSize = GetFileContentSize(fileLocator, userConnection);
			int contentSizeLimit = SysSettings.GetValue(userConnection, SessionFileContentSizeLimitSettingName,
				DefaultContentLimit);
			if (contentSize > contentSizeLimit) {
				string message = userConnection.GetLocalizableString("SingleFileContentSizeLimitationMessage", nameof(DocumentValidator))
					.Format(contentSizeLimit);
				throw new InvalidOperationException(message);
			}
			return contentSize;
		}

		/// <inheritdoc />
		public int GetTotalSessionFilesContentSize(UserConnection userConnection, Guid sessionId) {
			return GetTotalFilesContentSizeInternal(userConnection, sessionId);
		}

		/// <inheritdoc />
		public void ValidateContentSize(UserConnection userConnection, int targetContentSize) {
			int contentSizeLimit = SysSettings.GetValue(userConnection, SessionFileContentSizeLimitSettingName,
				DefaultContentLimit);
			if (targetContentSize > contentSizeLimit) {
				string message = userConnection.GetLocalizableString("TotalFilesContentSizeLimitationMessage", nameof(DocumentValidator))
					.Format(contentSizeLimit);
				throw new InvalidOperationException(message);
			}
		}

		/// <inheritdoc />
		public int ValidateDocument(UserConnection userConnection, ICreatioAIDocument document,
				int? totalDocumentSizeInBytes = null) {
			ValidateExtension(userConnection, document.FileName);
			ValidateTotalSize(userConnection, document, totalDocumentSizeInBytes);
			try {
				return ValidateContent(userConnection, new EntityFileLocator(document.FileSchemaName, document.FileId));
			} catch (Exception e) {
				string message = userConnection.GetLocalizableString("DocumentContentValidationFailed", nameof(DocumentValidator))
					.Format(document.FileName, e.Message);
				throw new InvalidOperationException(message);
			}
		}

		#endregion

	}

	#endregion

}

