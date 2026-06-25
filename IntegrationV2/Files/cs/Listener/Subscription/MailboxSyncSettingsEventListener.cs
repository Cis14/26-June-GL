namespace IntegrationV2.Files.cs.Listener.Subscription
{
	using Creatio.FeatureToggling;
	using IntegrationApi.Interfaces;
	using IntegrationApi.MailboxDomain.Interfaces;
	using System;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Core.Factories;
	using Terrasoft.IntegrationV2.Utils;
	using Terrasoft.Messaging.Common;

	#region Class: MailboxSyncSettingsEventListener

	[EntityEventListener(SchemaName = "MailboxSyncSettings")]
	[EntityEventListener(SchemaName = "VwSysAdminUnit")]
	public class MailboxSyncSettingsEventListener: BaseEntityEventListener {

		#region Properties: Private

		private IMsgChannelManager _channelManager;
		private IMsgChannelManager ChannelManager {
			get {
				if (_channelManager != null) {
					return _channelManager;
				}
				if (MsgChannelManager.IsRunning) {
					_channelManager = MsgChannelManager.Instance;
				}
				return _channelManager;
			}
		}

		#endregion

		#region Methods: Private

		private void SendInfoToClient(string eventName, string messageBody, Guid userId, string bodyType) {
			try {
				var channel = ChannelManager.FindItemByUId(userId);
				if (channel == null) {
					return;
				}
				var simpleMessage = new SimpleMessage {
					Id = userId,
					Body = messageBody,
				};
				simpleMessage.Header.Sender = eventName;
				simpleMessage.Header.BodyTypeName = bodyType;
				channel.PostMessage(simpleMessage);
			} catch (Exception) { }
		}

		private void SendInfoToClient(string eventName, Entity entity) {
			if (entity.SchemaName != "MailboxSyncSettings") {
				return;
			}
			var messageBody = $"{{\"Id\":\"{entity.PrimaryColumnValue}\"," +
					$"\"Caption\": \"{entity.GetTypedColumnValue<string>("SenderEmailAddress")}\"," +
					$"\"IsShared\": \"{entity.GetTypedColumnValue<bool>("IsShared")}\" }}";
			SendInfoToClient(eventName, messageBody, entity.UserConnection.CurrentUser.Id, "Info");
		}

		private void TryStopSubscription(Guid mailboxId, UserConnection uc) {
			try {
				var managerFactory = ClassFactory.Get<IListenerManagerFactory>();
				var manager = managerFactory.GetExchangeListenerManager(uc);
				manager.StopListener(mailboxId);
			} catch (Exception ex) {
				SendInfoToClient("SyncMsgLogger", $"Error on stopListener: {ex}", uc.CurrentUser.Id, "Error");
			}
		}

		private void ProcessMailboxChange(Entity entity) {
			Guid oldTokenId = entity.GetTypedOldColumnValue<Guid>("OAuthTokenStorageId");
			Guid tokenId = entity.GetTypedColumnValue<Guid>("OAuthTokenStorageId");
			string oldPassword = entity.GetTypedOldColumnValue<string>("UserPassword");
			string password = entity.GetTypedColumnValue<string>("UserPassword");
			if ((oldPassword.IsNotNullOrEmpty() && password.IsNullOrEmpty())
					|| (oldTokenId.IsNotEmpty() && tokenId.IsEmpty())) {
				TryStopSubscription(entity.PrimaryColumnValue, entity.UserConnection);
			}
		}

		private void ProcessOwnerChange(Entity entity) {
			var oldActive = entity.GetTypedOldColumnValue<bool>("Active");
			var active = entity.GetTypedColumnValue<bool>("Active");
			if (!active && oldActive) {
				var userConnection = entity.UserConnection;
				var mailboxService = ClassFactory.Get<IMailboxService>(new ConstructorArgument("uc", userConnection));
				foreach (var mailbox in mailboxService.GetAllSynchronizableMailboxes().Where(m => m.OwnerId == entity.PrimaryColumnValue)) {
					TryStopSubscription(mailbox.Id, userConnection);
				}
			}
		}

		private void DeleteCalendar(Entity entity) {
			if (entity.SchemaName != "MailboxSyncSettings" || !Features.GetIsEnabled("GoogleMultidomain")) {
				return;
			}
			var uc = entity.UserConnection;
			var schema = uc.EntitySchemaManager.GetInstanceByName("SocialAccount");
			var socialAccountEntity = schema.CreateEntity(uc);
			if (socialAccountEntity.FetchFromDB("MailboxSyncSettings", entity.PrimaryColumnValue)
					&& socialAccountEntity.PrimaryColumnValue.IsNotEmpty()) {
				socialAccountEntity.Delete();
			}
		}

		private void DeleteOAuthTokenStorage(Entity entity)
		{
			if (entity.SchemaName != "MailboxSyncSettings")
			{
				return;
			}

			var uc = entity.UserConnection;
			var oAuthTokenStorageId = entity.GetTypedColumnValue<Guid>("OAuthTokenStorageId");

			if (oAuthTokenStorageId == Guid.Empty)
			{
				return;
			}

			var schema = uc.EntitySchemaManager.GetInstanceByName("OAuthTokenStorage");
			var oAuthTokenStorage = schema.CreateEntity(uc);

			if (oAuthTokenStorage.FetchFromDB(oAuthTokenStorageId))
			{
				oAuthTokenStorage.Delete();
			}
		}


		#endregion

		#region Methods: Internal

		internal void SetMsgChannelManager(IMsgChannelManager channelManager) {
			_channelManager = channelManager;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// <see cref="BaseEntityEventListener.OnDeleting"/>
		/// </summary>
		public override void OnDeleting(object sender, EntityBeforeEventArgs e) {
			base.OnDeleting(sender, e);
			var entity = (Entity)sender;
			TryStopSubscription(entity.PrimaryColumnValue, entity.UserConnection);
			DeleteCalendar(entity);
        }

		/// <inheritdoc cref="BaseEntityEventListener.OnDeleted"/>
		public override void OnDeleted(object sender, EntityAfterEventArgs e) {
			base.OnDeleted(sender, e);
			var entity = (Entity)sender;
            DeleteOAuthTokenStorage(entity);
            SendInfoToClient("MailboxDeleted", entity);
		}

		/// <summary>
		/// <see cref="BaseEntityEventListener.OnInserted"/>
		/// </summary>
		public override void OnInserted(object sender, EntityAfterEventArgs e) {
			base.OnInserted(sender, e);
			var entity = (Entity)sender;
			SendInfoToClient("MailboxAdded", entity);
		}

		/// <summary>
		/// <see cref="BaseEntityEventListener.OnUpdated"/>
		/// </summary>
		public override void OnUpdated(object sender, EntityAfterEventArgs e) {
			base.OnUpdated(sender, e);
			var entity = (Entity)sender;
			if (entity.SchemaName != "MailboxSyncSettings") {
				return;
			}
			ProcessMailboxChange(entity);
		}

		/// <summary>
		/// <see cref="BaseEntityEventListener.OnUpdating(object, EntityBeforeEventArgs)"/>
		/// </summary>
		public override void OnUpdating(object sender, EntityBeforeEventArgs e) {
			base.OnUpdating(sender, e);
			var entity = (Entity)sender;
			if (entity.SchemaName != "VwSysAdminUnit") {
				return;
			}
			ProcessOwnerChange(entity);
		}

		#endregion

	}

	#endregion

}
