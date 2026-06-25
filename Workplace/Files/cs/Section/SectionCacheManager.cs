namespace Terrasoft.Configuration.Section
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using Core;
	using Core.DB;
	using Core.Factories;
	using Core.Store;
	using Terrasoft.Configuration.Domain;
	using Terrasoft.Common;

	#region Class: SectionCacheManager

	[DefaultBinding(typeof(ISectionCacheManager))]
	public class SectionCacheManager : ISectionCacheManager {

		#region Fields: Private

		private readonly ICacheStore _sessionCache;
		private readonly UserConnection _userConnection;
		private static readonly SectionType[] SectionTypes = { SectionType.General, SectionType.SSP };

		#endregion

		#region Constructors: Public

		public SectionCacheManager(ICacheStore sessionCache, UserConnection userConnection) {
			_sessionCache = sessionCache;
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		private string GetAllKey(string scope) => $"All_Sections_{scope}";
		private string GetTypeKey(string scope, SectionType type) => $"Sections_{type}_{scope}";

		private List<Section> GetOrAdd(string key, Func<List<Section>> factory) {
			var cached = _sessionCache[key] as List<Section>;
			if (cached != null && cached.Any()) {
				return cached;
			}
			var data = factory();
			_sessionCache[key] = data;
			return data;
		}

		private List<string> GetAllApplicationClientTypeIds() {
			string cacheKey = "ApplicationClientTypeIds";
			var ids = _sessionCache[cacheKey] as List<string>;
			if (ids != null) {
				return ids;
			}

			ids = new List<string>();
			var select = new Select(_userConnection)
				.Column("Id")
				.From("SysApplicationClientType");
			using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection()) {
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						var id = dataReader.GetColumnValue<Guid>("Id").ToString();
						if (!ids.Contains(id)) {
							ids.Add(id);
						}
					}
				}
			}
			_sessionCache[cacheKey] = ids;
			return ids;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="ISectionCacheManager.GetAll"/>
		public List<Section> GetAll(string scopeKey, Func<List<Section>> factory) {
			scopeKey = scopeKey ?? ClientTypes.BrowserClientTypeId.ToString();
			return GetOrAdd(GetAllKey(scopeKey), factory);
		}

		/// <inheritdoc cref="ISectionCacheManager.GetByType"/>
		public List<Section> GetByType(string scopeKey, SectionType type, Func<List<Section>> factory) {
			scopeKey = scopeKey ?? ClientTypes.BrowserClientTypeId.ToString();
			return GetOrAdd(GetTypeKey(scopeKey, type), factory);
		}

		/// <inheritdoc cref="ISectionCacheManager.Clear"/>
		public void Clear() {
			var scopes = GetAllApplicationClientTypeIds();
			foreach (var scope in scopes) {
				_sessionCache.Remove(GetAllKey(scope));
				foreach (var type in SectionTypes) {
					_sessionCache.Remove(GetTypeKey(scope, type));
				}
			}
		}

		#endregion

	}

	#endregion
}
