namespace Creatio.Copilot
{

	/// <summary>
	/// Interface for validating links in Copilot messages.
	/// </summary>
	public interface ICopilotLinkValidator
	{

		/// <summary>
		/// Validates and removes invalid links from the content.
		/// </summary>
		/// <param name="session">Copilot session.</param>
		/// <param name="content">Content to validate.</param>
		/// <param name="isApi">Indicates if the validation is for API.</param>
		/// <returns>Content with invalid links removed.</returns>
		string ValidateLinks(CopilotSession session, string content, bool isApi);

	}
}

