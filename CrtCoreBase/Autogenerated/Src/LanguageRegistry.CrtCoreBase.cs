namespace Terrasoft.Configuration.Translating
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using System.Linq;

	#region Class: LanguageInfo

	/// <summary>
	/// Represents a language entry with normalized (short) code, full (region) code,
	/// a system identifier, and an optional culture identifier.
	/// </summary>
	public sealed class LanguageInfo
	{
		/// <summary>System identifier for the language.</summary>
		public Guid Id { get; set; }

		/// <summary>Optional culture identifier from the host system (may be <c>null</c>).</summary>
		public Guid? CultureId { get; set; }

		/// <summary>Normalized ISO 639-1 language code (e.g., "en").</summary>
		public string ShortCode { get; set; }

		/// <summary>Full language code with region (e.g., "en-US").</summary>
		public string FullCode { get; set; }

		/// <summary>Creates an instance of <see cref="LanguageInfo"/>.</summary>
		/// <param name="id">System identifier.</param>
		/// <param name="shortCode">Normalized short code (e.g., "en").</param>
		/// <param name="fullCode">Full code with region (e.g., "en-US").</param>
		/// <param name="cultureId">Optional culture identifier.</param>
		public LanguageInfo(Guid id, string shortCode, string fullCode, Guid? cultureId = null) {
			Id = id;
			CultureId = cultureId;
			ShortCode = shortCode;
			FullCode = fullCode;
		}

		/// <summary>
		/// Creates an instance of <see cref="LanguageInfo"/> where short code is derived from <paramref name="fullCode"/>.
		/// If <paramref name="fullCode"/> is null or whitespace, creates an uninitialized instance (all properties remain default values).
		/// </summary>
		/// <param name="id">System identifier.</param>
		/// <param name="fullCode">Full language code to derive short code from.</param>
		/// <param name="cultureId">Optional culture identifier.</param>
		public LanguageInfo(Guid id, string fullCode, Guid? cultureId = null) {
			if (string.IsNullOrWhiteSpace(fullCode)) {
				return;
			}
			Id = id;
			CultureId = cultureId;
			FullCode = fullCode;
			ShortCode = LanguageRegistry.NormalizeLanguageCode(fullCode);
		}
	}

	#endregion

	#region Class: LanguageRegistry

	/// <summary>
	/// Thread-safe in-memory registry of languages. Supports multiple entries per short code
	/// (e.g., "en" → "en-US", "en-GB"). Provides lookups by Id, codes, and optional CultureId.
	/// Preferred entry policy: first with <see cref="LanguageInfo.CultureId"/> set; otherwise the first entry.
	/// 
	/// <para><strong>Warning:</strong> This registry may contain invalid <see cref="LanguageInfo"/> entries 
	/// created through the second constructor with null/whitespace fullCode. Such entries should be 
	/// filtered out during initialization.</para>
	/// </summary>
	public static class LanguageRegistry 
	{

		#region Fields: Private

		private static readonly object _sync = new object();

		private static ReadOnlyDictionary<Guid, LanguageInfo> _byId =
			new ReadOnlyDictionary<Guid, LanguageInfo>(new Dictionary<Guid, LanguageInfo>());

		private static ReadOnlyDictionary<string, List<LanguageInfo>> _byShort =
			new ReadOnlyDictionary<string, List<LanguageInfo>>(new Dictionary<string, List<LanguageInfo>>(StringComparer.OrdinalIgnoreCase));

		private static ReadOnlyDictionary<string, LanguageInfo> _byFull =
			new ReadOnlyDictionary<string, LanguageInfo>(new Dictionary<string, LanguageInfo>(StringComparer.OrdinalIgnoreCase));

		private static ReadOnlyDictionary<Guid, LanguageInfo> _byCultureId =
			new ReadOnlyDictionary<Guid, LanguageInfo>(new Dictionary<Guid, LanguageInfo>());

		#endregion

		#region Constructors: Static

		/// <summary>
		/// Static constructor intentionally left blank.
		/// Initialize via <see cref="SetLanguages"/> or <see cref="AddOrUpdate"/> methods.
		/// </summary>
		static LanguageRegistry() {
			// Intentionally blank.
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Rebuilds all internal indexes atomically from the provided sequence.
		/// Filters out invalid entries (with null/empty ShortCode or FullCode) before indexing.
		/// </summary>
		/// <param name="languages">Sequence of language entries to index.</param>
		private static void RebuildIndexes(IEnumerable<LanguageInfo> languages) {
			var list = languages
				.Where(l => l != null && !string.IsNullOrWhiteSpace(l.ShortCode) && !string.IsNullOrWhiteSpace(l.FullCode))
				.Select(l => new LanguageInfo(
					l.Id,
					NormalizeLanguageCode(l.ShortCode),
					EnsureFullCode(l.ShortCode, l.FullCode),
					l.CultureId))
				.ToList();

			var byId = new Dictionary<Guid, LanguageInfo>(list.Count);
			var byShort = new Dictionary<string, List<LanguageInfo>>(StringComparer.OrdinalIgnoreCase);
			var byFull = new Dictionary<string, LanguageInfo>(StringComparer.OrdinalIgnoreCase);
			var byCultureId = new Dictionary<Guid, LanguageInfo>();

			foreach (var lang in list) {
				byId[lang.Id] = lang;

				if (!byShort.TryGetValue(lang.ShortCode, out var bucket)) {
					bucket = new List<LanguageInfo>();
					byShort[lang.ShortCode] = bucket;
				}
				bucket.Add(lang);

				byFull[lang.FullCode] = lang;

				if (lang.CultureId.HasValue && lang.CultureId.Value != Guid.Empty) {
					byCultureId[lang.CultureId.Value] = lang;
				}
			}

			_byId = new ReadOnlyDictionary<Guid, LanguageInfo>(byId);
			_byShort = new ReadOnlyDictionary<string, List<LanguageInfo>>(byShort);
			_byFull = new ReadOnlyDictionary<string, LanguageInfo>(byFull);
			_byCultureId = new ReadOnlyDictionary<Guid, LanguageInfo>(byCultureId);
		}

		/// <summary>
		/// Ensures the full code is present; if not, attempts to derive it from the short code.
		/// </summary>
		/// <param name="shortCandidate">Short code candidate.</param>
		/// <param name="fullCandidate">Full code candidate.</param>
		/// <returns>Valid full code or the short code as fallback.</returns>
		private static string EnsureFullCode(string shortCandidate, string fullCandidate) {
			if (!string.IsNullOrWhiteSpace(fullCandidate) && fullCandidate.Contains('-')) {
				return fullCandidate;
			}
			var shortCode = NormalizeLanguageCode(shortCandidate);
			var denorm = DenormalizeLanguageCode(shortCode);
			return denorm.Contains('-') ? denorm : shortCode;
		}

		#endregion

		#region Methods: Public

		/// <summary>Gets a language by its system identifier.</summary>
		/// <param name="id">Language identifier.</param>
		/// <param name="info">Resolved language info, if any.</param>
		/// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
		public static bool TryGetById(Guid id, out LanguageInfo info) =>
			_byId.TryGetValue(id, out info);

		/// <summary>
		/// Gets all languages for a normalized short code (e.g., "en").
		/// </summary>
		/// <param name="shortCode">Short code to look up (case-insensitive).</param>
		/// <param name="infos">All matching entries (read-only) if found.</param>
		/// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
		public static bool TryGetByShort(string shortCode, out IReadOnlyList<LanguageInfo> infos) {
			shortCode = NormalizeLanguageCode(shortCode);
			if (_byShort.TryGetValue(shortCode, out var list) && list.Count > 0) {
				infos = list.AsReadOnly();
				return true;
			}
			infos = null;
			return false;
		}

		/// <summary>
		/// Gets a language by a code that may be short (e.g., "en") or full (e.g., "en-US").
		/// If a short code maps to multiple entries, returns the preferred one
		/// (first with <see cref="LanguageInfo.CultureId"/> set; else the first entry).
		/// </summary>
		/// <param name="code">Short or full code (case-insensitive).</param>
		/// <param name="info">Resolved language info, if any.</param>
		/// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
		public static bool TryGetByCode(string code, out LanguageInfo info) {
			info = null;
			if (string.IsNullOrWhiteSpace(code)) {
				return false;
			}
			// Exact full-code match
			if (_byFull.TryGetValue(code, out var exact)) {
				info = exact;
				return true;
			}
			// Short-code bucket with preferred selection
			return TryGetPreferredByShort(NormalizeLanguageCode(code), out info);
		}

		/// <summary>
		/// Gets the preferred language for a given short code.
		/// Preference rule: first entry with <see cref="LanguageInfo.CultureId"/> set; otherwise the first entry.
		/// </summary>
		/// <param name="shortCode">Short code to look up (will be normalized).</param>
		/// <param name="info">Preferred language info, if any.</param>
		/// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
		public static bool TryGetPreferredByShort(string shortCode, out LanguageInfo info) {
			info = null;
			shortCode = NormalizeLanguageCode(shortCode);
			if (!_byShort.TryGetValue(shortCode, out var list) || list.Count == 0) {
				return false;
			}
			var withCulture = list.FirstOrDefault(x => x.CultureId.HasValue && x.CultureId.Value != Guid.Empty);
			info = withCulture ?? list[0];
			return true;
		}

		/// <summary>
		/// Gets a language by its culture identifier.
		/// </summary>
		/// <param name="cultureId">Culture identifier.</param>
		/// <param name="info">Resolved language info, if any.</param>
		/// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
		public static bool TryGetByCultureId(Guid cultureId, out LanguageInfo info) =>
			_byCultureId.TryGetValue(cultureId, out info);

		/// <summary>
		/// Gets the normalized short code by language identifier.
		/// </summary>
		/// <param name="id">Language identifier.</param>
		/// <param name="shortCode">Normalized short code if found.</param>
		/// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
		public static bool TryGetShortById(Guid id, out string shortCode) {
			if (_byId.TryGetValue(id, out var info)) {
				shortCode = info.ShortCode;
				return true;
			}
			shortCode = null;
			return false;
		}

		/// <summary>
		/// Gets the normalized short code by culture identifier.
		/// </summary>
		/// <param name="cultureId">Culture identifier.</param>
		/// <param name="shortCode">Normalized short code if found.</param>
		/// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
		public static bool TryGetShortByCultureId(Guid cultureId, out string shortCode) {
			if (_byCultureId.TryGetValue(cultureId, out var info)) {
				shortCode = info.ShortCode;
				return true;
			}
			shortCode = null;
			return false;
		}

		/// <summary>
		/// Gets a language identifier by a code that may be short or full.
		/// For short code with multiple entries, returns the preferred one.
		/// </summary>
		/// <param name="code">Short or full code (case-insensitive).</param>
		/// <param name="id">Language identifier if found.</param>
		/// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
		public static bool TryGetIdByCode(string code, out Guid id) {
			if (TryGetByCode(code, out var info)) {
				id = info.Id;
				return true;
			}
			id = Guid.Empty;
			return false;
		}

		/// <summary>
		/// Gets all language identifiers by normalized short code.
		/// </summary>
		/// <param name="shortCode">Short code to look up (case-insensitive).</param>
		/// <param name="ids">All matching identifiers, if any.</param>
		/// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
		public static bool TryGetIdByShort(string shortCode, out IReadOnlyList<Guid> ids) {
			if (TryGetByShort(shortCode, out var infos)) {
				ids = infos.Select(i => i.Id).ToList().AsReadOnly();
				return true;
			}
			ids = null;
			return false;
		}

		/// <summary>
		/// Gets the preferred language identifier by short code.
		/// Preference: first entry with CultureId; otherwise first entry.
		/// </summary>
		/// <param name="shortCode">Short code to look up (case-insensitive, will be normalized).</param>
		/// <param name="id">Preferred language identifier if found.</param>
		/// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
		public static bool TryGetIdByShort(string shortCode, out Guid id) {
			if (TryGetPreferredByShort(shortCode, out var info)) {
				id = info.Id;
				return true;
			}
			id = Guid.Empty;
			return false;
		}

		/// <summary>
		/// Returns a snapshot of all registered languages.
		/// </summary>
		/// <returns>Read-only collection of all language entries.</returns>
		public static IReadOnlyCollection<LanguageInfo> GetAll() {
			return _byId.Values.ToList().AsReadOnly();
		}

		/// <summary>
		/// Replaces the entire registry with the provided set of languages.
		/// Invalid entries (null or with empty codes) are automatically filtered out.
		/// </summary>
		/// <param name="languages">Non-null sequence of language entries.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="languages"/> is null.</exception>
		public static void SetLanguages(IEnumerable<LanguageInfo> languages) {
			if (languages == null) {
				return;
			}
			lock (_sync) {
				RebuildIndexes(languages);
			}
		}

		/// <summary>
		/// Adds a new entry or updates an existing one (matched by <see cref="LanguageInfo.Id"/>).
		/// If the entry has invalid codes, it will be ignored.
		/// </summary>
		/// <param name="language">Language entry to add or update (null entries are ignored).</param>
		public static void AddOrUpdate(LanguageInfo language) {
			if (language == null || string.IsNullOrWhiteSpace(language.ShortCode) || string.IsNullOrWhiteSpace(language.FullCode)) {
				return;
			}
			lock (_sync) {
				var current = _byId.Values.ToDictionary(v => v.Id, v => v);
				current[language.Id] = new LanguageInfo(
					language.Id,
					NormalizeLanguageCode(language.ShortCode),
					EnsureFullCode(language.ShortCode, language.FullCode),
					language.CultureId
				);
				RebuildIndexes(current.Values);
			}
		}

		/// <summary>
		/// Normalizes a language code to its short (ISO 639-1) form (e.g., "en-US" → "en").
		/// </summary>
		/// <param name="code">A short or full code.</param>
		/// <returns>Lower-cased short code, or the input if null/whitespace.</returns>
		public static string NormalizeLanguageCode(string code) {
			if (string.IsNullOrWhiteSpace(code)) {
				return code;
			}
			var parts = code.Split('-');
			return parts[0].ToLowerInvariant();
		}

		/// <summary>
		/// Produces a full language code for a short code.
		/// If multiple registry entries exist, returns the preferred one (first with CultureId; else first).
		/// If no registry entry exists, attempts to use system culture information as fallback.
		/// </summary>
		/// <param name="shortCode">Short language code to denormalize.</param>
		/// <returns>Full code if resolved; otherwise a system culture fallback or the input short code.</returns>
		public static string DenormalizeLanguageCode(string shortCode) {
			shortCode = NormalizeLanguageCode(shortCode);
			if (string.IsNullOrWhiteSpace(shortCode)) {
				return shortCode;
			}
			if (_byShort.TryGetValue(shortCode, out var list) && list.Count > 0) {
				var withCulture = list.FirstOrDefault(x => x.CultureId.HasValue && x.CultureId.Value != Guid.Empty);
				return (withCulture ?? list[0]).FullCode;
			}
			try {
				var culture = CultureInfo.GetCultureInfo(shortCode);
				return culture.Name;
			} catch (CultureNotFoundException) {
				return shortCode;
			}
		}

		#endregion

	}

	#endregion

}
