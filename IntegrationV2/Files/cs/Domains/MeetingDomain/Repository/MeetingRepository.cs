namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Repository 
{
	using Creatio.FeatureToggling;
	using IntegrationApi.Calendar;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Client;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Utils;
	using IntegrationV2.Files.cs.Utils;
	using Newtonsoft.Json.Linq;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.EmailDomain;
	using Terrasoft.Sync;

	#region Class: MeetingRepository

	/// <summary>
	/// <see cref="IMeetingRepository"/> implementation.
	/// </summary>
	[DefaultBinding(typeof(IMeetingRepository))]
	public class MeetingRepository : IMeetingRepository 
	{

		#region Fields: Private

		/// <summary>
		/// <see cref="UserConnection"/> instance.
		/// </summary>
		private readonly UserConnection _uc;

		/// <summary>
		/// <see cref="ICalendarLogger"/> instance.
		/// </summary
		private readonly ICalendarLogger _log;

		/// <summary>
		/// <see cref="IParticipantRepository"/> implementation instance.
		/// </summary>
		private readonly IParticipantRepository _participantRepository;

		/// <summary>
		/// Activity not started status identifier.
		/// </summary>
		private readonly Guid _notStartedStatusId = new Guid("384D4B84-58E6-DF11-971B-001D60E938C6");

		/// <summary>
		/// Activity completed status identifier.
		/// </summary>
		private readonly Guid _completedStatusId = new Guid("4BDBB88F-58E6-DF11-971B-001D60E938C6");

		/// <summary>
		/// Activity canceled status identifier.
		/// </summary>
		private readonly Guid _canceledStatusId = new Guid("201CFBA8-58E6-DF11-971B-001D60E938C6");

		/// <summary>
		/// <see cref="IRecordOperationsNotificator"/> implementation instance.
		/// </summary>
		private readonly IRecordOperationsNotificator _recordOperationsNotificator;

		/// <summary>
		/// <see cref="ICalendarRepository"/> implementation instance.
		/// </summary>
		private readonly ICalendarRepository _calendarRepository;

		private readonly IEnumerable<string> _fetchColumns = new[] { "Title", "StartDate", "DueDate", "Location",
			"Notes", "Priority", "RemindToOwner", "RemindToOwnerDate", "ModifiedOn", "Status", "Organizer", "Type",
			"ShowInScheduler", "Id" };

		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="calendarRepository"><see cref="ICalendarRepository"/> instance.</param>
		/// <param name="sessionId">Session identifier.</param>
		public MeetingRepository(UserConnection uc, ICalendarRepository calendarRepository, string sessionId = null) {
			_uc = uc;
			_calendarRepository = calendarRepository ?? ClassFactory.Get<ICalendarRepository>("CalendarInMemoryRepository",
					new ConstructorArgument("uc", _uc),
					new ConstructorArgument("sessionId", _log.SessionId));
			_log = ClassFactory.Get<ICalendarLogger>(new ConstructorArgument("sessionId", sessionId),
				new ConstructorArgument("modelName", GetType().Name));
			_participantRepository = ClassFactory.Get<IParticipantRepository>(new ConstructorArgument("uc", uc),
				new ConstructorArgument("sessionId", _log?.SessionId),
				new ConstructorArgument("calendarRepository", calendarRepository));
			_recordOperationsNotificator = ClassFactory.Get<IRecordOperationsNotificator>(
				new ConstructorArgument("userConnection", uc));
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Gets list of <see cref="Meeting"/> models by <see cref="Entity.PrimaryColumnValue"/>.
		/// </summary>
		/// <param name="meetingEntity">Meeting <see cref="Entity"/>.</param>
		/// <returns>List of <see cref="Meeting"/> models by <see cref="Entity.PrimaryColumnValue"/>.</returns>
		private List<Meeting> GetMeetingsInternal(Entity meetingEntity) {
			var participants = _participantRepository.GetMeetingParticipants(meetingEntity.PrimaryColumnValue);
			return GetMeetingsInternal(meetingEntity, participants);
		}

		/// <summary>
		/// Gets list of <see cref="Meeting"/> models by <see cref="Entity.PrimaryColumnValue"/>.
		/// </summary>
		/// <param name="meetingEntity">Meeting <see cref="Entity"/>.</param>
		/// <returns>List of <see cref="Meeting"/> models by <see cref="Entity.PrimaryColumnValue"/>.</returns>
		private List<Meeting> GetMeetingsInternal(Entity meetingEntity, List<Participant> participants) {
			var mettings = new List<Meeting>();
			var metadataList = GetMeetingMetadata(meetingEntity.PrimaryColumnValue);
			foreach (var participant in participants) {
				var meeting = new Meeting(meetingEntity, _log?.SessionId);
				AddParticipantsToMeeting(participants, participant, meeting);
				var metadata = metadataList.FirstOrDefault(m => m.GetTypedColumnValue<Guid>("CreatedById") == participant.ContactId);
				FillCalendarProperties(meeting, participant, metadata);
				mettings.Add(meeting);
			}
			UpdateInvitesSent(mettings);
			return mettings;
		}

		private void UpdateInvitesSent(List<Meeting> meetings) {
			if (meetings.Any(m => m.InvitesSent)) {
				foreach (var meeting in meetings) {
					meeting.InvitesSent = true;
				}
			}
		}

		/// </summary>
		/// Add participants to <paramref name="meeting"/>.
		/// </summary>
		/// <param name="participants">List of <see cref="Meeting"/> instances.</param>
		/// <param name="participant"><see cref="Participant"/> instance.</param>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		private void AddParticipantsToMeeting(List<Participant> participants, Participant participant, Meeting meeting) {
			if (meeting.OrganizerId == participant.ContactId) {
				meeting.AddParticipants(participants);
			}
			else {
				meeting.AddParticipant(participant);
			}
		}

		/// <summary>
		/// Fill <see cref="Meeting"/> calendar properties.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		/// <param name="participant"><see cref="Participant"/> instance.</param>
		private void FillCalendarProperties(Meeting meeting, Participant participant, Entity metadata = null) {
			FillCalendar(meeting, participant);
			if (metadata != null || TryFetchMetadata(meeting, out metadata)) {
				SetExternalStoreProperties(meeting, metadata);
			}
		}

		/// <summary>
		/// Set integration identifiers to meeting.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		/// <param name="metaData">Meeting metadata <see cref="Entity"/> instance.</param>
		private void SetExternalStoreProperties(Meeting meeting, Entity metaData) {
			var schemaName = metaData.SchemaName;
			var extraParameters = metaData.GetTypedColumnValue<string>("ExtraParameters");
			var icalUid = schemaName == "ActivityCorrespondence"
					? GetExtraparametersPropertyValue<string>(extraParameters, "ICalUId")
					: metaData.GetTypedColumnValue<string>("RemoteId");
			var remoteId = schemaName == "ActivityCorrespondence"
					? metaData.GetTypedColumnValue<string>("RemoteId")
					: GetExtraparametersPropertyValue<string>(extraParameters, "RemoteId");
			meeting.SetIntegrationsId(new IntegrationId(remoteId, icalUid));
			meeting.SetHash(GetExtraparametersPropertyValue<string>(extraParameters, "ActivityHashV2"));
			meeting.SetNumberOfParticipants(GetExtraparametersPropertyValue<int>(extraParameters, "NumberOfParticipants"));
			meeting.InvitesSent = GetExtraparametersPropertyValue<bool>(extraParameters, "InvitesSent");
			meeting.RemoteItemInvitesSent = meeting.InvitesSent;
			meeting.SourceCalendarName = GetExtraparametersPropertyValue<string>(extraParameters, "SourceCalendarName");
			if (meeting.StartDate == DateTime.MinValue) {
				meeting.StartDate = DateTimeUtils.GetUserDateTime(GetExtraparametersPropertyValue<DateTime>(extraParameters, "StartDate"),
					_uc.CurrentUser.TimeZone);
			}
			meeting.State = (SyncState)metaData.GetTypedColumnValue<int>("RemoteState");
		}

		/// <summary>
		/// Fill <see cref="Meeting"/> calendar related properties.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		/// <param name="participant"><see cref="Participant"/> instance.</param>
		private void FillCalendar(Meeting meeting, Participant participant) {
			var participantCalendar = _calendarRepository.GetOwnerCalendar(participant.ContactId);
			meeting.Calendar = participantCalendar;
		}

		/// <summary>
		/// Get extra parameters <paramref name="propertyName"/> value.
		/// </summary>
		/// <param name="rawExtraParameters">Extra parameters string.</param>
		/// <param name="propertyName">Property name.</param>
		/// <returns><typeparamref name="T"/> extra parameter value.</returns>
		private T GetExtraparametersPropertyValue<T>(string rawExtraParameters, string propertyName) {
			if (string.IsNullOrEmpty(rawExtraParameters)) {
				return default;
			}
			JObject extraParams = JObject.Parse(rawExtraParameters);
			if (!extraParams.ContainsKey(propertyName)) {
				return default;
			}
			switch (typeof(T).Name) {
				case "Guid":
					return (T)(object)Guid.Parse(extraParams[propertyName].ToString());
				case "DateTime":
					return (T)(object)DateTime.Parse(extraParams[propertyName].ToString());
				case "Boolean":
					return (T)(object)bool.Parse(extraParams[propertyName].ToString());
				case "Int32":
					return (T)(object)int.Parse(extraParams[propertyName].ToString());
				default:
					return (T)(object)extraParams[propertyName].ToString();
			}
		}

		/// <summary>
		/// Fetch meeting <see cref="Entity"/> instance.
		/// </summary>
		/// <param name="meetingId">Meeting unique identifier.</param>
		/// <param name="meetingEntity">Meeting <see cref="Entity"/> instance.</param>
		/// <returns><c>True</c> if activity fetched, <c>false</c> otherwise.</returns>
		private bool TryFetchMeetingEntity(Guid meetingId, out Entity meetingEntity) {
			var schema = _uc.EntitySchemaManager.GetInstanceByName("Activity");
			meetingEntity = schema.CreateEntity(_uc);
			meetingEntity.PrimaryColumnValue = meetingId;
			if (!meetingEntity.FetchFromDB(_fetchColumns, false)) {
				_log?.LogWarn($"Insufficient permissions to read Activity '{meetingId}', skip processing.");
				return false;
			}
			var activityTypeId = meetingEntity.GetTypedColumnValue<Guid>("TypeId");
			if (activityTypeId == IntegrationConsts.EmailTypeId) {
				_log?.LogDebug($"The meeting '{meetingId}' has incorect email type, skip processing.");
				meetingEntity = null;
				return false;
			}
			var showInCalendar = meetingEntity.GetTypedColumnValue<bool>("ShowInScheduler");
			if (!showInCalendar) {
				_log?.LogDebug($"Meeting '{meetingId}' is hiden in the system, skip processing.");
				meetingEntity = null;
				return false;
			}
			return true;
		}

		/// <summary>
		/// Gets internal meetings for calendar.
		/// </summary>
		/// <param name="contactId">User contact identifier.</param>
		/// <param name="sinceDate">Since date.</param>
		/// <param name="dueDate">Due date.</param>
		/// <param name="createdBefore">Created before filter value.</param>
		/// <returns>Meeting <see cref="Entity"/> instances for <paramref name="calendar"/></returns>
		private EntityCollection InternalGetCalendarMeetings(Guid contactId, DateTime sinceDate, DateTime dueDate, DateTime createdBefore = default) {
			var esq = GetActivityEsq();
			esq.Columns.GetByName("StartDate").OrderByAsc();
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "[ActivityParticipant:Activity:Id].Participant.Id",
				contactId));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "ShowInScheduler", true));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.NotEqual, "Type", IntegrationConsts.EmailTypeId));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.GreaterOrEqual, "StartDate", sinceDate));
			if (dueDate != default) {
				esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.LessOrEqual, "StartDate", dueDate));
			}
			if (createdBefore != default) {
				esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Less, "CreatedOn", createdBefore));
			}
			return esq.GetEntityCollection(_uc);
		}



		/// <summary>
		/// Fetch meeting <see cref="Entity"/> identifiers from metadata.
		/// </summary>
		/// <param name="iCalUid">External meeting unique identifier.</param>
		/// <returns>Meeting <see cref="Entity"/> identifiers.</returns>
		private IEnumerable<object> GetActivityIdsFromMetadata(string iCalUid) {
			var esq = new EntitySchemaQuery(_uc.EntitySchemaManager, "SysSyncMetaData");
			esq.UseAdminRights = false;
			esq.AddColumn("LocalId");
			var iCalUidFilters = new EntitySchemaQueryFilterCollection(esq, LogicalOperationStrict.Or) {
				esq.CreateFilterWithParameters(FilterComparisonType.Equal, "RemoteId", iCalUid)
			};
			if (ICalUIdHelper.TryGetSimplifiedICalUid(iCalUid, out var simplifiedICalUid)) {
				iCalUidFilters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal,
					"RemoteId", simplifiedICalUid));
			}
			esq.Filters.Add(iCalUidFilters);
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "SyncSchemaName", "Activity"));
			return esq.GetEntityCollection(_uc)
				.Select(m => {
					var id = m.GetTypedColumnValue<Guid>("LocalId");
					return id.IsNotEmpty() ? (object)id : null;
				})
				.Where(id => id != null).Distinct();
		}

		/// <summary>
		/// Fetch meeting <see cref="Entity"/> instance.
		/// </summary>
		/// <param name="iCalUid">External meeting unique identifier.</param>
		/// <returns>Meeting <see cref="Entity"/> instance.</returns>
		private Entity FetchMeetingEntity(string iCalUid) {
			var activityIds = GetActivityIdsFromMetadata(iCalUid);
			if (!activityIds.Any()) {
				return null;
			}
			var esq = GetActivityEsq();
			esq.UseAdminRights = false;
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Id", activityIds.ToArray()));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.NotEqual,
				"Type", IntegrationConsts.EmailTypeId));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				"ShowInScheduler", true));
			var activity = esq.GetEntityCollection(_uc).OrderByDescending(entity => entity.GetTypedColumnValue<DateTime>("StartDate")).LastOrDefault();
			if (activity == null && Features.GetIsEnabled("GoogleMultidomain")) {
				var googleActivitiesEsq = GetActivityEsq();
				googleActivitiesEsq.UseAdminRights = false;
				googleActivitiesEsq.Filters.Add(googleActivitiesEsq.CreateFilterWithParameters(FilterComparisonType.NotEqual,
					"Type", IntegrationConsts.EmailTypeId));
				googleActivitiesEsq.Filters.Add(googleActivitiesEsq.CreateFilterWithParameters(FilterComparisonType.Equal,
					"ShowInScheduler", true));
				googleActivitiesEsq.Filters.Add(googleActivitiesEsq.CreateFilterWithParameters(FilterComparisonType.Contain,
					"[ActivityCorrespondence:Activity:Id].ExtraParameters", iCalUid));
				activity = googleActivitiesEsq.GetEntityCollection(_uc).OrderByDescending(entity => entity.GetTypedColumnValue<DateTime>("StartDate")).LastOrDefault();
			}
			return activity;
		}

		private EntitySchemaQuery GetMetadataEsq() {
			var esq = new EntitySchemaQuery(_uc.EntitySchemaManager, "SysSyncMetaData");
			esq.UseAdminRights = false;
			esq.PrimaryQueryColumn.IsAlwaysSelect = true;
			esq.AddColumn("CreatedBy");
			esq.AddColumn("RemoteId");
			esq.AddColumn("LocalId");
			esq.AddColumn("ExtraParameters");
			esq.AddColumn("RemoteState");
			esq.AddColumn("LocalState");
			esq.AddColumn("SyncSchemaName");
			esq.AddColumn("RemoteItemName");
			esq.AddColumn("SchemaOrder");
			esq.AddColumn("CreatedInStoreId");
			esq.AddColumn("ModifiedInStoreId");
			esq.AddColumn("RemoteStoreId");
			esq.AddColumn("Version");
			return esq;
		}

		private EntitySchemaQuery GetActivityCorrespondenceEsq() {
			var esq = new EntitySchemaQuery(_uc.EntitySchemaManager, "ActivityCorrespondence");
			esq.UseAdminRights = false;
			esq.PrimaryQueryColumn.IsAlwaysSelect = true;
			esq.AddColumn("CreatedBy");
			esq.AddColumn("ExtraParameters");
			esq.AddColumn("SourceActivityId").Name = "RemoteId";
			esq.AddColumn("Activity.Id").Name = "LocalId";
			esq.AddColumn(0, _uc.DataValueTypeManager.GetInstanceByName("Integer")).Name = "RemoteState";
			esq.AddColumn(0, _uc.DataValueTypeManager.GetInstanceByName("Integer")).Name = "LocalState";
			esq.AddColumn("Activity", _uc.DataValueTypeManager.GetInstanceByName("ShortText")).Name = "SyncSchemaName";
			esq.AddColumn("GoogleEvent", _uc.DataValueTypeManager.GetInstanceByName("ShortText")).Name = "RemoteItemName";
			esq.AddColumn(0, _uc.DataValueTypeManager.GetInstanceByName("Integer")).Name = "SchemaOrder";
			esq.AddColumn(Guid.Empty, _uc.DataValueTypeManager.GetInstanceByName("Guid")).Name = "CreatedInStoreId";
			esq.AddColumn(Guid.Empty, _uc.DataValueTypeManager.GetInstanceByName("Guid")).Name = "ModifiedInStoreId";
			esq.AddColumn(Guid.Empty, _uc.DataValueTypeManager.GetInstanceByName("Guid")).Name = "RemoteStoreId";
			esq.AddColumn("CreatedOn").Name = "Version";
			return esq;
		}

		private EntitySchemaQuery GetActivityEsq() {
			var esq = new EntitySchemaQuery(_uc.EntitySchemaManager, "Activity");
			esq.PrimaryQueryColumn.IsAlwaysSelect = true;
			esq.AddColumn("Title");
			esq.AddColumn("StartDate");
			esq.AddColumn("DueDate");
			esq.AddColumn("Location");
			esq.AddColumn("Notes");
			esq.AddColumn("Priority");
			esq.AddColumn("RemindToOwner");
			esq.AddColumn("RemindToOwnerDate");
			esq.AddColumn("ModifiedOn");
			esq.AddColumn("Status");
			esq.AddColumn("Organizer");
			return esq;
		}

		/// <summary>
		/// Fetch metadata <see cref="Entity"/>.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		/// <param name="metaData">Meeting metadata <see cref="Entity"/> instance.</param>
		/// <returns>Metadata <see cref="Entity"/>.</returns>
		private bool TryFetchMetadata(Meeting meeting, out Entity metadata) {
			var schema = _uc.EntitySchemaManager.GetInstanceByName("SysSyncMetaData");
			if (meeting.Calendar == null) {
				metadata = schema.CreateEntity(_uc);
				return false;
			}
			var esq = GetMetadataEsq();
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "LocalId", meeting.Id));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "CreatedBy", meeting.Calendar.OwnerId));
			var result = esq.GetEntityCollection(_uc);
			if (!result.Any() && Features.GetIsEnabled("GoogleMultidomain")) {
				var acEsq = GetActivityCorrespondenceEsq();
				acEsq.Filters.Add(acEsq.CreateFilterWithParameters(FilterComparisonType.Equal, "Activity", meeting.Id));
				acEsq.Filters.Add(acEsq.CreateFilterWithParameters(FilterComparisonType.Equal, "CreatedBy", meeting.Calendar.OwnerId));
				result = acEsq.GetEntityCollection(_uc);
			}
			metadata = result.Any()
				? result.FirstOrDefault()
				: schema.CreateEntity(_uc);
			return result.Any();
		}

		/// <summary>
		/// Get all metadata entities by <paramref name="meetingId"/>.
		/// </summary>
		/// <param name="meetingId">Meeting unique identifier.</param>
		/// <returns>All metadata entities by <paramref name="meetingId"/></returns>
		private List<Entity> GetMeetingMetadata(Guid meetingId) {
			var esq = GetMetadataEsq();
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "LocalId", meetingId));
			var ssmdList = esq.GetEntityCollection(_uc).ToList();
			if (Features.GetIsEnabled("GoogleMultidomain")) {
				var acEsq = GetActivityCorrespondenceEsq();
				acEsq.Filters.Add(acEsq.CreateFilterWithParameters(FilterComparisonType.Equal, "Activity", meetingId));
				var acList = acEsq.GetEntityCollection(_uc).ToList();
				ssmdList.AddRange(acList);
			}
			return ssmdList;
		}

		private List<Entity> GetMeetingsMetadata(IEnumerable<object> localIds, Guid ownerId)
		{
			if (localIds.IsEmpty()) {
				return new List<Entity>();
			}
			var ids = localIds.ToArray();
			var esq = GetMetadataEsq();
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "LocalId", ids));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "CreatedBy", ownerId));
			var ssmdList = esq.GetEntityCollection(_uc).ToList();
			if (Features.GetIsEnabled("GoogleMultidomain")) {
				var acEsq = GetActivityCorrespondenceEsq();
				acEsq.Filters.Add(acEsq.CreateFilterWithParameters(FilterComparisonType.Equal, "Activity", ids));
				acEsq.Filters.Add(acEsq.CreateFilterWithParameters(FilterComparisonType.Equal, "CreatedBy", ownerId));
				var acList = acEsq.GetEntityCollection(_uc).ToList();
				ssmdList.AddRange(acList);
			}
			return ssmdList;
		}

		/// <summary>
		/// Fill metadata entity.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		/// <param name="metaData">Meeting metadata <see cref="Entity"/> instance.</param>
		private void FillExtraParameters(Meeting meeting, Entity metadata) {
			var extraParameters = GetExtraParameters(metadata);
			var timezone = _uc.CurrentUser.TimeZone;
			extraParameters["RemoteId"] = meeting.RemoteId;
			extraParameters["ActivityHash"] = meeting.GetActualHash(timezone, false);
			extraParameters["StatusId"] = meeting.StatusId;
			extraParameters["StartDate"] = DateTimeUtils.ConvertTimeToUtc(meeting.StartDate, timezone);
			extraParameters["EndDate"] = DateTimeUtils.ConvertTimeToUtc(meeting.DueDate, timezone);
			extraParameters["Title"] = meeting.Title;
			extraParameters["InvitesSent"] = meeting.InvitesSent;
			extraParameters["NumberOfParticipants"] = meeting.Participants.Count;
			extraParameters["ActivityHashV2"] = meeting.GetActualHash(timezone);
			extraParameters["SourceCalendarName"] = meeting.Calendar?.Settings.SenderEmailAddress;
			metadata.SetColumnValue("ExtraParameters", Json.Serialize(extraParameters));
		}

		/// <summary>
		/// Returns extra parameters object for <paramref name="metadata"/>.
		/// Raw data will be taken from "ExtraParameters" column value.
		/// If "ExtraParameters" cloumn not loaded, or value for this column empty new object will be created.
		/// </summary>
		/// <param name="metadata"><see cref="Entity"/> metadata instance.</param>
		/// <returns><see cref="JObject"/> instance.</returns>
		private JObject GetExtraParameters(Entity metadata) {
			var extraParameters = new JObject();
			if (metadata.GetColumnValueNames().Contains("ExtraParameters")) {
				var rawMetadata = metadata.GetTypedColumnValue<string>("ExtraParameters");
				if (rawMetadata.IsNotNullOrEmpty()) {
					extraParameters = JObject.Parse(rawMetadata);
				}
			}
			return extraParameters;
		}

		/// <summary>
		/// Save activity metadata entity.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		/// <param name="state">Current meeting state.</param>
		private void SaveMetadata(Meeting meeting, SyncState state) {
			if (meeting.Calendar.Type == CalendarType.Google) {
				SaveGoogleMetadata(meeting, state);
				return;
			}
			if (TryFetchMetadata(meeting, out Entity metadata)) {
				if (state != SyncState.Deleted) {
					metadata.SetColumnValue("LocalState", state);
				}
				metadata.SetColumnValue("RemoteState", state);
				if (meeting.NeedInitialExport(meeting.Calendar)) {
					metadata.SetColumnValue("RemoteId", meeting.ICalUid);
				}
			}
			else {
				metadata.SetDefColumnValues();
				metadata.SetColumnValue("CreatedById", meeting.Calendar.OwnerId);
				metadata.SetColumnValue("LocalId", meeting.Id);
				metadata.SetColumnValue("SyncSchemaName", "Activity");
				metadata.SetColumnValue("RemoteItemName", "ExchangeAppointment");
				metadata.SetColumnValue("SchemaOrder", 0);
				metadata.SetColumnValue("LocalState", SyncState.New);
				metadata.SetColumnValue("RemoteState", SyncState.New);
				metadata.SetColumnValue("CreatedInStoreId", ExchangeConsts.LocalStoreId);
				metadata.SetColumnValue("ModifiedInStoreId", ExchangeConsts.LocalStoreId);
				metadata.SetColumnValue("RemoteStoreId", ExchangeConsts.AppointmentStoreId);
				metadata.SetColumnValue("RemoteId", meeting.ICalUid);
			}
			FillExtraParameters(meeting, metadata);
			metadata.SetColumnValue("Version", meeting.ModifiedOn);
			metadata.Save();
			_log?.LogDebug($"Meeting metadata '{metadata.PrimaryColumnValue}' saved for meeting {meeting}. State {state}");
			if (metadata.GetTypedColumnValue<SyncState>("LocalState") == SyncState.New) {
				_participantRepository.Save(meeting);
			}
		}

		/// <summary>
		/// Creates ActivityCorrespondence <see cref="Update"/> instance with
		/// Id filter using <paramref name="metadata"/> PrimaryColumnValue as filter value.
		/// </summary>
		/// <param name="metadata">Meeting metadata <see cref="Entity"/> instance.</param>
		/// <returns><see cref="Update"/> instance.</returns>
		private Update GetActivityCorrespondenceUpdate(Entity metadata) {
			return new Update(_uc, "ActivityCorrespondence")
				.Where("Id").IsEqual(Column.Parameter(metadata.PrimaryColumnValue)) as Update;
		}

		/// <summary>
		/// Save metadata in ActivityCorrespondence entity for google calendar meetings.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		/// <param name="state">Current meeting state.</param>
		private void SaveGoogleMetadata(Meeting meeting, SyncState state) {
			if (TryFetchMetadata(meeting, out Entity metadata)) {
				var update = GetActivityCorrespondenceUpdate(metadata);
				switch (state) {
					case SyncState.Deleted:
						_log?.LogInfo($"ActivityCorrespondence for '{meeting}' updated for delete in google.");
						update.Set("ActivityId", Column.Const(null));
						update.Execute();
						break;
					case SyncState.Modified:
						var extraParameters = GetExtraParameters(metadata);
						extraParameters["InvitesSent"] = meeting.InvitesSent;
						update.Set("ExtraParameters", Column.Parameter(Json.Serialize(extraParameters)));
						update.Execute();
						break;
				}
			}
		}

		/// <summary>
		/// Save activity entity.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		private void SaveActivity(Meeting meeting) {
			var schema = _uc.EntitySchemaManager.GetInstanceByName("Activity");
			var activity = schema.CreateEntity(_uc);
			if (!activity.FetchFromDB(meeting.Id)) {
				activity.SetDefColumnValues();
				activity.PrimaryColumnValue = meeting.Id;
				var statusId = meeting.DueDate > _uc.CurrentUser.GetCurrentDateTime()
					? _notStartedStatusId
					: _completedStatusId;

				if (SysSettings.TryGetValue(_uc, "SynchronizedActivitiesCategory", out object synchronizedActivitiesCategory)
					&& synchronizedActivitiesCategory is Guid) {
					activity.SetColumnValue("ActivityCategoryId", synchronizedActivitiesCategory);
				}
				activity.SetColumnValue("StatusId", statusId);
				if (meeting.OrganizerId.IsNotEmpty()) {
					activity.SetColumnValue("OrganizerId", meeting.OrganizerId);
				}
				else {
					activity.SetColumnValue("OrganizerId", null);
				}
			}
			else if (activity.GetTypedColumnValue<Guid>("OrganizerId").IsEmpty() &&
					meeting.OrganizerId.IsNotEmpty()) {
				activity.SetColumnValue("OrganizerId", meeting.OrganizerId);
			}
			if (meeting.RemoteCreatedOn != DateTime.MinValue &&
					activity.GetTypedColumnValue<DateTime>("RemoteCreatedOn") == DateTime.MinValue) {
				activity.SetColumnValue("RemoteCreatedOn", meeting.RemoteCreatedOn);
			}
			activity.SetColumnValue("Title", meeting.Title);
			activity.SetColumnValue("Location", meeting.Location);
			activity.SetColumnValue("Notes", meeting.Body);
			activity.SetColumnValue("ShowInScheduler", true);
			activity.SetColumnValue("StartDate", meeting.StartDate);
			activity.SetColumnValue("DueDate", meeting.DueDate);
			activity.SetColumnValue("ModifiedById", _uc.CurrentUser.ContactId);
			activity.SetColumnValue("PriorityId", meeting.PriorityId);
			activity.SetColumnValue("RemindToOwner", meeting.RemindToOwner);
			if (meeting.RemindToOwner) {
				activity.SetColumnValue("RemindToOwnerDate", meeting.RemindToOwnerDate);
			}
			else {
				activity.SetColumnValue("RemindToOwnerDate", null);
			}
			if (Features.GetIsEnabled("FeatureCancelActivityBySynchronization") && meeting.IsCancelled) {
				activity.SetColumnValue("StatusId", _canceledStatusId);
			}
			activity.Save();
			_log?.LogDebug($"Activity saved {meeting}.");
		}

		/// <summary>
		/// Send record change notification for participants.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		/// <param name="state"><see cref="EntityChangeType"/> record change type.</param>
		/// <param name="reason">Meeting UI notification reason.</param>
		private void SendRecordChange(Meeting meeting, EntityChangeType state, string reason) {
			var contactIds = new List<Guid>();
			if (meeting.InvitesSent) {
				contactIds = (from participant in meeting.Participants
							  where participant.ContactId.IsNotEmpty()
							  select participant.ContactId).ToList();
			}
			else if (meeting.Calendar != null) {
				contactIds.Add(meeting.Calendar.OwnerId);
			}
			if (!contactIds.Any()) {
				return;
			}
			_recordOperationsNotificator.SendRecordChange(contactIds, "Activity", meeting.Id, state);
			_log?.LogDebug($"Message to update client UI sent '{state}' to contacts: " +
				$"'{string.Join(", ", contactIds)}' for {meeting} with calendar owner {meeting.Calendar?.OwnerId}. Reason '{reason}'.");
		}

		private void DeleteMetadata(Meeting meeting) {
			if (TryFetchMetadata(meeting, out _)) {
				if (meeting.Calendar.Type == CalendarType.Exchange || meeting.Calendar.Type == CalendarType.Graph) {
					var perticipant = meeting.Participants.FirstOrDefault(p => p.ContactId == meeting.Calendar.OwnerId);
					if (perticipant != null) {
						_participantRepository.Delete(perticipant);
						_log?.LogInfo($"Participant '{meeting.Calendar.OwnerId}' deleted from meeting {meeting}.");
					}
				}
				SaveMetadata(meeting, SyncState.Deleted);
			}
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IMeetingRepository.GetMeetings(Guid)"/>
		public List<Meeting> GetMeetings(Guid meetingId) {
			var meetings = new List<Meeting>();
			if (TryFetchMeetingEntity(meetingId, out Entity meetingEntity)) {
				meetings = GetMeetingsInternal(meetingEntity);
			}
			return meetings;
		}

		/// <inheritdoc cref="IMeetingRepository.Load(Meeting)"/>
		public void Load(Meeting meeting) {
			if (meeting.Calendar == null) {
				return;
			}
			var metadataList = GetMeetingsMetadata(new List<object> { (object)meeting.Id }, meeting.Calendar.OwnerId);
			if (!metadataList.IsNullOrEmpty()) {
				var metadata = metadataList.First(e => e.GetTypedColumnValue<string>("SyncSchemaName") == "Activity");
				SetExternalStoreProperties(meeting, metadata);
			}
			var participants = _participantRepository.GetMeetingParticipants(meeting.Id);
			var participant = participants.First(p => p.ContactId == meeting.Calendar.OwnerId);
			meeting.ClearParticipants();
			AddParticipantsToMeeting(participants, participant, meeting);
		}

		/// <inheritdoc cref="IMeetingRepository.GetMeetings(Guid, DateTime)"/>
		public List<Meeting> GetMeetings(Guid contactId, DateTime syncSinceDate) {
			return GetMeetings(contactId, syncSinceDate, default);
		}

		/// <inheritdoc cref="IMeetingRepository.GetMeetings(Guid, DateTime, DateTime)"/>
		public List<Meeting> GetMeetings(Guid contactId, DateTime sinceDate, DateTime dueDate) {
			var meetings = new List<Meeting>();
			var meetingsEntities = InternalGetCalendarMeetings(contactId, sinceDate, dueDate);
			foreach (var meetingEntity in meetingsEntities) {
				meetings.AddRange(GetMeetingsInternal(meetingEntity));
			}
			return meetings;
		}

		/// <inheritdoc cref="IMeetingRepository.GetMeetings(string)"/>
		public List<Meeting> GetMeetings(string iCalUid) {
			var meetings = new List<Meeting>();
			var meetingEntity = FetchMeetingEntity(iCalUid);
			if (meetingEntity != null) {
				meetings = GetMeetingsInternal(meetingEntity);
			}
			return meetings;
		}

		/// <inheritdoc cref="IMeetingRepository.GetActivitiesByBaseICalUId"/>
		public List<(Guid ActivityId, DateTime DueDate)> GetActivitiesByBaseICalUId(string baseICalUid) {
			var activityEsq = new EntitySchemaQuery(_uc.EntitySchemaManager, "Activity");
			activityEsq.UseAdminRights = false;
			activityEsq.PrimaryQueryColumn.IsAlwaysSelect = true;
			var dueDateColumn = activityEsq.AddColumn("DueDate");
			var existsFilter = activityEsq.CreateExistsFilter("[SysSyncMetaData:LocalId:Id].Id");
			var subQuery = existsFilter.RightExpressions[0].SubQuery;
			subQuery.Filters.Add(subQuery.CreateFilterWithParameters(
				FilterComparisonType.StartWith,
				"RemoteId",
				baseICalUid));
			subQuery.Filters.Add(subQuery.CreateFilterWithParameters(
				FilterComparisonType.Equal,
				"SyncSchemaName",
				"Activity"));
			subQuery.Filters.Add(subQuery.CreateFilterWithParameters(
				FilterComparisonType.NotEqual,
				"LocalId",
				Guid.Empty));
			activityEsq.Filters.Add(existsFilter);
			var timezone = _uc.CurrentUser.TimeZone;
			return activityEsq.GetEntityCollection(_uc)
				.Select(e => (
					ActivityId: e.PrimaryColumnValue,
					DueDate: DateTimeUtils.ConvertTimeToUtc(e.GetTypedColumnValue<DateTime>(dueDateColumn.Name), timezone)
				))
				.ToList();
		}

		/// <inheritdoc cref="IMeetingRepository.GetDeletedMeetings(Guid)"/>
		public List<Meeting> GetDeletedMeetings(Guid meetingId) {
			var mettings = new List<Meeting>();
			var metadatas = GetMeetingMetadata(meetingId);
			foreach (var metadata in metadatas) {
				var meeting = new Meeting(meetingId, _log?.SessionId);
				var participant = _participantRepository.GetParticipant(meetingId,
					metadata.GetTypedColumnValue<Guid>("CreatedById")
				);
				FillCalendarProperties(meeting, participant, metadata);
				meeting.AddParticipant(participant);
				mettings.Add(meeting);
			}
			return mettings;
		}

		/// <inheritdoc cref="IMeetingRepository.GetDeletedMeetings(Guid, DateTime, DateTime, IEnumerable{string}, DateTime)"/>
		public List<Meeting> GetDeletedMeetings(Guid contactId, DateTime sinceDate, DateTime dueDate, IEnumerable<string> existingRemoteMeetings,
				DateTime createdBefore) {
			var meetings = new List<Meeting>();
			var meetingsEntities = InternalGetCalendarMeetings(contactId, sinceDate, dueDate, createdBefore);
			var metadata = GetMeetingsMetadata(meetingsEntities.Select(me => (object)me.PrimaryColumnValue), contactId)
				.Where(ssmd => !existingRemoteMeetings.Contains(ssmd.GetTypedColumnValue<string>("RemoteId")));
			var deletedMeetings = meetingsEntities.Where(me => metadata.Any(m => m.GetTypedColumnValue<Guid>("LocalId").Equals(me.PrimaryColumnValue))).ToList();
			foreach (var meetingEntity in deletedMeetings) {
				var meetingsForDeletion = GetMeetingsInternal(meetingEntity);
				foreach (var meetingForDeletion in meetingsForDeletion) {
					_log.LogDebug($"Added deleting meeting '{meetingForDeletion}'.");
				}
				meetings.AddRange(meetingsForDeletion);
			}
			return meetings;
		}

		/// <inheritdoc cref="IMeetingRepository.Save(Meeting)"/>
		public void Save(Meeting meeting) {
			SaveMetadata(meeting, SyncState.Modified);
			if (meeting.IsChanged(_uc.CurrentUser.TimeZone, true)) {
				SaveActivity(meeting);
				SendRecordChange(meeting, EntityChangeType.Updated, reason: "Activity create or update");
			}
			else {
				_log?.LogDebug($"Activity is not changed, save skipped {meeting}.");
			}
		}

		/// <inheritdoc cref="IMeetingRepository.SaveMetadata(Meeting)"/>
		public void SaveMetadata(Meeting meeting) {
			SaveMetadata(meeting, SyncState.Modified);
			if (meeting.IsChanged(_uc.CurrentUser.TimeZone, true)) {
				SendRecordChange(meeting, EntityChangeType.Updated, reason: "Update meeting metadata");
			}
		}

		/// <inheritdoc cref="IMeetingRepository.DeclineMeeting(Meeting)"/>
		public void DeclineMeeting(Meeting meeting) {
			DeleteMetadata(meeting);
			var participants = meeting.InvitesSent && meeting.OrganizerId.IsNotEmpty()
				? meeting.Participants
				: meeting.Participants.Where(p => p.ContactId == meeting.Calendar.OwnerId);
			if (Features.GetIsEnabled("FeatureCancelActivityBySynchronization") && meeting.InvitesSent) {
				var schema = _uc.EntitySchemaManager.GetInstanceByName("Activity");
				var activity = schema.CreateEntity(_uc);
				if (activity.FetchFromDB(meeting.Id, false)) {
					var statusId = activity.GetTypedColumnValue<Guid>("StatusId");
					if (statusId != _canceledStatusId) {
						activity.SetColumnValue("StatusId", _canceledStatusId);
						activity.Save();
					}
				}
			}
			_participantRepository.DeclineMeeting(participants);
			_log?.LogInfo($"Meeting marker as deleted in internal repository. {meeting}");
		}

		/// <inheritdoc cref="IMeetingRepository.Delete(Meeting)"/>
		public void Delete(Meeting meeting) {
			DeleteMetadata(meeting);
		}
		
		/// <inheritdoc cref="IMeetingRepository.GetActivitiesMetadataByLocalIds(Meeting)"/>
		public IEnumerable<(string ICalUId, Guid ActivityId)> GetActivitiesMetadataByLocalIds(IEnumerable<Guid> localIds, Guid contactId) {
			var ids = localIds.Select(id => (object)id).ToArray();
			var metadataEsq = new EntitySchemaQuery(_uc.EntitySchemaManager, "SysSyncMetaData") {
				PrimaryQueryColumn = {
					IsAlwaysSelect = true
				}
			};
			var remoteIdColumn = metadataEsq.AddColumn("RemoteId");
			var localIdColumn = metadataEsq.AddColumn("LocalId");
			metadataEsq.Filters.Add(metadataEsq.CreateFilterWithParameters(FilterComparisonType.Equal, "LocalId", ids));
			var remoteIdNotEmptyFilter = metadataEsq.CreateFilterWithParameters(FilterComparisonType.NotEqual,
				"RemoteId", string.Empty);
			metadataEsq.Filters.Add(remoteIdNotEmptyFilter);
			var metadataEntities = metadataEsq.GetEntityCollection(_uc);
			var results = metadataEntities
				.Select(e => (
					ICalUId: e.GetTypedColumnValue<string>(remoteIdColumn.Name),
					ActivityId: e.GetTypedColumnValue<Guid>(localIdColumn.Name)
				))
				.Where(tuple => !string.IsNullOrWhiteSpace(tuple.ICalUId))
				.Distinct()
				.ToList();
			return results;
		}

		#endregion

	}

	#endregion

}
