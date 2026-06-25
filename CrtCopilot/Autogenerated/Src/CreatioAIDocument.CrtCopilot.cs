namespace Creatio.Copilot
{
	using System;
	using System.Runtime.Serialization;

	#region Class: CreatioAIDocument

	/// <inheritdoc/>
	[DataContract]
	[Serializable]
	public class CreatioAIDocument : ICreatioAIDocument
	{

		#region Properties: Public

		/// <inheritdoc/>
		[DataMember(Name = "intentId")]
		public Guid? IntentId { get; set; }

		/// <inheritdoc/>
		[DataMember(Name = "sessionId")]
		public Guid? SessionId { get; set; }

		/// <inheritdoc/>
		[DataMember(Name = "fileId")]
		public Guid FileId { get; set; }

		/// <inheritdoc/>
		[DataMember(Name = "fileName")]
		public string FileName { get; set; }

		/// <inheritdoc/>
		[DataMember(Name = "fileSchemaName")]
		public string FileSchemaName { get; set; }

		#endregion

	}

	#endregion

}
