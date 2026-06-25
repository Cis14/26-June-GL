namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;

	#region Interface: IImportEntitiesChunksDataProvider

	/// <summary>
	/// Import data provider for <see cref="Terrasoft.Configuration.FileImport.EntityChunkData" />.
	/// </summary>
	public interface IImportEntitiesChunksDataProvider: IImportChunkDataProvider<ImportDataChunk<EntityChunkData>> {

		#region Methods

		/// <summary>
		/// Save information about processing row result.
		/// </summary>
		 void SaveProcessedRow(Guid chunkId, int rowIndex, bool success = true);

		/// <summary>
		/// Retrieve collection with information about processing rows result.
		/// </summary>
		IEnumerable<ProcessedRowResult> GetProcessedRows(Guid chunkId);

		/// <summary>
		/// Remove information about processing row result.
		/// </summary>
		void RemoveProcessedRows(Guid chunkId);

		#endregion

	}

	#endregion
}
