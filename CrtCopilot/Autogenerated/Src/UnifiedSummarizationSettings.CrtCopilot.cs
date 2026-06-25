namespace Creatio.Copilot
{
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using SystemSettings = Terrasoft.Core.Configuration.SysSettings;

	#region Class: UnifiedSummarizationSettings

	/// <summary>
	/// Provides summarization settings by retrieving them from SystemSettings.
	/// </summary>
	[DefaultBinding(typeof(IUnifiedSummarizationSettings))]
	public class UnifiedSummarizationSettings : IUnifiedSummarizationSettings {

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private const string SummarizerPromptTemplate = @"You are an advanced AI assistant specializing in chat history compression for the Creatio platform. Your task is to analyze and compress conversations while preserving their essential meaning and context. This compression is crucial for maintaining quality in subsequent skill executions within Creatio.

Each message object in the array contains the following fields:
- role: A string indicating the message sender's role (e.g., 'user', 'assistant', 'system', 'tool')
- content: A string containing the message content
- tool_call_id: (Optional) A string for tool calls
- tool_calls: (Optional) An array for tool call details

Your task is to compress this conversation history into a structured summary. Follow these steps:

1. Analyze the entire message array to understand the full context of the conversation.

2. For each skill or intent, conduct a thorough analysis. Consider the following aspects:
 - Messages related to this skill/intent
 - Key user inputs, requirements, and important details
 - Important decisions made by the user
 - Errors or issues encountered, and their resolution status
 - Main actions taken by the assistant
 - Entities or data points that were input or output, including their exact names and IDs
 - Critical context or configuration changes
 - JSON outputs or other structured data
 - Artifacts resulting from the skill execution, including generated data (html, json, yaml, etc), entity names, record IDs, or any other important information that allows retrieval or reproduction of results

3. Based on your analysis, create a compressed summary for each skill using the following structure:

<compressed_summary>
<skill>
Skill: [Name of the skill or intent]

Entities:
- Input: [List exact names and IDs of input entities]
- Output: [List exact names and IDs of output entities]

User Input:
- [List important user inputs and details]

Actions:
- [Summarize main actions taken by the assistant]

Output:
- [List key outputs or results]

Issues:
- [List any errors or problems encountered, noting if they were resolved]

Artifacts:
- [List any artifacts that resulted from the skill execution, including generated data (html, json, yaml, etc), entity names, record IDs, and any generated data. This artifacts will be used in future skills that need this artifacts for next continiue conversation]

Context:
- [Note any critical context, decisions, or configuration changes]
</skill>
</compressed_summary>

Important guidelines:
- Focus on preserving the most critical information for each skill.
- Ensure that the compressed summary provides enough context for continuing the conversation or executing subsequent skills.
- Use clear and concise language in your summary.
- If there are multiple skills, list them in the order they appeared in the conversation.
- Preserve json/yaml/html/etc outputs or other structured data as closely as possible, without summarizing them.
- Make sure to include any generated json/yaml/html/etc, entity names, record IDs, or other important information in the Artifacts section that allows for retrieval or reproduction of results.
- Always include any generated data (json, html, yaml, etc) in the Artifacts section.
- Include only the information within the <skill> tags as specified above.

Begin your compression of the conversation history now.
Here is the message array you need to analyze and compress:";

		#endregion

		#region Properties: Public

		public string PromptTemplate =>
			SystemSettings.GetValue(_userConnection, "CreatioAIUnifiedSummarizationPrompt", SummarizerPromptTemplate);
		public int MinRequiredMessages =>
			SystemSettings.GetValue(_userConnection, "CreatioAISummarizationMinRequiredMessages", 0);
		public int MinRequiredCharacters =>
			SystemSettings.GetValue(_userConnection, "CreatioAISummarizationMinRequiredCharacters", 5000);
		public int MinIntentsCountInSession =>
			SystemSettings.GetValue(_userConnection, "CreatioAISummarizationMinIntentsCountInSession", 3);

		#endregion

		#region Constructors: Public

		public UnifiedSummarizationSettings(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

	}

	#endregion

}

