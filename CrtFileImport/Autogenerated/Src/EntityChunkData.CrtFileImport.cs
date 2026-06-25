namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region  Class: EntityChunkData

	[Serializable]
	public class EntityChunkData
	{

		#region Properties: Public

		[DataMember(Name = "entities")]
		public IEnumerable<ImportEntityMemento> Entities { get; set; }

		[DataMember(Name = "successProcessedEntityCount")]
		public int SuccessProcessedEntityCount { get; set; }

		[DataMember(Name = "processedWithErrorEntityCount")]
		public int ProcessedWithErrorEntityCount { get; set; }

		#endregion

	}

	#endregion

}

