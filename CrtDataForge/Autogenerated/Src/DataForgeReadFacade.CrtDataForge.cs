namespace Terrasoft.Configuration.DataForge
{
	using System.Threading;
	using Common;
	using Core;
	using Core.Factories;
	using Core.ServiceModelContract;

	#region Class: DataForgeReadFacade

	/// <summary>
	/// Orchestrates read-only Data Forge operations, routing to either the microservice or local providers.
	/// </summary>
	[DefaultBinding(typeof(IDataForgeReadFacade))]
	public class DataForgeReadFacade : IDataForgeReadFacade
	{
		#region Fields: Private

		private readonly IDataForgeService _dataForgeService;
		private readonly ITableColumnsDetailsProvider _tableColumnsDetailsProvider;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="DataForgeReadFacade"/> class.
		/// </summary>
		/// <param name="userConnection">User connection instance.</param>
		public DataForgeReadFacade(UserConnection userConnection) {
			_dataForgeService = ClassFactory.Get<IDataForgeServiceFactory>(
				new ConstructorArgument("userConnection", userConnection)).Create();
			_tableColumnsDetailsProvider = ClassFactory.Get<ITableColumnsDetailsProvider>(
				new ConstructorArgument("userConnection", userConnection));
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Validates the request and returns an error response if validation fails.
		/// </summary>
		/// <typeparam name="T">The response type.</typeparam>
		/// <param name="value">The value to validate.</param>
		/// <param name="paramName">The parameter name for error messages.</param>
		/// <param name="response">The error response if validation fails.</param>
		/// <returns>True if validation succeeds; otherwise, false.</returns>
		private bool ValidateNotNullOrWhiteSpace<T>(string value, string paramName, out T response) where T : BaseResponse, new() {
			if (string.IsNullOrWhiteSpace(value)) {
				response = new T {
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = "ValidationError",
						Message = $"Parameter '{paramName}' is required and cannot be empty."
					}
				};
				return false;
			}
			response = null;
			return true;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Gets table names similar to the provided query string.
		/// </summary>
		/// <param name="request">The request containing query parameters.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetTablesDetailsResponse"/> containing similar table names.</returns>
		public DataForgeGetTablesDetailsResponse GetSimilarTableNames(GetSimilarTableNamesRequest request, CancellationToken cancellationToken = default) {
			request.CheckArgumentNull(nameof(request));
			if (!ValidateNotNullOrWhiteSpace<DataForgeGetTablesDetailsResponse>(request.Query, nameof(request.Query), out var errorResponse)) {
				return errorResponse;
			}
			return _dataForgeService.GetSimilarTableDetails(request.Query, cancellationToken);
		}

		/// <summary>
		/// Gets detailed information about tables similar to the provided query string.
		/// </summary>
		/// <param name="request">The request containing query parameters.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetTablesDetailsResponse"/> containing similar table details.</returns>
		public DataForgeGetTablesDetailsResponse GetTableDetails(GetTableDetailsRequest request, CancellationToken cancellationToken = default) {
			request.CheckArgumentNull(nameof(request));
			if (!ValidateNotNullOrWhiteSpace<DataForgeGetTablesDetailsResponse>(request.Query, nameof(request.Query), out var errorResponse)) {
				return errorResponse;
			}
			return _dataForgeService.GetSimilarTableDetails(request.Query, cancellationToken);
		}

		/// <summary>
		/// Gets relationships between two specified tables.
		/// </summary>
		/// <param name="request">The request containing source and target table names.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="GetTableRelationshipsResponse"/> with relationship information.</returns>
		public GetTableRelationshipsResponse GetTableRelationships(GetTableRelationshipsRequest request, CancellationToken cancellationToken = default) {
			request.CheckArgumentNull(nameof(request));
			if (!ValidateNotNullOrWhiteSpace<GetTableRelationshipsResponse>(request.SourceTable, nameof(request.SourceTable), out var errorResponse)) {
				return errorResponse;
			}
			if (!ValidateNotNullOrWhiteSpace(request.TargetTable, nameof(request.TargetTable), out errorResponse)) {
				return errorResponse;
			}
			return _dataForgeService.GetTableRelationships(request.SourceTable, request.TargetTable, cancellationToken);
		}

		/// <summary>
		/// Gets lookup values similar to the provided query string.
		/// </summary>
		/// <param name="request">The request containing query parameters.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetLookupsResponse"/> containing similar lookup values.</returns>
		public DataForgeGetLookupsResponse GetLookupValues(GetLookupValuesRequest request, CancellationToken cancellationToken = default) {
			request.CheckArgumentNull(nameof(request));
			if (!ValidateNotNullOrWhiteSpace<DataForgeGetLookupsResponse>(request.Query, nameof(request.Query), out var errorResponse)) {
				return errorResponse;
			}
			return _dataForgeService.GetSimilarLookups(request.Query, request.SchemaName, cancellationToken);
		}

		/// <summary>
		/// Gets column details for the specified table from local metadata.
		/// </summary>
		/// <param name="request">The request containing the table name.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetTableColumnsDetailsResponse"/> containing column details.</returns>
		public DataForgeGetTableColumnsDetailsResponse GetTableColumnsDetails(GetTableColumnsDetailsRequest request, CancellationToken cancellationToken = default) {
			request.CheckArgumentNull(nameof(request));
			if (!ValidateNotNullOrWhiteSpace<DataForgeGetTableColumnsDetailsResponse>(request.TableName, nameof(request.TableName), out var errorResponse)) {
				return errorResponse;
			}
			return _tableColumnsDetailsProvider.GetTableColumnsDetails(request.TableName, cancellationToken);
		}

		#endregion
	}

	#endregion
}

