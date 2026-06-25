namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Diagnostics;
	using System.Runtime.Serialization;

	#region Class: ImportColumnValue

	/// <summary>
	/// An instance of this class represents import column value.
	/// </summary>
	[DebuggerDisplay("Value={" + nameof(Value) + "}")]
	[Serializable]
	public class ImportColumnValue
	{

		#region Fields: Public

		/// <summary>
		/// Column index.
		/// </summary>
		[DataMember(Name = "columnIndex")]
		public string ColumnIndex;

		/// <summary>
		/// Row index.
		/// </summary>
		[DataMember(Name = "rowIndex")]
		public string RowIndex;

		#endregion

		#region Properties: Public

		/// <summary>
		/// Value.
		/// </summary>
		[DataMember(Name = "value")]
		public string Value { get; set; }

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="ImportColumnValue"/>.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="columnIndex">Column index.</param>
		/// <param name="rowIndex">Row index.</param>
		public ImportColumnValue(string value, string columnIndex, string rowIndex = "") {
			ColumnIndex = columnIndex;
			Value = value;
			RowIndex = rowIndex;
		}

		#endregion
	}

	#endregion

}
