namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Client
{
	using System;
	using System.Collections.Generic;
	using IntegrationApi.MailboxDomain.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Client.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.DTO;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using Terrasoft.Core.Factories;
	using IntegrationApi.Interfaces;
	using Terrasoft.IntegrationV2.Utils;
	using Terrasoft.GoogleServices;
	using Terrasoft.Core;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Social;

	#region Class: ExchangeCalendarClient
	/// <summary>
	/// <see cref="ICalendarClient"/> implementation.
	/// </summary>
	[DefaultBinding(typeof(ICalendarClient), Name = "Google")]
	public class GoogleCalendarClient : ICalendarClient
	{

		#region Fields: Private

		private UserConnection _uc;

		/// <summary>
		/// <see cref="ICalendarLogger"/> implementation instance.
		/// </summary
		private readonly ICalendarLogger _log;

		/// <summary>
		/// <see cref="ISynchronizationErrorHelper"/> implementation instance.
		/// </summary
		private readonly ISynchronizationErrorHelper _syncErrorHelper;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctor.
		/// </summary>
		public GoogleCalendarClient() {
		}

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="sessionId">Synchronization session identifier.</param>
		/// <param name="synchronizationErrorHelper"><see cref="ISynchronizationErrorHelper"/> implementation instance.</param>
		public GoogleCalendarClient(string sessionId, ISynchronizationErrorHelper synchronizationErrorHelper) : this() {
			_syncErrorHelper = synchronizationErrorHelper;
			_log = ClassFactory.Get<ICalendarLogger>(new ConstructorArgument("sessionId", sessionId),
				new ConstructorArgument("modelName", GetType().Name));
		}

		#endregion

		#region Methods: Private 

		/// <summary>
		/// Creates and initializes <see cref="GActivitySyncProvider"/> instance.
		/// </summary>
		/// <param name="settings">Current calendar settings.</param>
		/// <returns><see cref="GActivitySyncProvider"/> instance.</returns>
		/// <remarks>
		/// We have access token from OAuthTokenStorage as part of <paramref name="settings"/>
		/// and only thing we need to skip tokenProvider is corresponding refresh token.
		/// As for now this token is not provided for configuration, and avaliable only via OAuthTokenManager in core.
		/// It is not OK to allow that API in configuartion, and there is no point
		/// to change core to use different OAuth autorization flow in old google integration.
		/// </remarks>
		private GActivitySyncProvider GetProvider(CalendarSettings settings) {
			var mailboxService = ClassFactory.Get<IMailboxService>(new ConstructorArgument("uc", _uc));
			var mailbox = mailboxService.GetMailbox(settings.Id);
			var syncProvider = new GActivitySyncProvider() {
				UserConnection = _uc,
				MailServerId = mailbox.GetServerId(),
			};
			
			lock (BaseConsumer.TokenManagers) {
				var tokenProvider = BaseConsumer.GetTokenProvider(SocialNetwork.Google, mailbox.GetServerId());
				var userToken = tokenProvider.FindUserTokenByUserId(_uc.CurrentUser.Id.ToString());
				syncProvider.Authenticate(userToken.AccessToken, userToken.AccessSecretToken, userToken.Login);
			}
			return syncProvider;
		}

		private List<GoogleActivityParticipant> GetParticipants(Meeting meeting, GoogleActivity entry) {
			//meeting participants to entry logic here (same as in GCalendarTool.ActivityParticipantsToGoogleEvent method
			List<GoogleActivityParticipant> gParticipants = entry.Participants == null
				? new List<GoogleActivityParticipant>()
				: new List<GoogleActivityParticipant>(entry.Participants);
			foreach(var p in meeting.Participants) {
				if (p.EmailAddress.IsNullOrEmpty()) { continue; }
				if (!gParticipants.Any(gp => gp.Email == p.EmailAddress)) {
					gParticipants.Add(new GoogleActivityParticipant {
						Email = p.EmailAddress,
						ResponseStatus = "needsAction"
					});
				}
			}
			return gParticipants;
		}

		#endregion

		#region Methods: Public

		public void SetUserConnection(UserConnection uc) {
			_uc = uc;
		}

		/// <inheritdoc cref="ICalendarClient.SaveMeeting(Meeting, Calendar)"/>
		public IntegrationId SaveMeeting(Meeting meeting, Calendar calendar) {
			throw new NotImplementedException();
		}

		/// <inheritdoc cref="ICalendarClient.RemoveMeeting(Meeting, Calendar)"/>
		public void RemoveMeeting(Meeting meeting, Calendar calendar) {
			throw new NotImplementedException();
		}

		/// <inheritdoc cref="ICalendarClient.SendInvitations(Meeting, Calendar, bool)"/>
		public void SendInvitations(Meeting meeting, Calendar calendar, bool isOnlineMeeting) {
			if (string.IsNullOrEmpty(meeting.RemoteId) || !calendar.Settings.SyncEnabled
					|| !ListenerUtils.GetIsFeatureEnabled("MeetingInvitation")) {
				return;
			}
			var syncProvider = GetProvider(calendar.Settings);
			var entry = new GoogleActivity(true);
			if (!entry.Load(syncProvider, meeting.RemoteId)) {
				return;
			}
			entry.Participants = GetParticipants(meeting, entry);
			syncProvider.UpdateEntities(new List<GoogleActivity> { entry });
			syncProvider.Commit();
			meeting.InvitesSent = true;
		}

		/// <inheritdoc cref="ICalendarClient.GetSyncPeriodMeetings(Calendar, bool)"/>
		public List<MeetingDto> GetSyncPeriodMeetings(Calendar calendar, out bool isAllMeetingsLoaded) {
			throw new NotImplementedException();
		}

		/// <inheritdoc cref="ICalendarClient.GetDeletedMeetings(Calendar)"/>
		public List<MeetingDto> GetDeletedMeetings(Calendar calendar) {
			throw new NotImplementedException();
		}

		/// <inheritdoc cref="ICalendarClient.SetExtendedProperty(Meeting, Calendar, string, object)"/>
		public void SetExtendedProperty(Meeting meeting, Calendar calendar, string extendedPropetyName, object  extendedPropetyValue) {
			throw new NotImplementedException();
		}

		#endregion

	}

	#endregion

}
