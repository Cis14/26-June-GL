namespace Terrasoft.Configuration.Translation
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Core.Entities;

	#region Interface: ITranslationProvider

	public interface ITranslationProvider
	{

		#region Methods: Public

		/// <summary>
		/// Writes translation.
		/// </summary>
		/// <param name="key">Resource key.</param>
		/// <param name="translationColumnName">Translation column name.</param>
		/// <param name="translationColumnValue">Translation column value.</param>
		void WriteTranslation(string key, string translationColumnName, string translationColumnValue);

		void WriteTranslation(string key, string translationColumnName, string translationColumnValue,
			string isChangedColumnName);

		/// <summary>
		/// Selects changed translation columns values.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="readMethod">Translation column value processing method.</param>
		void ReadChangedTranslationColumnsValues(Entity entity, Action<string, string, int, DateTime> readMethod);

		/// <summary>
		/// Reads changed translations.
		/// <param name="sysCulturesInfo">Entity.</param>
		/// <param name="readMethod">Entity.</param>
		/// </summary>
		void ReadChangedTranslations(List<ISysCultureInfo> sysCulturesInfo,
			Action<string, string, int, DateTime> readMethod);

		/// <summary>
		/// Resets changed translations state.
		/// <param name="sysCulturesInfo">System cultures information.</param>
		/// </summary>
		void ResetChangedTranslationsState(IEnumerable<ISysCultureInfo> sysCulturesInfo);

		/// <summary>
		/// Resets the changed translations state for a specific record.
		/// </summary>
		/// <param name="sysCultureInfo">The system culture information.</param>
		/// <param name="key">The resource key.</param>
		void ResetChangedTranslationsStateForRecord(ISysCultureInfo sysCultureInfo, string key);

		/// <summary>
		/// Executes action in transaction.
		/// <param name="action">Action.</param>
		/// </summary>
		void Transaction(Action action);

		/// <summary>
		/// Retrieves a collection of changed translations for a specific culture.
		/// </summary>
		/// <param name="sysCultureInfo">The system culture information.</param>
		/// <returns>A collection of entities representing the changed translations.</returns>
		EntityCollection GetChangedTranslationsByCulture(ISysCultureInfo sysCultureInfo);

		/// <summary>
		/// Sets isChanged flag for the specified culture.
		/// </summary>
		void SetForceUpdate(ISysCultureInfo sysCultureInfo);

		/// <summary>
		/// Get is any configuration resource has changed flag in any of given cultures.
		/// </summary>
		bool GetIsAnyConfigurationResourceChanged(IEnumerable<ISysCultureInfo> sysCulturesInfo);

		#endregion

	}

	#endregion

}
