namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;

	#region Class: KnwSearchRequest

	/// <summary>
	/// Represents a request for searching knowledge sources with pagination and session tracking.
	/// </summary>
	/// <remarks>
	/// <para>Use this class to encapsulate the parameters required for a knowledge search operation, including the sources to query, session context, and pagination options.</para>
	/// </remarks>
	public class KnwSearchRequest
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the mapping of knowledge source identifiers to their associated search queries.
		/// </summary>
		/// <remarks>
		/// Each key is a unique identifier for a knowledge source, and the value is a collection
		/// of search query objects for that source.
		/// </remarks>
		public IDictionary<Guid, IEnumerable<KnwSearchRequestQuery>> KnwSourceQueries { get; set; }
			= new Dictionary<Guid, IEnumerable<KnwSearchRequestQuery>>();

		/// <summary>
		/// The unique identifier for the search session.
		/// </summary>
		public Guid SessionId { get; set; } = Guid.Empty;

		/// <summary>
		/// The number of search results to skip (for pagination).
		/// </summary>
		public int Skip { get; set; } = 0;

		/// <summary>
		/// The maximum number of search results to return.
		/// </summary>
		public int Take { get; set; } = 10;

		#endregion

	}

	#endregion

}

