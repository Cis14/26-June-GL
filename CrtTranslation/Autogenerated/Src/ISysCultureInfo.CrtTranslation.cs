namespace Terrasoft.Configuration.Translation
{
	using System;

	#region Interface: ISysCultureInfo

	public interface ISysCultureInfo
	{

		#region Properties: Public

		/// <summary>
		/// System culture identifier.
		/// </summary>
		Guid Id {
			get;
		}

		/// <summary>
		/// Language identifier.
		/// </summary>
		Guid LanguageId {
			get;
		}

		/// <summary>
		/// Language code.
		/// </summary>
		string Name {
			get;
		}

		/// <summary>
		/// System culture index.
		/// </summary>
		int Index {
			get;
		}

		/// <summary>
		/// Translation column name.
		/// </summary>
		string TranslationColumnName {
			get;
		}

		/// <summary>
		/// Verification column name.
		/// </summary>
		string VerificationColumnName {
			get;
		}

		string IsChangedColumnName {
			get;
		}
		
		bool IsActive {
			get;
		}

		#endregion

	}

	#endregion

}

