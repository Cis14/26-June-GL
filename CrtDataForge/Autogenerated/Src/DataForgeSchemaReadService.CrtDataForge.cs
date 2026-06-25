namespace Terrasoft.Configuration.DataForge
{
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Web;
	using global::Common.Logging;
	using Core.Factories;
	using Core.ServiceModelContract;
	using Web.Common;
	using Web.Common.ServiceRouting;

	#region Class: DataForgeSchemaReadService

	/// <summary>
	/// Provides read-only REST endpoints for Data Forge operations.
	/// Base route: /0/rest/DataForgeSchemaReadService/
	/// </summary>
	[ServiceContract]
	[DefaultServiceRoute]
	[SspServiceRoute]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class DataForgeSchemaReadService : BaseService
	{
		#region Constants: Private

		private const string ReadOperationCode = "CanReadDataStructureColumnDetails";

		#endregion

		#region Fields: Private

		private static readonly ILog Log = LogManager.GetLogger("DataForge");

		#endregion

		#region Properties: Private

		private IDataForgeReadFacade _readFacade;
		private IDataForgeReadFacade ReadFacade {
			get {
				if (_readFacade == null) {
					_readFacade = ClassFactory.Get<IDataForgeReadFacade>(
						new ConstructorArgument("userConnection", UserConnection));
				}
				return _readFacade;
			}
		}

		#endregion

		#region Methods: Private

		private T LogAndExecute<T>(string methodName, Func<T> action) where T : BaseResponse {
			string correlationId = Guid.NewGuid().ToString();
			Log.ThreadVariablesContext.Set("CorrelationId", correlationId);
			Log.Info($"{nameof(DataForgeSchemaReadService)}.{methodName} started");
			try {
				return action();
			} catch (Exception ex) {
				Log.Error($"{nameof(DataForgeSchemaReadService)}.{methodName} failed", ex);
				throw;
			}
		}

		private bool CheckReadAccess<T>(out T errorResponse) where T : BaseResponse, new() {
			if (!UserConnection.DBSecurityEngine.GetCanExecuteOperation(ReadOperationCode)) {
				errorResponse = new T {
					Success = false,
					ErrorInfo = new ErrorInfo {
						ErrorCode = "AccessDenied",
						Message = $"Operation '{ReadOperationCode}' is required."
					}
				};
				return false;
			}
			errorResponse = null;
			return true;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Retrieves table names similar to the provided query string.
		/// Endpoint: POST /0/rest/DataForgeSchemaReadService/GetSimilarTableNames
		/// </summary>
		/// <param name="request">The request containing query parameters.</param>
		/// <returns>A response containing similar table names with details.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public DataForgeGetTablesDetailsResponse GetSimilarTableNames(GetSimilarTableNamesRequest request) {
			return LogAndExecute(nameof(GetSimilarTableNames), () => {
				if (!CheckReadAccess<DataForgeGetTablesDetailsResponse>(out var errorResponse)) {
					return errorResponse;
				}
				return ReadFacade.GetSimilarTableNames(request);
			});
		}

		/// <summary>
		/// Retrieves detailed information about tables similar to the provided query string.
		/// Endpoint: POST /0/rest/DataForgeSchemaReadService/GetTableDetails
		/// </summary>
		/// <param name="request">The request containing query parameters.</param>
		/// <returns>A response containing similar table details.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public DataForgeGetTablesDetailsResponse GetTableDetails(GetTableDetailsRequest request) {
			return LogAndExecute(nameof(GetTableDetails), () => {
				if (!CheckReadAccess<DataForgeGetTablesDetailsResponse>(out var errorResponse)) {
					return errorResponse;
				}
				return ReadFacade.GetTableDetails(request);
			});
		}

		/// <summary>
		/// Retrieves relationships between two specified tables.
		/// Endpoint: POST /0/rest/DataForgeSchemaReadService/GetTableRelationships
		/// </summary>
		/// <param name="request">The request containing source and target table names.</param>
		/// <returns>A response containing relationship paths.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public GetTableRelationshipsResponse GetTableRelationships(GetTableRelationshipsRequest request) {
			return LogAndExecute(nameof(GetTableRelationships), () => {
				if (!CheckReadAccess<GetTableRelationshipsResponse>(out var errorResponse)) {
					return errorResponse;
				}
				return ReadFacade.GetTableRelationships(request);
			});
		}

		/// <summary>
		/// Retrieves lookup values similar to the provided query string.
		/// Endpoint: POST /0/rest/DataForgeSchemaReadService/GetLookupValues
		/// </summary>
		/// <param name="request">The request containing query parameters.</param>
		/// <returns>A response containing similar lookup values.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public DataForgeGetLookupsResponse GetLookupValues(GetLookupValuesRequest request) {
			return LogAndExecute(nameof(GetLookupValues), () => {
				if (!CheckReadAccess<DataForgeGetLookupsResponse>(out var errorResponse)) {
					return errorResponse;
				}
				return ReadFacade.GetLookupValues(request);
			});
		}

		/// <summary>
		/// Retrieves column details for the specified table.
		/// Endpoint: POST /0/rest/DataForgeSchemaReadService/GetTableColumnsDetails
		/// </summary>
		/// <param name="request">The request containing the table name.</param>
		/// <returns>A response containing table column details.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public DataForgeGetTableColumnsDetailsResponse GetTableColumnsDetails(GetTableColumnsDetailsRequest request) {
			return LogAndExecute(nameof(GetTableColumnsDetails), () => {
				if (!CheckReadAccess<DataForgeGetTableColumnsDetailsResponse>(out var errorResponse)) {
					return errorResponse;
				}
				return ReadFacade.GetTableColumnsDetails(request);
			});
		}

		#endregion
	}

	#endregion
}

