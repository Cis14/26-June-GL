namespace Terrasoft.Configuration.Section
{
	using System;
	using System.Collections.Generic;

	#region Class SectionRepository

	public abstract class BaseSectionRepository: ISectionRepository {

		/// <inheritdoc />
		public abstract Section Get(Guid sectionId);

		/// <inheritdoc />
		public abstract IEnumerable<Section> GetAll();

		/// <inheritdoc />
		public abstract IEnumerable<Section> GetByType(SectionType type);

		/// <inheritdoc />
		public abstract IEnumerable<Guid> GetRelatedEntityIds(Section section);

		/// <inheritdoc />
		public abstract IEnumerable<string> GetSectionNonAdministratedByRecordsEntityCaptions(Section section);

		/// <inheritdoc />
		public abstract void Save(Section section);

		/// <inheritdoc />
		public abstract void ClearCache();

		/// <inheritdoc />
		public virtual void SetSectionSchemasAdministratedByRecords(Section section) { }

		/// <inheritdoc />
		public virtual (IEnumerable<string> byRecords, IEnumerable<string> byOperations) GetEntitiesCaptionsNotAdministratedByRights(Guid entitySchemaUId) {
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public virtual void SetConnectedEntitiesRights(Guid entitySchemaUId) {
			throw new NotImplementedException();
		}

		public virtual void RemoveByWorkplaceId(Guid workplaceId) { }

	}

	#endregion

}
