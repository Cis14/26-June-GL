namespace Terrasoft.Configuration.Translation
{
	using System;
	using Terrasoft.Core.Entities;

	#region Interface: ITranslationActualizer

	public interface ITranslationActualizer
	{

		#region Events: Public

		event EventHandler<WriteLocalizableValueEventArgs> WriteLocalizableValueError;
		event EventHandler ActivityTracked;

		#endregion

		#region Methods: Public

		/// <summary>
		/// Actualizes translation.
		/// </summary>
		/// <param name="actualizeFrom">The date to start actualization from.</param>
		void ActualizeTranslation(DateTime actualizeFrom);

		/// <summary>
		/// Actualizes localizable values.
		/// </summary>
		void ActualizeLocalizableValues();
		
		/// <summary>
		/// Actualizes localizable values for a specific culture.
		/// </summary>
		/// <param name="cultureInfo">The culture information.</param>
		/// <param name="isForceUpdate">Indicates whether to force update.</param>
		void ActualizeLocalizableValues(ISysCultureInfo cultureInfo, bool isForceUpdate);

		/// <summary>
		/// Sets translation state.
		/// </summary>
		/// <param name="translation">Translation.</param>
		void SetIsTranslationChanged(Entity translation);

		#endregion

	}

	#endregion

}
