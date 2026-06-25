using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;

namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Repository {
    using Interfaces;
    using System;
    using System.Linq;
    using Terrasoft.Core;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Factories;

    #region Class: MeetingTranscriptRepository

    /// <summary>
    /// <see cref="IMeetingTranscriptRepository"/> implementation.
    /// </summary>
    [DefaultBinding(typeof(IMeetingTranscriptRepository))]
    public class MeetingTranscriptRepository : IMeetingTranscriptRepository {

        #region Constants: Private

        /// <summary>
        /// Meeting transcript schema name.
        /// </summary>
        private const string SchemaName = "MeetingTranscript";

        /// <summary>
        /// Transcript sync attempt schema name.
        /// </summary>
        private const string SyncAttemptSchemaName = "TranscriptSyncAttempt";

        #endregion

        #region Fields: Private

        /// <summary>
        /// <see cref="UserConnection"/> instance.
        /// </summary>
        private readonly UserConnection _uc;

        #endregion

        #region Constructors: Public

        /// <summary>
        /// .ctor.
        /// </summary>
        /// <param name="uc"><see cref="UserConnection"/> instance.</param>
        public MeetingTranscriptRepository(UserConnection uc) {
            _uc = uc;
        }

        #endregion

        #region Methods: Private

        /// <summary>
        /// Creates transcript query.
        /// </summary>
        /// <returns><see cref="EntitySchemaQuery"/> instance.</returns>
        private EntitySchemaQuery GetTranscriptQuery() {
            var esq = new EntitySchemaQuery(_uc.EntitySchemaManager, SchemaName) {
                UseAdminRights = false,
                PrimaryQueryColumn = {
                    IsAlwaysSelect = true
                }
            };
            esq.AddColumn("Activity");
            esq.AddColumn("Transcript");
            esq.AddColumn("MeetingSummary");
            esq.AddColumn("TranscriptUId");
            esq.AddColumn("CreatedOn");
            esq.AddColumn("ModifiedOn");
            return esq;
        }

        /// <summary>
        /// Maps database <see cref="Entity"/> to <see cref="MeetingTranscript"/> domain model.
        /// </summary>
        /// <param name="entity"><see cref="Entity"/> instance.</param>
        /// <returns><see cref="MeetingTranscript"/> instance.</returns>
        private static MeetingTranscript MapFromEntity(Entity entity) {
            return new MeetingTranscript {
                Id = entity.GetTypedColumnValue<Guid>("Id"),
                Transcript = entity.GetTypedColumnValue<string>("Transcript"),
                MeetingSummary = entity.GetTypedColumnValue<string>("MeetingSummary"),
                ActivityId = entity.GetTypedColumnValue<Guid>("ActivityId"),
                TranscriptUId = entity.GetTypedColumnValue<string>("TranscriptUId"),
                CreatedOn = entity.GetTypedColumnValue<DateTime>("CreatedOn"),
                ModifiedOn = entity.GetTypedColumnValue<DateTime>("ModifiedOn")
            };
        }

        /// <summary>
        /// Maps <see cref="MeetingTranscript"/> domain model to <see cref="Entity"/>.
        /// </summary>
        /// <param name="transcript">Domain model instance with transcript data.</param>
        /// <param name="entity">Entity instance to populate with data.</param>
        private static void MapToEntity(MeetingTranscript meetingTranscript, Entity entity) {
            entity.SetColumnValue("Transcript", meetingTranscript.Transcript);
            entity.SetColumnValue("MeetingSummary", meetingTranscript.MeetingSummary);
            entity.SetColumnValue("Activity", meetingTranscript.ActivityId);
            entity.SetColumnValue("TranscriptUId", meetingTranscript.TranscriptUId);
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Saves transcript sync attempt record.
        /// </summary>
        /// <param name="activityId">Activity identifier.</param>
        public void SaveTranscriptSyncAttempt(Guid activityId) {
            EntitySchema schema = _uc.EntitySchemaManager.GetInstanceByName(SyncAttemptSchemaName);
            Entity entity = schema.CreateEntity(_uc);
            entity.SetDefColumnValues();
            entity.SetColumnValue("ActivityId", activityId);
            entity.SetColumnValue("CreatedOn", DateTime.UtcNow);
            entity.Save();
        }

        /// <inheritdoc cref="IMeetingTranscriptRepository.GetTranscriptSyncInterval(Guid, DateTime, DateTime)"/>
        public System.Collections.Generic.IEnumerable<(Guid ActivityId, DateTime TargetSyncDateTime)>
            GetTranscriptSyncInterval(Guid contactId, DateTime syncPeriodStartDate, DateTime currentTime) {
            var intervalEsq = new EntitySchemaQuery(_uc.EntitySchemaManager, "TranscriptSyncInterval") {
                PrimaryQueryColumn = {
                    IsAlwaysSelect = true
                }
            };
            EntitySchemaQueryColumn minutesAfterEndColumn = intervalEsq.AddColumn("MinutesAfterEnd");
            EntityCollection intervalEntities = intervalEsq.GetEntityCollection(_uc);
            if (intervalEntities.Count == 0) {
                return Enumerable.Empty<(Guid, DateTime)>();
            }
            var activityEsq = new EntitySchemaQuery(_uc.EntitySchemaManager, "Activity") {
                PrimaryQueryColumn = {
                    IsAlwaysSelect = true
                }
            };
            EntitySchemaQueryColumn activityIdColumn = activityEsq.AddColumn("Id");
            EntitySchemaQueryColumn dueDateColumn = activityEsq.AddColumn("DueDate");
            IEntitySchemaQueryFilterItem participantFilter = activityEsq.CreateFilterWithParameters(
                FilterComparisonType.Equal, "[ActivityParticipant:Activity:Id].Participant", contactId);
            activityEsq.Filters.Add(participantFilter);
            IEntitySchemaQueryFilterItem dueDateStartFilter = activityEsq.CreateFilterWithParameters(
                FilterComparisonType.GreaterOrEqual, "DueDate", syncPeriodStartDate);
            activityEsq.Filters.Add(dueDateStartFilter);
            IEntitySchemaQueryFilterItem dueDateEndFilter = activityEsq.CreateFilterWithParameters(
                FilterComparisonType.LessOrEqual, "DueDate", currentTime);
            activityEsq.Filters.Add(dueDateEndFilter);
            activityEsq.Columns.GetByName("DueDate").OrderByAsc();
            EntityCollection activityEntities = activityEsq.GetEntityCollection(_uc);
            if (activityEntities.Count == 0) {
                return Enumerable.Empty<(Guid, DateTime)>();
            } 
            var activityInfo = new System.Collections.Generic.List<(Guid ActivityId, DateTime TargetSyncDateTime)>();
            foreach (Entity activity in activityEntities) {
                var activityId = activity.GetTypedColumnValue<Guid>(activityIdColumn.Name);
                DateTime dueDate = activity.GetTypedColumnValue<DateTime>(dueDateColumn.Name).ToUniversalTime();
                if (dueDate == DateTime.MinValue) {
                    continue;
                }
                foreach (Entity interval in intervalEntities) {
                    var minutesAfterEnd = interval.GetTypedColumnValue<int>(minutesAfterEndColumn.Name);
                    DateTime targetSyncDateTime = dueDate.AddMinutes(minutesAfterEnd);
                    if (targetSyncDateTime <= currentTime) {
                        activityInfo.Add((activityId, targetSyncDateTime));
                    }        
                }
            }
            return activityInfo;
        }

        /// <inheritdoc cref="IMeetingTranscriptRepository.GetTranscriptSyncAttemptsByActivity(System.Collections.Generic.IEnumerable{Guid}, DateTime)"/>
        public System.Collections.Generic.IDictionary<Guid, System.Collections.Generic.List<DateTime>>
            GetTranscriptSyncAttemptsByActivity(System.Collections.Generic.IEnumerable<Guid> activityIds,
                DateTime minTargetSyncTime) {
            var ids = activityIds?.ToList();
            if (ids == null || ids.Count == 0) {
                return new System.Collections.Generic.Dictionary<Guid, System.Collections.Generic.List<DateTime>>();
            } 
            var attemptEsq = new EntitySchemaQuery(_uc.EntitySchemaManager, SyncAttemptSchemaName) {
                PrimaryQueryColumn = {
                    IsAlwaysSelect = true
                }
            };
            EntitySchemaQueryColumn attemptActivityColumn = attemptEsq.AddColumn("Activity");
            EntitySchemaQueryColumn attemptCreatedOnColumn = attemptEsq.AddColumn("CreatedOn");
            attemptEsq.Filters.Add(attemptEsq.CreateFilterWithParameters(FilterComparisonType.Equal, "Activity",
                ids.Cast<object>().ToArray()));
            attemptEsq.Filters.Add(attemptEsq.CreateFilterWithParameters(FilterComparisonType.Greater, "CreatedOn",
                minTargetSyncTime));
            EntityCollection attemptEntities = attemptEsq.GetEntityCollection(_uc);
            return attemptEntities.GroupBy(a => a.GetTypedColumnValue<Guid>(attemptActivityColumn.Name)).ToDictionary(
                g => g.Key,
                g => g.Select(e => e.GetTypedColumnValue<DateTime>(attemptCreatedOnColumn.Name).ToUniversalTime())
                    .ToList());
        }

        /// <inheritdoc cref="IMeetingTranscriptRepository.TranscriptExists"/>
        public bool TranscriptExists(string transcriptUId) {
            if (string.IsNullOrEmpty(transcriptUId)) {
                return false;
            }
            var esq = new EntitySchemaQuery(_uc.EntitySchemaManager, SchemaName) {
                UseAdminRights = false
            };
            esq.AddColumn("Id");
            esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "TranscriptUId", transcriptUId));
            EntityCollection entities = esq.GetEntityCollection(_uc);
            return entities.Count > 0;
        }

        /// <inheritdoc cref="IMeetingTranscriptRepository.CreateTranscript"/>
        public MeetingTranscript CreateTranscript(MeetingTranscript transcript) {
            Entity entity = _uc.EntitySchemaManager.GetInstanceByName(SchemaName).CreateEntity(_uc);
            entity.SetDefColumnValues();
            MapToEntity(transcript, entity);
            entity.Save();
            transcript.Id = entity.PrimaryColumnValue;
            return transcript;
        }

        #endregion

    }

    #endregion

}
