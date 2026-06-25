namespace Creatio.Copilot
{
	using System;
	using System.Linq;
	using Terrasoft.Configuration.GenAI;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Store;

	#region Class: LlmModelCategoryRepository

	[DefaultBinding(typeof(ILlmModelCategoryRepository))]
	public class LlmModelCategoryRepository : ILlmModelCategoryRepository
	{

		#region Constants: Private

		private const string CacheKeyPrefix = "LlmModelCategory:";

		#endregion

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private readonly ICacheStore _cache;

		#endregion

		#region Constructors: Public

		public LlmModelCategoryRepository(UserConnection userConnection) {
			_userConnection = userConnection;
			_cache = userConnection.ApplicationCache.WithLocalCaching();
		}

		#endregion

		#region Methods: Private

		private string GetCacheKey(string code) {
			return $"{CacheKeyPrefix}{code}";
		}

		private LlmModelReference FetchFromDatabase(string code) {
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "LlmModelCategory");
			var idColumn = esq.AddColumn("Id");
			var codeColumn = esq.AddColumn("DefaultLlmModel.Code");
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Code", code));
			EntityCollection entities = esq.GetEntityCollection(_userConnection);
			Entity entity = entities.FirstOrDefault();
			if (entity != null) {
				return new LlmModelReference {
					Id = entity.GetTypedColumnValue<Guid>(idColumn.Name),
					Code = entity.GetTypedColumnValue<string>(codeColumn.Name)
				};
			}
			return new LlmModelReference();
		}

		#endregion

		#region Methods: Public

		public LlmModelReference FindByCategory(string code) {
			if (string.IsNullOrEmpty(code)) {
				return null;
			}
			var cacheKey = GetCacheKey(code);
			if (_cache[cacheKey] is LlmModelReference cachedResult) {
				return cachedResult;
			}
			var result = FetchFromDatabase(code);
			_cache[cacheKey] = result;
			return result;
		}

		public void ClearCache() {
			var knownCategories = new[] { LlmModelCategoryCode.Default, LlmModelCategoryCode.Mini };
			foreach (var category in knownCategories) {
				var cacheKey = GetCacheKey(category);
				_cache.Remove(cacheKey);
			}
		}

		#endregion

	}

	#endregion

}

