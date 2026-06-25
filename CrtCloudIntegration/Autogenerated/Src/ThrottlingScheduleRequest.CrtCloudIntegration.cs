  namespace Terrasoft.Configuration
{
    using System;
	using System.Runtime.Serialization;
	using Terrasoft.Configuration.CESModels;

	/// <summary>
	/// Throttling schedule data model.
	/// </summary>
	[DataContract]
	public class ThrottlingSchedule
	{

		#region Properties: Public

		[DataMember(Name = "day")]
		public int Day { get; set; }

		[DataMember(Name = "delay_per_email")]
		public int DelayPerEmail { get; set; }

		[DataMember(Name = "emails_per_day")]
		public int EmailsPerDay { get; set; }

		#endregion

	}
   

    /// <summary>
    /// Data contract to update throttling schedule.
    /// </summary>
    [DataContract]
    public class ThrottlingScheduleRequest: BaseServiceRequest
    {

        /// <summary>
        /// Gets or sets email unique identifier.
        /// </summary>
        [DataMember(Name = "email_id")]
        public Guid EmailId { get; set; }

        /// <summary>
        /// Gets or sets throttling strategy id.
        /// </summary>
        [DataMember(Name = "strategy_id")]
        public int StrategyId { get; set; }

        /// <summary>
        /// Gets or sets list of schedule <see cref="ThrottlingSchedule"/>.
        /// </summary>
		[DataMember(Name = "schedule")]
		public ThrottlingSchedule[] Schedule { get; set; }
    }
	
	
}
