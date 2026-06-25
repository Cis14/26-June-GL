namespace Terrasoft.Configuration.Section
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Configuration.Workplace;
	using Terrasoft.Core.Entities;

	#region Interface: ISectionRepository

	public interface ISectionRepository
	{
		/// <summary>
		/// Returns section instance with <paramref name="sectionId"/> identifier.
		/// </summary>
		/// <param name="sectionId"><see cref="Section"/> unique identifier.</param>
		/// <returns><see cref="Section"/> instance.</returns>
		Section Get(Guid sectionId);

		/// <summary>
		/// Returns all <see cref="Section"/> collection.
		/// </summary>
		/// <returns>All <see cref="Section"/> collection.</returns>
		IEnumerable<Section> GetAll();

		/// <summary>
		/// Returns requested <paramref name="type"/> <see cref="Section"/> instances collection.
		/// </summary>
		/// <param name="type"><see cref="SectionType"/> value.</param>
		/// <returns>Returns requested type <see cref="Section"/> instances collection.</returns>
		IEnumerable<Section> GetByType(SectionType type);

		/// <summary>
		/// Returns related to <paramref name="section"/> <see cref="Entity"/> unique identifiers collection.
		/// </summary>
		/// <param name="section"><see cref="Section"/> instance.</param>
		/// <returns><see cref="Entity"/> unique identifiers collection.</returns>
		IEnumerable<Guid> GetRelatedEntityIds(Section section);

		/// <summary>
		/// Returns related to <paramref name="section"/> <see cref="Entity"/> schema caption collection
		/// what are registered as module object and not administrated by records.
		/// </summary>
		/// <param name="section"><see cref="Section"/> instance.</param>
		/// <returns><see cref="Entity"/> schema caption collection.</returns>
		IEnumerable<string> GetSectionNonAdministratedByRecordsEntityCaptions(Section section);

		/// <summary>
		/// Returns related to entitySchema <see cref="Entity"/> caption collection
		/// what are registered as module object and not administrated by records and operations.
		/// </summary>
		/// <param name="entitySchemaUId">Entity schema UId.</param>
		/// <returns><see cref="Entity"/> schema caption collection by records.</returns>
		/// <returns><see cref="Entity"/> schema caption collection by operations.</returns>
		(IEnumerable<string> byRecords, IEnumerable<string> byOperations) GetEntitiesCaptionsNotAdministratedByRights(Guid entitySchemaUId);

		/// <summary>
		/// Sets <see cref="EntitySchema.AdministratedByRecords"/>,<see cref="EntitySchema.AdministratedByOperations"/> properties,
		/// Fills <see cref="SysSSPEntitySchemaAccessList"/> table for <paramref name="entitySchemaUId"/>
		/// <see cref="EntitySchema"/> and related schemas.
		/// </summary>
		/// <param name="entitySchemaUId">Entity schema UId</param>
		void SetConnectedEntitiesRights(Guid entitySchemaUId);

		/// <summary>
		/// Sets <see cref="EntitySchema.AdministratedByRecords"/> property for <paramref name="section"/>
		/// <see cref="EntitySchema"/> and related schemas.
		/// </summary>
		/// <param name="section"><see cref="Section"/> instance.</param>
		void SetSectionSchemasAdministratedByRecords(Section section);

		/// <summary>
		/// Saves <paramref name="section"/> in database.
		/// </summary>
		/// <param name="section"><see cref="Section"/> instance.</param>
		void Save(Section section);

		/// <summary>
		/// Clears section cache records/>
		/// </summary>
		void ClearCache();

		/// <summary>
		/// Removes all sections associated with the specified <paramref name="workplaceId"/>.
		/// </summary>
		/// <param name="workplaceId">The unique identifier of the workplace.</param>
		void RemoveByWorkplaceId(Guid workplaceId);

	}

	#endregion

}