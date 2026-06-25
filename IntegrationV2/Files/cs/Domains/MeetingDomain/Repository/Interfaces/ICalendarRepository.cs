namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces
{
	using System;
	using System.Collections.Generic;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;

	#region Interface: ICalendarRepository

	/// <summary>
	/// Calendar repository interface.
	/// </summary>
	public interface ICalendarRepository
	{

		#region Methods: Public

		/// <summary>
		/// Get all users <see cref="Calendar"/>`s.
		/// </summary>
		/// <returns>List of all users <see cref="Calendar"/>`s.</returns>
		List<Calendar> GetAllCalendars();

		/// <summary>
		/// Get all users enabled <see cref="Calendar"/>`s.
		/// </summary>
		/// <returns>List of enabled users <see cref="Calendar"/>`s.</returns>
		List<Calendar> GetEnabledCalendars();

		/// <summary>
		/// Gets <see cref="Calendar"/> instance filtered by email.
		/// </summary>
		/// <param name="email">Email address.</param>
		/// <returns><see cref="Calendar"/> instance filtered by email.</returns>
		Calendar GetOwnerCalendar(string email);

		/// <summary>
		/// Gets <see cref="Calendar"/> instance filtered by owner identifier.
		/// </summary>
		/// <param name="ownerId">Owner identifier.</param>
		/// <returns><see cref="Calendar"/> instance filtered by owner identifier.</returns>
		Calendar GetOwnerCalendar(Guid ownerId);

		/// <summary>
		/// Gets all <see cref="Calendar"/> instances filtered by owner identifier.
		/// </summary>
		/// <param name="ownerId">Owner identifier.</param>
		/// <returns>see cref="Calendar"/> instances filtered by owner identifier.</returns>
		List<Calendar> GetOwnerCalendars(Guid ownerId);

		/// <summary>
		/// Saves calendar to database.
		/// </summary>
		/// <param name="calendar"><see cref="Calendar"/> instance.</param>
		void Save(Calendar calendar);

		#endregion

	}

	#endregion

}
