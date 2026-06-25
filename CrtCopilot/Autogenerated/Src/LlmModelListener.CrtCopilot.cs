namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Common.Logging;
	using Newtonsoft.Json;
	using Terrasoft.Configuration;
	using Terrasoft.Configuration.GenAI;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Core.Factories;
	using Terrasoft.Enrichment.Interfaces.GenAI.Model;
	using Terrasoft.Messaging.Common;

	#region Class: LlmModelListener

	[EntityEventListener(SchemaName = "LlmModel")]
	public class LlmModelListener : BaseEntityEventListener
	{

		#region Fields: Private

		private static readonly ILog _log = LogManager.GetLogger("LlmModelListener");

		#endregion

		#region Properties: Private

		/// <summary>
		/// Gets the LLM configuration service proxy.
		/// </summary>
		private IGenAILlmConfigurationServiceProxy _llmConfigurationService;
		private IGenAILlmConfigurationServiceProxy LlmConfigurationService {
		    get {
				if (_llmConfigurationService == null) {
					_llmConfigurationService = ClassFactory.Get<IGenAILlmConfigurationServiceProxy>();
				}
				return _llmConfigurationService;
			}
		}

		#endregion

		#region Methods: Private

		private void SendNotification(LlmModel llmmodel, string errorMessage) {
			try {
				if (!MsgChannelManager.IsRunning) {
					return;
				}
				var body = new {
					ModelCode = llmmodel.Code,
					IsOperationSuccessful = string.IsNullOrEmpty(errorMessage),
					ErrorMessage = errorMessage
				};
				var simpleMessage = new SimpleMessage {
					Body = JsonConvert.SerializeObject(body),
					Header = {
						Sender = "LlmModelConfigurationNotifier"
					}
				};
				var userId = llmmodel.UserConnection.CurrentUser.Id;
				MsgChannelManager.Instance.Post(new[] { userId }, simpleMessage);
			} catch (Exception ex) {
				_log.Error($"Send model configuration notification error: {ex.Message}", ex);
			}
		}

		private void UpdateConfigurationFailureReason(LlmModel llmmodel, string failureReason) {
			try {
				var update = new Update(llmmodel.UserConnection, "LlmModel")
					.Set("ConfigurationFailureReason", Column.Parameter(failureReason))
					.Set("ModifiedOn", Column.Parameter(DateTime.UtcNow))
					.Set("ModifiedById", Column.Parameter(llmmodel.UserConnection.CurrentUser.Id))
					.Where("Id").IsEqual(Column.Parameter(llmmodel.Id));
				update.Execute();
			} catch (Exception ex) {
				_log.Error($"Failed to update ConfigurationFailureReason for LlmModel {llmmodel.Id}: {ex.Message}", ex);
			}
		}

		private void ClearConfigurationFailureReason(LlmModel llmmodel) {
			try {
				var update = new Update(llmmodel.UserConnection, "LlmModel")
					.Set("ConfigurationFailureReason", Column.Parameter(string.Empty))
					.Set("ModifiedOn", Column.Parameter(DateTime.UtcNow))
					.Set("ModifiedById", Column.Parameter(llmmodel.UserConnection.CurrentUser.Id))
					.Where("Id").IsEqual(Column.Parameter(llmmodel.Id));
				update.Execute();
			} catch (Exception ex) {
				_log.Error($"Failed to clear ConfigurationFailureReason for LlmModel {llmmodel.Id}: {ex.Message}", ex);
			}
		}

		private void AddLlmConfiguration(LlmModel llmmodel, string unmaskedEncryptedConfig) {
			try {
				var llmModelConfiguration = MapToModelConfiguration(llmmodel, unmaskedEncryptedConfig);
				LlmConfigurationService.AddLlmConfiguration(llmModelConfiguration);
				ClearConfigurationFailureReason(llmmodel);
				SendNotification(llmmodel, null);
			} catch (Exception exception) {
				_log.Error(exception);
				UpdateConfigurationFailureReason(llmmodel, exception.Message);
				SendNotification(llmmodel, exception.Message);
			}
		}

		private string LoadUnmaskedEncryptedConfig(LlmModel llmmodel) {
			var esq = new EntitySchemaQuery(llmmodel.UserConnection.EntitySchemaManager, "LlmModel");
			esq.PrimaryQueryColumn.IsAlwaysSelect = true;
			esq.UnmaskColumnValues = true;
			var encryptedConfigColumn = esq.AddColumn("EncryptedConfig");
			var entity = esq.GetEntity(llmmodel.UserConnection, llmmodel.Id);
			if (entity != null) {
				var unmaskedConfig = entity.GetTypedColumnValue<string>("EncryptedConfig");
				return unmaskedConfig;
			}
			return "{}";
		}

		private bool HasModelConfigurationChanges(LlmModel llmModel) {
			var codeChanged = HasColumnChanged(llmModel, "Code");
			var configChanged = HasColumnChanged(llmModel, "EncryptedConfig");
			return codeChanged || configChanged;
		}

		private bool HasColumnChanged(LlmModel llmModel, string columnName) {
			var currentValue = llmModel.GetColumnValue(columnName);
			var previousValue = llmModel.GetColumnOldValue(columnName);
			return !Equals(previousValue, currentValue);
		}

		private void HandleConfigurationUpdate(LlmModel llmModel, string unmaskedEncryptedConfig) {
			try {
				var llmModelConfiguration = MapToModelConfiguration(llmModel, unmaskedEncryptedConfig);
				LlmConfigurationService.UpdateLlmConfiguration(llmModelConfiguration);
				ClearConfigurationFailureReason(llmModel);
				SendNotification(llmModel, null);
			} catch (Exception exception) {
				HandleUpdateException(llmModel, exception);
			}
		}

		private void HandleUpdateException(LlmModel llmModel, Exception exception) {
			if (IsModelNotFoundError(exception)) {
				var unmaskedEncryptedConfig = LoadUnmaskedEncryptedConfig(llmModel);
				AddLlmConfiguration(llmModel, unmaskedEncryptedConfig);
				return;
			}
			_log.Error(exception);
			UpdateConfigurationFailureReason(llmModel, exception.Message);
			SendNotification(llmModel, exception.Message);
		}

		private bool IsModelNotFoundError(Exception exception) {
			return !string.IsNullOrEmpty(exception.Message) && exception.Message.Contains("model not found");
		}

		private ModelConfiguration MapToModelConfiguration(LlmModel llmModel, string encryptedConfig) {
			Dictionary<string, object> parameters;
			try {
				parameters = string.IsNullOrWhiteSpace(encryptedConfig)
					? new Dictionary<string, object>()
					: JsonConvert.DeserializeObject<Dictionary<string, object>>(encryptedConfig);
			} catch (JsonException ex) {
				throw new InvalidOperationException("Invalid JSON in LlmModel.EncryptedConfig", ex);
			}
			return new ModelConfiguration {
				Code = llmModel.Code,
				Parameters = parameters,
				Metadata = new Dictionary<string, object>()
			};
		}

		#endregion

		#region Methods: Public

		public override void OnInserted(object sender, EntityAfterEventArgs e) {
			var llmmodel = (LlmModel)sender;
			var unmaskedEncryptedConfig = LoadUnmaskedEncryptedConfig(llmmodel);
			AddLlmConfiguration(llmmodel, unmaskedEncryptedConfig);
			base.OnInserted(sender, e);
		}

		public override void OnDeleted(object sender, EntityAfterEventArgs e) {
			var llmmodel = (LlmModel)sender;
			try {
				var unmaskedEncryptedConfig = LoadUnmaskedEncryptedConfig(llmmodel);
				var llmModelConfiguration = MapToModelConfiguration(llmmodel, unmaskedEncryptedConfig);
				LlmConfigurationService.DeleteLlmConfiguration(llmModelConfiguration);
				ClearConfigurationFailureReason(llmmodel);
				SendNotification(llmmodel, null);
			} catch (Exception exception) {
				_log.Error(exception);
				UpdateConfigurationFailureReason(llmmodel, exception.Message);
				SendNotification(llmmodel, exception.Message);
			}
			base.OnDeleted(sender, e);
		}

		public override void OnUpdated(object sender, EntityAfterEventArgs e) {
			var llmModel = (LlmModel)sender;
			if (!HasModelConfigurationChanges(llmModel)) {
				base.OnUpdated(sender, e);
				return;
			}
			var unmaskedEncryptedConfig = LoadUnmaskedEncryptedConfig(llmModel);
			HandleConfigurationUpdate(llmModel, unmaskedEncryptedConfig);
			base.OnUpdated(sender, e);
		}

		#endregion

	}

	#endregion

}

