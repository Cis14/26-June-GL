namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Messaging.Common;
	using global::Common.Logging;

	#region Class: ImportNotifier

	[DefaultBinding(typeof(IImportNotifier), Name = nameof(ImportNotifier))]
	public class ImportNotifier : IImportNotifier
	{

		#region Constants: Private

		private const string MessageBodyFormat =
			"{{\"importSessionId\": \"{0}\", \"status\": {{\"stage\": \"{1}\", \"percent\": {2}}}}}";

		/// <summary>
		/// Message header.
		/// </summary>
		private const string MessageHeader = "FileImport";

		/// <summary>
		/// Message header.
		/// </summary>
		private const string TagImportMessageHeader = "TagImport";

		#endregion

		#region Fields: Private

		/// <summary>
		/// One percent value.
		/// </summary>
		private double _onePercentValue;

		/// <summary>
		/// User connection.
		/// </summary>
		private IMsgChannel _channel;

		private readonly string _resourceManagerName = nameof(ImportNotifier);

		#endregion

		#region Properties: Protected

		private ILog _logger;
		protected virtual ILog Logger { get => _logger ?? (_logger = LogManager.GetLogger("FileImportAppender")); }

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="ImportNotifier"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		/// <param name="importParameters">Import parameters.</param>
		public ImportNotifier(UserConnection userConnection, ImportParameters importParameters) {
			UserConnection = userConnection;
			ImportParameters = importParameters;
			InitMsgChannel();
		}

		private void InitMsgChannel() {
			try {
				_channel = MsgChannelManager.Instance.FindItemByUId(UserConnection.CurrentUser.Id);
				if (_channel != null) {
					_channel.OnMessage += ImportMsgHandler;
				}
			} catch(Exception e) {
				Logger.Error($"Import - {ImportParameters.ImportSessionId} could not get the instance of MsgChannelManager", e);
			}
		}

		protected ImportParameters ImportParameters { get; }

		/// <summary>
		/// User connection.
		/// </summary>
		protected UserConnection UserConnection { get; }

		#endregion

		#region Properties: Private

		private LocalizableString CompleteRemindingDescriptionTemplate =>
			new LocalizableString(ResourceStorage, _resourceManagerName,
				"LocalizableStrings.CompleteRemindingDescriptionTemplate.Value").ToString();

		private LocalizableString NotImportedRowsCountMessageTemplate =>
			new LocalizableString(ResourceStorage, _resourceManagerName,
				"LocalizableStrings.NotImportedRowsCountMessageTemplate.Value").ToString();

		private LocalizableString LookupProcessingErrorsLabel =>
			new LocalizableString(ResourceStorage, "FileImportResultPage",
				"LocalizableStrings.LookupProcessingErrorsLabel.Value").ToString();

		private LocalizableString CompleteRemindingSubject =>
			new LocalizableString(ResourceStorage, _resourceManagerName,
				"LocalizableStrings.CompleteRemindingSubject.Value").ToString();

		private IResourceStorage ResourceStorage => UserConnection.Workspace.ResourceStorage;

		#endregion

		#region Methods: Private

		/// <summary>
		/// Returns one percent value.
		/// </summary>
		/// <param name="rowsCount">Rows count.</param>
		/// <returns>One percent value.</returns>
		private double GetOnePercentValue(uint rowsCount) {
			if (_onePercentValue > 0) {
				return _onePercentValue;

			}
			_onePercentValue = Math.Ceiling(rowsCount / 100f);
			return _onePercentValue;
		}

		/// <summary>
		/// Returns one percent value.
		/// </summary>
		/// <param name="stage"></param>
		/// <param name="percent"></param>
		/// <returns>One percent value.</returns>
		private void PostMessage(string stage, uint percent) {
			if (_channel == null) {
				return;
			}
			SimpleMessage simpleMessage = new SimpleMessage {
				Body = string.Format(MessageBodyFormat, ImportParameters.ImportSessionId, stage, percent)
			};
			simpleMessage.Header.Sender = MessageHeader;
			_channel.PostMessage(simpleMessage);
		}

		private void CreateReminding() {
			Guid contactId = ImportParameters.AuthorId == Guid.Empty
				? UserConnection.CurrentUser.ContactId
				: ImportParameters.AuthorId;
			DateTime remindingTime = UserConnection.CurrentUser.GetCurrentDateTime();
			uint notImportedRowsCount = ImportParameters.TotalRowsCount - ImportParameters.ImportedRowsCount;
			string description = string.Format(CompleteRemindingDescriptionTemplate, ImportParameters.ImportedRowsCount,
				ImportParameters.TotalRowsCount, ImportParameters.FileName);
			if (notImportedRowsCount > 0) {
				description += string.Format(NotImportedRowsCountMessageTemplate, notImportedRowsCount);
			}
			if (ImportParameters.HasLookupProcessingErrors) {
				description += $" {LookupProcessingErrorsLabel}";
			}
			string caption = $"{CompleteRemindingSubject} {description}";
			ISchemaManagerItem<EntitySchema> importSessionItem =
				UserConnection.EntitySchemaManager.GetItemByName("ImportSession");
			ISchemaManagerItem<EntitySchema> sysProcessLogItem =
				UserConnection.EntitySchemaManager.GetItemByName("VwSysProcessLog");
			EntitySchema remindingSchema = UserConnection.EntitySchemaManager.GetInstanceByName("Reminding");
			Entity reminding = remindingSchema.CreateEntity(UserConnection);
			reminding.SetDefColumnValues();
			reminding.SetColumnValue("AuthorId", contactId);
			reminding.SetColumnValue("ContactId", contactId);
			reminding.SetColumnValue("SourceId", RemindingConsts.RemindingSourceAuthorId);
			reminding.SetColumnValue("RemindTime", remindingTime);
			reminding.SetColumnValue("Description", description);
			reminding.SetColumnValue("SubjectId", ImportParameters.ImportSessionId);
			reminding.SetColumnValue("SysEntitySchemaId", sysProcessLogItem.UId);
			reminding.SetColumnValue("SubjectCaption", caption);
			reminding.SetColumnValue("LoaderId", importSessionItem.UId);
			reminding.Save();
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IImportNotifier"/>
		public void Notify(object sender, ImportEntitySavingEventArgs eventArgs) {
			uint totalRowsCount = eventArgs.TotalRowsCount;
			uint processedRowsCount = eventArgs.ProcessedRowsCount;
			double onePercentValue = GetOnePercentValue(totalRowsCount);
			if (processedRowsCount % onePercentValue > 0) {
				return;
			}
			uint percent = processedRowsCount * 100 / totalRowsCount;
			PostMessage("processing", percent);
		}

		/// <inheritdoc cref="IImportNotifier"/>
		public void NotifyEnd() {
			PostMessage("complete", 100);
			CreateReminding();
		}

		/// <summary>
		/// Import message handler.
		/// </summary>
		/// <param name="sender">Message sender.</param>
		/// <param name="args">Event arguments.</param>
		public void ImportMsgHandler(IMsgChannel sender, IMsg args) {
			if (args == null || args.Header.Sender != TagImportMessageHeader || args.Body == null) {
				return;
			}
			SimpleMessage message = new SimpleMessage {
					Body = args.Body
			};
			message.Header.Sender = TagImportMessageHeader;
			_channel.PostMessage(message);
		}

		#endregion

	}

	#endregion

}

