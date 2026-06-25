namespace Terrasoft.Configuration.FileImport
{
	using Terrasoft.Core;

	#region Class: ImportEntityColumnValueInfo

	/// <summary>
	/// Designed to encapsulate information about a specific column value during the import process.
	/// </summary>
	internal class ImportEntityColumnValueInfo
	{

		#region Properties: Public

		/// <summary>
		/// Import entity column name.
		/// </summary>
		public string ColumnName { get; set; }

		/// <summary>
		/// Import entity column value.
		/// </summary>
		public object ColumnValue { get; set; }

		/// <summary>
		/// Import entity column data type.
		/// </summary>
		public DataValueType ColumnDataType { get; set; }

		/// <summary>
		/// Import entity source column name.
		/// </summary>
		public string SourceColumnName { get; set; }

		#endregion

	}

	#endregion

}
