namespace Terrasoft.Configuration.Workplace
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using Common;
	using Section;
	using Users;
	using Core;
	using Core.DB;
	using Core.Entities;
	using Core.Factories;
	using Core.Store;

	#region Class: WorkplaceRepository

	[DefaultBinding(typeof(IWorkplaceRepository))]
	public class WorkplaceRepository : IWorkplaceRepository
	{

		#region Fields: Private

		/// <summary>
		/// <see cref="UserConnection"/> instance.
		/// </summary>
		private readonly UserConnection _userConnection;

		/// <summary>
		/// <see cref="EntitySchemaManager"/> instance.
		/// </summary>
		private readonly EntitySchemaManager _entitySchemaManager;

		/// <summary>
		/// <see cref="ICacheStore"/> implementation instance.
		/// Represents session level cache.
		/// </summary>
		private readonly ICacheStore _sessionCache;

		/// <summary>
		/// <see cref="IResourceStorage"/> implementation instance.
		/// </summary>
		private readonly IResourceStorage _resourceStorage;

		#endregion

		#region Constructors: Public

		public WorkplaceRepository(UserConnection uc) {
			_userConnection = uc;
			_entitySchemaManager = uc.EntitySchemaManager;
			_sessionCache = uc.SessionCache;
			_resourceStorage = uc.ResourceStorage;
		}

		#endregion

		#region Methods: Private

		private Select GetClientUnitSchemaNameSelect(string sourceAlias, string sourceColumnAlias) {
			var clientUnitSchemaNameSelect = (Select)new Select(_userConnection)
					.Column("Name")
				.From("VwSysClientUnitSchema")
				.Where("SysWorkspaceId")
					.IsEqual(new QueryParameter("SysWorkspaceId", _userConnection.Workspace.Id, "Guid"))
				.And("UId").IsEqual(sourceAlias, sourceColumnAlias);
			clientUnitSchemaNameSelect.InitializeParameters();
			return clientUnitSchemaNameSelect;
		}

		/// <summary>
		/// Creates <see cref="Workplace"/> data select.
		/// </summary>
		/// <returns><see cref="Select"/> instance.</returns>
		private Select GetWorkplacesSelect() {
			Guid cultureId = _userConnection.CurrentUser.SysCultureId;
			var typeQuery = new Select(_userConnection)
					.Column(Func.Count("sm", "Id"))
					.From("SysModule").As("sm")
					.InnerJoin("SysModuleEntity").As("sme").On("sme", "Id").IsEqual("sm", "SysModuleEntityId")
					.InnerJoin("SysModuleInWorkplace").As("smiw").On("sm", "Id").IsEqual("smiw", "SysModuleId")
					.InnerJoin("SysModuleEntityInPortal").As("smeip").On("sme", "Id").IsEqual("smeip", "SysModuleEntityId")
					.Where("smiw", "SysWorkplaceId").IsEqual("SysWorkplace", "Id");
			var workplaceSelect = (Select)new Select(_userConnection)
					.Column("SysWorkplace", "Id")
					.Column("SysWorkplace", "Position")
					.Column("SysWorkplace", "IsPersonal")
					.Column("SysWorkplace", "LoaderId")
					.Column("SysWorkplace", "HomePageUId")
					.Column("SysWorkplace", "SysApplicationClientTypeId")
					.Column(GetClientUnitSchemaNameSelect("SysWorkplace", "LoaderId")).As("LoaderName")
				.From("SysWorkplace").As("SysWorkplace")
				.LeftOuterJoin("SysWorkplaceType").As("swt").On("SysWorkplace","TypeId")
					.IsEqual("swt", "Id");
			workplaceSelect.AddColumnLocalization("SysWorkplace",  "Name", "Name", cultureId);
			if (GetIsFeatureEnabled("UseTypedWorkplaces")) {
				workplaceSelect.Column("swt", "Code").As("Type");
			} else {
				workplaceSelect.Column(typeQuery).As("Type");
			}
			return workplaceSelect;
		}

		/// <summary>
		/// Creates new <see cref="Workplace"/> instance, using information from <paramref name="dataReader"/>.
		/// </summary>
		/// <param name="dataReader"><see cref="IDataReader"/> implementation instance.</param>
		/// <returns><see cref="Workplace"/> instance.</returns>
		private Workplace CreateWorkplaceInstance(IDataReader dataReader) {
			Guid workplaceId = dataReader.GetColumnValue<Guid>("Id");
			string name = dataReader.GetColumnValue<String>("Name");
			int position = dataReader.GetColumnValue<int>("Position");
			var type = dataReader.GetColumnValue<WorkplaceType>("Type");
			var homepage = dataReader.GetColumnValue<Guid>("HomePageUId");
			var workplace = new Workplace(workplaceId, name, type) {
				Position = position,
				IsPersonal = dataReader.GetColumnValue<bool>("IsPersonal"),
				LoaderId = dataReader.GetColumnValue<Guid>("LoaderId"),
				LoaderName = dataReader.GetColumnValue<string>("LoaderName"),
				ClientApplicationTypeId = dataReader.GetColumnValue<Guid>("SysApplicationClientTypeId"),
				HomePageUId = homepage == Guid.Empty ? null : (Guid?)homepage
			};
			var workplaceSections = GetWorkplaceSectionIds(workplaceId);
			workplace.AddSectionsRange(workplaceSections);
			var usersInWorkplace = GetWorkplaceUsers(workplaceId);
			workplace.AddUsersRange(usersInWorkplace);
			return workplace;
		}

		/// <summary>
		/// Returns <see cref="Section"/> from <paramref name="workplaceId"/> identifiers collection.
		/// </summary>
		/// <param name="workplaceId"><see cref="Workplace"/> unique identifier.</param>
		/// <returns><see cref="Section"/> identifiers collection.</returns>
		private IEnumerable<Guid> GetWorkplaceSectionIds(Guid workplaceId) {
			var select = (Select)new Select(_userConnection)
					.Column("smiw", "SysModuleId")
					.Column("smiw", "Position")
				.From("SysModuleInWorkplace").As("smiw")
				.Where("smiw", "SysWorkplaceId").IsEqual(Column.Parameter(workplaceId));
			List<KeyValuePair<int, Guid>> result = new List<KeyValuePair<int, Guid>>();
			using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection()) {
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						result.AddIfNotExists( new KeyValuePair<int, Guid> (
							dataReader.GetColumnValue<int>("Position"),
							dataReader.GetColumnValue<Guid>("SysModuleId")));
					}
				}
			}
			return result.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value);
		}

		/// <summary>
		/// Removes not actual section in workplace registration for <paramref name="workplace"/>.
		/// </summary>
		/// <param name="workplace"><see cref="Workplace"/> instance.</param>
		private void DeleteRemovedSections(Workplace workplace) {
			var delete = new Delete(_userConnection)
				.From("SysModuleInWorkplace")
				.Where("SysWorkplaceId").IsEqual(Column.Parameter(workplace.Id));
			if (workplace.HasSections()) {
				delete = delete.And("SysModuleId").Not().In(Column.Parameters(workplace.GetSectionIds()));
			}
			delete.Execute();
		}

		/// <summary>
		/// Creates section in workplace registration for <paramref name="workplace"/>.
		/// Existing items skipped.
		/// </summary>
		/// <param name="workplace"><see cref="Workplace"/> instance.</param>
		private void RegisterNewSections(Workplace workplace) {
			var schema = _entitySchemaManager.GetInstanceByName("SysModuleInWorkplace");
			foreach (var sectionId in workplace.GetSectionIds()) {
				var entity = schema.CreateEntity(_userConnection);
				entity.SetDefColumnValues();
				if (entity.FetchFromDB(new Dictionary<string, object> {
					{ "SysModule", sectionId },
					{ "SysWorkplace", workplace.Id }
				})) {
					continue;
				}
				entity.PrimaryColumnValue = Guid.NewGuid();
				entity.SetColumnValue("SysModuleId", sectionId);
				entity.SetColumnValue("SysWorkplaceId", workplace.Id);
				entity.Save();
			}
		}

		/// <summary>
		/// Actualise section in workplace registration for <paramref name="workplace"/>.
		/// </summary>
		/// <param name="workplace"><see cref="Workplace"/> instance.</param>
		private void ActualizeSectionsList(Workplace workplace) {
			DeleteRemovedSections(workplace);
			RegisterNewSections(workplace);
		}

		/// <summary>
		/// Selects workplaces data using <paramref name="select"/> and creates <see cref="Section"/> collection.
		/// </summary>
		/// <param name="select"><see cref="Select"/> instance.</param>
		/// <param name="cacheKey">Session cache item key.</param>
		/// <returns><see cref="Workplace"/> collection.</returns>
		private List<Workplace> GetWorkplaces(Select select, string cacheKey) {
			List<Workplace> workplaces = GetFromCache(cacheKey);
			if (workplaces != null) {
				return workplaces;
			}
			workplaces = GetWorkplacesFromDb(select);
			SetInCache(cacheKey, workplaces);
			return workplaces;
		}

		/// <summary>
		/// Selects workplaces data using <paramref name="select"/> and creates <see cref="Section"/> collection.
		/// </summary>
		/// <param name="select"><see cref="Select"/> instance.</param>
		/// <returns><see cref="Workplace"/> collection.</returns>
		private List<Workplace> GetWorkplacesFromDb(Select select) {
			var workplaces = new List<Workplace>();
			using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection()) {
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						workplaces.AddIfNotExists(CreateWorkplaceInstance(dataReader));
					}
				}
			}
			return workplaces;
		}

		/// <summary>
		/// Returns <see cref="Workplace"/> collection from cache.
		/// </summary>
		/// <param name="key">Cache key.</param>
		/// <returns><see cref="Workplace"/> collection.</returns>
		private List<Workplace> GetFromCache(string key) {
			return _sessionCache[key] as List<Workplace>;
		}

		private List<Guid> GetWorkplacesIdsFromCache(string key) {
			var workplaces = GetFromCache(key) ?? new List<Workplace>();
			return workplaces.Select(w => w.Id).ToList();
		}

		private List<Guid> GetCachedWorkplacesIdsByClientApplicationTypeId(Guid applicationClientTypeId) {
			string key = GetAllCacheKeyByClientApplicationType(applicationClientTypeId);
			List<Workplace> workplaces = GetFromCache(key) ?? new List<Workplace>();
			return workplaces.Select(w => w.Id).ToList();
		}

		/// <summary>
		/// Sets <paramref name="value"/> to cache in <paramref name="key"/>.
		/// </summary>
		/// <param name="key">Cache key.</param>
		/// <param name="value">Cache item value.</param>
		private void SetInCache(string key, List<Workplace> value) {
			_sessionCache[key] = value;
		}

		/// <summary>
		/// Clears all related to <paramref name="workplaceId"/> cache items.
		/// </summary>
		/// <param name="workplaceId"><see cref="Workplace"/> unique identifier.</param>
		private void ClearWorkplaceCache(Guid workplaceId) {
			_sessionCache.Remove(GetAllCacheKey());
			_sessionCache.Remove(GetCacheKey(workplaceId));
		}

		private void ClearAllWorkplaceCacheByClientApplicationType(Guid applicationClientTypeId) {
			_sessionCache.Remove(GetAllCacheKeyByClientApplicationType(applicationClientTypeId));
		}

		/// <summary>
		/// Returns all workplaces cache key.
		/// </summary>
		/// <returns>All workplaces cache key.</returns>
		private string GetAllCacheKey() {
			return "AllWorkplaces";
		}

		private string GetAllCacheKeyByClientApplicationType(Guid applicationClientTypeId) {
			return $"{GetAllCacheKey()}_{applicationClientTypeId}";
		}

		/// <summary>
		/// Returns workplace cache key.
		/// </summary>
		/// <param name="workplaceId"><see cref="Workplace"/> unique identifier.</param>
		/// <returns>Workplace cache key.</returns>
		private string GetCacheKey(Guid workplaceId) {
			return $"Workplace_{workplaceId}";
		}

		/// <summary>
		/// Creates workplace not found exception message.
		/// </summary>
		/// <param name="workplaceId"><see cref="Workplace"/> unique identifier.</param>
		/// <returns>Workplace not found exception message.</returns>
		private string GetItemNotFoundMessage(Guid workplaceId) {
			var messageTpl = new LocalizableString(_resourceStorage, "SectionExceptionResources",
					"LocalizableStrings.WorkplaceNotFoundByIdTpl.Value").ToString();
			return string.Format(messageTpl, workplaceId.ToString());
		}

		private Select GetSysAdminUnitInWorkplaceSelect(Guid entityId) {
			return new Select(_userConnection)
				.Column("sauiw", "SysAdminUnitId").As("UserGroupId")
				.Column("sauir", "SysAdminUnitId").As("UserInGroupId")
				.From("SysAdminUnitInWorkplace").As("sauiw")
				.InnerJoin("SysAdminUnitInRole").As("sauir").On("sauiw", "SysAdminUnitId").IsEqual("sauir", "SysAdminUnitRoleId")
				.Where("sauiw", "SysWorkplaceId").IsEqual(Column.Parameter(entityId)) as Select;
		}

		/// <summary>
		/// Loads user groups information from database using <paramref name="select"/>.
		/// </summary>
		/// <param name="select"><see cref="Select"/> instance.</param>
		/// <returns>Dictionary, user group identifier as key, user identifiers from group as values.</returns>
		private Dictionary<Guid, HashSet<Guid>> CreateUserGroupsData(Select select) {
			Dictionary<Guid, HashSet<Guid>> data = new Dictionary<Guid, HashSet<Guid>>();
			using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection()) {
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						var userGroupId = dataReader.GetColumnValue<Guid>("UserGroupId");
						var userInGroupId = dataReader.GetColumnValue<Guid>("UserInGroupId");
						if (!data.ContainsKey(userGroupId)) {
							data.Add(userGroupId, new HashSet<Guid>());
						}
						data[userGroupId].Add(userInGroupId);
					}
				}
			}
			return data;
		}

		private IEnumerable<IAdministrationUnit> GetWorkplaceUsers(Guid workplaceId) {
			var select = GetSysAdminUnitInWorkplaceSelect(workplaceId);
			var result = new List<IAdministrationUnit>();
			foreach (var kvp in CreateUserGroupsData(select)) {
				bool isUser = kvp.Value.All(id => id.Equals(kvp.Key));
				if (isUser) {
					result.Add(new WorkplaceUser(kvp.Key));
				} else {
					result.Add(new WorkplaceGroup(kvp.Key, kvp.Value));
				}
			}
			return result;
		}

		private Guid? GetWorkplaceTypeId(WorkplaceType type) {
			var typeEsq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "SysWorkplaceType") {
				Cache = _userConnection.SessionCache.WithLocalCaching("SysWorkplaceType"),
				CacheItemName = "WorkplaceType_Code"
			};
			string idColumnName = typeEsq.AddColumn("Id").Name;
			string codeColumnName = typeEsq.AddColumn("Code").Name;
			EntityCollection entityCollection = typeEsq.GetEntityCollection(_userConnection);
			var workplaceType = entityCollection.Where(t => t.GetTypedColumnValue<int>(codeColumnName) == (int)type)
				.FirstOrDefault();
			return workplaceType?.GetTypedColumnValue<Guid>(idColumnName);
		}

		private bool GetIsFeatureEnabled(string featureCode) {
			string featuresCacheGroupName = "FeaturesCache_{0}";
			string featuresCacheGroupItem = "FeatureInitializing_{0}_{1}";
			var currentUser = _userConnection.CurrentUser;
			if (currentUser == null) {
				return false;
			}
			var cacheGroupName = string.Format(featuresCacheGroupName, "AdminUnitFeatureState");
			var cacheItemName = string.Format(featuresCacheGroupItem, featureCode, currentUser.Id);
			var cacheStore = _userConnection.SessionCache.WithLocalCaching(cacheGroupName);
			if (cacheStore[cacheItemName] is int cachedValue) {
				return cachedValue == 1;
			}
			var select = (Select)new Select(_userConnection).Top(1)
				.Column("AdminUnitFeatureState", "FeatureState")
				.From("AdminUnitFeatureState")
				.InnerJoin("Feature").On("Feature", "Id").IsEqual("AdminUnitFeatureState", "FeatureId")
				.InnerJoin("SysAdminUnitInRole").On("SysAdminUnitInRole", "SysAdminUnitRoleId")
					.IsEqual("AdminUnitFeatureState", "SysAdminUnitId")
				.Where("Feature", "Code").IsEqual(Column.Parameter(featureCode))
					.And("SysAdminUnitInRole", "SysAdminUnitId").IsEqual(Column.Parameter(currentUser.Id))
				.OrderByDesc("AdminUnitFeatureState", "FeatureState");
			var value = select.ExecuteScalar<int>();
			cacheStore[cacheItemName] = value;
			return value == 1;
		}

		private void ClearWorkplacesCacheByIds(List<Guid> workplacesId) {
			foreach (var workplaceId in workplacesId) {
				ClearWorkplaceCache(workplaceId);
			}
		}

		private List<Guid> GetAllApplicationClientTypeIds() {
			string cacheKey = "ApplicationClientTypeIds";
			var ids = _sessionCache[cacheKey] as List<Guid>;
			if (ids != null) {
				return ids;
			}

			ids = new List<Guid>();
			var select = new Select(_userConnection)
				.Column("Id")
				.From("SysApplicationClientType");
			using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection()) {
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						ids.AddIfNotExists(dataReader.GetColumnValue<Guid>("Id"));
					}
				}
			}
			return ids;
		}

		private List<Guid> GetCachedWorkplacesByApplicationClientTypes(List<Guid> allApplicationClientTypeIds) {
			return allApplicationClientTypeIds.SelectMany(GetCachedWorkplacesIdsByClientApplicationTypeId)
				.Distinct().ToList();
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public IEnumerable<Workplace> GetAll() {
			var select = GetWorkplacesSelect();
			var cacheKey = GetAllCacheKey();
			return GetWorkplaces(select, cacheKey);
		}

		/// <inheritdoc />
		public Workplace Get(Guid workplaceId) {
			workplaceId.CheckArgumentEmpty(nameof(workplaceId));
			var workplace = Find(workplaceId);
			if (workplace != null) {
				return workplace;
			}
			var message = GetItemNotFoundMessage(workplaceId);
			throw new ItemNotFoundException(message);
		}

		/// <inheritdoc />
		public bool SaveWorkplace(Workplace workplace) {
			ActualizeSectionsList(workplace);
			var schema = _entitySchemaManager.GetInstanceByName("SysWorkplace");
			var entity = schema.CreateEntity(_userConnection);
			entity.SetDefColumnValues();
			entity.FetchFromDB(workplace.Id);
			entity.PrimaryColumnValue = workplace.Id;
			entity.SetColumnValue("Name", workplace.Name);
			entity.SetColumnValue("Position", workplace.Position);
			entity.SetColumnValue("HomePageUId", workplace.HomePageUId);
			if (workplace.ClientApplicationTypeId != Guid.Empty) {
				entity.SetColumnValue("SysApplicationClientTypeId", workplace.ClientApplicationTypeId);
			}
			if (GetIsFeatureEnabled("UseTypedWorkplaces")) {
				var workplaceType = GetWorkplaceTypeId(workplace.Type);
				if (workplaceType == null) {
					return false;
				}
				entity.SetColumnValue("TypeId", workplaceType);
			}
			var result = entity.Save();
			if (result) {
				ClearWorkplaceCache(workplace.Id);
				ClearAllWorkplaceCacheByClientApplicationType(workplace.ClientApplicationTypeId);
			}
			return result;
		}

		/// <inheritdoc />
		public void DeleteWorkplace(Guid workplaceId) {
			workplaceId.CheckArgumentEmpty(nameof(workplaceId));
			Workplace workplace = Get(workplaceId);
			var delete = (Delete)new Delete(_userConnection)
				.From("SysWorkplace")
				.Where("Id").IsEqual(Column.Parameter(workplaceId));
			delete.Execute();
			ClearWorkplaceCache(workplaceId);
			ClearAllWorkplaceCacheByClientApplicationType(workplace.ClientApplicationTypeId);
		}

		/// <inheritdoc />
		public void ClearCache() {
			var key = GetAllCacheKey();
			var workplacesId = GetWorkplacesIdsFromCache(key);
			List<Guid> allApplicationClientTypeIds = GetAllApplicationClientTypeIds();
			workplacesId.AddRangeIfNotExists(GetCachedWorkplacesByApplicationClientTypes(allApplicationClientTypeIds));
			ClearWorkplacesCacheByIds(workplacesId);
			ClearWorkplaceCache(Guid.Empty);
			foreach (var applicationClientTypeId in allApplicationClientTypeIds) {
				ClearAllWorkplaceCacheByClientApplicationType(applicationClientTypeId);
			}
		}

		/// <inheritdoc cref="IWorkplaceRepository.GetAllByApplicationClientType"/>
		public IEnumerable<Workplace> GetAllByApplicationClientType(Guid applicationClientTypeId) {
			applicationClientTypeId.CheckArgumentEmpty(nameof(applicationClientTypeId));
			var select = GetWorkplacesSelect();
			select = select.Where("SysWorkplace", "SysApplicationClientTypeId")
				.IsEqual(Column.Parameter(applicationClientTypeId)) as Select;
			var cacheKey = GetAllCacheKeyByClientApplicationType(applicationClientTypeId);
			return GetWorkplaces(select, cacheKey);
		}

		/// <inheritdoc cref="IWorkplaceRepository.ClearCacheByApplicationClientType"/>
		public void ClearCacheByApplicationClientType(Guid applicationClientTypeId) {
			applicationClientTypeId.CheckArgumentEmpty(nameof(applicationClientTypeId));
			var workplacesId = GetCachedWorkplacesIdsByClientApplicationTypeId(applicationClientTypeId);
			ClearWorkplacesCacheByIds(workplacesId);
			ClearAllWorkplaceCacheByClientApplicationType(applicationClientTypeId);
		}

		/// <inheritdoc cref="IWorkplaceRepository.Find"/>
		public Workplace Find(Guid workplaceId) {
			workplaceId.CheckArgumentEmpty(nameof(workplaceId));
			var select = GetWorkplacesSelect();
			select.Where("SysWorkplace", "Id").IsEqual(Column.Parameter(workplaceId));
			var cacheKey = GetCacheKey(workplaceId);
			var workplaces = GetWorkplaces(select, cacheKey);
			return workplaces.Any() ? workplaces.First() : null;
		}

		#endregion

	}

	#endregion


}