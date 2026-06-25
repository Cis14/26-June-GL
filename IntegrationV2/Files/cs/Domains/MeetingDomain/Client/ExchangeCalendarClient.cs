namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Client
{
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Net.Security;
	using System.Security.Cryptography.X509Certificates;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Client.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.DTO;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using Microsoft.Exchange.WebServices.Data;
	using Terrasoft.Common;
	using Terrasoft.Configuration;
	using Terrasoft.Core.Factories;
	using IntegrationApi.Interfaces;
	using IntegrationApi.MailboxDomain.Exceptions;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Utils;
	using System.Linq;
	using Terrasoft.Common.Json;
	using Terrasoft.IntegrationV2.Utils;
	using IntegrationApi.Calendar;

	#region Class: ExchangeCalendarClient

	/// <summary>
	/// <see cref="ICalendarClient"/> implementation.
	/// </summary>
	[DefaultBinding(typeof(ICalendarClient), Name = "Exchange")]
	public class ExchangeCalendarClient: ICalendarClient
	{

		#region Constants: Private

		/// <summary>
		/// Definition of property which contains record identifier in local storage.
		/// </summary>
		private readonly ExtendedPropertyDefinition _localIdProperty = new ExtendedPropertyDefinition(
			DefaultExtendedPropertySet.PublicStrings, "LocalId", MapiPropertyType.String);

		/// <summary>
		/// Definition of property which contains record identifier in remote storage.
		/// Needed for v0 delete feature.
		/// </summary>
		private readonly ExtendedPropertyDefinition _remoteIdProperty = new ExtendedPropertyDefinition(
			DefaultExtendedPropertySet.PublicStrings, "RemoteId", MapiPropertyType.String);

		/// <summary>
		/// Definition of property which included internal apllication path.
		/// </summary>
		private readonly ExtendedPropertyDefinition _applicationPath = new ExtendedPropertyDefinition(
			DefaultExtendedPropertySet.PublicStrings, "ApplicationPath", MapiPropertyType.String);

		/// <summary>
		/// Definition of property which included internal calendar identifier.
		/// </summary>
		private readonly ExtendedPropertyDefinition _calendarId = new ExtendedPropertyDefinition(
			DefaultExtendedPropertySet.PublicStrings, "CalendarId", MapiPropertyType.String);

		/// <summary>
		/// First class property set.
		/// </summary>
		private readonly PropertySet _propertySet = new PropertySet(BasePropertySet.FirstClassProperties);

		/// <summary>
		/// Priority activity - Low.
		/// </summary>
		private readonly Guid _activityLowPriorityId = new Guid("AC96FA02-7FE6-DF11-971B-001D60E938C6");

		/// <summary>
		/// Priority activity - Medium.
		/// </summary>
		private readonly Guid _activityNormalPriorityId = new Guid("AB96FA02-7FE6-DF11-971B-001D60E938C6");

		/// <summary>
		/// Priority activity - High.
		/// </summary>
		private readonly Guid _activityHighPriorityId = new Guid("D625A9FC-7EE6-DF11-971B-001D60E938C6");

		/// <summary>
		/// Requested items limit.
		/// </summary>
		private readonly int _maxItemsPerQuery = 150;

		/// <summary>
		/// Creatio mettings synchronization category.
		/// </summary>
		private readonly string _creatioSyncCategory = "Creatio sync";

		#endregion

		#region Fields: Private

		private ExchangeService _service;

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
		public ExchangeCalendarClient() {
			_propertySet.RequestedBodyType = BodyType.HTML;
			_propertySet.Add(_localIdProperty);
			_propertySet.Add(_remoteIdProperty);
			_propertySet.Add(_calendarId);
			_propertySet.Add(_applicationPath);
		}

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="sessionId">Synchronization session identifier.</param>
		/// <param name="synchronizationErrorHelper"><see cref="ISynchronizationErrorHelper"/> implementation instance.</param>
		public ExchangeCalendarClient(string sessionId, ISynchronizationErrorHelper synchronizationErrorHelper) : this() {
			_syncErrorHelper = synchronizationErrorHelper;
			_log = ClassFactory.Get<ICalendarLogger>(new ConstructorArgument("sessionId", sessionId),
				new ConstructorArgument("modelName", GetType().Name));
		}

		#endregion

		#region Methods: Private 

		/// <summary>
		/// Creates appointment in <see cref="Calendar"/>.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> model instance.</param>
		/// <returns><see cref="Meeting"/> unique identifier in the external store.</returns>
		private IntegrationId CreateAppointment(Meeting meeting) {
			Appointment appointment = new Appointment(_service);
			appointment.SetExtendedProperty(_localIdProperty, meeting.Id.ToString().ToLower());
			appointment.Sensitivity = Sensitivity.Normal;
			appointment.LegacyFreeBusyStatus = LegacyFreeBusyStatus.Busy;
			FillAppointment(appointment, meeting);
			var sendInvitationsMode = GetSendInvitationsMode(meeting);
			SaveExchangeAppointment(appointment, sendInvitationsMode);
			_log?.LogInfo($"Meeting added to external repository with {sendInvitationsMode} mode. {meeting}");
			appointment.Load(new PropertySet(AppointmentSchema.ICalUid));
			return new IntegrationId(appointment.Id.UniqueId, appointment.ICalUid);
		}

		/// <summary>
		/// Updates appointment in <see cref="Calendar"/> with single retry of updating.
		/// There is some case when appointment is being unsuccessfully updating from different threads at the same time.
		/// Right for such cases appointment is updating ones again.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> model instance.</param>
		/// <param name="integrationId"><see cref="Meeting"/> unique identifier in the external store.</param>
		private void UpdateAppointmentWithSingleRetry(Meeting meeting, IntegrationId integrationId) {
			try {
				UpdateAppointment(meeting, integrationId);
			} catch (AggregateException ex) {
				_log?.LogWarn($"{ex.InnerException.Message}. Attempt to re-update appointment...");
				UpdateAppointment(meeting, integrationId);
			}
		}

		/// <summary>
		/// Updates appointment in <see cref="Calendar"/>.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> model instance.</param>
		/// <param name="integrationId"><see cref="Meeting"/> unique identifier in the external store.</param>
		private void UpdateAppointment(Meeting meeting, IntegrationId integrationId) {
			if (!TryBindAppointment(integrationId.RemoteId, out Appointment appointment)) {
				return;
			}
			appointment.Load(_propertySet);
			if (!appointment.TryGetProperty(_localIdProperty, out string _)) {
				appointment.SetExtendedProperty(_localIdProperty, meeting.Id.ToString());
			}
			FillAppointment(appointment, meeting);
			var sendInvitationsMode = GetSendInvitationsMode(meeting);
			SaveExchangeAppointment(appointment, sendInvitationsMode);
			_log?.LogInfo($"Meeting updated in external repository with {sendInvitationsMode} mode. {meeting}");
		}

		/// <summary>
		/// Binds and removes appointment from calendar.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		private void RemoveExchangeAppointment(Meeting meeting) {
			if (!TryBindAppointment(meeting.RemoteId, out Appointment appointment)) {
				return;
			}
			meeting.InvitesSent = GetInvitesSent(appointment, meeting.Calendar);
			var sendInvitationsMode = (SendCancellationsMode)GetSendInvitationsMode(meeting);
			appointment?.Delete(DeleteMode.MoveToDeletedItems, sendInvitationsMode);
			_log?.LogInfo($"Meeting deleted from external repository with {sendInvitationsMode} mode. {meeting}");
		}

		private void FillAppointment(Appointment appointment, Meeting meeting) {
			if (appointment.Sensitivity != Sensitivity.Private) {
				appointment.Subject = meeting.Title;
				appointment.Location = meeting.Location;
				appointment.Body = meeting.Body;
			}
			if (meeting.Calendar.Settings.IsLimitMode && !appointment.Categories.Contains(_creatioSyncCategory)) {
				appointment.Categories.Add(_creatioSyncCategory);
			}
			appointment.Start = meeting.StartDate;
			appointment.StartTimeZone = meeting.StartTimeZone;
			appointment.End = meeting.DueDate;
			appointment.EndTimeZone = meeting.EndTimeZone;
			appointment.Importance = GetExchangeImportance(meeting.PriorityId);
			appointment.IsReminderSet = meeting.RemindToOwner;
			var remindToOwnerDate = meeting.RemindToOwnerDate;
			if (remindToOwnerDate != DateTime.MinValue && remindToOwnerDate <= meeting.StartDate) {
				TimeSpan duration = meeting.StartDate - remindToOwnerDate;
				appointment.ReminderMinutesBeforeStart = Convert.ToInt32(duration.TotalMinutes);
			} else {
				appointment.ReminderMinutesBeforeStart = 0;
			}
			appointment.RequiredAttendees.Clear();
			if (meeting.IsOrganizerMeeting()) {
				foreach (var participant in meeting.Participants) {
					var emailAddress = participant.EmailAddress;
					if (emailAddress.IsNullOrEmpty()) {
						continue;
					}
					if (participant.IsRequired) {
						appointment.RequiredAttendees.Add(emailAddress);
					} else {
						appointment.OptionalAttendees.Add(emailAddress);
					}
				}
			}
			meeting.InvitesSent = GetInvitesSent(appointment, meeting.Calendar);
		}

		/// <summary>
		/// Saves an appointment on an external calendar.
		/// </summary>
		/// <param name="appointment"><see cref="Appointment"/> instance.</param>
		/// <param name="sendInvitationsMode">Send invitaions mode.</param>
		/// <returns><see cref="Appointment"/> instance.</returns>
		private void SaveExchangeAppointment(Appointment appointment, SendInvitationsMode sendInvitationsMode) {
			if (appointment.Id == null) {
				appointment.Save(sendInvitationsMode);
			} else {
				appointment.Update(ConflictResolutionMode.AlwaysOverwrite, (SendInvitationsOrCancellationsMode)sendInvitationsMode);
			}
		}

		/// <summary>
		/// Get <see cref="SendInvitationsMode"/> for saving appontment.
		/// </summary>
		/// <param name="meeting"></param>
		/// <returns><see cref="SendInvitationsMode"/> for saving appontment.</returns>
		private SendInvitationsMode GetSendInvitationsMode(Meeting meeting) {
			if (meeting.InvitesSent && meeting.IsNeedSendInvitations && ListenerUtils.GetIsFeatureEnabled("MeetingInvitation")) {
				return SendInvitationsMode.SendOnlyToAll;
			} else {
				return SendInvitationsMode.SendToNone;
			}
		}

		private bool GetInvitesSent(Appointment appointment, Calendar calendar) {
			if (appointment.Id == null) {
				return false;
			}
			var organizerEmail = GetOrganizerEmail(appointment);
			return appointment.MeetingRequestWasSent || !calendar.IsOwnerCalendar(organizerEmail);
		}

		/// <summary>
		/// Returns <see cref="Exchange.Item"/> importance.
		/// </summary>
		/// <param name="activityPriorityId"><see cref="ActivityPriority"/> instance id.</param>
		/// <returns><see cref="Exchange.Importance"/> instance.</returns>
		private Importance GetExchangeImportance(Guid activityPriorityId) {
			if (activityPriorityId == _activityHighPriorityId) {
				return Importance.High;
			} else if (activityPriorityId == _activityNormalPriorityId) {
				return Importance.Normal;
			}
			return Importance.Low;
		}

		/// <summary>
		/// Returns <paramref name="importance"/> identifier.
		/// </summary>
		/// <param name="importance">Appointment <see cref="Importance"/>.</param>
		/// <returns>Activity importance identifier.</returns>
		private Guid GetActivityPriority(Importance importance) {
			switch (importance) {
				case Importance.High:
					return _activityHighPriorityId;
				case Importance.Low:
					return _activityLowPriorityId;
				default:
					return _activityNormalPriorityId;
			}
		}

		/// <summary>
		/// Get an instance of Exchange service.
		/// </summary>
		/// <param name="settings"><see cref="CalendarSettings"/> instance.</param>
		/// <returns><see cref="ExchangeService"/> instance.</returns>
		private ExchangeService GetExchangeService(CalendarSettings settings) {
			var exchangeService = new ExchangeService(ExchangeVersion.Exchange2010_SP1, TimeZoneInfo.Utc);
			exchangeService.Url = new Uri(string.Format("https://{0}/EWS/Exchange.asmx", settings.ServiceUrl));
			if (settings.UseImpersonation) {
				exchangeService.ImpersonatedUserId = new ImpersonatedUserId(ConnectingIdType.SmtpAddress, settings.SenderEmailAddress);
			}
			if (settings.UseOAuth) {
				string token = settings.AccessToken;
				exchangeService.Credentials = new OAuthCredentials(token);
			} else {
				exchangeService.Credentials = new WebCredentials(settings.Login, settings.Password);
			}
			TestConnection(exchangeService, settings.SenderEmailAddress);
			return exchangeService;
		}

		/// <summary>
		/// Callback function to verify the server certificate.
		/// </summary>
		/// <param name="sender">An object that contains state information for this verification.</param>
		/// <param name="certificate">Certificate, used to verify the authenticity of the remote side.</param>
		/// <param name="chain">CA chain associated with the remote certificate.</param>
		/// <param name="policyErrors">One or more errors associated with the remote certificate.</param>
		/// <returns>Result command execution.</returns>
		private bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain,
				SslPolicyErrors policyErrors) {
			return true;
		}

		/// <summary>
		/// Set security protocol options.
		/// </summary>
		private void SetSecurityProtocolOptions() {
			ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
		}

		/// <summary>
		/// Reset security protocol options
		/// </summary>
		private void ResetSecurityProtocolOptions() {
			ServicePointManager.ServerCertificateValidationCallback -= ValidateRemoteCertificate;
		}

		/// <summary>
		/// Check connection.
		/// </summary>
		/// <param name="service"><see cref="ExchangeService"/> instance.</param>
		/// <param name="emailAddress">Email Address.</param>
		private void TestConnection(ExchangeService service, string emailAddress = "") {
			var id = new FolderId(WellKnownFolderName.MsgFolderRoot, emailAddress);
			service.FindFolders(id, new FolderView(1));
		}

		/// <summary>
		/// Calls <paramref name="action"/>. SSL errors will be skipped.
		/// </summary>
		/// <param name="action">Sync action.</param>
		/// <returns><c>True</c> if <paramref name="action"/> invoked without errors. Otherwise returns <c>false</c>.</returns>
		private bool InvokeInSafeContext(CalendarSettings settings, Action action) {
			SetSecurityProtocolOptions();
			try {
				_service = GetExchangeService(settings);
				action();
				return true;
			} catch (Exception e) {
				_log?.LogError($"Invoke service action is failed for {settings}.", e);
				var ex = e is AggregateException
					? e.InnerException
					: e;
				ex = settings.UseOAuth ? (Exception)new OAuthSyncException(ex) : new BasicAuthSyncException(ex);
				_syncErrorHelper?.ProcessSynchronizationError(settings.SenderEmailAddress, ex);
				return false;
			} finally {
				ResetSecurityProtocolOptions();
			}
		}

		/// <summary>
		/// Returns exchange sync folders.
		/// In order to synchronize only default calendar folder (without any custom) there is no need to find all child folders recursively.
		/// </summary>
		/// <param name="calendar"><see cref="Calendar"/> instance.</param>
		/// <returns>Exchange folders list.</returns>
		private Folder GetSyncFolder() {
			return Folder.Bind(_service, WellKnownFolderName.Calendar);
		}

		private bool TryBindAppointment(string remoteId, out Appointment appointment) {
			if (remoteId.IsNotNullOrEmpty()) {
				try {
					var item = Item.Bind(_service, new ItemId(remoteId));
					appointment = item as Appointment;
					return appointment != null;
				} catch (Exception exception) when (exception.GetBaseException() is ServiceResponseException) {
					_log?.LogError($"Failed binding remote item {remoteId}.", exception);
				}
			}
			appointment = null;
			return false;
		}

		/// <summary>
		/// Returns <see cref="InvitationState"/> for <paramref name="responseType"/> invitation state.
		/// </summary>
		/// <param name="responseType"><see cref="MeetingResponseType?"/> instance.</param>
		/// <returns><see cref="InvitationState"/> participant invitation state.</returns>
		private InvitationState ConvertToInternalInvitationState(MeetingResponseType? responseType) {
			switch (responseType) {
				case MeetingResponseType.Accept:
				case MeetingResponseType.Organizer:
					return InvitationState.Confirmed;
				case MeetingResponseType.Decline:
					return InvitationState.Declined;
				case MeetingResponseType.Unknown:
				case MeetingResponseType.Tentative:
					return InvitationState.InDoubt;
				default:
					return InvitationState.Empty;
			}
		}

		/// <summary>
		/// Add a participant to the meeting DTO.
		/// </summary>
		/// <param name="meeting"><see cref="MeetingDto"/> instance.</param>
		/// <param name="email">Participant email.</param>
		/// <param name="response"><see cref="MeetingResponseType"/> instance.</param>
		private void AddParticipantToMeetingDto(MeetingDto meetingDto, string email, MeetingResponseType? response = null) {
			if (email.IsNullOrEmpty() || !email.Contains("@")) {
				return;
			}
			email = email.ExtractEmailAddress().ToLower();
			if (!meetingDto.Participants.ContainsKey(email) && email.IsNotNullOrEmpty()) {
				meetingDto.Participants.Add(email, ConvertToInternalInvitationState(response));
			}
		}

		/// <summary>
		/// Updates calendar owner participant state using "MyResponseType" property value.
		/// </summary>
		/// <param name="meetingDto"><see cref="MeetingDto"/> instance.</param>
		/// <param name="calendar"><see cref="Calendar"/> instance.</param>
		/// <param name="response"><see cref="MeetingResponseType"/> instance.</param>
		/// <remarks>In some cases EWS API returns current exchange user state in MyResponseType property, and does not have
		/// user invite response in participant. See "RND-78960" for details.</remarks>
		private void ProcessCurrentCalendarResponse(MeetingDto meetingDto, Calendar calendar, MeetingResponseType? response = null) {
			if (ListenerUtils.GetIsFeatureDisabled("ProcessMeetingMyResponseType") || response == null) {
				return;
			}
			var inviteState = ConvertToInternalInvitationState(response);
			if (inviteState == InvitationState.InDoubt || inviteState == InvitationState.Empty) {
				return;
			}
			foreach (var participant in meetingDto.Participants.Where(p => calendar.IsOwnerCalendar(p.Key)).ToList()) {
				if (participant.Value != inviteState) {
					meetingDto.Participants[participant.Key] = inviteState;
				}
			}
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> instance converted to UTC timezone.
		/// </summary>
		/// <param name="date"><see cref="DateTime"/> instance.</param>
		/// <returns><see cref="DateTime"/> instance converted to UTC timezone.</returns>
		private DateTime GetUtcDate(DateTime date) {
			return date.Kind == DateTimeKind.Utc
				? date
				: date.ToUniversalTime();
		}

		/// <summary>
		/// Creates <see cref="MeetingDto"/> instance and fills it with data from <paramref name="appointment"/>.
		/// </summary>
		/// <param name="appointment"><see cref="Appointment"/> instance.</param>
		/// <param name="calendar"><see cref="Calendar"/> instance.</param>
		/// <param name="meeting"><see cref="MeetingDto"/> instance.</param>
		/// <returns>Return <c>true</c>, if successfully converted <paramref name="appointment"/> 
		/// to <paramref name="meeting"/>, otherwise <c>false</c>.</returns>
		private bool TryConvertToMeetingDto(Appointment appointment, Calendar calendar, out MeetingDto meeting) {
			try {
				appointment.Load(_propertySet);
				var organizerEmail = GetOrganizerEmail(appointment);
				if (organizerEmail.IsNullOrEmpty()) {
					meeting = null;
					var organizer = Json.Serialize(appointment.Organizer);
					_log.LogWarn($"Metting organizer without address '{organizer}, Subject '{appointment.Subject}'");
					return false;
				}
				if (appointment.ICalUid.IsNullOrEmpty()) {
					meeting = null;
					_log.LogWarn($"Metting  without ICalUid '{appointment.ICalUid}, Subject '{appointment.Subject}' {appointment.Start} {appointment.End}");
					return false;
				}
				var startDate = GetUtcDate(appointment.SafeGetValue<DateTime>(AppointmentSchema.Start));
				var dueDate = GetUtcDate(appointment.SafeGetValue<DateTime>(AppointmentSchema.End));
				var remindToOwner = appointment.SafeGetValue<bool>(ItemSchema.IsReminderSet);
				var iCalUid = GetICalUid(appointment);
				meeting = new MeetingDto() {
					Title = appointment.Subject,
					StartDateUtc = startDate,
					DueDateUtc = dueDate,
					Location = appointment.Location ?? string.Empty,
					Body = appointment.Body.Text,
					PriorityId = GetActivityPriority(appointment.Importance),
					InvitesSent = GetInvitesSent(appointment, calendar),
					RemindToOwner = remindToOwner,
					RemindToOwnerDateUtc = remindToOwner ? startDate.AddMinutes(-appointment.ReminderMinutesBeforeStart) : DateTime.MinValue,
					OrganizerEmail = organizerEmail,
					RemoteId = appointment.Id.UniqueId,
					ICalUid = iCalUid,
					IsPrivate = appointment.Sensitivity == Sensitivity.Private,
					RemoteCreatedOn = appointment.DateTimeCreated,
					IsRecurent = appointment.IsRecurrent(),
					IsCancelled = appointment.IsCancelled
				};
				AddParticipantToMeetingDto(meeting, organizerEmail, MeetingResponseType.Organizer);
				foreach (var requiredAttende in appointment.RequiredAttendees) {
					AddParticipantToMeetingDto(meeting, requiredAttende.Address, requiredAttende.ResponseType);
				}
				foreach (var optionalAttende in appointment.OptionalAttendees) {
					AddParticipantToMeetingDto(meeting, optionalAttende.Address, optionalAttende.ResponseType);
				}
				ProcessCurrentCalendarResponse(meeting, calendar, appointment.MyResponseType);
				return true;
			} catch(Exception e) {
				_log.LogError($"Convert raw meeting to model meeting failed", e);
				meeting = null;
				return false;
			}
		}

		private string GetOrganizerEmail(Appointment appointment) {
			var organizer = appointment.Organizer;
			return organizer == null || organizer.Address.IsNullOrEmpty()
				? string.Empty
				: appointment.Organizer.Address.ExtractEmailAddress();
		}

		private void SetExtendedProperties(Appointment appointment, Calendar calendar, MeetingDto meetingDto) {
			var organizerEmail = GetOrganizerEmail(appointment);
			if (!calendar.IsOwnerCalendar(organizerEmail)) {
				return;
			}
			bool isSetExtendedProperty = SetRemoteIdExtendedProperty(appointment);
			isSetExtendedProperty |= SetCalendarIdExtendedProperty(appointment, calendar, meetingDto);
			if (isSetExtendedProperty) {
				SaveExchangeAppointment(appointment, SendInvitationsMode.SendToNone);
			}
		}

		private bool SetCalendarIdExtendedProperty(Appointment appointment, Calendar calendar, MeetingDto meetingDto) {
			var currentMailboxId = calendar.Id.ToString().ToLower();
			bool isSetExtendedProperty = false;
			if (!appointment.TryGetProperty(_calendarId, out string mailboxId)) {
				appointment.SetExtendedProperty(_calendarId, currentMailboxId);
				isSetExtendedProperty = true;
			} else if (mailboxId != currentMailboxId) {
				if(appointment.TryGetProperty(_applicationPath, out string applicationPath)) {
					_log.LogWarn($"Synchronization of meetings for this calendar account is enabled in another application." +
							$" Another application path '{applicationPath}'.");
				}
				appointment.SetExtendedProperty(_calendarId, currentMailboxId);
				isSetExtendedProperty = true;
				meetingDto.IsChangedFromAnotherApp = true;
			}
			return isSetExtendedProperty;
		}

		private bool SetRemoteIdExtendedProperty(Appointment appointment) {
			if (!appointment.TryGetProperty(_remoteIdProperty, out string _)) {
				var iCalUid = GetICalUid(appointment);
				appointment.SetExtendedProperty(_remoteIdProperty, iCalUid);
				return true;
			}
			return false;
		}

		private string GetICalUid(Appointment appointment) {
			return appointment.IsRecurring && appointment.ICalRecurrenceId != null
				? GetICalUid(appointment.ICalUid, appointment.ICalRecurrenceId.Value)
				: appointment.ICalUid;
		}

		private string GetICalUid(string masterICalUid, DateTime occurrenceStart) {
			return masterICalUid + occurrenceStart.ToString("_yyyy_MM_dd");
		}

		/// <summary>
		/// Gets filters for Exchange data query.
		/// </summary>
		/// <returns>Filter instance.</returns>
		private SearchFilter GetSearchFilters(Calendar calendar) {
			DateTime lastSyncDateUtc = calendar.LastSyncDateUtc;
			if (lastSyncDateUtc == DateTime.MinValue) {
				return null;
			}
			return new SearchFilter.IsGreaterThan(ItemSchema.LastModifiedTime, lastSyncDateUtc.ToLocalTime());
		}

		private List<MeetingDto> GetChangedAppointments(Calendar calendar) {
			var changedItemsFilter = GetSearchFilters(calendar);
			if (calendar.LastSyncDateUtc.Date < DateTime.UtcNow.Date || changedItemsFilter == null) {
				return GetAllAppointments(calendar);
			}
			var result = new List<MeetingDto>();
			bool hasRecurenceWithRules = false;
			var syncFolder = GetSyncFolder();
			foreach (var item in GetAppointmentsFromFolder(syncFolder, changedItemsFilter, calendar)) {
				if (TryBindAppointment(item.Id.UniqueId, out Appointment appointment)) {
					LoadMeeting(calendar, appointment, result);
					hasRecurenceWithRules |= appointment.IsRecurrent();
				}
				if (hasRecurenceWithRules) {
					break;
				}
			}
			return hasRecurenceWithRules
				? GetAllAppointments(calendar)
				: result;
		}

		private List<MeetingDto> GetDeletedAppointments(Calendar calendar) {
			var result = new List<MeetingDto>();
			var changedItemsFilter = GetSearchFilters(calendar);
			if (changedItemsFilter == null) {
				return result;
			}
			var folder = Folder.Bind(_service, WellKnownFolderName.DeletedItems);
			foreach (var item in GetAppointmentsFromFolder(folder, changedItemsFilter, calendar)) {
				if (TryBindAppointment(item.Id.UniqueId, out Appointment appointment) 
						&& TryConvertToMeetingDto(appointment, calendar, out var meetingDto)) {
					result.Add(meetingDto);
					_log.LogDebug($"Added deleting meeting '{meetingDto.Title}' with ICalUid {meetingDto.ICalUid}");
				}
			}
			return result;
		}

		private IEnumerable<Item> GetAppointmentsFromFolder(Folder folder, SearchFilter filter, Calendar calendar) {
			var itemView = new ItemView(_maxItemsPerQuery);
			FindItemsResults<Item> itemCollection;
			do {
				itemCollection = folder.FindItems(filter, itemView);
				itemView.Offset = itemCollection.NextPageOffset ?? 0;
				foreach (var item in itemCollection) {
					if(IsSkipItemFromImport(calendar, item)) {
						continue;
					}
					yield return item;
				}
			} while (itemCollection.MoreAvailable);
		}

		private List<MeetingDto> GetAllAppointments(Calendar calendar) {
			var result = new List<MeetingDto>();
			var startDate = calendar.SyncSinceDate;
			var endDate = calendar.SyncTillDate;
			var currentStartDate = startDate;
			var syncFolder = GetSyncFolder();
			_log?.LogDebug($"Load meetings from folder '{syncFolder.DisplayName} {syncFolder.Id}'.");
			while (currentStartDate < endDate) {
				var calendarItemView = new CalendarView(currentStartDate, currentStartDate.AddDays(1), _maxItemsPerQuery);
				_log?.LogDebug($"Request all meetings from period '{calendarItemView.StartDate}' - '{calendarItemView.EndDate}'.");
				foreach (var appointment in _service.FindAppointments(syncFolder.Id, calendarItemView)) {
					if(IsSkipItemFromImport(calendar, appointment)) {
						continue;
					}
					LoadMeeting(calendar, appointment, result);
				}
				_log?.LogDebug($"Meetings from period '{calendarItemView.StartDate}' - '{calendarItemView.EndDate}' loaded.");
				currentStartDate = currentStartDate.AddDays(1);
			}
			_log?.LogDebug($"Load meetings from folder '{syncFolder.DisplayName}' ended '{syncFolder.Id}'.");
			return result;
		}

		private void LoadMeeting(Calendar calendar, Appointment item, List<MeetingDto> result) {
			if (TryConvertToMeetingDto(item, calendar, out var meetingDto)) {
				SetExtendedProperties(item, calendar, meetingDto);
				_log?.LogDebug($"Meeting loaded by import. {meetingDto} {calendar}.");
				result.Add(meetingDto);
			} else {
				_log.LogWarn($"Skip import meeting from calendar {calendar}. External meeting '{item.Subject}', " +
					$"start '{GetUtcDate(item.SafeGetValue<DateTime>(AppointmentSchema.Start))}', " +
					$"due '{GetUtcDate(item.SafeGetValue<DateTime>(AppointmentSchema.End))}'");
			}
		}

		private async System.Threading.Tasks.Task ConvertToTeamsMeeting(Meeting meeting, Calendar calendar) {
			try {
				_log?.LogDebug($"ConvertToTeamsMeeting started for '{meeting}' in calendar '{calendar}'.");
				if ((calendar.Type != CalendarType.Exchange && calendar.Type != CalendarType.Graph) ||
						!calendar.Settings.UseOAuth) {
					_log?.LogWarn($"Calendar {calendar} is not added by OAuth or not Exchange/Graph calendar. " +
						$"Skip converting meeting {meeting} to teams meeting. ");
					return;
				}
				var graphApiClient = new GraphApiServiceAccountClient(calendar.Settings.ServiceAccountToken,calendar.Settings.Login, _log);
				var result = await graphApiClient.ConvertToTeamsMeeting(meeting.RemoteId);
				_log?.LogDebug($"ConvertToTeamsMeeting ended for '{meeting}' in calendar '{calendar}'. Result {result}");
			} catch(Exception ex) {
				_log?.LogError($"Error converting meeting {meeting} to teams meeting in calendar {calendar}.", ex);
			}
		}

		private bool IsSkipItemFromImport(Calendar calendar, Item item) {
			if (calendar.Settings.IsLimitMode && !item.Categories.Contains(_creatioSyncCategory)) {
				_log.LogDebug($"Skip meeting '{item.Subject}' sync without category '{_creatioSyncCategory}'.");
				return true;
			}
			return false;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="ICalendarClient.SaveMeeting(Meeting, Calendar)"/>
		public IntegrationId SaveMeeting(Meeting meeting, Calendar calendar) {
			if (!calendar.Settings.SyncEnabled) {
				_log?.LogInfo($"Export disabled when save meeting. {meeting} {calendar}.");
				return null;
			}
			var integrationId = new IntegrationId(meeting.RemoteId, meeting.ICalUid);
			var success = InvokeInSafeContext(calendar.Settings, () => {
				if (string.IsNullOrEmpty(integrationId.RemoteId) || meeting.NeedInitialExport(calendar)) {
					_log?.LogInfo($"Export meeting to external repository. {meeting} {calendar}");
					integrationId = CreateAppointment(meeting);
					_log?.LogInfo($"Meeting successfully exported to external repository. {meeting} {calendar}");
				} else {
					_log?.LogInfo($"Updating meeting in external repository. {meeting} {calendar}.");
					UpdateAppointmentWithSingleRetry(meeting, integrationId);
					_log?.LogInfo($"Meeting updated in external repository. {meeting} {calendar}.");
				}
			});
			return success ? integrationId : null;
		}

		/// <inheritdoc cref="ICalendarClient.RemoveMeeting(Meeting, Calendar)"/>
		public void RemoveMeeting(Meeting meeting, Calendar calendar) {
			if (!calendar.Settings.SyncEnabled) {
				_log?.LogWarn($"Export disabled when deleting meeting {meeting} {calendar}.");
				return;
			}
			InvokeInSafeContext(calendar.Settings, () => {
				_log?.LogInfo($"Delete meeting {meeting} {calendar}.");
				RemoveExchangeAppointment(meeting);
				_log?.LogInfo($"Meeting deleted from external repository. {meeting} {calendar}.");
			});
		}

		/// <inheritdoc cref="ICalendarClient.SendInvitations(Meeting, Calendar, bool)"/>
		public void SendInvitations(Meeting meeting, Calendar calendar, bool isOnlineMeeting) {
			if (string.IsNullOrEmpty(meeting.RemoteId) || !calendar.Settings.SyncEnabled) {
				return;
			}
			meeting.InvitesSent = true;
			SaveMeeting(meeting, calendar);
			if (isOnlineMeeting) {
				ConvertToTeamsMeeting(meeting, calendar).Wait();
			}
		}

		/// <inheritdoc cref="ICalendarClient.GetSyncPeriodMeetings(Calendar, bool)"/>
		public List<MeetingDto> GetSyncPeriodMeetings(Calendar calendar, out bool isAllMeetingsLoaded) {
			var result = new List<MeetingDto>();
			if (!calendar.Settings.SyncEnabled) {
				_log?.LogWarn($"Import disabled when sync meetings. {calendar}.");
				isAllMeetingsLoaded = true;
				return result;
			}
			isAllMeetingsLoaded = InvokeInSafeContext(calendar.Settings, () => {
				result.AddRange(GetChangedAppointments(calendar));
			});
			_log?.LogDebug($"External repository loaded {result.Count} meetings for import.");
			return result;
		}

		/// <inheritdoc cref="ICalendarClient.GetDeletedMeetings(Calendar)"/>
		public List<MeetingDto> GetDeletedMeetings(Calendar calendar) {
			var result = new List<MeetingDto>();
			if (!calendar.Settings.SyncEnabled) {
				return result;
			}
			InvokeInSafeContext(calendar.Settings, () => {
				result.AddRange(GetDeletedAppointments(calendar));
			});
			_log?.LogDebug($"External repository loaded {result.Count} deleted meetings for import.");
			return result;
		}

		/// <inheritdoc cref="ICalendarClient.SetExtendedProperty(Meeting, Calendar, string, object)"/>
		public void SetExtendedProperty(Meeting meeting, Calendar calendar, string extendedPropetyName, object  extendedPropetyValue) {
			if (!calendar.Settings.SyncEnabled || extendedPropetyValue == null) {
				_log?.LogWarn($"Set extended property is skeeped {meeting} {calendar}.");
				return;
			}
			InvokeInSafeContext(calendar.Settings, () => {
				var extendedProperty = (ExtendedPropertyDefinition)_propertySet.FirstOrDefault(ps 
					=> ((ExtendedPropertyDefinition)ps).Name == extendedPropetyName);
				if(extendedProperty == null) {
					extendedProperty = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings,
						extendedPropetyName, MapiPropertyType.String);
				}
				if(TryBindAppointment(meeting.RemoteId, out var appointment)) {
					appointment.Load(_propertySet);
					appointment.SetExtendedProperty(extendedProperty, extendedPropetyValue.ToString());
					SaveExchangeAppointment(appointment, SendInvitationsMode.SendToNone);
				}
			});
		}

		#endregion

	}

	#endregion

}
