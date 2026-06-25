namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;

	#region Interface: Public

	/// <summary>
	/// Import data provider.
	/// </summary>
	/// <typeparam name="T">Type is any class.</typeparam>
	public interface IImportChunkDataProvider<T> where T: class
	{

		#region Methods

		/// <summary>
		/// Add specified import data.
		/// </summary>
		/// <param name="datasChunks">Import data chunks.</param>
		/// <returns>Count rows who been inserted.</returns>
		int Add(IEnumerable<T> datasChunks);

		/// <summary>
		/// Split data on chunks and save they in db.
		/// </summary>
		/// <param name="importParameters"><see cref="ImportParameters"/></param>
		void CreateDataChunks(ImportParameters importParameters);

		/// <summary>
		/// Get specified import data.
		/// </summary>
		/// <param name="importSessionId">Import session Id.</param>
		/// <returns>Get class type objects.</returns>
		IEnumerable<T> Get(Guid importSessionId);

		/// <summary>
		/// Chunk exists in db any chunks for specified import session.
		/// </summary>
		/// <param name="importSessionId">Import session Id.</param>
		/// <returns>True if for specified import session exists any chunk, else false.</returns>
		bool GetIsChunksExists(Guid importSessionId);

		/// <summary>
		/// Update specified import data.
		/// </summary>
		/// <param name="chunk">Import data chunk.</param>
		/// <returns>Flag, was row updated?</returns>
		bool Update(T chunk);

		/// <summary>
		/// Delete specified import data.
		/// </summary>
		/// <param name="importSessionId">Import data chunk.</param>
		void Delete(Guid importSessionId);
		
		#endregion

	}

	#endregion

}
