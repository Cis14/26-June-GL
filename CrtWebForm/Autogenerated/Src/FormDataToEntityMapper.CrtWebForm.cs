namespace Terrasoft.Configuration.GeneratedWebFormService
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: FormDataToEntityMapper

	/// <summary>
	/// Maps form data onto given entity.
	/// </summary>
	public class FormDataToEntityMapper : IFormDataToEntityMapper
	{

		#region Constructors: Public

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="validator">Instance of <see cref="IGeneratedWebFormValidator" />.</param>
		/// <param name="defaultValueManager">Instance of <see cref="IDefaultValueManager" />.</param>
		/// <param name="userConnection">Instance of user connection.</param>
		public FormDataToEntityMapper(IGeneratedWebFormValidator validator,
				IDefaultValueManager defaultValueManager, UserConnection userConnection) {
			GeneratedWebFormValidator = validator;
			DefaultValueManager = defaultValueManager;
			UserConnection = userConnection;
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Instance of user connection.
		/// </summary>
		public UserConnection UserConnection {
			get;
			set;
		}

		/// <summary>
		/// Flag that indicates whether mapping performed successfully.
		/// </summary>
		public bool Success {
			get;
			private set;
		}

		/// <summary>
		/// Message that is occured during mapping.
		/// </summary>
		public string Message {
			get;
			private set;
		}

		/// <summary>
		/// Instance of landing data validator.
		/// </summary>
		public IGeneratedWebFormValidator GeneratedWebFormValidator {
			get;
			set;
		}

		/// <summary>
		/// Insatnce of class that fills default values of created entity.
		/// </summary>
		public IDefaultValueManager DefaultValueManager {
			get;
			set;
		}

		#endregion

		#region Methods: Private

		private string SetColumnsFromFormFieldsData(FormFieldsData[] formFieldsData, Entity entity) {
			string resultMessage = string.Empty;
			if (formFieldsData != null) {
				foreach (FormFieldsData formField in formFieldsData) {
					KeyValuePair<string, string> columnNameValue
						= NameValueCorrector.GetCorrectNameValue(formField.name, formField.value);
					string columnName = columnNameValue.Key;
					string columnValue = columnNameValue.Value;
					if (string.IsNullOrEmpty(columnValue))
						continue;
					EntitySchemaColumn column = entity.Schema.Columns.GetByName(columnName);
					Type columnType = column.DataValueType.ValueType;
					if (column.IsLookupType) {
						SetLookupColumn(column, columnValue, entity);
					} else {
						try {
							if (WebFormHelper.IsGeographicalColumn(columnName)) {
								KeyValuePair<string, string> geogColumn =
									WebFormHelper.GetGeographicalUnitColumn(columnName, columnValue, UserConnection);
								entity.SetColumnValue(geogColumn.Key, geogColumn.Value);
							} else {
								object value = DataTypeUtilities.ValueAsType(columnValue, columnType);
								entity.SetColumnValue(column, value);
							}
						} catch (Exception ex) {
							resultMessage += Environment.NewLine;
							string errorMessage;
							if (ex is FormatException) {
								errorMessage = GeneratedWebFormLczUtilities.GetLczStringValue(
									"FormatExceptionMessage", "GeneratedWebFormService", UserConnection);
							} else if (ex is OverflowException) {
								errorMessage = GeneratedWebFormLczUtilities.GetLczStringValue(
									"OverflowExceptionMessage", "GeneratedWebFormService", UserConnection);
							} else {
								errorMessage = GeneratedWebFormLczUtilities.GetLczStringValue(
									"ConversionExceptionMessage", "GeneratedWebFormService", UserConnection);
							}
							resultMessage += string.Format(errorMessage, formField.value, columnType);
						}
					}
				}
			}
			return resultMessage;
		}

		private void SetLookupColumn(EntitySchemaColumn column, string columnValue, Entity entity) {
			string dictionaryName = column.ReferenceSchema.Name;
			Guid itemIdValue = WebFormHelper.GetItemIdFromDictionary(dictionaryName, columnValue, UserConnection);
			entity.SetColumnValue(column, itemIdValue != Guid.Empty ? itemIdValue : (object)null);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Creates entity from landing.
		/// </summary>
		/// <param name="formData">Data from landing.</param>
		/// <param name="entity">Entity onto which data from landing is mapped.</param>
		public void Map(FormData formData, Entity entity) {
			Guid webFormId;
			if (!Guid.TryParse(formData.formId, out webFormId)) {
				Message = GeneratedWebFormLczUtilities
					.GetLczStringValue("UnknownWebFormIdentifierMessage", "GeneratedWebFormService", UserConnection);
				Success = false;
				return;
			}
			FormFieldsData[] formFieldsData = formData.formFieldsData;
			var formItemValidationResult = GeneratedWebFormValidator.Validate(webFormId);
			if (!formItemValidationResult.Succes) {
				Message = formItemValidationResult.Message;
				Success = false;
				return;
			}
			entity.SetDefColumnValues();
			Dictionary<string, object> defaultValues = DefaultValueManager.GetValues(webFormId, UserConnection);
			foreach (string columnName in defaultValues.Keys) {
				entity.SetColumnValue(columnName, defaultValues[columnName]);
			}
			Message += SetColumnsFromFormFieldsData(formFieldsData, entity);
			Success = true;
		}

		#endregion
	}

	#endregion
}

