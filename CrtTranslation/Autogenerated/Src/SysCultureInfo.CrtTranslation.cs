namespace Terrasoft.Configuration.Translation
{

	using System;
	using Terrasoft.Common;

	#region Class: SysCultureInfo

	public class SysCultureInfo : ISysCultureInfo
	{

		#region Constants: Private

		private const string TranslationColumnPrefix = "Language";
		private const string VerificationColumnPrefix = "IsVerified";
		private const string IsChangedColumnPrefix = "IsChanged";
		private const int MaxCultureIndex = 35;

		#endregion

		#region Constructors: Public

		public SysCultureInfo(Guid id, int index) : this(id, index, Guid.Empty) { }

		public SysCultureInfo(Guid id, int index, Guid languageId) : this(id, index, languageId, string.Empty) {
		}

		public SysCultureInfo(Guid id, int index, Guid languageId, string name) : this(id, index, languageId, name,
			false) {
		}
		
		public SysCultureInfo(Guid id, int index, Guid languageId, string name, bool isActive) {
			if (index < 0 || index > MaxCultureIndex) {
				throw new ArgumentOutOfRangeException(
					new LocalizableString("Terrasoft.Common", "Exception.IndexOutOfRangeException"));
			}
			_id = id;
			_index = index;
			_languageId = languageId;
			_name = name;
			_isActive = isActive;
		}
		#endregion

		#region Properties: Public

		/// <summary>
		/// System culture identifier.
		/// </summary>
		private Guid _id;
		public Guid Id {
			get {
				return _id;
			}
		}

		/// <summary>
		/// Language identifier.
		/// </summary>
		private Guid _languageId;
		public Guid LanguageId {
			get {
				return _languageId;
			}
		}
		
		private string _name;
		public string Name {
			get {
				return _name;
			}
		}

		/// <summary>
		/// System culture index.
		/// </summary>
		private int _index;
		public int Index {
			get {
				return _index;
			}
		}
		
		/// <summary>
		/// System culture is active.
		/// </summary>
		private bool _isActive;
		public bool IsActive {
			get {
				return _isActive;
			}
		}

		/// <summary>
		/// Translation column name.
		/// </summary>
		private string _translationColumnName;
		public string TranslationColumnName {
			get {
				if (_translationColumnName.IsNullOrEmpty()) {
					_translationColumnName = string.Concat(TranslationColumnPrefix, Index);
				}
				return _translationColumnName;
			}
		}

		/// <summary>
		/// Verification column name.
		/// </summary>
		private string _verificationColumnName;
		public string VerificationColumnName {
			get {
				if (_verificationColumnName.IsNullOrEmpty()) {
					_verificationColumnName = string.Concat(VerificationColumnPrefix, Index);
				}
				return _verificationColumnName;
			}
		}

		private string _isChangedColumnName;
		public string IsChangedColumnName {
			get {
				if (_isChangedColumnName.IsNullOrEmpty()) {
					_isChangedColumnName = string.Concat(IsChangedColumnPrefix, Index);
				}
				return _isChangedColumnName;
			}
		}

		#endregion

	}

	#endregion

}

