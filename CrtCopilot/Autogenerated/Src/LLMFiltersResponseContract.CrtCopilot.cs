namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using Terrasoft.Core.Entities;
	
	/// <summary>
	///  Common contract for LLM filter response. It con be both a filter or a filter group.
	/// </summary>
	[DataContract]
	public class LLMUnknownFilterResponseContract
	{
		#region usual filter properties
		
		[DataMember]
		public string columnPath { get; set; }

		[DataMember]
		public string comparisonType { get; set; }

		/// <summary>
		///  can contain Guid, DateTime, string, double etc
		/// </summary>
		[DataMember]
		public object value { get; set; }

        #endregion

        #region filter-group related properties

        #region backward-reference filter properties

        /// <summary>
        ///  Possible values are: SUM | MIN | MAX | AVG
        /// </summary>
        [DataMember]
        public string aggregationType { get; set; }

        /// <summary>
        /// value to compare with the result of aggregation. Its string, but actual meaning can be int or ISO date
        /// </summary>
        [DataMember]
        public string aggregationValue { get; set; }


        [DataMember]
        public LLMUnknownFilterResponseContract subFilters { get; set; }



        #endregion

        [DataMember]
		public List<LLMUnknownFilterResponseContract> filters { get; set; }

        [DataMember]
        public List<LLMUnknownFilterResponseContract> backwardReferenceFilters { get; set; }

        [DataMember]
		public string logicalOperation { get; set; } // AND, OR
		
		#endregion
	}
}

