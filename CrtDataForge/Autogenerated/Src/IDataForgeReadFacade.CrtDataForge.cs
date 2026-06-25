namespace Terrasoft.Configuration.DataForge
{
	using System.Threading;

	#region Interface: IDataForgeReadFacade

	/// <summary>
	/// Provides a unified facade for read-only Data Forge operations.
	/// </summary>
	public interface IDataForgeReadFacade
	{
		/// <summary>
		/// Gets table names similar to the provided query string.
		/// </summary>
		/// <param name="request">The request containing query parameters.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetTablesDetailsResponse"/> containing similar table names.</returns>
		DataForgeGetTablesDetailsResponse GetSimilarTableNames(GetSimilarTableNamesRequest request, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets detailed information about tables similar to the provided query string.
		/// </summary>
		/// <param name="request">The request containing query parameters.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetTablesDetailsResponse"/> containing similar table details.</returns>
		DataForgeGetTablesDetailsResponse GetTableDetails(GetTableDetailsRequest request, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets relationships between two specified tables.
		/// </summary>
		/// <param name="request">The request containing source and target table names.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="GetTableRelationshipsResponse"/> with relationship information.</returns>
		GetTableRelationshipsResponse GetTableRelationships(GetTableRelationshipsRequest request, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets lookup values similar to the provided query string.
		/// </summary>
		/// <param name="request">The request containing query parameters.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetLookupsResponse"/> containing similar lookup values.</returns>
		DataForgeGetLookupsResponse GetLookupValues(GetLookupValuesRequest request, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets column details for the specified table from local metadata.
		/// </summary>
		/// <param name="request">The request containing the table name.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetTableColumnsDetailsResponse"/> containing column details.</returns>
		DataForgeGetTableColumnsDetailsResponse GetTableColumnsDetails(GetTableColumnsDetailsRequest request, CancellationToken cancellationToken = default);
	}

	#endregion
}

