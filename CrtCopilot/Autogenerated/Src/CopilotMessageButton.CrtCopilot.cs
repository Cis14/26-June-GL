namespace Creatio.Copilot
{
	using System;
	using System.Runtime.Serialization;

	#region Class: CopilotMessageConfirmationButton

	[DataContract]
	[Serializable]
	public class CopilotMessageButton
	{

		#region Constructors: Public

		public CopilotMessageButton(string caption, string action, string code) {
			Caption = caption;
			Action = action;
			Code = code;
		}

		#endregion

		#region Properties: Public

		[DataMember(Name = "caption", IsRequired = false)]
		public string Caption { get; set; }

		[DataMember(Name = "action", IsRequired = false)]
		public string Action { get; set; }

		[DataMember(Name = "code", IsRequired = false)]
		public string Code { get; set; }

		#endregion

	}

	#endregion

}
