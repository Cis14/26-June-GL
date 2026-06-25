namespace Terrasoft.Configuration.FileImport
{
	using DocumentFormat.OpenXml;
	using DocumentFormat.OpenXml.Packaging;
	using DocumentFormat.OpenXml.Spreadsheet;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: SaxExcelFileProcessor

	/// <summary>
	///  An instance of this class is responsible for processing an Excel file using sax method.
	/// </summary>
	[DefaultBinding(typeof(ISaxFileProcessor), Name = nameof(SaxExcelFileProcessor))]
	public class SaxExcelFileProcessor : ExcelBaseProcess, ISaxFileProcessor
	{

		#region Fields: Private

		private IImportParametersRepository _importParametersRepository;

		#endregion

		#region Properties: Private

		private IImportParametersRepository ImportParametersRepository =>
			_importParametersRepository ??
			(_importParametersRepository = ClassFactory.Get<IImportParametersRepository>(
				new ConstructorArgument("userConnection", UserConnection)));

		#endregion

		#region Constructors: Public

		public SaxExcelFileProcessor(UserConnection userConnection)
			: base(userConnection) { }

		#endregion

		#region Methods: Private

		/// <summary>
		/// Processes given spreadsheet document.
		/// </summary>
		/// <param name="spreadsheetDocument">Spreadsheet document.</param>
		private WorksheetPart ProcessSpreadsheetDocument(SpreadsheetDocument spreadsheetDocument) {
			WorkbookPart = spreadsheetDocument.WorkbookPart;
			return GetWorksheetPart(WorkbookPart);
		}

		/// <summary>
		/// Get import entity from row.
		/// </summary>
		/// <param name="row">Source entity.</param>
		/// <param name="entity">Out Entity.</param>
		/// <returns>New import entity from row.</returns>
		private bool GetEntity(Row row, out ImportEntity entity) {
			entity = null;
			IEnumerable<ImportColumnValue> columnValues = ProcessRow(row);
			bool isColumnsValuesEmpty = IsColumnsValuesEmpty(columnValues);
			if (isColumnsValuesEmpty) {
				return false;
			}
			entity = new ImportEntity() {
				ColumnValues = columnValues,
				RowIndex = row.RowIndex.Value
			};
			return true;
		}

		/// <summary>
		/// Read rows in excel file.
		/// </summary>
		/// <param name="importParametersId">Session Id.</param>
		/// <returns>Row</returns>
		private IEnumerable<Row> ReadAllRows(Guid importParametersId) {
			using (Stream stream = ImportParametersRepository.GetFileStream(importParametersId)) {
				using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(stream, false)) {
					using (var reader = OpenXmlReader.Create(ProcessSpreadsheetDocument(spreadsheetDocument))) {
						while (reader.Read()) {
							if (reader.ElementType != typeof(Row)) {
								continue;
							}
							do {
								var row = (Row)reader.LoadCurrentElement();
								var celValues = ProcessRow(row);
								if (celValues.Any()) {
									yield return row;
								}
							} while (reader.ReadNextSibling());
						}
					}
				}
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Get headers of excel file.
		/// </summary>
		/// <param name="importParametersId">Session Id.</param>
		/// <returns>List header ImportColumnValue.</returns>
		public IEnumerable<ImportColumn> GetHeaders(Guid importParametersId) {
			var firstRow = ReadAllRows(importParametersId).FirstOrDefault();
			var header= GetHeader(firstRow);
			var columnValues= ProcessCells(header);
			return ProcessHeader(columnValues);
		}

		/// <summary>
		/// Check that file contains headers and rows.
		/// </summary>
		/// <param name="importParametersId">Session Id.</param>
		/// <returns>True of False.</returns>
		public void CheckIsFileValid(Guid importParametersId) {
			var rows = ReadAllRows(importParametersId).Take(2).ToList();
			if (rows.Any() && (rows.FirstOrDefault(r => r.RowIndex != 1) != null)) {
				var firstRow = rows.SingleOrDefault(r => r.RowIndex == 1);
				var checkHeaders = GetHeader(firstRow);
			} else {
				throw new EmptyDataException(UserConnection.Workspace.ResourceStorage);
			}
		}

		/// <summary>
		/// Read all rows in Excel file. Only skip first row. 
		/// </summary>
		/// <param name="importParametersId">Session Id.</param>
		/// <returns>Entity.</returns>
		public IEnumerable<ImportEntity> ReadEntities(Guid importParametersId) {
			foreach (var row in ReadAllRows(importParametersId).Skip(1)) {
				if (!GetEntity(row, out ImportEntity entity)) {
					continue;
				}
				yield return entity;
			}
		}

		#endregion

	}

	#endregion

}
