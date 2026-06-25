namespace Creatio.Copilot
{

	#region Interface: ICopilotConfirmationMessageGenerator

	public interface ICopilotConfirmationMessageGenerator
	{
		CopilotMessage GenerateConfirmationMessage(CopilotConfirmationToolContextModel toolContext);
	}

	#endregion

}

