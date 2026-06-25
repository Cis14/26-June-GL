namespace Creatio.Copilot
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	#region Class: CopilotConfirmationToolContextModel

	[DataContract]
	public class CopilotConfirmationToolContextModel
	{
		[DataMember(Name = "actions")]
		public List<CopilotConfirmationToolActionModel> Actions { get; set; } = new List<CopilotConfirmationToolActionModel>();
	}

	#endregion

	#region Class: CopilotConfirmationToolActionModel

	[DataContract]
	public class CopilotConfirmationToolActionModel
	{
		[DataMember(Name = "toolCallId")]
		public string ToolCallId { get; set; }

		[DataMember(Name = "toolName")]
		public string ToolName { get; set; }

		[DataMember(Name = "toolDescription")]
		public string Description { get; set; }

		[DataMember(Name = "parameters")]
		public List<ToolContextParameterModel> Parameters { get; set; } = new List<ToolContextParameterModel>();
	}

	#endregion

	#region Class: ToolContextParameterModel

	[DataContract]
	public class ToolContextParameterModel
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "value")]
		public object Value { get; set; }
	}

	#endregion

}

