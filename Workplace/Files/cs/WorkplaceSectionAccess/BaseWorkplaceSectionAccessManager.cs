namespace Terrasoft.Configuration.WorkplaceSectionAccess
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Core;
	using Core.Factories;
	using Common;
	using Section;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Configuration.Domain;
	using Workplace;
	using WorkplaceApi;

	#region Class: BaseWorkplaceSectionAccessManager

	public abstract class BaseWorkplaceSectionAccessManager : IBaseWorkplaceSectionAccessManager
	{

		#region Constants: Private

		private const string OperationCodeAllowedSectionsCount = "AllowedSectionsCount";
		private const string OperationCodeAllowedWorkplaceTypes = "AllowedWorkplaceTypes";

		#endregion

		#region Fields: Private

		private IEnumerable<string> _excludedFromAdditionalUserSections;
		private int _additionalUserSectionsCount;
		private ISectionCacheManager _sectionCacheManager;

		#endregion

		#region Constructors: Public

		protected BaseWorkplaceSectionAccessManager(UserConnection userConnection) {
			UserConnection = userConnection;
			InitAllowedWorkplaceTypesLimits();
			InitExcludedFromAdditionalUserSectionsLimits();
			InitAdditionalUserSectionsCountLimits();
		}

		#endregion

		#region Properties: Private

		private ILicManager _licManager;
		private ILicManager LicManager => _licManager ?? (_licManager = UserConnection.AppConnection.LicManager);

		private SysUserInfo _currentUser;
		private SysUserInfo CurrentUser => _currentUser ?? (_currentUser = UserConnection.CurrentUser);

		private IReadOnlyCollection<WorkplaceType> AllowedWorkplaceTypes { get; set; } =
			new List<WorkplaceType>();

		private IWorkplaceRepository _workplaceRepository;
		private IWorkplaceRepository WorkplaceRepository => _workplaceRepository ?? (_workplaceRepository =
			ClassFactory.Get<IWorkplaceRepository>(
				new ConstructorArgument("uc", UserConnection)));

		private ISectionCacheManager SectionCacheManager => _sectionCacheManager ?? (_sectionCacheManager = ClassFactory.Get<ISectionCacheManager>(
			new ConstructorArgument("sessionCache", UserConnection.SessionCache)));

		#endregion

		#region Properties: Private

		private WorkplaceLoadingResult WorkplaceLoadingResult { get; set; } = WorkplaceLoadingResult.Successful;

		#endregion

		#region Properties: Protected

		protected UserConnection UserConnection { get; }

		#endregion

		#region Methods: Private

		private void InitAdditionalUserSectionsCountLimits() {
			_additionalUserSectionsCount = LicManager.FindOperationLimit(CurrentUser.Id,
				OperationCodeAllowedSectionsCount) ?? 0;
		}

		private void InitExcludedFromAdditionalUserSectionsLimits() {
			_excludedFromAdditionalUserSections = GetExcludedFromAdditionalUserSectionsLimits();
		}

		private static WorkplaceType ConvertToWorkplaceType(int allowedWorkplaceType) {
			var workplaceType = (WorkplaceType)allowedWorkplaceType;
			if (!Enum.IsDefined(typeof(WorkplaceType), workplaceType)) {
				throw new ItemNotFoundException($"Workplace type {workplaceType} from LicManager not found");
			}
			return workplaceType;
		}

		private void InitAllowedWorkplaceTypesLimits() {
			ICollection<string> allowedWorkplaceTypesCodes =
				LicManager.GetOperationValues(UserConnection.CurrentUser.Id, OperationCodeAllowedWorkplaceTypes).ToList();
			AllowedWorkplaceTypes = allowedWorkplaceTypesCodes
				.Select(allowedWorkplaceTypeCode => ConvertToWorkplaceType(int.Parse(allowedWorkplaceTypeCode)))
				.ToList();
		}

		private static IEnumerable<Workplace> SortWorkplaces(IEnumerable<Workplace> workplaces) {
			return workplaces.OrderBy(workplace => workplace.Position);
		}

		private ISectionRepository GetSectionRepository(Guid? applicationClientTypeId = null) {
			string repositoryName = applicationClientTypeId == ClientTypes.BrowserClientTypeId
				? CurrentUser.ConnectionType.ToString()
				: "Mobile";
			return ClassFactory.Get<ISectionRepository>(repositoryName,
				new ConstructorArgument("uc", UserConnection));
		}

		private Dictionary<Guid, List<Section>> GetSectionsInWorkplaces(Guid? applicationClientTypeId = null) {
			if (applicationClientTypeId == null || applicationClientTypeId == Guid.Empty) {
				applicationClientTypeId = ClientTypes.BrowserClientTypeId;
			}
			var sectionRepository = GetSectionRepository(applicationClientTypeId);
			var result = new Dictionary<Guid, List<Section>>();
			IEnumerable<Section> allSections;
			if (SysSettings.GetValue(UserConnection, "EnableCheckSectionPermissions", false)) {
				allSections = sectionRepository.GetAll()
					.Where(section => GetIsSectionAllowed(section.Code));
			} else {
				allSections = sectionRepository.GetAll();
			}
			allSections.ForEach(section => {
				section.WorkplacesIds.ForEach(workplaceId => {
					if (!result.ContainsKey(workplaceId)) {
						result[workplaceId] = new List<Section>();
					}
					result[workplaceId].Add(section);
				});
			});
			return result;
		}

		private bool GetIsSectionAllowed(string sysModuleName) {
			var securityEngine = UserConnection.DBSecurityEngine;
			return securityEngine.GetIsEntitySchemaReadingAllowed(sysModuleName) &&
			       GetAccessFromSspLicense(sysModuleName);
		}

		private bool GetAccessFromSspLicense(string moduleName) {
			return UserConnection.CurrentUser.ConnectionType != UserType.SSP ||
			       UserConnection.DBSecurityEngine.GetIsAvailableOnSsp(moduleName);
		}

		private void FillWorkplaceSections(AllowedWorkplaceStructureInfo allowedWorkplaceStructureInfo,
			List<Section> sections) {
			var allowedSectionsIds = new List<Guid>();
			var allowedSections = new List<Section>();
			if (!GetUserHasLimits()) {
				allowedWorkplaceStructureInfo.AllowedSectionsIds.AddRange(sections.Select(section => section.Id));
				allowedWorkplaceStructureInfo.AllowedSections.AddRange(sections);
				return;
			}
			int sectionCounter = 0;
			foreach (Section section in sections) {
				if (_excludedFromAdditionalUserSections.Contains(section.Code)) {
					allowedSectionsIds.Add(section.Id);
					allowedSections.Add(section);
					continue;
				}

				if (sectionCounter < _additionalUserSectionsCount) {
					allowedSectionsIds.Add(section.Id);
					allowedSections.Add(section);
					sectionCounter++;
				}
			}
			int allowedSectionCount = _excludedFromAdditionalUserSections.Count() + _additionalUserSectionsCount;
			allowedWorkplaceStructureInfo.AllowedSectionsIds.AddRange(allowedSectionsIds
				.Take(allowedSectionCount));
			allowedWorkplaceStructureInfo.AllowedSections.AddRange(allowedSections.Take(allowedSectionCount));
		}

		private bool GetUserHasLimits() {
			return _excludedFromAdditionalUserSections.IsNotNullOrEmpty() ||
			       _additionalUserSectionsCount > 0 ||
			       AllowedWorkplaceTypes.IsNotNullOrEmpty();
		}

		private void DefineWorkplaceLoadingResult(int workplacesBeforeLicensesCount, int allowedWorkplacesCount) {
			bool isUserHasSetUpWorkplaces = workplacesBeforeLicensesCount > 0;
			bool isUserHasLicenseRestrictions = isUserHasSetUpWorkplaces && allowedWorkplacesCount == 0;
			bool isUserSelfService = LicManager.GetCurrentUserHasOperationLicense("SelfServicePortalLogin");
			if (isUserHasLicenseRestrictions) {
				WorkplaceLoadingResult = isUserSelfService
					? WorkplaceLoadingResult.WorkplacesNotAvailableForSelfServiceUserByLicenses
					: WorkplaceLoadingResult.WorkplacesNotAvailableByLicenses;
			} else if (!isUserHasSetUpWorkplaces) {
				WorkplaceLoadingResult = isUserSelfService
					? WorkplaceLoadingResult.WorkplacesNotSetupForSelfServiceUser
					: WorkplaceLoadingResult.WorkplacesNotSetup;
			}
		}

		#endregion

		#region Methods: Protected

		protected virtual string[] GetExcludedFromAdditionalUserSectionsLimits() {
			string[] defaultLimitation =
				GetLicOperationValue("ExcludedFromSectionsCountLimitation")?.ToArray();
			return defaultLimitation;
		}

		protected IEnumerable<string> GetLicOperationValue(string operationCode) {
			return LicManager.GetOperationValues(CurrentUser.Id, operationCode);
		}

		protected IEnumerable<Workplace> GetAllAllowedWorkplacesInternal(Guid userId, Guid? applicationClientTypeId = null) {
			ICollection<Workplace> allWorkplaces = WorkplaceRepository.GetAllByApplicationClientType(
					applicationClientTypeId ?? ClientTypes.BrowserClientTypeId)
				.Where(workplace => workplace.GetIsAllowedForUser(userId)).ToList();
			int workplacesBeforeLicensesCount = allWorkplaces.ToList().Count;
			if (!GetUserHasLimits()) {
				if (workplacesBeforeLicensesCount == 0) {
					WorkplaceLoadingResult = WorkplaceLoadingResult.WorkplacesNotSetup;
				}
				return SortWorkplaces(allWorkplaces);
			}
			List<Workplace> allowedWorkplaces = SortWorkplaces(allWorkplaces
				.Where(workplace => AllowedWorkplaceTypes.Contains(workplace.Type))).Take(1).ToList();
			DefineWorkplaceLoadingResult(workplacesBeforeLicensesCount, allowedWorkplaces.ToList().Count);
			return allowedWorkplaces;
		}

		protected IEnumerable<AllowedWorkplaceStructureInfo> GetAllAllowedWorkplaceStructuresInternal(Guid userId,
				Guid? applicationClientTypeId = null) {
			IEnumerable<Workplace> allowedWorkplaces = GetAllAllowedWorkplacesInternal(userId, applicationClientTypeId);
			Dictionary<Guid, List<Section>> sections = GetSectionsInWorkplaces(applicationClientTypeId);
			var allowedWorkplacesStructures = new List<AllowedWorkplaceStructureInfo>();
			foreach (var workplace in allowedWorkplaces) {
				Guid workplaceId = workplace.Id;
				var allowedWorkplaceStructureInfo = new AllowedWorkplaceStructureInfo() {
					Workplace = workplace
				};
				if (sections.TryGetValue(workplaceId, out var workplaceSections)) {
					List<Guid> sortedSectionIds = workplace.GetSectionIds().ToList();
					List<Section> sortedSections = workplaceSections
						.OrderBy(section => sortedSectionIds.IndexOf(section.Id)).ToList();
					FillWorkplaceSections(allowedWorkplaceStructureInfo, sortedSections);
				}
				allowedWorkplacesStructures.Add(allowedWorkplaceStructureInfo);
			}
			return allowedWorkplacesStructures;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IBaseWorkplaceSectionAccessManager.GetAllowedWorkplacesForUser"/>
		public IEnumerable<AllowedWorkplaceStructureInfo> GetAllowedWorkplacesForUser(Guid userId) {
			return GetAllAllowedWorkplaceStructuresInternal(userId);
		}

		/// <inheritdoc cref="IBaseWorkplaceSectionAccessManager.ClearCache"/>
		public void ClearCache() {
			WorkplaceRepository.ClearCache();
			SectionCacheManager.Clear();
		}

		/// <inheritdoc cref="IWorkplaceSectionAccessManager.GetWorkplacesInfoForUser"/>
		public WorkplacesInfo GetWorkplacesInfoForUser(Guid userId) {
			userId.CheckArgumentEmpty(nameof(userId));
			return GetAllowedWorkplaces(new AllowedWorkplacesInfoRequest {
				UserId = userId,
				ApplicationClientTypeId = ClientTypes.BrowserClientTypeId
			});
		}

		/// <inheritdoc cref="IWorkplaceSectionAccessManager.GetAllowedWorkplaces"/>
		public WorkplacesInfo GetAllowedWorkplaces(AllowedWorkplacesInfoRequest request) {
			request.CheckArgumentNull(nameof(request));
			return new WorkplacesInfo {
				AllowedWorkplaceStructureInfos = GetAllAllowedWorkplaceStructuresInternal(request.UserId,
					request.ApplicationClientTypeId),
				LoadingResult = WorkplaceLoadingResult
			};
		}

		#endregion

	}

	#endregion

}