using System;

namespace Terrasoft.Configuration.FileImport
{
	public interface IFileImporter : IBaseFileImporter
	{
		/// <summary>
		/// Save import values to db for search entities
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		[Obsolete("7.15.1 | The method is deprecated.")]
		bool SaveRawImportEntities(ImportParameters parameters);

		#region Events: Public

		event EventHandler<ImportEntitySavingEventArgs> ImportEntitySaving;

		event EventHandler<ImportEntitySaveErrorEventArgs> ImportEntitySaveError;

		event EventHandler<BeforeImportEntitiesSaveEventArgs> BeforeImportEntitiesSave;

		event EventHandler<AfterImportEntitiesSaveEventArgs> AfterImportEntitiesSave;

		event EventHandler<InfoMessageEventArgs> ImportEntitiesMerge;

		event EventHandler<ImportEntitySavedEventArgs> ImportEntitySaved;

		event EventHandler<ColumnProcessErrorEventArgs> ColumnProcessError;

		#endregion

	}
}
