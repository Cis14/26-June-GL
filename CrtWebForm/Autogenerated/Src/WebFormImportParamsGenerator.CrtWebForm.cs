namespace Terrasoft.Configuration.GeneratedWebFormService
{
	using System;
	using System.Linq;
	using Core;
	using FileImport;
	using System.Collections.Generic;
	using Common;
	using Core.Entities;

	#region Class: WebFormImportParamsGenerator

	/// <summary>
	/// Webform import parameters generator.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.BaseImportParamsGenerator" />
	/// <seealso cref="Terrasoft.Configuration.GeneratedWebFormService.IWebFormImportParamsGenerator" />
	public class WebFormImportParamsGenerator: BaseImportParamsGenerator, IWebFormImportParamsGenerator
	{
		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors

		public WebFormImportParamsGenerator(FormData formData, IGeneratedWebFormValidator validator,
				IDefaultValueManager defaultValueManager, UserConnection userConnection) {
			_userConnection = userConnection;
			GeneratedWebFormValidator = validator;
			DefaultValueManager = defaultValueManager;
			FormData = formData;
		}
		
		#endregion

		#region Properties: Protected

		protected Guid WebFormId {
			get; 
			set;
		}

		protected FormData FormData {
			get; 
			set;
		}

		protected List<FormFieldsData> FormFieldsData {
			get;
			set;
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Gets or sets the generated web form validator.
		/// </summary>
		/// <value>
		/// The generated web form validator.
		/// </value>
		private IGeneratedWebFormValidator _generatedWebFormValidator;
		public IGeneratedWebFormValidator GeneratedWebFormValidator {
			get {
				return _generatedWebFormValidator;
			}
			set {
				if (value != null) {
					_generatedWebFormValidator = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the default value manager.
		/// </summary>
		/// <value>
		/// The default value manager.
		/// </value>
		private IDefaultValueManager _defaultValueManager;
		public IDefaultValueManager DefaultValueManager {
			get {
				return _defaultValueManager;
			} 
			set {
				if (value != null) {
					_defaultValueManager = value;
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="WebFormImportParamsGenerator"/> is success.
		/// </summary>
		/// <value>
		/// <c>true</c> if success; otherwise, <c>false</c>.
		/// </value>
		public bool Success {
			get; private set; 
		}

		/// <summary>
		/// Gets the message.
		/// </summary>
		/// <value>
		/// The message.
		/// </value>
		public string ErrorMessage {
			get; private set; 
		}

		#endregion

		#region Methods: Private

		private void ValidateWebForm() {
			var formItemValidationResult = GeneratedWebFormValidator.Validate(WebFormId);
			Success = formItemValidationResult.Succes;
			ErrorMessage = formItemValidationResult.Message;
		}

		private Guid GetWebFormId(FormData formData) {
			return formData.GetWebFormId();
		}

		private void CreateFormFieldsData(string columnName, string columnValue) {
			FormFieldsData.Add(new FormFieldsData() {
				name = columnName,
				value = columnValue
			});
		}

		#endregion
		
		#region Methods: Protected

		protected virtual void SetDefaultValues() {
			Dictionary<string, object> defaultValues = DefaultValueManager.GetValues(WebFormId, _userConnection);
			foreach (string key in defaultValues.Keys) {
				string columnName = key;
				string columnValue = !defaultValues[key].Equals(null) ? defaultValues[key].ToString() : string.Empty;
				FormFieldsData formData = FormFieldsData.FirstOrDefault(data => data.name == columnName);
				if (formData == null) {
					CreateFormFieldsData(columnName, columnValue);
				} else if (formData.value.IsNullOrEmpty()) {
					formData.value = columnValue;
				}
			}
		}

		protected Dictionary<string, string> CastFormFieldsDataToDictionary() {
			var dictionary = new Dictionary<string, string>();
			FormFieldsData.ForEach(data => {
				dictionary[data.name] = data.value;
			});
			return dictionary;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Generates the import parameters.
		/// </summary>
		/// <param name="formData">The form data.</param>
		/// <returns>The import parameters.</returns>
		public override ImportParameters GenerateParameters(EntitySchema entitySchema) {
			if (FormData == null) {
				throw new ArgumentNullException("formData");
			}
			if (FormData.formFieldsData == null) {
				throw new ArgumentNullException("formFieldsData");
			}
			Success = true;
			WebFormId = GetWebFormId(FormData);
			FormFieldsData = FormData.formFieldsData.ToList();
			if (entitySchema == null) {
				Success = false;
				ErrorMessage = GeneratedWebFormLczUtilities.GetLczStringValue("SchemaNotFoundError", 
					"WebFormImportParamsGenerator", _userConnection);
				return null;
			}
			ValidateWebForm();
			if (!Success) {
				return null;
			}
			SetDefaultValues();
			return GenerateParameters(entitySchema, CastFormFieldsDataToDictionary());
		}

		#endregion

	}

	#endregion

}

