namespace Terrasoft.Configuration.FileImport
{
	using System;

	#region Class: ImportParametersIterator

	public static class ImportParametersIterator
	{

		#region Methods: Public

		public static void Iterate(ImportParameters parameters,
				Action<ImportEntity, ImportColumn, ImportColumnValue, ImportColumnDestination> iterator) {
			foreach (ImportEntity importEntity in parameters.Entities) {
				foreach (ImportColumn column in parameters.Columns) {
					ImportColumnValue columnValue = importEntity.FindColumnValue(column);
					if (columnValue == null) {
						continue;
					}
					foreach (ImportColumnDestination destination in column.Destinations) {
						iterator(importEntity, column, columnValue, destination);
					}
				}
			}
		}

		#endregion

	}

	#endregion

}
