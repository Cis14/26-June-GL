namespace Terrasoft.Configuration.FileImport
{
	using DocumentFormat.OpenXml.Packaging;
	using DocumentFormat.OpenXml.Spreadsheet;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: ExcelBaseProcess

	public class ExcelBaseProcess {

		#region Fields: Private

		private readonly Regex _cellColumnIndexRegex = new Regex("[A-Za-z]+");
		private readonly Regex _cellRowIndexRegex = new Regex("[0-9]+");
		private bool _sharedStringItemsValuesInitialized;
		private List<string> _sharedStringItemsValues;
		private INonPersistentColumnsAggregator _columnsProcessor;

		#endregion

		#region Constructors: Public

		public ExcelBaseProcess(UserConnection userConnection) => UserConnection = userConnection;

		#endregion

		#region Fields: Private

		private Guid _rootSchemaUId;
		private EntitySchema _rootEntitySchema;
		private IColumnsAggregatorFactory _columnsAggregatorFactory;
		
		#endregion

		#region Fields: Protected

		protected readonly string replaceableSymbol = "_x000D_";

		/// <summary>
		/// Shared string items values.
		/// </summary>
		protected List<string> SharedStringItemsValues {
			get {
				if (!_sharedStringItemsValuesInitialized) {
					IEnumerable<SharedStringItem> sharedStringItems = GetSharedStringItems(WorkbookPart);
					_sharedStringItemsValues = GetSharedStringItemsValues(sharedStringItems);
					_sharedStringItemsValuesInitialized = true;
				}
				return _sharedStringItemsValues;
			}
		}

		#endregion
		
		#region Properties: Private

		private IColumnsAggregatorFactory ColumnsAggregatorFactory => _columnsAggregatorFactory ??
			(_columnsAggregatorFactory = ClassFactory.Get<IColumnsAggregatorFactory>());

		#endregion
		
		#region Properties: Protected

		/// <summary>
		/// User connection.
		/// </summary>
		protected UserConnection UserConnection { get; }

		private WorkbookPart _workbookPart;
		/// <summary>
		/// Workbook part.
		/// </summary>
		protected WorkbookPart WorkbookPart {
			get => _workbookPart;
			set {
				_sharedStringItemsValuesInitialized = false;
				_workbookPart = value;
			}
		}

		/// <summary>
		/// Root schema.
		/// </summary>
		protected EntitySchema RootSchema {
			get {
				if (_rootEntitySchema != null) {
					return _rootEntitySchema;
				}
				SetRootEntitySchema(_rootSchemaUId);
				return _rootEntitySchema;
			}
		}

		/// <summary>
		/// Columns processor.
		/// </summary>
		[Obsolete("7.15.4 | Method is not in use and will be removed in upcoming builds. " +
			"Use instead \"FileImportHeadersProcessor\"")]
		protected INonPersistentColumnsAggregator ColumnsProcessor => _columnsProcessor ??
			(_columnsProcessor = ColumnsAggregatorFactory?.GetColumnsAggregator(UserConnection));
		
		#endregion

		#region Methods: Private

		/// <summary>
		/// Gets cell column index.
		/// </summary>
		/// <param name="regexExpression"></param>
		/// <param name="cell">Cell.</param>
		/// <returns>Cell column index.</returns>
		private string GetCellIndex(Regex regexExpression, Cell cell) {
			Match match = regexExpression.Match(cell.CellReference.Value);
			return match.Value;
		}

		/// <summary>
		/// Processes cell value.
		/// </summary>
		/// <param name="value">Text value.</param>
		/// <returns>Processed value.</returns>
		private string ProcessCellValue(string value) {
			bool success = double.TryParse(value, System.Globalization.NumberStyles.AllowDecimalPoint
				| System.Globalization.NumberStyles.AllowExponent,
				System.Globalization.CultureInfo.InvariantCulture, out double doubleValue);
			return (success) ? doubleValue.ToString() : value;
		}

		/// <summary>
		/// Gets shared cell value.
		/// </summary>
		/// <param name="cell">Cell.</param>
		/// <returns>Shared cell value.</returns>
		private string GetSharedCellValue(Cell cell) {
			int itemIndex = int.Parse(cell.CellValue.Text);
			return SharedStringItemsValues[itemIndex];
		}

		/// <summary>
		/// Gets a collection of shared string table items.
		/// </summary>
		/// <param name="workbookPart">Workbook part.</param>
		/// <returns>A collection of shared string table items.</returns>
		private IEnumerable<SharedStringItem> GetSharedStringItems(WorkbookPart workbookPart) {
			SharedStringTablePart sharedStringTablePart = workbookPart.SharedStringTablePart;
			SharedStringTable sharedStringTable = sharedStringTablePart.SharedStringTable;
			return sharedStringTable.Elements<SharedStringItem>();
		}

		/// <summary>
		/// Gets shared string items values.
		/// </summary>
		/// <param name="sharedStringItems">Shared string items.</param>
		/// <returns>Shared string items values.</returns>
		private List<string> GetSharedStringItemsValues(IEnumerable<SharedStringItem> sharedStringItems) {
			List<string> sharedStringItemValues = new List<string>();
			foreach (SharedStringItem item in sharedStringItems) {
				sharedStringItemValues.Add(item.InnerText);
			}
			return sharedStringItemValues;
		}
		
		private void SetRootEntitySchema(Guid rootSchemaUId) {
			_rootSchemaUId = rootSchemaUId;
			_rootEntitySchema = UserConnection.EntitySchemaManager.GetInstanceByUId(_rootSchemaUId);
		}
		
		#endregion

		#region Methods: Protected
		
		/// <summary>
		/// Processes data header.
		/// </summary>
		/// <returns>A collection of import columns.</returns>
		protected IEnumerable<ImportColumn> ProcessHeader(IEnumerable<ImportColumnValue> columnValues) {
			IFileImportHeadersProcessorFactory processorFactory  = ClassFactory.Get<FileImportHeadersProcessorFactory>();
			IFileImportHeadersCreator headersProcessor = processorFactory.GetProcessor(UserConnection, RootSchema);
			return headersProcessor.CreateHeaderColumns(RootSchema, columnValues);
		}
		
 		#endregion
		
		#region Methods: Public

		/// <summary>
		/// Gets header cells.
		/// </summary>
		/// <param name="firstRow">First row from table.</param>
		/// <returns>A collection of header cells.</returns>
		public IEnumerable<Cell> GetHeader(Row firstRow) {
			if (firstRow == null) {
				throw new EmptyHeaderException(UserConnection.Workspace.ResourceStorage);
			}
			IEnumerable<Cell> headerCells = firstRow.Elements<Cell>();
			if (!headerCells.Any()) {
				throw new EmptyHeaderException(UserConnection.Workspace.ResourceStorage);
			}
			return headerCells;
		}

		/// <summary>
		/// Processes data row.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <returns>A collection of row columns.</returns>
		public IEnumerable<ImportColumnValue> ProcessRow(Row row) {
			IEnumerable<Cell> cells = row.Elements<Cell>();
			return ProcessCells(cells);
		}

		/// <summary>
		/// Processes data cells.
		/// </summary>
		/// <param name="cells">Cells.</param>
		/// <returns>A collection of cells values.</returns>
		public IEnumerable<ImportColumnValue> ProcessCells(IEnumerable<Cell> cells) {
			List<ImportColumnValue> columnValues = new List<ImportColumnValue>();
			foreach (Cell cell in cells) {
				var columnValue = ProcessCell(cell);
				if (columnValue.Value.IsNotNullOrEmpty()) {
					columnValue.Value = columnValue.Value.Replace(replaceableSymbol, String.Empty);
					columnValues.Add(columnValue);
				}
			}
			return columnValues;
		}

		/// <summary>
		/// Processes data cell.
		/// </summary>
		/// <param name="cell">Cell.</param>
		/// <returns>Cell information.</returns>
		public ImportColumnValue ProcessCell(Cell cell) {
			string cellValue = GetCellValue(cell);
			string cellColumnIndex = GetCellIndex(_cellColumnIndexRegex, cell);
			string rowIndex = GetCellIndex(_cellRowIndexRegex, cell);
			return new ImportColumnValue(cellValue, cellColumnIndex, rowIndex);
		}

		/// <summary>
		/// Gets cell value.
		/// </summary>
		/// <param name="cell">Cell.</param>
		/// <returns>Cell value.</returns>
		public string GetCellValue(Cell cell) {
			if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString) {
				return GetSharedCellValue(cell);
			}
			return (cell.CellValue != null) ? ProcessCellValue(cell.CellValue.Text) : null;
		}

		/// <summary>
		/// Determines if columns values collection is empty.
		/// </summary>
		/// <param name="columnsValues">A collection of column values.</param>
		/// <returns>
		/// <c>true</c> if collection <paramref name="columnsValues"/> is empty.
		/// </returns>
		public bool IsColumnsValuesEmpty(IEnumerable<ImportColumnValue> columnsValues) {
			return columnsValues
				.All(columnValue => columnValue.Value.IsNullOrEmpty());
		}

		/// <summary>
		/// Gets the first child sheet part.
		/// </summary>
		/// <param name="workbookPart">Workbook part.</param>
		/// <returns>The first child sheet part.</returns>
		public WorksheetPart GetWorksheetPart(WorkbookPart workbookPart) {
			IEnumerable<Sheet> sheets = workbookPart.Workbook.Descendants<Sheet>();
			Sheet firstSheet = sheets.First();
			WorksheetPart worksheetPart = (WorksheetPart)WorkbookPart.GetPartById(firstSheet.Id);
			return worksheetPart;
		}

		/// <summary>
		/// Processes given object, considering it contains data of entity schema
		/// with unique identifier <paramref name="rootSchemaUId"/>.
		/// </summary>
		/// <param name="rootSchemaUId">Root schema unique identifier.</param>
		/// <returns></returns>
		public void InitRootSchema(Guid rootSchemaUId) {
			rootSchemaUId.CheckArgumentEmpty(nameof(rootSchemaUId));
			SetRootEntitySchema(rootSchemaUId);
		}

		#endregion

	}
	#endregion
}
