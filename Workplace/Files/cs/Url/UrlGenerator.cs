namespace Terrasoft.Configuration.Url
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::Common.Logging;
	using Newtonsoft.Json;
	using Terrasoft.Common;
	using Terrasoft.Configuration.Domain;
	using Terrasoft.Configuration.Exception;
	using Terrasoft.Configuration.PageEntity;
	using Terrasoft.Configuration.Section;
	using Terrasoft.Configuration.Workplace;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: UrlGenerator

	[DefaultBinding(typeof(IUrlGenerator))]
	public class UrlGenerator : IUrlGenerator {

		#region Fiedls: Private

		/// <summary>
		/// <see cref="ILog"/> implementation instance.
		/// </summary>
		private static readonly ILog _log = LogManager.GetLogger("Workplace");

		/// <summary>
		/// Section schema view module schema UId.
		/// </summary>
		private readonly Guid _sectionSchemaViewModuleSchemaUid = new Guid("12244568-6D4F-F201-ED26-AC3913021080");

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Page view operation.
		/// </summary>
		protected string Operation {
			get; set;
		} = "edit";

		/// <summary>
		/// <see cref="IDomainResolver"/> implementation instance.
		/// </summary>
		protected IDomainResolver DomainResolver {
			get; private set;
		}

		/// <summary>
		/// <see cref="IPageEntityManager"/> implementation instance.
		/// </summary>
		protected IPageEntityManager PageEntityManager {
			get; private set;
		}

		/// <summary>
		/// <see cref="ISectionManager"/> implementation instance.
		/// </summary>
		protected ISectionManager SectionManager {
			get; private set;
		}

		/// <summary>
		/// <see cref="IWorkplaceManager"/> implementation instance.
		/// </summary>
		protected IWorkplaceManager WorkplaceManager {
			get; private set;
		}

		/// <summary>
		/// <see cref="Core.UserConnection"/> instance.
		/// </summary>
		protected UserConnection UserConnection {
			get; private set;
		}

		/// <summary>
		/// <see cref="EntitySchemaManager"/> instance.
		/// </summary>
		protected EntitySchemaManager EntitySchemaManager {
			get; private set;
		}

		/// <summary>
		/// Use new application shell flag.
		/// </summary>
		protected bool UseShell { get; set; }

		/// <summary>
		/// Application pages type in shell host.
		/// </summary>
		protected Guid EditPagesUITypeIdForFreedomHost { get; set; }

		/// <summary>
		/// Application pages type in EXT host.
		/// </summary>
		protected Guid EditPagesUITypeIdForEXTHost { get; set; }

		#endregion

		#region Constructors: Public

		public UrlGenerator(UserConnection uc) {
			Init(uc);
		}

		public UrlGenerator(UserConnection uc, string operation) {
			Init(uc);
			Operation = operation;
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Initialize class dependencies.
		/// </summary>
		/// <param name="uc"></param>
		protected void Init(UserConnection uc) {
			UserConnection = uc;
			DomainResolver = ClassFactory.Get<IDomainResolver>();
			PageEntityManager = ClassFactory.Get<IPageEntityManager>(new ConstructorArgument("uc", UserConnection));
			SectionManager = ClassFactory.Get<ISectionManager>(new ConstructorArgument("uc", UserConnection),
					new ConstructorArgument("sectionType", UserConnection.CurrentUser.ConnectionType.ToString()));
			WorkplaceManager = ClassFactory.Get<IWorkplaceManager>(new ConstructorArgument("uc", UserConnection));
			EntitySchemaManager = UserConnection.EntitySchemaManager;
			UseShell = SysSettings.GetValue(uc, "UseNewShell", false);
			EditPagesUITypeIdForEXTHost = SysSettings.GetValue(uc, "EditPagesUITypeForEXTHost", Guid.Empty);
			EditPagesUITypeIdForFreedomHost = SysSettings.GetValue(uc, "EditPagesUITypeForFreedomHost", Guid.Empty);
		}

		/// <summary>
		/// Gets <see cref="PageEntity"/>'s collection, allowed by the current user.
		/// and filtered by entities and type column.
		/// </summary>
		/// <param name="schemaName"><see cref="EntitySchema"/> name.</param>
		/// <param name="recordId">Record unique identifier.</param>
		/// <returns>Return <see cref="PageEntity"/>'s collection</returns>
		private IEnumerable<PageEntity> GetPageEntities(string schemaName, Guid recordId) {
			_log.Info("GetPageEntities Started");
			var entitySchema = EntitySchemaManager.GetInstanceByName(schemaName);
			var sections = GetSections(entitySchema);
			LogInfoObjectList("Allowed entity sections in allowed workplaces", sections);
			var pageEntities = new List<PageEntity>();
			foreach (var section in sections) {
				var sectionPages = PageEntityManager.GetSectionPages(section.Id);
				LogInfoObjectList($"Section '{section.SchemaName}' pages", sectionPages);
				if (section.IsTyped(PageEntityManager)) {
					var typeColumnValue = GetEntityTypeColumnValue(entitySchema, recordId, section.TypeColumnName);
					LogInfoObject($"EntitySchemaName = {entitySchema.Name}, TypecolumnValue is", typeColumnValue);
					sectionPages = FilterPagesByType(sectionPages, typeColumnValue);
				}
				LogInfoObjectList($"Section '{section.SchemaName}' filtered pages", sectionPages);
				pageEntities.AddRangeIfNotExists(sectionPages);
			}
			_log.Info($"Get pages by schema '{entitySchema.Name}' Uid '{entitySchema.UId}'");
			var entityPages = PageEntityManager.GetEntityPages(entitySchema.UId);
			LogInfoObjectList($"Entity pages", entityPages);
			entityPages = entityPages.Where(p => !p.HasSection && !(p.Actions != null && p.Actions.Add));
			LogInfoObjectList($"Entity pages with no sections", entityPages);
			entityPages = FilterEntityPagesByType(recordId, entitySchema, entityPages.ToList());
			LogInfoObjectList($"Entity pages after filtering by type", entityPages);
			pageEntities.AddRangeIfNotExists(entityPages);
			if (pageEntities.IsEmpty()) {
				throw new NotFoundPageEntityException("PageEntity not found.");
			}
			return pageEntities.OrderByDescending(p => p.TypeColumnValue);
		}

		private IEnumerable<PageEntity> FilterEntityPagesByType(Guid recordId, EntitySchema entitySchema, List<PageEntity> entityPages) {
			try {
				var typedEntityPage = entityPages.FirstOrDefault(ep => ep.TypeColumnUId.HasValue && !ep.TypeColumnUId.Value.IsEmpty());
				if (typedEntityPage == null || !typedEntityPage.TypeColumnUId.HasValue || typedEntityPage.TypeColumnUId.Value.IsEmpty()) {
					return entityPages;
				}
				var entityTypeColumn = entitySchema.Columns.FindByUId(typedEntityPage.TypeColumnUId.Value);
				var entityTypeColumnValue = GetEntityTypeColumnValue(entitySchema, recordId, entityTypeColumn.Name);
				return entityPages.Where(ep =>
					(ep.TypeColumnUId.HasValue && ep.TypeColumnValue == entityTypeColumnValue) || ep.TypeColumnValue.IsEmpty());
			} catch (Exception e) {
				_log.Error("Filter entity pages by type failed.", e);
			}
			return entityPages;
		}

		/// <summary>
		/// Gets <see cref="Section"/>'s allowed by the current user and filtered by entities.
		/// </summary>
		/// <param name="entitySchema"><see cref="EntitySchema"/> instance.</param>
		/// <returns>Return <see cref="Section"/>'s allowed by the current user and filtered by entities.</returns>
		private IEnumerable<Section> GetSections(EntitySchema entitySchema) {
			var sections = SectionManager.GetSectionsByEntityUId(entitySchema.UId);
			LogInfoObjectList($"Allowed schema '{entitySchema.Name}' sections", sections);
			var sectionType = GetSectionType();
			return sections.Where(s => s.Type == sectionType);
		}

		/// <summary>
		/// Logging <see cref="IEnumerable"/> list of objects.
		/// </summary>
		/// <param name="message">Logging message.</param>
		/// <param name="list">Logging objects list.</param>
		private void LogInfoObjectList(string message, IEnumerable<object> list) {
			foreach (var obj in list) {
				LogInfoObject(message, obj);
			}
		}

		/// <summary>
		/// Logging serialize <paramref name="obj"/> with <paramref name="message"/>.
		/// </summary>
		/// <param name="message">Logging message.</param>
		/// <param name="obj">Logging object.</param>
		private void LogInfoObject(string message, object obj) {
			_log.Info($"{message}. Object '{obj.GetType().Name}': {JsonConvert.SerializeObject(obj)}");
		}

		/// <summary>
		/// Gets section type by current user.
		/// </summary>
		/// <returns>Return section type.</returns>
		private SectionType GetSectionType() {
			return UserConnection.CurrentUser.ConnectionType == UserType.General
				? SectionType.General
				: SectionType.SSP;
		}

		/// <summary>
		/// Gets entity type column value.
		/// </summary>
		/// <param name="entitySchema"<see cref="EntitySchema"/> instance.</param>
		/// <param name="recordId">Record unique identifier.</param>
		/// <param name="typeColumnName">Entity type column name.</param>
		/// <returns>Return entity type column value.</returns>
		private Guid GetEntityTypeColumnValue(EntitySchema entitySchema, Guid recordId, string typeColumnName) {
			if (string.IsNullOrEmpty(typeColumnName)) {
				return Guid.Empty;
			}
			var typeColumn = entitySchema.GetSchemaColumnByPath(typeColumnName);
			var select = new Select(UserConnection)
							.Column(typeColumn.ColumnValueName)
						.From(entitySchema.Name)
						.Where(entitySchema.PrimaryColumn.Name).IsEqual(Column.Parameter(recordId)) as Select;
			try {
				return select.ExecuteScalar<Guid>();
			} catch (Exception ex) {
				throw new NotFoundEntityException($"Entity not found with name = {entitySchema.Name}, recordId = {recordId}", ex);
			}
		}

		/// <summary>
		/// Gets filtered <see cref="PageEntity"/> collection by type,
		/// if <paramref name="typeColumnValue"/> is not empty.
		/// </summary>
		/// <param name="pages"><see cref="PageEntity"/> collection.</param>
		/// <param name="typeColumnValue">Type column value.</param>
		/// <returns>Return filtered <see cref="PageEntity"/> collection by type.</returns>
		private IEnumerable<PageEntity> FilterPagesByType(IEnumerable<PageEntity> pages, Guid typeColumnValue) {
			if (!typeColumnValue.IsEmpty()) {
				pages = pages.Where(p => p.TypeColumnValue.Equals(typeColumnValue));
			}
			return pages;
		}

		private Guid GetObjectPageTypeException(string schemaName) {
			var columnName = UseShell ? "FreedomId" : "EXTId";
			var select = new Select(UserConnection)
				.Top(1)
				.Column(columnName)
				.From("EntityEditPagesUITypes")
				.Where("EntitySchemaName").IsEqual(Column.Const(schemaName)) as Select;
			return select.ExecuteScalar<Guid>();
		}

		private Guid GetDefultPageType(string schemaName) {
			var objectPageTypeId = GetObjectPageTypeException(schemaName);
			if (objectPageTypeId.IsNotEmpty()) {
				return objectPageTypeId;
			}
			return UseShell? EditPagesUITypeIdForFreedomHost : EditPagesUITypeIdForEXTHost;
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Gets last segment of Url.
		/// </summary>
		/// <param name="schemaName"><see cref="EntitySchema"/> name.</param>
		/// <param name="page"><see cref="PageEntity"/> instance.</param>
		/// <param name="recordId">Record unique identifier.</param>
		/// <returns>Last Url segment.</returns>
		protected string GetUrlHash(string schemaName, PageEntity pageEntity, Guid recordId) {
			var schemaGroup = UserConnection.ClientUnitSchemaManager.FindItemByUId(pageEntity.CardSchemaUId)?.GetPropertyValue(i => i.Group);
			var urlHash = schemaGroup == "MiniPage"
				? GetModalPageUrlHash(schemaName, pageEntity, recordId, Operation)
				: GetFormPageUrlHash(pageEntity, recordId, Operation);
			return urlHash;
		}

		/// <summary>
		/// Gets last segment of form page Url.
		/// </summary>
		/// <param name="page"><see cref="PageEntity"/> instance.</param>
		/// <param name="recordId">Record unique identifier.</param>
		/// <param name="operation">Page view operation.</param>
		/// <returns>Last form page Url segment.</returns>
		protected string GetFormPageUrlHash(PageEntity page, Guid recordId, string operation) {
			var urlComponents = new string[] { page.PageModuleName, page.PageSchemaName, operation, recordId.ToString() };
			return string.Join("/", urlComponents);
		}

		/// <summary>
		/// Gets last segment of modal page Url.
		/// </summary>
		/// <param name="schemaName"><see cref="EntitySchema"/> name.</param>
		/// <param name="page"><see cref="PageEntity"/> instance.</param>
		/// <param name="recordId">Record unique identifier.</param>
		/// <param name="operation">Page view operation.</param>
		/// <returns>Last modal page Url segment.</returns>
		protected string GetModalPageUrlHash(string schemaName, PageEntity page, Guid recordId, string operation) {
			var entitySchema = EntitySchemaManager.GetInstanceByName(schemaName);
			var sections = GetSections(entitySchema).ToList();
			Section section = null;
			if (sections.Count == 1) {
				section = sections[0];
			} else {
				section = UseShell
					? sections.Find(s => s.ModuleSchemaUId == _sectionSchemaViewModuleSchemaUid)
					: sections.Find(s => s.ModuleSchemaUId != _sectionSchemaViewModuleSchemaUid);
			}
			var sectionHash = string.Empty;
			if (section != null) {
				sectionHash = $"Section/{section.SchemaName}";
			}
			var modalHash = $"{page.PageSchemaName}/{operation}/{recordId}";
			return $"{sectionHash}[modal={modalHash}]";
		}

		/// <summary>
		/// Gets <see cref="PageEntity"/> defining hash of Url.
		/// </summary>
		/// <param name="schemaName"><see cref="EntitySchema"/> name.</param>
		/// <param name="recordId">Record unique identifier.</param>
		/// <returns><see cref="PageEntity"/> instance.</returns>
		protected PageEntity GetPageEntity(string schemaName, Guid recordId) {
			var pageEntities = GetPageEntities(schemaName, recordId);
			LogInfoObjectList($"Final pages", pageEntities);
			var defaultPageType = GetDefultPageType(schemaName);
			var typedPages = pageEntities.Where(p => p.PageTypeId.Equals(defaultPageType));
			return typedPages?.FirstOrDefault( p => Convert.ToInt32(p.IsExternal) == (int)GetSectionType())
				?? typedPages.FirstOrDefault()
				?? pageEntities.First();
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IUrlGenerator.GetUrl"/>
		public string GetUrl(string schemaName, Guid recordId) {
			try {
				_log.Info("Generate URL Started");
				var defaultUrl = GetDefaultUrl();
				var pageEntity = GetPageEntity(schemaName, recordId);
				LogInfoObject($"Final page", pageEntity);
				var urlHash = GetUrlHash(schemaName, pageEntity, recordId);
				var url = string.Concat(defaultUrl, "#", urlHash);
				_log.Info($"Generate URL ended. Url is '{url}'");
				return url;
			} catch (Exception e) {
				_log.Error($"Generate default URL. Error - {e.Message}");
				return GetDefaultUrl();
			}
		}

		/// <inheritdoc cref="IUrlGenerator.GeDefaultUrl"/>
		public string GetDefaultUrl() {
			var domain = DomainResolver.GetDomain();
			var module = UseShell ? "Shell/" : "Nui/ViewModule.aspx";
			return $"{domain}/{module}";
		}

		#endregion

	}

	#endregion

}
