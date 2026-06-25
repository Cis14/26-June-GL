namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;

	#region  Class: LookupColumnValues

	/// <summary>
	/// An instance of this class contains lookup identifier and its values.
	/// </summary>
	public class LookupColumnValues<T> : Dictionary<string, T>
	{
		#region Constants: Private

		private readonly Func<Guid, string, string> _getKeyFunc;

		#endregion

		#region Constructors: Public

		public LookupColumnValues(Func<Guid, string, string> geyGetKeyFunc) => _getKeyFunc = geyGetKeyFunc;

		#endregion

		#region Methods: Public

		/// <summary>
		/// Adds new lookup values using specific key parameters.
		/// </summary>
		/// <param name="entitySchemaUId">Entity schema unique identifier.</param>
		/// <param name="columnName">Entity schema column name.</param>
		/// <param name="value">Value.</param>
		public void Add(Guid entitySchemaUId, string columnName, T value) {
			Add(_getKeyFunc(entitySchemaUId, columnName), value);
		}

		/// <summary>
		/// Checks if lookup values contain value using specific key parameters.
		/// </summary>
		/// <param name="entitySchemaUId">Entity schema unique identifier.</param>
		/// <param name="columnName">Entity schema column name.</param>
		/// <returns>Determines if lookup values contain value.</returns>
		public bool ContainsKey(Guid entitySchemaUId, string columnName) =>
				ContainsKey(_getKeyFunc(entitySchemaUId, columnName));

		/// <summary>
		/// Gets lookup values using specific key parameters.
		/// </summary>
		/// <param name="entitySchemaUId">Entity schema unique identifier.</param>
		/// <param name="columnName">Entity schema column name.</param>
		/// <returns>Lookup values</returns>
		public T GetValue(Guid entitySchemaUId, string columnName) => base[_getKeyFunc(entitySchemaUId, columnName)];

		#endregion
	}

	#endregion
}

