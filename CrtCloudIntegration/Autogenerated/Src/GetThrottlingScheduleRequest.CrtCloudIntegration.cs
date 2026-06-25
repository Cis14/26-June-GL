namespace Terrasoft.Configuration.CESResponses
{
	using System.Runtime.Serialization;
	using Terrasoft.Configuration.CESModels;

	#region Class: GetThrottlingScheduleRequest

	/// <summary>
	/// Data contract to get throttling schedules.
	/// </summary>
	[DataContract]
	public class GetThrottlingScheduleRequest : BaseServiceRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets ID of throttling strategy.
		/// </summary>
		[DataMember(Name = "strategy_id")]
		public int StrategyId { get; set; }

		#endregion

	}

	#endregion

}

