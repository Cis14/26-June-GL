namespace Terrasoft.Configuration.FileImport
{
	using System;

	#region  Interface: IFileImportEntitiesChunkProcessor

	public interface IFileImportEntitiesChunkProcessor
	{
		#region Events : Public
		event EventHandler<ImportEntitySavedEventArgs> ImportEntitySaved;

		event EventHandler<ImportEntitySaveErrorEventArgs> ImportEntitySaveError;

		#endregion

		#region Methods: Public

		/// <summary>
		/// Execute import entities by passed parameters.
		/// </summary>
		/// <param name="importParameters"></param>
		void ProcessChunk(ImportParameters importParameters);

		#endregion

	}

	#endregion

}

