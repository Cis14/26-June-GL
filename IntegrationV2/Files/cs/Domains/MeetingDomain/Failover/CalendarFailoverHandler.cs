namespace Terrasoft.Configuration
{
	using System.Collections.Generic;
	using global::IntegrationV2.Files.cs.Domains.MeetingDomain;
	using IntegrationApi.Interfaces;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Factories;
	using Terrasoft.IntegrationV2.Logging.Interfaces;
	using Terrasoft.IntegrationV2.Utils;

	#region Class: CalendarFailoverHandler

	/// <summary>
	/// Class for starting periodic calendar sync process.
	/// </summary>
	public class CalendarFailoverHandler : IJobExecutor
	{

		#region Methods: Private

		private int GetPeriod(UserConnection uc, IDictionary<string, object> parameters) {
			var period = (int)parameters["PeriodInMinutes"];
			return parameters.ContainsKey("ProccessName") && (string)parameters["ProccessName"] != ExchangeConsts.ActivitySyncProcessName
				? SysSettings.GetValue(uc, "GoogleCalendarSynchInterval", period)
				: period;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Starts periodic calendar sync process.
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/> instance.</param>
		/// <param name="parameters">Parameters collection.</param>
		public void Execute(UserConnection userConnection, IDictionary<string, object> parameters) {
			var log = ClassFactory.Get<ISynchronizationLogger>();
			log.DebugFormat("CalendarFailoverHandler started");
			if (!userConnection.LicHelper.GetHasExplicitlyLicensedOperationLicense(LicenseConsts.CalendarSynchronization)) {
				log.Warn($"Calendar synchronization process is not licensed. Please request CalendarSynchronization.Use license.");
				return;
			}
			if (ListenerUtils.GetIsFeatureDisabled("NewMeetingIntegration")) {
				log.DebugFormat("NewMeetingIntegration feature disabled, CalendarFailoverHandler ended");
				return;
			}
			var period = GetPeriod(userConnection, parameters);
			var syncJobScheduler = ClassFactory.Get<ISyncJobScheduler>();
			var processName = parameters.ContainsKey("ProccessName") 
				? parameters["ProccessName"].ToString()
				: ExchangeConsts.ActivitySyncProcessName;
			syncJobScheduler.CreateSyncJob(userConnection, period, processName, parameters);
			log.DebugFormat("CalendarFailoverHandler ended");
		}

		#endregion

	}

	#endregion

}