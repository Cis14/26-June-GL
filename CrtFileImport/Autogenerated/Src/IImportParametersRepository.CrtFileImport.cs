namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.IO;

	public interface IImportParametersRepository : IEntityRepository<ImportParameters>
	{
		/// <summary>
		/// Update stage of import session
		/// </summary>
		/// <param name="id"></param>
		/// <param name="newImportStage"></param>
		void UpdateImportStage(Guid id, FileImportStagesEnum newImportStage);

		/// <summary>
		/// Delete parameters for import session
		/// </summary>
		/// <param name="importSessionId"></param>
		void Delete(Guid importSessionId);

		/// <summary>
		/// Gets import cancel flag.
		/// </summary>
		/// <param name="importSessionId"></param>
		/// <returns>Import cancel flag.</returns>
		bool GetIsImportSessionCanceled(Guid importSessionId);

		/// <summary>
		/// Update flag canceled of import session
		/// </summary>
		/// <param name="importSessionId">Import session Id.</param>
		/// <param name="newImportStage">Value import cancel flag.</param>
		void CancelImportSession(Guid importSessionId);

		/// <summary>
		/// Update process Id of import session.
		/// </summary>
		/// <param name="importSessionId">Import session Id.</param>
		/// <param name="processId">Import process instance Id.</param>
		void UpdateImportProcessId(Guid importSessionId, Guid processId);

		/// <summary>
		/// Gets import parameters with process incomplete.
		/// </summary>
		/// <returns>Dictionary with key is process instance Id and value is import parameters.</returns>
		Dictionary<Guid, ImportParameters> GetWithProcessIncomplete();

		/// <summary>
		/// Get file from db.
		/// </summary>
		/// <returns>Return file stream.</returns>
		Stream GetFileStream(Guid importSessionId);

		/// <summary>
		/// Delete file from db.
		/// </summary>
		/// <param name="importSessionId">Import session Id.</param>
		void DeleteFile(Guid importSessionId);
	}
}
