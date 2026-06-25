namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Client
{
	using System;
	using System.IO;
	using System.Net;
	using System.Threading.Tasks;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
	using Terrasoft.Common;
	using Terrasoft.Core.Factories;
	using Terrasoft.Social.OAuth;

	#region Class: GraphApiAbstractClient

	/// <summary>
	/// PoC implementation of graph API usage.
	/// </summary>
	public abstract class GraphApiAbstractClient
	{

		#region Fields: Protected

		/// <summary>
		/// <see cref="ICalendarLogger"/> implementation instance.
		/// </summary>
		protected ICalendarLogger _log;

		#endregion

		#region Methods: Protected

		protected async Task<string> SendRequest(string serviceUri, string method, byte[] data, string token = null, string acceptContentType = null) {
			var requestFactory = ClassFactory.Get<IHttpWebRequestFactory>();
			WebRequest request = requestFactory.Create(serviceUri);
			request.Method = method;
			if (!string.IsNullOrEmpty(acceptContentType))
				((HttpWebRequest)request).Accept = acceptContentType;
			if (data != null && data.Length > 0) {
				request.ContentType = "application/json; charset=utf-8";
				request.ContentLength = data.Length;
			}
			if (token != null)
				request.Headers.Add("Authorization", $"Bearer {token}");
			request.Timeout = 5 * 60 * 1000;
			WebResponse response = null;
			try {
				if (data != null && data.Length > 0)
					using (Stream stream = request.GetRequestStream()) {
						stream.Write(data, 0, data.Length);
					}
				response = request.GetResponse();
				using (Stream dataStream = response.GetResponseStream()) {
					var reader = new StreamReader(dataStream);
					return await reader.ReadToEndAsync();
				}
			} catch (Exception ex) {
				var logMessage = $"Error calling {serviceUri} (method {method}).";
				string responseContent = ex.GetExceptionContent();
				if (responseContent.IsNotNullOrEmpty())
					logMessage += $"\r\nResponse content '{responseContent}'";
				_log?.LogError(logMessage, ex);
				throw;
			} finally {
				response?.Close();
			}
		}

		#endregion

	}

	#endregion

}
