namespace Terrasoft.Configuration.FileImport
{
	using System;

	#region  Class: LookupValuesEntitySchemaColumn

	/// <summary>
	/// An instance of this class contains name and entity schema unique identifier of lookup entity column.
	/// </summary>
	public class LookupValuesEntitySchemaColumn
	{
		#region  Fields: Public

		/// <summary>
		/// Lookup entity unique identifier.
		/// </summary>
		public Guid EntitySchemaUId;

		/// <summary>
		/// Lookup entity column name.
		/// </summary>
		public string Name;

		#endregion

		#region Constructors: Public

		public LookupValuesEntitySchemaColumn() { }

		public LookupValuesEntitySchemaColumn(Guid entitySchemaUId, string name) {
			EntitySchemaUId = entitySchemaUId;
			Name = name;
		}

		#endregion
	}

	#endregion
}

