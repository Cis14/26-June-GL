namespace Terrasoft.Configuration.FileImport
{
	using Core;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Store;

	#region  Class: FileImporter

	/// <summary>
	/// An instance of this class is responsible for importing a file.
	/// </summary>
	[DefaultBinding(typeof(IFileImporter), Name = nameof(FileImporter))]
	public class FileImporter : BaseFileImporter, IFileImporter
	{

		#region  Fields: Private

		private FileImportTagManager _fileImportTagManager;
		private PrimaryImportEntitiesGetter _primaryImportEntitiesGetter;
		private PrimaryImportEntitiesSetter _primaryImportEntitiesSetter;
		private ChildImportEntitiesGetter _childImportEntitiesGetter;
		private ChildImportEntitiesSetter _childImportEntitiesSetter;

		#endregion

		#region Properties: Private

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Primary import entities getter.
		/// </summary>
		protected PrimaryImportEntitiesGetter PrimaryImportEntitiesGetter => _primaryImportEntitiesGetter ??
				(_primaryImportEntitiesGetter = new PrimaryImportEntitiesGetter(UserConnection, ColumnsProcessor));

		/// <summary>
		/// Child import entities getter.
		/// </summary>
		protected ChildImportEntitiesGetter ChildImportEntitiesGetter => _childImportEntitiesGetter ??
				(_childImportEntitiesGetter = new ChildImportEntitiesGetter(UserConnection, ColumnsProcessor));

		/// <summary>
		/// Primary import entities setter.
		/// </summary>
		protected PrimaryImportEntitiesSetter PrimaryImportEntitiesSetter => _primaryImportEntitiesSetter ??
				(_primaryImportEntitiesSetter = new PrimaryImportEntitiesSetter(UserConnection, ColumnsProcessor));

		/// <summary>
		/// Child import entities setter.
		/// </summary>
		protected ChildImportEntitiesSetter ChildImportEntitiesSetter => _childImportEntitiesSetter ??
				(_childImportEntitiesSetter = new ChildImportEntitiesSetter(UserConnection, ColumnsProcessor));

		/// <summary>
		/// File import tag manager.
		/// </summary>
		protected FileImportTagManager FileImportTagManager => _fileImportTagManager ??
				(_fileImportTagManager = new FileImportTagManager(UserConnection));

		#endregion

		#region Events: Public

		public event EventHandler<ImportEntitySaveErrorEventArgs> ImportEntitySaveError;

		public event EventHandler<ImportEntitySavingEventArgs> ImportEntitySaving;

		public event EventHandler<ColumnProcessErrorEventArgs> ColumnProcessError;

		public event EventHandler<InfoMessageEventArgs> ImportEntitiesMerge;

		public event EventHandler<BeforeImportEntitiesSaveEventArgs> BeforeImportEntitiesSave;

		public event EventHandler<ImportEntitySavedEventArgs> ImportEntitySaved;

		public event EventHandler<AfterImportEntitiesSaveEventArgs> AfterImportEntitiesSave;

		#endregion

		#region Constructors: Public

		/// <inheritdoc cref="BaseFileImporter"/>
		/// <summary>
		/// Creates instance of type <see cref="FileImporter" />.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public FileImporter(UserConnection userConnection)
				: base(userConnection) { }

		/// <summary>
		/// Create instance of <see cref="FileImporter"/>
		/// </summary>
		/// <param name="userConnection"></param>
		/// <param name="cacheStore"></param>
		/// <param name="columnsProcessor"></param>
		/// <param name="peGetter"></param>
		/// <param name="ceGetter"></param>
		/// <param name="peSetter"></param>
		/// <param name="_ceSetter"></param>
		public FileImporter(UserConnection userConnection, ICacheStore cacheStore,
				IColumnsAggregatorAdapter columnsProcessor, PrimaryImportEntitiesGetter peGetter,
				ChildImportEntitiesGetter ceGetter, PrimaryImportEntitiesSetter peSetter,
				ChildImportEntitiesSetter _ceSetter)
				: base(userConnection, cacheStore, columnsProcessor) {
			_primaryImportEntitiesGetter = peGetter;
			_primaryImportEntitiesSetter = peSetter;
			_childImportEntitiesGetter = ceGetter;
			_childImportEntitiesSetter = _ceSetter;
		}

		#endregion

		#region Methods: Private

		private void InitColumnsProcessor(ImportParameters parameters) {
			ImportParametersIterator.Iterate(parameters, ColumnsProcessor.Add);
		}

		private void InitEntityForSave(ImportParameters parameters, ImportEntity importEntity) {
			importEntity.InitPrimaryEntity(UserConnection, parameters);
			foreach (var column in parameters.Columns) {
				var columnValue = importEntity.FindColumnValue(column);
				if (columnValue == null) {
					continue;
				}
				foreach (var destination in column.Destinations) {
					var entity = importEntity.GetEntityForSave(UserConnection, destination);
					var columnValueName = destination.ColumnValueName;
					var valueForSave = ColumnsProcessor.FindValueForSave(destination, columnValue);
					if (valueForSave == null) {
						continue;
					}
					if (entity.StoringState != StoringObjectState.New) {
						var entityValue = entity.GetColumnValue(columnValueName);
						if (valueForSave.Equals(entityValue) || destination.IsKey) {
							continue;
						}
					}
					if (entity.Schema.Columns.GetByName(destination.ColumnName).DataValueType is TextDataValueType) {
						valueForSave = valueForSave.ToString().Trim();
					}
					entity.SetColumnValue(columnValueName, valueForSave);
				}
			}
		}

		private List<ImportEntity> GetMergedImportEntities(IEnumerable<ImportEntity> entities, IEnumerable<ImportColumn> keyColumns) {
			return entities
				.GroupBy(importEntity => importEntity.GetRawKey(keyColumns))
				.Select(entityGroup => entityGroup.First())
				.ToList();
		}

		private string GetMergedImportEntitiesInfoMessage(uint mergedRowsCount) {
			string messageTemplate = new LocalizableString(UserConnection.Workspace.ResourceStorage, "FileImporter",
					"LocalizableStrings.MergedImportEntitiesInfoMessageTemplate.Value");
			return string.Format(messageTemplate, mergedRowsCount);
		}

		/// <summary>
		/// Handles columns processor error.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		private void HandleColumnProcessError(object sender, ColumnProcessErrorEventArgs eventArgs) {
			OnColumnProcessError(eventArgs);
		}

		/// <summary>
		/// Send message before save entity
		/// </summary>
		/// <param name="eventArgs"></param>
		private void OnImportEntitySaving(ImportEntitySavingEventArgs eventArgs) {
			ImportEntitySaving?.Invoke(this, eventArgs);
		}

		/// <summary>
		/// Validates columns mapping.
		/// </summary>
		/// <returns>Validation result.</returns>
		private void ValidateColumnsMapping(ImportParameters parameters) {
			var rootSchemaUId = parameters.RootSchemaUId;
			var columns = parameters.Columns;
			var entitySchema = UserConnection.EntitySchemaManager.GetInstanceByUId(rootSchemaUId);
			var schemaColumns = entitySchema.Columns.Where(column =>
					column.RequirementType.Equals(EntitySchemaColumnRequirementType.ApplicationLevel) &&
					column.UsageType.Equals(EntitySchemaColumnUsageType.General) && !column.HasDefValue &&
					!column.HasConstDefValue);
			var destinations = columns.SelectMany(parameterColumn => parameterColumn.Destinations)
				.Where(destination => destination.SchemaUId.Equals(rootSchemaUId));
			var notFilledColumns = schemaColumns.Where(schemaColumn =>
					!destinations.Any(destination => destination.ColumnName.Equals(schemaColumn.Name)));
			var entitySchemaColumns = notFilledColumns.ToList();
			if (entitySchemaColumns.Any()) {
				throw new RequiredColumnsEmptyValuesException(entitySchemaColumns);
			}
		}

		private void NotifyOnComplete(ImportParameters parameters) {
			if (!parameters.NeedSendNotify) {
				return;
			}
			var importNotifier = ClassFactory.Get<IImportNotifier>(
				new ConstructorArgument("userConnection", UserConnection),
				new ConstructorArgument("importParameters", parameters));
			importNotifier.NotifyEnd();
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="BaseFileImporter"/>
		protected override void Import(ImportParameters parameters) {
			ValidateColumnsMapping(parameters);
			MergeImportEntities(parameters);
			ProcessColumnValues(parameters);
			InitImportEntities(parameters);
			SaveImportEntities(parameters);
			OnAfterImportEntitiesSave(new AfterImportEntitiesSaveEventArgs {
					ImportedRowsCount = parameters.ImportedRowsCount
			});
			NotifyOnComplete(parameters);
		}

		/// <summary>
		/// Initializes import entities.
		/// </summary>
		/// <param name="parameters">File import parameters.</param>
		protected virtual void InitImportEntities(ImportParameters parameters) {
			var keyColumns = GetKeyColumns(parameters);
			var importColumns = keyColumns.ToList();
			var primaryEntities = PrimaryImportEntitiesGetter.Get(parameters, importColumns);
			PrimaryImportEntitiesSetter.Set(parameters, primaryEntities, importColumns);
			var childEntities = ChildImportEntitiesGetter.Get(parameters);
			ChildImportEntitiesSetter.Set(parameters, childEntities);
		}

		/// <summary>
		/// Merges import entities.
		/// </summary>
		/// <param name="parameters">File import parameters.</param>
		protected virtual void MergeImportEntities(ImportParameters parameters) {
			var keyColumns = GetKeyColumns(parameters).ToList();
			if (keyColumns.Any()) {
				parameters.Entities = GetMergedImportEntities(parameters.Entities, keyColumns);
				var mergedRowsCount = (uint)(parameters.TotalRowsCount - parameters.Entities.Count);
				if (mergedRowsCount > 0) {
					parameters.NotImportedRowsCount += mergedRowsCount;
					var eventArgs = new InfoMessageEventArgs {
							Message = GetMergedImportEntitiesInfoMessage(mergedRowsCount)
					};
					OnImportEntitiesMerge(eventArgs);
				}
			}
		}

		/// <summary>
		/// Send message after save entities
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnAfterImportEntitiesSave(AfterImportEntitiesSaveEventArgs eventArgs) {
			AfterImportEntitiesSave?.Invoke(this, eventArgs);
		}

		/// <summary>
		/// Send message before save entities
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnBeforeImportEntitiesSave(BeforeImportEntitiesSaveEventArgs eventArgs) {
			BeforeImportEntitiesSave?.Invoke(this, eventArgs);
		}

		/// <summary>
		/// Send message on column process error
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnColumnProcessError(ColumnProcessErrorEventArgs eventArgs) {
			ColumnProcessError?.Invoke(this, eventArgs);
		}

		/// <summary>
		/// Send message after merge entities
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnImportEntitiesMerge(InfoMessageEventArgs eventArgs) {
			ImportEntitiesMerge?.Invoke(this, eventArgs);
		}

		/// <summary>
		/// Send message after save entity
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnImportEntitySaved(ImportEntitySavedEventArgs eventArgs) {
			ImportEntitySaved?.Invoke(this, eventArgs);
		}

		/// <summary>
		/// Send message on entity saving error
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnImportEntitySaveError(ImportEntitySaveErrorEventArgs eventArgs) {
			ImportEntitySaveError?.Invoke(this, eventArgs);
		}
	
		/// <inheritdoc cref="BaseFileImporter"/>
		protected override IColumnsAggregatorAdapter CreateColumnsAggregator() {
			var columnsProcessor = new NonPersistentColumnsAggregatorAdapter(UserConnection);
			columnsProcessor.ProcessError += HandleColumnProcessError;
			return columnsProcessor;
		}

		/// <summary>
		/// Processes column values.
		/// </summary>
		/// <param name="parameters">File import parameters.</param>
		protected virtual void ProcessColumnValues(ImportParameters parameters) {
			InitColumnsProcessor(parameters);
			ColumnsProcessor.Process();
		}

		/// <summary>
		/// Saves import entities.
		/// </summary>
		/// <param name="parameters">File import parameters.</param>
		protected virtual void SaveImportEntities(ImportParameters parameters) {
			OnBeforeImportEntitiesSave(new BeforeImportEntitiesSaveEventArgs {
				RootSchemaUId = parameters.RootSchemaUId,
				ImportTags = parameters.ImportTags
			});

			var entities = parameters.Entities;
			ImportEntity importEntity;
			while ((importEntity = entities.FirstOrDefault()) != null) {
				try {
					InitEntityForSave(parameters, importEntity);
					var eventArgs = new ImportEntitySavingEventArgs {
						TotalRowsCount = parameters.TotalRowsCount,
						ProcessedRowsCount = parameters.ProcessedRowsCount
					};
					OnImportEntitySaving(eventArgs);
					importEntity.Save();
					OnImportEntitySaved(new ImportEntitySavedEventArgs {
						RootSchemaUId = parameters.RootSchemaUId,
						PrimaryEntity = importEntity.PrimaryEntity,
						ImportSessionId = parameters.ImportSessionId
					});
					parameters.ImportedRowsCount++;
				} catch (OutOfMemoryException e) {
					throw;
				} catch (Exception e) {
					parameters.NotImportedRowsCount++;
					var eventArgs = new ImportEntitySaveErrorEventArgs {
						Exception = e,
						ImportEntity = importEntity,
						ImportSessionId = parameters.ImportSessionId
					};
					OnImportEntitySaveError(eventArgs);
				} finally {
					parameters.ProcessedRowsCount++;
					entities.Remove(importEntity);
				}
			}
		}

		protected override void ImportInternal(Guid importSessionId) {
			base.ImportInternal(importSessionId);
			var parameters = GetImportParameters(importSessionId);
			Import(parameters);
			SaveImportParameters(parameters);
		}

		#endregion

		#region Methods: Public 

		/// <inheritdoc cref="IFileImporter"/>
		[Obsolete("7.15.1 | The method is deprecated.")]
		public bool SaveRawImportEntities(ImportParameters parameters) {
			parameters.CheckArgumentNull(nameof(parameters));

			if (!UserConnection.GetIsFeatureEnabled("HighestSpeedFileImport")) {
				return true;
			}
			if (!parameters.CanUseImportEntitiesStorage) {
				return true;
			}
			var keyColumns = GetKeyColumns(parameters);
			return ImportEntitiesDataProvider.SaveImportEntitiesToDB(parameters, keyColumns);
		}

		#endregion
	}

	#endregion
}

