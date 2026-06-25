namespace Terrasoft.Configuration.Section
{
    using global::Common.Logging;
    using System;
	using System.Collections.Generic;
	using System.Data;
    using System.Diagnostics;
    using System.Linq;
	using Common;
	using Core;
	using Core.DB;
	using Core.Entities;
	using Core.Factories;
	using Terrasoft.Configuration.Domain;

	#region Class SectionRepository

	[DefaultBinding(typeof(ISectionRepository), Name = "General")]
	public class SectionRepository : BaseSectionRepository
	{

		#region Fields: Private

		private readonly string[] _sectionRelatedEntitySufixes = { "File", "Folder", "InFolder", "Tag", "InTag" };

		/// <summary>
		/// <see cref="ILog"/> implementation instance.
		/// </summary>
		private static readonly ILog Log = LogManager.GetLogger("Workplace");

		/// <summary>
		/// Section cache manager service.
		/// </summary>
		private readonly ISectionCacheManager _sectionCacheManager;

		#endregion

		#region Fields: Protected

		/// <summary>
		/// <see cref="UserConnection"/> instance.
		/// </summary>
		protected readonly UserConnection UserConnection;

		/// <summary>
		/// <see cref="EntitySchemaManager"/> instance.
		/// </summary>
		protected readonly EntitySchemaManager EntitySchemaManager;

		/// <summary>
		/// <see cref="IResourceStorage"/> implementation instance.
		/// </summary>
		protected readonly IResourceStorage ResourceStorage;

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Section schema unique identifier column name.
		/// </summary>
		protected virtual string SectionSchemaUIdColumnName => "SectionSchemaUId";

		/// <summary>
		/// Cache key scope for sections - Browser platform GUID.
		/// </summary>
		protected virtual string ScopeKey => ClientTypes.BrowserClientTypeId.ToString();

		#endregion

		#region Constructors: Public

		public SectionRepository(UserConnection uc) {
			UserConnection = uc;
			EntitySchemaManager = uc.EntitySchemaManager;
			ResourceStorage = uc.ResourceStorage;
			_sectionCacheManager = ClassFactory.Get<ISectionCacheManager>(new ConstructorArgument("sessionCache", uc.SessionCache));
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Creates new <see cref="Section"/> instance, using information from <paramref name="dataReader"/>.
		/// </summary>
		/// <param name="dataReader"><see cref="IDataReader"/> implementation instance.</param>
		/// <param name="sectionWorkplaceIds">Sections workplaces identifiers collection.</param>
		/// <returns><see cref="Section"/> instance.</returns>
		private Section CreateSectionInstance(IDataReader dataReader, Dictionary<Guid, List<Guid>> sectionWorkplaceIds) {
			Guid sectionId = dataReader.GetColumnValue<Guid>("Id");
			int type = dataReader.GetColumnValue<int>("Type");
			string caption = dataReader.GetColumnValue<string>("Caption");
			string schemaName = dataReader.GetColumnValue<string>("SectionSchema");
			string moduleName = dataReader.GetColumnValue<string>("SectionModuleSchema");
			bool isModule = string.IsNullOrEmpty(schemaName) && !string.IsNullOrEmpty(moduleName);
			string sectionSchemaName = isModule ? moduleName : schemaName;
			string typeColumnName = GetTypeColumnName(dataReader);
			string iconBackground = dataReader.GetColumnValue<string>("IconBackground");
			Guid sysModuleEntityId = dataReader.GetColumnValue<Guid>("SysModuleEntityId");
			Guid entityUId = dataReader.GetColumnValue<Guid>("EntityUId");
			Guid sysModuleVisaEntityUId = dataReader.GetColumnValue<Guid>("VisaSchemaUId");
			Guid image32Id = dataReader.GetColumnValue<Guid>("Image32Id");
			Guid moduleSchemaUId = dataReader.GetColumnValue<Guid>("ModuleSchemaUId");
			var section = new Section(sectionId, sysModuleEntityId, (SectionType)type) {
				Caption = caption,
				Code = dataReader.GetColumnValue<string>("Code"),
				SchemaName = sectionSchemaName,
				EntityUId = entityUId,
				TypeColumnName = typeColumnName,
				SysModuleVisaEntityUId = sysModuleVisaEntityUId,
				IconBackground = iconBackground,
				Image32Id = image32Id,
				IsModule = isModule,
				ModuleSchemaUId = moduleSchemaUId,
			};
			if (sectionWorkplaceIds.ContainsKey(sectionId)){
				section.AddSectionInWorkplaceRange(sectionWorkplaceIds[sectionId]);
			}
			return section;
		}

		/// <summary>
		/// Gets type column name.
		/// </summary>
		/// <param name="dataReader"><see cref="IDataReader"/> implementation instance.</param>
		/// <returns>Type column name.</returns>
		private string GetTypeColumnName(IDataReader dataReader) {
			return dataReader.GetColumnValue<string>("Attribute");
		}

		/// <summary>
		/// Loads workplaces info.
		/// </summary>
		/// <returns>Sections workplaces identifiers collection.</returns>
		private Dictionary<Guid, List<Guid>> LoadSectionWorkplaceIds() {
			var select = new Select(UserConnection)
				.Column("smiw", "SysModuleId")
				.Column("smiw", "SysWorkplaceId")
				.From("SysModuleInWorkplace").As("smiw")
				.OrderByAsc("smiw", "Position") as Select;
			Dictionary<Guid, List<Guid>> result = new Dictionary<Guid, List<Guid>>();
			using (DBExecutor dbExecutor = UserConnection.EnsureDBConnection()) {
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()){
						Guid sectionId = dataReader.GetColumnValue<Guid>("SysModuleId");
						if (!result.ContainsKey(sectionId)){
							result[sectionId] = new List<Guid>();
						}
						result[sectionId].AddIfNotExists(dataReader.GetColumnValue<Guid>("SysWorkplaceId"));
					}
				}
			}
			return result;
		}

		/// <summary>
		/// When <paramref name="item"/> is not null, adds <paramref name="item"/> unique identifier to <paramref name="list"/>.
		/// </summary>
		/// <param name="list"><see cref="List{Guid}"/> instance.</param>
		/// <param name="item"><see cref="ISchemaManagerItem"/> instance.</param>
		private void AddUIdIfNotNull(List<Guid> list, ISchemaManagerItem item) {
			if (item != null) {
				list.AddIfNotExists(item.UId);
			}
		}

		/// <summary>
		/// Returns section required entities unique identifiers list.
		/// </summary>
		/// <param name="sectionMainEntity">Section main entity <see cref="ISchemaManagerItem"/> instance.</param>
		/// <returns>Section required entities unique identifiers list.</returns>
		private IEnumerable<Guid> GetSectionRequiredEntityIds(ISchemaManagerItem sectionMainEntity) {
			var result = new List<Guid>();
			result.AddIfNotExists(sectionMainEntity.UId);
			foreach (var entityNameSuffix in _sectionRelatedEntitySufixes) {
				AddUIdIfNotNull(result, EntitySchemaManager.FindItemByName(sectionMainEntity.Name + entityNameSuffix));
			}
			return result;
		}

		/// <summary>
		/// Creates section not found exception message.
		/// </summary>
		/// <param name="sectionId"><see cref="Section"/> unique identifier.</param>
		/// <returns>Section not found exception message.</returns>
		private string GetItemNotFoundMessage(Guid sectionId) {
			var messageTpl = new LocalizableString(ResourceStorage, "SectionExceptionResources",
					"LocalizableStrings.SectionNotFoundByIdTpl.Value").ToString();
			return string.Format(messageTpl, sectionId.ToString());
		}

		private void RemoveByWorkplaceIdInternal(Guid workplaceId) {
			new Delete(UserConnection)
				.From("SysModuleInWorkplace")
				.Where("SysWorkplaceId").IsEqual(Column.Parameter(workplaceId))
				.Execute();
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Creates <see cref="Section"/> data select.
		/// </summary>
		/// <returns><see cref="Select"/> instance.</returns>
		protected Select GetSectionsSelect() {
			Guid cultureId = UserConnection.CurrentUser.SysCultureId;
			var select =
				new Select(UserConnection)
					.Column("SysModule", "Id")
					.Column("SysModule", "Type")
					.Column("SysModule", "Code")
					.Column("SysModule", "Attribute")
					.Column("SysModule", "SysModuleEntityId")
					.Column("SysModule", "IconBackground")
					.Column("SysModule", "Image32Id")
					.Column("SysModule", "SectionModuleSchemaUId").As("ModuleSchemaUId")
					.Column("smv", "VisaSchemaUId")
					.Column("vscus", "Name").As("SectionSchema")
					.Column("vscus1", "Name").As("SectionModuleSchema")
					.Column("sme", "SysEntitySchemaUId").As("EntityUId")
					.Column("sme", "TypeColumnUId").As("TypeColumnUId")
				.From("SysModule").As("SysModule")
				.InnerJoin("SysModuleEntity").As("sme")
					.On("sme", "Id").IsEqual("SysModule", "SysModuleEntityId")
				.LeftOuterJoin("SysModuleVisa").As("smv")
					.On("SysModule", "SysModuleVisaId").IsEqual("smv", "Id")
				.LeftOuterJoin("VwSysClientUnitSchema").As("vscus")
					.On("SysModule", SectionSchemaUIdColumnName).IsEqual("vscus", "UId")
					.And("vscus", "SysWorkspaceId").IsEqual(Column.Parameter(UserConnection.Workspace.Id))
				.LeftOuterJoin("VwSysClientUnitSchema").As("vscus1")
					.On("SysModule", "SectionModuleSchemaUId").IsEqual("vscus1", "UId")
					.And("vscus1", "SysWorkspaceId").IsEqual(Column.Parameter(UserConnection.Workspace.Id)) as Select;
			select.AddColumnLocalization("SysModule", "Caption", "Caption", cultureId);
			return select;
		}

		/// <summary>
		/// Selects sections data using <paramref name="select"/> and creates <see cref="Section"/> collection.
		/// </summary>
		/// <param name="select"><see cref="Select"/> instance.</param>
		/// <returns><see cref="Section"/> collection.</returns>
		protected List<Section> GetSectionsFromDb(Select select) {
			var sectionWorkplaceIds = LoadSectionWorkplaceIds();
			List<Section> sections = new List<Section>();
			using (DBExecutor dbExecutor = UserConnection.EnsureDBConnection()) {
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						sections.AddIfNotExists(CreateSectionInstance(dataReader, sectionWorkplaceIds));
					}
				}
			}
			return sections;
		}

		/// <summary>
		/// Returns <see cref="ISchemaManagerItem"/> used by <paramref name="section"/>.
		/// </summary>
		/// <param name="section"><see cref="Section"/> instance.</param>
		/// <returns><see cref="ISchemaManagerItem"/> used by <paramref name="section"/>.</returns>
		protected ISchemaManagerItem GetSectionEntitySchemaItem(Section section) {
			return EntitySchemaManager.FindItemByUId(section.EntityUId);
		}

		/// <summary>
		/// Sets sections by type filters to <paramref name="select"/>.
		/// </summary>
		/// <param name="select"><see cref="Select"/> instance.</param>
		/// <param name="type">Type filter value</param>
		protected virtual void SetSectionsByTypeFilters(Select select, SectionType type) {
			select.Where("SysModule", "Type").IsEqual(Column.Parameter(type));
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public override Section Get(Guid sectionId) {
			var watch = Stopwatch.StartNew();
			Log.Debug($"[Get] [{watch.ElapsedMilliseconds}ms] Start.");
			var sections = _sectionCacheManager.GetAll(ScopeKey, () => GetSectionsFromDb(GetSectionsSelect()));
			var section = sections.FirstOrDefault(s => s.Id.Equals(sectionId));
			if (section == null) {
				var message = GetItemNotFoundMessage(sectionId);
				throw new ItemNotFoundException(message);
			}
			Log.Debug($"[Get] [{watch.ElapsedMilliseconds}ms] End.");
			return section;
		}

		/// <inheritdoc />
		public override IEnumerable<Section> GetAll() {
			return _sectionCacheManager.GetAll(ScopeKey, () => GetSectionsFromDb(GetSectionsSelect()));
		}

		/// <inheritdoc />
		public override IEnumerable<Section> GetByType(SectionType type) {
			return _sectionCacheManager.GetByType(ScopeKey, type, () => {
				var select = GetSectionsSelect();
				SetSectionsByTypeFilters(select, type);
				return GetSectionsFromDb(select);
			});
		}

		/// <inheritdoc />
		public override IEnumerable<Guid> GetRelatedEntityIds(Section section) {
			var result = new List<Guid>();
			var sectionMainEntity = GetSectionEntitySchemaItem(section);
			if (sectionMainEntity == null) {
				return result;
			}
			result.AddRangeIfNotExists(GetSectionRequiredEntityIds(sectionMainEntity));
			if (section.SysModuleVisaEntityUId.IsNotEmpty()) {
				AddUIdIfNotNull(result, EntitySchemaManager.FindItemByUId(section.SysModuleVisaEntityUId));
			}
			return result;
		}

		/// <inheritdoc />
		public override IEnumerable<string> GetSectionNonAdministratedByRecordsEntityCaptions(Section section) {
			return new List<string>();
		}

		/// <inheritdoc />
		public override void Save(Section section) {
			ClearCache();
		}

		/// <inheritdoc />
		public override void ClearCache() {
			_sectionCacheManager.Clear();
		}

		public override void RemoveByWorkplaceId(Guid workplaceId) {
			workplaceId.CheckArgumentEmpty(nameof(workplaceId));
			base.RemoveByWorkplaceId(workplaceId);
			RemoveByWorkplaceIdInternal(workplaceId);
		}

		#endregion

	}

	#endregion

}