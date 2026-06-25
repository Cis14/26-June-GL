namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: FileImportPrimaryEntityLoader

	/// <inheritdoc cref="IFileImportPrimaryEntityLoader"/>
	[DefaultBinding(typeof(IFileImportPrimaryEntityLoader), Name = nameof(FileImportPrimaryEntityLoader))]
	public class FileImportPrimaryEntityLoader : IFileImportPrimaryEntityLoader
	{

		#region Properties: Private

		private UserConnection UserConnection { get; }

		#endregion

		#region Constructors: Public

		public FileImportPrimaryEntityLoader(UserConnection userConnection) {
			UserConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		private Dictionary<Guid, Entity> LoadEntitiesColumnValues(string schemaName, IEnumerable<Guid> entitiesIds) {
			var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, schemaName) {
				UseAdminRights = false,
				IgnoreDisplayValues = true
			};
			esq.AddAllSchemaColumns();
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Id",
				entitiesIds.Cast<object>()));
			EntityCollection entityCollection = esq.GetEntityCollection(UserConnection);
			return entityCollection.ToDictionary(entity => entity.PrimaryColumnValue);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IFileImportPrimaryEntityLoader.LoadEntities"/>
		public void LoadEntities(ImportParameters parameters) {
			parameters.CheckArgumentNull(nameof(parameters));
			List<ImportEntity> primaryEntities = parameters.Entities.Where(e => e.PrimaryEntity != null).ToList();
			if (!primaryEntities.Any()) {
				return;
			}
			string rootSchemaName = UserConnection.EntitySchemaManager.GetItemByUId(parameters.RootSchemaUId).Name;
			IEnumerable<Guid> entitiesIds = primaryEntities.Select(e => e.PrimaryEntity.PrimaryColumnValue);
			Dictionary<Guid, Entity> entitiesColumnValues = LoadEntitiesColumnValues(rootSchemaName, entitiesIds);
			primaryEntities.ForEach(importEntity => {
				importEntity.PrimaryEntity = entitiesColumnValues[importEntity.PrimaryEntity.PrimaryColumnValue];
			});
		}

		#endregion

	}

	#endregion

}
