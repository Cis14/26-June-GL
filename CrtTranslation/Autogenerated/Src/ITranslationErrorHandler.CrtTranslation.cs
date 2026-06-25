using System.Collections.Generic;

namespace Terrasoft.Configuration.Translation
{

	#region Interface: ITranslationErrorHandler

	public interface ITranslationErrorHandler
	{

		#region Methods: Public

		/// <summary>
		/// Persists errors occured during applying translations.
		/// </summary>
		/// <param name="erronousRecords">Error messages collected for corresponding translation keys.</param>
		void SaveErrors(Dictionary<string, string> erronousRecords);

		/// <summary>
		/// Removes all error messages from the Translation table.
		/// </summary>
		void ResetErrors();

		#endregion

	}

	#endregion

} 
