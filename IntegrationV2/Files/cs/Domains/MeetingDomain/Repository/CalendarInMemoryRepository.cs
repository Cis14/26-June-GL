namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Repository
{
	using System;
	using System.Collections.Generic;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: CalendarInMemoryRepository

	[DefaultBinding(typeof(ICalendarRepository), Name = nameof(CalendarInMemoryRepository))]
	public class CalendarInMemoryRepository : CalendarRepository, ICalendarRepository
	{

		#region Fields: Private

		/// <summary>
		/// Collection of <see cref="Calendar"/>`s.
		/// </summary>
		private readonly Lazy<List<Calendar>> _calendars;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="sessionId">Session identifier.</param>
		public CalendarInMemoryRepository(UserConnection uc, string sessionId) : base(uc, sessionId) {
			_calendars = new Lazy<List<Calendar>>(() => base.GetCalendars(false, false));
		}

		#endregion

		#region Methods: Protected

		/// <inheritdoc cref="CalendarRepository.GetCalendars(bool, bool)"/>
		protected override List<Calendar> GetCalendars(bool importRequired = true, bool exportRequired = true) {
			var calendars = _calendars.Value;
			if (importRequired || exportRequired) {
				calendars = FilterEnabledCalendars(calendars);
			}
			return calendars;
		}

		#endregion

	}

	#endregion

}
