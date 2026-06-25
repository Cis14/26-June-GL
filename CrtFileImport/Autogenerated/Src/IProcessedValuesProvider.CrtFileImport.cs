namespace Terrasoft.Configuration.FileImport
{
	public interface IProcessedValuesProvider
	{
		/// <summary>
		/// Finds processed value.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <param name="columnValue">Import column value.</param>
		/// <returns>Processed value.</returns>
		object FindValueForSave(ImportColumnDestination destination, ImportColumnValue columnValue);

	}
}
