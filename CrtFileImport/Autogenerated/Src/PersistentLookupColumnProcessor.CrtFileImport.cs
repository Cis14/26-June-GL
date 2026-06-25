namespace Terrasoft.Configuration.FileImport {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: PersistentLookupColumnProcessor

	/// <inheritdoc />
	[DefaultBinding(typeof(IPersistentColumnProcessor), Name = nameof(PersistentLookupColumnProcessor))]
	public class PersistentLookupColumnProcessor : BaseColumnProcessor, IPersistentColumnProcessor
	{

		#region Properties: Protected

		/// <summary>
		/// Data value type unique identifier.
		/// </summary>
		protected override Guid DataValueTypeUId => DataValueType.LookupDataValueTypeUId;

		#endregion

		#region Fields: Private

		private readonly IImportLookupChunksDataProvider _importDataProvider;
		private LookupProcessedValues _lookupProcessedValues;
		private readonly IImportParametersRepository _importParametersRepository;
		private ImportParameters _importParameters;

		#endregion

		#region Properties: Private

		private IChunkLookupValuesHandler _chunkLookupValuesHandler;
		private IChunkLookupValuesHandler ChunkLookupValuesHandler => _chunkLookupValuesHandler ??
			(_chunkLookupValuesHandler = ClassFactory.Get<IChunkLookupValuesHandler>() ?? new ChunkLookupValuesHandler(UserConnection));

		#endregion

		#region Constructors: Public

		public PersistentLookupColumnProcessor(UserConnection userConnection) : base(userConnection) {
			_importDataProvider = ClassFactory.Get<IImportLookupChunksDataProvider>(
				new ConstructorArgument("userConnection", UserConnection)
			);
			_importParametersRepository = ClassFactory.Get<IImportParametersRepository>();
		}

		#endregion

		#region Methods: Private

		private LookupProcessedValuesMemento JoinLookupProcessedValues(List<LookupProcessedValuesMemento> list) {
			var result = new LookupProcessedValuesMemento {
				Results = new Dictionary<string, Dictionary<string, Guid>>()
			};
			foreach (var item in list) {
				AddMemento(result.Results, item.Results);
			}
			return result;
		}

		private void AddMemento(Dictionary<string, Dictionary<string, Guid>> results, Dictionary<string, Dictionary<string, Guid>> collection) {
			foreach (var item in collection) {
				if (!results.TryGetValue(item.Key, out Dictionary<string, Guid> value)) {
					results.Add(item.Key, item.Value);
				} else {
					item.Value.ForEach(i => value[i.Key] = i.Value);
				}
			}
		}

		private IEnumerable<ImportDataChunk<LookupChunkData>> GetOrCreateDataChuncks(ImportParameters importParameters) {
			if (!_importDataProvider.GetIsChunksExists(importParameters.ImportSessionId)) {
				_importDataProvider.CreateDataChunks(importParameters);
			}
			return _importDataProvider.Get(importParameters.ImportSessionId);
		}

		private void SendProcessErrorInternal(string cellReference, string message) {
			_importParameters.HasLookupProcessingErrors = true;
			_importParametersRepository.Update(_importParameters);
			SendProcessError(cellReference, message);
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Gets column destination default properties.
		/// </summary>
		/// <param name="entitySchemaColumn">Entity schema column.</param>
		/// <param name="columnValue">Column value information.</param>
		/// <returns>Column destination default properties.</returns>
		protected override Dictionary<string, object> GetColumnDestinationProperties(
			EntitySchemaColumn entitySchemaColumn, ImportColumnValue columnValue) {
			Dictionary<string, object> destinationProperties = base.GetColumnDestinationProperties(entitySchemaColumn,
				columnValue);
			destinationProperties.Add(ReferenceSchemaUIdPropertyName, entitySchemaColumn.ReferenceSchemaUId);
			return destinationProperties;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public object FindValueForSave(ImportColumnDestination destination, ImportColumnValue columnValue) {
			return _lookupProcessedValues.FindValueForSave(destination, columnValue);
		}

		/// <inheritdoc />
		public void Process(ImportParameters importParameters) {
			var lookupChunksData = GetOrCreateDataChuncks(importParameters);
			_importParameters = importParameters;
			ChunkLookupValuesHandler.ProcessError += SendProcessErrorInternal;
			try {
				foreach (var item in lookupChunksData.Where(l => l.State == ImportChunkState.ToProcess)) {
					if (importParameters.GetIsImportCancelled(UserConnection)) {
						break;
					}
					ChunkLookupValuesHandler.Execute(item);
				}
			} finally {
				ChunkLookupValuesHandler.ProcessError -= SendProcessErrorInternal;
			}
			_lookupProcessedValues = new LookupProcessedValues(UserConnection);
			_lookupProcessedValues.RestoreState(JoinLookupProcessedValues(lookupChunksData.Select(c => c.Data.ProcessedValuesState).ToList()));
		}

		#endregion

	}

	#endregion
}


