using System;

namespace Terrasoft.Configuration.CrtFiscalPeriods
{
	/// <summary>
	/// Contains identifiers of PeriodType records used for fiscal periods.
	/// </summary>
	public static class FiscalPeriodTypeConsts
	{
		#region Fields: Public

		/// <summary>Fiscal year PeriodType Id.</summary>
		public static readonly Guid FiscalYearTypeId = new Guid("2e8ff8c6-6b1a-4f3c-9e6c-7b9d8a3f4b11");

		/// <summary>Fiscal quarter PeriodType Id.</summary>
		public static readonly Guid FiscalQuarterTypeId = new Guid("c0a41f71-5a8c-4d61-8d44-4c36e8ebb8e2");

		/// <summary>Fiscal month PeriodType Id.</summary>
		public static readonly Guid FiscalMonthTypeId = new Guid("9b0802a4-4845-4e2e-b2d8-0b9f1cf9f633");

		/// <summary>Fiscal week PeriodType Id.</summary>
		public static readonly Guid FiscalWeekTypeId = new Guid("48e3b341-8917-4db5-9951-ff2869017d3c");
		
		/// <summary>Fiscal half year PeriodType Id.</summary>
		public static readonly Guid FiscalHalfYearTypeId = new Guid("fc360ec1-2177-4e26-a5f9-7742880b9bdb");

		#endregion
	}
}


