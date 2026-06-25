namespace Terrasoft.Configuration.DataForge
{
	using System.Runtime.Serialization;

	#region Class: GetSimilarTableNamesRequest

	/// <summary>
	/// Request model for retrieving similar table names.
	/// </summary>
	[DataContract]
	public class GetSimilarTableNamesRequest
	{
		/// <summary>
		/// Search query string.
		/// </summary>
		[DataMember(Name = "query")]
		public string Query { get; set; }

		/// <summary>
		/// Maximum number of results to return.
		/// </summary>
		[DataMember(Name = "limit")]
		public int? Limit { get; set; }
	}

	#endregion

	#region Class: GetTableDetailsRequest

	/// <summary>
	/// Request model for retrieving table details.
	/// </summary>
	[DataContract]
	public class GetTableDetailsRequest
	{
		/// <summary>
		/// Search query string.
		/// </summary>
		[DataMember(Name = "query")]
		public string Query { get; set; }

		/// <summary>
		/// Maximum number of results to return.
		/// </summary>
		[DataMember(Name = "limit")]
		public int? Limit { get; set; }
	}

	#endregion

	#region Class: GetTableRelationshipsRequest

	/// <summary>
	/// Request model for retrieving table relationships.
	/// </summary>
	[DataContract]
	public class GetTableRelationshipsRequest
	{
		/// <summary>
		/// Source table name.
		/// </summary>
		[DataMember(Name = "sourceTable")]
		public string SourceTable { get; set; }

		/// <summary>
		/// Target table name.
		/// </summary>
		[DataMember(Name = "targetTable")]
		public string TargetTable { get; set; }

		/// <summary>
		/// Maximum number of results to return.
		/// </summary>
		[DataMember(Name = "limit")]
		public int? Limit { get; set; }

		/// <summary>
		/// Whether to search bidirectionally.
		/// </summary>
		[DataMember(Name = "bidirectional")]
		public bool? Bidirectional { get; set; }

		/// <summary>
		/// Whether to skip detailed information.
		/// </summary>
		[DataMember(Name = "skipDetails")]
		public bool? SkipDetails { get; set; }
	}

	#endregion

	#region Class: GetLookupValuesRequest

	/// <summary>
	/// Request model for retrieving lookup values.
	/// </summary>
	[DataContract]
	public class GetLookupValuesRequest
	{
		/// <summary>
		/// Search query string.
		/// </summary>
		[DataMember(Name = "query")]
		public string Query { get; set; }

		/// <summary>
		/// Optional schema name to filter lookups.
		/// </summary>
		[DataMember(Name = "schemaName")]
		public string SchemaName { get; set; }

		/// <summary>
		/// Maximum number of results to return.
		/// </summary>
		[DataMember(Name = "limit")]
		public int? Limit { get; set; }
	}

	#endregion

	#region Class: GetTableColumnsDetailsRequest

	/// <summary>
	/// Request model for retrieving table column details.
	/// </summary>
	[DataContract]
	public class GetTableColumnsDetailsRequest
	{
		/// <summary>
		/// Table name to retrieve column details for.
		/// </summary>
		[DataMember(Name = "tableName")]
		public string TableName { get; set; }
	}

	#endregion
}

