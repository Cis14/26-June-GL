namespace IntegrationV2.Files.cs.Utils {
	using System;

	#region Class: DateTimeUtils

	/// <summary>
	/// Provides <see cref="DateTime"/> modivication methods.
	/// </summary>
	internal class DateTimeUtils {

		#region Methods: internal

		/// <summary>
		/// Converts the time in a specified time zone to Coordinated Universal Time (UTC).
		/// </summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="timeZone">The time zone of dateTime.</param>
		/// <returns>The Coordinated Universal Time (UTC) that corresponds to the dateTime parameter.</returns>
		internal static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo timeZone) {
			if (dateTime.Kind == DateTimeKind.Utc) {
				return dateTime;
			}
			var rawDate = new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);
			return TimeZoneInfo.ConvertTimeToUtc(rawDate, timeZone);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> instance converted to user timezone.
		/// </summary>
		/// <param name="utcDateTime"><see cref="DateTime"/> instance.</param>
		/// <param name="timeZone"><see cref="TimeZoneInfo"/> instance.</param>
		/// <returns><see cref="DateTime"/> instance converted to user timezone.</returns>
		internal static DateTime GetUserDateTime(DateTime dateTime, TimeZoneInfo timeZone) {
			return TimeZoneInfo.ConvertTimeFromUtc(dateTime.Kind == DateTimeKind.Utc
				? dateTime
				: dateTime.ToUniversalTime(), timeZone);
		}

		#endregion

	}

	#endregion

}
