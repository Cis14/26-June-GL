namespace Terrasoft.Configuration
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.SystemSettings.Notification;
	using Terrasoft.Core.SystemSettings.Objects;
	using Terrasoft.Configuration.CrtFiscalPeriods;
	using CoreSysSettings = Terrasoft.Core.Configuration.SysSettings;

	#region Class: FiscalYearSysSettingsListener

	/// <summary>
	/// Listens for changes of fiscal-year-related system settings and triggers fiscal period generation.
	/// Implemented via ISysSettingsListener for UpdateSysSettingRequest pipeline.
	/// </summary>
	[DefaultBinding(typeof(ISysSettingsListener), Name = "FiscalYearSysSettingsListener")]
	public class FiscalYearSysSettingsListener : ISysSettingsListener
	{
		#region Constants: Private

		private const string FiscalYearStartDateCode = "FiscalYearStartDate";
		private const string FiscalYearRangeCode = "FiscalYearRange";
		private const string FiscalWeekStartDayCode = "FiscalWeekStartDay";
		private const string FiscalPeriodYearNameTemplateCode = "FiscalPeriodYearNameTemplate";
		private const string FiscalPeriodMonthNameTemplateCode = "FiscalPeriodMonthNameTemplate";
		private const string FiscalPeriodQuarterNameTemplateCode = "FiscalPeriodQuarterNameTemplate";
		private const string FiscalPeriodWeekNameTemplateCode = "FiscalPeriodWeekNameTemplate";
		private const string FiscalPeriodHalfYearNameTemplateCode = "FiscalPeriodHalfYearNameTemplate";
		private const int DefaultFiscalYearStartMonth = 1;
		private const int DefaultFiscalYearRange = 5;
		private const int FiscalYearDefaultYear = 2000;

		#endregion

		#region Methods: Private

		/// <summary>
		/// Determines whether the specified system setting code is related to fiscal year configuration
		/// that triggers period regeneration.
		/// </summary>
		/// <param name="code">The system setting code to check.</param>
		/// <returns>True if the code is a fiscal year setting; otherwise, false.</returns>
		private static bool IsFiscalYearSetting(string code) {
			return string.Equals(code, FiscalYearStartDateCode, StringComparison.Ordinal)
				|| string.Equals(code, FiscalYearRangeCode, StringComparison.Ordinal)
				|| string.Equals(code, FiscalWeekStartDayCode, StringComparison.Ordinal);
		}

		/// <summary>
		/// Determines whether the specified system setting code is related to fiscal period name templates
		/// that triggers name updates for existing periods.
		/// </summary>
		/// <param name="code">The system setting code to check.</param>
		/// <returns>True if the code is a fiscal period template setting; otherwise, false.</returns>
		private static bool IsFiscalYearTemplateSetting(string code) {
			return string.Equals(code, FiscalPeriodYearNameTemplateCode, StringComparison.Ordinal)
				|| string.Equals(code, FiscalPeriodMonthNameTemplateCode, StringComparison.Ordinal)
				|| string.Equals(code, FiscalPeriodQuarterNameTemplateCode, StringComparison.Ordinal)
				|| string.Equals(code, FiscalPeriodWeekNameTemplateCode, StringComparison.Ordinal)
				|| string.Equals(code, FiscalPeriodHalfYearNameTemplateCode, StringComparison.Ordinal);
		}

		/// <summary>
		/// Retrieves the fiscal year configuration including start month and year range.
		/// Returns default values if the settings are not properly configured.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <returns>A tuple containing the fiscal year start month and range in years.</returns>
		private static (int startMonth, int rangeYears) GetFiscalConfig(UserConnection userConnection) {
			DateTime date = CoreSysSettings.GetValue<DateTime>(userConnection, FiscalYearStartDateCode, default);
			if (date == default) {
				date = new DateTime(FiscalYearDefaultYear, DefaultFiscalYearStartMonth, 1);
			}
			int month = date.Month;
			if (month < 1 || month > 12) {
				month = DefaultFiscalYearStartMonth;
			}
			int rangeYears = CoreSysSettings.GetValue<int>(userConnection, FiscalYearRangeCode, 0);
			if (rangeYears <= 0) {
				rangeYears = DefaultFiscalYearRange;
			}
			return (month, rangeYears);
		}

		/// <summary>
		/// Generates fiscal periods based on the current fiscal year configuration.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		private static void GenerateFiscalPeriods(UserConnection userConnection) {
			var (startMonth, rangeYears) = GetFiscalConfig(userConnection);
			var generator = new FiscalPeriodGenerator(userConnection);
			generator.GenerateFiscalPeriods(startMonth, rangeYears);
		}

		/// <summary>
		/// Updates the names of existing fiscal periods when a template setting changes.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="templateCode">The system setting code of the changed template.</param>
		private static void UpdatePeriodNamesForTemplate(UserConnection userConnection, string templateCode) {
			var generator = new FiscalPeriodGenerator(userConnection);
			generator.UpdatePeriodNamesForTemplateChange(templateCode);
		}
		
		#endregion

		#region Methods: Public
		
		public void OnSysSettingsChanged(UserConnection userConnection, ISysSettings sysSettings) {
			// Not used.
		}

		public void OnSysSettingsRightsChanged(UserConnection userConnection, ISysSettings sysSettings,
			ISysSettingsRights sysSettingsRights) {
			// Not used.
		}

		public void OnSysSettingsValueChanged(UserConnection userConnection, ISysSettings sysSettings,
			Guid sysAdminUnitId, object sysSettingsValue) {
			if (sysSettings?.Code == null) {
				return;
			}
			if (IsFiscalYearTemplateSetting(sysSettings.Code)) {
				UpdatePeriodNamesForTemplate(userConnection, sysSettings.Code);
			} else if (IsFiscalYearSetting(sysSettings.Code)) {
				GenerateFiscalPeriods(userConnection);
			}
		}
		
		#endregion
	}

	#endregion
}


