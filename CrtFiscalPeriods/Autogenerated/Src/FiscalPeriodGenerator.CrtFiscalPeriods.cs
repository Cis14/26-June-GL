using System;
using System.Globalization;
using Terrasoft.Core;
using Terrasoft.Core.DB;
using CoreSysSettings = Terrasoft.Core.Configuration.SysSettings;

namespace Terrasoft.Configuration.CrtFiscalPeriods
{
	/// <summary>
	/// Generates and manages fiscal period records including years, quarters, months, weeks, and half-years.
	/// </summary>
	public class FiscalPeriodGenerator
	{
		#region Constants: Private

		private const string FiscalYearTemplateCode = "FiscalPeriodYearNameTemplate";
		private const string FiscalHalfYearTemplateCode = "FiscalPeriodHalfYearNameTemplate";
		private const string FiscalQuarterTemplateCode = "FiscalPeriodQuarterNameTemplate";
		private const string FiscalMonthTemplateCode = "FiscalPeriodMonthNameTemplate";
		private const string FiscalWeekTemplateCode = "FiscalPeriodWeekNameTemplate";
		private const string FiscalWeekStartDayCode = "FiscalWeekStartDay";
		private const int DefaultWeekStartDay = 2;
		private const int MonthsPerQuarter = 3;
		private const int MonthsPerHalfYear = 6;
		private const int MonthsPerYear = 12;
		private const int QuartersPerYear = 4;
		private const int HalfYearsPerYear = 2;
		private const int DaysPerWeek = 7;

		#endregion

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private readonly string _fiscalYearTemplate;
		private readonly string _fiscalQuarterTemplate;
		private readonly string _fiscalMonthTemplate;
		private readonly string _fiscalWeekTemplate;
		private readonly string _fiscalHalfYearTemplate;

		#endregion

		#region Constructors: Public

		public FiscalPeriodGenerator(UserConnection userConnection) {
			_userConnection = userConnection ?? throw new ArgumentNullException(nameof(userConnection));
			_fiscalYearTemplate = GetTemplate(FiscalYearTemplateCode, "Fiscal Year {0:yyyy}-{1:yyyy}");
			_fiscalQuarterTemplate = GetTemplate(FiscalQuarterTemplateCode, "Fiscal Quarter {0} {1:yyyy}-{2:yyyy}");
			_fiscalMonthTemplate = GetTemplate(FiscalMonthTemplateCode, "Fiscal Month {0:00} {1:yyyy}-{2:yyyy}");
			_fiscalWeekTemplate = GetTemplate(FiscalWeekTemplateCode, "Fiscal Week {0:00} {1:yyyy}-{2:yyyy}");
			_fiscalHalfYearTemplate = GetTemplate(FiscalHalfYearTemplateCode, "{0} Fiscal Half Year {1:yyyy}-{2:yyyy}");
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Generates fiscal periods for years in the range [currentYear - fiscalYearRange, currentYear + fiscalYearRange]
		/// using the specified fiscal year start month. Ensures that required PeriodType records exist..
		/// </summary>
		/// <param name="fiscalYearStartMonth">Month (1-12) that fiscal year starts.</param>
		/// <param name="fiscalYearRange">Number of years before and after the current year to generate periods for.</param>
		public void GenerateFiscalPeriods(int fiscalYearStartMonth, int fiscalYearRange) {
			if (fiscalYearStartMonth < 1 || fiscalYearStartMonth > 12) {
				throw new ArgumentOutOfRangeException(nameof(fiscalYearStartMonth),
					"Fiscal year start month must be between 1 and 12.");
			}
			if (fiscalYearRange < 0) {
				throw new ArgumentOutOfRangeException(nameof(fiscalYearRange),
					"Fiscal year range must be non-negative.");
			}

			DateTime now = _userConnection.CurrentUser.GetCurrentDateTime();
			int currentYear = now.Year;
			int startYear = currentYear - fiscalYearRange;
			int endYear = currentYear + fiscalYearRange;

			EnsurePeriodType(FiscalPeriodTypeConsts.FiscalMonthTypeId, "Fiscal Month");
			EnsurePeriodType(FiscalPeriodTypeConsts.FiscalQuarterTypeId, "Fiscal Quarter");
			EnsurePeriodType(FiscalPeriodTypeConsts.FiscalYearTypeId, "Fiscal Year");
			EnsurePeriodType(FiscalPeriodTypeConsts.FiscalHalfYearTypeId, "Fiscal Half Year");
			EnsurePeriodType(FiscalPeriodTypeConsts.FiscalWeekTypeId, "Fiscal Week");

			for (int year = startYear; year <= endYear; year++) {
				GeneratePeriodsForFiscalYear(year, fiscalYearStartMonth);
			}
		}

		/// <summary>
		/// Updates the names of all existing fiscal periods of a specific type to match the new template.
		/// </summary>
		/// <param name="templateCode">The system setting code of the template that changed.</param>
		public void UpdatePeriodNamesForTemplateChange(string templateCode) {
			if (string.IsNullOrEmpty(templateCode)) {
				throw new ArgumentNullException(nameof(templateCode));
			}

			Guid? periodTypeId = GetPeriodTypeIdForTemplateCode(templateCode);
			if (!periodTypeId.HasValue) {
				// Unknown template code, nothing to update
				return;
			}

			string newTemplate = GetTemplateForCode(templateCode);

			var select = new Select(_userConnection)
					.Column("Id")
					.Column("StartDate")
					.Column("DueDate")
					.Column("YearId")
				.From("Period")
				.Where("PeriodTypeId").IsEqual(Column.Parameter(periodTypeId.Value)) as Select;

			using (var dbExecutor = _userConnection.EnsureDBConnection()) {
				using (var reader = select.ExecuteReader(dbExecutor)) {
					while (reader.Read()) {
						Guid periodId = (Guid)reader["Id"];
						DateTime startDate = (DateTime)reader["StartDate"];
						DateTime dueDate = (DateTime)reader["DueDate"];
						Guid yearId = reader["YearId"] != DBNull.Value ? (Guid)reader["YearId"] : Guid.Empty;

						string newName = CalculatePeriodName(periodTypeId.Value, templateCode, newTemplate,
							periodId, startDate, dueDate, yearId);

						if (!string.IsNullOrEmpty(newName)) {
							var update = new Update(_userConnection, "Period")
								.Set("Name", Column.Parameter(newName))
								.Where("Id").IsEqual(Column.Parameter(periodId));
							update.Execute();
						}
					}
				}
			}
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Finds an existing period by type, name, and optional parent filters.
		/// </summary>
		/// <param name="periodTypeId">The period type ID.</param>
		/// <param name="name">The period name.</param>
		/// <param name="yearId">Optional year ID filter.</param>
		/// <param name="quarterId">Optional quarter ID filter.</param>
		/// <returns>The period ID if found, otherwise Guid.Empty.</returns>
		private Guid FindExistingPeriod(Guid periodTypeId, string name, Guid? yearId = null, Guid? quarterId = null) {
			var select = new Select(_userConnection)
				.Column("Id")
				.From("Period")
				.Where("PeriodTypeId").IsEqual(Column.Parameter(periodTypeId))
				.And("Name").IsEqual(Column.Parameter(name)) as Select;

			if (yearId.HasValue) {
				select = select.And("YearId").IsEqual(Column.Parameter(yearId.Value)) as Select;
			}

			if (quarterId.HasValue) {
				select = select.And("QuarterId").IsEqual(Column.Parameter(quarterId.Value)) as Select;
			}

			return select.ExecuteScalar<Guid>();
		}

		/// <summary>
		/// Updates the start and due dates of an existing period.
		/// </summary>
		/// <param name="periodId">The period ID to update.</param>
		/// <param name="startDate">The new start date.</param>
		/// <param name="dueDate">The new due date.</param>
		private void UpdatePeriodDates(Guid periodId, DateTime startDate, DateTime dueDate) {
			var update = new Update(_userConnection, "Period")
				.Set("StartDate", Column.Parameter(startDate))
				.Set("DueDate", Column.Parameter(dueDate))
				.Where("Id").IsEqual(Column.Parameter(periodId));
			update.Execute();
		}

		/// <summary>
		/// Inserts a new period record.
		/// </summary>
		/// <param name="periodId">The new period ID.</param>
		/// <param name="name">The period name.</param>
		/// <param name="startDate">The start date.</param>
		/// <param name="dueDate">The due date.</param>
		/// <param name="periodTypeId">The period type ID.</param>
		/// <param name="yearId">Optional year ID.</param>
		/// <param name="quarterId">Optional quarter ID.</param>
		private void InsertPeriod(Guid periodId, string name, DateTime startDate, DateTime dueDate, 
			Guid periodTypeId, Guid? yearId = null, Guid? quarterId = null) {
			var insert = new Insert(_userConnection)
				.Into("Period")
				.Set("Id", Column.Parameter(periodId))
				.Set("Name", Column.Parameter(name))
				.Set("StartDate", Column.Parameter(startDate))
				.Set("DueDate", Column.Parameter(dueDate))
				.Set("PeriodTypeId", Column.Parameter(periodTypeId));

			if (yearId.HasValue) {
				insert = insert.Set("YearId", Column.Parameter(yearId.Value));
			}

			if (quarterId.HasValue) {
				insert = insert.Set("QuarterId", Column.Parameter(quarterId.Value));
			}

			insert.Execute();
		}

		/// <summary>
		/// Creates a new period or updates an existing one. If the period exists, updates its dates.
		/// If it doesn't exist, creates it with the provided information.
		/// </summary>
		/// <param name="periodTypeId">The period type ID.</param>
		/// <param name="name">The period name.</param>
		/// <param name="startDate">The start date.</param>
		/// <param name="dueDate">The due date.</param>
		/// <param name="yearId">Optional year ID.</param>
		/// <param name="quarterId">Optional quarter ID.</param>
		/// <returns>The period ID (existing or newly created).</returns>
		private Guid CreateOrUpdatePeriod(Guid periodTypeId, string name, DateTime startDate, DateTime dueDate,
			Guid? yearId = null, Guid? quarterId = null) {
			Guid existingId = FindExistingPeriod(periodTypeId, name, yearId, quarterId);

			if (existingId != Guid.Empty) {
				UpdatePeriodDates(existingId, startDate, dueDate);
				return existingId;
			}

			Guid newId = Guid.NewGuid();
			InsertPeriod(newId, name, startDate, dueDate, periodTypeId, yearId, quarterId);
			return newId;
		}

		/// <summary>
		/// Ensures that a PeriodType record exists with the specified ID and name.
		/// Creates it if it doesn't exist.
		/// </summary>
		/// <param name="periodTypeId">The unique identifier for the period type.</param>
		/// <param name="name">The name of the period type.</param>
		private void EnsurePeriodType(Guid periodTypeId, string name) {
			var select = new Select(_userConnection)
					.Column("Id")
				.From("PeriodType")
				.Where("Id").IsEqual(Column.Parameter(periodTypeId)) as Select;
			
			Guid existingId = select.ExecuteScalar<Guid>();
			if (existingId != Guid.Empty) {
				return;
			}
			
			var insert = new Insert(_userConnection)
				.Into("PeriodType")
				.Set("Id", Column.Parameter(periodTypeId))
				.Set("Name", Column.Parameter(name));
			insert.Execute();
		}

		/// <summary>
		/// Generates all fiscal periods (year, quarters, half-years, months, weeks) for a given fiscal year.
		/// </summary>
		/// <param name="year">The calendar year to generate fiscal periods for.</param>
		/// <param name="fiscalYearStartMonth">The month (1-12) when the fiscal year starts.</param>
		private void GeneratePeriodsForFiscalYear(int year, int fiscalYearStartMonth) {
			DateTime fiscalYearStart = new DateTime(year, fiscalYearStartMonth, 1);
			DateTime fiscalYearEnd = fiscalYearStart.AddYears(1).AddDays(-1);

			Guid yearId = EnsureYearPeriod(year, fiscalYearStart, fiscalYearEnd);
			Guid[] quarterIds = EnsureQuarterPeriods(yearId, fiscalYearStart, fiscalYearEnd);
			EnsureHalfYearPeriods(yearId, fiscalYearStart, fiscalYearEnd);
			EnsureMonthPeriods(yearId, quarterIds, fiscalYearStart, fiscalYearEnd);
			EnsureWeekPeriods(yearId, fiscalYearStart, fiscalYearEnd);
		}

		/// <summary>
		/// Ensures a fiscal year period exists. Creates if missing, updates dates if exists.
		/// </summary>
		/// <param name="yearNumber">The year number.</param>
		/// <param name="start">The fiscal year start date.</param>
		/// <param name="end">The fiscal year end date.</param>
		/// <returns>The ID of the year period.</returns>
		private Guid EnsureYearPeriod(int yearNumber, DateTime start, DateTime end) {
			string name = FormatTemplate(_fiscalYearTemplate, start, end);
			return CreateOrUpdatePeriod(FiscalPeriodTypeConsts.FiscalYearTypeId, name, start, end);
		}

		/// <summary>
		/// Ensures all four fiscal quarter periods exist for a given fiscal year.
		/// Creates if missing, updates dates if exists.
		/// </summary>
		/// <param name="yearId">The fiscal year period ID.</param>
		/// <param name="fiscalYearStart">The fiscal year start date.</param>
		/// <param name="fiscalYearEnd">The fiscal year end date.</param>
		/// <returns>Array of four quarter period IDs.</returns>
		private Guid[] EnsureQuarterPeriods(Guid yearId, DateTime fiscalYearStart, DateTime fiscalYearEnd) {
			Guid[] quarterIds = new Guid[QuartersPerYear];
			DateTime quarterStart = fiscalYearStart;
			
			for (int q = 1; q <= QuartersPerYear; q++) {
				DateTime quarterEnd = quarterStart.AddMonths(MonthsPerQuarter).AddDays(-1);
				if (quarterEnd > fiscalYearEnd) {
					quarterEnd = fiscalYearEnd;
				}

			string name = FormatTemplate(_fiscalQuarterTemplate, q, fiscalYearStart, fiscalYearEnd);
			quarterIds[q - 1] = CreateOrUpdatePeriod(FiscalPeriodTypeConsts.FiscalQuarterTypeId, name, 
				quarterStart, quarterEnd, yearId);
			quarterStart = quarterEnd.AddDays(1);
			}
			
			return quarterIds;
		}

		/// <summary>
		/// Ensures both fiscal half-year periods exist for a given fiscal year.
		/// Creates if missing, updates dates if exists.
		/// </summary>
		/// <param name="yearId">The fiscal year period ID.</param>
		/// <param name="fiscalYearStart">The fiscal year start date.</param>
		/// <param name="fiscalYearEnd">The fiscal year end date.</param>
		private void EnsureHalfYearPeriods(Guid yearId, DateTime fiscalYearStart, DateTime fiscalYearEnd) {
			DateTime halfYearStart = fiscalYearStart;
	
			for (int h = 1; h <= HalfYearsPerYear; h++) {
				DateTime halfYearEnd = halfYearStart.AddMonths(MonthsPerHalfYear).AddDays(-1);
				if (halfYearEnd > fiscalYearEnd) {
					halfYearEnd = fiscalYearEnd;
				}
		
			string name = FormatTemplate(_fiscalHalfYearTemplate, h, fiscalYearStart, fiscalYearEnd);
			CreateOrUpdatePeriod(FiscalPeriodTypeConsts.FiscalHalfYearTypeId, name, 
				halfYearStart, halfYearEnd, yearId);
		
			halfYearStart = halfYearEnd.AddDays(1);
			}
		}

		/// <summary>
		/// Ensures all 12 fiscal month periods exist for a given fiscal year.
		/// Creates if missing, updates dates if exists. Links months to appropriate quarters.
		/// </summary>
		/// <param name="yearId">The fiscal year period ID.</param>
		/// <param name="quarterIds">Array of quarter period IDs.</param>
		/// <param name="fiscalYearStart">The fiscal year start date.</param>
		/// <param name="fiscalYearEnd">The fiscal year end date.</param>
		private void EnsureMonthPeriods(Guid yearId, Guid[] quarterIds, DateTime fiscalYearStart, DateTime fiscalYearEnd) {
			DateTime monthStart = fiscalYearStart;
			
			for (int i = 1; i <= MonthsPerYear; i++) {
				DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);
				if (monthEnd > fiscalYearEnd) {
					monthEnd = fiscalYearEnd;
				}

				int offsetMonths = ((monthStart.Year - fiscalYearStart.Year) * MonthsPerYear) + 
					(monthStart.Month - fiscalYearStart.Month);
				int quarterIndex = Math.Min(offsetMonths / MonthsPerQuarter, MonthsPerQuarter);
				Guid quarterId = quarterIds[quarterIndex];

			string name = FormatTemplate(_fiscalMonthTemplate, i, fiscalYearStart, fiscalYearEnd);
			CreateOrUpdatePeriod(FiscalPeriodTypeConsts.FiscalMonthTypeId, name, monthStart, monthEnd, 
				yearId, quarterId);

			if (monthEnd == fiscalYearEnd) {
					break;
			}
			monthStart = monthEnd.AddDays(1);
			}
		}
		
		/// <summary>
		/// Ensures all fiscal week periods exist for a given fiscal year.
		/// Weeks start on the configured day of week and run for 7 days each.
		/// Creates if missing, updates dates if exists.
		/// </summary>
		/// <param name="yearId">The fiscal year period ID.</param>
		/// <param name="fiscalYearStart">The fiscal year start date.</param>
		/// <param name="fiscalYearEnd">The fiscal year end date.</param>
		private void EnsureWeekPeriods(Guid yearId, DateTime fiscalYearStart, DateTime fiscalYearEnd) {
			int weekStartDay = GetWeekStartDay();
			System.DayOfWeek targetDayOfWeek = (System.DayOfWeek)(weekStartDay - 1);
			
			DateTime weekStart = fiscalYearStart;
			while (weekStart.DayOfWeek != targetDayOfWeek) {
				weekStart = weekStart.AddDays(1);
			}
			
			int weekNumber = 1;
			
			while (weekStart <= fiscalYearEnd) {
				DateTime weekEnd = weekStart.AddDays(DaysPerWeek - 1);
				if (weekEnd > fiscalYearEnd) {
					weekEnd = fiscalYearEnd;
				}
				
			string name = FormatTemplate(_fiscalWeekTemplate, weekNumber, fiscalYearStart, fiscalYearEnd);
			CreateOrUpdatePeriod(FiscalPeriodTypeConsts.FiscalWeekTypeId, name, weekStart, weekEnd, yearId);
				
			weekNumber++;
			weekStart = weekStart.AddDays(DaysPerWeek);
			}
		}

		/// <summary>
		/// Gets the configured fiscal week start day from system settings.
		/// </summary>
		/// <returns>Day number (1=Sunday, 2=Monday, etc.) or default to Monday if not configured.</returns>
		private int GetWeekStartDay() {
			var weekStartDayId = CoreSysSettings.GetValue<Guid>(_userConnection, FiscalWeekStartDayCode, Guid.Empty);
			
			if (weekStartDayId == Guid.Empty) {
				return DefaultWeekStartDay;
			}
			
			var select = new Select(_userConnection)
					.Column("Number")
				.From("DayOfWeek")
				.Where("Id").IsEqual(Column.Parameter(weekStartDayId)) as Select;
			
			int number = select.ExecuteScalar<int>();
			return number;
		}
		
		/// <summary>
		/// Gets a template value from system settings, or returns the fallback if not found.
		/// </summary>
		/// <param name="code">The system setting code.</param>
		/// <param name="fallback">The default template to use if setting is not configured.</param>
		/// <returns>The template string.</returns>
		private string GetTemplate(string code, string fallback) {
			var value = CoreSysSettings.GetValue<string>(_userConnection, code, default);
			return value as string ?? fallback;
		}

		/// <summary>
		/// Formats a template string with the provided arguments, handling invalid format exceptions.
		/// </summary>
		/// <param name="template">The template string with format placeholders.</param>
		/// <param name="args">The arguments to format into the template.</param>
		/// <returns>The formatted string.</returns>
		private string FormatTemplate(string template, params object[] args) {
			try {
				return string.Format(CultureInfo.InvariantCulture, template, args);
			} catch (FormatException) {
				return string.Format(CultureInfo.InvariantCulture, "{0}", template);
			}
		}

		/// <summary>
		/// Maps template codes to their corresponding PeriodType IDs.
		/// </summary>
		private Guid? GetPeriodTypeIdForTemplateCode(string templateCode) {
			switch (templateCode) {
				case FiscalYearTemplateCode:
					return FiscalPeriodTypeConsts.FiscalYearTypeId;
				case FiscalQuarterTemplateCode:
					return FiscalPeriodTypeConsts.FiscalQuarterTypeId;
				case FiscalMonthTemplateCode:
					return FiscalPeriodTypeConsts.FiscalMonthTypeId;
				case FiscalWeekTemplateCode:
					return FiscalPeriodTypeConsts.FiscalWeekTypeId;
				case FiscalHalfYearTemplateCode:
					return FiscalPeriodTypeConsts.FiscalHalfYearTypeId;
				default:
					return null;
			}
		}

		/// <summary>
		/// Gets the current template value for a given template code.
		/// </summary>
		private string GetTemplateForCode(string templateCode) {
			switch (templateCode) {
				case FiscalYearTemplateCode:
					return _fiscalYearTemplate;
				case FiscalQuarterTemplateCode:
					return _fiscalQuarterTemplate;
				case FiscalMonthTemplateCode:
					return _fiscalMonthTemplate;
				case FiscalWeekTemplateCode:
					return _fiscalWeekTemplate;
				case FiscalHalfYearTemplateCode:
					return _fiscalHalfYearTemplate;
				default:
					return string.Empty;
			}
		}

		/// <summary>
		/// Calculates the new name for a period based on its metadata and the new template.
		/// </summary>
		private string CalculatePeriodName(Guid periodTypeId, string templateCode, string template,
			Guid periodId, DateTime startDate, DateTime dueDate, Guid yearId) {
			DateTime fiscalYearStart = startDate;
			DateTime fiscalYearEnd = dueDate;
		
			if (periodTypeId != FiscalPeriodTypeConsts.FiscalYearTypeId && yearId != Guid.Empty) {
				(fiscalYearStart, fiscalYearEnd) = GetFiscalYearBoundaries(yearId);
			}

			switch (periodTypeId) {
				case var _ when periodTypeId == FiscalPeriodTypeConsts.FiscalYearTypeId:
					return FormatTemplate(template, startDate, dueDate);
				
				case var _ when periodTypeId == FiscalPeriodTypeConsts.FiscalQuarterTypeId:
					int quarterNumber = CalculateQuarterNumber(startDate, fiscalYearStart);
					return FormatTemplate(template, quarterNumber, fiscalYearStart, fiscalYearEnd);
				
				case var _ when periodTypeId == FiscalPeriodTypeConsts.FiscalMonthTypeId:
					int monthNumber = CalculateMonthNumber(startDate, fiscalYearStart);
					return FormatTemplate(template, monthNumber, fiscalYearStart, fiscalYearEnd);
				
				case var _ when periodTypeId == FiscalPeriodTypeConsts.FiscalWeekTypeId:
					int weekNumber = CalculateWeekNumber(periodId, yearId);
					return FormatTemplate(template, weekNumber, fiscalYearStart, fiscalYearEnd);
				
				case var _ when periodTypeId == FiscalPeriodTypeConsts.FiscalHalfYearTypeId:
					int halfYearNumber = CalculateHalfYearNumber(startDate, fiscalYearStart);
					return FormatTemplate(template, halfYearNumber, fiscalYearStart, fiscalYearEnd);
				
				default:
					return string.Empty;
			}
		}

		/// <summary>
		/// Gets the fiscal year start and end dates from a year period.
		/// </summary>
		private (DateTime startDate, DateTime endDate) GetFiscalYearBoundaries(Guid yearId) {
			var select = new Select(_userConnection)
				.Column("StartDate")
				.Column("DueDate")
				.From("Period")
				.Where("Id").IsEqual(Column.Parameter(yearId)) as Select;

			using (var dbExecutor = _userConnection.EnsureDBConnection()) {
				using (var reader = select.ExecuteReader(dbExecutor)) {
					if (reader.Read()) {
						DateTime startDate = (DateTime)reader["StartDate"];
						DateTime dueDate = (DateTime)reader["DueDate"];
						return (startDate, dueDate);
					}
				}
			}

			return (DateTime.MinValue, DateTime.MinValue);
		}

		/// <summary>
		/// Calculates the quarter number (1-4) based on the start date and fiscal year start.
		/// </summary>
		/// <param name="quarterStart">The quarter start date.</param>
		/// <param name="fiscalYearStart">The fiscal year start date.</param>
		/// <returns>Quarter number from 1 to 4.</returns>
		private int CalculateQuarterNumber(DateTime quarterStart, DateTime fiscalYearStart) {
			int monthsFromFiscalStart = ((quarterStart.Year - fiscalYearStart.Year) * MonthsPerYear) + 
				(quarterStart.Month - fiscalYearStart.Month);
			return (monthsFromFiscalStart / MonthsPerQuarter) + 1;
		}

		/// <summary>
		/// Calculates the month number (1-12) based on the start date and fiscal year start.
		/// </summary>
		/// <param name="monthStart">The month start date.</param>
		/// <param name="fiscalYearStart">The fiscal year start date.</param>
		/// <returns>Month number from 1 to 12.</returns>
		private int CalculateMonthNumber(DateTime monthStart, DateTime fiscalYearStart) {
			int monthsFromFiscalStart = ((monthStart.Year - fiscalYearStart.Year) * MonthsPerYear) + 
				(monthStart.Month - fiscalYearStart.Month);
			return monthsFromFiscalStart + 1;
		}

		/// <summary>
		/// Calculates the half year number (1-2) based on the start date and fiscal year start.
		/// </summary>
		/// <param name="halfYearStart">The half year start date.</param>
		/// <param name="fiscalYearStart">The fiscal year start date.</param>
		/// <returns>Half year number, either 1 or 2.</returns>
		private int CalculateHalfYearNumber(DateTime halfYearStart, DateTime fiscalYearStart) {
			int monthsFromFiscalStart = ((halfYearStart.Year - fiscalYearStart.Year) * MonthsPerYear) + 
				(halfYearStart.Month - fiscalYearStart.Month);
			return (monthsFromFiscalStart / MonthsPerHalfYear) + 1;
		}

		/// <summary>
		/// Calculates the week number by querying all weeks in the year and finding the position.
		/// </summary>
		private int CalculateWeekNumber(Guid weekId, Guid yearId) {
			var select = new Select(_userConnection)
					.Column("Id")
				.From("Period")
				.Where("PeriodTypeId").IsEqual(Column.Parameter(FiscalPeriodTypeConsts.FiscalWeekTypeId))
					.And("YearId").IsEqual(Column.Parameter(yearId))
				.OrderByAsc("StartDate") as Select;

			int weekNumber = 1;
			using (var dbExecutor = _userConnection.EnsureDBConnection()) {
				using (var reader = select.ExecuteReader(dbExecutor)) {
					while (reader.Read()) {
						Guid currentWeekId = (Guid)reader["Id"];
						if (currentWeekId == weekId) {
							return weekNumber;
						}
						weekNumber++;
					}
				}
			}

			return 1;
		}

		#endregion
	}
}

