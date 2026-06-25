namespace Terrasoft.Configuration.FileImport {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: DefaultFileImportHeadersProcessor

	/// <inheritdoc cref="BaseFileImportHeadersCreator"/>
	/// <summary>
	/// Provides default class for process headers.
	/// </summary>
	public class DefaultFileImportHeadersProcessor : BaseFileImportHeadersCreator
	{

		#region Constructors: Public

		public DefaultFileImportHeadersProcessor(UserConnection userConnection)
			: base(userConnection) { }

		#endregion

		#region Methods: Protected

		/// <summary>
		/// <inheritdoc cref="BaseFileImportHeadersCreator"/> 
		/// </summary>
		protected override EntitySchemaColumn FindColumn(IEnumerable<EntitySchemaColumn> rootSchemaColumns, string columnValue) {
			return rootSchemaColumns.FirstOrDefault(column => string.Equals(column.Caption, columnValue, StringComparison.OrdinalIgnoreCase));
		}

		#endregion

	}

	#endregion
}
