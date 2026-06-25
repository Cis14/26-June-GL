namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Configuration.Translation;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: SysTranslationFileImportHeadersProcessor

	///<inheritdoc cref="BaseFileImportHeadersCreator"/>
	/// <summary>
	/// Provides class for process headers of SysTranslation.
	/// </summary>
	public class SysTranslationFileImportHeadersProcessor : BaseFileImportHeadersCreator
	{

		#region Fields: Private

		private IEnumerable<TranslationColumnConfigure> _columnsConfigured;

		#endregion

		#region Properties: Private

		private IEnumerable<TranslationColumnConfigure> ColumnsConfigured {
			get {
				if(_columnsConfigured == null) {
					var configurator = ClassFactory.Get<ISysTranslationColumnsConfigurator>(new ConstructorArgument("userConnection", UserConnection));
					_columnsConfigured = configurator.GetTranslationColumnsConfigured();
				}
				return _columnsConfigured;
			}
		}

		#endregion

		#region Constructors: Public

		public SysTranslationFileImportHeadersProcessor(UserConnection userConnection)
			: base(userConnection) { }

		#endregion

		#region Methods: Private

		private EntitySchemaColumn FindColumnByName(IEnumerable<EntitySchemaColumn> rootSchemaColumns, string columnValue) {
			return rootSchemaColumns.FirstOrDefault(entityColumn =>
				string.Equals(entityColumn.Name, columnValue, StringComparison.CurrentCultureIgnoreCase));
		}

		private EntitySchemaColumn FindColumnByCaption(IEnumerable<TranslationColumnConfigure> columnsWithCultureName,
			IEnumerable<EntitySchemaColumn> rootSchemaColumns, string value) {
			TranslationColumnConfigure configuredColumn = columnsWithCultureName.FirstOrDefault(column =>
				column.Caption.Equals(value, StringComparison.OrdinalIgnoreCase));
			return configuredColumn == null ? null : FindColumnByName(rootSchemaColumns, configuredColumn.Name);
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// <inheritdoc cref="BaseFileImportHeadersCreator"/> 
		/// </summary>
		protected override EntitySchemaColumn FindColumn(IEnumerable<EntitySchemaColumn> rootSchemaColumns, string columnValue) {
			return FindColumnByName(rootSchemaColumns, columnValue) ??
				FindColumnByCaption(ColumnsConfigured, rootSchemaColumns, columnValue);
		}

		#endregion

	}

	#endregion

}

