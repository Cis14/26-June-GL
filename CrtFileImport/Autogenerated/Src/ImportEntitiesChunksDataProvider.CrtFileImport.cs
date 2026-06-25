namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;

	#region Class: ImportEntitiesChunksDataProvider

	/// <inheritdoc cref="IImportEntitiesChunksDataProvider"/>
	[DefaultBinding(typeof(IImportEntitiesChunksDataProvider))]
	public class ImportEntitiesChunksDataProvider : ImportChunkDataProvider<EntityChunkData>, IImportEntitiesChunksDataProvider
	{

		#region Fields: Private

		private readonly string _chunkProcessResultTableName = "ChunkProcessResult";

		#endregion

		#region Constructors: Public

		public ImportEntitiesChunksDataProvider(UserConnection userConnection) : base(userConnection) {
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc />
		protected override ImportChunkType GetImportChunkType() {
			return ImportChunkType.Entity;
		}

		#endregion

		#region Methods: Private

		private ImportDataChunk<EntityChunkData> CreateEntityDataChunk(Guid importSessionId, 
			IEnumerable<ImportEntity> importEntities) => new ImportDataChunk<EntityChunkData> {
			Data = new EntityChunkData {
				Entities = importEntities.Select(importEntity => importEntity.SaveState()).ToList() 
			},
			State = ImportChunkState.ToProcess,
			ChunkId = Guid.NewGuid(),
			ImportSessionId = importSessionId,
			Type = ImportChunkType.Entity
		};

		private void CreateChunkFromFileProcessor(ImportParameters importParameters) {
			var fileProcessor = ClassFactory.Get<ISaxFileProcessor>(
				new ConstructorArgument("userConnection", UserConnection));
			var importSessionId = importParameters.ImportSessionId;
			var chunkSize = importParameters.ChunkSize;
			var importEntities = fileProcessor.ReadEntities(importSessionId);
			var chunk = new List<ImportEntity>(chunkSize);
			var counter = 0;
			foreach (var importEntity in importEntities) {
				chunk.Add(importEntity);
				counter++;
				importParameters.TotalRowsCount++;
				if (counter < chunkSize) {
					continue;
				}
				Add(CreateEntityDataChunk(importSessionId, chunk));
				chunk = new List<ImportEntity>(chunkSize);
				counter = 0;
			}
			if (chunk.Any()) {
				Add(CreateEntityDataChunk(importSessionId, chunk));
			}
		}

		private void CreateChunksFromParameters(ImportParameters importParameters) {
			var entitiesChunks = importParameters.Entities.SplitOnChunks(importParameters.ChunkSize);
			var newEntitiesChunks = new List<ImportDataChunk<EntityChunkData>>();
			foreach (var chunk in entitiesChunks) {
				newEntitiesChunks.Add(CreateEntityDataChunk(importParameters.ImportSessionId, chunk));
			}
			Add(newEntitiesChunks);
		}

		private void ClearOldChunks(Guid importSessionId) {
			if (!GetIsChunksExists(importSessionId)) {
				return;
			}
			new Delete(UserConnection).From(ImportSessionChunkSchemaName).Where("ImportParametersId")
				.IsEqual(Column.Parameter(importSessionId)).Execute();
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public void SaveProcessedRow(Guid chunkId, int rowIndex, bool success = true) {
			new Insert(UserConnection)
				.Set("ImportSessionChunkId", Column.Parameter(chunkId))
				.Set("RowIndex", Column.Parameter(rowIndex))
				.Set("Success", Column.Parameter(success))
				.Set("CreatedOn", Column.Parameter(DateTime.UtcNow))
				.Set("ModifiedOn", Column.Parameter(DateTime.UtcNow))
				.Set("ModifiedById", Column.Parameter(UserConnection.CurrentUser.ContactId))
				.Set("CreatedById", Column.Parameter(UserConnection.CurrentUser.ContactId))
				.Into(_chunkProcessResultTableName)
				.Execute();
		}

		/// <inheritdoc />
		public IEnumerable<ProcessedRowResult> GetProcessedRows(Guid chunkId) {
			var select = new Select(UserConnection)
					.Column("RowIndex")
					.Column("Success")
				.From(_chunkProcessResultTableName)
				.Where("ImportSessionChunkId").IsEqual(Column.Parameter(chunkId)) as Select;
			return select.ExecuteEnumerable(reader => new ProcessedRowResult {
				RowIndex = (int)reader["RowIndex"],
				Success = (bool)reader["Success"]
			});
		}

		/// <inheritdoc />
		public void RemoveProcessedRows(Guid chunkId) {
			new Delete(UserConnection)
				.From(_chunkProcessResultTableName)
				.Where("ImportSessionChunkId").IsEqual(Column.Parameter(chunkId))
				.Execute();
		}

		/// <inheritdoc />
		public override void CreateDataChunks(ImportParameters importParameters) {
			ClearOldChunks(importParameters.ImportSessionId);
			if (!UserConnection.GetIsFeatureEnabled("UploadLargeFileByChunk")) {
				CreateChunksFromParameters(importParameters);
				return;
			}
			CreateChunkFromFileProcessor(importParameters);
		}

		#endregion

	}

	#endregion

	#region Class: ProcessedRowResult

	public class ProcessedRowResult
	{
		#region Properties: Public

		public int RowIndex { get; set; }
		public bool Success { get; set; }

		#endregion
	}

	#endregion

}
