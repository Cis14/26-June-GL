namespace Terrasoft.Configuration.Workplace
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Common;
	using Section;
	using Core;
	using Core.Factories;
	using WorkplaceSectionAccess;
	using WorkplaceApi;

	#region Class: WorkplaceManager

	[DefaultBinding(typeof(IWorkplaceManager))]
	public class WorkplaceManager : IWorkplaceManager
	{

		#region Fields: Private

		/// <summary>
		/// <see cref="IWorkplaceRepository"/> implementation instance.
		/// </summary>
		private readonly IWorkplaceRepository _workplaceRepository;

		/// <summary>
		/// <see cref="ISectionRepository"/> implementation instance.
		/// </summary>
		private readonly ISectionRepository _sectionRepository;

		/// <summary>
		/// <see cref="IResourceStorage"/> implementation instance.
		/// </summary>
		private readonly IResourceStorage _resourceStorage;

		/// <summary>
		/// Current user identifier.
		/// </summary>
		private readonly Guid _currentUserId;

		private readonly IWorkplaceSectionAccessManager _workplaceSectionAccessManager;
		private readonly ClientUnitSchemaManager _clientUnitSchemaManager;

		#endregion

		#region Constructors: Public

		public WorkplaceManager(UserConnection uc) {
			_workplaceRepository = ClassFactory.Get<IWorkplaceRepository>(new ConstructorArgument("uc", uc));
			_sectionRepository = ClassFactory.Get<ISectionRepository>("General", new ConstructorArgument("uc", uc));
			_workplaceSectionAccessManager = ClassFactory.Get<IWorkplaceSectionAccessManager>(new ConstructorArgument("userConnection", uc));
			_resourceStorage = uc.ResourceStorage;
			_currentUserId = uc.CurrentUser.Id;
			_clientUnitSchemaManager = uc.ClientUnitSchemaManager;
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Creates workplace exception message.
		/// </summary>
		/// <param name="exceptionMessageKey">Localizable string name.</param>
		/// <returns>Workplace exception message.</returns>
		private string GetWorkplaceExceptionMessage(string exceptionMessageKey) {
			return new LocalizableString(_resourceStorage, "SectionExceptionResources",
				$"LocalizableStrings.{exceptionMessageKey}.Value").ToString();
		}

		private WorkplaceDto ConvertToWorkplaceDto(Workplace source, IEnumerable<Section> sections) {
			var dto = new WorkplaceDto
			{
				Id = source.Id,
				Name = source.Name,
				Position = source.Position,
				Type = source.Type,
				IsPersonal = source.IsPersonal,
				LoaderId = source.LoaderId,
				LoaderName = source.LoaderName,
				ClientApplicationTypeId = source.ClientApplicationTypeId,
				HomePageUId = source.HomePageUId,
				Sections = sections.Select(ConvertToSectionDto).ToList()
			};
			return dto;
		}

		private SectionDto ConvertToSectionDto(Section source)
		{
			return new SectionDto
			{
				Id = source.Id,
				Caption = source.Caption,
				Code = source.Code,
				EntityUId = source.EntityUId,
				IconBackground = source.IconBackground,
				Image32Id = source.Image32Id,
				IsModule = source.IsModule,
				ModuleSchemaUId = source.ModuleSchemaUId,
				SchemaName = source.SchemaName,
				SysModuleVisaEntityUId = source.SysModuleVisaEntityUId,
				TypeColumnName = source.TypeColumnName,
				SysModuleEntityId = source.SysModuleEntityId,
				Type = source.Type,
			};
		}

		private WorkplacesInfoDto GetAllowedWorkplacesInternal(AvailableWorkplacesInfoRequest request)
		{
			WorkplacesInfo workplacesInfo = _workplaceSectionAccessManager.GetAllowedWorkplaces(
				new AllowedWorkplacesInfoRequest {
					UserId = request.UserId,
					ApplicationClientTypeId = request.ApplicationClientTypeId
				});
			List<WorkplaceDto> workplaces = workplacesInfo.AllowedWorkplaceStructureInfos
				.Select(info => ConvertToWorkplaceDto(info.Workplace, info.AllowedSections)).ToList();
			return new WorkplacesInfoDto {
				Workplaces = workplaces,
				LoadingResult = workplacesInfo.LoadingResult
			};
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Returns new workplace position.
		/// </summary>
		/// <param name="type">New workplace type.</param>
		/// <returns>New workplace position.</returns>
		protected int GetNewWorkplacePosition(WorkplaceType type) {
			var workplacePositions = GetWorkplacesByType(type).Select(w => w.Position);
			if (!workplacePositions.Any()) {
				return 0;
			}
			return workplacePositions.Max() + 1;
		}

		/// <summary>
		/// Retruns workplaces, which  position must be changed.
		/// </summary>
		/// <param name="workplace"><see cref="Workplace"/> that changed position instance.</param>
		/// <param name="position"><paramref name="workplace"/> new position.</param>
		/// <returns><see cref="Workplace"/> collection.</returns>
		protected IEnumerable<Workplace> GetWorkplacesToChange(Workplace workplace, int position) {
			var workplaces = GetWorkplacesByType(workplace.Type);
			IEnumerable<Workplace> result;
			if (workplace.Position > position) {
				result = workplaces.Where(w => w.Position >= position && w.Id != workplace.Id);
			} else {
				result = workplaces.Where(w => w.Position <= position && w.Id != workplace.Id);
			}
			return result.OrderBy(w => w.Position);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IWorkplaceManager.GetCurrentUserWorkplaces"/>
		public IEnumerable<Workplace> GetCurrentUserWorkplaces(Guid applicationClientTypeId) {
			var allAllowedWorkplaces = _workplaceSectionAccessManager.GetAllAllowedWorkplacesIds();
			return _workplaceRepository.GetAll()
				.Where(workplace => workplace.ClientApplicationTypeId.Equals(applicationClientTypeId)
					&& workplace.GetIsAllowedForUser(_currentUserId)
					&& allAllowedWorkplaces.Contains(workplace.Id)).OrderBy(workplace => workplace.Position);
		}

		/// <inheritdoc cref="IWorkplaceManager.GetAvailableWorkplaces"/>
		public IEnumerable<WorkplaceDto> GetAvailableWorkplaces(Guid userId, Guid applicationClientTypeId) {
			userId.CheckArgumentEmpty(nameof(userId));
			applicationClientTypeId.CheckArgumentEmpty(nameof(applicationClientTypeId));
			return GetAllowedWorkplacesInternal(new AvailableWorkplacesInfoRequest {
				UserId = userId,
				ApplicationClientTypeId = applicationClientTypeId
			}).Workplaces.ToList();
		}

		/// <inheritdoc cref="IWorkplaceManager.GetWorkplacesByType"/>
		public IEnumerable<Workplace> GetWorkplacesByType(WorkplaceType type) {
			var result = _workplaceRepository.GetAll().Where(w => w.Type == type);
			return result;
		}

		/// <inheritdoc cref="IWorkplaceManager.CreateWorkplace"/>
		public Workplace CreateWorkplace(CreateWorkplaceParameters parameters) {
			var position = GetNewWorkplacePosition(parameters.Type);
			var workplace = new Workplace(Guid.NewGuid(), parameters.Name, parameters.Type) {
				Position = position,
				HomePageUId = parameters.HomePageUId
			};
			if (parameters.ClientApplicationTypeId != null) {
				workplace.ClientApplicationTypeId = parameters.ClientApplicationTypeId.Value;
			}
			if (_workplaceRepository.SaveWorkplace(workplace)) {
				return workplace;
			}
			var message = GetWorkplaceExceptionMessage("WorkplaceCreateFailed");
			throw new WorkplaceCreateFailedException(message);
		}

		/// <inheritdoc cref="IWorkplaceManager.ChangeName"/>
		public void ChangeName(Guid workplaceId, string name) {
			var workplace = _workplaceRepository.Get(workplaceId);
			workplace.SetName(name);
			_workplaceRepository.SaveWorkplace(workplace);
		}

		/// <inheritdoc cref="IWorkplaceManager.ChangePosition"/>
		public void ChangePosition(Guid workplaceId, int position) {
			var workplace = _workplaceRepository.Get(workplaceId);
			if (workplace.Position == position) {
				return;
			}
			var workplacesToChange = GetWorkplacesToChange(workplace, position);
			var index = workplace.Position > position ? position + 1 : 0;
			foreach (var w in workplacesToChange) {
				if (w.Position != index) {
					w.Position = index;
					_workplaceRepository.SaveWorkplace(w);
				}
				index++;
			}
			workplace.Position = position;
			_workplaceRepository.SaveWorkplace(workplace);
		}

		/// <inheritdoc cref="IWorkplaceManager.DeleteWorkplace"/>
		public void DeleteWorkplace(Guid workplaceId) {
			_workplaceRepository.DeleteWorkplace(workplaceId);
		}

		/// <inheritdoc cref="IWorkplaceManager.AddSectionToWorkplace"/>
		public void AddSectionToWorkplace(Guid workplaceId, Guid sectionId) {
			var workplace = _workplaceRepository.Get(workplaceId);
			var section = _sectionRepository.Get(sectionId);
			workplace.AddSection(section.Id);
			_workplaceRepository.SaveWorkplace(workplace);
			_sectionRepository.ClearCache();
		}

		/// <inheritdoc cref="IWorkplaceManager.RemoveSectionFromWorkplace"/>
		public void RemoveSectionFromWorkplace(Guid workplaceId, Guid sectionId) {
			var workplace = _workplaceRepository.Get(workplaceId);
			workplace.RemoveSection(sectionId);
			_workplaceRepository.SaveWorkplace(workplace);
			_sectionRepository.ClearCache();
		}

		/// <inheritdoc cref="IWorkplaceManager.ReloadWorkplaces"/>
		public void ReloadWorkplaces() {
			_workplaceRepository.ClearCache();
			_sectionRepository.ClearCache();
		}

		/// <inheritdoc cref="IWorkplaceManager.GetAvailableWorkplacesInfo(Guid, Guid)"/>
		public WorkplacesInfoDto GetAvailableWorkplacesInfo(Guid userId, Guid applicationClientTypeId) {
			userId.CheckArgumentEmpty(nameof(userId));
			applicationClientTypeId.CheckArgumentEmpty(nameof(applicationClientTypeId));
			return GetAllowedWorkplacesInternal(new AvailableWorkplacesInfoRequest {
				UserId = userId,
				ApplicationClientTypeId = applicationClientTypeId
			});
		}

		/// <inheritdoc cref="IWorkplaceManager.GetIsSchemaNameExistsInWorkplaceStructure"/>
		public bool GetIsSchemaNameExistsInWorkplaceStructure(string schemaName) {
			schemaName.CheckArgumentNullOrEmpty(nameof(schemaName));
			WorkplacesInfo workplacesInfo = _workplaceSectionAccessManager.GetWorkplacesInfoForUser(_currentUserId);
			ISchemaManagerItem<ClientUnitSchema> clientUnitSchema = _clientUnitSchemaManager.FindItemByName(schemaName);
			if (clientUnitSchema == null) {
				return false;
			}
			AllowedWorkplaceStructureInfo[] allowedWorkplaceStructureInfos =
				workplacesInfo.AllowedWorkplaceStructureInfos.ToArray();
			return allowedWorkplaceStructureInfos.Any(info =>
						info.AllowedSections.Any(section => section.SchemaName == schemaName))
					|| allowedWorkplaceStructureInfos.Any(info => info.Workplace.HomePageUId == clientUnitSchema.UId);
		}

		/// <inheritdoc cref="IWorkplaceManager.GetAvailableWorkplacesInfo(AvailableWorkplacesInfoRequest)"/>
		public WorkplacesInfoDto GetAvailableWorkplacesInfo(AvailableWorkplacesInfoRequest request) {
			request.CheckArgumentNull(nameof(request));
			return GetAllowedWorkplacesInternal(request);
		}

		/// <inheritdoc cref="IWorkplaceManager.ReloadWorkplacesByApplicationClientType"/>
		public void ReloadWorkplacesByApplicationClientType(Guid applicationClientTypeId) {
			applicationClientTypeId.CheckArgumentEmpty(nameof(applicationClientTypeId));
			_workplaceRepository.ClearCacheByApplicationClientType(applicationClientTypeId);
			_sectionRepository.ClearCache();
		}

		/// <inheritdoc cref="IWorkplaceManager.GetWorkplaceSectionCount"/>
		public int GetWorkplaceSectionCount(Guid workplaceId) {
			workplaceId.CheckArgumentEmpty(nameof(workplaceId));
			var workplace = _workplaceRepository.Find(workplaceId);
			return workplace == null ? 0 : workplace.GetSectionIds().ToArray().Length;
		}

		/// <inheritdoc cref="IWorkplaceManager.RemoveAllSectionsFromWorkplace"/>
		public void RemoveAllSectionsFromWorkplace(Guid workplaceId) {
			workplaceId.CheckArgumentEmpty(nameof(workplaceId));
			var workplace = _workplaceRepository.Get(workplaceId);
			workplace.RemoveAllSections();
			_workplaceRepository.SaveWorkplace(workplace);
			_sectionRepository.RemoveByWorkplaceId(workplaceId);
			_sectionRepository.ClearCache();
		}

		#endregion

	}

	#endregion

}