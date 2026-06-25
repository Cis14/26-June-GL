namespace Creatio.Copilot
{

	#region Interface: IUnifiedSummarizationSettings

	/// <summary>
	/// Defines the settings required for unified summarization in the Copilot session.
	/// </summary>
	public interface IUnifiedSummarizationSettings {
		string PromptTemplate { get; }
		int MinRequiredMessages { get; }
		int MinRequiredCharacters { get; }
		int MinIntentsCountInSession { get; }
	}

	#endregion

}
