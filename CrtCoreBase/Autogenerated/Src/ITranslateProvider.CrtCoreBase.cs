namespace Terrasoft.Configuration.Translating
{
	using System.Collections.Generic;

	#region Model: DetectedLanguageResult

	/// <summary>
	/// Represents the result of a language detection operation.
	/// </summary>
	public class DetectedLanguageResult
	{
		/// <summary>
		/// The detected language code (e.g., "en", "uk").
		/// </summary>
		public string LanguageCode { get; set; } = string.Empty;

		/// <summary>
		/// Confidence score of the detection (0.0 - 1.0).
		/// </summary>
		public float Confidence { get; set; }
	}

	#endregion

	#region Interface: ITranslateProvider

	/// <summary>
	/// Interface for a translation provider service.
	/// </summary>
	public interface ITranslateProvider
	{
		/// <summary>
		/// Detects the language of a given text.
		/// </summary>
		/// <param name="text">Text to analyze.</param>
		/// <returns>DetectedLanguageResult containing the detected language and confidence.</returns>
		DetectedLanguageResult DetectLanguage(string text);

		/// <summary>
		/// Translates a single text from source language to target language.
		/// </summary>
		/// <param name="text">Text to translate.</param>
		/// <param name="targetLanguage">Target language code (e.g., "uk").</param>
		/// <param name="sourceLanguage">Source language code.</param>
		/// <returns>Translated text as a string.</returns>
		string TranslateText(string text, string targetLanguage, string sourceLanguage);

		/// <summary>
		/// Translates multiple texts at once, preserving the input order.
		/// </summary>
		/// <param name="texts">Collection of texts to translate.</param>
		/// <param name="targetLanguage">Target language code.</param>
		/// <param name="sourceLanguage">Source language code.</param>
		/// <returns>List of translated texts in the same order as input.</returns>
		IReadOnlyList<string> TranslateMultiple(IEnumerable<string> texts, string targetLanguage, string sourceLanguage);
	}

	#endregion
}

