namespace Terrasoft.Configuration.FileImport
{
	using Terrasoft.Core;

	#region Class: TextColumnHelper

	/// <summary>
	/// Contains text columns utility methods.
	/// </summary>
	public static class TextColumnHelper
	{

		#region Methods: Public

		/// <summary>
		/// Prepares text column value. Shortens value if it does not fit the column.
		/// </summary>
		/// <param name="dataValueType">Data value type.</param>
		/// <param name="value">Column value.</param>
		/// <returns>Prepared text column value.</returns>
		public static string PrepareTextColumnValue(TextDataValueType dataValueType, string value) {
			if (!dataValueType.IsMaxSize && value.Length > dataValueType.Size) {
				value = value.Substring(0, dataValueType.Size);
			}
			return value.Trim();
		}

		#endregion

	}

	#endregion

}
