namespace Terrasoft.Configuration.FileImport {

	using System;
	using Terrasoft.Common;

	#region Class: FileImportExceededAvailableKeyColumnsLimitException

	/// <summary>
	/// Exception is thrown when the maximum limit of available key columns is exceeded during file import operations.
	/// </summary>
	public class FileImportExceededAvailableKeyColumnsLimitException : ApplicationException
	{

		#region Constructors: Public

		public FileImportExceededAvailableKeyColumnsLimitException(IResourceStorage storage, string templateName,
			int columnCount): base(string.Format(new LocalizableString(storage,
			"FileImportExceededAvailableKeyColumnsLimitException",
			$"LocalizableStrings.{templateName}.Value"), columnCount)) {
		}

		#endregion

	}

	#endregion

}
