namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Repository
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.FeatureToggling;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
    using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Entities;
	using Terrasoft.Common;
	using Terrasoft.Core.DB;
	using Terrasoft.EmailDomain;
	using Terrasoft.Sync;

	#region Class: ParticipantRepository

	/// <summary>
	/// <see cref="IParticipantRepository"/> implementation.
	/// </summary>
	[DefaultBinding(typeof(IParticipantRepository))]
	public class ParticipantRepository : IParticipantRepository
	{

		#region Fields: Private

		/// <summary>
		/// <see cref="ICalendarRepository"/> implementation instance.
		/// </summary>
		private readonly ICalendarRepository _calendarRepository;

		/// <summary>
		/// <see cref="UserConnection"/> instance.
		/// </summary>
		private readonly UserConnection _userConnection;

		/// <summary>
		/// <see cref="IRecordOperationsNotificator"/> implementation instance.
		/// </summary>
		private readonly IRecordOperationsNotificator _recordOperationsNotificator;

		/// <summary>
		/// <see cref="ICalendarLogger"/> instance.
		/// </summary
		private readonly ICalendarLogger _log;

        /// <summary>
        /// List of Mail server domain identifiers and names
        /// </summary>
        private readonly Dictionary<Guid, string> _mailServerDomains;
		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="calendarRepository"><see cref="ICalendarRepository"/> instance.</param>
		public ParticipantRepository(UserConnection uc, ICalendarRepository calendarRepository) {
			_userConnection = uc;
			_calendarRepository = calendarRepository;
			_recordOperationsNotificator = ClassFactory.Get<IRecordOperationsNotificator>(
				new ConstructorArgument("userConnection", uc));
            
			 _mailServerDomains = new Dictionary<Guid, string>();
			 if (Features.GetIsEnabled("UseCreateContactsByEmails")) {
			 	_mailServerDomains = GetMailServerDomains();
			 }
		}

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="sessionId">Session identifier.</param>
		/// <param name="calendarRepository"><see cref="ICalendarRepository"/> instance.</param>
		public ParticipantRepository(UserConnection uc, string sessionId, ICalendarRepository calendarRepository) :
			this(uc, calendarRepository) {
			_log = ClassFactory.Get<ICalendarLogger>(new ConstructorArgument("sessionId", sessionId),
				new ConstructorArgument("modelName", GetType().Name));
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Returns Mail server domains identifiers and names.
		/// </summary>
		/// <returns>List of Mail server domain identifiers and names.</returns>
		private Dictionary<Guid, string> GetMailServerDomains() {
			var domains = new Dictionary<Guid, string>();
			var select = new Select(_userConnection)
				.Column("Id")
				.Column("Domain")
				.From("MailServerDomain") as Select;
			using (var dbExecutor = _userConnection.EnsureDBConnection()) {
				using (var reader = select.ExecuteReader(dbExecutor)) {
					while (reader.Read())
						domains[reader.GetColumnValue<Guid>("Id")] = reader.GetColumnValue<string>("Domain").Trim().ToLower();
				}
			}

			return domains;
		}
                
		/// <summary>
		/// Get activity participants <see cref="EntityCollection"/> by <paramref name="meetingId"/>.
		/// </summary>
		/// <param name="meetingId"><see cref="Meeting"/> instance identifier.</param>
		/// <returns>Activity participants <see cref="EntityCollection"/>.</returns>
		private EntityCollection GetActivityParticipantEntities(Guid meetingId) {
			var esq = GetAllParticipantEsq();
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Activity", meetingId));
			return esq.GetEntityCollection(_userConnection);
		}
	        
		/// <summary>
		/// Get <see cref="EntitySchemaQuery"/> of all participant.
		/// </summary>
		/// <returns><see cref="EntitySchemaQuery"/> of all participant.</returns>
		private EntitySchemaQuery GetAllParticipantEsq() {
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "ActivityParticipant");
            esq.PrimaryQueryColumn.IsAlwaysSelect = true;
            esq.AddAllSchemaColumns();
            esq.AddColumn("Participant.Email").Name = "Email";
            return esq;
		}

		/// <summary>
		/// Create <see cref="Participant"/> model.
		/// </summary>
		/// <param name="activityParticipantEntity">Activity participant <see cref="Entity"/> instance.</param>
		/// <returns><see cref="Participant"/> model.</returns>
		private Participant CreateParticipantModel(Entity activityParticipantEntity) {
			if (activityParticipantEntity == null) {
				return default;
			}
			var participant = new Participant(activityParticipantEntity);
			var calendar = _calendarRepository.GetOwnerCalendar(participant.ContactId);
			var email = calendar == null
				? activityParticipantEntity.GetTypedColumnValue<string>("Email")
				: calendar.Settings.SenderEmailAddress;
			participant.EmailAddress = email;
			return participant;
		}

		/// <summary>
		/// Returns contact identifiers and emails.
		/// </summary>
		/// <param name="emails"> A list of email addresses.</param>
		/// <returns>List of contact identifiers and emails.</returns>
		private List<KeyValuePair<Guid, string>> GetContactsByEmails(HashSet<string> emails) {
			var contacts = new List<KeyValuePair<Guid, string>>();
			IEnumerable<string> searchEmails = emails.Select(e => e.Trim().ToLower()).Where(e => e.IsNotNullOrEmpty());
			if (!searchEmails.Any()) return contacts;
			var select = new Select(_userConnection)
					.Column("ContactCommunication", "ContactId")
					.Column("ContactCommunication", "SearchNumber").As("EmailAddress")
					.From("ContactCommunication")
					.InnerJoin("Contact").On("Contact", "Id").IsEqual("ContactCommunication", "ContactId")
					.Where("SearchNumber").In(Column.Parameters(searchEmails))
					.And("CommunicationTypeId")
					.IsEqual(Column.Parameter(Guid.Parse("ee1c85c3-cfcb-df11-9b2a-001d60e938c6")))
					.And().OpenBlock("SearchNumber").IsNotEqual(Column.Parameter(string.Empty))
					.Or("SearchNumber").Not().IsNull()
					.CloseBlock()
					.OrderByDesc("ContactCommunication", "CreatedOn")
				as Select;
			using (var dbExecutor = _userConnection.EnsureDBConnection()) {
				using (var reader = select.ExecuteReader(dbExecutor)) {
					while (reader.Read()) {
						contacts.Add(new KeyValuePair<Guid, string>(reader.GetColumnValue<Guid>("ContactId"), reader.GetColumnValue<string>("EmailAddress")));
					}
				}
			}

			return contacts;
		}

		/// <summary>
		/// Returns contact identifiers and emails.
		/// </summary>
		/// <param name="emails"> A list of email addresses.</param>
		/// <returns>List of contact identifiers and emails.</returns>
		public Dictionary<Guid, string> CreateContactsByEmails(List<string> emails) {
			var contacts = new Dictionary<Guid, string>();
			if (emails.Count == 0) {
				return contacts;
			}
			var schema = _userConnection.EntitySchemaManager.GetInstanceByName("Contact");
			foreach (string email in emails) {
				var contact = schema.CreateEntity(_userConnection);
				contact.SetDefColumnValues();
				contact.SetColumnValue("Name", email);
				contact.SetColumnValue("Email", email);
				contact.Save();
				contacts.Add(contact.PrimaryColumnValue, email);
			}
			return contacts;
		}

		/// <summary>
		/// Add new participant metadata.
		/// </summary>
		/// <param name="participantId"><see cref="Participant"/> unique identifier.</param>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		/// <param name="parentMetadata">Meeting metadta <see cref="Entity"/>.</param>
		private void AddParticipantMetadata(Guid participantId, Meeting meeting, Entity parentMetadata = null) {
			var schema = _userConnection.EntitySchemaManager.GetInstanceByName("SysSyncMetaData");
			var metadata = schema.CreateEntity(_userConnection);
			var entityCreatedBy = parentMetadata?.GetTypedColumnValue<Guid>("CreatedById") ?? Guid.Empty;
			var entityRemoteId = parentMetadata?.GetTypedColumnValue<string>("RemoteId");
			var createdBy = entityCreatedBy == Guid.Empty ? meeting.Calendar?.OwnerId : entityCreatedBy;
			var remoteId = entityRemoteId.IsNullOrEmpty() ? meeting.ICalUid : entityRemoteId;
			metadata.SetDefColumnValues();
			metadata.SetColumnValue("CreatedById", createdBy);
			metadata.SetColumnValue("ModifiedById", createdBy);
			metadata.SetColumnValue("LocalId", participantId);
			metadata.SetColumnValue("SyncSchemaName", "ActivityParticipant");
			metadata.SetColumnValue("RemoteItemName", "ExchangeAppointment");
			metadata.SetColumnValue("Version", meeting.ModifiedOn);
			metadata.SetColumnValue("SchemaOrder", 1);
			metadata.SetColumnValue("LocalState", SyncState.New);
			metadata.SetColumnValue("CreatedInStoreId", ExchangeConsts.LocalStoreId);
			metadata.SetColumnValue("ModifiedInStoreId", ExchangeConsts.LocalStoreId);
			metadata.SetColumnValue("RemoteStoreId", ExchangeConsts.AppointmentStoreId);
			metadata.SetColumnValue("RemoteId", remoteId);
			metadata.Save();
			_log?.LogDebug($"Participant '{participantId}' meatadata '{metadata.PrimaryColumnValue}' added {meeting}.");
		}

		/// <summary>
		/// Fetch meeting metadata entities.
		/// </summary>
		/// <param name="meetingId"></param>
		/// <returns>Entity collection of <see cref="Meeting"/> metadata <see cref="Entity"/>.</returns>
		private EntityCollection FetchParentMetadatas(Guid meetingId) {
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "SysSyncMetaData");
			esq.PrimaryQueryColumn.IsAlwaysSelect = true;
			esq.AddColumn("RemoteId");
			esq.AddColumn("CreatedBy");
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "LocalId", meetingId));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "SyncSchemaName", "Activity"));
			return esq.GetEntityCollection(_userConnection);
		}

		/// <summary>
		/// Add participants metadatas to parent <paramref name="meeting"/> metadata.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		/// <param name="participantId"><see cref="Participant"/> unique identifier.</param>
		private void ActualizeParticipantsForParentMetedata(Meeting meeting, Guid participantId) {
			var missingParticipants = GetActivityParticipantEntities(meeting.Id).Where(p => p.PrimaryColumnValue != participantId);
			foreach (var missingParticipant in missingParticipants) {
				AddParticipantMetadata(missingParticipant.PrimaryColumnValue, meeting);
			}
		}

		/// <summary>
		/// Add participant metadata to each parents <paramref name="meeting"/> metadatas.
		/// </summary>
		/// <param name="meeting"><see cref="Meeting"/> instance.</param>
		/// <param name="participantId"><see cref="Participant"/> unique identifier.</param>
		private void AddParticipantToParentsMetadatas(Meeting meeting, Guid participantId) {
			var adllParentsMetadatas = FetchParentMetadatas(meeting.Id);
			foreach (var metadata in adllParentsMetadatas) {
				AddParticipantMetadata(participantId, meeting, metadata);
			}
		}

		private void UpdateParticipants(List<Participant> actualParticipants, EntityCollection existingParticipants) {
			var toUpdate = existingParticipants
				.Where(ep => actualParticipants.Any(ap =>
					ap.InviteResponseId.IsNotEmpty() &&
					ep.GetTypedColumnValue<Guid>("ParticipantId").Equals(ap.ContactId) &&
					!ep.GetTypedColumnValue<Guid>("InviteResponseId").Equals(ap.InviteResponseId)))
				.ToList();
			foreach (var entity in toUpdate) {
				var participantId = entity.GetTypedColumnValue<Guid>("ParticipantId");
				var participant = actualParticipants.Find(act => act.ContactId == participantId);
				if (participant.InviteResponseId != Guid.Empty) {
					entity.SetColumnValue("InviteResponseId", participant.InviteResponseId);
				} else {
					entity.SetColumnValue("InviteResponseId", null);
				}
				entity.SetColumnValue("InviteParticipant", participant.IsInvited);
				try {
					entity.Save();
					_log?.LogDebug($"Update invite for participant '{entity.PrimaryColumnValue}' " +
					$"'{participant.ContactId}' to meeting {participant.MeetingId}.");
				} catch (ItemNotFoundException) {
					_log?.LogWarn($"Update invite for participant '{entity.PrimaryColumnValue}' " +
					$"'{participant.ContactId}' to meeting {participant.MeetingId} failed. InviteResponseId is not changed.");
				}
			}
		}

		private void CreateParticipants(List<Participant> actualParticipants, Guid meetingId, EntityCollection existingParticipants) {
			var toCreate = actualParticipants
				.Where(ap => !existingParticipants.Any(ep => ep.GetTypedColumnValue<Guid>("ParticipantId").Equals(ap.ContactId)))
				.ToList();
			var schema = _userConnection.EntitySchemaManager.GetInstanceByName("ActivityParticipant");
			foreach (var newParticipant in toCreate) {
				CreateParticipant(newParticipant, schema);
			}
			var contactIds = toCreate.Select(p => p.ContactId).ToList();
			if (contactIds.IsNullOrEmpty()) {
				return;
			}
			_recordOperationsNotificator.SendRecordChange(contactIds, "Activity", meetingId, EntityChangeType.Updated);
			_log?.LogDebug($"Message to update client UI sent '{EntityChangeType.Updated}' " +
				$"to contacts '{string.Join(", ", contactIds)}' for meeting {meetingId}. Reason 'Create new participant'.");
		}

		/// <summary>
		/// Creates new participant in internal repository.
		/// </summary>
		/// <param name="participant"><see cref="Participant"/> instance.</param>
		/// <param name="schema"><see cref="EntitySchema"/> instance.</param>
		private void CreateParticipant(Participant participant, EntitySchema schema) {
			var entity = schema.CreateEntity(_userConnection);
			entity.SetDefColumnValues();
			entity.SetColumnValue("ParticipantId", participant.ContactId);
			entity.SetColumnValue("ActivityId", participant.MeetingId);
			if (participant.InviteResponseId != Guid.Empty) {
				entity.SetColumnValue("InviteResponseId", participant.InviteResponseId);
				entity.SetColumnValue("InviteParticipant", participant.IsInvited);
			}
			entity.Save();
			_log?.LogDebug(string.Concat($"Add participant '{entity.PrimaryColumnValue}' '{participant.ContactId}' ",
				$"to meeting {participant.MeetingId}."));
		}

		/// <summary>
		/// Delete participants that are in the external repository but not in the internal repository.
		/// </summary>
		/// <param name="actualParticipants">Participants from extenrnal repository.</param>
		/// <param name="meetingId">Meeteni unique identifier.</param>
		/// <param name="existingParticipants">Participants from internal repository repository.</param>
		private void DeleteParticipants(List<Participant> actualParticipants, Guid meetingId, EntityCollection existingParticipants) {
			var toDelete = existingParticipants
				.Where(ep => !actualParticipants.Any(ap => ep.GetTypedColumnValue<Guid>("ParticipantId").Equals(ap.ContactId)))
				.ToList();
			foreach (var participant in toDelete) {
				participant.Delete();
				_log?.LogDebug($"Delete participant '{participant.PrimaryColumnValue}' " +
					$"'{participant.GetTypedColumnValue<Guid>("ParticipantId")}' from meeting {meetingId}.");
			}
			var contactIds = toDelete.Select(p => p.GetTypedColumnValue<Guid>("ParticipantId")).ToList();
			if (contactIds.IsNullOrEmpty()) {
				return;
			}
			_recordOperationsNotificator.SendRecordChange(contactIds, "Activity", meetingId, EntityChangeType.Deleted);
			_log?.LogDebug($"Message to update client UI sent '{EntityChangeType.Deleted}' " +
				$"to contacts '{string.Join(", ", contactIds)}' for meeting {meetingId}. Reason 'Delete participant'.");
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IParticipantRepository.GetMeetingParticipants(Guid)"/>
		public List<Participant> GetMeetingParticipants(Guid meetingId) {
			var activityParticipantsEntities = GetActivityParticipantEntities(meetingId);
			var result = new List<Participant>();
			foreach (var activityParticipantEntity in activityParticipantsEntities) {
				result.Add(CreateParticipantModel(activityParticipantEntity));
			}
			return result;
		}

		/// <inheritdoc cref="IParticipantRepository.GetParticipant(Guid, Guid)"/>
		public Participant GetParticipant(Guid meetingId, Guid contactId) {
			return new Participant(meetingId, contactId);
		}

		/// <inheritdoc cref="IParticipantRepository.GetParticipantContacts(List{string})"/>
		public Dictionary<Guid, string> GetParticipantContacts(List<string> emails) {
			var result = new Dictionary<Guid, string>();
			var contactEmails = new HashSet<string>();
			foreach (var email in emails) {
				var calendar = _calendarRepository.GetOwnerCalendar(email);
				if (calendar != null) {
					if (!result.ContainsKey(calendar.OwnerId)) {
						result.Add(calendar.OwnerId, email);
					}
				} else {
					contactEmails.Add(email);
				}
			}
			var contacts = GetContactsByEmails(contactEmails);
			foreach (var contact in contacts) {
				if (!result.ContainsKey(contact.Key)) {
					result.Add(contact.Key, contact.Value);
				}
			}

			if (Features.GetIsEnabled("UseCreateContactsByEmails")) {
				var existingEmails = new HashSet<string>(
					contacts.Select(c => c.Value.Trim().ToLower())
				);

				List<string> emailsForNewContacts = contactEmails.Select(e => e.Trim().ToLower())
					.Where(e => e.IsNotNullOrEmpty()
								&& !existingEmails.Contains(e)
								&& !_mailServerDomains.Values.Any(domain =>
									e.EndsWith(domain, StringComparison.InvariantCultureIgnoreCase))
					)
					.ToList();

				Dictionary<Guid, string> newContacts = CreateContactsByEmails(emailsForNewContacts);
				if (newContacts.Any()) {
					_log?.LogDebug(
						$"CreateContactsByEmails emailsForNewContacts: ['{string.Join(", ", emailsForNewContacts)}'], " +
						$"contacts: ['{string.Join(", ", contacts.Select(x => $"{x.Key},{x.Value}"))}'], " +
						$"contactEmails: ['{string.Join(", ", contactEmails)}'], " +
						$"newContacts: ['{string.Join(", ", newContacts.Select(x => $"{x.Key},{x.Value}"))}'], " +
						$"_mailServerDomain: ['{string.Join(", ", _mailServerDomains.Select(x => $"{x.Key},{x.Value}"))}']"
					);
				}				
				foreach (KeyValuePair<Guid, string> contact in newContacts)
					if (!result.ContainsKey(contact.Key))
						result.Add(contact.Key, contact.Value);
			}
			return result;
		}

		/// <inheritdoc cref="IParticipantRepository.GetParticipants(Guid, List{string}"/>
		public List<Participant> GetParticipants(Guid meetingId, List<string> emails) {
			var participants = new List<Participant>();
			var participantContacts = GetParticipantContacts(emails);
			foreach (var participantContact in participantContacts) {
				var participant = GetParticipant(meetingId, participantContact.Key);
				participant.EmailAddress = participantContact.Value;
				participants.Add(participant);
			}
			return participants;
		}

		/// <inheritdoc cref="IParticipantRepository.UpdateMeetingParticipants(List{Participant}, bool)"/>
		public void UpdateMeetingParticipants(List<Participant> actualParticipants, bool isOrganizer) {
			var esq = GetAllParticipantEsq();
			var meetingId = actualParticipants.First().MeetingId;
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Activity", meetingId));
			var existingParticipants = esq.GetEntityCollection(_userConnection);
			if (isOrganizer) {
				DeleteParticipants(actualParticipants, meetingId, existingParticipants);
			}
			UpdateParticipants(actualParticipants, existingParticipants);
			CreateParticipants(actualParticipants, meetingId, existingParticipants);
		}

		/// <inheritdoc cref="IParticipantRepository.UpdateParticipantInvitation(Participant)"/>
		public void UpdateParticipantInvitation(Participant participant) {
			var esq = GetAllParticipantEsq();
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.NotEqual,
				"InviteResponse", participant.InviteResponseId));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.NotEqual,
				"InviteParticipant", participant.IsInvited));
			var participantEntity = esq.GetEntity(_userConnection, participant.Id);
			if (participantEntity != null) {
				participantEntity.SetColumnValue("InviteResponseId", participant.InviteResponseId);
				participantEntity.SetColumnValue("InviteParticipant", participant.IsInvited);
				participantEntity.Save(false);
			}
		}

		/// <inheritdoc cref="IParticipantRepository.DeclineMeeting(IEnumerable{Participant})"/>
		public void DeclineMeeting(IEnumerable<Participant> participants) {
			if (participants.IsNullOrEmpty()) {
				return;
			}
			var meetingId = participants.First().MeetingId;
			var esq = GetAllParticipantEsq();
			esq.UseAdminRights = false;
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				"Participant", participants.Select(p => (object)p.ContactId)));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				"Activity", meetingId));
			foreach(var participantEntity in esq.GetEntityCollection(_userConnection)) {
				var contactId = participantEntity.GetTypedColumnValue<Guid>("ParticipantId");
				participantEntity.SetColumnValue("InviteResponseId", IntegrationConsts.Calendar.ParticipantResponse.Declined);
				participantEntity.SetColumnValue("InviteParticipant", false);
				participantEntity.Save(false);
				_log?.LogInfo($"Meeting {meetingId} declined for participant '{participantEntity.PrimaryColumnValue}'," +
					$" contact '{contactId}'");
			}
		}

		/// <inheritdoc cref="IParticipantRepository.Delete(Participant)"/>
		public void Delete(Participant participant) {
			var parentRemote = FetchParentMetadatas(participant.MeetingId).Select(m => m.GetTypedColumnValue<string>("RemoteId"));
			if (!parentRemote.Any()) {
				return;
			}
			var delete = new Delete(_userConnection)
					.From("SysSyncMetaData")
					.Where("SyncSchemaName").IsEqual(Column.Parameter("ActivityParticipant"))
					.And("RemoteId").In(Column.Parameters(parentRemote));
			delete.Execute();
		}

		/// <inheritdoc cref="IParticipantRepository.Save(Guid, Meeting)"/>
		public void Save(Meeting meeting) {
			var participant = meeting.Participants.Where(p => p.ContactId == meeting.Calendar?.OwnerId).FirstOrDefault();
			if (participant != null) {
				AddParticipantToParentsMetadatas(meeting, participant.Id);
				ActualizeParticipantsForParentMetedata(meeting, participant.Id);
			}
		}

		/// <inheritdoc cref="IParticipantRepository.CreateCurrentUserParticipant(Guid)"/>
		public void CreateCurrentUserParticipant(Guid meetingId) {
			var esq = GetAllParticipantEsq();
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Activity", meetingId));
			var existingParticipants = esq.GetEntityCollection(_userConnection);
			var currentContactId = _userConnection.CurrentUser.ContactId;
			var needCreateCurrentUserParticipant =
				!existingParticipants.Any(p => p.GetTypedColumnValue<Guid>("ParticipantId") == currentContactId);
			if (needCreateCurrentUserParticipant) {
				var schema = _userConnection.EntitySchemaManager.GetInstanceByName("ActivityParticipant");
				var participant = new Participant(meetingId, currentContactId);
				CreateParticipant(participant, schema);
			}
		}

		#endregion

	}

	#endregion

}
