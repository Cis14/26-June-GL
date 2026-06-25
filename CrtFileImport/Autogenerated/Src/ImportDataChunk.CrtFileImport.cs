namespace Terrasoft.Configuration.FileImport
{
	using System;

	#region Enum: ImportChunkState

	public enum ImportChunkState
	{
		ToProcess = 1,
		InProcess = 2,
		Processed = 3
	}

	#endregion

	#region Enum: ImportChunkType

	public enum ImportChunkType
	{
		Lookup = 1,
		Entity = 2
	}

	#endregion

	#region Class: ImportDataChunk

	/// <summary>
	/// Chunk of data import.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class ImportDataChunk<T>
	{
		#region Properties: Public

		/// <summary>
		/// Session Id of import.
		/// </summary>
		public Guid ImportSessionId { get; set; }
		/// <summary>
		/// Chunk Id of data import.
		/// </summary>
		public Guid ChunkId { get; set; }
		/// <summary>
		/// State chunk of import .
		/// </summary>
		public ImportChunkState State { get; set; }
		/// <summary>
		/// Data type of import.
		/// </summary>
		public ImportChunkType Type { get; set; }
		/// <summary>
		/// Data of import.
		/// </summary>
		public T Data { get; set; }

		#endregion
	}

	#endregion

}
