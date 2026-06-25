namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Configuration.FileImport;
	using Terrasoft.Configuration.GeneratedWebFormService;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Process;
	using global::Common.Logging;
	using CoreTasks = Terrasoft.Core.Tasks;
	using Terrasoft.Web.Http.Abstractions;


	#region Class: WebFormAsyncHandler
	/// <summary>
	/// Provides web form save handling.
	/// </summary>
	/// <seealso cref="Terrasoft.Configuration.IWebFormHandler" />
	public class WebFormAsyncHandler : IWebFormHandler
	{
		#region Fields: Private

		private readonly UserConnection _userConnection;

		private static readonly ILog _log = LogManager.GetLogger("WebToObject");

		#endregion

		#region Constructors

		public WebFormAsyncHandler(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		public WebFormAsyncHandler(UserConnection userConnection, IWebFormImportParamsGenerator importParamsGenerator,
			IWebFormProcessHandlersFactory factory, FileImporter fileImporter) {
			_userConnection = userConnection;
			FileImporter = fileImporter;
		}

		#endregion

		#region Properties: Protected

		protected FormData FormData {
			get;
			set;
		}

		private FileImporter _fileImporter;
		protected FileImporter FileImporter {
			get {
				return _fileImporter ??
					(_fileImporter = GetFileImporter());
			}
			set { _fileImporter = value; }
		}

		private GeneratedWebFormValidator _webFormValidator;
		protected GeneratedWebFormValidator WebFormValidator {
			get {
				return _webFormValidator ??
					(_webFormValidator = new GeneratedWebFormValidator(_userConnection));
			}
			set { _webFormValidator = value; }
		}

		private IWebFormHandler _webFormHandler;
		protected IWebFormHandler WebFormHandler {
			get {
				return _webFormHandler ?? (_webFormHandler = new WebFormHandler(_userConnection, WebFormValidator));
			}
			set { _webFormHandler = value; }
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

		private FileImporter GetFileImporter() {
			var fileImporter = new FileImporter(_userConnection);
			fileImporter.ImportEntitySaveError += (sender, args) => {
				SetErrorStatus(args.Message ?? args.Exception.Message, args.Exception);
			};
			fileImporter.ColumnProcessError += (sender, args) => {
				SetErrorStatus(args.Message);
			};
			return fileImporter;
		}

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

		private void SetErrorStatus(string message) {
			Success = false;
			ErrorMessage = message;
			LogError();
		}

		private void SetErrorStatus(string message, Exception exception) {
			Exception = exception;
			SetErrorStatus(message);
		}

		private void SaveWebFormData(Guid webFormDataId, Guid webFormId) {
			EntitySchema schema = _userConnection.EntitySchemaManager.FindInstanceByName("WebFormData");
			if (schema == null) {
				SetErrorStatus(GeneratedWebFormLczUtilities.GetLczStringValue("SchemaNotFoundError",
					"WebFormImportParamsGenerator", _userConnection));
				return;
			}
			var fieldsData = new Dictionary<string, string>();
			FormData.formFieldsData.ForEach(data => {
				fieldsData[data.name] = data.value;
			});
			var values = new Dictionary<string, string>();
			values["Id"] = webFormDataId.ToString();
			values["WebFormId"] = webFormId.ToString();
			values["Data"] = Json.Serialize(fieldsData, true);
			if (FormData.contactFieldsData != null) {
				FormData.contactFieldsData.ForEach(data => {
					values[data.name] = data.value;
				});
			}
			ImportParameters parameters = new BaseImportParamsGenerator().GenerateParameters(schema, values);
			FileImporter.ImportWithParams(parameters);
			if (Success) {
				LogSuccess(schema?.Name, webFormId);
			}
		}

		private void AddFormFieldValue(string fieldName, string value) {
			var fieldsData = FormData.formFieldsData.ToList();
			var contactField = fieldsData.FirstOrDefault(f => f.name.Equals(fieldName));
			if (contactField == null) {
				fieldsData.Add(new FormFieldsData() {
					name = fieldName,
					value = value
				});
				FormData.formFieldsData = fieldsData.ToArray();
			}
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
			UpdateValidatorRules(WebFormValidator);
			var formItemValidationResult = WebFormValidator.Validate(webFormId);
			if (!formItemValidationResult.Succes) {
				Success = false;
				ErrorMessage = formItemValidationResult.Message;
				LogError();
				return;
			}
			var webFormDataId = Guid.NewGuid();
			SaveWebFormData(webFormDataId, webFormId);
			if (!Success) {
				return;
			}
			AddFormFieldValue("WebFormDataId", webFormDataId.ToString());
			var data = new WebFormAsyncHandlerBackgroundTaskDto {
				webFormId= webFormId,
				webFormDataId = webFormDataId,
				FormData= formData,
				ReferrerUri = HttpContext.Current.Request.UrlReferrer?.AbsoluteUri
			};
			CoreTasks.Task.StartNewWithUserConnection<WebFormAsyncHandlerBackgroundTask, WebFormAsyncHandlerBackgroundTaskDto>(data);
		}

		#endregion

	}

	#endregion

	#region Class: WebFormAsyncHandlerBackgroundTaskDto

	/// <summary>
	/// DTO of the background task for WebFormAsyncHandler.
	/// </summary>
	public class WebFormAsyncHandlerBackgroundTaskDto
	{

		#region Properties: Public

		public Guid webFormId { get; set; }
		public Guid webFormDataId { get; set; }
		public FormData FormData { get; set; }
		public string ReferrerUri { get; set; }

		#endregion

	}

	#endregion

	#region Class: WebFormAsyncHandlerBackgroundTask

	/// <summary>
	/// Background task for WebFormAsyncHandler.
	/// </summary>
	public class WebFormAsyncHandlerBackgroundTask : CoreTasks.IBackgroundTask<WebFormAsyncHandlerBackgroundTaskDto>,
		IUserConnectionRequired
	{

		#region Fields: Private

		private UserConnection _userConnection;

		#endregion

		#region Methods: Private

		private EntitySchemaQuery GetWebFormFindContactProcessEsq() {
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "GeneratedWebForm");
			esq.AddColumn("IdentificationProcess.SysProcess.UId");
			esq.AddColumn("CreateContact");
			esq.AddColumn("Type.SchemaUid");
			return esq;
		}

		private void AddFormFieldValue(FormData formData, string fieldName, string value) {
			var fieldsData = formData.formFieldsData.ToList();
			var contactField = fieldsData.FirstOrDefault(f => f.name.Equals(fieldName));
			if (contactField == null) {
				fieldsData.Add(new FormFieldsData() {
					name = fieldName,
					value = value
				});
				formData.formFieldsData = fieldsData.ToArray();
			}
		}

		private Guid StartFindOrCreateContactProcess(WebFormAsyncHandlerBackgroundTaskDto data) {
			var esq = GetWebFormFindContactProcessEsq();
			Entity entity;
			entity = esq.GetEntity(_userConnection, data.webFormId);
			if (entity == null) {
				return Guid.Empty;
			}
			var isCreateContact = entity.GetTypedColumnValue<bool>("CreateContact");
			var schemaUid = entity.GetTypedColumnValue<Guid>("Type_SchemaUidId");
			var webFormEntity = _userConnection.EntitySchemaManager.FindInstanceByUId(schemaUid);
			if (webFormEntity.Name == "Lead" && !isCreateContact) {
				return Guid.Empty;
			}
			var processUId = entity.GetTypedColumnValue<Guid>("IdentificationProcess_SysProcess_UId");
			if (processUId == Guid.Empty) {
				return Guid.Empty;
			}
			IProcessEngine processEngine = _userConnection.ProcessEngine;
			IProcessExecutor processExecutor = processEngine.ProcessExecutor;
			var inputParameters = new Dictionary<string, string> {
				["WebFormDataId"] = data.webFormDataId.ToString()
			};
			return processExecutor.Execute<Guid>(processUId, "ContactId", inputParameters);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Runs the operation with parameters.
		/// </summary>
		/// <param name="data">The parameters.</param>
		public void Run(WebFormAsyncHandlerBackgroundTaskDto data) {
			var contactId = StartFindOrCreateContactProcess(data);
			if (contactId != Guid.Empty) {
				AddFormFieldValue(data.FormData, "ContactId", contactId.ToString());
			}
			var validator = new GeneratedWebFormValidator(data.ReferrerUri, _userConnection);
			var handler = new WebFormHandler(_userConnection, validator);
			handler.HandleForm(data.FormData);
		}

		/// <summary>
		/// Sets the UserConnection.
		/// </summary>
		/// <param name="userConnection">The UserConnection.</param>
		public void SetUserConnection(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

	}

	#endregion

}

