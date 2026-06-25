namespace Terrasoft.Configuration.DataForge
{
	using Core.Factories;
	using Core.Requests;
	using global::Common.Logging;
	using OAuthIntegration;
	using System;
	using System.Runtime.Serialization;
	using System.ServiceModel;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Web;
	using System.Threading;
	using Terrasoft.Web.Common;
	using Terrasoft.Web.Common.ServiceRouting;
	using SysSettings = Terrasoft.Core.Configuration.SysSettings;

	#region Class: ServiceStatus

	[DataContract]
	public class ServiceStatus
	{
		[DataMember]
		public bool IsOnline { get; set; }
		[DataMember]
		public ProbeStatus Liveness { get; set; }
		[DataMember]
		public ProbeStatus Readiness { get; set; }
		[DataMember]
		public string DataStructureReadiness { get; set; }
		[DataMember]
		public string LookupsReadinessInfo { get; set; }
	}

	#endregion

	#region Class: ProbeStatus

	[DataContract]
	public class ProbeStatus
	{
		[DataMember]
		public int HttpStatusCode { get; set; }
		[DataMember]
		public string Message { get; set; }
	}

	#endregion

	#region Class: DataForgeMaintenanceService

	[ServiceContract]
	[DefaultServiceRoute]
	[SspServiceRoute]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class DataForgeMaintenanceService : BaseService
	{
		#region Fields: Private

		private static readonly ILog _log = LogManager.GetLogger("DataForge");
		private readonly IHttpRequestClient _httpClient = ClassFactory.Get<IHttpRequestClient>();
		private readonly IIdentityServiceWrapper _identityService = IdentityServiceWrapperHelper.GetInstance();
		private readonly IDataForgeJobScheduler _dataForgeJobScheduler = ClassFactory.Get<IDataForgeJobFactory>().Create();

		#endregion

		#region Properties: Private

		private Uri ServiceUrl {
			get {
				var value = (string)SysSettings.GetValue(UserConnection, "DataForgeServiceUrl");
				return new Uri(value);
			}
		}

		private int RequestTimeout {
			get {
				int value = SysSettings.GetValue(UserConnection, "DataForgeServiceQueryTimeout",
					30000);
				if (value < 0) {
					value = 0;
				}
				return value / 1000;
			}
		}

		#endregion

		#region Methods: Private

		private IHttpResponse SendRequest(string relativePath, IHttpRequestClient client, CancellationToken token = default) {
			var url = new Uri(ServiceUrl, relativePath);
			var request = new HttpRequestConfig {
				Url = url,
				Method = HttpRequestMethod.GET,
				RequestTimeout = RequestTimeout,
				CancellationToken = token
			}.WithOAuth<DataForgeFeatures.UseOAuth>(_identityService, string.Empty);
			return client.Send(request);
		}

		private ProbeStatus GetProbeStatus(string path, IHttpRequestClient client, CancellationToken token = default) {
			var response = SendRequest(path, client, token);
			return new ProbeStatus {
				HttpStatusCode = (int)response.StatusCode,
				Message = response.Exception != null || !response.IsSuccessStatusCode
					? response.Exception?.Message ?? $"Status {response.StatusCode}"
					: response.Content
			};
		}

		private string GetReadinessDetails(string relativePath, IHttpRequestClient client, CancellationToken token = default) {
			var response = SendRequest(relativePath, client, token);
			if (response.Exception != null || !response.IsSuccessStatusCode) {
				_log.Error($"Request failed for {relativePath}. StatusCode: {response.StatusCode}, Content: {response.Content}");
				return response.Content ?? response.Exception?.Message;
			}
			return response.Content;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Performs initialization on the DataForge microservice by creating any required
		/// database structures and lookups.
		/// </summary>
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public void InitializeDataStructuresAndLookups() {
			_dataForgeJobScheduler.Schedule<DataForgeInitializeDataStructureJob>();
			_dataForgeJobScheduler.Schedule<DataForgeInitializeLookupsJob>();
		}

		/// <summary>
		/// Performs an incremental update on the DataForge microservice by uploading
		/// any data structures or lookups that are pending since the last initialization.
		/// </summary>
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public void UpdateDataStructuresAndLookups() {
			_dataForgeJobScheduler.Schedule<DataForgeUploadPendingDataStructuresJob>();
			_dataForgeJobScheduler.Schedule<DataForgeUploadPendingLookupsJob>();
		}

		/// <summary>
		/// Retrieves the current liveness and readiness status of the DataForge microservice,
		/// including status for both data structures and lookups.
		/// </summary>
		/// <returns>
		/// A <see cref="ServiceStatus"/> instance describing the current liveness and readiness
		/// of the data forge microservice.
		/// </returns>
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		public ServiceStatus GetServiceStatus() {
			var status = new ServiceStatus();
			var liveResult = GetProbeStatus("liveness", _httpClient);
			status.Liveness = liveResult;
			status.IsOnline = liveResult.HttpStatusCode != 0;

			if (status.IsOnline) {
				status.Readiness = GetProbeStatus("readiness", _httpClient);
				if (status.Readiness.HttpStatusCode == 200) {
					status.DataStructureReadiness = GetReadinessDetails("api/v1/dataStructure/readiness", _httpClient);
					status.LookupsReadinessInfo = GetReadinessDetails("api/v1/lookups/readiness", _httpClient);
				}
			}

			return status;
		}

		#endregion

	}

	#endregion
}

