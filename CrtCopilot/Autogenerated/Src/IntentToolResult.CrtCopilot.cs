namespace Creatio.Copilot
{
	using System.Runtime.Serialization;

	[DataContract]
	internal class IntentToolResult
	{
		[DataMember(Name = "isSuccess")]
		public bool IsSuccess { get; set; } = true;

		[DataMember(Name = "warning", EmitDefaultValue = false)]
		public string Warning { get; set; }

		[DataMember(Name = "eventType")]
		public string EventType { get; set; }

		[DataMember(Name = "caption")]
		public string Caption { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "currentAgent")]
		public string CurrentAgent { get; set; }

		[DataMember(Name = "currentSkill")]
		public string CurrentSkill { get; set; }
	}
} 
