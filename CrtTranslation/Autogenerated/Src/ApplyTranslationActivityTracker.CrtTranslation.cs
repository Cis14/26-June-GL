namespace Terrasoft.Configuration.Translation
{
	using System;
	using Creatio.FeatureToggling;
	using Terrasoft.Core;

	#region Class: ApplyTranslationActivityTracker

	public class ApplyTranslationActivityTracker
	{
		#region Constants: Private

		public const string ApplySessionKeyTemplate = "ApplyTranslations_{0}";

		#endregion

		#region Fields: Private

		private readonly object _applySessionUpdateLock = new object();
		private DateTime? _lastApplySessionUpdateUtc;

		#endregion

		#region Methods: Public

		public void UpdateApplySessionActiveTime(UserConnection userConnection, object applySessionId) {
			if (Features.GetIsDisabled("UseApplyTranslationSessionActivityTracking")) {
				return;
			}
			var now = DateTime.UtcNow;
			var applySessionKey = string.Format(ApplySessionKeyTemplate, applySessionId);
			lock (_applySessionUpdateLock) {
				if (_lastApplySessionUpdateUtc.HasValue && (now - _lastApplySessionUpdateUtc.Value).TotalSeconds < 30) {
					return;
				}
				userConnection.ApplicationCache[applySessionKey] = now;
				_lastApplySessionUpdateUtc = now;
			}
		}

		#endregion

	}

	#endregion

}
