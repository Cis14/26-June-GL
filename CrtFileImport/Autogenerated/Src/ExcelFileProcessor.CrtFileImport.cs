namespace Terrasoft.Configuration.FileImport
{
	using DocumentFormat.OpenXml.Packaging;
	using DocumentFormat.OpenXml.Spreadsheet;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Web.Http.Abstractions;

	#region Class: ExcelFileProcessor

	/// <summary>
	/// An instance of this class is responsible for processing an Excel file.
	/// </summary>
	public class ExcelFileProcessor : ExcelBaseProcess, IFileProcessor
	{

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="ExcelFileProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public ExcelFileProcessor(UserConnection userConnection) : base(userConnection) { }

		/// <summary>
		/// Creates instance of type <see cref="ExcelFileProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="parameters">File import parameters.</param>
		public ExcelFileProcessor(UserConnection userConnection, ImportParameters parameters): this(userConnection) => Parameters = parameters;

		#endregion

		#region Properties: Protected

		private ImportObject _importObject;

		/// <summary>
		/// Import object.
		/// </summary>
		protected ImportObject ImportObject {
			get => _importObject ?? (_importObject = new ImportObject());
			set => _importObject = value;
		}

		/// <summary>
		/// File import parameters.
		/// </summary>
		protected ImportParameters Parameters { get; set; }

		#endregion

		#region Methods: Private

		/// <summary>
		/// Processes given spreadsheet document.
		/// </summary>
		/// <param name="spreadsheetDocument">Spreadsheet document.</param>
		private void ProcessSpreadsheetDocument(SpreadsheetDocument spreadsheetDocument) {
			WorkbookPart = spreadsheetDocument.WorkbookPart;
			WorksheetPart worksheetPart = GetWorksheetPart(WorkbookPart);
			ProcessWorksheetPart(worksheetPart);
		}

		/// <summary>
		/// Processes given worksheet part.
		/// </summary>
		/// <param name="worksheetPart">Worksheet part.</param>
		private void ProcessWorksheetPart(WorksheetPart worksheetPart) {
			Worksheet worksheet = worksheetPart.Worksheet;
			SheetData sheetData = worksheet.GetFirstChild<SheetData>();
			ProcessSheetData(sheetData);
		}

		/// <summary>
		/// Processes given sheet data.
		/// </summary>
		/// <param name="sheetData">Sheet data.</param>
		private void ProcessSheetData(SheetData sheetData) {
			IEnumerable<Row> rows = sheetData.Elements<Row>();
			IEnumerable<Cell> header = GetHeader(rows.FirstOrDefault());
			Parameters.HeaderColumnsValues = ProcessCells(header);
			IEnumerable<Row> table = GetTable(rows);
			List<ImportEntity> entities = ProcessTable(table);
			Parameters.SetEntities(entities);
		}
		
		/// <summary>
		/// Gets table cells.
		/// </summary>
		/// <param name="rows">Rows.</param>
		/// <returns>A collection of import entities.</returns>
		private IEnumerable<Row> GetTable(IEnumerable<Row> rows) {
			IEnumerable<Row> table = rows.Skip(1);
			var firstRow = table.FirstOrDefault();
			if (firstRow == null) {
				throw new EmptyDataException(UserConnection.Workspace.ResourceStorage);
			}
			return table;
		}

		/// <summary>
		/// Processes data table.
		/// </summary>
		/// <param name="table">Table cells.</param>
		/// <returns>A collection of import entities.</returns>
		private List<ImportEntity> ProcessTable(IEnumerable<Row> table) {
			List<ImportEntity> entities = new List<ImportEntity>();
			foreach (Row row in table) {
				IEnumerable<ImportColumnValue> columnsValues = ProcessRow(row);
				bool isColumnsValuesEmpty = IsColumnsValuesEmpty(columnsValues);
				if (isColumnsValuesEmpty) {
					continue;
				}
				ImportEntity entity = new ImportEntity {
					RowIndex = row.RowIndex.Value
				};
				entity.ColumnValues = columnsValues;
				entities.Add(entity);
			}
			if (entities.Count == 0) {
				throw new EmptyDataException(UserConnection.Workspace.ResourceStorage);
			}
			return entities;
		}
		
		#endregion

		#region Methods: Public

		/// <summary>
		/// Processes given file.
		/// </summary>
		/// <param name="file">File.</param>
		public void ProcessFile(HttpPostedFile file) {
			file.CheckArgumentNull(nameof(file));
			Stream stream = file.InputStream;
			bool isEditable = false;
			using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(stream, isEditable)) {
				ProcessSpreadsheetDocument(spreadsheetDocument);
			}
			Parameters.FileName = file.FileName;
		}

		/// <summary>
		/// Processes given object, considering it contains data of entity schema
		/// with unique identifier <paramref name="rootSchemaUId"/>.
		/// </summary>
		/// <param name="rootSchemaUId">Root schema unique identifier.</param>
		/// <returns>File import parameters.</returns>
		public void ProcessObject(Guid rootSchemaUId) {
			InitRootSchema(rootSchemaUId);
			Parameters.RootSchemaUId = rootSchemaUId;
			Parameters.Columns = ProcessHeader(Parameters.HeaderColumnsValues);
		}

		#endregion

	}

	#endregion

}

