namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;

	public interface IBaseFileImporter
	{

		#region Methods: Public


		/// <summary>
		/// Gets file import parameters.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <returns>File import parameters.</returns>
		ImportParameters GetImportParameters(Guid importSessionId);
		
		/// <summary>
		/// Finds file import parameters.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <returns>File import parameters.</returns>
		ImportParameters FindImportParameters(Guid importSessionId);

		/// <summary>
		/// Returns columns mapping parameters.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <returns>Columns mapping parameters.</returns>
		ColumnsMappingResponse GetColumnsMappingParameters(Guid importSessionId);

		/// <summary>
		/// Starts import.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		void Import(Guid importSessionId);

		/// <summary>
		/// Starts import.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		void ImportWithParams(ImportParameters parameters);

		/// <summary>
		/// Processes file and saves its information to cache.
		/// </summary>
		void SaveFile();

		/// <summary>
		/// Sets tags.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <param name="tags">Import tag names.</param>
		void SaveImportTags(Guid importSessionId, List<ImportTag> tags);

		/// <summary>
		/// Sets new columns mapping parameters.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <param name="columns">New columns mapping parameters.</param>
		void SetColumnsMappingParameters(Guid importSessionId, List<ImportColumn> columns);

		/// <summary>
		/// Sets file info.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <param name="fileName">File name.</param>
		void SetFileInfo(Guid importSessionId, string fileName);

		/// <summary>
		/// Sets import object.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <param name="importObject">Import object.</param>
		void SetImportObject(Guid importSessionId, ImportObject importObject);

		/// <summary>
		/// Validate imports settings.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		/// <returns>Result validate.</returns>
		bool Validate(Guid importSessionId);

		/// <summary>
		/// Refresh column mapping.
		/// </summary>
		/// <param name="importSessionId">Import session identifier.</param>
		void RefreshColumnMapping(Guid importSessionId);

		#endregion

	}
}
