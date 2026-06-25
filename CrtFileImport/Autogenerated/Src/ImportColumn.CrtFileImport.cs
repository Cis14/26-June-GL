namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Runtime.Serialization;

	#region  Class: ImportColumn

	[DataContract]
	[DebuggerDisplay("Index={Index}; Source={Source}")]
	[Serializable]
	public class ImportColumn
	{
		#region  Fields: Private

		private List<ImportColumnDestination> _destinations;

		#endregion

		#region  Fields: Public

		[DataMember(Name = "index")]
		public string Index;

		[DataMember(Name = "source")]
		public string Source;

		/// <summary>
		/// Provide database column name for search exists records.
		/// </summary>
		[DataMember(Name = "findExistsRowsDBColumnName")]
		public string FindExistsRowsDBColumnName;

		#endregion

		#region Properties: Public

		[DataMember(Name = "destinations")]
		public List<ImportColumnDestination> Destinations {
			get => _destinations ?? (_destinations = new List<ImportColumnDestination>());
			set => _destinations = value;
		}

		#endregion

		#region Methods: Private

		private int ExcelColumnNameToNumber(string columnName) {
			if (string.IsNullOrEmpty(columnName)) {
				throw new ArgumentNullException(nameof(columnName));
			}
			columnName = columnName.ToUpperInvariant();
			var sum = 0;
			foreach (var t in columnName) {
				sum *= 26;
				sum += t - 'A' + 1;
			}
			return sum;
		}

		#endregion

		#region Methods: Public

		public int GetNumberedIndex() => ExcelColumnNameToNumber(Index);

		/// <summary>
		/// Get destination for root EntitySchema and where import key column is true.
		/// </summary>
		/// <param name="rootEntitySchemaUid">Root EntitySchema ID.</param>
		/// <returns></returns>
		public IEnumerable<ImportColumnDestination> GetDestinationRootSchemaKeyColumns(Guid rootEntitySchemaUid) {
			return Destinations.Where(d => d.IsKey && d.SchemaUId.Equals(rootEntitySchemaUid));
		}

		#endregion
	}

	#endregion
}

