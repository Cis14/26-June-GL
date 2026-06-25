namespace Terrasoft.Configuration.FileImport
{
	using System.Collections.Generic;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Interface: IFileImportFindExistsRowByTextColumnProvider

	/// <summary>
	/// Provides functionality to manage column names for searching existing rows by texts column during file import.
	/// </summary>
	public interface IFileImportFindExistsRowByTextColumnProvider : IFileImportFindExistsRowColumnProvider{}

	#endregion

	#region Class: FileImportFindExistsRowByTextColumnProvider

	/// <inheritdoc cref="IFileImportFindExistsRowByTextColumnProvider"/>
	[DefaultBinding(typeof(IFileImportFindExistsRowByTextColumnProvider))]
	public class FileImportFindExistsRowByTextColumnProvider : BaseFileImportFindExistsRowColumnProvider,
		IFileImportFindExistsRowByTextColumnProvider
	{

		#region Constants: Private

		private const int MaxColumnCount = 50;

		#endregion

		#region Constructors: Public

		public FileImportFindExistsRowByTextColumnProvider(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Methods: Private

		private void CheckKeyColumnsCount(IReadOnlyCollection<ImportColumn> importColumns) {
			if (importColumns.Count <= MaxColumnCount) {
				return;
			}
			ThrowExitedAvailableKeyColumnsLimit("ErrorMessageTemplateForTextColumns", MaxColumnCount);
		}

		#endregion

		#region Methods: Protected

		protected override void InternalInitDbColumnNames(ImportColumn[] importKeyColumns) {
			CheckKeyColumnsCount(importKeyColumns);
			int startColumnIndex = 1;
			importKeyColumns.ForEach(column => {
				column.FindExistsRowsDBColumnName = GetTextColumnName(startColumnIndex++);
			});
		}

		#endregion

	}

	#endregion

}

