namespace Creatio.Copilot
{
	using System.Runtime.Serialization;

	#region Class: CopilotConfirmationMessageModel

	[DataContract]
	public class CopilotConfirmationMessageModel
	{

		#region Fields: Public

		[DataMember(Name = "confirmButtonTitle")]
		public string ConfirmButtonTitle { get; set; }

		[DataMember(Name = "cancelButtonTitle")]
		public string CancelButtonTitle { get; set; }

		[DataMember(Name = "messageBody")]
		public string MessageBody { get; set; }

		#endregion

	}

	#endregion

}
