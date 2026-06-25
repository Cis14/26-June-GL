namespace Terrasoft.Configuration.FileImport
{
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: BaseFileImportFindExistsRowColumnProvider

	/// <summary>
	/// Provides base class manage column names for searching existing rows by texts column during file import.
	/// </summary>
	public abstract class BaseFileImportFindExistsRowColumnProvider : IFileImportFindExistsRowColumnProvider
	{

		#region Fields: Protected

		/// <summary>
		/// User connection.
		/// </summary>
		protected readonly UserConnection UserConnection;

		#endregion

		#region Properties: Private

		private IFileImportInfoLogger _fileImportInfoLogger;
		private IFileImportInfoLogger FileImportInfoLogger =>
			_fileImportInfoLogger ?? (_fileImportInfoLogger = ClassFactory.Get<IFileImportInfoLogger>());

		#endregion

		#region Constructors: Protected

		/// <summary>
		/// Create instance of <see cref="BaseFileImportFindExistsRowColumnProvider"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		protected BaseFileImportFindExistsRowColumnProvider(UserConnection userConnection) {
			UserConnection = userConnection;
		}

		/// <summary>
		/// Create instance of <see cref="BaseFileImportFindExistsRowColumnProvider"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="fileImportInfoLogger">Import logger instance.</param>
		protected BaseFileImportFindExistsRowColumnProvider(UserConnection userConnection,
				IFileImportInfoLogger fileImportInfoLogger): this(userConnection) {
			_fileImportInfoLogger = fileImportInfoLogger;
		}

		#endregion

		#region Methods: Private

		private static ImportColumn[] GetKeyColumns(ImportParameters importParameters) {
			return importParameters.GetKeyColumns().ToArray();
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Generates column name for text columns.
		/// </summary>
		/// <param name="index">Column index.</param>
		/// <returns></returns>
		protected static string GetTextColumnName(int index) {
			return $"Column{index}";
		}

		/// <summary>
		/// Throws new FileImportExceededAvailableKeyColumnsLimitException with passed column count.
		/// </summary>
		/// <param name="templateName">Name of template with an error.</param>
		/// <param name="columnCount">Available column count.</param>
		/// <exception cref="FileImportExceededAvailableKeyColumnsLimitException"></exception>
		protected void ThrowExitedAvailableKeyColumnsLimit(string templateName, int columnCount) {
			throw new FileImportExceededAvailableKeyColumnsLimitException(UserConnection.Workspace.ResourceStorage,
				templateName, columnCount);
		}

		/// <summary>
		/// Determines database column name.
		/// </summary>
		/// <param name="importKeyColumns">Collection on the <see cref="ImportColumn"/>.</param>
		/// <returns>Column name.</returns>
		protected abstract void InternalInitDbColumnNames(ImportColumn[] importKeyColumns);

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IFileImportFindExistsRowColumnProvider.GetDbColumnName"/>
		public string GetDbColumnName(FileImportFindExistsRowColumnInfo columnInfo) {
			columnInfo.CheckArgumentNull(nameof(columnInfo));
			columnInfo.ImportColumn.CheckArgumentNull(nameof(columnInfo.ImportColumn));
			string columnName = columnInfo.ImportColumn.FindExistsRowsDBColumnName;
			return columnName.IsNotNullOrEmpty() ? columnName : GetTextColumnName(columnInfo.ColumnIndex);
		}

		/// <inheritdoc cref="IFileImportFindExistsRowColumnProvider.InitDbColumnNameForKeyColumns"/>
		public void InitDbColumnNameForKeyColumns(ImportParameters importParameters) {
			importParameters.CheckArgumentNull(nameof(importParameters));
			FileImportInfoLogger.WriteSessionActionStartMessage(importParameters.ImportSessionId,
				"BaseFileImportFindExistsRowColumnProvider.InitDbColumnNameForKeyColumns");
			ImportColumn[] importColumns = GetKeyColumns(importParameters);
			InternalInitDbColumnNames(importColumns);
			importParameters.Save();
			FileImportInfoLogger.WriteSessionActionEndMessage(importParameters.ImportSessionId,
				"BaseFileImportFindExistsRowColumnProvider.InitDbColumnNameForKeyColumns");
		}

		#endregion

	}

	#endregion
}
