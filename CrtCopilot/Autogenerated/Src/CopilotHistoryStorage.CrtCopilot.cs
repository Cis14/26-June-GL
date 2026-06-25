namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Common.Logging;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;

	[DefaultBinding(typeof(ICopilotHistoryStorage))]
	internal class CopilotHistoryStorage : ICopilotHistoryStorage
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private static readonly ILog _log = LogManager.GetLogger("Copilot");
		private readonly IDocumentTool _documentTool;
		private readonly ICopilotIntentsStorage _intentsStorage;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotHistoryStorage"/> class with the specified
		/// user connection and document tool.
		/// </summary>
		/// <param name="userConnection">The <see cref="UserConnection"/>
		/// instance representing the current user connection.</param>
		/// <param name="documentTool">The <see cref="IDocumentTool"/>
		/// instance for handling document-related operations.</param>
		/// <param name="intentsStorage">Intent storage</param>
		public CopilotHistoryStorage(UserConnection userConnection, IDocumentTool documentTool,
				ICopilotIntentsStorage intentsStorage) {
			_userConnection = userConnection;
			_documentTool = documentTool;
			_intentsStorage = intentsStorage;
		}

		#endregion

		#region Properties: Private

		private static Dictionary<string, Guid> _roleMapping;
		private Dictionary<string, Guid> RoleMapping => _roleMapping ?? (_roleMapping = LoadRoleMapping());

		#endregion

		#region Methods: Private

		private Dictionary<string,Guid> LoadRoleMapping() {
			var result = new Dictionary<string, Guid>();
			Select select = new Select(_userConnection)
				.Column("Id")
				.Column("Code")
				.From("CopilotMessageRoleEnt");
			select.ExecuteReader(dataReader => result.Add(
				dataReader.GetColumnValue<string>("Code"), dataReader.GetColumnValue<Guid>("Id")));
			return result;
		}

		private Guid InternalSaveCopilotRequest(CopilotRequestInfo requestInfo) {
			Entity requestEntity = 
				_userConnection.EntitySchemaManager.GetEntityByName("CopilotRequestEnt", _userConnection);
			requestEntity.UseAdminRights = false;
			requestEntity.SetDefColumnValues();
			requestEntity.PrimaryColumnValue = Guid.NewGuid();
			requestEntity.SetColumnValue("Error", requestInfo.Error);
			requestEntity.SetColumnValue("IsFailed", requestInfo.IsFailed);
			requestEntity.SetColumnValue("Duration", requestInfo.Duration);
			requestEntity.SetColumnValue("CreatedOn", requestInfo.StartDate);
			requestEntity.SetColumnValue("TotalActions", requestInfo.TotalActions);
			requestEntity.SetColumnValue("TotalTokens", requestInfo.TotalTokens);
			requestEntity.SetColumnValue("PromptTokens", requestInfo.PromptTokens);
			requestEntity.SetColumnValue("CompletionTokens", requestInfo.CompletionTokens);
			requestEntity.Save();
			return requestEntity.PrimaryColumnValue;
		}

		private void InternalSaveSession(CopilotSession copilotSession) {
			Entity sessionEntity = _userConnection.EntitySchemaManager.GetInstanceByName("CopilotSessionEnt").CreateEntity(_userConnection);
			sessionEntity.UseAdminRights = false;
			if (!sessionEntity.FetchFromDB(copilotSession.Id)) {
				sessionEntity.SetDefColumnValues();
				sessionEntity.PrimaryColumnValue = copilotSession.Id;
			}
			if (copilotSession.Title.IsNotNullOrEmpty()) {
				sessionEntity.SetColumnValue("Title", copilotSession.Title);
			}
			sessionEntity.SetColumnValue("StartDate", copilotSession.StartDate);
			sessionEntity.SetColumnValue("EndDate", copilotSession.EndDate);
			sessionEntity.SetColumnValue("UserId", copilotSession.UserId);
			sessionEntity.SetColumnValue("StateId", copilotSession.State);
			sessionEntity.SetColumnValue("CurrentIntentId", copilotSession.CurrentIntentId);
			sessionEntity.SetColumnValue("RootIntentId", copilotSession.RootIntentId);
			sessionEntity.SetColumnValue("BoundedIntentId", copilotSession.BoundedIntentId);
			sessionEntity.SetColumnValue("RootSessionId", copilotSession.RootSessionId);
			sessionEntity.SetColumnValue("ProcessElementId", copilotSession.ProcessElementId);
			int maxOutputTokens = copilotSession.MaxOutputTokens.GetValueOrDefault();
			sessionEntity.SetColumnValue("MaxOutputTokens", maxOutputTokens > 0 ? maxOutputTokens : 0);
			string filterDescriptors = copilotSession.FilterDescriptors?.Any() == true
				? Json.Serialize(copilotSession.FilterDescriptors)
				: null;
			sessionEntity.SetColumnValue("FilterDescriptors", filterDescriptors);
			sessionEntity.Save();
		}

		private void InternalSaveMessage(CopilotMessage copilotMessage, Guid copilotSessionId) {
			if (copilotMessage.IsSaved) {
				return;
			}
			Entity messageEntity = 
				_userConnection.EntitySchemaManager.GetInstanceByName("CopilotMessageEnt").CreateEntity(_userConnection);
			messageEntity.UseAdminRights = false;
			if (!messageEntity.FetchFromDB(copilotMessage.Id)) {
				messageEntity.SetDefColumnValues();
				messageEntity.PrimaryColumnValue = copilotMessage.Id;
			} else {
				DeleteToolCalls(copilotMessage.Id);
			}
			messageEntity.SetColumnValue("IsSummary", copilotMessage.IsSummary);
			messageEntity.SetColumnValue("SummarizedById", copilotMessage.SummarizedById);
			messageEntity.SetColumnValue("ToolCallId", copilotMessage.ToolCallId);
			messageEntity.SetColumnValue("Content", copilotMessage.Content);
			messageEntity.SetColumnValue("RoleId", RoleMapping[copilotMessage.Role]);
			messageEntity.SetColumnValue("CreatedOn", copilotMessage.Date);
			messageEntity.SetColumnValue("IntentId", copilotMessage.IntentId);
			messageEntity.SetColumnValue("CopilotRequestId", copilotMessage.CopilotRequestId);
			messageEntity.SetColumnValue("RootIntentId", copilotMessage.RootIntentId);
			messageEntity.SetColumnValue("CopilotSessionId", copilotSessionId);
			messageEntity.SetColumnValue("IsFromSystemIntent", copilotMessage.IsFromSystemIntent);
			messageEntity.SetColumnValue("IsFromSystemPrompt", copilotMessage.IsFromSystemPrompt);
			messageEntity.SetColumnValue("ForwardToClient", copilotMessage.ForwardToClient);
			messageEntity.SetColumnValue("ContentType", copilotMessage.ContentType);
			messageEntity.SetColumnValue("OmitAssistantResponse", copilotMessage.OmitAssistantResponse);
			messageEntity.SetColumnValue("ConfirmationResult", copilotMessage.ConfirmationResult);
			messageEntity.SetColumnValue("ToolCallIdsRequireConfirmation", copilotMessage.ToolCallIdsRequireConfirmation);
			messageEntity.SetColumnValue("ProcessElementId", copilotMessage.ProcessElementId);
			copilotMessage.IsSaved = messageEntity.Save();
			InternalSaveToolCalls(copilotMessage);
		}

		private void InternalSaveToolCalls(CopilotMessage copilotMessage) {
			EntitySchema toolCallEntitySchema = 
				_userConnection.EntitySchemaManager.GetInstanceByName("CopilotToolCallEnt");
			foreach (ToolCall toolCall in copilotMessage.ToolCalls) {
				Entity toolCallEntity = toolCallEntitySchema.CreateEntity(_userConnection);
				toolCallEntity.UseAdminRights = false;
				toolCallEntity.SetDefColumnValues();
				toolCallEntity.SetColumnValue("ToolCallId", toolCall.Id);
				toolCallEntity.SetColumnValue("Arguments", toolCall.FunctionCall?.Arguments);
				toolCallEntity.SetColumnValue("FunctionName", toolCall.FunctionCall?.Name);
				toolCallEntity.SetColumnValue("CopilotMessageId", copilotMessage.Id);
				toolCallEntity.SetColumnValue("ConfirmationResult", toolCall.ConfirmationResult);
				toolCallEntity.Save();
			}
		}

		private void DeleteToolCalls(Guid copilotMessageId) {
			new Delete(_userConnection)
				.From("CopilotToolCallEnt")
				.Where("CopilotMessageId").IsEqual(Column.Parameter(copilotMessageId))
				.Execute();
		}

		private DateTime? GetDateTimeAsUtc(Entity entity, string columnName) {
			object dateTimeObj = entity.GetColumnValue(columnName);
			if (dateTimeObj == null) {
				return null;
			}
			var dateTime = (DateTime)dateTimeObj;
			return TimeZoneUtilities.ConvertToUtc(_userConnection, dateTime);
		}

		private CopilotSession InternalLoadSession(Guid sessionId) {
			Entity sessionEntity = _userConnection.EntitySchemaManager.GetInstanceByName("CopilotSessionEnt")
				.CreateEntity(_userConnection);
			sessionEntity.UseAdminRights = false;
			if (!sessionEntity.FetchFromDB(sessionId)) {
				return null;
			}
			int maxOutputTokens = sessionEntity.GetTypedColumnValue<int>("MaxOutputTokens");
			var copilotSession = new CopilotSession {
				FilterDescriptors = new List<AIToolFilterDescriptor>(),
				Id = sessionEntity.PrimaryColumnValue,
				Title = sessionEntity.GetTypedColumnValue<string>("Title"),
				StartDate = GetDateTimeAsUtc(sessionEntity, "StartDate"),
				EndDate = GetDateTimeAsUtc(sessionEntity, "EndDate"),
				UserId = sessionEntity.GetTypedColumnValue<Guid>("UserId"),
				State = sessionEntity.GetTypedColumnValue<Guid>("StateId"),
				CurrentIntentId = (Guid?)sessionEntity.GetColumnValue("CurrentIntentId"),
				RootIntentId = (Guid?)sessionEntity.GetColumnValue("RootIntentId"),
				BoundedIntentId = (Guid?)sessionEntity.GetColumnValue("BoundedIntentId"),
				RootSessionId = (Guid?)sessionEntity.GetColumnValue("RootSessionId"),
				ProcessElementId = (Guid?)sessionEntity.GetColumnValue("ProcessElementId"),
				MaxOutputTokens = maxOutputTokens > 0 ? (int?)maxOutputTokens : null,
				LoadedOn = DateTime.UtcNow
			};
			string filterDescriptors = sessionEntity.GetTypedColumnValue<string>("FilterDescriptors");
			if (filterDescriptors.IsNotNullOrEmpty()) {
				copilotSession.FilterDescriptors = Json.Deserialize<List<AIToolFilterDescriptor>>(filterDescriptors) ??
					new List<AIToolFilterDescriptor>();
			}
			List<CopilotMessage> messages = LoadSessionMessages(sessionId);
			LoadRootIntentCaption(messages);
			copilotSession.AddMessages(messages.Where(x => !x.IsSummary), skipIntentUpdate: true);
			copilotSession.AddSummaryMessages(messages.Where(x => x.IsSummary));
			_documentTool.LoadSessionDocuments(_userConnection, copilotSession);
			return copilotSession;
		}

		private void LoadRootIntentCaption(List<CopilotMessage> messages) {
			foreach (var message in messages) {
				message.RootIntentCaption = _intentsStorage.GetIntentCaptionByIntentId(message.RootIntentId);
			}
		}

		private List<CopilotMessage> LoadSessionMessages(Guid sessionId) {
			var messages = new List<CopilotMessage>();
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "CopilotMessageEnt") {
				UseAdminRights = false
			};
			esq.AddAllSchemaColumns();
			esq.Columns.GetByName("CreatedOn").OrderByAsc(0);
			esq.Columns.GetByName("ModifiedOn").OrderByAsc(1);
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "CopilotSession", sessionId));
			EntityCollection entities = esq.GetEntityCollection(_userConnection);
			foreach (Entity entity in entities) {
				var messageId = entity.GetTypedColumnValue<Guid>("Id");
				var roleId = entity.GetTypedColumnValue<Guid>("RoleId");
				string role = CopilotMessageRole.System;
				if (!RoleMapping.ContainsValue(roleId)) {
					_log.Error($"Role {roleId} is unrecognized for the message {messageId}");
				} else {
					role = RoleMapping.First(r => r.Value == roleId).Key;
				}
				DateTime? createdOn = GetDateTimeAsUtc(entity, "CreatedOn");
				List<ToolCall> toolCalls = LoadMessageToolCalls(messageId);
				var message = new CopilotMessage {
					Id = messageId,
					SummarizedById = (Guid?)entity.GetColumnValue("SummarizedById"),
					IsSummary = entity.GetTypedColumnValue<bool>("IsSummary"),
					IsFromSystemIntent = entity.GetTypedColumnValue<bool>("IsFromSystemIntent"),
					IsFromSystemPrompt = entity.GetTypedColumnValue<bool>("IsFromSystemPrompt"),
					Content = entity.GetTypedColumnValue<string>("Content"),
					Role = role,
					CreatedOnTicks = createdOn?.Ticks ?? DateTime.MinValue.Ticks,
					IntentId = (Guid?)entity.GetColumnValue("IntentId"),
					RootIntentId = (Guid?)entity.GetColumnValue("RootIntentId"),
					CopilotRequestId = (Guid?)entity.GetColumnValue("CopilotRequestId"),
					ToolCallId = entity.GetTypedColumnValue<string>("ToolCallId"),
					ToolCalls = toolCalls,
					IsSentToClient = true,
					IsSaved = true,
					ForwardToClient = entity.GetTypedColumnValue<bool>("ForwardToClient"),
					ContentType = entity.GetTypedColumnValue<string>("ContentType"),
					OmitAssistantResponse = entity.GetTypedColumnValue<bool>("OmitAssistantResponse"),
					ConfirmationResult = entity.GetTypedColumnValue<string>("ConfirmationResult"),
					ToolCallIdsRequireConfirmation = entity.GetTypedColumnValue<string>("ToolCallIdsRequireConfirmation"),
					ProcessElementId = entity.GetColumnValue("ProcessElementId")?.ToString()
				};
				messages.Add(message);
			}
			return messages;
		}

		private List<ToolCall> LoadMessageToolCalls(Guid messageId) {
			var toolCalls = new List<ToolCall>();
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "CopilotToolCallEnt") {
				UseAdminRights = false
			};
			esq.AddAllSchemaColumns();
			esq.Columns.GetByName("CreatedOn").OrderByAsc();
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "CopilotMessage", messageId));
			var entities = esq.GetEntityCollection(_userConnection);
			foreach (var entity in entities) {
				toolCalls.Add(new ToolCall {
					Id = entity.GetTypedColumnValue<string>("ToolCallId"),
					FunctionCall = new FunctionCall {
						Name = entity.GetTypedColumnValue<string>("FunctionName"),
						Arguments = entity.GetTypedColumnValue<string>("Arguments")
					},
					ConfirmationResult = entity.GetTypedColumnValue<string>("ConfirmationResult")
				});
			}
			return toolCalls;
		}

		#endregion

		#region Methods: Public

		public Guid SaveCopilotRequest(CopilotRequestInfo requestInfo) {
			return InternalSaveCopilotRequest(requestInfo);
		}

		public void SaveSession(CopilotSession copilotSession) {
			InternalSaveSession(copilotSession);
			var allMessages = new[] { copilotSession.Messages, copilotSession.SummaryMessages }
				.SelectMany(m => m);
			foreach (CopilotMessage copilotMessage in allMessages) {
				InternalSaveMessage(copilotMessage, copilotSession.Id);
			}
		}

		public CopilotSession LoadSession(Guid sessionId) {
			return InternalLoadSession(sessionId);
		}

		#endregion

	}
}


