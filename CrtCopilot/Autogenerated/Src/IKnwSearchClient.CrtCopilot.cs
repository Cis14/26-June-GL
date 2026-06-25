namespace Creatio.Copilot
{
	using System.Threading;

	#region Interface: IKnwSearchClient

	/// <summary>
	/// Provides an interface for searching knowledge sources using a RAG service.
	/// </summary>
	public interface IKnwSearchClient
	{

		#region Methods: Public

		/// <summary>
		/// Searches multiple knowledge sources by their identifiers and associated queries.
		/// </summary>
		/// <param name="request">The search request containing knowledge source queries, session ID
		/// and pagination options.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.
		/// Defaults to <see cref="CancellationToken.None"/> if not specified.</param>
		/// <returns>A <see cref="KnwSearchDTO"/> object with the search results for each source.</returns>
		KnwSearchDTO Search(KnwSearchRequest request, CancellationToken cancellationToken = default);

		#endregion

	}

	#endregion

}

