namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Utils
{
	using Microsoft.Exchange.WebServices.Data;

	#region Class: AppointmentExtension

	/// <summary>
	/// Appointment extensions.
	/// </summary>
	internal static class AppointmentExtension
	{

		#region Methods: Internal

		/// <summary>
		/// Idintify whether appointment is recurrent.
		/// </summary>
		/// <param name="appointment"><see cref="Appointment"/> instance.</param>
		/// <returns>Is recurrent flag of <paramref name="appointment"/>.</returns>
		internal static bool IsRecurrent(this Appointment appointment) {
			return appointment.Recurrence != null || appointment.IsRecurring;
		}

		/// <summary>
		/// Loads property value from <paramref name="appointment"/>. Value not loaded errors will be skipped.
		/// </summary>
		/// <typeparam name="T">Return value type.</typeparam>
		/// <param name="item">Exchange item instance.</param>
		/// <param name="propertyDefinition">Requested property definition.</param>
		/// <returns>Requested property value.</returns>
		internal static T SafeGetValue<T>(this Appointment appointment, PropertyDefinition propertyDefinition) {
			appointment.TryGetProperty(propertyDefinition, out T value);
			return value;
		}

		#endregion

	}

	#endregion

}