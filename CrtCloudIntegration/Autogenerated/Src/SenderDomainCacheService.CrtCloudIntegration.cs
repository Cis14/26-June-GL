namespace Terrasoft.Configuration
{
	using global::Common.Logging;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Configuration.CES;
	using Terrasoft.Configuration.CESModels;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: SenderDomainCacheService

	/// <inheritdoc cref="ISenderDomainCacheService"/>
	[DefaultBinding(typeof(ISenderDomainRepository))]
	[DefaultBinding(typeof(ISenderDomainCacheService))]
	[DefaultBinding(typeof(ISenderDomainValidationService))]
	public class SenderDomainCacheService : ISenderDomainRepository, ISenderDomainCacheService, ISenderDomainValidationService
	{
		#region Constants: Private

		private const string ActiveStatus = "active";
		private const string InactiveStatus = "inactive";
		private const string OkStatus = "ok";
		private const int MaxErrorLength = 500;

		#endregion

		#region Fields: Private

		private readonly ICESApi _serviceApi;
		private ILog _logger;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="SenderDomainCacheService"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="serviceApi">The service API.</param>
		public SenderDomainCacheService(UserConnection userConnection, ICESApi serviceApi) {
			_serviceApi = serviceApi;
			UserConnection = userConnection;
		}

		#endregion

		#region Properties: Private

		private ILog Logger => _logger ?? (_logger = LogManager.GetLogger("SenderDomainCacheService"));

		private int SenderDomainCachePeriod =>
			(int)Core.Configuration.SysSettings.GetValue(UserConnection, "SenderDomainCachePeriod");

		private UserConnection UserConnection { get; }

		#endregion

		#region Methods: Private

		private static string GetAuthKey(UserConnection userConnection) {
			return (string)Core.Configuration.SysSettings.GetValue(userConnection, "CloudServicesAuthKey");
		}

		private static void SetRecordDetailValues(Entity recordEntity, CloudSenderDomainRecordDetail detail) {
			var recordStatus = detail.Status.ToLowerInvariant() == "active"
				? SenderDomainRecordStatusValues.Verified
				: SenderDomainRecordStatusValues.Unverified;
			recordEntity.SetColumnValue("RecordStatus", recordStatus);
			recordEntity.SetColumnValue("Hostname", detail.Hostname);
			recordEntity.SetColumnValue("Type", detail.Type);
			recordEntity.SetColumnValue("Value", detail.Value);
		}

		private AccountInfo AccountInfo() {
			try {
				string authKey = GetAuthKey(UserConnection);
				return _serviceApi.AccountInfo(authKey);
			} catch (Exception e) {
				Logger.Error("Error while trying to get account info. " + $"ApiKey: {_serviceApi.ApiKey};", e);
				return null;
			}
		}

		private Guid CreateEmptySenderDomainsInfo(string cacheKey) {
			EntitySchema schema = UserConnection.EntitySchemaManager.GetInstanceByName("SenderDomainsInfo");
			Entity senderDomainsInfoSchema = schema.CreateEntity(UserConnection);
			var senderDomainsInfoId = Guid.NewGuid();
			senderDomainsInfoSchema.SetColumnValue("Id", senderDomainsInfoId);
			senderDomainsInfoSchema.SetColumnValue("CacheKey", cacheKey);
			senderDomainsInfoSchema.Save();
			return senderDomainsInfoId;
		}

		private Entity CreateFreshSenderDomainRecordDetailEntity(Guid senderDomainId, Guid categoryId) {
			EntitySchema schema = UserConnection.EntitySchemaManager.GetInstanceByName("SenderDomainRecordDetail");
			Entity recordEntity = schema.CreateEntity(UserConnection);
			recordEntity.SetColumnValue("Id", Guid.NewGuid());
			recordEntity.SetColumnValue("SenderDomain", senderDomainId);
			recordEntity.SetColumnValue("Category", categoryId);
			return recordEntity;
		}

		private CloudSenderDomainsInfo FetchSenderDomains(SenderDomainsInfoRequest senderDomainsInfoRequest) {
			try {
				return _serviceApi.SenderDomainsInfo(senderDomainsInfoRequest);
			} catch (Exception e) {
				Logger.Error(
					"Error while trying to get sender domains. " + $"ApiKey: {senderDomainsInfoRequest.ApiKey}; " +
					$"ProviderName: {senderDomainsInfoRequest.ProviderName}", e);
				return null;
			}
		}

		private DateTime? GetActualCheckDate(CloudSenderDomainsInfo storedSenderDomains) {
			DateTime? actualCheckDate = storedSenderDomains?.Domains?.Select(domain => domain.ModifiedOn).Min();
			return actualCheckDate;
		}

		private string GetCacheKey(string apiKey, string providerName) {
			return $"{providerName}_sender_domains_{apiKey}";
		}

		private EntityCollection GetSenderDomainRecordDetails(Guid senderDomainId) {
			EntitySchema schema = UserConnection.EntitySchemaManager.GetInstanceByName("SenderDomainRecordDetail");
			var esq = new EntitySchemaQuery(schema) {
				PrimaryQueryColumn = {
					IsAlwaysSelect = true
				}
			};
			esq.AddAllSchemaColumns();
			IEntitySchemaQueryFilterItem filter = esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				"SenderDomain.Id", senderDomainId);
			esq.Filters.Add(filter);
			return esq.GetEntityCollection(UserConnection);
		}

		private CloudSenderDomainsInfo GetSenderDomainsInfoByRequest(SenderDomainsInfoRequest senderDomainsInfoRequest,
			bool forceRefresh) {
			string cacheKey = GetCacheKey(senderDomainsInfoRequest.ApiKey, senderDomainsInfoRequest.ProviderName);
			CloudSenderDomainsInfo storedSenderDomainsInfo = GetStoredSenderDomainsData(cacheKey);
			if (!forceRefresh && IsCacheValid(storedSenderDomainsInfo)) {
				Logger.InfoFormat("Obtaining sender domains from cache. ApiKey: {0}; ProviderName: {1}",
					senderDomainsInfoRequest.ApiKey, senderDomainsInfoRequest.ProviderName);
				return storedSenderDomainsInfo;
			}
			CloudSenderDomainsInfo externalSenderDomains = FetchSenderDomains(senderDomainsInfoRequest);
			if (externalSenderDomains == null ||
				!externalSenderDomains.Status.Equals(OkStatus, StringComparison.InvariantCultureIgnoreCase)) {
				Logger.WarnFormat("Unable to fetch sender domains from CES. ApiKey: {0}; ProviderName: {1}; Status: {2}; Message: {3}",
					senderDomainsInfoRequest.ApiKey, senderDomainsInfoRequest.ProviderName, externalSenderDomains?.Status, externalSenderDomains?.Message);
				return storedSenderDomainsInfo;
			}
			IList<SenderDomainModel> upsertedDomains = UpsertDomains(
				storedSenderDomainsInfo,
				externalSenderDomains,
				cacheKey,
				senderDomainsInfoRequest.ProviderName);
			RemoveIrrelevantDomains(senderDomainsInfoRequest.ProviderName, upsertedDomains);
			RemoveOrphanedSenderDomainsInfo();
			return externalSenderDomains;
		}

		private IEnumerable<CloudSenderDomain> GetStoredDomains(CloudSenderDomainsInfo domainsInfo) {
			EntitySchema schema = UserConnection.EntitySchemaManager.GetInstanceByName("SenderDomain");
			var esq = new EntitySchemaQuery(schema);
			string idColumn = esq.AddColumn("Id").Name;
			string domainColumn = esq.AddColumn("Domain").Name;
			string errorColumn = esq.AddColumn("Error").Name;
			string mxColumn = esq.AddColumn("Mx").Name;
			string keyColumn = esq.AddColumn("Key").Name;
			string spfKeyColumn = esq.AddColumn("SpfKey").Name;
			string statusColumn = esq.AddColumn("Status").Name;
			string senderDomainsInfoColumn = esq.AddColumn("SenderDomainsInfo.Id").Name;
			string modifiedOnColumn = esq.AddColumn("ModifiedOn").Name;
			IEntitySchemaQueryFilterItem filter =
				esq.CreateFilterWithParameters(FilterComparisonType.Equal, "SenderDomainsInfo.Id", domainsInfo.Id);
			esq.Filters.Add(filter);
			EntityCollection entityCollection = esq.GetEntityCollection(UserConnection);
			IEnumerable<CloudSenderDomain> domains = entityCollection.Select(domain => new CloudSenderDomain {
				Id = domain.GetTypedColumnValue<Guid>(idColumn),
				Domain = domain.GetTypedColumnValue<string>(domainColumn),
				Error = domain.GetTypedColumnValue<string>(errorColumn),
				Mx = domain.GetTypedColumnValue<string>(mxColumn),
				Key = domain.GetTypedColumnValue<string>(keyColumn),
				SpfKey = domain.GetTypedColumnValue<string>(spfKeyColumn),
				Status = domain.GetTypedColumnValue<string>(statusColumn),
				SenderDomainsInfoId = domain.GetTypedColumnValue<Guid>(senderDomainsInfoColumn),
				ModifiedOn = TimeZoneUtilities.ConvertToUtc(UserConnection,
					domain.GetTypedColumnValue<DateTime>(modifiedOnColumn))
			});
			return domains;
		}

		private IEnumerable<CloudSenderDomainsInfo> GetStoredDomainsInfo(string cacheKey) {
			EntitySchema schema = UserConnection.EntitySchemaManager.GetInstanceByName("SenderDomainsInfo");
			var esq = new EntitySchemaQuery(schema);
			string idColumn = esq.AddColumn("Id").Name;
			string codeColumn = esq.AddColumn("Code").Name;
			string defaultDKIMKeyColumn = esq.AddColumn("DefaultDKIMKey").Name;
			string dKIMRecordColumn = esq.AddColumn("DKIMRecord").Name;
			string dmarkValueColumn = esq.AddColumn("DmarkValue").Name;
			string domainValidationRecordColumn = esq.AddColumn("DomainValidationRecord").Name;
			string messageColumn = esq.AddColumn("Message").Name;
			string spfValueColumn = esq.AddColumn("SpfValue").Name;
			string statusColumn = esq.AddColumn("Status").Name;
			string cacheKeyColumn = esq.AddColumn("CacheKey").Name;
			IEntitySchemaQueryFilterItem filter =
				esq.CreateFilterWithParameters(FilterComparisonType.Equal, cacheKeyColumn, cacheKey);
			esq.Filters.Add(filter);
			EntityCollection entityCollection = esq.GetEntityCollection(UserConnection);
			return entityCollection.Select(domainsInfo => new CloudSenderDomainsInfo {
				Id = domainsInfo.GetTypedColumnValue<Guid>(idColumn),
				Code = domainsInfo.GetTypedColumnValue<int>(codeColumn),
				DefaultDKIMKey = domainsInfo.GetTypedColumnValue<string>(defaultDKIMKeyColumn),
				DKIMRecord = domainsInfo.GetTypedColumnValue<string>(dKIMRecordColumn),
				DmarkValue = domainsInfo.GetTypedColumnValue<string>(dmarkValueColumn),
				DomainValidationRecord = domainsInfo.GetTypedColumnValue<string>(domainValidationRecordColumn),
				Message = domainsInfo.GetTypedColumnValue<string>(messageColumn),
				SpfValue = domainsInfo.GetTypedColumnValue<string>(spfValueColumn),
				Status = domainsInfo.GetTypedColumnValue<string>(statusColumn),
				CacheKey = domainsInfo.GetTypedColumnValue<string>(cacheKeyColumn),
				Domains = new List<CloudSenderDomain>()
			});
		}

		private CloudSenderDomainsInfo GetStoredSenderDomainsData(string cacheKey) {
			CloudSenderDomainsInfo storedDomainsInfo = GetStoredDomainsInfo(cacheKey).FirstOrDefault();
			if (storedDomainsInfo == null) {
				return null;
			}
			IEnumerable<CloudSenderDomain> storedDomains = GetStoredDomains(storedDomainsInfo);
			if (!storedDomains.Any()) {
				return storedDomainsInfo;
			}
			storedDomainsInfo.Domains.AddRange(storedDomains);
			return storedDomainsInfo;
		}

		private bool IsCacheValid(CloudSenderDomainsInfo storedSenderDomains) {
			DateTime checkDateTime = DateTime.UtcNow.AddMinutes(-SenderDomainCachePeriod);
			return storedSenderDomains != null && storedSenderDomains.Domains.Any() &&
				storedSenderDomains.Domains.All(domain =>
					domain.Status.Equals(ActiveStatus, StringComparison.InvariantCultureIgnoreCase)) &&
				checkDateTime < GetActualCheckDate(storedSenderDomains);
		}

		private void RemoveSenderDomainChildRelationsByIds(Guid[] domainIds) {
			if (!domainIds.Any()) {
				return;
			}
			QueryColumnExpression[] parameters = domainIds.Select(id => Column.Parameter(id)).ToArray();
			var deleteRecordDetail = new Delete(UserConnection)
				.From("SenderDomainRecordDetail")
				.Where("SenderDomainId").In(parameters);
			deleteRecordDetail.Execute();
			var deleteDnsGuideFile = new Delete(UserConnection)
				.From("DNSGuideFile")
				.Where("SenderDomainId").In(parameters);
			deleteDnsGuideFile.Execute();
			var deleteDnsGuideReport = new Delete(UserConnection)
				.From("DNSGuideReport")
				.Where("SenderDomainId").In(parameters);
			deleteDnsGuideReport.Execute();
		}

		/// <summary>
		/// Removes sender domains that are duplicates, legacy (empty provider), or no longer present in CES
		/// </summary>
		private void RemoveIrrelevantDomains(string providerName, IList<SenderDomainModel> validDomains) {
			Guid[] validIds = validDomains.Select(d => d.Id).ToArray();
			var domainQuery = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "SenderDomain") {
				PrimaryQueryColumn = { IsAlwaysSelect = true }
			};
			var providerFilterGroup = new EntitySchemaQueryFilterCollection(domainQuery,
				LogicalOperationStrict.Or) {
				domainQuery.CreateFilterWithParameters(FilterComparisonType.Equal, "SenderProvider", providerName),
				domainQuery.CreateFilterWithParameters(FilterComparisonType.Equal, "SenderProvider", string.Empty)
			};
			domainQuery.Filters.Add(providerFilterGroup);
			if (validIds.Any()) {
				foreach (Guid validId in validIds) {
					domainQuery.Filters.Add(domainQuery.CreateFilterWithParameters(
						FilterComparisonType.NotEqual, "Id", validId));
				}
			}
			Entity[] domainsToDelete = domainQuery.GetEntityCollection(UserConnection).ToArray();
			Guid[] domainsToDeleteIds = domainsToDelete.Select(d => d.PrimaryColumnValue).ToArray();
			Logger.InfoFormat(
				"Removing irrelevant sender domains. ProviderName: {0}; DomainsCount: {1}",
				providerName, domainsToDeleteIds.Length);
			RemoveSenderDomainChildRelationsByIds(domainsToDeleteIds);
			foreach (var entity in domainsToDelete) {
				entity.Delete();
			}
		}

		/// <summary>
		/// Removes SenderDomainsInfo records that are no longer referenced by any SenderDomain.
		/// </summary>
		private void RemoveOrphanedSenderDomainsInfo() {
			var infoEsq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "SenderDomainsInfo") {
				PrimaryQueryColumn = { IsAlwaysSelect = true }
			};
			infoEsq.AddColumn("CacheKey");
			Entity[] allInfoEntities = infoEsq.GetEntityCollection(UserConnection).ToArray();
			foreach (var infoEntity in allInfoEntities) {
				var count = (Select)new Select(UserConnection)
					.Column(Func.Count("Id"))
					.From("SenderDomain")
					.Where("SenderDomainsInfoId").IsEqual(Column.Parameter(infoEntity.PrimaryColumnValue));
				if (count.ExecuteScalar<int>() == 0) {
					Logger.InfoFormat(
						"Removing orphaned SenderDomainsInfo. Id: {0}; CacheKey: {1}",
						infoEntity.PrimaryColumnValue, infoEntity.GetTypedColumnValue<string>("CacheKey"));
					infoEntity.Delete();
				}
			}
		}

		private void SetProviderNameIfNeeded(Action<string> setProviderName, string providerName) {
			if (providerName.IsNotNullOrEmpty()) {
				return;
			}
			AccountInfo accountInfo = AccountInfo();
			providerName = accountInfo?.Services?.FirstOrDefault()?.Settings?.Provider;
			if (!string.IsNullOrEmpty(providerName)) {
				setProviderName(providerName);
			}
		}

		private Guid UpdateSenderDomainValidationStatus(Guid domainId,
			CloudDomainValidationResponse validationResponse) {
			EntitySchema schema = UserConnection.EntitySchemaManager.GetInstanceByName("SenderDomain");
			Entity entity = schema.CreateEntity(UserConnection);
			bool fetchResult = entity.FetchFromDB(domainId);
			if (!fetchResult) {
				return Guid.Empty;
			}
			string domainStringStatus = validationResponse.IsValid ? "active" : "inactive";
			Guid domainValidationStatus = validationResponse.IsValid
				? SenderDomainStatusValues.Validated
				: SenderDomainStatusValues.Unvalidated;
			entity.SetColumnValue("Status", domainStringStatus);
			entity.SetColumnValue("ModifiedOn", DateTime.UtcNow);
			entity.SetColumnValue("SenderDomainStatus", domainValidationStatus);
			entity.Save();
			UpsertDomainRecordDetailsStatus(domainId, validationResponse);
			return entity.PrimaryColumnValue;
		}

		private IList<SenderDomainModel> UpsertDomains(
			CloudSenderDomainsInfo storedDomainInfo,
			CloudSenderDomainsInfo externalDomainsInfo,
			string cacheKey,
			string providerName) {
			var senderDomainsInfoId = UpsertSenderDomainsInfo(storedDomainInfo, externalDomainsInfo, cacheKey);
			var result = new List<SenderDomainModel>();
			if (externalDomainsInfo.Domains != null) {
				foreach (var domainInfo in externalDomainsInfo.Domains) {
					domainInfo.SenderDomainsInfoId = senderDomainsInfoId;
					var senderDomain = UpsertSenderDomain(domainInfo, providerName);
					result.Add(senderDomain);
					UpsertDomainRecordDetails(senderDomain.Id, domainInfo);
				}
			}
			return result;
		}

		private Guid UpsertSenderDomainsInfo(CloudSenderDomainsInfo storedSenderDomains,
			CloudSenderDomainsInfo externalSenderDomains, string cacheKey) {
			EntitySchema schema = UserConnection.EntitySchemaManager.GetInstanceByName("SenderDomainsInfo");
			Entity senderDomainsInfoSchema = schema.CreateEntity(UserConnection);
			Guid senderDomainsInfoId;
			if (storedSenderDomains != null && storedSenderDomains.Id != default) {
				senderDomainsInfoSchema.FetchFromDB(new Dictionary<string, object> {
					{ "Id", storedSenderDomains.Id }
				});
				senderDomainsInfoId = storedSenderDomains.Id;
			} else {
				senderDomainsInfoId = Guid.NewGuid();
				senderDomainsInfoSchema.SetColumnValue("Id", senderDomainsInfoId);
			}
			senderDomainsInfoSchema.SetColumnValue("Code", externalSenderDomains?.Code);
			senderDomainsInfoSchema.SetColumnValue("DefaultDKIMKey", externalSenderDomains?.DefaultDKIMKey);
			senderDomainsInfoSchema.SetColumnValue("DKIMRecord", externalSenderDomains?.DKIMRecord);
			senderDomainsInfoSchema.SetColumnValue("DmarkValue", externalSenderDomains?.DmarkValue);
			senderDomainsInfoSchema.SetColumnValue("DomainValidationRecord",
				externalSenderDomains?.DomainValidationRecord);
			senderDomainsInfoSchema.SetColumnValue("Message", externalSenderDomains?.Message);
			senderDomainsInfoSchema.SetColumnValue("SpfValue", externalSenderDomains?.SpfValue);
			senderDomainsInfoSchema.SetColumnValue("Status", externalSenderDomains?.Status);
			senderDomainsInfoSchema.SetColumnValue("CacheKey", cacheKey);
			senderDomainsInfoSchema.Save();
			return senderDomainsInfoId;
		}

		private void UpsertDomainRecordDetails(Guid senderDomainId, CloudSenderDomain domainInfo) {
			List<CloudSenderDomainRecordDetail> recordDetails =
				domainInfo.RecordDetails ?? new List<CloudSenderDomainRecordDetail>();
			Dictionary<Guid, Entity> detailEntities = GetSenderDomainRecordDetails(senderDomainId)
				.ToDictionary(x => x.GetTypedColumnValue<Guid>("Category"), x => x);
			foreach (var recordDetail in recordDetails) {
				string categoryName = recordDetail.SenderDomainRecordCategory.ToUpperInvariant();
				if (!SenderDomainRecordCategoryValues.CategoryNameToIdMap.TryGetValue(categoryName,
						out Guid categoryId)) {
					continue;
				}
				if (!detailEntities.TryGetValue(categoryId, out Entity recordEntity)) {
					recordEntity = CreateFreshSenderDomainRecordDetailEntity(senderDomainId, categoryId);
					detailEntities[categoryId] = recordEntity;
				}
				SetRecordDetailValues(recordEntity, recordDetail);
				recordEntity.Save();
			}
		}

		private void UpsertDomainRecordDetailsStatus(Guid senderDomainId,
			CloudDomainValidationResponse validationResponse) {
			Dictionary<Guid, Entity> detailEntities = GetSenderDomainRecordDetails(senderDomainId)
				.ToDictionary(x => x.GetTypedColumnValue<Guid>("Category"), x => x);
			foreach (CloudRecordValidationResult recordValidationResult in validationResponse.RecordsValidation) {
				string categoryName = recordValidationResult.RecordCategory.ToUpperInvariant();
				if (!SenderDomainRecordCategoryValues.CategoryNameToIdMap.TryGetValue(categoryName,
						out Guid categoryId)) {
					continue;
				}
				if (detailEntities.TryGetValue(categoryId, out Entity recordEntity)) {
					if (recordValidationResult.IsValid) {
						recordEntity.SetColumnValue("RecordStatus", SenderDomainRecordStatusValues.Verified);
						recordEntity.SetColumnValue("ErrorMessage", string.Empty);
					} else {
						recordEntity.SetColumnValue("RecordStatus", SenderDomainRecordStatusValues.Unverified);
						string message = recordValidationResult.Message ?? string.Empty;
						if (!string.IsNullOrWhiteSpace(recordValidationResult.Message) && recordValidationResult.Message.Length > MaxErrorLength) {
							message = recordValidationResult.Message.Substring(0, MaxErrorLength);
						}
						recordEntity.SetColumnValue("ErrorMessage", message);
					}
					recordEntity.SetColumnValue("ModifiedOn", DateTime.UtcNow);
					recordEntity.Save();
				}
			}
		}

		/// <summary>
		/// Adds or updates sender domain based on the domain name and provider.
		/// This allows to avoid duplicates in case of multiple requests for the same domain while still updating domain status if needed.
		/// </summary>
		/// <param name="senderDomain"></param>
		/// <param name="providerName"></param>
		private SenderDomainModel UpsertSenderDomain(CloudSenderDomain senderDomain, string providerName) {
			EntitySchema schema = UserConnection.EntitySchemaManager.GetInstanceByName("SenderDomain");
			Entity entity = schema.CreateEntity(UserConnection);
			var domainId = Guid.Empty;
			var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "SenderDomain") {
				PrimaryQueryColumn = { IsAlwaysSelect = true }
			};
			esq.Filters.Add(esq.CreateFilterWithParameters(
				FilterComparisonType.Equal, "Domain", senderDomain.Domain));
			var providerFilter = new EntitySchemaQueryFilterCollection(esq,
				LogicalOperationStrict.Or) {
				esq.CreateFilterWithParameters(FilterComparisonType.Equal, "SenderProvider", providerName),
				esq.CreateFilterWithParameters(FilterComparisonType.Equal, "SenderProvider", string.Empty)
			};
			esq.Filters.Add(providerFilter);
			esq.AddColumn("ModifiedOn").OrderByDesc();
			Entity found = esq.GetEntityCollection(UserConnection).FirstOrDefault();
			if (found != null) {
				entity.FetchFromDB(found.PrimaryColumnValue);
				domainId = entity.PrimaryColumnValue;
			} else {
				domainId = Guid.NewGuid();
				entity.SetColumnValue("Id", domainId);
				entity.SetColumnValue("Domain", senderDomain.Domain);
			}
			entity.SetColumnValue("SenderProvider", providerName);
			entity.SetColumnValue("Status", senderDomain.Status);
			entity.SetColumnValue("SenderDomainsInfoId", senderDomain.SenderDomainsInfoId);
			entity.SetColumnValue("ModifiedOn", DateTime.UtcNow);
			Guid senderDomainStatus = senderDomain.Status == ActiveStatus
				? SenderDomainStatusValues.Validated
				: SenderDomainStatusValues.Unvalidated;
			entity.SetColumnValue("SenderDomainStatus", senderDomainStatus);
			entity.Save();
			return new SenderDomainModel {
				Id = domainId,
				Domain = entity.GetTypedColumnValue<string>("Domain"),
				ProviderName = providerName
			};
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Retrieves the sender domain information by its unique identifier.
		/// </summary>
		/// <param name="domainId">The unique identifier of the domain.</param>
		/// <returns>A <see cref="SenderDomainModel"/> object containing the domain information.</returns>
		public SenderDomainModel GetDomainById(Guid domainId) {
			var query = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "SenderDomain") {
				PrimaryQueryColumn = { IsAlwaysSelect = true }
			};
			query.AddColumn("Domain");
			query.AddColumn("SenderProvider");
			query.AddColumn("SenderDomainStatus");
			query.Filters.Add(query.CreateFilterWithParameters(FilterComparisonType.Equal, "Id", domainId));
			try {
				var entity = query.GetEntityCollection(UserConnection).Single();
				return new SenderDomainModel {
					Id = entity.PrimaryColumnValue,
					Domain = entity.GetTypedColumnValue<string>("Domain"),
					ProviderName = entity.GetTypedColumnValue<string>("SenderProvider"),
					StatusId = entity.GetTypedColumnValue<Guid>("SenderDomainStatusId")
				};
			} catch (Exception ex) {
				Logger.Error("Error while retrieving sender domain by id", ex);
				return null;
			}
		}

		/// <inheritdoc cref="ISenderDomainRepository.AddDomain"/>
		public SenderDomainModel AddDomain(AddSenderDomainsInfoRequest request, CloudSenderDomain domainInfo) {
			SetProviderNameIfNeeded(providerName => request.ProviderName = providerName, request.ProviderName);
			string cacheKey = GetCacheKey(request.ApiKey, request.ProviderName);
			CloudSenderDomainsInfo storedDomainsInfo = GetStoredDomainsInfo(cacheKey).FirstOrDefault();
			Guid senderDomainsInfoId = storedDomainsInfo?.Id ?? CreateEmptySenderDomainsInfo(cacheKey);
			domainInfo.SenderDomainsInfoId = senderDomainsInfoId;
			var senderDomain = UpsertSenderDomain(domainInfo, request.ProviderName);
			UpsertDomainRecordDetails(senderDomain.Id, domainInfo);
			return senderDomain;
		}

		/// <inheritdoc cref="ISenderDomainRepository.AddRequestedDomain"/>
		public SenderDomainModel AddRequestedDomain(string domain, string providerName) {
			string cacheKey = GetCacheKey(_serviceApi.ApiKey, providerName);
			CloudSenderDomainsInfo storedDomainsInfo = GetStoredDomainsInfo(cacheKey).FirstOrDefault();
			Guid senderDomainsInfoId = storedDomainsInfo?.Id ?? CreateEmptySenderDomainsInfo(cacheKey);
			var entity = UserConnection.EntitySchemaManager.GetInstanceByName("SenderDomain")
				.CreateEntity(UserConnection);
			var domainId = Guid.NewGuid();
			entity.SetColumnValue("Id", domainId);
			entity.SetColumnValue("Domain", domain);
			entity.SetColumnValue("SenderProvider", providerName);
			entity.SetColumnValue("SenderDomainStatus", SenderDomainStatusValues.Requested);
			entity.SetColumnValue("SenderDomainsInfo", senderDomainsInfoId); // Remove after cache decommission
			entity.Save();
			return new SenderDomainModel {
				Id = domainId,
				Domain = domain,
				ProviderName = providerName
			};
		}

		/// <inheritdoc cref="ISenderDomainCacheService.GetSenderDomainsInfo"/>
		public CloudSenderDomainsInfo GetSenderDomainsInfo(SenderDomainsInfoRequest request) {
			SetProviderNameIfNeeded(providerName => request.ProviderName = providerName, request.ProviderName);
			return GetSenderDomainsInfoByRequest(request, false);
		}

		/// <inheritdoc cref="ISenderDomainCacheService.RefreshSenderDomains"/>
		public CloudSenderDomainsInfo RefreshSenderDomains(SenderDomainsInfoRequest request) {
			SetProviderNameIfNeeded(providerName => request.ProviderName = providerName, request.ProviderName);
			return GetSenderDomainsInfoByRequest(request, true);
		}

		/// <inheritdoc cref="ISenderDomainRepository.DeleteSenderDomain"/>
		public bool DeleteSenderDomain(DeleteSenderDomainRequest request) {
			try {
				EntitySchema schema = UserConnection.EntitySchemaManager.GetInstanceByName("SenderDomain");
				Entity domain = schema.CreateEntity(UserConnection);
				bool fetchResult = domain.FetchFromDB(request.SenderDomainId);
				if (!fetchResult) {
					return false;
				}
				var domainDetails = GetSenderDomainRecordDetails(request.SenderDomainId).ToList();
				foreach (var detail in domainDetails) {
					detail.Delete();
				}
				domain.Delete();
				return true;
			} catch (Exception ex) {
				Logger.Error($"Error while sender domain delete {request.SenderDomainId}", ex);
				return false;
			}
		}

		/// <summary>
		/// Validates Sender domain by id.
		/// </summary>
		/// <param name="domainId"></param>
		/// <returns>Validation result.</returns>
		public ValidateDomainResponse ValidateSenderDomain(Guid domainId) {
			EntitySchema schema = UserConnection.EntitySchemaManager.GetInstanceByName("SenderDomain");
			Entity entity = schema.CreateEntity(UserConnection);
			bool fetchResult = entity.FetchFromDB(domainId);
			if (!fetchResult) {
				return new ValidateDomainResponse {
					IsSuccess = false,
					IsValid = false
				};
			}
			var request = new CloudValidateDomainRequest {
				ApiKey = _serviceApi.ApiKey,
				Domain = entity.GetTypedColumnValue<string>("Domain"),
				ProviderName = entity.GetTypedColumnValue<string>("SenderProvider")
			};
			CloudDomainValidationResponse validationResponse = _serviceApi.ValidateSenderDomain(request);
			if (validationResponse.IsSuccess) {
				UpdateSenderDomainValidationStatus(domainId, validationResponse);
			}
			return new ValidateDomainResponse {
				IsSuccess = validationResponse.IsSuccess,
				IsValid = validationResponse.IsValid
			};
		}

		#endregion

	}

	public static class SenderDomainStatusValues
	{
		public static readonly Guid Validated = new Guid("7f64a233-ac37-4c21-86b8-07ad612f2391");
		public static readonly Guid Unvalidated = new Guid("61f2c77b-7d50-41d1-b3f4-fd3f212721c0");
		public static readonly Guid Requested = new Guid("828c5d74-fd3d-4fb9-9408-9624aeadf649");
	}

	public static class SenderDomainRecordStatusValues
	{
		public static readonly Guid Verified = new Guid("87e2e931-1602-4ca9-9ad0-5bbd44ca1cd8");
		public static readonly Guid Unverified = new Guid("c7f6b9a6-bd0d-4b46-a523-192b0dcaf1e1");
	}

	public static class SenderDomainRecordCategoryValues
	{
		public static readonly Guid SPF = new Guid("1acba6af-5d6c-4d5e-80c3-ae3ae36662ca");
		public static readonly Guid MX = new Guid("30686bed-7260-49d3-89cc-a8ae6e657bc0");
		public static readonly Guid DKIM = new Guid("4e032252-2189-4b5d-b25f-b2b3d5aa5b08");
		public static readonly Guid CNAME = new Guid("dc45ce98-6b89-4243-aa96-b5c225cd23e3");

		public static readonly Dictionary<string, Guid> CategoryNameToIdMap = new Dictionary<string, Guid> {
			{ "SPF", SPF },
			{ "MX", MX },
			{ "DKIM", DKIM },
			{ "CNAME", CNAME }
		};

		public static readonly Dictionary<Guid, string> CategoryIdToNameMap =
			CategoryNameToIdMap.ToDictionary(kv => kv.Value, kv => kv.Key);
	}

	#endregion

	/// <summary>
	/// Represents the model for sender domain information, including its unique identifier, domain name, and provider name.
	/// </summary>
	public class SenderDomainModel
	{
		/// <summary>
		/// Gets or sets the unique identifier of the sender domain.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the sender domain.
		/// </summary>
		public string Domain { get; set; }

		/// <summary>
		/// Gets or sets the name of the provider associated with the sender domain.
		/// </summary>
		public string ProviderName { get; set; }

		/// <summary>
		/// Gets or sets the status of the domain.
		/// </summary>
		public Guid StatusId { get; set; }
	}

	/// <summary>
	/// Interface for managing and retrieving sender domain information.
	/// </summary>
	public interface ISenderDomainRepository
	{
		/// <summary>
		/// Retrieves the sender domain information by its unique identifier.
		/// </summary>
		/// <param name="domainId">The unique identifier of the domain.</param>
		/// <returns>A <see cref="SenderDomainModel"/> object containing the domain information.</returns>
		SenderDomainModel GetDomainById(Guid domainId);

		/// <summary>
		/// Adds the sender domain to the cache.
		/// </summary>
		/// <param name="addSenderDomainsInfoRequest">Add sender domains information request.</param>
		/// <param name="domainInfo">Domain info returned from CES API</param>
		SenderDomainModel AddDomain(AddSenderDomainsInfoRequest addSenderDomainsInfoRequest, CloudSenderDomain domainInfo);

		/// <summary>
		/// Adds a new requested sender domain with the specified domain name and provider name.
		/// </summary>
		/// <param name="domain">The domain name to be added.</param>
		/// <param name="providerName">The name of the provider associated with the domain.</param>
		/// <returns>A <see cref="SenderDomainModel"/> containing the details of the newly added requested sender domain.</returns>
		SenderDomainModel AddRequestedDomain(string domain, string providerName);

		/// <summary>
		/// Refreshes sender domains info by the provider name.
		/// Updates cache and returns fresh data.
		/// </summary>
		/// <param name="request">The sender domains information request.</param>
		/// <returns>Sender domains info.</returns>
		CloudSenderDomainsInfo RefreshSenderDomains(SenderDomainsInfoRequest request);

		/// <summary>
		/// Deletes the specified sender domain from the system.
		/// </summary>
		/// <param name="request">An object containing the details of the sender domain to delete. Cannot be null.</param>
		/// <returns>A response object indicating the result of the delete operation. Is false in case of errors.</returns>
		bool DeleteSenderDomain(DeleteSenderDomainRequest request);

	}

	/// <summary>
	/// Provides a data caching service for the sender's domain.
	/// </summary>
	public interface ISenderDomainCacheService
	{
		/// <summary>
		/// Returns sender domains info by the provider name.
		/// </summary>
		/// <param name="senderDomainsInfoRequest">The sender domains information request.</param>
		/// <returns>Sender domains info.</returns>
		CloudSenderDomainsInfo GetSenderDomainsInfo(SenderDomainsInfoRequest senderDomainsInfoRequest);

		/// <summary>
		/// Refreshes sender domains info by the provider name.
		/// Updates cache and returns fresh data.
		/// </summary>
		/// <param name="request">The sender domains information request.</param>
		/// <returns>Sender domains info.</returns>
		CloudSenderDomainsInfo RefreshSenderDomains(SenderDomainsInfoRequest request);
	}

	/// <summary>
	/// Interface for Sender domain validation service.
	/// </summary>
	public interface ISenderDomainValidationService
	{
		/// <summary>
		/// Validates Sender domain by id.
		/// </summary>
		/// <param name="domainId"></param>
		/// <returns>Validation result.</returns>
		ValidateDomainResponse ValidateSenderDomain(Guid domainId);

	}
}

