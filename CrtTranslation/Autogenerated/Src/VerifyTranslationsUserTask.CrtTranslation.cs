namespace Terrasoft.Core.Process.Configuration
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;
	using Terrasoft.Common;
	using Terrasoft.Configuration;
	using Terrasoft.Configuration.Translation;
	using Terrasoft.Core;
	using CoreSysSettings = Terrasoft.Core.Configuration.SysSettings;
	using Terrasoft.Core.ConfigurationBuild;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Translation;
	using global::Common.Logging;
	using Creatio.FeatureToggling;

	#region Class: VerifyTranslationsUserTask

	/// <exclude/>
	public partial class VerifyTranslationsUserTask
	{

		#region Constants: Private

		private const string ResourceTemplate = "Configuration:{0}:LocalizableStrings.{1}.Value";
		private const string ExcludedSchemasSettingName = "ExcludeSchemasFromVerification";
		
		#endregion

		#region Fields: Private

		private readonly IConfigurationRuntimeResourcesDirectoryProvider _runtimeResourcesDirectoryProvider =
			ClassFactory.Get<IConfigurationRuntimeResourcesDirectoryProvider>();
		
		private readonly IConfigurationRuntimeDirectoryProvider _configurationRuntimeDirectoryProvider =
			ClassFactory.Get<IConfigurationRuntimeDirectoryProvider>();

		private readonly ILog _log = LogManager.GetLogger("Translation");

		private ConcurrentBag<ILocalizableItem> _localizableItems = new ConcurrentBag<ILocalizableItem>();
		
		private List<string> _excludeLocalizableItemNames = new List<string>();

		private readonly ApplyTranslationActivityTracker _activityTracker = new ApplyTranslationActivityTracker();

		#endregion

		#region Properties: Private

		private ISysCultureInfoProvider _sysCultureInfoProvider;
		private ISysCultureInfoProvider SysCultureInfoProvider {
			get {
				_sysCultureInfoProvider = _sysCultureInfoProvider ?? ClassFactory.Get<SysCultureInfoProvider>(
					new ConstructorArgument("userConnection", UserConnection));
				return _sysCultureInfoProvider;
			}
		}

		private Guid? _defCultureId;
		private Guid? DefCultureId => _defCultureId ?? (_defCultureId =
			CoreSysSettings.GetDefValue(UserConnection, "PrimaryCulture") as Guid?);

		#endregion

		#region Methods: Private

		private Dictionary<string, string> GetTranslationResources(ISysCultureInfo culture) {
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
			var cultureName = culture.Name;
			string cultureColumnName = culture.TranslationColumnName;
			var result = new Dictionary<string, string>();
			LogExecutionTime(() => {
				var select = 
					new Select(UserConnection)
							.Column("Key")
							.Column(cultureColumnName)
							.From("SysTranslation")
							.Where(cultureColumnName).IsNotEqual(Column.Parameter(string.Empty))
						as Select;
				select.ExecuteReader(dataReader => {
					var key = dataReader.GetColumnValue<string>("Key");
					var value = dataReader.GetColumnValue<string>(cultureColumnName);
					result.Add(key, value);
				});	
			}, "Read translation resources for culture {0} in {1} ms", cultureName);
			return result;
		}

		private void SaveToConcurrentBag(ILocalizableItem item) {
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
			_localizableItems.Add(item);
		}

		private void ParseLocalizableStrings(string schemaName, string jsContent,
				ref Dictionary<string, string> result) {
			Match localizableStringsMatch = Regex.Match(jsContent, @"var\s+localizableStrings\s*=\s*\{([\s\S]*?)\};");
			if (!localizableStringsMatch.Success) {
				return;
			}
			string localizableStringsContent = localizableStringsMatch.Groups[1].Value;
			string pattern = @"\s*(?:""(?<key>[^""]+)""|(?<key>[\w]+))\s*:\s*""(?<value>[^""]+)""";
			MatchCollection matches = Regex.Matches(localizableStringsContent, pattern);
			foreach (Match match in matches) {
				string key = match.Groups["key"].Value;
				string value = match.Groups["value"].Value;
				var resourceKey = string.Format(ResourceTemplate, schemaName, key);
				result[resourceKey] = Regex.Unescape(value);
			}
		}

		private Dictionary<string,string> ReadAllFileContentByCulture(ISysCultureInfo culture, CancellationToken ct) {
			var result = new Dictionary<string, string>();
			if (!culture.IsActive) {
				return result;
			}
			var path = GetResourcesDirectory(culture.Name);
			if (!Directory.Exists(path)) {
				return result;
			}
			var files = Directory.GetFiles(path, "*.js", SearchOption.AllDirectories);
			foreach (string fileName in files) {
				ct.ThrowIfCancellationRequested();
				string content = System.IO.File.ReadAllText(fileName);
				var schemaName = GetSchemaNameFromFileName(fileName);
				try {
					result.Add(schemaName, content);
				}
				catch (Exception ex) {
					_log.ErrorFormat("Error while adding schema {0} to dictionary. Error: {1}", schemaName, ex);
					throw;
				}
			}
			return result;
		}

		private string GetResourcesDirectory(string culture) {
			return Path.Combine(_configurationRuntimeDirectoryProvider.GetConfigurationRuntimeDirectory(),
				_runtimeResourcesDirectoryProvider
					.GetConfigurationRuntimeResourcesDirectoryForSpecificCulture(culture));
		}

		private string GetSchemaNameFromFileName(string fileName) {
			var partToRemove = "Resources.js";
			var fileNameWithoutExtension = Path.GetFileName(fileName);
			string result = fileNameWithoutExtension.EndsWith(partToRemove)
				? fileNameWithoutExtension.Replace(partToRemove, string.Empty)
				: fileNameWithoutExtension;
			return result;
		}
		
		private void LogExecutionTime(Action action, string message, params object[] args) {
			var stopwatch = Stopwatch.StartNew();
			action();
			stopwatch.Stop();
			_log.InfoFormat(message, args.Concat(new object[] { stopwatch.ElapsedMilliseconds }).ToArray());
			stopwatch.Reset();
		}

		private T LogExecutionTime<T>(Func<T> func, string message, params object[] args) {
			var stopwatch = Stopwatch.StartNew();
			T result = func();
			stopwatch.Stop();
			_log.InfoFormat(message, args.Concat(new object[] { stopwatch.ElapsedMilliseconds }).ToArray());
			stopwatch.Reset();
			return result;
		}

		private void CleanTranslationStatus() {
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
			var delete = new Delete(UserConnection)
				.From("TranslationStatus");
			delete.Execute();
		}

		private void ReadLocalizableValues() {
			LogExecutionTime(() => {
				var resourceProvider = ClassFactory.Get<IResourceProvider>();
				resourceProvider.ReadLocalizableValues(DateTime.MinValue, SaveToConcurrentBag);
			}, "Read all localizable values in {0} ms");
			GC.Collect();
		}

		private Dictionary<string, string> GetActualResources(ISysCultureInfo culture) {
			var cultureName = culture.Name;
			var actualResources = new Dictionary<string, string>();
			LogExecutionTime(() => {
				foreach (var localizableItem in _localizableItems) {
					if (localizableItem.CultureId == culture.Id) {
						actualResources[localizableItem.Key] = localizableItem.Value;
					}
				}
			}, "Read actual resources for culture {0} in {1} ms", cultureName);
			return actualResources;
		}

		private string GetDefCultureValueByKey(string key) {
			return _localizableItems
				.Where(localizableItem => localizableItem.CultureId == DefCultureId && localizableItem.Key == key)
				.Select(c => c.Value).FirstOrDefault();
		}

		private Dictionary<string, string> GetStaticContentResources(ISysCultureInfo culture, CancellationToken ct) {
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
			var staticContentResources = new Dictionary<string, string>();
			if (IgnoreStaticContent) {
				return staticContentResources;
			}
			string cultureName = culture.Name;
			var fileContentForSchemas = LogExecutionTime(() => ReadAllFileContentByCulture(culture, ct), 
				"Read all files for culture {0} in {1} ms", cultureName);
			if (fileContentForSchemas.IsNullOrEmpty()) {
				return staticContentResources;
			}
			LogExecutionTime(() => {
				foreach (var schemaFileContent in fileContentForSchemas) {
					ct.ThrowIfCancellationRequested();
					ParseLocalizableStrings(schemaFileContent.Key, schemaFileContent.Value,
						ref staticContentResources);
				}
			}, "Read all localizable strings for culture {0} in {1} ms", cultureName);
			return staticContentResources;
		}

		private void SaveTranslationStatus(ISysCultureInfo culture, KeyValuePair<string, string> kvp, string expectedLocalizationValue,
				string actualLocalizationValue, string actualStaticContentValue) {
			try {
				var schema = UserConnection.EntitySchemaManager.GetInstanceByName("TranslationStatus");
				var translationStatus  = schema.CreateEntity(UserConnection);
				translationStatus.SetDefColumnValues();
				translationStatus.SetColumnValue("TranslationKey", kvp.Key);
				translationStatus.SetColumnValue("SysCultureId", culture.Id);
				translationStatus.SetColumnValue("ExpectedLocalizationValue", expectedLocalizationValue);
				translationStatus.SetColumnValue("ActualLocalizationValue", actualLocalizationValue);
				translationStatus.SetColumnValue("StaticContentValue", actualStaticContentValue);
				translationStatus.Save();
			} catch (Exception e) {
				_log.Debug($"Error occured when saving translation status for key '{kvp.Key}', culture {culture.Name}."
					+ " Retry saving attempt.", e);
				InsertTranslationStatus(kvp.Key, culture.Id, expectedLocalizationValue, actualLocalizationValue,
					actualStaticContentValue);
			}
		}

		private void InsertTranslationStatus(string key, Guid cultureId, string expectedLocalizationValue,
				string actualLocalizationValue, string actualStaticContentValue) {
			var insert = new Insert(UserConnection)
				.Into("TranslationStatus")
					.Set("TranslationKey", Column.Parameter(key))
					.Set("SysCultureId", Column.Parameter(cultureId))
					.Set("ExpectedLocalizationValue", Column.Parameter(expectedLocalizationValue))
					.Set("ActualLocalizationValue", Column.Parameter(actualLocalizationValue))
					.Set("StaticContentValue", Column.Parameter(actualStaticContentValue));
			insert.Execute();
		}

		private ParallelOptions GetParallelOptions(CancellationToken cancellationToken) {
			var maxDegreeOfParallelism = TranslationParallelOptions.GetMaxDegreeOfParallelism(
				UserConnection, "TranslationUpdateTaskConcurrencyLimit");
			_log.InfoFormat("Verify translations by culture in parallel with {0} threads", maxDegreeOfParallelism);
			return new ParallelOptions {
				MaxDegreeOfParallelism = maxDegreeOfParallelism,
				CancellationToken = cancellationToken
			};
		}

		private void VerifyByCulture(ISysCultureInfo culture, string defCultureName,
				CancellationToken ctCancellationToken) {
			string cultureName = culture.Name;
			Dictionary<string, string> translationResources = GetTranslationResources(culture);
			var actualResources = GetActualResources(culture);
			var staticContentResources = GetStaticContentResources(culture, ctCancellationToken);
			LogExecutionTime(() => {
				var options = GetParallelOptions(ctCancellationToken);
				Parallel.ForEach(translationResources, options, kvp => {
					_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
					bool hasProblem = false;
					string expectedLocalizationValue = kvp.Value;
					string actualLocalizationValue = string.Empty;
					string actualStaticContentValue = string.Empty;
					if (actualResources.TryGetValue(kvp.Key, out var value)) {
						actualLocalizationValue = value;
						if (Features.GetIsDisabled("ShowUntranslatedDataInValidationResults")) {
							if (value != kvp.Value) {
								_log.InfoFormat("Data:Translation for key {0} in culture {1} is not equal to static content. " +
												"Translation value: {2}, static content value: {3}", kvp.Key, cultureName, kvp.Value, value);
								hasProblem = true;
							}
						}
					}
					if (Features.GetIsEnabled("ShowUntranslatedDataInValidationResults")) {
						if (actualLocalizationValue != kvp.Value) {
							_log.InfoFormat("Data:Translation for key {0} in culture {1} is not equal to static content. " +
											"Translation value: {2}, static content value: {3}", kvp.Key, cultureName, kvp.Value, value);
							hasProblem = true;
						}
					}
					if (staticContentResources.TryGetValue(kvp.Key, out var staticContentValue)) {
						actualStaticContentValue = staticContentValue;
						if (staticContentValue != kvp.Value) {
							_log.InfoFormat("Static content for key {0} in culture {1} is not equal to translation. " +
							                "Static content value: {2}, translation value: {3}", kvp.Key, cultureName, staticContentValue, kvp.Value);
							hasProblem = true;	
						}
					}
					if (hasProblem && kvp.Key.StartsWith("Data:") && cultureName != defCultureName
							&& actualLocalizationValue.IsNullOrEmpty()
							&& GetDefCultureValueByKey(kvp.Key) == expectedLocalizationValue) {
						hasProblem = false;
						_log.DebugFormat("Translation for key {0} in culture {1} is the same as default culture and cannot be saved to Lcz table",
							kvp.Key, cultureName);
					}
					if (hasProblem && !_excludeLocalizableItemNames.Any(lczItem => kvp.Key.Contains(lczItem))) {
						SaveTranslationStatus(culture, kvp, expectedLocalizationValue, actualLocalizationValue, actualStaticContentValue);
					}
				});
			}, "Compare resources for culture {0} in {1} ms", cultureName);
			GC.Collect();
		}

		private void InitExcludedLocalizableItemNames() {
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
			const string captionKeyTemplate = "Configuration:{0}:Caption";
			const string lczStringsKeyTemplate = "Configuration:{0}:LocalizableStrings.";
			var schemaNames = CoreSysSettings.GetDefValue(UserConnection, ExcludedSchemasSettingName) as string;
			if (string.IsNullOrEmpty(schemaNames)) {
				return;
			}
			var schemaNamesArray = schemaNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var schemaName in schemaNamesArray) {
				_excludeLocalizableItemNames.Add(string.Format(captionKeyTemplate, schemaName.Trim()));
				_excludeLocalizableItemNames.Add(string.Format(lczStringsKeyTemplate, schemaName.Trim()));
			}
		}
		
		private List<ISysCultureInfo> GetCultureInfoList() {
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
			if (!UseSpecifiedLanguageOnly || LanguageId.Equals(Guid.Empty)){
				return SysCultureInfoProvider.Read();
			}
			var defCultureName = GeneralResourceStorage.DefCulture.Name;
			ISysCultureInfo cultureInfo = SysCultureInfoProvider.GetByLanguageId(LanguageId);
			var result = new List<ISysCultureInfo> { cultureInfo };
			if (!cultureInfo.Name.Equals(defCultureName)) {
				var defCultureInfo = SysCultureInfoProvider.Read(DefCultureId.Value);
				result.Add(defCultureInfo);
			}
			return result;
		}
		
		private void VerifyTranslationStatus(CancellationToken ctCancellationToken) {
			InitExcludedLocalizableItemNames();
			CleanTranslationStatus();
			var defCultureName = GeneralResourceStorage.DefCulture.Name;
			List<ISysCultureInfo> cultures = GetCultureInfoList();
			try {
				ReadLocalizableValues();
				foreach (ISysCultureInfo culture in cultures) {
					ctCancellationToken.ThrowIfCancellationRequested();
					VerifyByCulture(culture, defCultureName, ctCancellationToken);
				}
			} catch (OperationCanceledException) {
				_log.Info("Operation was canceled by user");
				throw;
			}
		}

		#endregion

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {
			ApplyTranslationProcessExtension.UpdateApplyProcessStage(context, ApplySessionId,
				ApplyTranslationsStagesEnum.VerifyTranslations);
			VerifyTranslationStatus(context.CancellationToken);
			return true;
		}

		#endregion

		#region Methods: Public

		public override bool CompleteExecuting(params object[] parameters) {
			return base.CompleteExecuting(parameters);
		}

		public override void CancelExecuting(params object[] parameters) {
			ApplyTranslationProcessExtension.CancelApplySession(UserConnection, ApplySessionId);
			base.CancelExecuting(parameters);
		}

		public override string GetExecutionData() {
			return string.Empty;
		}

		public override ProcessElementNotification GetNotificationData() {
			return base.GetNotificationData();
		}

		#endregion

	}

	#endregion

}