namespace Terrasoft.Configuration.Translating
{
	using global::Common.Logging;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using SystemSettings = Terrasoft.Core.Configuration.SysSettings;

	/// <summary>
	/// Factory class for creating and managing instances of ITranslateProvider.
	/// Retrieves provider name from system settings and resolves the provider
	/// using ClassFactory.
	/// </summary>
	public class TranslateProviderFactory
	{
		#region Fields: Private

		private readonly UserConnection _userConnection;

		private ILog _log;
		protected ILog Log {
			get
			{
				return _log ?? (_log = LogManager.GetLogger("TranslateProvider"));
			}
		}

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="TranslateProviderFactory"/> class.
		/// </summary>
		/// <param name="userConnection">The current user connection.</param>
		public TranslateProviderFactory(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Gets the translate provider name from system settings.
		/// </summary>
		/// <returns>Provider name as a string, or empty string if not set.</returns>
		private string GetProviderName() {
			return SystemSettings.GetValue(_userConnection, "TranslateProviderName", string.Empty);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Returns an instance of <see cref="ITranslateProvider"/>.
		/// Resolves the provider name through system settings, 
		/// creates the provider instance via <see cref="ClassFactory"/>,
		/// and returns it.
		/// </summary>
		/// <returns>An initialized <see cref="ITranslateProvider"/> instance.</returns>
		public ITranslateProvider Get() {
			var key = GetProviderName();
			try {
				return ClassFactory.Get<ITranslateProvider>(key, new ConstructorArgument("userConnection", _userConnection));
			} catch {
				if (string.IsNullOrEmpty(key)) {
					Log.Error("TranslateProviderFactory: No provider key specified in system settings. Unable to resolve ITranslateProvider.");
				} else {
					Log.Error($"TranslateProviderFactory: Unable to resolve ITranslateProvider for key '{key}'.");
				}
				return null;
			}
		}

		#endregion
	}
}


