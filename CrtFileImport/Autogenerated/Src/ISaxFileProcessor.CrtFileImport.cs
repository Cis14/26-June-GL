namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;

	#region Interface: ISaxFileProcessor

	public interface ISaxFileProcessor
	{
		#region Methods: Public

		/// <summary>
		/// Processes given list headers.
		/// </summary>
		/// <param name="importParametersId">Index session.</param>
		/// <returns>List headers.</returns>
		IEnumerable<ImportColumn> GetHeaders(Guid importParametersId);

		/// <summary>
		/// Check that file contains headers and rows.
		/// </summary>
		/// <param name="importParametersId"></param>
		/// <returns></returns>
		void CheckIsFileValid(Guid importParametersId);

		/// <summary>
		/// Process given list entities from excel.
		/// </summary>
		/// <param name="importParametersId">Index session.</param>
		/// <returns>List entities.</returns>
		IEnumerable<ImportEntity> ReadEntities(Guid importParametersId);

		/// <summary>
		/// Set up root schema by id.
		/// </summary>
		/// <param name="rootSchemaUId"></param>
		void InitRootSchema(Guid rootSchemaUId);

		#endregion

	}

	#endregion

}
