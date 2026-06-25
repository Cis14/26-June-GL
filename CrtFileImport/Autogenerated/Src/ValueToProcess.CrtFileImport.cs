namespace Terrasoft.Configuration.FileImport
{
	public class ValueToProcess
	{
		#region Constructor: Public
		public ValueToProcess(uint rowIndex, string columnIndex, string value) {
			RowIndex = rowIndex;
			ColumnIndex = columnIndex;
			Value = value;
		}

		#endregion

		#region Properties: Public

		public string Value { get; }
		public string ColumnIndex { get; }
		public uint RowIndex { get; }

		#endregion

	}
}

