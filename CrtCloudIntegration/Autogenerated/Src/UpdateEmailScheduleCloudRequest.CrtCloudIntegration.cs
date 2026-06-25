 namespace Terrasoft.Configuration.CESModels
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Represents data contract for updating email's schedule.
	/// </summary>
	[DataContract]
	public class UpdateEmailScheduleCloudRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the API key.
		/// </summary>
		[DataMember(Name = "key")]
		public string ApiKey { get; set; }

		/// <summary>
		/// Gets or sets the business time end.
		/// </summary>
		/// <value>
		/// The business time end.
		/// </value>
		[DataMember(Name = "business_time_end")]
		public long BusinessTimeEnd { get; set; }

		/// <summary>
		/// Gets or sets the business time start.
		/// </summary>
		/// <value>
		/// The business time start.
		/// </value>
		[DataMember(Name = "business_time_start")]
		public long BusinessTimeStart { get; set; }

		/// <summary>
		/// Gets or sets the delivery schedule days.
		/// </summary>
		/// <value>
		/// The delivery schedule days.
		/// </value>
		[DataMember(Name = "delivery_schedule_days")]
		public string DeliveryScheduleDays { get; set; }

		/// <summary>
		/// Gets or sets unique identifier of the email.
		/// </summary>
		/// <value>
		/// The email identifier.
		/// </value>
		[DataMember(Name = "email_id")]
		public Guid EmailId { get; set; }

		/// <summary>
		/// Gets or sets the due date when email must be canceled.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		[DataMember(Name = "expiration_date")]
		public DateTime ExpirationDate { get; set; }

		/// <summary>
		/// Gets or sets the priority of Email.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		[DataMember(Name = "priority")]
		public int Priority { get; set; }

		/// <summary>
		/// Gets or sets the id of time zone for sending.
		/// </summary>
		/// <value>
		/// The target time zone identifier.
		/// </value>
		[DataMember(Name = "target_time_zone_id")]
		public string TargetTimeZoneId { get; set; }

		#endregion

	}
}
