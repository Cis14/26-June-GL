namespace Terrasoft.Configuration.Translate.Providers
{
	using System;
	using Newtonsoft.Json;
	using Creatio.Copilot;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;
	using System.Collections.Generic;
	using Terrasoft.Configuration.Translating;
	using Terrasoft.Configuration.Omnichannel.Messaging;

	#region Class: CreatioAiTranslateResponse

	/// <summary>
	/// Represents the response from the Creatio AI translation service.
	/// </summary>
	public class CreatioAiTranslateResponse
	{
	    /// <summary>
	    /// Gets or sets the locale of the translated text (e.g., "fr-FR" for French).
	    /// </summary>
	    public string Locale { get; set; } = string.Empty;
	
	    /// <summary>
	    /// Gets or sets the translated text content.
	    /// </summary>
	    public string TranslatedText { get; set; } = string.Empty;
	}

	#endregion


	#region Class: CreatioAiTranslateProvider

	/// <summary>
	/// Provides translation and language detection using Creatio Copilot engine.
	/// </summary>
	[DefaultBinding(typeof(ITranslateProvider), Name = "CreatioAiTranslateProvider")]
	public class CreatioAiTranslateProvider : BaseTranslateProvider
	{
		#region Fields: Private

		/// <summary>
		/// Name of the intent used for language detection.
		/// </summary>
		private readonly string _detectIntent = "LanguageDetector";

		/// <summary>
		/// Name of the intent used for text translation.
		/// </summary>
		private readonly string _translateIntent = "TextTranslation";

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CreatioAiTranslateProvider"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection object.</param>
		public CreatioAiTranslateProvider(UserConnection userConnection) : base(userConnection) { }

		#endregion

		#region Methods: Private

		/// <summary>
		/// Executes the specified Copilot intent call request and validates the result.
		/// </summary>
		/// <param name="request">The Copilot intent call request.</param>
		/// <returns>The result of the Copilot intent execution.</returns>
		/// <exception cref="InvalidOperationException">Thrown when the operation fails.</exception>
		private CopilotIntentCallResult ExecuteWithValidation(CopilotIntentCall request) {
			CopilotIntentCallResult result = UserConnection.CopilotEngine.ExecuteIntent(request);
			if (!result.IsSuccess) {
				throw new InvalidOperationException($"Translation operation failed: {result.ErrorMessage}");
			}
			return result;
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Detects the language of the specified text.
		/// </summary>
		/// <param name="text">The text to analyze.</param>
		/// <returns>The detected language result.</returns>
		protected override DetectedLanguageResult InnerDetectLanguage(string text) {
			var request = new CopilotIntentCall {
				IntentName = _detectIntent,
				Parameters = new Dictionary<string, object> {
					{ "Text", text }
				}
			};
			CopilotIntentCallResult result = ExecuteWithValidation(request);
			var data = JsonConvert.DeserializeObject<CreatioAiTranslateResponse>(result.Content);
			return new DetectedLanguageResult { LanguageCode = data.Locale, Confidence = 1.0f };
		}

		/// <summary>
		/// Translates the specified text from the source language to the target language.
		/// </summary>
		/// <param name="text">The text to translate.</param>
		/// <param name="targetLanguage">The language to translate the text into.</param>
		/// <param name="sourceLanguage">The original language of the text.</param>
		/// <returns>The translated text.</returns>
		protected override string InnerTranslateText(string text, string targetLanguage, string sourceLanguage) {
			var request = new CopilotIntentCall {
				IntentName = _translateIntent,
				Parameters = new Dictionary<string, object> {
					{ "Text", text },
					{ "Target", targetLanguage },
					{ "Source", sourceLanguage }
				}
			};
			CopilotIntentCallResult result = ExecuteWithValidation(request);
			var data = JsonConvert.DeserializeObject<CreatioAiTranslateResponse>(result.Content);
			return data.TranslatedText;
		}

		#endregion
	}

	#endregion
}

