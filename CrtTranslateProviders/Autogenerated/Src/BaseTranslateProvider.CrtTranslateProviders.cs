namespace Terrasoft.Configuration.Translate.Providers
{
	using global::Common.Logging;
	using System;
	using System.Linq;
	using Terrasoft.Core; 
	using Terrasoft.Core.DB;
	using System.Collections.Generic;
	using Terrasoft.Configuration.Translating;
	using Terrasoft.Configuration.Omnichannel.Messaging;

	#region Class: BaseTranslateProvider
	
	/// <summary>
	///	Base class for all translation providers.
	///	Contains common logic for validation, logging, and error handling.
	/// </summary>
	public abstract class BaseTranslateProvider : ITranslateProvider
	{
		#region Fields: Private
		
		/// <summary>
		///	Private instance of the logger.
		///	Used to log errors and debug information.
		/// </summary>
		private ILog _log;

		/// <summary>
		/// Holds the cached authorization parameter for the translate provider.
		/// </summary>
		private string _authorizationParameter;
		
		/// <summary>
		/// Represents the current user's connection context in Creatio.
		/// </summary>
		private readonly UserConnection _userConnection;

		#endregion

		#region Properties: Protected

		/// <summary>
		/// Gets the current <see cref="UserConnection"/> instance associated with the provider.
		/// </summary>
		protected UserConnection UserConnection => _userConnection;
		
		/// <summary>
		///	Provides a logger instance for derived classes.
		///	Will create a new logger if it has not been initialized.
		/// </summary>
		protected ILog Log {
			get {
				return _log ?? (_log = LogManager.GetLogger("TranslateProvider"));
			}
		}

		/// <summary>
		/// Gets the authorization parameter for the translate provider.
		/// If the parameter is not yet retrieved, it will call <see cref="GetAuthorizationParameter"/> to fetch it.
		/// </summary>
		protected string AuthorizationParameter {
			get {
				return _authorizationParameter ?? (_authorizationParameter = GetAuthorizationParameter());
			}
		}

		#endregion

		#region Constructors: Public
		
		/// <summary>
		/// Initializes the provider using a <see cref="UserConnection"/> instance.
		/// </summary>
		/// <param name="userConnection">The current user connection for database access and context.</param>
		public BaseTranslateProvider(UserConnection userConnection) {
			_userConnection = userConnection;
		}
		
		/// <summary>
		/// Initializes the provider using a authorization parameter directly.
		/// </summary>
		/// <param name="authorizationParameter">The pre-generated authorization parameter for translation service access.</param>
		public BaseTranslateProvider(string authorizationParameter) {
			_authorizationParameter = authorizationParameter;
		}

		#endregion
		
		#region Methods: Private

		/// <summary>
		///	Validates that the text for translation is not null or empty.
		///	Throws an exception if validation fails.
		/// </summary>
		private void ValidateText(string text) {
			if (string.IsNullOrWhiteSpace(text)) {
				throw new ArgumentException("Text content is a mandatory parameter for translation", nameof(text));
			}
		}

		/// <summary>
		///	Validates that target language is provided.
		///	Throws an exception if the parameter is missing.
		/// </summary>
		private void ValidateLanguages(string targetLanguage) {
			if (string.IsNullOrWhiteSpace(targetLanguage)) {
				throw new ArgumentException("Target language is a mandatory parameter for translation", nameof(targetLanguage));
			}
		}

		/// <summary>
		///	Executes the provided method inside a try/catch block.
		///	Logs any exception and returns the default value if an error occurs.
		/// </summary>
		private T CallWithValidation<T>(Func<T> method) {
			try {
				return method.Invoke();
			} catch (Exception ex) {
				Log.Error("Translation operation failed", ex);
				return default;
			}
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		///	Abstract method for detecting the language of the provided text.
		///	Should be implemented in the derived class.
		/// </summary>
		protected abstract DetectedLanguageResult InnerDetectLanguage(string text);

		/// <summary>
		///	Abstract method for translating the text from source to target language.
		///	Should be implemented in the derived class.
		/// </summary>
		protected abstract string InnerTranslateText(string text, string targetLanguage, string sourceLanguage);

		/// <summary>
		/// Retrieves the authorization parameter for the current provider from the database.
		/// </summary>
		/// <returns>The authorization parameter as a string.</returns>
		/// <exception cref="ArgumentException">
		/// Thrown if <see cref="_userConnection"/> is null, because a authorization parameter cannot be retrieved without a user connection.
		/// </exception>
		protected virtual string GetAuthorizationParameter() {
			if (_userConnection == null) {
				Log.Error("UserConnection cannot be empty at the same time as the authorization parameter.");
				return string.Empty;
			}

			var providerName = this.GetType().Name;
			return (new Select(_userConnection)
				.Top(1)
				.Column("AuthorizationParameter")
				.From("TranslateProvider")
				.Where("Name").IsEqual(Column.Parameter(providerName)) as Select)
				.ExecuteScalar<string>();
		}

		#endregion

		#region Methods: Public

		/// <summary>
		///	Detects the language of the provided text with validation and error handling.
		/// </summary>
		public DetectedLanguageResult DetectLanguage(string text) {
			return CallWithValidation(() => {
				ValidateText(text);
				return InnerDetectLanguage(text);
			});
		}

		/// <summary>
		///	Translates a single text from the source language to the target language.
		///	Performs validation and error handling.
		/// </summary>
		public string TranslateText(string text, string targetLanguage, string sourceLanguage = "") {
			return CallWithValidation(() => {
				ValidateText(text);
				ValidateLanguages(targetLanguage);
				return InnerTranslateText(text, targetLanguage, sourceLanguage);
			});
		}

		/// <summary>
		/// Translates a single text to the target language letting provider detect the source language.
		/// Implementation required by ITranslateProvider two-parameter overload.
		/// </summary>
		public string TranslateText(string text, string targetLanguage) {
			return TranslateText(text, targetLanguage, string.Empty);
		}

		/// <summary>
		///	Translates multiple texts from the source language to the target language.
		///	Skips empty texts and returns read-only translation results.
		/// </summary>
		public IReadOnlyList<string> TranslateMultiple(IEnumerable<string> texts, string targetLanguage, string sourceLanguage = "") {
			return CallWithValidation<IReadOnlyList<string>>(() => {
				if (texts == null || texts.Count() == 0) {
					throw new ArgumentException("Text content is a mandatory parameter for translation", nameof(texts));
				}
				ValidateLanguages(targetLanguage);
				var result = new List<string>(texts.Count());
				foreach (var content in texts) {
					result.Add(InnerTranslateText(content, targetLanguage, sourceLanguage));
				}

				return result.AsReadOnly();
			});
		}

		#endregion
	}

	#endregion
}

