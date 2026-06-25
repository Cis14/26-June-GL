namespace Terrasoft.Configuration.CESResponses
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region Class: GetThrottlingSchedulesResponse

	/// <summary>
	/// Represents the result of the throttling schedule.
	/// </summary>
	[DataContract]
	public class GetThrottlingSchedulesResponse
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets collection of throttling schedules.
		/// </summary>
		[DataMember(Name = "throttlingSchedulesList")]
		public List<ThrottlingSchedule> ThrottlingSchedulesList { get; set; }

		#endregion

	}

	#endregion

}

