namespace Terrasoft.Configuration.DataForge
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using Terrasoft.Core.ServiceModelContract;

	#region Class: DataForgeGetTableColumnsDetailsResponse

	[DataContract]
	public class DataForgeGetTableColumnsDetailsResponse : BaseResponse
	{
		[DataMember(Name = "data")]
		public TableColumnsDetailsData Data { get; set; }
	}

	#endregion

	#region Class: TableColumnsDetailsData

	[DataContract]
	public class TableColumnsDetailsData
	{
		[DataMember(Name = "tableName")]
		public string TableName { get; set; }

		[DataMember(Name = "columns")]
		public List<TableColumnDetails> Columns { get; set; }
	}

	#endregion

	#region Class: TableColumnDetails

	[DataContract]
	public class TableColumnDetails
	{
		[DataMember(Name = "columnName")]
		public string ColumnName { get; set; }

		[DataMember(Name = "columnCaption", EmitDefaultValue = false)]
		public string ColumnCaption { get; set; }

		[DataMember(Name = "columnDescription", EmitDefaultValue = false)]
		public string ColumnDescription { get; set; }

		[DataMember(Name = "columnType")]
		public string ColumnType { get; set; }

		[DataMember(Name = "columnRefersToTable", EmitDefaultValue = false)]
		public string ColumnRefersToTable { get; set; }

		[DataMember(Name = "columnRequired")]
		public bool ColumnRequired { get; set; }
	}

	#endregion
}

