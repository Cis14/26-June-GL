namespace IntegrationV2.MailboxDomain.Repository
{
	using Terrasoft.Core;
	using Terrasoft.Core.Store;

	#region Class: BaseRepository

	/// <summary>
	/// Base repository implementation.
	/// </summary>
	internal abstract class BaseRepository
	{
		#region Fields: Private

		/// <summary>
		/// Collection of cached repository items.
		/// </summary>
		private object _cache;

		#endregion

		#region Fields: Protected

		/// <summary>
		/// <see cref="UserConnection"/> instance.
		/// </summary>
		protected UserConnection UserConnection;

		/// <summary>
		/// Repository cache name.
		/// </summary>
		protected string CacheName;

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Returns repository cache.
		/// </summary>
		/// <returns>Repository cache.</returns>
		protected object GetCache() {
			if (_cache == null) {
				ICacheStore applicationCache = UserConnection.ApplicationCache;
				object store = applicationCache[CacheName];
				_cache = store;
			}
			return _cache;
		}

		/// <summary>
		/// Sets value in repository cache.
		/// </summary>
		protected void SetCache(object value) {
			ICacheStore applicationCache = UserConnection.ApplicationCache;
			applicationCache[CacheName] = value;
			_cache = value;
		}

		#endregion

	}

	#endregion

}
