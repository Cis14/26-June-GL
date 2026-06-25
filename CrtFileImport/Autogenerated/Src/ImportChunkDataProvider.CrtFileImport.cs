namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Newtonsoft.Json;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;

	#region Class: ImportDataProvider

	/// <inheritdoc />
	public abstract class ImportChunkDataProvider<T> : IImportChunkDataProvider<ImportDataChunk<T>>
	{
		#region Fields: Protected

		/// <summary>
		/// Schema name for ImportSessionChunk.
		/// </summary>
		protected const string ImportSessionChunkSchemaName = "ImportSessionChunk";

		/// <summary>
		/// Rows count part for inserting.
		/// </summary>
		protected int ImportDataPartSize = 349;

		protected readonly UserConnection UserConnection;

		#endregion

		#region Constructors: Public

		public ImportChunkDataProvider(UserConnection userConnection) {
			UserConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		private IEnumerable<IEnumerable<ImportDataChunk<T>>> GetImportDataParts(IEnumerable<ImportDataChunk<T>> importData) {
			return importData.SplitOnChunks(ImportDataPartSize);
		}

		private byte[] GetSerialize(T chunkData) {
			return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(chunkData));
		}

		private T GetDeserialize(byte[] chunkData) {
			return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(chunkData));
		}
		
		private void AddValuesToInsertQuery(Insert insert, ImportDataChunk<T> importDataChunk) {
			insert.Set("Id", Column.Parameter(importDataChunk.ChunkId));
			insert.Set("CreatedOn", Column.Parameter(DateTime.Now));
			insert.Set("Type", Column.Parameter((int)importDataChunk.Type));
			insert.Set("State", Column.Parameter((int)importDataChunk.State));
			insert.Set("Data", Column.Parameter(GetSerialize(importDataChunk.Data)));
			insert.Set("ImportParametersId", Column.Parameter(importDataChunk.ImportSessionId));
		}

		private static void PreparePageableOptions(EntitySchemaQueryOptions pageOptions, Entity lastEntity,
			string primaryColumnName) {
			DateTime lastEntityCreatedOn = lastEntity.GetTypedColumnValue<DateTime>("CreatedOn");
			Guid lastEntityId = lastEntity.PrimaryColumnValue;
			pageOptions.PageableConditionValues.Clear();
			pageOptions.PageableConditionValues.Add("CreatedOn", lastEntityCreatedOn);
			pageOptions.PageableConditionValues.Add(primaryColumnName, lastEntityId);
		}

		private ImportDataChunk<T> ConvertToChunk(Entity entity) {
			Enum.TryParse<ImportChunkType>(entity.GetColumnValue("Type").ToString(), true, out var type);
			Enum.TryParse<ImportChunkState>(entity.GetColumnValue("State").ToString(), true, out var state);
			T data = GetDeserialize(entity.GetStreamValue("Data").ToArray());
			return new ImportDataChunk<T>() {
				ImportSessionId = entity.GetTypedColumnValue<Guid>("ImportParametersId"),
				ChunkId = entity.PrimaryColumnValue,
				Type = type,
				State = state,
				Data = data
			};
		}

		#endregion
		
		#region Methods: Protected

		/// <summary>
		/// Type import chunk.
		/// </summary>
		/// <returns>Type import chunk.</returns>
		protected abstract ImportChunkType GetImportChunkType();

		protected void Add(ImportDataChunk<T> importDataChunk) {
			var insert = new Insert(UserConnection).Into(ImportSessionChunkSchemaName);
			AddValuesToInsertQuery(insert, importDataChunk);
			insert.Execute();
		}

		#endregion

		#region Methods: Public

		
		private EntitySchemaQuery GetChunkQuery(Guid importSessionId) {
			var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, ImportSessionChunkSchemaName);
			esq.PrimaryQueryColumn.IsVisible = true;
			var createdOnColumn = esq.AddColumn("CreatedOn");
			createdOnColumn.OrderByAsc(1);
			esq.PrimaryQueryColumn.OrderByAsc(2);
			esq.AddColumn("Type");
			esq.AddColumn("State");
			esq.AddColumn("Data");
			esq.AddColumn("ImportParameters");
			esq.Filters.LogicalOperation = LogicalOperationStrict.And;
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "ImportParameters",
				importSessionId));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Type",
				(int)GetImportChunkType()));
			
			return esq;
		}
		
		/// <inheritdoc />
		public IEnumerable<ImportDataChunk<T>> Get(Guid importSessionId) {
			var esq = GetChunkQuery(importSessionId);
			int pageSize = 1;
			EntitySchemaQueryOptions pageOptions = new EntitySchemaQueryOptions {
				PageableDirection = PageableSelectDirection.First,
				PageableRowCount = pageSize,
				PageableConditionValues = new Dictionary<string, object>()
			};
			EntityCollection entityCollection = esq.GetEntityCollection(UserConnection, pageOptions);
			if (entityCollection.Count == 0) {
				yield break;
			}
			yield return ConvertToChunk(entityCollection.Last.Value);

			pageOptions.PageableDirection = PageableSelectDirection.Next;
			pageOptions.PageableConditionValues.Clear();

			bool useOffsetFetchPaging = GlobalAppSettings.UseOffsetFetchPaging;
			int pageOffset = entityCollection.Count;
			string primaryColumnName = esq.PrimaryQueryColumn.Name;
			do {
				if (useOffsetFetchPaging) {
					pageOptions.RowsOffset = pageOffset;
					esq.ResetSelectQuery();
				} else {
					PreparePageableOptions(pageOptions, entityCollection.Last.Value, primaryColumnName);
				}
				entityCollection = esq.GetEntityCollection(UserConnection, pageOptions);
				if (entityCollection.Count == 0) {
					yield break;
				}
				yield return ConvertToChunk(entityCollection.Last.Value);
				pageOffset += entityCollection.Count;
			} while (entityCollection.Count == pageSize);
		}

		/// <inheritdoc />
		public int Add(IEnumerable<ImportDataChunk<T>> importDataChunk) {
			var insertRowsCount = 0;
			var importDataPart = GetImportDataParts(importDataChunk);
			importDataPart.AsParallel().ForEach(
				importDataPartsPart => {
					var insert = new Insert(UserConnection).Into(ImportSessionChunkSchemaName);
					foreach(var importData in importDataPartsPart) {
						insert.Values();
						AddValuesToInsertQuery(insert, importData);
					}
					insertRowsCount += insert.Execute();
				}
			);
			return insertRowsCount;
		}

		/// <inheritdoc />
		public bool Update(ImportDataChunk<T> importData) {
			var update = new Update(UserConnection, ImportSessionChunkSchemaName)
				.Set("State", Column.Parameter((int)importData.State))
				.Set("Data", Column.Parameter(GetSerialize(importData.Data)))
				.Where("ImportParametersId").IsEqual(Column.Parameter(importData.ImportSessionId))
				.And("Id").IsEqual(Column.Parameter(importData.ChunkId))
				.And("Type").IsEqual(Column.Parameter((int)importData.Type)) as Update;
			return update.Execute() > 0;
		}

		public abstract void CreateDataChunks(ImportParameters importParameters);

		/// <inheritdoc />
		public bool GetIsChunksExists(Guid importSessionId) {
			var importChunkType = GetImportChunkType();
			var select = new Select(UserConnection)
				.Top(1)
				.Column(Column.Const(true)).As("ChunkExists")
				.From(ImportSessionChunkSchemaName)
				.Where("ImportParametersId")
				.IsEqual(Column.Parameter(importSessionId))
				.And("Type").IsEqual(Column.Parameter((int)importChunkType)) as Select;
			return select.ExecuteScalar<bool>();
		}

		/// <inheritdoc />
		public void Delete(Guid importSessionId) {
			var importChunkType = GetImportChunkType();
			var delete = new Delete(UserConnection)
				.From(ImportSessionChunkSchemaName)
				.Where("ImportParametersId")
					.IsEqual(Column.Parameter(importSessionId))
					.And("Type").IsEqual(Column.Parameter((int)importChunkType));
			delete.Execute();
		}

		#endregion
	}

	#endregion

}
