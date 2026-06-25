namespace Terrasoft.Configuration
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Runtime.Serialization;
	using System.Text;
	using System.Web;
	using global::Common.Logging;
	using Polly;

	#region Interface: IWebServiceClient

	/// <summary>
	/// Performs requests to specified web service.
	/// </summary>
	public interface IWebServiceClient
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the end point of the service.
		/// </summary>
		string EndPoint { get; set; }

		/// <summary>
		/// Gets a status code of the request.
		/// </summary>
		HttpStatusCode StatusCode { get; }

		/// <summary>
		/// Gets a status description of the request.
		/// </summary>
		string StatusDescription { get; }

		/// <summary>
		/// Gets or sets a value that indicates whether the request should follow redirection responses.
		/// </summary>
		bool AllowAutoRedirect { get; set; }

		/// <summary>
		/// Gets or sets a message of the caught exception.
		/// </summary>
		Dictionary<string, string> Headers { get; set; }

		/// <summary>
		/// Gets or sets a timeout in milliseconds (default 60000 = 60 seconds).
		/// </summary>
		int RequestTimeout { get; set; }

		/// <summary>
		/// Gets or sets a content type.
		/// </summary>
		RequestContentType ContentType { get; set; }

		/// <summary>
		/// Gets or sets a success request flag.
		/// </summary>
		bool Success { get; }

		/// <summary>
		/// Gets or sets a message of the caught exception.
		/// </summary>
		WebException Exception { get; }

		#endregion

		#region Methods: Public

		/// <summary>
		/// Returns new instance of <see cref="HttpWebRequest"/> use for executing request.
		/// </summary>
		/// <param name="method">Http method verb <see cref="HttpAction"/>.</param>
		/// <param name="servicePath">The service path.</param>
		/// <returns>Instance of <see cref="HttpWebRequest"/>.</returns>
		HttpWebRequest GetRequest(HttpMethod method, string servicePath);

		/// <summary>
		/// Returns new instance of <see cref="HttpWebRequest"/> use for executing request.
		/// </summary>
		/// <param name="method">Http method verb <see cref="HttpAction"/>.</param>
		/// <returns>Instance of <see cref="HttpWebRequest"/>.</returns>
		HttpWebRequest GetRequest(HttpMethod method);

		/// <summary>
		/// Get response serialized to Json.
		/// </summary>
		/// <returns>Json-string.</returns>
		string GetResponseJson();

		/// <summary>
		/// Performs POST request.
		/// </summary>
		/// <param name="request">The request object.</param>
		/// <param name="servicePath">The service path.</param>
		/// <typeparam name="T">The type of the response.</typeparam>
		/// <returns>Typed response.</returns>
		T Post<T>(object request, string servicePath)
			where T : class;


		/// <summary>
		/// Performs POST request.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <typeparam name="T">The type of the response.</typeparam>
		/// <returns>Typed response.</returns>
		T Post<T>(object request)
			where T : class;

		#endregion

	}

	public interface IWebServiceClientWithRetry: IWebServiceClient
	{
		/// <summary>
		/// Performs POST request with retry when exception occurred.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="servicePath">The service path.</param>
		/// <returns>Typed response.</returns>
		T PostWithRetry<T>(object request, string servicePath)
			where T : class;
	}

	#endregion

	#region Class: WebServiceClient

	/// <summary>
	/// Performs requests to specified web service.
	/// </summary>
	public class WebServiceClient : IWebServiceClientWithRetry
	{

		#region Fields: Private

		private string _responseJson;
		private readonly Policy _retryPolicy;
		private readonly Context _retryContext;
		private static readonly int _retryCount = 3;
		private const string CurrentRequest = "CurrentRequest";
		private static readonly ILog _log = LogManager.GetLogger("Mailing");
		#endregion

		#region Constructors: Private

		/// <summary>
		/// Initializes a new instance of the <see cref="WebServiceClient"/> class.
		/// </summary>
		static WebServiceClient() {
			ServicePointManager.Expect100Continue = true;
#if NETFRAMEWORK
			ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12
				| SecurityProtocolType.Tls11
				| SecurityProtocolType.Tls
				| SecurityProtocolType.Ssl3;
#endif
		}

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="WebServiceClient"/> class.
		/// </summary>
		public WebServiceClient() {
			Headers = new Dictionary<string, string>();
			_retryPolicy = Policy.Handle<Exception>().WaitAndRetry(_retryCount, 
				retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
				(exception, timeSpan, retryIteration, context) => OnRetry(context, retryIteration, exception));
			_retryContext = new Context("WebServiceClient", new Dictionary<string, object>());
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WebServiceClient"/> class.
		/// </summary>
		/// <param name="endPoint">The web service url.</param>
		public WebServiceClient(string endPoint)
			: this() {
			EndPoint = endPoint;
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Gets or sets the end point of the service.
		/// </summary>
		public string EndPoint { get; set; }
		/// <summary>
		/// Gets a status code of the request.
		/// </summary>
		public HttpStatusCode StatusCode { get; private set; }
		/// <summary>
		/// Gets a status description of the request.
		/// </summary>
		public string StatusDescription { get; private set; }
		/// <summary>
		/// Gets or sets a value that indicates whether the request should follow redirection responses.
		/// </summary>
		public bool AllowAutoRedirect { get; set; }
		/// <summary>
		/// Gets or sets a message of the caught exception.
		/// </summary>
		public Dictionary<string, string> Headers { get; set; }
		/// <summary>
		/// Gets or sets a timeout in milliseconds (default 60000 = 60 seconds).
		/// </summary>
		public int RequestTimeout { get; set; } = 60000;
		/// <summary>
		/// Gets or sets a content type.
		/// </summary>
		public RequestContentType ContentType { get; set; } = RequestContentType.Json;
		/// <summary>
		/// Gets or sets a success request flag.
		/// </summary>
		public bool Success { get; set; }
		/// <summary>
		/// Gets or sets a message of the caught exception.
		/// </summary>
		public WebException Exception { get; set; }
		/// <summary>
		/// Gets or sets a flag that allow web exceptions while request.
		/// </summary>
		public bool AllowWebException { get; set; }

		/// <summary>
		/// Gets or sets request body.
		/// </summary>
		public byte[] LastRequestBody { get; private set; }

		#endregion

		#region Methods: Private

		/// <summary>
		/// Generates string with parameter and value data from object properties.
		/// Properties with [ServiceStack.DataAnnotations.Ignore] attribute will be ignored.
		/// To change the name of the parameter can be used [System.Runtime.Serialization.DataMemberAttribute].
		/// </summary>
		/// <param name="requestData">Object with properties.</param>
		/// <param name="paramValuePattern">String pattern for parameter and value data.</param>
		/// <param name="paramJoinString">Parameters concatenation string.</param>
		/// <param name="useValueEncoding">Value encoding sign.</param>
		/// <returns>Parameter value string.</returns>
		public static string GetParametersString(object requestData, string paramValuePattern, string paramJoinString,
			bool useValueEncoding = true) {
			var properties = requestData.GetType().GetProperties();
			var valuesList = from property in properties
							 let attributes = property.GetCustomAttributes(true)
							 let propValue = property.GetValue(requestData, null)
							 where propValue != null
							 let paramValue = property.PropertyType.IsEnum ? ((int)propValue).ToString() : propValue
							 let descriptionAttribute = attributes.OfType<DataMemberAttribute>().FirstOrDefault()
							 let propName = descriptionAttribute != null ? descriptionAttribute.Name : property.Name
							 let value = useValueEncoding ? HttpUtility.UrlEncode(paramValue.ToString()) : paramValue.ToString()
							 select string.Format(paramValuePattern, propName, value);
			var result = string.Join(paramJoinString, valuesList);
			return result;
		}

		private static void OnRetry(Context context, int retryIteration, Exception exception) {
			_log.WarnFormat(
				"[WebServiceClient]: " +
				$"Error while executing request. Iteration: {retryIteration - 1}, Request: #1; Exception: #1;"
				, context[CurrentRequest], exception);
		}

		private void CheckEndPoint() {
			if (string.IsNullOrEmpty(EndPoint)) {
				throw new ArgumentNullException("EndPoint");
			}
		}

		private T SendRequest<T>(HttpMethod method, object requestBody, string servicePath)
			where T : class {
			return SendRequest<T>(method, requestBody, servicePath, null);
		}

		private T SendRequest<T>(HttpMethod method, object requestBody, string servicePath, int? timeout)
				where T : class {
			Success = true;
			var request = GetRequest(method, servicePath);
			if (requestBody == null) {
				request.ContentLength = 0;
			}
			if (method != HttpMethod.Get && method != HttpMethod.Head && requestBody != null) {
				SetRequestBody(request, requestBody);
			}
			request.Timeout = timeout ?? RequestTimeout;
			_responseJson = GetResponse(request);
			return DeserializeResponse<T>(_responseJson);
		}

		protected virtual string GetResponse(HttpWebRequest request) {
			string responseJson;
			using (var webResponse = request.GetResponse()) {
				using (var streamResponse = webResponse.GetResponseStream()) {
					var httpWebResponse = webResponse as HttpWebResponse;
					StatusCode = httpWebResponse.StatusCode;
					StatusDescription = httpWebResponse.StatusDescription;
					if (streamResponse == null) {
						return null;
					}
					using (var reader = new StreamReader(streamResponse)) {
						responseJson = reader.ReadToEnd();
					}
				}
			}
			return responseJson;
		}

		private T DeserializeResponse<T>(string responseJson)
			where T : class {
			T response;
			if (typeof(T) == typeof(string)) {
				response = responseJson as T;
			} else {
				response = string.IsNullOrEmpty(responseJson)
					? null
					: JsonConvert.DeserializeObject<T>(responseJson);
			}
			return response;
		}

		private void SetRequestBody(HttpWebRequest request, object requestBody) {
			string stringRequest;
			byte[] byteArray;
			request.ContentType = "application/json";
			if (requestBody is byte[]) {
				byteArray = requestBody as byte[];
			} else {
				stringRequest = JsonConvert.SerializeObject(requestBody);
				byteArray = Encoding.UTF8.GetBytes(stringRequest);
			}
			LastRequestBody = byteArray;
			using (var stream = request.GetRequestStream()) {
				stream.Write(byteArray, 0, byteArray.Length);
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Returns new instance of <see cref="HttpWebRequest"/> use for executing request.
		/// </summary>
		/// <param name="method">Http method verb <see cref="HttpMethod"/>.</param>
		/// <returns>Instance of <see cref="HttpWebRequest"/>.</returns>
		public virtual HttpWebRequest GetRequest(HttpMethod method) {
			return GetRequest(method, string.Empty);
		}

		/// <summary>
		/// Returns new instance of <see cref="HttpWebRequest"/> use for executing request.
		/// </summary>
		/// <param name="method">Http method verb <see cref="HttpMethod"/>.</param>
		/// <param name="servicePath">The service path.</param>
		/// <returns>Instance of <see cref="HttpWebRequest"/>.</returns>
		public virtual HttpWebRequest GetRequest(HttpMethod method, string servicePath) {
			CheckEndPoint();
			var serviceUrl = EndPoint + servicePath;
			var request = (HttpWebRequest)WebRequest.Create(serviceUrl);
			request.KeepAlive = false;
			request.AllowAutoRedirect = AllowAutoRedirect;
			request.Method = method.ToString();
			if (Headers == null || Headers.Count <= 0) {
				return request;
			}
			foreach (var header in Headers) {
				request.Headers.Add(header.Key, header.Value);
			}
			return request;
		}

		/// <summary>
		/// Get response serialized to Json.
		/// </summary>
		/// <returns>Json-string.</returns>
		public string GetResponseJson() {
			return _responseJson;
		}

		/// <summary>
		/// Performs POST request.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>Typed response.</returns>
		public T Post<T>(object request)
			where T : class {
			return Post<T>(request, string.Empty);
		}

		/// <summary>
		/// Performs POST request.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="servicePath">The service path.</param>
		/// <returns>Typed response.</returns>
		public T Post<T>(object request, string servicePath)
			where T : class {
			CheckEndPoint();
			return SendRequest<T>(HttpMethod.Post, request, servicePath);
		}

		/// <summary>
		/// Performs POST request with retry when exception occurred.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="servicePath">The service path.</param>
		/// <returns>Typed response.</returns>
		public T PostWithRetry<T>(object request, string servicePath)
			where T : class {
			CheckEndPoint();
			_retryContext[CurrentRequest] = request;
			return _retryPolicy.Execute(() => SendRequest<T>(HttpMethod.Post, request, servicePath), _retryContext);
		}

		#endregion

	}

	#endregion
}
