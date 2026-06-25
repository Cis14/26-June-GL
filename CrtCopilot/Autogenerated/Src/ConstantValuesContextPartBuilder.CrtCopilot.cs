namespace Creatio.Copilot {
	using System.Linq;
	using Terrasoft.Common.Json;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using SystemSettings = Terrasoft.Core.Configuration.SysSettings;

	#region Class: ConstantValuesContextPartBuilder

	/// <summary>
	/// Implementation of <see cref="IContextPartBuilder"/> for <see cref="ConstantValuesContextPart"/> parts.
	/// </summary>
	[DefaultBinding(typeof(IContextPartBuilder), Name = "ConstantValuesContextPart")]
	internal class ConstantValuesContextPartBuilder : BaseContextPartBuilder {

		#region Constants: Private

		private const string ConstantValuesContextPromptTemplate = "## Get page data\n" +
			"When interacting with you, the user sees a page with data on it\n\n" +
			"### Data structure\n" +
			"* You receive some of the page data in a form of a set of constants\n" +
			"* Names of constants is very technical information, AVOID IT IN YOU RESPONSES\n\n" +
			"### Emphasize data over structure\n" +
			"* Prioritize using the values from constants\n" +
			"* Focus on providing relevant data based on the user's query, regardless of the specific entity type shown on the current page\n\n" +
			"### Responding\n" +
			"* In your responses, prioritize data. No one is interested in structure\n" +
			"* Prefer captions not technical names\n" +
			"* Avoid displaying large amounts of text without urgent necessity\n" +
			"* Limit lists of columns/fields to the most essential (e.g., less than 7) to maintain clarity\n" +
			"* Prefer **aggregated information** not static data\n" +
			"* Provide concise, aggregated insights from actual data\n\n" +
			"### Assist with data analysis\n" +
			"* Proactively offer data analysis and trends based on the information available on the page\n" +
			"* Highlight key metrics, patterns, or anomalies found in the data to deliver valuable insights\n" +
			"* Suggest actions or recommendations informed by the data analysis when applicable\n" +
			"* constanst is passsed as Key: Value pairs, where key is contant name, value is constant value\n" +
			"Current avaliable constants are:\n" +
			"{contextParts}";

		private const string ConstantValuesFromContextPromptShortTemplate = "### Additional constant values\n" +
			"* in addition to informaion on Creatio page and data retrived by RetrieveEntityData function you can use set of constants\n" +
			"* constanst is passsed as Key: Value pairs, where key is contant name, value is constant value\n" +
			"Current avaliable constants are:\n" +
			"{contextParts}";

		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="userConnection"><see cref="UserConnection"/> instance.</param>
		public ConstantValuesContextPartBuilder(UserConnection userConnection): base(userConnection) {
		}



		#endregion
		#region Properties: Private

		private string MessageContentLongTemplate => SystemSettings.GetValue(_userConnection,
			"CreatioAiConstantValuesContextPrompt", ConstantValuesContextPromptTemplate);

		private string MessageContentShortTemplate => SystemSettings.GetValue(_userConnection,
			"CreatioAiConstantValuesContextPromptShort", ConstantValuesFromContextPromptShortTemplate);

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IContextPartBuilder.BuildMessageContent(CopilotContext)"/>
		public override string BuildMessageContent(CopilotContext copilotContext) {
			var parts = copilotContext.Parts.OfType<ConstantValuesContextPart>();
			if (!parts.Any()) {
				return string.Empty;
			}
			string contextContent = Json.Serialize(parts);
			var tpl = copilotContext.Parts.Count > parts.Count()
				? MessageContentShortTemplate
				: MessageContentLongTemplate;
			return tpl.Replace("{contextParts}", contextContent);
		}

		#endregion

	}

	#endregion

}
