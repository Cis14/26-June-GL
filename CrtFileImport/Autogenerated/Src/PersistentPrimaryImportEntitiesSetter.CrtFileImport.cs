namespace Terrasoft.Configuration.FileImport
{
	using System;
	using Terrasoft.Core.Entities;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: PersistentPrimaryImportEntitiesSetter

	/// <summary>
	/// An instance of this class is responsible for setting primary import entities.
	/// </summary>
	public class PersistentPrimaryImportEntitiesSetter
	{

		#region Constants: Private

		private const string ResourceManagerName = "PersistentPrimaryImportEntitiesSetter";
		private const string BufImportExcelRowIndexName = "ImportExcelRowIndex";

		#endregion

		#region Fields: Private

		private IImportLogger _logger;

		#endregion

		#region Properties: Private

		private UserConnection UserConnection { get; }

		private ImportParameters ImportParameters { get; set; }

		private LocalizableString FoundDuplicateDataException {
			get {
				return new LocalizableString(ResourceStorage, ResourceManagerName,
					"LocalizableStrings.FoundDuplicateDataException.Value").ToString();
			}
		}

		private IResourceStorage ResourceStorage {
			get { return UserConnection.Workspace.ResourceStorage; }
		}

		private IImportLogger Logger => _logger ?? (_logger = ClassFactory.Get<IImportLogger>(
			new ConstructorArgument("userConnection", UserConnection), 
			new ConstructorArgument("importParameters", ImportParameters)));

		private IFileImportPrimaryEntityLoader _fileImportPrimaryEntityLoader;
		private IFileImportPrimaryEntityLoader FileImportPrimaryEntityLoader => _fileImportPrimaryEntityLoader ??
			(_fileImportPrimaryEntityLoader = ClassFactory.Get<IFileImportPrimaryEntityLoader>(
			new ConstructorArgument("userConnection", UserConnection)));

		#endregion

		#region Constructor : Public

		public PersistentPrimaryImportEntitiesSetter(UserConnection userConnection) {
			UserConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		private void OnImportEntityError(Exception e, ImportEntity importEntity, Guid importSessionId) {
			var eventArgs = new ImportEntitySaveErrorEventArgs {
				Exception = e,
				ImportEntity = importEntity,
				ImportSessionId = importSessionId
			};
			Logger.HandleException(this, eventArgs);
		}

		private void SetPrimaryEntitiesRemoveDuplicate(Dictionary<uint, ImportEntity> hashTable, IGrouping<uint, Entity> groupEntity) {
			var importExcelRowIndex = groupEntity.First().GetTypedColumnValue<uint>(BufImportExcelRowIndexName);
			if(hashTable.TryGetValue(importExcelRowIndex, out ImportEntity paramEntity)) {
				if (groupEntity.Count() == 1) {
					paramEntity.PrimaryEntity = groupEntity.First();
				} 
				else {
					var importEntityException = new DublicateDataException(FoundDuplicateDataException);
					OnImportEntityError(importEntityException, paramEntity, ImportParameters.ImportSessionId);
					hashTable.Remove(importExcelRowIndex);
				}
			}
		}

		private void ProcessEntities(IEnumerable<Entity> entities) {
			var groupByEntities = entities.GroupBy(o => o.GetTypedColumnValue<uint>(BufImportExcelRowIndexName));
			var hashTable = ImportParameters.Entities.ToDictionary(e => e.RowIndex);
			foreach(var entity in groupByEntities) {
				SetPrimaryEntitiesRemoveDuplicate(hashTable, entity);
			}
			ImportParameters.Entities = hashTable.Values.ToList();
			SetPrimaryEntitiesColumnValues();
		}

		private void SetPrimaryEntitiesColumnValues() {
			if (!ImportParameters.LoadOnlyPrimaryColumnValuesInDuplicateSearch) {
				return;
			}
			FileImportPrimaryEntityLoader.LoadEntities(ImportParameters);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Sets existing entities to import entities.
		/// </summary>
		/// <param name="parameters">Import parameters.</param>
		/// <param name="entities">Existing entities.</param>
		public void Set(ImportParameters parameters, IEnumerable<Entity> entities) {
			parameters.CheckArgumentNull(nameof(parameters));
			if(entities.Any()) {
				ImportParameters = parameters;
				ProcessEntities(entities);
				Logger.SaveLog();
			}
		}

		#endregion

	}

	#endregion
}
