namespace Terrasoft.Configuration.DataForge
{
	using global::Common.Logging;
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Requests;
	using Terrasoft.Core.ServiceModelContract;
	using Terrasoft.OAuthIntegration;
	using Terrasoft.Services;


	#region Interface: IDataForgeService
	/// <summary>
	/// Provides methods for interacting with the Data Forge service for synchronizing data structures and lookups.
	/// </summary>
	public interface IDataForgeService
	{
		/// <summary>
		/// Checks the state of lookups in the Data Forge service.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeCheckLookupsResponse"/> representing the result of the lookup state check.</returns>
		DataForgeCheckLookupsResponse CheckLookupsState(CancellationToken cancellationToken = default);

		/// <summary>
		/// Checks the state of the specified tables in the Data Forge service.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <param name="schemaItems">Tables to check.</param>
		/// <returns>A <see cref="DataForgeCheckTablesResponse"/> representing the table state check result.</returns>
		DataForgeCheckTablesResponse CheckTablesState(CancellationToken cancellationToken = default, params ISchemaManagerItem<EntitySchema>[] schemaItems);

		/// <summary>
		/// Deletes a lookup by its identifier.
		/// </summary>
		/// <param name="lookupId">The entity id of lookup</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		void DeleteLookup(Guid lookupId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets lookups similar to the provided query string.
		/// </summary>
		/// <param name="queryString">Search query.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetLookupsResponse"/> containing similar lookups.</returns>
		DataForgeGetLookupsResponse GetSimilarLookups(string queryString, string schemaName, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets table names similar to the provided query string.
		/// </summary>
		/// <param name="queryString">Search query.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetTablesResponse"/> containing similar table names.</returns>
		DataForgeGetTablesResponse GetSimilarTableNames(string queryString, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets detailed information about tables similar to the provided query string.
		/// </summary>
		/// <param name="queryString">Search query.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="DataForgeGetTablesDetailsResponse"/> containing similar table details.</returns>
		DataForgeGetTablesDetailsResponse GetSimilarTableDetails(string queryString, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets relationships between two specified tables.
		/// </summary>
		/// <param name="sourceTable">Source table name.</param>
		/// <param name="targetTable">Target table name.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <returns>A <see cref="GetTableRelationshipsResponse"/> with relationship information.</returns>
		GetTableRelationshipsResponse GetTableRelationships(string sourceTable, string targetTable, CancellationToken cancellationToken = default);

		/// <summary>
		/// Initializes all data structures in the Data Forge service.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		void InitializeDataStructure(CancellationToken cancellationToken = default);

		/// <summary>
		/// Initializes all lookups in the Data Forge service.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		void InitializeLookups(CancellationToken cancellationToken = default);

		/// <summary>
		/// Removes a table structure by name.
		/// </summary>
		/// <param name="tableName">Name of the table to remove.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		void RemoveTableByName(string tableName, CancellationToken cancellationToken = default);

		/// <summary>
		/// Updates lookups for the specified value entity.
		/// </summary>
		/// <param name="valuesSchemaUId">Schema UId of the lookup value.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		void UpdateLookupsForValue(Guid valuesSchemaUId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Uploads table definitions for the given schema items.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		/// <param name="schemaItems">Schema items to upload.</param>
		void UploadDataStructure(CancellationToken cancellationToken = default, params ISchemaManagerItem<EntitySchema>[] schemaItems);

		/// <summary>
		/// Uploads a single entity (table) definition.
		/// </summary>
		/// <param name="schemaItemData">Schema item to upload.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		//	void UploadEntity(SchemaItemData schemaItemData, CancellationToken cancellationToken = default);

		/// <summary>
		/// Uploads a single table definition.
		/// </summary>
		/// <param name="tableDefintion">Table definition to upload.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		void UploadTableDefinition(TableDefinition tableDefinition, CancellationToken cancellationToken = default);

		/// <summary>
		/// Uploads a single lookup definition.
		/// </summary>
		/// <param name="lookupDefinition">Lookup defintion to be uploaded.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		void UploadLookup(LookupDefinition lookupDefinition, CancellationToken cancellationToken = default);

		/// <summary>
		/// Uploads multiple lookups by their identifiers.
		/// </summary>
		/// <param name="ids">Identifiers of lookups to upload.</param>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		void UploadLookups(IReadOnlyList<Guid> ids, CancellationToken cancellationToken = default);

		/// <summary>
		/// Checks the current database schema state after the application starts and uploads
		/// any missing or outdated entity schemas (tables) to ensure the data structure
		/// is synchronized with the application model.
		/// </summary>
		/// <remarks>
		/// This method is intended to be called during application startup.
		/// It queries the DataForge service for tables that require creation or updates,
		/// and if any are found, uploads the corresponding entity schemas.
		/// </remarks>
		void UploadPendingDataStructures();

		/// <summary>
		/// Checks the current database schema state after the application starts and uploads
		/// any missing or outdated entity schemas (tables) to ensure the data structure
		/// is synchronized with the application model.
		/// </summary>
		/// <remarks>
		/// This method is intended to be called during application startup.
		/// It queries the DataForge service for tables that require creation or updates,
		/// and if any are found, uploads the corresponding entity schemas.
		/// </remarks>
		void UploadPendingLookups();

		/// <summary>
		/// Checks if the Data Forge service is enabled.
		/// </summary>
		/// <returns>True if the Data Forge service is enabled; otherwise, false.</returns>
		bool IsDataForgeEnabled();
	}


	#endregion

	[DefaultBinding(typeof(IDataForgeService), Name = "DefaultDataForgeService")]
	public class DataForgeService : IDataForgeService
	{

		#region Class: ApiRoutes

		private static class ApiRoutes
		{

			#region Constants: Private

			private const string ApiV1Prefix = "/api/v1";

			#endregion

			#region Class: DataStructure

			public static class DataStructure
			{

				#region Constants: Private

				private const string Base = ApiV1Prefix + "/dataStructure";

				#endregion

				#region Constants: Public

				public const string InitializeAll = Base;
				public const string UpdateAll = Base;
				public const string SimilarTables = Base + "/tables/similar";
				public const string SimilarTablesDetails = Base + "/tables/similarDetails";
				public const string RelationshipsJson = Base + "/tables/relations/json";
				public const string RelationshipsCypher = Base + "/tables/relations/cypher";
				public const string State = Base + "/state";
				public const string Table = Base + "/table";

				#endregion

			}

			#endregion

			#region Class: Lookups

			public static class Lookups
			{

				#region Constants: Private

				private const string Base = ApiV1Prefix + "/lookups";

				#endregion

				#region Constants: Public

				public const string InitializeAll = Base;
				public const string UpdateAll = Base;
				public const string Lookup = Base + "/lookup";
				public const string State = Base + "/state";
				public const string SimilarLookups = Base + "/similar";

				#endregion

			}

			#endregion

		}

		#endregion

		#region Class: ArgumentValidator

		private static class ArgumentValidator
		{
			public static void NotNull<T>(T value, string name) where T : class {
				if (value == null) {
					throw new ArgumentNullException(name);
				}
			}

			public static void NotNullOrEmpty<T>(ICollection<T> collection, string name) {
				if (collection == null) {
					throw new ArgumentNullException(name);
				}

				if (collection.Count == 0) {
					throw new ArgumentException($"{name} must not be empty.", name);
				}
			}
		}

		#endregion

		#region Constants: Private

		private const string CorrelationIdHeader = "X-Correlation-ID";

		#endregion

		#region Fields: Private

		private static readonly ILog _log = LogManager.GetLogger("DataForge");
		private readonly IDataStructureHandler _dataStructureHandler;
		private readonly ILookupHandler _lookupHandler;
		private readonly UserConnection _userConnection;
		private readonly IHttpRequestClient _httpClient;
		private readonly IIdentityServiceWrapper _identityServiceWrapper;
		private readonly IIdentityServiceSettingsProvider _identityServiceSettingsProvider;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="DataForgeService"/> class with the specified dependencies.
		/// </summary>
		/// <param name="dataStructureHandler">Service for retrieving and handling table data structures.</param>
		/// <param name="lookupHandler">Service for managing lookup definitions and values.</param>
		/// <param name="userConnection">The user connection context for accessing configuration and schema data.</param>
		/// <param name="httpRequestClient">The HTTP client used for sending requests to the Data Forge service.</param>
		/// <param name="identityServiceWrapper">The identity service wrapper for authenticating and authorizing outgoing HTTP requests.</param>
		public DataForgeService(
			IDataStructureHandler dataStructureHandler,
			ILookupHandler lookupHandler,
			UserConnection userConnection,
			IHttpRequestClient httpRequestClient,
			IIdentityServiceWrapper identityServiceWrapper) {
			_dataStructureHandler = dataStructureHandler;
			_lookupHandler = lookupHandler;
			_userConnection = userConnection;
			_httpClient = httpRequestClient;
			_identityServiceWrapper = identityServiceWrapper;
		}

		#endregion

		#region Properties: Private

		private Uri ServiceUrl {
			get {
				var value = SysSettings.GetValue(_userConnection, "DataForgeServiceUrl", string.Empty);
				if (string.IsNullOrEmpty(value)) {
					return null;
				}
				return new Uri(value);
			}
		}

		private int RequestTimeout {
			get {
				int value = SysSettings.GetValue(_userConnection, "DataForgeServiceQueryTimeout",
					30000);
				if (value < 0) {
					value = 0;
				}
				return value / 1000;
			}
		}

		private int SimilarTablesResultLimit {
			get {
				return SysSettings.GetValue(_userConnection,
					"DataForgeSimilarTablesResultLimit", 50);
			}
		}

		private int LookupResultLimit {
			get {
				return SysSettings.GetValue(_userConnection,
					"DataForgeLookupResultLimit", 5);
			}
		}

		private int TableRelationshipsCountLimit {
			get {
				return SysSettings.GetValue(_userConnection,
					"DataForgeTableRelationshipsCountLimit", 5);
			}
		}

		private bool TableRelationshipsDetailsIncluded {
			get {
				return SysSettings.GetValue(_userConnection,
					"DataForgeTableRelationshipsDetailsIncluded", false);
			}
		}

		private bool EnableSensitiveDataLogging {
			get {
				return SysSettings.GetValue(_userConnection,
					"DataForgeEnableSensitiveDataLogging", false);
			}
		}

		#endregion

		#region Methods: Private

		private void LogOperationInfo(string operation, object sensitiveData = null) {
			if (EnableSensitiveDataLogging && sensitiveData != null) {
				_log.Info($"{nameof(DataForgeService)} {operation} {JsonConvert.SerializeObject(sensitiveData)}");
			} else {
				_log.Info($"{nameof(DataForgeService)} {operation}");
			}
		}

		private void LogOperationError(string operation, Exception ex) {
			if (EnableSensitiveDataLogging) {
				_log.Error($"{nameof(DataForgeService)} {operation} error: {ex.Message}", ex);
			} else {
				_log.Error($"{nameof(DataForgeService)} {operation} error: {ex.Message}");
			}
		}

		private ErrorInfo CreateErrorInfo(Exception ex) {
			return new ErrorInfo {
				Message = ex.Message,
				StackTrace = ex.StackTrace
			};
		}

		private HttpRequestConfig CreateRequest(
			string relativePath,
			HttpRequestMethod method,
			object body = null,
			Dictionary<string, string> queryParams = null,
			Guid? id = null,
			CancellationToken cancellationToken = default) {
			string path = id.HasValue ? $"{relativePath}/{id}" : relativePath;
			Uri url = new Uri(ServiceUrl, path);
			string correlationId = Guid.NewGuid().ToString();

			HttpRequestConfig request = new HttpRequestConfig {
				Url = url,
				Method = method,
				RequestTimeout = RequestTimeout,
				Body = body,
				CancellationToken = cancellationToken
			}.WithOAuth<DataForgeFeatures.UseOAuth>(_identityServiceWrapper, string.Empty);

			if (queryParams != null) {
				foreach (KeyValuePair<string, string> param in queryParams) {
					request.AddQueryParam(param.Key, param.Value);
				}
			}

			request.Headers.Add(CorrelationIdHeader, correlationId);

			_log.ThreadVariablesContext.Set("CorrelationId", correlationId);

			return request;
		}

		private HttpRequestConfig CreateGetTablesRequest(
			string queryString,
			CancellationToken cancellationToken = default) =>
			CreateRequest(
				ApiRoutes.DataStructure.SimilarTables,
				HttpRequestMethod.GET,
				queryParams: new Dictionary<string, string> {
					{ "query", queryString },
					{ "limit", SimilarTablesResultLimit.ToString() }
				},
				cancellationToken: cancellationToken);

		private HttpRequestConfig CreateGetTablesDetailsRequest(
			string queryString,
			CancellationToken cancellationToken = default) =>
			CreateRequest(
				ApiRoutes.DataStructure.SimilarTablesDetails,
				HttpRequestMethod.GET,
				queryParams: new Dictionary<string, string> {
					{ "query", queryString },
					{ "limit", SimilarTablesResultLimit.ToString() }
				},
				cancellationToken: cancellationToken);

		private HttpRequestConfig CreateGetLookupsRequest(
			string queryString,
			string schemaName,
			CancellationToken cancellationToken = default) =>
			CreateRequest(
			   ApiRoutes.Lookups.SimilarLookups,
				HttpRequestMethod.GET,
				queryParams: new Dictionary<string, string> {
					{ "query", queryString },
					{ "limit", LookupResultLimit.ToString() },
					{ "lookupValueSchemaName", schemaName },
				},
				cancellationToken: cancellationToken);

		private HttpRequestConfig CreateInitializeDataStructureRequest(
			List<TableDefinition> tableDefinitions,
			CancellationToken cancellationToken = default) =>
			CreateRequest(
				ApiRoutes.DataStructure.InitializeAll,
				HttpRequestMethod.POST,
				body: new DataForgeInitializeDataStructureRequestBody(tableDefinitions),
				cancellationToken: cancellationToken);

		private HttpRequestConfig CreateUpdateDataStructureRequest(
			List<TableDefinition> tableDefinitions,
			CancellationToken cancellationToken = default) =>
			CreateRequest(
				ApiRoutes.DataStructure.UpdateAll,
				HttpRequestMethod.PATCH,
				body: new DataForgeUpdateDataStructureRequestBody(tableDefinitions),
				cancellationToken: cancellationToken);

		private HttpRequestConfig CreateUploadTableStructureRequest(
			TableDefinition tableDefinition,
			CancellationToken cancellationToken = default) =>
			CreateRequest(
				ApiRoutes.DataStructure.Table,
				HttpRequestMethod.POST,
				body: new DataForgeUploadTableStructureRequestBody { Table = tableDefinition },
				cancellationToken: cancellationToken);

		private HttpRequestConfig CreateRemoveTableStructureRequest(
			string tableName,
			CancellationToken cancellationToken = default) =>
			CreateRequest(
				$"{ApiRoutes.DataStructure.Table}/{tableName}",
				HttpRequestMethod.DELETE,
				cancellationToken: cancellationToken);

		private HttpRequestConfig CreateGetDataStructureStateRequest(
			List<TableSummary> tableSummaries,
			CheckTablesOptions options,
			CancellationToken cancellationToken = default) =>
			CreateRequest(
				ApiRoutes.DataStructure.State,
				HttpRequestMethod.POST,
				body: new DataForgeCheckTablesRequestBody {
					TableStates = tableSummaries,
					Options = options
				},
				cancellationToken: cancellationToken);

		private HttpRequestConfig CreateGetTableRelationshipsRequest(
			string sourceTable,
			string targetTable,
			int limit,
			bool bidirectional,
			bool skipDetails,
			CancellationToken cancellationToken = default) =>
			CreateRequest(
				ApiRoutes.DataStructure.RelationshipsCypher,
				HttpRequestMethod.GET,
				queryParams: new Dictionary<string, string> {
					{ "sourceTable", sourceTable },
					{ "targetTable", targetTable },
					{ "limit", limit.ToString() },
					{ "bidirectional", bidirectional.ToString() },
					{ "skipDetails", skipDetails.ToString() }
				},
				cancellationToken: cancellationToken);

		private HttpRequestConfig CreateGetLookupsStateRequest(
			List<LookupSummary> lookupSummaries,
			List<LookupValueSummary> lookupValueSummaries,
			CheckLookupsOptions options,
			CancellationToken cancellationToken = default) {
			ArgumentValidator.NotNullOrEmpty(lookupSummaries, nameof(lookupSummaries));
			ArgumentValidator.NotNullOrEmpty(lookupValueSummaries, nameof(lookupValueSummaries));

			return CreateRequest(ApiRoutes.Lookups.State,
				HttpRequestMethod.POST,
				body: new DataForgeCheckLookupsRequestBody {
					Lookups = lookupSummaries,
					LookupValues = lookupValueSummaries,
					Options = options
				},
				cancellationToken: cancellationToken);
		}

		private HttpRequestConfig CreateInitializeLookupsRequest(
			List<LookupDefinition> lookupDefinitions,
			List<LookupValueDefinition> lookupValuesDefinitions,
			CancellationToken cancellationToken = default) {
			ArgumentValidator.NotNullOrEmpty(lookupDefinitions, nameof(lookupDefinitions));
			ArgumentValidator.NotNullOrEmpty(lookupValuesDefinitions, nameof(lookupValuesDefinitions));

			return CreateRequest(ApiRoutes.Lookups.InitializeAll,
				HttpRequestMethod.POST,
				body: new InitializeLookupsRequestBody {
					Lookups = lookupDefinitions,
					LookupValues = lookupValuesDefinitions
				},
				cancellationToken: cancellationToken);
		}

		private HttpRequestConfig CreateUpdateLookupsRequest(
			List<LookupDefinition> lookupDefinitions,
			List<LookupValueDefinition> lookupValuesDefinitions,
			CancellationToken cancellationToken = default) {
			ArgumentValidator.NotNullOrEmpty(lookupDefinitions, nameof(lookupDefinitions));
			ArgumentValidator.NotNullOrEmpty(lookupValuesDefinitions, nameof(lookupValuesDefinitions));

			return CreateRequest(ApiRoutes.Lookups.UpdateAll,
				HttpRequestMethod.PATCH,
				body: new UpdateLookupsRequestBody(lookupDefinitions, lookupValuesDefinitions),
				cancellationToken: cancellationToken);
		}

		private HttpRequestConfig CreateDeleteLookupRequest(Guid id, CancellationToken cancellationToken = default) =>
			CreateRequest(
				ApiRoutes.Lookups.Lookup,
				HttpRequestMethod.DELETE,
				id: id,
				cancellationToken: cancellationToken);

		private T ProcessResponse<T>(IHttpResponse response) {
			if (response.IsTimedOut) {
				var ex = new TimeoutException("Request timed out");
				LogOperationError(nameof(ProcessResponse), ex);
				throw ex;
			}

			if (response.Exception != null || !response.IsSuccessStatusCode) {
				string error = response.Exception?.Message ?? response.ReasonPhrase;
				var ex = new InvalidOperationException($"Request failed: {error}", response.Exception);
				LogOperationError(nameof(ProcessResponse), ex);
				throw ex;
			}

			if (string.IsNullOrWhiteSpace(response.Content)) {
				return default;
			}

			try {
				return JsonConvert.DeserializeObject<T>(response.Content);
			} catch (Exception ex) {
				string errorMessage = EnableSensitiveDataLogging
					? $"Failed to deserialize response content: {response.Content}"
					: "Failed to deserialize response content";

				var wrappedException = new InvalidOperationException(errorMessage, ex);
				LogOperationError(nameof(ProcessResponse), wrappedException);
				throw wrappedException;
			}
		}

		private List<string> GetMissingIdentitySettings() {
			var requiredSettings = new Dictionary<string, string> {
				["IdentityServerClientId"] = SysSettings.GetValue(_userConnection, "IdentityServerClientId", ""),
				["IdentityServerClientSecret"] = SysSettings.GetValue(_userConnection, "IdentityServerClientSecret", ""),
				["IdentityServerUrl"] = SysSettings.GetValue(_userConnection, "IdentityServerUrl", "")
			};

			return requiredSettings
				.Where(kvp => string.IsNullOrWhiteSpace(kvp.Value) ||
					(kvp.Key == "IdentityServerUrl" && !Uri.IsWellFormedUriString(kvp.Value, UriKind.Absolute)))
				.Select(kvp => kvp.Key)
				.ToList();
		}

		private string[] GetProductEditions() {
			try {
				var productEditionsSelect = (Select)new Select(_userConnection)
					.Column("SysSettingsValue", "TextValue").As("ProductEdition")
					.From("SysSettings")
					.LeftOuterJoin("SysSettingsValue")
					.On("SysSettings", "Id").IsEqual("SysSettingsValue", "SysSettingsId")
					.Where("SysSettings", "Name").IsEqual(Column.Parameter("Product version"));
				productEditionsSelect.SpecifyNoLockHints();
				List<string> productEditions = new List<string>();
				productEditionsSelect.ExecuteReader(reader => {
					string name = reader.GetColumnValue<string>("ProductEdition");
					if (!string.IsNullOrWhiteSpace(name)) {
						productEditions.Add(name);
					}
				});
				return productEditions.ToArray();
			} catch (Exception ex) {
				_log.Error("Failed to get product editions", ex);
				return Array.Empty<string>();
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Retrieves similar table names based on the provided query string.
		/// </summary>
		/// <param name="queryString">The search query used to find similar table names.</param>
		/// <returns>
		/// A <see cref="DataForgeGetTablesResponse"/> object containing the list of table names,
		/// success flag, and error information if applicable.
		/// </returns>
		public DataForgeGetTablesResponse GetSimilarTableNames(string queryString, CancellationToken cancellationToken = default) {
			DataForgeGetTablesResponse dfResponse = new DataForgeGetTablesResponse();
			try {
				HttpRequestConfig request = CreateGetTablesRequest(queryString, cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, request.Url.Query);
				IHttpResponse response = _httpClient.Send(request);
				List<string> result = ProcessResponse<List<string>>(response);

				if (result != null) {
					dfResponse.Data = result;
					dfResponse.Success = true;
				}
			} catch (Exception ex) {
				LogOperationError(nameof(GetSimilarTableNames), ex);
				dfResponse.Success = false;
				dfResponse.ErrorInfo = CreateErrorInfo(ex);
			}

			return dfResponse;
		}

		/// <summary>
		/// Retrieves similar table details based on the provided query string.
		/// </summary>
		/// <param name="queryString">The search query used to find similar table names.</param>
		/// <returns>
		/// A <see cref="DataForgeGetTablesResponse"/> object containing the list of table names,
		/// success flag, and error information if applicable.
		/// </returns>
		public DataForgeGetTablesDetailsResponse GetSimilarTableDetails(string queryString, CancellationToken cancellationToken = default) {
			DataForgeGetTablesDetailsResponse dfResponse = new DataForgeGetTablesDetailsResponse();
			try {
				HttpRequestConfig request = CreateGetTablesDetailsRequest(queryString, cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, request.Url.Query);
				IHttpResponse response = _httpClient.Send(request);
				List<SimilarTable> result = ProcessResponse<List<SimilarTable>>(response);

				if (result != null) {
					dfResponse.Data = result;
					dfResponse.Success = true;
				}
			} catch (Exception ex) {
				LogOperationError(nameof(GetSimilarTableDetails), ex);
				dfResponse.Success = false;
				dfResponse.ErrorInfo = CreateErrorInfo(ex);
			}

			return dfResponse;
		}

		/// <summary>
		/// Retrieves similar lookups based on the provided query string.
		/// </summary>
		/// <param name="queryString">The search query used to find similar table names.</param>
		/// <param name="schemaName">The schema name used to find lookups.</param>
		/// <returns>
		/// A <see cref="DataForgeGetLookupsResponse"/> object containing the list of retrieved lookups,
		/// success flag, and error information if applicable.
		/// </returns>
		public DataForgeGetLookupsResponse GetSimilarLookups(
			string queryString,
			string schemaName,
			CancellationToken cancellationToken = default) {
			DataForgeGetLookupsResponse dfResponse = new DataForgeGetLookupsResponse();
			try {
				HttpRequestConfig request = CreateGetLookupsRequest(queryString, schemaName, cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, request.Url.Query);
				IHttpResponse response = _httpClient.Send(request);
				List<LookupDefinitionResponse> result = ProcessResponse<List<LookupDefinitionResponse>>(response);

				if (result != null) {
					dfResponse.Data = result;
					dfResponse.Success = true;
				}
			} catch (Exception ex) {
				LogOperationError(nameof(GetSimilarLookups), ex);
				dfResponse.Success = false;
				dfResponse.ErrorInfo = CreateErrorInfo(ex);
			}

			return dfResponse;
		}

		/// <summary>
		/// Retrieves the relationships between two specified tables from the DataForge service.
		/// </summary>
		/// <param name="sourceTable">The name of the source table to check for relationships.</param>
		/// <param name="targetTable">The name of the target table to check for relationships.</param>
		/// <returns>
		/// A <see cref="GetTableRelationshipsResponse"/> object containing relationship details or error information.
		/// </returns>
		public GetTableRelationshipsResponse GetTableRelationships(
			string sourceTable,
			string targetTable,
			CancellationToken cancellationToken = default) {
			var dfResponse = new GetTableRelationshipsResponse();
			try {
				HttpRequestConfig request = CreateGetTableRelationshipsRequest(
					sourceTable,
					targetTable,
					TableRelationshipsCountLimit,
					true,
					!TableRelationshipsDetailsIncluded,
					cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, request.Url.Query);
				IHttpResponse response = _httpClient.Send(request);
				List<string> result = ProcessResponse<List<string>>(response);

				if (result != null) {
					dfResponse.Paths = result;
					dfResponse.Success = true;
				}
			} catch (Exception ex) {
				LogOperationError(nameof(GetTableRelationships), ex);
				dfResponse.Success = false;
				dfResponse.ErrorInfo = CreateErrorInfo(ex);
			}

			return dfResponse;
		}

		/// <inheritdoc/>
		public void InitializeDataStructure(CancellationToken cancellationToken = default) {
			try {
				List<TableDefinition> tableDataStructures = _dataStructureHandler.GetTableDefinitions(false, true);
				HttpRequestConfig request = CreateInitializeDataStructureRequest(
					tableDataStructures,
					cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, $"{tableDataStructures.Count} tables");
				IHttpResponse response = _httpClient.SendWithJsonBody(request);
				ProcessResponse<object>(response);
			} catch (Exception ex) {
				LogOperationError(nameof(InitializeDataStructure), ex);
			}
		}

		/// <inheritdoc/>
		public void UploadDataStructure(CancellationToken cancellationToken = default, params ISchemaManagerItem<EntitySchema>[] schemaItems) {
			try {
				List<TableDefinition> tableDataStructures = _dataStructureHandler.GetTableDefinitions(false, true, schemaItems);
				HttpRequestConfig request = CreateUpdateDataStructureRequest(
					tableDataStructures,
					cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, $"{tableDataStructures.Count} tables");
				IHttpResponse response = _httpClient.SendWithJsonBody(request);
				ProcessResponse<object>(response);
			} catch (Exception ex) {
				LogOperationError(nameof(UploadDataStructure), ex);
			}
		}

		/// <inheritdoc/>
		public DataForgeCheckTablesResponse CheckTablesState(
			CancellationToken cancellationToken = default,
			params ISchemaManagerItem<EntitySchema>[] schemaItems) {
			DataForgeCheckTablesResponse response = new DataForgeCheckTablesResponse();
			try {
				List<TableSummary> tableSummaries = _dataStructureHandler.GetTableSummaries(schemaItems);
				string[] productEditions = GetProductEditions();
				var options = new CheckTablesOptions {
					IsDemoApp = _userConnection.AppConnection.IsDemoMode,
					IsTrialApp = false,
					Editions = productEditions,
					ExtraPackages = Array.Empty<string>()
				};
				HttpRequestConfig request =
					CreateGetDataStructureStateRequest(tableSummaries, options, cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, $"{tableSummaries.Count} tables");
				IHttpResponse httpResponse = _httpClient.SendWithJsonBody(request);
				DataForgeCheckTablesResponse processedResponse = ProcessResponse<DataForgeCheckTablesResponse>(httpResponse);

				if (processedResponse != null) {
					response.TableNames = processedResponse?.TableNames;
					response.Success = true;
				}
			} catch (Exception ex) {
				LogOperationError(nameof(CheckTablesState), ex);
				response.Success = false;
				response.ErrorInfo = CreateErrorInfo(ex);
			}

			return response;
		}

		/// <inheritdoc/>
		public void UploadTableDefinition(TableDefinition tableDefinition, CancellationToken cancellationToken = default) {
			try {
				HttpRequestConfig request = CreateUploadTableStructureRequest(tableDefinition, cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, tableDefinition.Name);
				IHttpResponse response = _httpClient.SendWithJsonBody(request);
				ProcessResponse<object>(response);
			} catch (Exception ex) {
				LogOperationError(nameof(UploadTableDefinition), ex);
			}
		}

		/// <inheritdoc/>
		public void RemoveTableByName(string tableName, CancellationToken cancellationToken = default) {
			try {
				HttpRequestConfig request = CreateRemoveTableStructureRequest(tableName, cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, tableName);
				IHttpResponse response = _httpClient.Send(request);
				ProcessResponse<object>(response);
			} catch (Exception ex) {
				LogOperationError(nameof(RemoveTableByName), ex);
			}
		}

		/// <inheritdoc/>
		public DataForgeCheckLookupsResponse CheckLookupsState(CancellationToken cancellationToken = default) {
			DataForgeCheckLookupsResponse response = new DataForgeCheckLookupsResponse();
			try {
				List<LookupDefinition> lookupDefinitions = _lookupHandler.GetLookupDefinitions();
				List<LookupValueDefinition> lookupValueDefinitions = _lookupHandler.GetLookupValueDefinitionsForLookups(lookupDefinitions);
				string[] productEditions = GetProductEditions();
				var options = new CheckLookupsOptions {
					IsDemoApp = _userConnection.AppConnection.IsDemoMode,
					IsTrialApp = false,
					Editions = productEditions,
					ExtraPackages = Array.Empty<string>()
				};
				HttpRequestConfig request = CreateGetLookupsStateRequest(
					lookupDefinitions.Select(l => _lookupHandler.MapToLookupSummary(l)).ToList(),
					lookupValueDefinitions.Select(l => _lookupHandler.MapToLookupValueSummary(l)).ToList(),
					options,
					cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, $"{lookupDefinitions.Count} lookups, {lookupValueDefinitions.Count} lookup values.");
				IHttpResponse httpResponse = _httpClient.SendWithJsonBody(request);
				DataForgeCheckLookupsResponse processedResponse = ProcessResponse<DataForgeCheckLookupsResponse>(httpResponse);

				if (processedResponse != null) {
					response.LookupIds = processedResponse?.LookupIds;
					response.Success = true;
				}
			} catch (Exception ex) {
				LogOperationError(nameof(CheckLookupsState), ex);
				response.Success = false;
				response.ErrorInfo = CreateErrorInfo(ex);
			}

			return response;
		}

		/// <inheritdoc/>
		public void InitializeLookups(CancellationToken cancellationToken = default) {
			try {
				List<LookupDefinition> lookupDefinitions = _lookupHandler.GetLookupDefinitions();
				List<LookupValueDefinition> lookupValueDefinitions = _lookupHandler.GetLookupValueDefinitionsForLookups(lookupDefinitions);
				HttpRequestConfig request = CreateInitializeLookupsRequest(
					lookupDefinitions,
					lookupValueDefinitions,
					cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, $"{lookupDefinitions.Count} lookups, {lookupValueDefinitions.Count} lookup values");
				IHttpResponse response = _httpClient.SendWithJsonBody(request);
				ProcessResponse<object>(response);
			} catch (Exception ex) {
				LogOperationError(nameof(InitializeLookups), ex);
			}
		}

		/// <inheritdoc/>
		public void UploadLookups(IReadOnlyList<Guid> ids, CancellationToken cancellationToken = default) {
			try {
				List<LookupDefinition> lookupDefinitions = _lookupHandler.GetLookupDefinitions(ids);
				List<LookupValueDefinition> lookupValueDefinitions = _lookupHandler.GetLookupValueDefinitionsForLookups(lookupDefinitions);

				HttpRequestConfig request = CreateUpdateLookupsRequest(
					lookupDefinitions,
					lookupValueDefinitions,
					cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, $"{lookupDefinitions.Count} lookups, {lookupValueDefinitions.Count} lookup values.");
				IHttpResponse response = _httpClient.SendWithJsonBody(request);
				ProcessResponse<object>(response);
			} catch (Exception ex) {
				LogOperationError(nameof(UploadLookups), ex);
			}
		}

		/// <inheritdoc/>
		public void UploadLookup(LookupDefinition lookupDefinition, CancellationToken cancellationToken = default) {
			try {
				List<LookupDefinition> lookupDefinitions = new List<LookupDefinition> { lookupDefinition };
				List<LookupValueDefinition> lookupValueDefinitions = _lookupHandler.GetLookupValueDefinitionsForLookups(lookupDefinitions);
				HttpRequestConfig request = CreateUpdateLookupsRequest(
					lookupDefinitions,
					lookupValueDefinitions,
					cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, $"{lookupDefinitions.Count} lookups, {lookupValueDefinitions.Count} lookup values.");
				IHttpResponse response = _httpClient.SendWithJsonBody(request);
				ProcessResponse<object>(response);
			} catch (Exception ex) {
				LogOperationError(nameof(UploadLookup), ex);
			}
		}

		/// <inheritdoc/>
		public void DeleteLookup(Guid lookupId, CancellationToken cancellationToken = default) {
			try {
				HttpRequestConfig request = CreateDeleteLookupRequest(lookupId, cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, lookupId);
				IHttpResponse response = _httpClient.Send(request);
				ProcessResponse<object>(response);
			} catch (Exception ex) {
				LogOperationError(nameof(DeleteLookup), ex);
			}
		}

		/// <inheritdoc/>
		public void UpdateLookupsForValue(Guid valuesSchemaUId, CancellationToken cancellationToken = default) {
			try {
				List<LookupDefinition> lookupDefinitions = _lookupHandler.GetLookupDefinitionsForValue(valuesSchemaUId);
				List<LookupValueDefinition> lookupValueDefinitions = _lookupHandler.GetLookupValueDefinitionsForLookups(lookupDefinitions);
				HttpRequestConfig request = CreateUpdateLookupsRequest(lookupDefinitions, lookupValueDefinitions, cancellationToken);
				LogOperationInfo(request.Url.AbsolutePath, $"{lookupDefinitions.Count} lookups, {lookupValueDefinitions.Count} lookup values.");
				IHttpResponse response = _httpClient.SendWithJsonBody(request);
				ProcessResponse<object>(response);
			} catch (Exception ex) {
				LogOperationError(nameof(UpdateLookupsForValue), ex);
			}
		}

		/// <inheritdoc/>
		public void UploadPendingDataStructures() {
			_log.Info("Checking data structure state after application start");
			DataForgeCheckTablesResponse response = CheckTablesState();
			if (response.Success) {
				if (response.TableNames.Count > 0) {
					_log.Info($"Uploading data structure after application start, {response.TableNames.Count} tables found");
					var schemaManager = _userConnection.EntitySchemaManager;
					var items = response.TableNames
						.Select(name => schemaManager.FindItemByName(name))
						.Where(item => item != null)
						.ToArray();
					_log.Info($"Uploading data structure after application start");
					UploadDataStructure(CancellationToken.None, items);
				} else {
					_log.Info("No tables to upload after application start");
				}
			}
		}

		/// <inheritdoc/>
		public void UploadPendingLookups() {
			_log.Info("Checking lookups state after application start");
			DataForgeCheckLookupsResponse checkLookupsResponse = CheckLookupsState();
			if (checkLookupsResponse.Success) {
				if (checkLookupsResponse.LookupIds.Count > 0) {
					_log.Info($"Uploading lookups after application start, {checkLookupsResponse.LookupIds.Count} lookups found");
					UploadLookups(checkLookupsResponse.LookupIds);
				} else {
					_log.Info($"No lookups to upload after application start, {checkLookupsResponse.LookupIds.Count} lookups found");
				}
			}
		}

		/// <inheritdoc/>
		public bool IsDataForgeEnabled() {
			try {
				Uri serviceUrl = ServiceUrl;
				if (serviceUrl == null) {
					_log.Info("DataForge service is disabled: DataForgeServiceUrl system setting is empty or not configured.");
					return false;
				}

				List<string> missingIdentitySettings = GetMissingIdentitySettings();
				if (missingIdentitySettings.Count > 0) {
					_log.Info($"DataForge service is disabled: Missing identity settings - {string.Join(", ", missingIdentitySettings)}");
					return false;
				}

				_log.Debug($"DataForge service is enabled with URL: {serviceUrl}");
				return true;
			} catch (Exception ex) {
				_log.Warn($"DataForge service is disabled: Error accessing DataForgeServiceUrl - {ex.Message}");
				return false;
			}
		}

		#endregion

	}
}

