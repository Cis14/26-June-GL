namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Repository
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Store;
	using Terrasoft.IntegrationV2.Utils;
	using Terrasoft.EmailDomain;
	using Terrasoft.Common;
	using IntegrationApi.MailboxDomain.Interfaces;
	using IntegrationApi.Calendar;
	using IntegrationApi.MailboxDomain.Model;

	#region Class: CalendarRepository
	/// <summary>
	/// <see cref="ICalendarRepository"/> implementation.
	/// </summary>
	[DefaultBinding(typeof(ICalendarRepository), Name = nameof(CalendarRepository))]
	public class CalendarRepository : ICalendarRepository
	{

		#region Fields: Private

		/// <summary>
		/// <see cref="UserConnection"/> instance.
		/// </summary>
		private readonly UserConnection _userConnection;

		/// <summary>
		/// Synchronization session identifier.
		private readonly string _sessionId;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="sessionId">Session identifier.</param>
		public CalendarRepository(UserConnection uc, string sessionId) {
			_userConnection = uc;
			_sessionId = sessionId;
		}

		#endregion

		#region Methods: Private

		private List<Calendar> GetCalendarsFromCache(ICacheStore applicationCache) {
			object store = applicationCache[IntegrationConsts.Calendar.CacheItemName];
			if (ListenerUtils.GetIsFeatureEnabled("IsCalendarCached") && store != null) {
				return store as List<Calendar>;
			}
			return null;
		}

		private List<Calendar> ConvertEntitiesToModels(EntityCollection calendarsEntities) {
			var result = new List<Calendar>();
			var mailboxService = ClassFactory.Get<IMailboxService>(new ConstructorArgument("uc", _userConnection));
			foreach (var calendarEntity in calendarsEntities) {
				var senderEmailAddress = calendarEntity.GetTypedColumnValue<string>("SenderEmailAddress");
				var mailbox = mailboxService.GetMailboxBySenderEmailAddress(senderEmailAddress, false, true);
				var emails = mailboxService.GetEmails(mailbox.Id);
				var calendarType = GetCalendarType(calendarEntity);
				var calendar = new Calendar(calendarEntity, _sessionId, emails, calendarType);
				var calendarSettings = CreateCalendarSettings(calendarEntity, mailbox);
				var isSyncPeriodSet = calendar.SyncSinceDate != DateTime.MinValue && calendar.SyncTillDate != DateTime.MinValue;
				var isCalendarSyncEnabled = calendarEntity.GetTypedColumnValue<bool>("ImportAppointments") ||
						calendarEntity.GetTypedColumnValue<bool>("ExportActivities");
				calendarSettings.SyncEnabled = isCalendarSyncEnabled && isSyncPeriodSet &&
						!calendarEntity.GetTypedColumnValue<bool>("SynchronizationStopped") &&
						CanUseGraphCalendarSync(calendarType, mailbox);
				calendar.OldSyncEnabled = ListenerUtils.GetIsFeatureDisabled("NewMeetingIntegration", mailbox.OwnerId);
				calendar.SetOwner(calendarEntity.GetTypedColumnValue<Guid>("CreatedById"));
				calendar.SetCalendarSettings(calendarSettings);
				result.Add(calendar);
			}
			return result;
		}

		/// <summary>
		/// Returns whether calendar synchronization stays enabled for the mailbox.
		/// Graph calendars reuse mailbox-level activity-sync eligibility rules.
		/// </summary>
		/// <param name="calendarType">Calendar type.</param>
		/// <param name="mailbox"><see cref="Mailbox"/> instance.</param>
		/// <returns><c>True</c> when calendar synchronization is allowed for the mailbox.</returns>
		private static bool CanUseGraphCalendarSync(CalendarType calendarType, Mailbox mailbox) {
			return calendarType != CalendarType.Graph || mailbox.CanUseExchangeActivitySync();
		}

		private static CalendarType GetCalendarType(Entity calendarEntity) {
			var mailServerType = calendarEntity.GetTypedColumnValue<Guid>("MailServerTypeId");
			if (mailServerType == IntegrationConsts.GraphMailServerTypeId) {
				return CalendarType.Graph;
			} else if (mailServerType == IntegrationConsts.ImapMailServerTypeId) {
				return CalendarType.Google;
			}
			return CalendarType.Exchange;
		}

		private CalendarSettings CreateCalendarSettings(Entity calendarEntity, Mailbox mailbox) {
			return new CalendarSettings(calendarEntity.GetTypedColumnValue<string>("OAuthClassName")) {
				Id = mailbox.Id,
				OAuthApplicationId = calendarEntity.GetTypedColumnValue<Guid>("OAuthApplicationId"),
				Login = mailbox.Login,
				Password = mailbox.Password,
				UserId = mailbox.OwnerId,
				UserName = mailbox.OwnerUserName,
				SenderEmailAddress = mailbox.SenderEmailAddress,
				ServiceUrl = mailbox.GetServerAddress(true),
				WarningCodeId = calendarEntity.GetTypedColumnValue<Guid>("WarningCodeId"),
				IsLimitMode = calendarEntity.GetTypedColumnValue<bool>("IsLimitMode"),
				UseImpersonation = mailbox.GetUseImpersonation(),
				UseEwsOAuthScope = mailbox.GetIsEwsForGraphMailboxSync()
			};
		}

		private EntitySchemaQuery GetCalendarEsq() {
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "ActivitySyncSettings");
			esq.UseAdminRights = false;
			esq.PrimaryQueryColumn.IsAlwaysSelect = true;
			esq.AddColumn("ActivitySyncPeriod");
			esq.AddColumn("ExportActivities");
			esq.AddColumn("ImportAppointments");
			esq.AddColumn("ImportTranscripts");
			esq.AddColumn("AppointmentLastSyncDate");
			esq.AddColumn("MailboxSyncSettings.SysAdminUnit.Contact").Name = "CreatedBy";
			esq.AddColumn("MailboxSyncSettings.SenderEmailAddress").Name = "SenderEmailAddress";
			esq.AddColumn("MailboxSyncSettings.MailServer.IsLimitMode").Name = "IsLimitMode";
			esq.AddColumn("MailboxSyncSettings.MailServer.OAuthApplication").Name = "OAuthApplication";
			esq.AddColumn("MailboxSyncSettings.MailServer.OAuthApplication.ClientClassName").Name = "OAuthClassName";
			esq.AddColumn("MailboxSyncSettings.MailServer.Type").Name = "MailServerType";
			esq.AddColumn("MailboxSyncSettings.SynchronizationStopped").Name = "SynchronizationStopped";
			esq.AddColumn("MailboxSyncSettings.WarningCode").Name = "WarningCode";
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "MailboxSyncSettings.SysAdminUnit.Active", true));
			return esq;
		}

		/// <summary>
		/// Updates appointments last synchronization date.
		/// </summary>
		/// <param name="calendar"><see cref="Calendar"/> instance.</param>
		private void UpdateLastSyncDate(Calendar calendar) {
			var update = new Update(_userConnection, "ActivitySyncSettings")
				.Set("AppointmentLastSyncDate", Column.Parameter(calendar.LastSyncDateUtc))
				.Where("Id").IsEqual(Column.Parameter(calendar.Id));
			update.Execute();
		}

		private void UpdateSyncDates(Calendar calendar) {
			calendar.UpdateSyncPeriodDates(_userConnection);
			UpdateLastSyncDate(calendar);
		}

		private void UpdateCache(Calendar calendar) {
			ICacheStore applicationCache = _userConnection.ApplicationCache;
			var calendars = GetCalendarsFromCache(applicationCache);
			if (calendars != null) {
				calendars.RemoveAll(c => c.Id == calendar.Id);
				calendars.Add(calendar);
				applicationCache[IntegrationConsts.Calendar.CacheItemName] = calendars;
			}
		}

		private List<Calendar> GetGoogleCalendars() {
			var result = new List<Calendar>();
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "SocialAccount");
			esq.UseAdminRights = false;
			esq.PrimaryQueryColumn.IsAlwaysSelect = true;
			esq.AddColumn("Login");
			esq.AddColumn("User");
			esq.AddColumn("Enable");
			esq.AddColumn("MailboxSyncSettings");
			esq.AddColumn("User.Contact").Name = "CreatedBy";
			esq.AddColumn("MailboxSyncSettings.SynchronizationStopped").Name = "SynchronizationStopped";
			esq.AddColumn("MailboxSyncSettings.MailServer.OAuthApplication").Name = "OAuthApplication";
			esq.AddColumn("MailboxSyncSettings.MailServer.OAuthApplication.ClientClassName").Name = "OAuthClassName";
			esq.AddColumn("MailboxSyncSettings.MailServer.IsLimitMode").Name = "IsLimitMode";
			esq.AddColumn("MailboxSyncSettings.WarningCode").Name = "WarningCode";
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "User.Active", true));
			var socialAccountEntities = esq.GetEntityCollection(_userConnection);
			var mailboxService = ClassFactory.Get<IMailboxService>(new ConstructorArgument("uc", _userConnection));
			foreach (var calendarEntity in socialAccountEntities) {
				var mailbox = mailboxService.GetMailbox(calendarEntity.GetTypedColumnValue<Guid>("MailboxSyncSettingsId"), false);
				var emails = mailboxService.GetEmails(mailbox.Id);
				var calendar = new Calendar(calendarEntity, _sessionId, emails, CalendarType.Google);
				var calendarSettings = CreateCalendarSettings(calendarEntity, mailbox);
				calendarSettings.SyncEnabled = calendarEntity.GetTypedColumnValue<bool>("Enable") &&
						!calendarEntity.GetTypedColumnValue<bool>("SynchronizationStopped");
				calendar.SetOwner(calendarEntity.GetTypedColumnValue<Guid>("CreatedById"));
				calendar.SetCalendarSettings(calendarSettings);
				result.Add(calendar);
			}
			return result;
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Filter by enabled calendars.
		/// </summary>
		/// <param name="calendars">Collection of <see cref="Calendar"/>`s.</param>
		/// <returns>Filtered collection of <see cref="Calendar"/>`s by enabled.</returns>
		protected List<Calendar> FilterEnabledCalendars(List<Calendar> calendars) {
			return calendars.Where(c => c.Settings.SyncEnabled).ToList();
		}

		/// <summary>
		/// Getting a collection of users calendars.
		/// </summary>
		/// <param name="importRequired">Import must be enabled.</param>
		/// <param name="exportRequired">Export must be enabled.</param>
		/// <returns>Collection of <see cref="Calendar"/>`s</returns>
		protected virtual List<Calendar> GetCalendars(bool importRequired = true, bool exportRequired = true) {
			ICacheStore applicationCache = _userConnection.ApplicationCache;
			var calendars = GetCalendarsFromCache(applicationCache);
			if (calendars == null) {
				var esq = GetCalendarEsq();
				var calendarsEntities = esq.GetEntityCollection(_userConnection);
				calendars = ConvertEntitiesToModels(calendarsEntities);
				if (ListenerUtils.GetIsFeatureEnabled("GoogleMultidomain")) {
					var googleCalendars = GetGoogleCalendars();
					calendars.AddRange(googleCalendars);
				}
				if (ListenerUtils.GetIsFeatureEnabled("IsCalendarCached")) {
					applicationCache[IntegrationConsts.Calendar.CacheItemName] = calendars;
				}
			}
			if (importRequired || exportRequired) {
				calendars = FilterEnabledCalendars(calendars);
			}
			return calendars;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="ICalendarRepository.GetAllCalendars()"/>
		public List<Calendar> GetAllCalendars() {
			return GetCalendars(false, false);
		}

		/// <inheritdoc cref="ICalendarRepository.GetEnabledCalendars()"/>
		public List<Calendar> GetEnabledCalendars() {
			return GetCalendars(true, true);
		}

		/// <inheritdoc cref="ICalendarRepository.GetOwnerCalendar(string)"/>
		public Calendar GetOwnerCalendar(string email) {
			if (email.IsNullOrEmpty()) {
				return default;
			}
			var calendars = GetCalendars(true, true);
			return calendars.FirstOrDefault(c => c.IsOwnerCalendar(email));
		}

		/// <inheritdoc cref="ICalendarRepository.GetOwnerCalendars(Guid)"/>
		public List<Calendar> GetOwnerCalendars(Guid ownerId) {
			var calendars = GetAllCalendars();
			return calendars.Where(c => c.OwnerId == ownerId).ToList();
		}

		/// <inheritdoc cref="ICalendarRepository.GetOwnerCalendar(Guid)"/>
		public Calendar GetOwnerCalendar(Guid ownerId) {
			var calendars = GetCalendars(true, true);
			return calendars.FirstOrDefault(c => c.OwnerId == ownerId);
		}

		/// <inheritdoc cref="ICalendarRepository.Save(Calendar)"/>
		public void Save(Calendar calendar) {
			UpdateSyncDates(calendar);
			UpdateCache(calendar);
		}

		#endregion

	}

	#endregion

}
