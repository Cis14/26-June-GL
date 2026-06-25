namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Client
{
	using IntegrationApi.Calendar;
	using IntegrationApi.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Client.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Class: CalendarClientFactory

	/// <summary>
	/// Calendar client factory.
	/// </summary>
	public class CalendarClientFactory
	{

		#region Consts: Private

		private const string _exchangeClientBindName = "Exchange";

		private const string _googleClientBindName = "Google";

		#endregion

		#region Methods: Public

		/// <summary>
		/// Get <see cref="ICalendarClient"/> instance.
		/// </summary>
		/// <param name="calendar"><see cref="Calendar"/> model.</param>
		/// <param name="sessionId">Synchronization session identifier.</param>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <returns><see cref="ICalendarClient"/> instance.</returns>
		public static ICalendarClient GetCalendarClient(Calendar calendar, string sessionId, UserConnection uc = null) {
			var helper = uc != null
				? ClassFactory.Get<ISynchronizationErrorHelper>(new ConstructorArgument("userConnection", uc))
				: null;
			var calendarClientName = calendar.Type == CalendarType.Exchange || calendar.Type == CalendarType.Graph
				? _exchangeClientBindName
				: _googleClientBindName;
			return ClassFactory.Get<ICalendarClient>(calendarClientName, new ConstructorArgument("sessionId", sessionId),
				new ConstructorArgument("synchronizationErrorHelper", helper));
		}

		#endregion

	}

	#endregion

}
