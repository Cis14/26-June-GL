namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Newtonsoft.Json;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: RemindingUtilities

	/// <summary>
	/// Utility methods for notification messages.
	/// </summary>
	public class RemindingUtilities
	{

		#region Fields: Private

		private const int DescriptionTypeStringLength = 250;

		private const int SubjectCaptionTypeStringLength = 500;

		#endregion

		#region Methods: Private

		private string CreateRemindingHash(Guid contactId, Guid authorId, RemindingConfig config) {
			var condition = new Dictionary<string, object> {
					{ "Author", authorId },
					{ "Contact", contactId },
					{ "Source", RemindingConsts.RemindingSourceAuthorId },
					{ "SubjectId", config.SubjectId },
					{ "SysEntitySchema", config.EntitySchemaUId }
				};
			var str = new StringBuilder();
			foreach (object value in condition.Values) {
				str.Append(value);
			}
			return FileUtilities.GetMD5Hash(Encoding.Unicode.GetBytes(str.ToString()));
		}

		private string TruncateString(string source, int length) {
			return (source.Length > length)
				? source = source.Substring(0, length)
				: source;
		}

		private bool FindRecordByHash(Entity entity, string hash) {
			return entity.FetchFromDB(new Dictionary<string, object> { { "Hash", hash } }, false);
		}

		private Entity GetRemindingEntity(UserConnection userConnection, RemindingConfig config) {
			string hash = config.UpdateExisting
				? CreateRemindingHash(config.ContactId, config.AuthorId, config)
				: string.Empty;
			EntitySchema remindingSchema = userConnection.EntitySchemaManager.GetInstanceByName("Reminding");
			Entity newReminding = remindingSchema.CreateEntity(userConnection);
			if (!config.UpdateExisting || !FindRecordByHash(newReminding, hash)) {
				newReminding.SetDefColumnValues();
			}
			if (config.UpdateExisting) {
				newReminding.SetColumnValue("Hash", hash);
			}
			return newReminding;
		}

		private void ValidateConfig(RemindingConfig config) {
			config.CheckArgumentNull(nameof(config));
			if (config.AuthorId == Guid.Empty) {
				throw new ArgumentEmptyException("AuthorId");
			}
			if (config.ContactId == Guid.Empty) {
				throw new ArgumentEmptyException("ContactId");
			}
			if (config.UpdateExisting && config.SubjectId == Guid.Empty) {
				throw new ArgumentEmptyException("SubjectId");
			}
		}
		
		private DateTime GetUserDateTime(UserConnection userConnection, RemindingConfig config) {
			return config.RemindTime.Equals(default(DateTime)) ? 
				userConnection.CurrentUser.GetCurrentDateTime() :
				config.RemindTime;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Creates notification message for user.
		/// </summary>
		/// <param name="userConnection">Instance of user connection.</param>
		/// <param name="config">Configuration object for notification message.</param>
		public virtual void CreateReminding(UserConnection userConnection, RemindingConfig config) {
			ValidateConfig(config);
			DateTime userDateTime = GetUserDateTime(userConnection, config);
			string subject = TruncateString(config.Description, SubjectCaptionTypeStringLength);
			string description = TruncateString(config.Description, DescriptionTypeStringLength);
			Entity remindingEntity = GetRemindingEntity(userConnection, config);
			remindingEntity.SetColumnValue("ModifiedOn", userDateTime);
			remindingEntity.SetColumnValue("AuthorId", config.AuthorId);
			remindingEntity.SetColumnValue("ContactId", config.ContactId);
			remindingEntity.SetColumnValue("SourceId", config.SourceId);
			remindingEntity.SetColumnValue("RemindTime", userDateTime);
			remindingEntity.SetColumnValue("Description", description);
			remindingEntity.SetColumnValue("SubjectCaption", subject);
			remindingEntity.SetColumnValue("SysEntitySchemaId", config.EntitySchemaUId);
			remindingEntity.SetColumnValue("SubjectId", config.SubjectId);
			remindingEntity.SetColumnValue("NotificationTypeId", config.NotificationTypeId);
			remindingEntity.SetColumnValue("LoaderId", config.LoaderId);
			remindingEntity.SetColumnValue("SenderId", config.SenderId);
			remindingEntity.SetColumnValue("IsNeedToSend", config.IsNeedToSend);
			remindingEntity.SetColumnValue("PopupTitle", config.PopupTitle);
			remindingEntity.SetColumnValue("Config", config.Config != null 
				? JsonConvert.SerializeObject(config.Config)
				: string.Empty);
			remindingEntity.Save();
		}

		#endregion

	}

	#endregion

	#region Class: RemindingConfig

	/// <summary>
	/// Parameters configuration for notification message.
	/// </summary>
	public class RemindingConfig
	{

		#region Constructors: Public

		/// <summary>
		/// Returns instance of reminding configuration.
		/// </summary>
		/// <param name="schemaUId">Identifier of entity schema for notification.</param>
		/// <returns>Instance of reminding configuration.</returns>
		public RemindingConfig(Guid schemaUId) {
			_entitySchemaUId = schemaUId;
		}

		/// <summary>
		/// Returns instance of reminding configuration.
		/// </summary>
		/// <param name="entity">Instance of entity for notification.</param>
		/// <returns>Instance of reminding configuration.</returns>
		public RemindingConfig(Entity entity) : this(entity.Schema.UId) {
			_subjectId = entity.PrimaryColumnValue;
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Identifier of entity schema.
		/// </summary>
		private Guid _entitySchemaUId;
		public Guid EntitySchemaUId {
			get {
				return _entitySchemaUId;
			}
		}

		/// <summary>
		/// Identifier of notification subject.
		/// </summary>
		private Guid _subjectId;
		public Guid SubjectId {
			set {
				_subjectId = value;
			}
			get {
				return _subjectId;
			}
		}

		/// <summary>
		/// Makes update of existing notification instead of creating new when true.
		/// </summary>
		public bool UpdateExisting { 
			get;
			set;
		}

		/// <summary>
		/// Text of notification message.
		/// </summary>
		public string Description {
			get;
			set;
		}
		
		/// <summary>
		/// Text of popup title.
		/// </summary>
		public string PopupTitle {
			get;
			set;
		}

		/// <summary>
		/// Identifier of author.
		/// </summary>
		public Guid AuthorId {
			get;
			set;
		}

		/// <summary>
		/// Identifier of contact.
		/// </summary>
		public Guid ContactId {
			get;
			set;
		}

		/// <summary>
		/// Time of send.
		/// </summary>
		public DateTime RemindTime {
			get;
			set;
		}

		/// <summary>
		/// Identifier of type.
		/// </summary>
		private Guid _notificationTypeId; 
		public Guid NotificationTypeId {
			get {
				if (_notificationTypeId.IsEmpty()) {
					_notificationTypeId = RemindingConsts.NotificationTypeNotificationId;
				}
				return _notificationTypeId;
			}
			set {
				_notificationTypeId = value;
			}
		}

		/// <summary>
		/// Identifier of loader.
		/// </summary>
		public Guid? LoaderId {
			get;
			set;
		}
		
		/// <summary>
		/// Identifier of sender.
		/// </summary>
		public Guid? SenderId {
			get;
			set;
		}

		/// <summary>
		/// Signs that message of reminding need to send. 
		/// </summary>
		public bool IsNeedToSend {
		 	get;
			set;
		}

		/// <summary>
		/// Identifier of source.
		/// </summary>
		private Guid _sourceId;
		public Guid SourceId {
			get {
				if (_sourceId.IsEmpty()) {
					_sourceId = RemindingConsts.RemindingSourceAuthorId;
				}
				return	_sourceId;
			}
			set {
				_sourceId = value;
			}
		}

		/// <summary>
		/// Notification custom configuration.
		/// </summary>
		public IDictionary<string, object> Config { get; set; }

		#endregion

	}
	
	#endregion

}
