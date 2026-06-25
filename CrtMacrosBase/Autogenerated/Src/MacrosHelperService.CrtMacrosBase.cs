namespace Terrasoft.Configuration
{
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Web;
	using System.ServiceModel.Activation;
	using System.Runtime.Serialization;
	using System.Collections.Generic;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Configuration.Utils;
	using Terrasoft.Web.Common;
	using Terrasoft.Web.Common.ServiceRouting;
	using System.Web.SessionState;
	using Terrasoft.Configuration.Translating;

	#region Class: MacrosHelperService

	[ServiceContract]
	[DefaultServiceRoute]
	[SspServiceRoute]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class MacrosHelperService : BaseService, IReadOnlySessionState
	{

		#region Fields: Private

		/// <summary>
		/// Instance of <see cref="MacrosHelperV2"/> type.
		/// </summary>
		private readonly MacrosHelperV2 _macrosHelper;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Creates instance of <see cref="MacrosHelperService"/> type.
		/// </summary>
		public MacrosHelperService() {
			_macrosHelper = new GlobalMacrosHelper();
		}

		/// <summary>
		/// Creates instance of <see cref="MacrosHelperService"/> type.
		/// </summary>
		/// <param name="macrosHelper">Macros helper.</param>
		/// <param name="userConnection">User connection.</param>
		public MacrosHelperService(MacrosHelperV2 macrosHelper, UserConnection userConnection) {
			_macrosHelper = macrosHelper;
			UserConnection = userConnection;
		}

		#endregion

		#region Properties: Private

		/// <summary>
		/// Initializes new instance of <see cref="MLangContentFactory"/>
		/// </summary>
		private MLangContentFactory _mlangContentFactory;
		public MLangContentFactory MLangContentFactory {
			get => _mlangContentFactory ?? (_mlangContentFactory = new MLangContentFactory(UserConnection));
			set => _mlangContentFactory = value;
		}


		#endregion

		#region Methods: Protected

		[Obsolete("Method is deprecated, use instead MacrosHelperV2.GetMacrosArguments.")]
		protected virtual Dictionary<Guid, Object> GetMacrosArguments(string entityName, Guid recordId,
				List<MacrosInfo> macrosInfos) {
			return _macrosHelper.GetMacrosArguments(entityName, recordId, macrosInfos);
		}

		[Obsolete("Method is deprecated, use instead MacrosHelperV2.IsCurrentUserMacrosExists.")]
		protected virtual bool IsCurrentUserMacrosExists(List<MacrosInfo> macrosInfos) {
			return _macrosHelper.IsCurrentUserMacrosExists(macrosInfos);
		}

		/// <summary>
		/// Gets processed template.
		/// </summary>
		/// <param name="templateBody">Template body.</param>
		/// <param name="templateSubject">Template subject.</param>
		/// <param name="entitySchemaName">Entity schema name.</param>
		/// <param name="entityId">Entity record identifier.</param>
		/// <param name="languageId">Language identifier.</param>
		/// <returns>Processed template text.<see cref="MacrosHelperServiceResponse"/></returns>
		protected MacrosHelperServiceResponse GetResponseWithReplacedMacroces(string templateBody,
			string templateSubject, string entitySchemaName, Guid entityId, Guid languageId = default(Guid)) {
			MacrosHelperServiceResponse response = new MacrosHelperServiceResponse();
			MacrosExtendedProperties extendedProperties = null;
			try {
				if (_macrosHelper is GlobalMacrosHelper globalMacrosHelper) {
					if (languageId.IsNotEmpty()) {
						extendedProperties = new MacrosExtendedProperties { LanguageId = languageId };
					}
					response.TextTemplate = globalMacrosHelper.GetTextTemplate(templateBody, entitySchemaName,
						entityId, extendedProperties);
					response.SubjectTemplate = globalMacrosHelper.GetPlainTextTemplate(templateSubject, entitySchemaName,
						entityId, extendedProperties);
					response.Success = true;

				} else {
					response.TextTemplate = _macrosHelper.GetTextTemplate(templateBody, entitySchemaName, entityId);
					response.SubjectTemplate = _macrosHelper.GetTextTemplate(templateSubject, entitySchemaName, entityId);
					response.Success = true;
				}
			} catch (Exception e) {
				response.Exception = e;
				response.Success = false;
			}
			return response;
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Finds existing usage record for the current operator and template.
		/// </summary>
		/// <param name="templateId">Template identifier.</param>
		/// <returns>Entity if found, null otherwise.</returns>
		private Entity FindExistingUsageRecord(Guid templateId) {
			var entitySchema = UserConnection.EntitySchemaManager.GetInstanceByName("OperatorTemplateUsage");
			var esq = new EntitySchemaQuery(entitySchema);
			esq.PrimaryQueryColumn.IsAlwaysSelect = true;
			esq.AddColumn("UsageCount");
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, 
				"Operator", UserConnection.CurrentUser.Id));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, 
				"Template", templateId));
			var collection = esq.GetEntityCollection(UserConnection);
			return collection.Count > 0 ? collection[0] : null;
		}

		/// <summary>
		/// Updates existing usage record by incrementing UsageCount.
		/// </summary>
		/// <param name="entity">Existing usage entity.</param>
		private void UpdateUsageRecord(Entity entity) {
			var currentCount = entity.GetTypedColumnValue<int>("UsageCount");
			entity.SetColumnValue("UsageCount", currentCount + 1);
			entity.Save();
		}

		/// <summary>
		/// Creates new usage record for the current operator and template.
		/// </summary>
		/// <param name="templateId">Template identifier.</param>
		private void CreateUsageRecord(Guid templateId) {
			var entitySchema = UserConnection.EntitySchemaManager.GetInstanceByName("OperatorTemplateUsage");
			var entity = entitySchema.CreateEntity(UserConnection);
			entity.SetDefColumnValues();
			entity.SetColumnValue("OperatorId", UserConnection.CurrentUser.Id);
			entity.SetColumnValue("TemplateId", templateId);
			entity.SetColumnValue("UsageCount", 1);
			entity.Save();
		}

		/// <summary>
		/// Tracks template usage for the current operator.
		/// Performs upsert: increments UsageCount if exists, creates new record otherwise.
		/// </summary>
		/// <param name="templateId">Template identifier.</param>
		private void TrackTemplateUsage(Guid templateId) {
			try {
				var existingRecord = FindExistingUsageRecord(templateId);
				if (existingRecord != null) {
					UpdateUsageRecord(existingRecord);
				} else {
					CreateUsageRecord(templateId);
				}
			} catch (Exception) {
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Returns template text where macroses replaced by their values.
		/// </summary>
		/// <param name="request">Service request.</param>
		/// <returns>Response contains result text of template.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "GetTemplate", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public MacrosHelperServiceResponse GetTemplate(MacrosHelperServiceRequest request) {
			var textTemplate = request.TextTemplate.IsNullOrEmpty() ? string.Empty : request.TextTemplate;
			return GetResponseWithReplacedMacroces(textTemplate, string.Empty, request.EntityName, request.EntityId);
		}

		/// <summary>
		/// Returns template text where macroses replaced by their values.
		/// Creates temporary Activity entity to resolve macros and deletes it afterwards.
		/// </summary>
		/// <param name="request">Service request.</param>
		/// <returns>Response contains result text of template.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "GetTemplateFromTemporaryEntity", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public MacrosHelperServiceResponse GetTemplateFromTemporaryEntity(ExtendedMacrosHelperServiceRequest request) {
			var textTemplate = request.TextTemplate.IsNullOrEmpty() ? string.Empty : request.TextTemplate;
			Entity temporaryActivity = null;
			try {
				var activitySchema = UserConnection.EntitySchemaManager.GetInstanceByName("Activity");
				temporaryActivity = activitySchema.CreateEntity(UserConnection);
				temporaryActivity.SetDefColumnValues();
				temporaryActivity.SetColumnValue("Id", Guid.NewGuid());
				temporaryActivity.SetColumnValue("Title", "Subject");
				Guid contactId = request.ContactId;
				if (contactId != Guid.Empty) {
					temporaryActivity.SetColumnValue("ContactId", contactId);
				}
				temporaryActivity.Save();
				var response = GetResponseWithReplacedMacroces(textTemplate, string.Empty, "Activity", 
					temporaryActivity.PrimaryColumnValue);
				return response;
			} finally {
				if (temporaryActivity != null && temporaryActivity.PrimaryColumnValue != Guid.Empty) {
					try {
						temporaryActivity.Delete();
					} catch (Exception) {
					}
				}
			}
		}



		/// <summary>
		/// Returns multilingual template text where macroses replaced by their values.
		/// </summary>
		/// <param name="request">Service request.</param>
		/// <returns>Response contains multilingual result text of template.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "GetMultiLanguageTextTemplate", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public MacrosHelperServiceResponse GetMultiLanguageTextTemplate(MacrosHelperServiceRequest request) {
			IContentKit contentKit = MLangContentFactory.GetContentKit(request.EntityName, "EmailTemplate");
			Entity emailTemplate = contentKit.GetContent(request.TemplateId, request.EntityId);
			string templateBody = emailTemplate.GetTypedColumnValue<string>("Body");
			string templateSubject = emailTemplate.GetTypedColumnValue<string>("Subject");
			var response = UserConnection.GetIsFeatureEnabled("UseMacrosAdditionalParameters") ?
				GetResponseWithReplacedMacroces(templateBody, templateSubject, request.EntityName,
					request.EntityId, emailTemplate.GetTypedColumnValue<Guid>("LanguageId")) :
				GetResponseWithReplacedMacroces(templateBody, templateSubject, request.EntityName, request.EntityId);
			LanguageRegistry.TryGetById(emailTemplate.GetTypedColumnValue<Guid>("LanguageId"), out LanguageInfo lang);
			response.FoundTemplateLanguage = lang?.FullCode;
			return response;
		}

		/// <summary>
		/// Returns multilingual template text with usage tracking for omnichannel.
		/// Tracks template usage for personalized suggestions.
		/// </summary>
		/// <param name="request">Service request.</param>
		/// <returns>Response contains multilingual result text of template.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "GetMultiLanguageTextTemplateWithTracking", 
			BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public MacrosHelperServiceResponse GetMultiLanguageTextTemplateWithTracking(MacrosHelperServiceRequest request) {
			var response = GetMultiLanguageTextTemplate(request);
			if (response.Success && request.TemplateId.IsNotEmpty()) {
				TrackTemplateUsage(request.TemplateId);
			}
			return response;
		}


		#endregion

	}

	#endregion

	#region Class: MacrosHelperServiceRequest

	[DataContract]
	public class MacrosHelperServiceRequest
	{
		/// <summary>
		/// Entity identifier.
		/// </summary>
		[DataMember(Name = "entityId")]
		public Guid EntityId {
			get;
			set;
		}

		/// <summary>
		/// Entity name
		/// </summary>
		[DataMember(Name = "entityName")]
		public string EntityName {
			get;
			set;
		}

		/// <summary>
		/// Text of template.
		/// </summary>
		[DataMember(Name = "textTemplate")]
		public string TextTemplate {
			get;
			set;
		}

		/// <summary>
		/// Template identifier.
		/// </summary>
		[DataMember(Name = "templateId")]
		public Guid TemplateId {
			get;
			set;
		}
	}

	[DataContract]
	public class ExtendedMacrosHelperServiceRequest : MacrosHelperServiceRequest
	{

		/// <summary>
		/// Contact identifier.
		/// </summary>
		[DataMember(Name = "contactId")]
		public Guid ContactId {
			get;
			set;
		}
	}

	#endregion

	#region Class: MacrosHelperServiceResponse

	[DataContract]
	public class MacrosHelperServiceResponse : ConfigurationServiceResponse
	{
		/// <summary>
		/// Text of template.
		/// </summary>
		[DataMember(Name = "textTemplate")]
		public string TextTemplate {
			get;
			set;
		}

		/// <summary>
		/// Subject of template.
		/// </summary>
		[DataMember(Name = "subjectTemplate")]
		public string SubjectTemplate {
			get;
			set;
		}

		/// <summary>
		/// Language of template.
		/// </summary>
		[DataMember(Name = "foundTemplateLanguage")]
		public string FoundTemplateLanguage {
			get;
			set;
		}
	}

	#endregion
}

