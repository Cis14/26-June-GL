namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;

	public interface IImportEntitiesDataProvider
	{

		#region Methods: Public

		/// <summary>
		/// Clears data from BufferedImportEntity table by provided <paramref name="importSessionId" />
		/// </summary>
		/// <param name="importSessionId"></param>
		void CleanBufferedImportEntities(Guid importSessionId);

		/// <summary>
		/// Clears data from BufferedImportEntity table.
		/// </summary>
		void CleanBufferedImportEntities();

		/// <summary>
		/// Delete old import entities.
		/// </summary>
		/// <param name="findImportParametersFunc"></param>
		void CleanOldImportEntitiesRawData(Func<Guid, ImportParameters> findImportParametersFunc);

		/// <summary>
		/// Insert import entities to db.
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="keyColumns"></param>
		/// <returns></returns>
		bool SaveImportEntitiesToDB(ImportParameters parameters, IEnumerable<ImportColumn> keyColumns);

		/// <summary>
		/// Check available key columns count.
		/// </summary>
		/// <param name="keyColumnsCount">Key columns count.</param>
		/// <returns>Result validate.</returns>
		bool ValidateKeyColumnsCount(int keyColumnsCount);

		#endregion

		event EventHandler<ErrorMessageEventArgs> BufferedImportEntitySaveDBError;
	}
}
