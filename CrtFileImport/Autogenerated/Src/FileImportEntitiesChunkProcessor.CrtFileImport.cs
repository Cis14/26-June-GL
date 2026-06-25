namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Core;
	using Terrasoft.Core.Configuration;
	using Core.Entities;
	using Core.Factories;
	using SystemSettings = Terrasoft.Core.Configuration.SysSettings;

	#region Class: FileImportEntitiesChunkProcessor

	///<inheritdoc cref="IFileImportEntitiesChunkProcessor"/>
	/// <summary>
	/// Execute import entities by passed parameters.
	/// </summary>
	[DefaultBinding(typeof(IFileImportEntitiesChunkProcessor), Name = nameof(FileImportEntitiesChunkProcessor))]
	public class FileImportEntitiesChunkProcessor : IFileImportEntitiesChunkProcessor
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private readonly IColumnsAggregatorAdapter _columnsProcessor;
		private IChildImportEntitiesGetter _childImportEntitiesGetter;
		private IChildImportEntitiesSetter _childImportEntitiesSetter;
		private IPrimaryEntityFinder _primaryEntityFinder;
		private bool? _saveInBackgroundMode;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Create instance of <see cref="FileImportEntitiesChunkProcessor"/>.
		/// </summary>
		/// <param name="columnsProcessor">Columns processor instance.</param>
		/// <param name="userConnection">User connection.</param>
		public FileImportEntitiesChunkProcessor(UserConnection userConnection,
				IColumnsAggregatorAdapter columnsProcessor) {
			_userConnection = userConnection;
			_columnsProcessor = columnsProcessor;
		}

		/// <summary>
		/// Create instance of <see cref="FileImportEntitiesChunkProcessor"/>.
		/// </summary>
		/// <param name="columnsProcessor">Columns processor instance.</param>
		/// <param name="userConnection">User connection.</param>
		/// <param name="fileImportInfoLogger">Import logger instance.</param>
		public FileImportEntitiesChunkProcessor(UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor,
				IFileImportInfoLogger fileImportInfoLogger): this(userConnection, columnsProcessor) {
			_fileImportInfoLogger = fileImportInfoLogger;
		}

		#endregion

		#region Properties: Private

		private IPrimaryEntityFinder PrimaryEntityFinder
		{
			get {
				if (_primaryEntityFinder is null) {
					var fileImportFactory = ClassFactory.Get<IFileImportFactory>();
					_primaryEntityFinder = fileImportFactory.GetPrimaryEntityFinder(_userConnection, _columnsProcessor);
				}
				return _primaryEntityFinder;
			}
		}

		private IChildImportEntitiesGetter ChildImportEntitiesGetter => _childImportEntitiesGetter ??
				(_childImportEntitiesGetter = ClassFactory.Get<IChildImportEntitiesGetter>(GetConstructorArguments()));

		private IChildImportEntitiesSetter ChildImportEntitiesSetter => _childImportEntitiesSetter ??
				(_childImportEntitiesSetter = ClassFactory.Get<IChildImportEntitiesSetter>(GetConstructorArguments()));

		private bool SaveInBackgroundMode {
			get {
				if (_saveInBackgroundMode.HasValue) {
					return _saveInBackgroundMode.Value;
				}
				_saveInBackgroundMode = SystemSettings.GetValue(_userConnection, "RunProcessesInBackgroundOnFileImport", false);
				return _saveInBackgroundMode.Value;
			}
		}

		private IFileImportInfoLogger _fileImportInfoLogger;
		private IFileImportInfoLogger FileImportInfoLogger =>
			_fileImportInfoLogger ?? (_fileImportInfoLogger = ClassFactory.Get<IFileImportInfoLogger>());

		#endregion

		#region Events: Public

		public event EventHandler<ImportEntitySavedEventArgs> ImportEntitySaved;

		public event EventHandler<ImportEntitySaveErrorEventArgs> ImportEntitySaveError;

		#endregion

		#region Methods: Private

		private ConstructorArgument[] GetConstructorArguments() => new[] {
			GetUserConnectionArgument(),
			new ConstructorArgument("columnsProcessor", _columnsProcessor)
		};

		private IEnumerable<ImportColumn> GetKeyColumns(ImportParameters parameters) => parameters.GetKeyColumns();

		private ConstructorArgument GetUserConnectionArgument() =>
			new ConstructorArgument("userConnection", _userConnection);

		private void InitEntityForSave(ImportParameters parameters, ImportEntity importEntity) {
			importEntity.InitPrimaryEntity(_userConnection, parameters);
			foreach (ImportColumn column in parameters.Columns) {
				ImportColumnValue columnValue = importEntity.FindColumnValue(column);
				if (columnValue == null) {
					continue;
				}
				SetEntityColumnValue(importEntity, column, columnValue);
			}
		}

		private void SetEntityColumnValue(ImportEntity importEntity, ImportColumn column, ImportColumnValue columnValue) {
			foreach (ImportColumnDestination destination in column.Destinations) {
				Entity entity = importEntity.GetEntityForSave(_userConnection, destination);
				string columnValueName = destination.ColumnValueName;
				object valueForSave = _columnsProcessor.FindValueForSave(destination, columnValue);
				if (valueForSave == null) {
					continue;
				}
				if (entity.StoringState != StoringObjectState.New) {
					object entityValue = entity.GetColumnValue(columnValueName);
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

		private void InitImportEntities(ImportParameters parameters) {
			WriteInfoMessage(parameters);
			FileImportInfoLogger.WriteSessionActionStartMessage(parameters.ImportSessionId,
				$"(GetKeyColumns step): {parameters.ChunkId}");
			var keyColumns = GetKeyColumns(parameters);
			FileImportInfoLogger.WriteSessionActionEndMessage(parameters.ImportSessionId,
				$"(GetKeyColumns step): {parameters.ChunkId}");
			FileImportInfoLogger.WriteSessionActionStartMessage(parameters.ImportSessionId,
				$"(LoadPrimaryEntity step): {parameters.ChunkId}");
			PrimaryEntityFinder.LoadPrimaryEntity(parameters, keyColumns);
			FileImportInfoLogger.WriteSessionActionEndMessage(parameters.ImportSessionId,
				$"(LoadPrimaryEntity step): {parameters.ChunkId}");
			FileImportInfoLogger.WriteSessionActionStartMessage(parameters.ImportSessionId,
				$"(ChildImportEntitiesGetter.Get step): {parameters.ChunkId}");
			var childEntities = ChildImportEntitiesGetter.Get(parameters);
			FileImportInfoLogger.WriteSessionActionEndMessage(parameters.ImportSessionId,
				$"(ChildImportEntitiesGetter.Get step): {parameters.ChunkId}");
			FileImportInfoLogger.WriteSessionActionStartMessage(parameters.ImportSessionId,
				$"(ChildImportEntitiesSetter.Set step): {parameters.ChunkId}");
			ChildImportEntitiesSetter.Set(parameters, childEntities);
			FileImportInfoLogger.WriteSessionActionEndMessage(parameters.ImportSessionId,
				$"(ChildImportEntitiesGetter.Set step): {parameters.ChunkId}");
			FileImportInfoLogger.WriteSessionActionEndMessage(parameters.ImportSessionId,
				$"duplicate search for Chunk: {parameters.ChunkId}");
		}

		private void WriteInfoMessage(ImportParameters parameters) {
			FileImportInfoLogger.WriteSessionActionStartMessage(parameters.ImportSessionId,
				$"duplicate search for Chunk: {parameters.ChunkId}");
		}

		private void OnImportEntitySaved(Entity primaryEntity, ImportParameters parameters, uint rowIndex) {
			var args = new ImportEntitySavedEventArgs {
				PrimaryEntity = primaryEntity,
				RootSchemaUId = parameters.RootSchemaUId,
				ImportSessionId = parameters.ImportSessionId,
				ChunkId = parameters.ChunkId,
				RowIndex = rowIndex
			};
			ImportEntitySaved?.Invoke(this, args);
		}

		private void OnImportEntitySaveError(Exception e, ImportEntity importEntity, Guid importSessionId) {
			var eventArgs = new ImportEntitySaveErrorEventArgs {
				Exception = e,
				ImportEntity = importEntity,
				ImportSessionId = importSessionId
			};
			ImportEntitySaveError?.Invoke(this, eventArgs);
		}

		private void SaveImportEntities(ImportParameters parameters) {
			Guid sessionId = parameters.ImportSessionId;
			var entities = parameters.Entities;
			ImportEntity importEntity;
			while ((importEntity = entities.FirstOrDefault()) != null) {
				if (importEntity.ImportEntityException != null) {
					OnImportEntitySaveError(importEntity.ImportEntityException, importEntity, sessionId);
					entities.Remove(importEntity);
					continue;
				}
				try {
					InitEntityForSave(parameters, importEntity);
					FileImportInfoLogger.WriteSessionActionStartMessage(parameters.ImportSessionId,
						$"saving entity: {importEntity.PrimaryEntity.PrimaryColumnValue}");
					Guid[] childEntitiesIds = importEntity.ChildEntities
						.Select(childEntity => childEntity.Value.PrimaryColumnValue).ToArray();
					if (childEntitiesIds.Length > 0) {
						FileImportInfoLogger.WriteSessionActionMessage(parameters.ImportSessionId,
							$"saving child entities: {string.Join(", ", childEntitiesIds)}");
					}
					importEntity.Save(SaveInBackgroundMode);
					FileImportInfoLogger.WriteSessionActionEndMessage(parameters.ImportSessionId,
						$"saving entity: {importEntity.PrimaryEntity.PrimaryColumnValue}");
					OnImportEntitySaved(importEntity.PrimaryEntity, parameters, importEntity.RowIndex);
				} catch (OutOfMemoryException) {
					throw;
				} catch (Exception e) {
					OnImportEntitySaveError(e, importEntity, sessionId);
				} finally {
					entities.Remove(importEntity);
				}
			}
		}

		#endregion

		#region Methods: Public

		///<inheritdoc cref="IFileImportEntitiesChunkProcessor"/>
		public void ProcessChunk(ImportParameters importParameters) {
			if (importParameters.GetIsImportCancelled(_userConnection)) {
				return;
			}
			InitImportEntities(importParameters);
			SaveImportEntities(importParameters);
		}

		#endregion
	}

	#endregion
}

