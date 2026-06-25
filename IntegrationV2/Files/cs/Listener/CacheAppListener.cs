namespace IntegrationV2.Files.cs.Listener {
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Store;
	using Terrasoft.Web.Common;

	#region Class: CacheAppListener

	public class CacheAppListener: AppEventListenerBase {

		#region Fields: Private

		private AppEventContext _appEventContext;
		private AppConnection _appConnection;
		private UserConnection _userConnection;

		#endregion

		#region Properties: Private

		private UserConnection UserConnection {
			get {
				return _userConnection ?? (_userConnection = AppConnection.SystemUserConnection);
			}
			set {
				_userConnection = value;
			}
		}

		private AppConnection AppConnection {
			get {
				if (_appConnection == null) {
					_appEventContext.CheckArgumentNull("AppEventContext");
					_appConnection = _appEventContext.Application["AppConnection"] as AppConnection;
				}
				return _appConnection;
			}
			set {
				_appConnection = value;
			}
		}

		#endregion

		#region Methods: Private

		private void ClearIntegrationCache() {
			try {
				ICacheStore applicationCache = UserConnection.ApplicationCache;
				applicationCache.Remove("MailboxFolderList");
				applicationCache.Remove("MailServerList");
				applicationCache.Remove("CalendarList");
				applicationCache.Remove("MailboxList");
			} catch { }
		}

		#endregion

		#region Methods: Public

		public override void OnAppStart(AppEventContext context) {
			_appEventContext = context;
			ClearIntegrationCache();
		}

		#endregion

	}

	#endregion

}
