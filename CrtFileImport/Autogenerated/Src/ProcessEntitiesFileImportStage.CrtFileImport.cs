namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region  Class: ProcessEntitiesFileImportStage

	/// <inheritdoc cref="BaseFileImportStage"/>
	/// <summary>
	/// Execute import entities
	/// </summary>
	[DefaultBinding(typeof(IBaseFileImportStage), Name = nameof(ProcessEntitiesFileImportStage))]
	public class ProcessEntitiesFileImportStage : BaseFileImportStage
	{

		#region  Fields: Private

		private IFileImportTagManager _fileImportTagManager;
		private IImportEntitiesChunksDataProvider _importEntitiesChunksDataProvider;
		private IImportNotifier _importNotifier;
		private IImportParametersRepository _importParametersRepository;
		private double _onePercentValue;
		private uint _successProcessedRowsCount;
		private uint _processedWithErrorRowsCount;
		private uint _totalRowsCount;
		private bool _isNeedNotify;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Create instance of <see cref="ProcessEntitiesFileImportStage"/>
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/></param>
		/// <param name="columnsProcessor"><see cref="IColumnsAggregatorAdapter"/></param>
		public ProcessEntitiesFileImportStage(UserConnection userConnection, IColumnsAggregatorAdapter columnsProcessor)
				: base(userConnection, columnsProcessor) { }

		#endregion

		#region Properties: Private

		private IFileImportTagManager FileImportTagManager => _fileImportTagManager ?? (_fileImportTagManager =
				ClassFactory.Get<IFileImportTagManager>(GetUserConnectionArgument()));

		private IImportEntitiesChunksDataProvider ImportEntitiesChunksDataProvider =>
				_importEntitiesChunksDataProvider ?? (_importEntitiesChunksDataProvider =
						ClassFactory.Get<IImportEntitiesChunksDataProvider>(GetUserConnectionArgument()));

		private IImportParametersRepository ImportParametersRepository => _importParametersRepository ??
			(_importParametersRepository = ClassFactory.Get<IImportParametersRepository>(GetUserConnectionArgument()));

		#endregion

		#region Properties: Public

		/// <inheritdoc cref="BaseFileImportStage"/>
		public override FileImportStagesEnum StageId => FileImportStagesEnum.ProcessEntitiesFileImportStage;

		#endregion

		#region Methods: Private

		private void AddTagsToImportedEntity(ImportEntitySavedEventArgs args) {
			FileImportTagManager.SaveEntityTags(this, new ImportEntitySavedEventArgs {
					RootSchemaUId = args.RootSchemaUId,
					PrimaryEntity = args.PrimaryEntity,
					ImportSessionId = args.ImportSessionId
			});
		}

		private IFileImportEntitiesChunkProcessor CreateChunkProcessor() {
			var chunkProcessor = ClassFactory.Get<IFileImportEntitiesChunkProcessor>(GetUserConnectionArgument(),
					new ConstructorArgument("columnsProcessor", ColumnsProcessor));
			return chunkProcessor;
		}

		private void CreateTags(Guid rootSchemaUId, List<ImportTag> importTags) {
			FileImportTagManager.CreateTags(this, new BeforeImportEntitiesSaveEventArgs {
					RootSchemaUId = rootSchemaUId,
					ImportTags = importTags
			});
		}

		private void DeleteTags() {
			if(_successProcessedRowsCount != default(uint)) {
				return;
			}
			FileImportTagManager.DeleteNotUsedTags(this, new AfterImportEntitiesSaveEventArgs {
					ImportedRowsCount = 0
			});
		}
		private IEnumerable<ImportDataChunk<EntityChunkData>> GetEntitiesChunksData(ImportParameters parameters) {
			if (!ImportEntitiesChunksDataProvider.GetIsChunksExists(parameters.ImportSessionId)) {
				ImportEntitiesChunksDataProvider.CreateDataChunks(parameters);
			}
			return ImportEntitiesChunksDataProvider.Get(parameters.ImportSessionId);
		}

		private ConstructorArgument GetUserConnectionArgument() =>
				new ConstructorArgument("userConnection", UserConnection);

		private void InitCounts(ImportParameters parameters) {
			_totalRowsCount = parameters.TotalRowsCount;
			_onePercentValue = Math.Ceiling(_totalRowsCount / 100f);
			ImportParametersRepository.Update(parameters);
		}

		private void InitImportNotifier(ImportParameters parameters) {
			if ((_isNeedNotify = !parameters.NeedSendNotify)) {
				return;
			}
			_importNotifier = ClassFactory.Get<IImportNotifier>(GetUserConnectionArgument(),
					new ConstructorArgument("importParameters", parameters));
		}

		private void NotifyListenersOnEntitySaved(uint processedRowsCount) {
			if (_isNeedNotify || processedRowsCount % _onePercentValue > 0) {
				return;
			}
			var eventArgs = new ImportEntitySavingEventArgs {
					TotalRowsCount = _totalRowsCount,
					ProcessedRowsCount = processedRowsCount
			};
			_importNotifier.Notify(this, eventArgs);
		}

		private void OnImportEntitySaved(object sender, ImportEntitySavedEventArgs args) {
			_successProcessedRowsCount++;
			AddTagsToImportedEntity(args);
			NotifyListenersOnEntitySaved(_successProcessedRowsCount);
		}

		private void PopulateProcessedChunks(ImportDataChunk<EntityChunkData> entityChunk) {
			_processedWithErrorRowsCount += (uint)entityChunk.Data.ProcessedWithErrorEntityCount;
			_successProcessedRowsCount += (uint)entityChunk.Data.SuccessProcessedEntityCount;
		}

		private ImportParameters PrepareImportParameters(ImportParameters importParameters,
				ImportDataChunk<EntityChunkData> chunk) {
			var processedRowIndexes = new HashSet<uint>();
			if(chunk.State == ImportChunkState.InProcess) {
				var processedRowResult = _importEntitiesChunksDataProvider.GetProcessedRows(chunk.ChunkId)
					.ToArray();
				chunk.Data.SuccessProcessedEntityCount = processedRowResult.Count(i => i.Success);
				chunk.Data.ProcessedWithErrorEntityCount = processedRowResult.Count() - chunk.Data.SuccessProcessedEntityCount;
				_successProcessedRowsCount += (uint)chunk.Data.SuccessProcessedEntityCount;
				_processedWithErrorRowsCount += (uint)chunk.Data.ProcessedWithErrorEntityCount;
				processedRowIndexes.AddRange(processedRowResult.Select(i => (uint)i.RowIndex));
			}
			var chunkEntities = chunk.Data.Entities
				.Where(e => !processedRowIndexes.Contains(e.RowIndex))
				.Select(RestoreImportEntity).ToList();
			ImportParameters newImportParameters = importParameters.Clone();
			newImportParameters.ChunkId = chunk.ChunkId;
			newImportParameters.SetEntities(chunkEntities);
			return newImportParameters;
		}

		private void OnImportEntitySaveError(object sender, ImportEntitySaveErrorEventArgs args) {
			_processedWithErrorRowsCount++;
		}

		private void ProcessChunks(ImportParameters importParameters,
			ImportDataChunk<EntityChunkData> entityChunk) {
			if (entityChunk.State == ImportChunkState.Processed) {
				return;
			}
			IFileImportEntitiesChunkProcessor chunkProcessor = CreateChunkProcessor();
			SubscribeChunkProcessorEvents(chunkProcessor);
			ProcessChunk(chunkProcessor, importParameters, entityChunk);
			UnsubscribeChunkProcessorEvents(chunkProcessor);
		}

		private void UnsubscribeChunkProcessorEvents(IFileImportEntitiesChunkProcessor chunkProcessor) {
			chunkProcessor.ImportEntitySaveError -= ImportLogger.HandleException;
			chunkProcessor.ImportEntitySaveError -= OnImportEntitySaveError;
			chunkProcessor.ImportEntitySaved -= OnImportEntitySaved;
		}

		private void SubscribeChunkProcessorEvents(IFileImportEntitiesChunkProcessor chunkProcessor) {
			chunkProcessor.ImportEntitySaveError += ImportLogger.HandleException;
			chunkProcessor.ImportEntitySaveError += OnImportEntitySaveError;
			chunkProcessor.ImportEntitySaved += OnImportEntitySaved;
		}

		private void ProcessChunk(IFileImportEntitiesChunkProcessor chunkProcessor, ImportParameters importParameters, ImportDataChunk<EntityChunkData> chunk) {
			EventHandler<ImportEntitySavedEventArgs> successImportEntitySavedHandler = (o, arg) => {
				chunk.Data.SuccessProcessedEntityCount ++;
				_importEntitiesChunksDataProvider.SaveProcessedRow(chunk.ChunkId, (int)arg.RowIndex);
			};
			chunkProcessor.ImportEntitySaved += successImportEntitySavedHandler;
			EventHandler<ImportEntitySaveErrorEventArgs> importEntitySavedWithErrorHandler = (o, arg) => {
				chunk.Data.ProcessedWithErrorEntityCount ++;
				_importEntitiesChunksDataProvider.SaveProcessedRow(chunk.ChunkId, (int)arg.ImportEntity.RowIndex, false);
			};
			chunkProcessor.ImportEntitySaveError += importEntitySavedWithErrorHandler;

			ImportParameters newImportParameters = PrepareImportParameters(importParameters, chunk);
			SaveProcessedChunk(chunk, ImportChunkState.InProcess);
			chunkProcessor.ProcessChunk(newImportParameters);
			SaveProcessedChunk(chunk, ImportChunkState.Processed);

			_importEntitiesChunksDataProvider.RemoveProcessedRows(chunk.ChunkId);
			chunkProcessor.ImportEntitySaved -= successImportEntitySavedHandler;
			chunkProcessor.ImportEntitySaveError -= importEntitySavedWithErrorHandler;
		}

		private ImportEntity RestoreImportEntity(ImportEntityMemento memento) {
			var importEntity = new ImportEntity();
			importEntity.RestoreState(memento);
			return importEntity;
		}

		private void SaveProcessedChunk(ImportDataChunk<EntityChunkData> chunk, ImportChunkState chunkState) {
			chunk.State = chunkState;
			ImportEntitiesChunksDataProvider.Update(chunk);
		}

		private void UpdateImportResults(ImportParameters parameters) {
			parameters.ImportedRowsCount = _successProcessedRowsCount;
			parameters.NotImportedRowsCount = _processedWithErrorRowsCount + parameters.NotImportedRowsCount;
			parameters.ProcessedRowsCount = parameters.ImportedRowsCount + parameters.NotImportedRowsCount;
			ImportParametersRepository.Update(parameters);
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="BaseFileImportStage"/>
		protected override FileImportStagesEnum? InternalProcess(ImportParameters parameters) {
			ColumnsProcessor.ProcessError += ImportLogger.HandleError;
			InitImportNotifier(parameters);
			CreateTags(parameters.RootSchemaUId, parameters.ImportTags);
			var entityChunks = GetEntitiesChunksData(parameters);
			InitCounts(parameters);
			foreach (var entityChunk in entityChunks) {
				PopulateProcessedChunks(entityChunk);
				ProcessChunks(parameters, entityChunk);
			}
			DeleteTags();
			UpdateImportResults(parameters);
			ColumnsProcessor.ProcessError -= ImportLogger.HandleError;
			return FileImportStagesEnum.CompleteFileImportStage;
		}

		#endregion

	}

	#endregion

}

