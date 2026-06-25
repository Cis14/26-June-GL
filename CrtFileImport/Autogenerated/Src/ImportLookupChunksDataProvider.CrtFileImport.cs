namespace Terrasoft.Configuration.FileImport {

	using System;
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: ImportLookupChunksDataProvider

	/// <inheritdoc />
	[DefaultBinding(typeof(IImportLookupChunksDataProvider))]
	public class ImportLookupChunksDataProvider : ImportChunkDataProvider<LookupChunkData>, IImportLookupChunksDataProvider {

		#region Classe: Private

		private class LookupChunkCreator {

			#region Fields: Private

			private readonly Guid _importSessionId;
			private readonly IEnumerable<ImportColumn> _lookupColumns;
			private readonly UserConnection _userConnection;

			#endregion

			#region Constructors: Public

			public LookupChunkCreator(ImportParameters importParameters, UserConnection userConnection) {
				_importSessionId = importParameters.ImportSessionId;
				_lookupColumns = importParameters.GetLookupColumns();
				_userConnection = userConnection;
			}

			#endregion

			#region Methods: Private

			private ImportDataChunk<LookupChunkData> CreateChunk(LookupValuesToProcessMemento values) {
				return new ImportDataChunk<LookupChunkData>() {
					ImportSessionId = _importSessionId,
					ChunkId = Guid.NewGuid(),
					State = ImportChunkState.ToProcess,
					Type = ImportChunkType.Lookup,
					Data = new LookupChunkData() {
						ValuesToProcessState = values
					}
				};
			}

			#endregion

			#region Methods: Public

			public ImportDataChunk<LookupChunkData> CreateChunk(ImportDataChunk<EntityChunkData> chunk) {
				var processor = new LookupValuesToProcess(_userConnection);
				foreach (var entityMemento in chunk.Data.Entities) {
					var entity = ImportEntity.CreateFromMemento(entityMemento);
					processor.Add(entity, _lookupColumns);
				}
				var newLookupChunk = CreateChunk(processor.SaveState());
				return newLookupChunk;
			}

			#endregion
		}

		#endregion

		#region Fields: Private

		private readonly ImportEntitiesChunksDataProvider _importDataProvider;

		#endregion

		#region Constructors: Public

		public ImportLookupChunksDataProvider(UserConnection userConnection) : base(userConnection) {
			_importDataProvider = ClassFactory.Get<ImportEntitiesChunksDataProvider>(
				new ConstructorArgument("userConnection", UserConnection)
			);
		}

		#endregion

		#region Methods: Private

		private void CreateLookupChunks(ImportParameters importParameters) {
			var entityChunks = _importDataProvider.Get(importParameters.ImportSessionId);
			var creator = new LookupChunkCreator(importParameters, UserConnection);
			foreach (var chunk in entityChunks) {
				var newChunk = creator.CreateChunk(chunk);
				Add(newChunk);
			}
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc />
		protected override ImportChunkType GetImportChunkType() {
			return ImportChunkType.Lookup;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public override void CreateDataChunks(ImportParameters importParameters) {
			if (importParameters.AnyLookupColumns()) {
				CreateLookupChunks(importParameters);
			}
		}

		#endregion
	}

	#endregion
}
