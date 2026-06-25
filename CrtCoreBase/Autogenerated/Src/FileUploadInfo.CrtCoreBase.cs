namespace Terrasoft.Configuration.FileUpload
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.IO;
	using System.Linq;
	using System.Net.Http.Headers;
	using System.Text.RegularExpressions;
	using Newtonsoft.Json;
	using Terrasoft.Common;
	using Terrasoft.WebApp;

#if NETSTANDARD2_0 // TODO CRM-46497
	using TS = Terrasoft.Web.Http.Abstractions;
#else
	using MS = System.Web;
#endif
	#region Class: FileUploadInfo

	public class FileUploadInfo : IFileUploadInfo
	{

		#region Constants: Private

		private const string ContentRangeHeaderName = "Content-Range";
		private const string RangeHeaderName = "Range";

		#endregion

		#region Fields: Private
#if NETSTANDARD2_0 // TODO CRM-46497
		private TS.HttpRequest _request;
		private TS.RequestDataCollection _queryString;
#else
		private MS.HttpRequestBase _request;
		private NameValueCollection _queryString;
#endif
		private readonly Stream _fileContent;
		private readonly IResourceStorage _storage;

		/// <summary>
		/// Regular expression for the first chunk.
		/// </summary>
		private Regex _firstChunkRegex = new Regex(@"^\D*0");

		#endregion

		#region Constructors: Public
#if NETSTANDARD2_0 // TODO CRM-46497
		public FileUploadInfo(Stream fileContent, TS.HttpRequest request, IResourceStorage storage) {
			_request = request;
			_fileContent = fileContent;
			_storage = storage;
		}
#else
		public FileUploadInfo(Stream fileContent, MS.HttpRequestBase request, IResourceStorage storage) {
			_request = request;
			_fileContent = fileContent;
			_storage = storage;
		}
#endif
		#endregion

		#region Properties: Protected
#if NETSTANDARD2_0 // TODO CRM-46497
		protected TS.RequestDataCollection QueryString {
			get => _queryString 
				?? (_queryString = IsChunkedUpload 
					? (TS.RequestDataCollection) _request.QueryString
					: _request.Form
				);
		}
#else
		protected NameValueCollection QueryString {
			get {
				if (_queryString == null) {
					_queryString = IsChunkedUpload ? _request.QueryString : _request.Form;
				}
				return _queryString;
			}
		}
#endif
		#endregion

		#region Properties: Public

		/// <summary>
		/// File entity schema name.
		/// </summary>
		public string EntitySchemaName => QueryString["entitySchemaName"];

		/// <summary>
		/// Data entity schema column name.
		/// </summary>
		public string ColumnName => QueryString["columnName"];

		/// <summary>
		/// File name.
		/// </summary>
		public string FileName => IsChunkedUpload ? QueryString["fileName"] : _request.Files["files"].FileName;

		/// <summary>
		/// File identifier.
		/// </summary>
		public Guid FileId => new Guid(QueryString["fileId"]);

		/// <summary>
		/// File type identifier.
		/// </summary>
		public virtual Guid TypeId {
			get {
				string fileTypeId = QueryString["fileTypeId"];
				return string.IsNullOrEmpty(fileTypeId) ? FileConsts.FileTypeUId : new Guid(fileTypeId);
			}
		}

		/// <summary>
		/// Total file length.
		/// </summary>
		public decimal TotalFileLength {
			get {
				string totalFileLengthString = QueryString["totalFileLength"];
				if (!decimal.TryParse(totalFileLengthString, out decimal totalFileLength)) {
					throw new InvalidFileSizeException(_storage);
				}
				return totalFileLength;
			}
		}

		/// <summary>
		/// Parent entity schema name.
		/// </summary>
		public string ParentEntitySchemaName => QueryString["parentEntitySchemaName"];

		/// <summary>
		/// Parent column name.
		/// </summary>
		public string ParentColumnName => QueryString["parentColumnName"];

		/// <summary>
		/// Parent column value.
		/// </summary>
		public Guid ParentColumnValue => new Guid(QueryString["parentColumnValue"]);

		/// <summary>
		/// File content.
		/// </summary>
		public Stream Content {
			get {
				if (!IsChunkedUpload) {
					return _request.Files["files"].InputStream;
				}
				byte[] fileContentBytes = new byte[_request.TotalBytes];
				_fileContent.Read(fileContentBytes, 0, fileContentBytes.Length);
				return new MemoryStream(fileContentBytes);
			}
		}

		/// <summary>
		///  Additional parameters.
		/// </summary>
		public Dictionary<string, object> AdditionalParams {
			get {
				var value = new Dictionary<string, object>();
				string additionalParams = QueryString["AdditionalParams"];
				if (additionalParams.IsNotNullOrEmpty()) {
					value = JsonConvert.DeserializeObject<Dictionary<string, object>>(additionalParams);
				}
				return value;
			}
		}

		/// <summary>
		/// Determines if it is chunked upload.
		/// </summary>
		public bool IsChunkedUpload => !string.IsNullOrEmpty(ByteRange);

		/// <summary>
		/// Byte range.
		/// </summary>
		/// <remarks>Range examples: 0-, 123-456. Content range example: 123-456/789.</remarks>
		public string ByteRange {
			get {
				string value = _request.Headers[RangeHeaderName];
				if (value.IsNullOrEmpty()) {
					value = _request.Headers[ContentRangeHeaderName];
				}
				return value;
			}
		}

		/// <summary>
		/// File version.
		/// </summary>
		public int Version => 1;

		/// <inheritdoc cref="IFileUploadInfo.IsFirstChunk"/>
		public bool IsFirstChunk => _firstChunkRegex.IsMatch(ByteRange);

		/// <inheritdoc cref="IFileUploadInfo.IsLastChunk"/>
		public bool IsLastChunk {
			get {
				string headerValue = _request.Headers[ContentRangeHeaderName];
				if (ContentRangeHeaderValue.TryParse(headerValue,
						out ContentRangeHeaderValue contentRangeHeaderValue)) {
					return contentRangeHeaderValue.To == contentRangeHeaderValue.Length - 1;
				}
				return true;
			}
		}

		#endregion

	}

	#endregion
}
