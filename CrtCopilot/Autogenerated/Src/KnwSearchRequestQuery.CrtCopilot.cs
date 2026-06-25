namespace Creatio.Copilot
{
	using System.Collections.Generic;

	#region Class: KnwSearchRequestQuery

	/// <summary>
	/// Represents a single search query with optional keywords for filtering knowledge source results.
	/// </summary>
	/// <remarks>
	/// <para>Use this class to specify both the semantic search query text and optional keywords 
	/// that should be present in the results for more precise filtering.</para>
	/// </remarks>
	public class KnwSearchRequestQuery
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the search query text for semantic search.
		/// </summary>
		/// <remarks>
		/// This text is used for semantic similarity matching against indexed knowledge base content.
		/// </remarks>
		public string Query { get; set; }

		/// <summary>
		/// Gets or sets the optional list of keywords for filtering search results.
		/// </summary>
		/// <remarks>
		/// When provided, these keywords can be used to refine the search results by ensuring 
		/// specific terms, technical names, or identifiers are present in the retrieved content.
		/// Keywords are applied per-query, allowing different filtering for different searches.
		/// </remarks>
		public IEnumerable<string> Keywords { get; set; }

		#endregion

	}

	#endregion

}

