 namespace Terrasoft.Configuration.Translating
{
	using global::Common.Logging;
	using System.ServiceModel;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Web;
	using Terrasoft.Core;
	using Terrasoft.Web.Common;

	#region Class: LanguageTranslationService

	/// <summary>
	/// Provides a service for translating text. 
	/// </summary>
	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class LanguageTranslationService : BaseService
	{

		#region Fields: Private

		private readonly TranslateProviderFactory _translateFactory;

		#endregion

		#region Properties: Private

		private ILog _log;
		protected ILog Log {
			get	{
				return _log ?? (_log = LogManager.GetLogger("TranslationService"));
			}
		}

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="OmniChatTranslationService"/> class.
		/// Resolves chat provider and translation provider factory using the current <see cref="UserConnection"/>.
		/// </summary>
		public LanguageTranslationService() {
			_translateFactory = new TranslateProviderFactory(UserConnection);
		}

		#endregion

		#region Methods: Private

		private string GetUserLanguageCode() {
			LanguageRegistry.TryGetShortByCultureId(UserConnection.CurrentUser.SysCultureId, out string shortCode);
			return shortCode;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Translates a single text string from the source language to the target language.
		/// </summary>
		/// <param name="text">The text to translate.</param>
		/// <param name="targetLanguage">
		/// Target language code. Accepts values like "en" or "en-US". The value is normalized to the short form ("en").
		/// </param>
		/// <param name="sourceLanguage">
		/// Source language code. Accepts values like "uk" or "uk-UA". The value is normalized to the short form ("uk").
		/// </param>
		/// <returns>The translated text.</returns>
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
		ResponseFormat = WebMessageFormat.Json)]
		public string TranslateText(string text, string targetLanguage, string sourceLanguage) {
			var provider = _translateFactory.Get();
			targetLanguage = string.IsNullOrEmpty(targetLanguage) ? GetUserLanguageCode() : LanguageRegistry.NormalizeLanguageCode(targetLanguage);
			sourceLanguage = string.IsNullOrEmpty(sourceLanguage) ? GetUserLanguageCode() : LanguageRegistry.NormalizeLanguageCode(sourceLanguage);
			var result = provider.TranslateText(text, targetLanguage, sourceLanguage);
			Log.Debug($"Translating text message = {text}");
			return result;
		}

		/// <summary>
		/// Translates a single text to the specified target language while letting the provider detect the source language automatically.
		/// </summary>
		/// <param name="text">Text to translate.</param>
		/// <param name="targetLanguage">Target language code (e.g. "en" or "en-US"). If empty, the current user's language is used.</param>
		/// <returns>Translated text.</returns>
		/// <remarks>
		/// Internally calls <see cref="ITranslateProvider.TranslateText(string, string)"/> overload which triggers provider-level source language detection.
		/// The provided target language is normalized to its short code form.
		/// </remarks>
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
ResponseFormat = WebMessageFormat.Json)]
		public string TranslateTextWithSourceLanguageDetecting(string text, string targetLanguage) {
			var provider = _translateFactory.Get();
			targetLanguage = string.IsNullOrEmpty(targetLanguage) ? GetUserLanguageCode() : LanguageRegistry.NormalizeLanguageCode(targetLanguage);
			var result = provider.TranslateText(text, targetLanguage, string.Empty);
			Log.Debug($"Translating text message = {text}");
			return result;
		}

		#endregion

	}

	#endregion

}

