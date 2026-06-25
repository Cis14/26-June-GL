namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Interface: IFileImportFindExistsRowByTypedColumnsProvider

	/// <summary>
	/// Provides functionality to manage column names for searching existing rows by typed column during file import.
	/// </summary>
	public interface IFileImportFindExistsRowByTypedColumnsProvider : IFileImportFindExistsRowColumnProvider {}

	#endregion

	#region Class: FileImportFindExistsRowByTypedColumnsProvider

	/// <inheritdoc cref="IFileImportFindExistsRowByTypedColumnsProvider"/>
	[DefaultBinding(typeof(IFileImportFindExistsRowByTypedColumnsProvider))]
	public class FileImportFindExistsRowByTypedColumnsProvider : BaseFileImportFindExistsRowColumnProvider,
		IFileImportFindExistsRowByTypedColumnsProvider
	{

		#region Constants: Private

		private const int TypedColumnsCount = 10;

		#endregion

		#region Fields: Private

		private readonly Dictionary<Guid, Guid> _columnDataValueTypeMapping = new Dictionary<Guid, Guid> {
			{ DataValueType.LookupDataValueTypeUId, DataValueType.GuidDataValueTypeUId },
			{ DataValueType.Float0DataValueTypeUId, DataValueType.Float8DataValueTypeUId },
			{ DataValueType.Float1DataValueTypeUId, DataValueType.Float8DataValueTypeUId },
			{ DataValueType.Float2DataValueTypeUId, DataValueType.Float8DataValueTypeUId },
			{ DataValueType.Float3DataValueTypeUId, DataValueType.Float8DataValueTypeUId },
			{ DataValueType.Float4DataValueTypeUId, DataValueType.Float8DataValueTypeUId },
			{ DataValueType.FloatDataValueTypeUId, DataValueType.Float8DataValueTypeUId },
			{ DataValueType.Money0DataValueTypeUId, DataValueType.Float8DataValueTypeUId },
			{ DataValueType.Money1DataValueTypeUId, DataValueType.Float8DataValueTypeUId },
			{ DataValueType.Money3DataValueTypeUId, DataValueType.Float8DataValueTypeUId },
			{ DataValueType.MoneyDataValueTypeUId, DataValueType.Float8DataValueTypeUId },
			{ DataValueType.ShortTextDataValueTypeUId, DataValueType.MaxSizeTextDataValueTypeUId },
			{ DataValueType.TextDataValueTypeUId, DataValueType.MaxSizeTextDataValueTypeUId },
			{ DataValueType.MediumTextDataValueTypeUId, DataValueType.MaxSizeTextDataValueTypeUId },
			{ DataValueType.LongTextDataValueTypeUId, DataValueType.MaxSizeTextDataValueTypeUId },
			{ DataValueType.EmailTextDataValueTypeUId, DataValueType.MaxSizeTextDataValueTypeUId },
			{ DataValueType.ColorDataValueTypeUId, DataValueType.MaxSizeTextDataValueTypeUId },
			{ DataValueType.PhoneTextDataValueTypeUId, DataValueType.MaxSizeTextDataValueTypeUId },
			{ DataValueType.RichTextDataValueTypeUId, DataValueType.MaxSizeTextDataValueTypeUId },
			{ DataValueType.WebTextDataValueTypeUId, DataValueType.MaxSizeTextDataValueTypeUId },
		};

		#endregion

		#region Constructors: Public

		public FileImportFindExistsRowByTypedColumnsProvider(UserConnection userConnection): base(userConnection) {
		}

		#endregion

		#region Methods: Private

		private Guid GetColumnDataValueTypeUId(ImportColumn keyColumn) {
			Guid columnDataValueType = keyColumn.Destinations.First().FindColumnTypeUId();
			return _columnDataValueTypeMapping.TryGetValue(columnDataValueType, out Guid dataValueTypeUId)
				? dataValueTypeUId
				: columnDataValueType;
		}

		private Dictionary<Guid, List<string>> GetColumnsNamesByTypes() {
			EntitySchema entitySchema = UserConnection.EntitySchemaManager.GetInstanceByName("BufferedImportEntity");
			return entitySchema.Columns.Where(column => column.Name.StartsWith("Column"))
				.GroupBy(column => column.DataValueType.UId)
				.ToDictionary(group => group.Key,
					group => group.Select(column => column.Name).ToList());
		}

		private void SetColumnName(Dictionary<Guid, List<string>> columnsNamesByTypes, ImportColumn keyColumn) {
			List<string> availableColumnNames = columnsNamesByTypes[GetColumnDataValueTypeUId(keyColumn)];
			string columnName = availableColumnNames.FirstOrDefault();
			if (columnName == null) {
				ThrowExitedAvailableKeyColumnsLimit("ErrorMessageTemplateForTypedColumns", TypedColumnsCount);
			}
			availableColumnNames.RemoveAt(0);
			keyColumn.FindExistsRowsDBColumnName = columnName;
		}

		#endregion

		#region Methods: Public

		protected override void InternalInitDbColumnNames(ImportColumn[] importKeyColumns) {
			Dictionary<Guid, List<string>> columnsNamesByTypes = GetColumnsNamesByTypes();
			importKeyColumns.ForEach(keyColumn => SetColumnName(columnsNamesByTypes, keyColumn));
		}

		#endregion

	}

	#endregion

}
