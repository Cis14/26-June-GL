namespace Terrasoft.Configuration.Section
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: ExternalSectionRepository

	[DefaultBinding(typeof(ISectionRepository), Name = "External")]
	public class ExternalSectionRepository : SspSectionRepository
	{

		#region Properties: Private

		private IEnumerable<PageEntity.PageEntity> _allPages;

		#endregion

		#region Properties: Public

		public static string ActualizeRightsProcess = "ObjectRecordRightsActualizationProcess";

		#endregion

		#region Constructors: Public

		public ExternalSectionRepository(UserConnection uc) : base(uc) { }

		#endregion

		#region Methods: Private

		private IEnumerable<Guid> GetReferenceEntities(Guid entitySchemaUId) {
			var result = new List<Guid> { entitySchemaUId };
			var schema = EntitySchemaManager.GetInstanceByUId(entitySchemaUId);
			foreach (var column in schema.Columns) {
				if (column != null && column.ReferenceSchemaUId.IsNotEmpty()) {
					result.AddIfNotExists(column.ReferenceSchemaUId);
				}
			}
			return result;
		}

		private void SetupOperationRights(IEnumerable<Guid> relatedEntityUIds) {
			var notAdministratedEntities = GetNonAdministratedByOperationsEntityUIds(relatedEntityUIds);
			SetEntityEnabledAdministratedByOperations(notAdministratedEntities);
			foreach (var entityUId in relatedEntityUIds.Where(uid => !notAdministratedEntities.Contains(uid))) {
				UserConnection.DBSecurityEngine.SetEntitySchemaOperationsRightLevel(UsersConsts.PortalUsersSysAdminUnitUId, entityUId,
					SchemaOperationRightLevels.CanAppend | SchemaOperationRightLevels.CanEdit | SchemaOperationRightLevels.CanRead);

			}
		}

		private void SetupRecordRights(IEnumerable<Guid> relatedEntityUIds, Guid entitySchemaUId) {
			var filteredSections = FilterSections(relatedEntityUIds).ToList();
			filteredSections.AddIfNotExists(entitySchemaUId);
			var filteredEntities = GetNonAdministratedByRecordsEntityUIds(filteredSections);
			SetSchemasAdministratedByRecords(filteredEntities);
			SetDefaultRecordRightsAndActualize(filteredEntities);
		}

		private void SetDefaultRecordRightsAndActualize(IEnumerable<Guid> filteredEntities) {
			var manager = UserConnection.ProcessSchemaManager;
			var processSchema = manager.GetInstanceByName(ActualizeRightsProcess);
			var processEngine = UserConnection.ProcessEngine;
			var processExecutor = processEngine.ProcessExecutor;
			foreach (var entityUid in filteredEntities) {
				SetDefaultRecordRights(entityUid);
				Dictionary<string, string> parameterValues = new Dictionary<string, string> {
					["EntitySchemaUId"] = entityUid.ToString()
				};
				processExecutor.Execute(processSchema.UId, parameterValues);
			}
		}

		private void SetDefaultRecordRights(Guid entityUid) {
			UserConnection.DBSecurityEngine.SetEntitySchemaAdministratedByRecords(entityUid, true);
			AddRecordDefRights(UsersConsts.AllEmployersSysAdminUnitUId, UsersConsts.AllEmployersSysAdminUnitUId, entityUid);
			AddRecordDefRights(UsersConsts.PortalUsersSysAdminUnitUId, UsersConsts.AllEmployersSysAdminUnitUId, entityUid);
		}

		private void AddRecordDefRights(Guid authorUId, Guid granteeUId, Guid entityUid) {
			UserConnection.DBSecurityEngine.SetEntitySchemaRecordDefRightLevel(authorUId, granteeUId, entityUid,
				EntitySchemaRecordRightOperation.Read, EntitySchemaRecordRightLevel.Allow);
			UserConnection.DBSecurityEngine.SetEntitySchemaRecordDefRightLevel(authorUId, granteeUId, entityUid,
				EntitySchemaRecordRightOperation.Edit, EntitySchemaRecordRightLevel.Allow);
			UserConnection.DBSecurityEngine.SetEntitySchemaRecordDefRightLevel(authorUId, granteeUId, entityUid,
				EntitySchemaRecordRightOperation.Delete, EntitySchemaRecordRightLevel.Allow);
		}

		private bool IsExternalSection(Guid entityUId) {
			var pages = _allPages ?? (_allPages = PageEntityRepository.GetAll());
			return pages.Any(p => p.SysEntitySchemaUId.Equals(entityUId) && p.IsExternal);
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc />
		protected override IEnumerable<Guid> FilterSections(IEnumerable<Guid> relatedEntityIds) {
			var sections = GetAll().Where(s => 
				 relatedEntityIds.Any(e => s.EntityUId == e && s.SchemaName.IsNotNullOrEmpty())
			);
			return sections.Where(s => IsRegisteredAsSsp(s.SysModuleEntityId) || IsExternalSection(s.EntityUId))
				.Select(s => s.EntityUId);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public override IEnumerable<Guid> GetRelatedEntityIds(Section section) {
			return GetReferenceEntities(section.EntityUId);
		}

		/// <inheritdoc />
		public override (IEnumerable<string> byRecords, IEnumerable<string> byOperations) GetEntitiesCaptionsNotAdministratedByRights(Guid entitySchemaUId) {
			IEnumerable<Guid> relatedEntityIds = GetReferenceEntities(entitySchemaUId);
			IEnumerable<Guid> filteredBySectionsIds = FilterSections(relatedEntityIds);
			filteredBySectionsIds.ToList().AddIfNotExists(entitySchemaUId);
			return (
				GetEntitySchemaCaptions(GetNonAdministratedByRecordsEntityUIds(filteredBySectionsIds)),
				GetEntitySchemaCaptions(GetNonAdministratedByOperationsEntityUIds(relatedEntityIds))
			);
		}

		/// <inheritdoc />
		public override void SetConnectedEntitiesRights(Guid entitySchemaUId) {
			IEnumerable<Guid> relatedEntityUIds = GetReferenceEntities(entitySchemaUId);
			SetupOperationRights(relatedEntityUIds);
			AddSchemaAccessAndSavePackageSchemaData(relatedEntityUIds);
			SetupRecordRights(relatedEntityUIds, entitySchemaUId);
		}

		#endregion

	}

	#endregion

}
