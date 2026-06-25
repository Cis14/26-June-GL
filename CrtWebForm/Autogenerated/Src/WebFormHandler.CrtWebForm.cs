namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using FileImport;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Configuration.GeneratedWebFormService;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using global::Common.Logging;

	#region Class: WebFormHandler

	/// <summary>
	/// Provied web form save handling.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.IWebFormHandler" />
	public class WebFormHandler : IWebFormHandler
	{
		#region Fields: Private

		private readonly UserConnection _userConnection;

		private static readonly ILog _log = LogManager.GetLogger("WebToObject");

		#endregion

		#region Constructors

		public WebFormHandler(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		public WebFormHandler(UserConnection userConnection, GeneratedWebFormValidator webFormValidator) {
			_userConnection = userConnection;
			WebFormValidator = webFormValidator;
		}

		public WebFormHandler(UserConnection userConnection, IWebFormImportParamsGenerator importParamsGenerator,
			IWebFormProcessHandlersFactory factory, FileImporter fileImporter) {
			_userConnection = userConnection;
			ImportParamsGenerator = importParamsGenerator;
			WebFormProcessHandlersFactory = factory;
			FileImporter = fileImporter;
		}

		#endregion

		#region Properties: Protected

		protected FormData FormData {
			get;
			set;
		}

		private GeneratedWebFormValidator _webFormValidator;
		protected GeneratedWebFormValidator WebFormValidator {
			get {
				return _webFormValidator ??
					(_webFormValidator = new GeneratedWebFormValidator(_userConnection));
			}
			set { _webFormValidator = value; }
		}

		private IWebFormImportParamsGenerator _importParamsGenerator;
		protected IWebFormImportParamsGenerator ImportParamsGenerator {
			get {
				return _importParamsGenerator ?? (_importParamsGenerator = GetImportParamsGenerator());
			}
			set { _importParamsGenerator = value; }
		}

		private IWebFormProcessHandlersFactory _webFormProcessHandlersFactory;
		protected IWebFormProcessHandlersFactory WebFormProcessHandlersFactory {
			get {
				return _webFormProcessHandlersFactory ??
					(_webFormProcessHandlersFactory = new WebFormProcessHandlersFactory());
			}
			set { _webFormProcessHandlersFactory = value; }
		}

		private FileImporter _fileImporter;
		protected FileImporter FileImporter {
			get {
				return _fileImporter ??
					(_fileImporter = new FileImporter(_userConnection));
			}
			set { _fileImporter = value; }
		}


		private IEnumerable<IGeneratedWebFormPostProcessHandler> _listPostProcessHandlers;
		protected IEnumerable<IGeneratedWebFormPostProcessHandler> ListPostProcessHandlers {
			get {
				return _listPostProcessHandlers ?? (_listPostProcessHandlers = new List<IGeneratedWebFormPostProcessHandler>());
			}
			set { _listPostProcessHandlers = value; }
		}

		private IEnumerable<IGeneratedWebFormPreProcessHandler> _listPreProcessHandlers;
		protected IEnumerable<IGeneratedWebFormPreProcessHandler> ListPreProcessHandlers {
			get {
				return _listPreProcessHandlers ?? (_listPreProcessHandlers = new List<IGeneratedWebFormPreProcessHandler>());
			}
			set { _listPreProcessHandlers = value; }
		}

		#endregion

		#region Properties: Public

		public bool Success {
			get;
			private set;
		}

		public string ErrorMessage {
			get;
			private set;
		}

		public Exception Exception {
			get;
			private set;
		}

		#endregion

		#region Methods: Private

		private void LogError() {
			_log.Error($"Error: {ErrorMessage}. Exception: {Exception}");
		}

		private void LogSuccess(string schemaName, Guid webFormId) {
			var formDataJson = Json.Serialize(FormData);
			_log.Debug($"Entity {schemaName} is created. Landing: {webFormId}. Form data: {formDataJson}");
		}

		private void UpdateValidatorRules(GeneratedWebFormValidator validator) {
			var disableRefererPolicy = (FormData?.options?.disableRefererPolicy).GetValueOrDefault();
			if (disableRefererPolicy) {
				validator.DisableExternalUrlValidation = true;
			}
		}

		private IWebFormImportParamsGenerator GetImportParamsGenerator() {
			UpdateValidatorRules(WebFormValidator);
			var defaultValueManager = new ObjectDefaultValueManager();
			return new WebFormImportParamsGenerator(FormData, WebFormValidator, defaultValueManager,
				_userConnection);
		}

		private IEnumerable<IGeneratedWebFormPostProcessHandler> GetPostProcessHandlers(Guid webFormId) {
			return WebFormProcessHandlersFactory.GetPostProcessHandlers(_userConnection, webFormId);
		}

		private IEnumerable<IGeneratedWebFormPreProcessHandler> GetPreProcessHandlers(Guid webFormId) {
			return WebFormProcessHandlersFactory.GetPreProcessHandlers(_userConnection, webFormId);
		}

		private void InitHandlers(Guid webFormId) {
			ListPreProcessHandlers = GetPreProcessHandlers(webFormId);
			ListPostProcessHandlers = GetPostProcessHandlers(webFormId);
		}

		private void SetErrorStatus(string message) {
			Success = false;
			ErrorMessage = message;
			LogError();
		}

		private void SetErrorStatus(string message, Exception exception) {
			Exception = exception;
			SetErrorStatus(message);
		}

		#endregion

		#region Methods: Protected

		protected void PostProcessData(FormData formData, Guid webFormId, Guid entityId) {
			try {
				ListPostProcessHandlers.ForEach(handler => {
					handler.Execute(_userConnection, webFormId, formData.formFieldsData, entityId);
				});
			} catch (Exception ex) {
				var message = GeneratedWebFormLczUtilities.GetLczStringValue("PostProcessErrorMessage",
					nameof(WebFormHandler), _userConnection);
				SetErrorStatus(message, ex);
			}
		}

		protected FormData PreProcessData(FormData formData) {
			try {
				ListPreProcessHandlers.ForEach(handler => {
					formData = handler.Execute(_userConnection, formData, ImportParamsGenerator);
				});
			} catch (Exception ex) {
				var message = GeneratedWebFormLczUtilities.GetLczStringValue("PreProcessErrorMessage",
					nameof(WebFormHandler), _userConnection);
				SetErrorStatus(message, ex);
			}
			return formData;
		}

		protected Guid SetUpNewEntityId(FormData formData) {
			Guid entityId = Guid.NewGuid();
			var values = formData.formFieldsData.ToList();
			FormFieldsData idData = values.FirstOrDefault(field => field.name.ToLower().Equals("id"));
			if (idData == null) {
				idData = new FormFieldsData {
					name = "Id",
					value = entityId.ToString()
				};
				values.Add(idData);
			} else {
				if (idData.value.IsValidGuid()) {
					entityId = Guid.Parse(idData.value);
				} else {
					idData.value = entityId.ToString();
				}
			}
			formData.formFieldsData = values.ToArray();
			return entityId;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Handles the form data.
		/// </summary>
		/// <param name="formData">The form data.</param>
		public void HandleForm(FormData formData) {
			Success = true;
			FormData = formData;
			Guid webFormId = WebFormHelper.GetWebFormId(formData);
			if (webFormId.Equals(Guid.Empty)) {
				SetErrorStatus(GeneratedWebFormLczUtilities
					.GetLczStringValue("UnknownWebFormIdentifierMessage",
						"WebFormHandler", _userConnection));
				return;
			}
			if (!(ImportParamsGenerator is IImportParamsGenerator)) {
				SetErrorStatus(GeneratedWebFormLczUtilities
					.GetLczStringValue("ImportParamsGeneratorImplementationMessage",
						"WebFormHandler", _userConnection));
				return;
			}
			InitHandlers(webFormId);
			Guid newEntityId = SetUpNewEntityId(formData);
			FormData = PreProcessData(formData);
			if (!Success) {
				return;
			}
			EntitySchema schema = WebFormHelper.GetWebFormEntitySchema(webFormId, _userConnection);
			ImportParameters parameters =
				((IImportParamsGenerator)ImportParamsGenerator).GenerateParameters(schema);
			if (!ImportParamsGenerator.Success) {
				SetErrorStatus(ImportParamsGenerator.ErrorMessage);
				return;
			}
			FileImporter.ImportEntitySaveError += (sender, args) => {
				SetErrorStatus(args.Message ?? args.Exception.Message, args.Exception);
			};
			FileImporter.ColumnProcessError += (sender, args) => {
				SetErrorStatus(args.Message);
			};
			FileImporter.ImportWithParams(parameters);
			if (!Success) {
				return;
			}
			PostProcessData(formData, webFormId, newEntityId);
			if (Success) {
				LogSuccess(schema?.Name, webFormId);
			}
		}


		#endregion

	}

	#endregion

}

