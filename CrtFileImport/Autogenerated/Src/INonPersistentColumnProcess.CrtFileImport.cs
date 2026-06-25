namespace Terrasoft.Configuration.FileImport
{
	public interface INonPersistentColumnProcess
	{
		#region Methods: Public

		/// <summary>
		/// Adds raw value that will be processed.
		/// </summary>
		/// <param name="importEntity">Import entity.</param>
		/// <param name="column">Import column.</param>
		/// <param name="columnValue">Import column value.</param>
		/// <param name="destination">Import column destination.</param>
		void Add(ImportEntity importEntity, ImportColumn column, ImportColumnValue columnValue,
			ImportColumnDestination destination);

		/// <summary>
		/// Runs column values processing.
		/// </summary>
		void Process();

		#endregion

	}
}
