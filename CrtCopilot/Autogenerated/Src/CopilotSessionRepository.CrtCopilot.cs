namespace Creatio.Copilot
{
	using System;
	using System.Linq;
	using System.Data;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using Terrasoft.Configuration;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Entities;

	#region Interface: ICopilotSessionRepository

	public interface ICopilotSessionRepository
	{

		#region Methods: Public

		/// <summary>
		/// Returns all active Copilot sessions of the current user with preview data.
		/// </summary>
		/// <param name="userId">User identifier for filtering sessions.</param>
		/// <param name="request">Data for sessions pagination and search.</param>
		/// <returns>List of active sessions.</returns>
		List<CopilotActiveSessionDto> GetActiveSessionsWithPreview(Guid userId, CopilotSearchRequest request);

		/// <summary>
		/// Returns all active Copilot sessions of the current user with preview data.
		/// </summary>
		/// <param name="userId">User identifier for filtering sessions.</param>
		/// <param name="offset">Offset for sessions pagination.</param>
		/// <param name="count">Count of rows for sessions pagination.</param>
		/// <returns>List of active sessions.</returns>
		List<CopilotActiveSessionDto> GetActiveSessionsWithPreview(Guid userId, int offset, int count);

		/// <summary>
		/// Returns all active Copilot sessions of the current user with preview data.
		/// </summary>
		/// <param name="userId">User identifier for filtering sessions.</param>
		/// <returns>List of active sessions.</returns>
		List<CopilotActiveSessionDto> GetActiveSessionsWithPreview(Guid userId);

		/// <summary>
		/// Returns active Copilot session of the current user with preview data by id.
		/// </summary>
		/// <param name="userId">User identifier for filtering sessions.</param>
		/// <param name="sessionId">Session identifier.</param>
		/// <returns>Active session by id.</returns>
		CopilotActiveSessionDto GetActiveSessionPreviewById(Guid userId, Guid sessionId);

		#endregion

	}

	#endregion

	#region Class: CopilotIntentAuthor

	[DataContract]
	public class CopilotIntentAuthor
	{

		#region Properties: Public

		[DataMember(Name = "id")]
		public Guid Id {
			get; set;
		}

		[DataMember(Name = "name")]
		public string Name {
			get; set;
		}

		[DataMember(Name = "caption")]
		public string Caption {
			get; set;
		}

		#endregion

	}

	#endregion

	#region Class: CopilotSearchRequest

	[DataContract]
	public class CopilotSearchRequest
	{
		#region Properties: Public

		[DataMember(Name = "offset")]
		public int Offset {
			get; set;
		}

		[DataMember(Name = "count")]
		public int Count {
			get; set;
		}

		[DataMember(Name = "search")]
		public string Search {
			get; set;
		}

		#endregion
	}

	#endregion

	#region Class: CopilotActiveSessionDto

	[DataContract]
	public class CopilotActiveSessionDto
	{

		#region Properties: Public

		[DataMember(Name = "id")]
		public Guid Id {
			get; set;
		}

		[DataMember(Name = "date")]
		public string Date {
			get; set;
		}

		[DataMember(Name = "author")]
		public CopilotIntentAuthor Author {
			get; set;
		}

		[DataMember(Name = "caption")]
		public string Caption {
			get; set;
		}

		[DataMember(Name = "preview")]
		public string Preview {
			get; set;
		}

		[DataMember(Name = "previewMessageId")]
		public string PreviewMessageId {
			get; set;
		}

		[DataMember(Name = "boundedIntentId")]
		public Guid? BoundedIntentId {
			get; set;
		}

		#endregion

	}

	#endregion

	#region Class: CopilotSessionRepository

	/// <summary>
	/// Provides methods for accessing active Copilot sessions from the database.
	/// </summary>
	[DefaultBinding(typeof(ICopilotSessionRepository))]
	public class CopilotSessionRepository : ICopilotSessionRepository
	{

		#region Constants: Private

		private const string DateFormat = "yyyy'-'MM'-'ddTHH':'mm':'ss";

		#endregion

		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotSessionRepository"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection used for database operations.</param>
		public CopilotSessionRepository(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Properties: Protected

		private ICopilotSessionManager _copilotSessionManager;
		protected ICopilotSessionManager CopilotSessionManager {
			get
			{
				return _copilotSessionManager = _copilotSessionManager ?? ClassFactory.Get<ICopilotSessionManager>();
			}
		}

		#endregion

		#region Methods: Private

		private Select CreateSessionSelectQuery(Guid userId, Guid? sessionId = null) {
			var select = (Select)new Select(_userConnection)
					.Column("CSE", "Id").As("Id")
					.Column("CSE", "RootIntentId").As("IntentId")
					.Column("SS", "Name").As("IntentName")
					.Column("SS", "Caption").As("IntentCaption")
					.Column("CSE", "Title").As("Caption")
					.Column("CSE", "LastMessageContent").As("LastMessageContent")
					.Column("CSE", "LastMessageDate").As("LastMessageDate")
					.Column("CSE", "BoundedIntentId").As("BoundedIntentId")
					.Column("CSE", "ModifiedOn").As("ModifiedOn")
				.From("VwCopilotSessionEx").WithHints(Hints.NoLock).As("CSE")
				.LeftOuterJoin("SysSchema").As("SS")
					.On("CSE", "RootIntentId").IsEqual("SS", "UId")
				.Where("CSE", "StateId").IsEqual(Column.Parameter(CopilotSessionState.Active))
					.And("CSE", "UserId").IsEqual(Column.Parameter(userId));
			if (sessionId.HasValue) {
				select.And("CSE", "Id").IsEqual(Column.Parameter(sessionId.Value));
			}
			return select.OrderByDesc("CSE", "LastMessageDate") as Select;
		}

		private Select CreateSessionSubSearchQuery(Guid userId, string searchQuery) {
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "CopilotSessionEnt");
			esq.UseAdminRights = false;
			esq.AddColumn("Id");
			esq.AddColumn("Title");
			esq.AddColumn("User");
			var userFilter = esq.CreateFilterWithParameters(FilterComparisonType.Equal, "User", userId);
			esq.Filters.Add(userFilter);
			esq.AddColumn("[CopilotMessageEnt:CopilotSession].Content");
			esq.AddColumn("[CopilotMessageEnt:CopilotSession].CreatedOn");
			esq.AddColumn("[CopilotMessageEnt:CopilotSession].Id");
			var userRoleFilter = esq.CreateFilterWithParameters(FilterComparisonType.Equal, "[CopilotMessageEnt:CopilotSession].Role.Code", "user");
			var assistantRoleFilter = esq.CreateFilterWithParameters(FilterComparisonType.Equal, "[CopilotMessageEnt:CopilotSession].Role.Code", "assistant");
			var roleFilterGroup = new EntitySchemaQueryFilterCollection(esq, LogicalOperationStrict.Or, userRoleFilter, assistantRoleFilter);
			esq.Filters.Add(roleFilterGroup);
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.NotEqual, "[CopilotMessageEnt:CopilotSession].Content", string.Empty));
			if (!string.IsNullOrEmpty(searchQuery)) {
				var messageContentFilter = esq.CreateFilterWithParameters(FilterComparisonType.Contain, "[CopilotMessageEnt:CopilotSession].Content", searchQuery);
				var titleFilter = esq.CreateFilterWithParameters(FilterComparisonType.Contain, "Title", searchQuery);
				var contentOrTitleFilter = new EntitySchemaQueryFilterCollection(esq, LogicalOperationStrict.Or, messageContentFilter, titleFilter);
				esq.Filters.Add(contentOrTitleFilter);
			}

			return (Select)new Select(_userConnection)
				.Column("Id").As("SessionId")
				.Column(Func.Max("SearchQuery", "CopilotMessageEnt.CreatedOn")).As("MaxModifiedOn")
				.From(esq.GetSelectQuery(_userConnection)).As("SearchQuery")
				.GroupBy("Id");
		}

		private Select CreateSessionSearchQuery(Guid userId, string search) {
			var searchQuery = CreateSessionSubSearchQuery(userId, search);
			var select = (Select)new Select(_userConnection)
					.Column("CSE", "Id").As("Id")
					.Column("CSE", "RootIntentId").As("IntentId")
					.Column("SS", "Name").As("IntentName")
					.Column("SS", "Caption").As("IntentCaption")
					.Column("CSE", "Title").As("Caption")
					.Column("CME", "Content").As("LastMessageContent")
					.Column("CME", "CreatedOn").As("LastMessageDate")
					.Column("CSE", "BoundedIntentId").As("BoundedIntentId")
					.Column("CSE", "ModifiedOn").As("ModifiedOn")
					.Column("CME", "Id").As("LastMessageId")
				.From("CopilotSessionEnt").As("CSE")
				.InnerJoin("CopilotMessageEnt").As("CME").On("CSE", "Id").IsEqual("CME", "CopilotSessionId")
				.InnerJoin("CopilotMessageRoleEnt").As("CMR").On("CMR", "Id").IsEqual("CME", "RoleId")
					.And().OpenBlock("CMR", "Code").IsEqual(Column.Parameter("assistant"))
						.Or("CMR", "Code").IsEqual(Column.Parameter("user"))
						.CloseBlock()
					.And("CME", "Content").Not().IsEqual(Column.Parameter(string.Empty))
				.LeftOuterJoin("SysSchema").As("SS").On("CSE", "RootIntentId").IsEqual("SS", "UId")
				.InnerJoin(searchQuery).As("SQ")
					.On("CSE", "Id").IsEqual("SQ", "SessionId")
					.And("CME", "CreatedOn").IsEqual("SQ", "MaxModifiedOn");
			return select.OrderByDesc("CME", "CreatedOn") as Select;
		}

		private string FormatDateString(DateTime date) {
			return TimeZoneInfo
				.ConvertTime(date, TimeZoneInfo.Utc, _userConnection.CurrentUser.TimeZone)
				.ToString(DateFormat);
		}

		private bool HasColumn(IDataReader reader, string columnName) {
			return Enumerable.Range(0, reader.FieldCount)
				.Any(i => reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase));
		}

		private CopilotActiveSessionDto MapReaderToSession(IDataReader reader) {
			var session = new CopilotActiveSessionDto {
				Id = reader.GetColumnValue<Guid>("Id"),
				Caption = reader.GetColumnValue<string>("Caption"),
				Date = FormatDateString(reader.GetColumnValue<DateTime>("ModifiedOn")),
				Author = new CopilotIntentAuthor {
					Id = reader.GetColumnValue<Guid>("IntentId"),
					Name = reader.GetColumnValue<string>("IntentName"),
					Caption = reader.GetColumnValue<string>("IntentCaption")
				},
				BoundedIntentId = reader.GetColumnValue("BoundedIntentId") is Guid boundedIntentId ? (Guid?)boundedIntentId : null
			};
			string lastMessageContent = reader.GetColumnValue<string>("LastMessageContent");
			var lastMessageDate = reader.GetColumnValue("LastMessageDate") is DateTime date ? (DateTime?)date : null;
			if (lastMessageContent.IsNullOrEmpty() && !TryGetLiveLastMessageValues(session,
					out lastMessageContent, out lastMessageDate)) {
				return session;
			}
			session.Preview = lastMessageContent;
			if (HasColumn(reader, "LastMessageId")) {
				var lastMessageId = reader.GetColumnValue<Guid>("LastMessageId");
				session.PreviewMessageId = lastMessageId != Guid.Empty ? lastMessageId.ToString() : null;
			}
			if (lastMessageDate == null) {
				return session;
			}
			session.Date = FormatDateString(lastMessageDate.Value);
			return session;
		}

		private bool TryGetLiveLastMessageValues(CopilotActiveSessionDto session, out string lastMessageContent,
				out DateTime? lastMessageDate) {
			lastMessageContent = null;
			lastMessageDate = null;
			CopilotSession liveSession = CopilotSessionManager.FindById(session.Id);
			if (liveSession == null) {
				return false;
			}
			CopilotSession copilotSession = CopilotSessionManager.FindById(session.Id);
			CopilotMessage lastMessage = copilotSession?.Messages?
				.Where(m => new[] { "assistant", "user", "tool" }.Contains(m.Role))
				.OrderByDescending(message => message.Date)
				.FirstOrDefault();
			if (lastMessage == null) {
				return false;
			}
			lastMessageContent = lastMessage.Content;
			lastMessageDate = lastMessage.Date;
			return true;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />

		public List<CopilotActiveSessionDto> GetActiveSessionsWithPreview(Guid userId, int offset, int count) {
			return GetActiveSessionsWithPreview(userId, new CopilotSearchRequest {
				Offset = offset,
				Count = count
			});
		}

		public List<CopilotActiveSessionDto> GetActiveSessionsWithPreview(Guid userId, CopilotSearchRequest request) {
			var select = string.IsNullOrWhiteSpace(request.Search)
				? CreateSessionSelectQuery(userId)
				: CreateSessionSearchQuery(userId, request.Search);

			return select
				.OffsetFetch(request.Offset, request.Count)
				.ExecuteEnumerable(MapReaderToSession)
				.DistinctBy(s => s.Id)
				.ToList();
		}

		/// <inheritdoc />
		public List<CopilotActiveSessionDto> GetActiveSessionsWithPreview(Guid userId) {
			return CreateSessionSelectQuery(userId)
				.ExecuteEnumerable(MapReaderToSession)
				.DistinctBy(s => s.Id)
				.ToList();
		}

		/// <inheritdoc />
		public CopilotActiveSessionDto GetActiveSessionPreviewById(Guid userId, Guid sessionId) {
			CopilotActiveSessionDto session = CreateSessionSelectQuery(userId, sessionId)
				.ExecuteEnumerable(MapReaderToSession)
				.FirstOrDefault();
			return session;
		}

		#endregion

	}

	#endregion

}

