namespace Terrasoft.Configuration.DataForge
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using Terrasoft.Core.ServiceModelContract;

	#region Class: DataForgeGetTablesResponse

	/// <summary>
	/// Represents the response model for retrieving table names in the Data Forge service.
	/// </summary>
	[DataContract]
	public class DataForgeGetTablesResponse : BaseResponse
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the list of table names retrieved from the Data Forge service.
		/// </summary>
		[DataMember(Name = "data")]
		public List<string> Data { get; set; }

		#endregion

	}

	#endregion

	#region Class: SimilarTable

	/// <summary>
	/// Represents a database table with its details.
	/// </summary>
	public class SimilarTable
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the name of the table.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///	Gets or sets the description of the table.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the caption of the table.
		/// </summary>
		public string Caption { get; set; }

		#endregion

	}

	#endregion

	#region Class: DataForgeGetTablesDetailsResponse

	/// <summary>
	/// Represents the response model for retrieving detailed table information in the Data Forge service.
	/// </summary>
	public class DataForgeGetTablesDetailsResponse : BaseResponse
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the list of tables' details retrieved from the Data Forge service.
		/// </summary>
		[DataMember(Name = "data")]
		public List<SimilarTable> Data { get; set; }

		#endregion

	}

	#endregion

}

