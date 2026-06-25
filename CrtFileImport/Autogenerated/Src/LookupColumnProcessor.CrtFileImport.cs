namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Core.Factories;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: LookupColumnProcessor

	/// <summary>
	/// An instance of this class is responsible for processing Lookup column values.
	/// </summary>
	public class LookupColumnProcessor : BaseColumnProcessor, IColumnProcessor
	{

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="LookupColumnProcessor"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public LookupColumnProcessor(UserConnection userConnection) : base(userConnection) {
			ValidateRequiredColumns = true;
		}

		#endregion

		#region Properties: Private

		private LookupValuesToProcess _lookupValuesToProcess;

		private LookupValuesToProcess LookupValuesToProcess =>
			_lookupValuesToProcess ?? (_lookupValuesToProcess = ClassFactory.Get<LookupValuesToProcess>(
					new ConstructorArgument("userConnection", UserConnection)));

		private LookupProcessedValues _results;

		/// <summary>
		/// Results.
		/// </summary>
		private LookupProcessedValues Results => _results ?? (_results = ClassFactory.Get <LookupProcessedValues>(
				new ConstructorArgument("userConnection", UserConnection)));

		private LookupValuesProcessor _lookupValuesProcessor;

		private LookupValuesProcessor LookupValuesProcessor {
			get {
				if (_lookupValuesProcessor != null) {
					return _lookupValuesProcessor;
				}
				_lookupValuesProcessor = new LookupValuesProcessor(UserConnection, ValidateRequiredColumns);
				_lookupValuesProcessor.ProcessError += OnProcessErrorReceived;
				return _lookupValuesProcessor;
			}
		}

		/// <summary>
		/// Error message template.
		/// </summary>
		private string _errorMessageTemplate;

		private string ErrorMessageTemplate {
			get {
				if (_errorMessageTemplate == null) {
					IResourceStorage storage = UserConnection.Workspace.ResourceStorage;
					_errorMessageTemplate = new LocalizableString(storage, "LookupColumnProcessor",
						"LocalizableStrings.ErrorMessageTemplate.Value");
				}
				return _errorMessageTemplate;
			}
		}

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId {
			get {
				return DataValueType.LookupDataValueTypeUId;
			}
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Determines if required columns validation is needed.
		/// </summary>
		public bool ValidateRequiredColumns {
			get;
			set;
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Creates information about processing error.
		/// </summary>
		/// <param name="cellRefference">Information about excel cell.</param>
		/// <param name="entitySchemaUId">Entity schema unique identifier.</param>
		/// <param name="missingValue">Exception message.</param>
		/// <returns></returns>
		private string CreateColumnProcessErrorMessage(Guid entitySchemaUId, string missingValue) {
			ISchemaManagerItem<EntitySchema> schema = UserConnection.EntitySchemaManager.GetItemByUId(entitySchemaUId);
			return string.Format(ErrorMessageTemplate, schema.Caption, missingValue);

		}

		private void OnProcessErrorReceived(object sender, LookupValuesProcessorErrorEventArgs eventArgs) {
			Guid entitySchemaUId = eventArgs.EntitySchemaUId;
			string messingValue = eventArgs.MissingValue;
			var cellsIndexes = LookupValuesToProcess.GetCellsIndexes(entitySchemaUId, eventArgs.ColumnName, messingValue);
			if (!cellsIndexes.Any()) {
				return;
			}
			foreach (string cellsIndex in cellsIndexes) {
				SendProcessError(cellsIndex, CreateColumnProcessErrorMessage(entitySchemaUId, eventArgs.Exception.Message));
			}
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Gets column destination default properties.
		/// </summary>
		/// <param name="entitySchemaColumn">Entity schema column.</param>
		/// <param name="columnValue">Column value information.</param>
		/// <returns>Column destination default properties.</returns>
		protected override Dictionary<string, object> GetColumnDestinationProperties(
				EntitySchemaColumn entitySchemaColumn, ImportColumnValue columnValue) {
			Dictionary<string, object> destinationProperties = base.GetColumnDestinationProperties(entitySchemaColumn,
				columnValue);
			destinationProperties.Add(ReferenceSchemaUIdPropertyName, entitySchemaColumn.ReferenceSchemaUId);
			return destinationProperties;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Adds raw value that will be processed.
		/// </summary>
		/// <param name="importEntity">Import entity.</param>
		/// <param name="column">Import column.</param>
		/// <param name="columnValue">Import column value.</param>
		/// <param name="destination">Import column destination.</param>
		public void Add(ImportEntity importEntity, ImportColumn column, ImportColumnValue columnValue,
				ImportColumnDestination destination) {
			LookupValuesToProcess.Add(importEntity, column, columnValue, destination);
		}

		/// <summary>
		/// Finds processed value.
		/// </summary>
		/// <param name="destination">Import column destination.</param>
		/// <param name="columnValue">Import column value.</param>
		/// <returns>Processed value.</returns>
		public object FindValueForSave(ImportColumnDestination destination, ImportColumnValue columnValue) {
			return Results.FindValueForSave(destination, columnValue);
		}

		/// <summary>
		/// Runs column values processing.
		/// </summary>
		public void Process() {
			_results = LookupValuesProcessor.ProcessValues(LookupValuesToProcess.GetItems());
		}

		#endregion

	}

	#endregion

}
