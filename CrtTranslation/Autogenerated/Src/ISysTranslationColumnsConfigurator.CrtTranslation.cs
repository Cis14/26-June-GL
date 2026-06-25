namespace Terrasoft.Configuration.Translation
{
	using System.Collections.Generic;

	#region Interface: ISysTranslationColumnsConfigurator

	/// <summary>
	/// SysTranlation columns configurator.
	/// </summary>
	public interface ISysTranslationColumnsConfigurator
	{

		#region Methods: Public

		/// <summary>
		/// Gets SysTranlation columns configure.
		/// </summary>
		/// <returns><see cref="TranslationColumnConfigure"/></returns>
		IEnumerable<TranslationColumnConfigure> GetTranslationColumnsConfigured();

		#endregion

	}

	#endregion

}
