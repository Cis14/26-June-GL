namespace Terrasoft.Configuration.Translation
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using Creatio.FeatureToggling;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Translation;

	#region Class: WriteLocalizableValueEventArgs

	public class WriteLocalizableValueEventArgs : EventArgs
	{

		public IActionResult Result;

	}

	#endregion

	#region Class: TranslationActualizer

	public class TranslationActualizer : ITranslationActualizer
	{

		#region Enum: ActualizationStatus

		private enum ActualizationStatus
		{
			InProgress,
			NotStarted
		}

		#endregion

		#region Constants: Private

		private const string TranslationActualizationStartMessageTemplate =
			"Translation actualization start = [{0} ({1})]";
		private const string TranslationActualizationEndMessage = "Translation actualization end";
		private const string TranslationActualizationSkipMessage = "Translation actualization skip";
		private const string LocalizableValuesActualizationStartMessageTemplate =
			"Localizable values actualization start = [{0} ({1})]";
		private const string LocalizableValuesActualizationEndMessage = "Localizable values actualization end";

		#endregion

		#region Fields: Private

		private static readonly object LockObject = new object();

		private readonly Lazy<int> _maxDegreeOfParallelism = new Lazy<int>(() =>
			TranslationParallelOptions.GetMaxDegreeOfParallelism(
				UserConnection.Current, "ApplyTranslationConcurrencyLimit"));

		#endregion

		#region Constructors

		public TranslationActualizer(ITranslationProvider translationProvider,
				ISysCultureInfoProvider sysCultureInfoProvider, ITranslationLogger translationLogger) {
			TranslationProvider = translationProvider;
			SysCultureInfoProvider = sysCultureInfoProvider;
			TranslationLogger = translationLogger;
			TranslationErrorHandler = translationProvider as ITranslationErrorHandler;
		}

		public TranslationActualizer(IResourceProvider resourceProvider, ITranslationProvider translationProvider,
				ISysCultureInfoProvider sysCultureInfoProvider, ITranslationLogger translationLogger)
				: this(translationProvider, sysCultureInfoProvider, translationLogger) {
			_resourceProvider = resourceProvider;
		}

		#endregion

		#region Properties: Private

		private static ActualizationStatus _currentActualizationStatus = ActualizationStatus.NotStarted;
		private static ActualizationStatus CurrentActualizationStatus {
			get {
				lock (LockObject) {
					return _currentActualizationStatus;
				}
			}
			set {
				lock (LockObject) {
					_currentActualizationStatus = value;
				}
			}
		}

		private readonly Dictionary<string, string> _erroneousRecords = new Dictionary<string, string>();

		#endregion

		#region Properties: Protected

		private IResourceProvider _resourceProvider;
		protected IResourceProvider ResourceProvider {
			get {
				return _resourceProvider ?? (_resourceProvider = ClassFactory.Get<IResourceProvider>());
			}
		}

		protected ITranslationProvider TranslationProvider {
			get;
			private set;
		}

		protected ISysCultureInfoProvider SysCultureInfoProvider {
			get;
			private set;
		}

		protected ITranslationLogger TranslationLogger {
			get;
			private set;
		}

		public ITranslationErrorHandler TranslationErrorHandler
		{
			get;
			private set;
		}

		#endregion

		#region Events: Public

		public event EventHandler<WriteLocalizableValueEventArgs> WriteLocalizableValueError;
		public event EventHandler ActivityTracked;

		#endregion

		#region Methods: Private

		/// <summary>
		/// Saves translation.
		/// </summary>
		private void WriteTranslation(ILocalizableItem item) {
			ISysCultureInfo sysCultureInfo = SysCultureInfoProvider.Read(item.CultureId);
			TranslationLogger.Debug($"Writing translation: key={item.Key}, culture={item.CultureId}");
			TranslationProvider.WriteTranslation(item.Key, sysCultureInfo.TranslationColumnName, item.Value,
				sysCultureInfo.IsChangedColumnName);
		}

		/// <summary>
		/// Writes localizable value.
		/// </summary>
		/// <param name="key">Translation key.</param>
		/// <param name="value">Value.</param>
		/// <param name="cultureIndex">Culture index.</param>
		/// <param name="modifiedOn">Modification date.</param>
		private void WriteLocalizableValue(string key, string value, int cultureIndex, DateTime modifiedOn) {
			WriteLocalizableValue(key, value, cultureIndex, modifiedOn, false);
		}

		private void WriteLocalizableValue(string key, string value, int cultureIndex, DateTime modifiedOn,
				bool useParallel) {
			ISysCultureInfo sysCultureInfo = SysCultureInfoProvider.Read(cultureIndex);
			ILocalizableItem item = new LocalizableItem(key, value, sysCultureInfo.Id, modifiedOn, false);
			IResourceProvider resourceProvider = useParallel
				? ClassFactory.Get<IResourceProvider>()
				: ResourceProvider;
			IActionResult result = null;
			try {
				result = resourceProvider.WriteLocalizableValue(item);
			} catch (Exception ex) {
				TranslationLogger.Info($"[WriteLocalizableValue] EXCEPTION for key '{key}': {ex.Message}. Value length: {value?.Length ?? 0}");
				TranslationLogger.Error(ex);
				throw;
			}
			string resultMessage = result.IsSuccess ? "Success" : result.Message;
			TranslationLogger.Debug($"[WriteLocalizableValue] Key: {key}, valueLen: {value?.Length ?? 0}, culture: {cultureIndex}, result: {resultMessage}");
			if (useParallel) {
				if (result.IsSuccess) {
					try {
						TranslationProvider.ResetChangedTranslationsStateForRecord(sysCultureInfo, key);
						TranslationLogger.Debug($"[ResetFlag] SUCCESS for key: {key}");
					} catch (Exception ex) {
						TranslationLogger.Info($"[ResetFlag] FAILED for key '{key}': {ex.Message}. Translation applied but flag NOT reset!");
						TranslationLogger.Error(ex);
						try {
							TranslationErrorHandler?.SaveErrors(new Dictionary<string, string> { 
								[key] = $"Flag reset failed: {ex.Message}" 
							});
						} catch (Exception handlerException) {
							TranslationLogger.Info($"[SaveErrors] ALSO FAILED for key '{key}': {handlerException.Message}");
							TranslationLogger.Error(handlerException);
						}
					}
				} else {
					try {
						TranslationErrorHandler?.SaveErrors(new Dictionary<string, string> { [key] = resultMessage });
						TranslationLogger.Debug($"[SaveError] SUCCESS for key: {key}, error: {resultMessage}");
					} catch (Exception ex) {
						TranslationLogger.Info($"[SaveError] FAILED for key '{key}': {ex.Message}. Error NOT saved in DB!");
						TranslationLogger.Error(ex);
					}
				}
			}
			CollectErroneousRecords(item, result);
			ProcessWriteLocalizableValueResult(result);
		}

		private void CollectErroneousRecords(ILocalizableItem item, IActionResult result) {
			if (result == null || result.IsSuccess || item == null || _erroneousRecords.ContainsKey(item.Key)) {
				return;
			}
			_erroneousRecords.Add(item.Key, result.Message);
		}

		private void SetIsChanged(Entity translation, int cultureIndex) {
			ISysCultureInfo sysCultureInfo = SysCultureInfoProvider.Read(cultureIndex);
			if (sysCultureInfo != null) {
				translation.SetColumnValue(sysCultureInfo.IsChangedColumnName, true);
			}
		}
		
		private void OnTranslationActualizationStart(DateTime actualizeFrom) {
			CurrentActualizationStatus = ActualizationStatus.InProgress;
			TranslationLogger.Info(string.Format(TranslationActualizationStartMessageTemplate, actualizeFrom,
				actualizeFrom.Kind));
		}

		private void OnTranslationActualizationEnd() {
			CurrentActualizationStatus = ActualizationStatus.NotStarted;
			TranslationLogger.Info(TranslationActualizationEndMessage);
		}

		private void OnLocalizableValuesActualizationStart() {
			TranslationLogger.Info(
				string.Format(LocalizableValuesActualizationStartMessageTemplate, DateTime.UtcNow,
				DateTime.UtcNow.Kind));
			_erroneousRecords.Clear();
		}

		private void OnLocalizableValuesActualizationEnd() {
			TranslationLogger.Info(LocalizableValuesActualizationEndMessage);
			_erroneousRecords.Clear();
		}

		private void ActualizeLocalizableValue(Entity entity, ISysCultureInfo sysCultureInfo) {
			var key = entity.GetTypedColumnValue<string>(TranslationConst.TranslationKeyColumnName);
			var value = entity.GetTypedColumnValue<string>(sysCultureInfo.TranslationColumnName);
			var modifiedOn = entity.GetTypedColumnValue<DateTime>(TranslationConst.TranslationModifiedOnColumnName);
			WriteLocalizableValue(key, value, sysCultureInfo.Index, modifiedOn, true);
		}

		private void ActualizeLocalizableValues(EntityCollection entities, ISysCultureInfo sysCulture) {
			var maxDegreeOfParallelism = _maxDegreeOfParallelism.Value;
			TranslationLogger.Info($"Running translations apply in parallel with {maxDegreeOfParallelism} threads for {entities.Count} records");
			var options = new ParallelOptions {
				MaxDegreeOfParallelism = maxDegreeOfParallelism
			};
			int successCount = 0;
			int errorCount = 0;
			object lockObj = new object();
			Parallel.ForEach(entities, options, entity => {
				var key = entity.GetTypedColumnValue<string>(TranslationConst.TranslationKeyColumnName);
				try {
					ActualizeLocalizableValue(entity, sysCulture);
					lock (lockObj) { successCount++; }
				} catch (Exception ex) {
					lock (lockObj) { errorCount++; }
					TranslationLogger.Info($"[Parallel] ERROR processing key '{key}': {ex.Message}");
					TranslationLogger.Error(ex);
					try {
						TranslationErrorHandler?.SaveErrors(new Dictionary<string, string> { 
							[key] = $"Unhandled exception: {ex.Message}" 
						});
					} catch (Exception handlerException) {
    					TranslationLogger.Info($"[Parallel] Failed to save error for key '{key}': {handlerException.Message}");
						TranslationLogger.Error(handlerException);
					}
				}
				OnActivityTracked();
			});
			TranslationLogger.Info($"[Parallel] Completed: {successCount} success, {errorCount} errors out of {entities.Count} records");
			if (errorCount > 0) {
				TranslationLogger.Info($"[Parallel] {errorCount} records failed. Check logs for details. These records may be reprocessed in next batch.");
			}
		}

		private void ActualizeLocalizableValues(ISysCultureInfo sysCulture) {
			EntityCollection records = TranslationProvider.GetChangedTranslationsByCulture(sysCulture);
			while (records.Count != 0) {
				ActualizeLocalizableValues(records, sysCulture);
				OnActivityTracked();
				records = TranslationProvider.GetChangedTranslationsByCulture(sysCulture);
			}
		}

		private void ActualizeLocalizableValuesParallel(List<ISysCultureInfo> sysCulturesInfo) {
			foreach (ISysCultureInfo sysCultureInfo in sysCulturesInfo) {
				ActualizeLocalizableValues(sysCultureInfo);
				OnActivityTracked();
			}
		}

		private void ActualizeLocalizableValues(List<ISysCultureInfo> sysCulturesInfo) {
			TranslationProvider.ReadChangedTranslations(sysCulturesInfo, WriteLocalizableValue);
			TranslationErrorHandler?.SaveErrors(_erroneousRecords);
			TranslationProvider.ResetChangedTranslationsState(sysCulturesInfo);
			OnActivityTracked();
		}
		
		private void ActualizeLocalizableValuesInternal(List<ISysCultureInfo> sysCulturesInfo) {
			OnLocalizableValuesActualizationStart();
			TranslationErrorHandler?.ResetErrors();
			if (Features.GetIsEnabled<EnableParallelApplyTranslations>()) {
				ActualizeLocalizableValuesParallel(sysCulturesInfo);
			} else {
				ActualizeLocalizableValues(sysCulturesInfo);
			}
			ResourceProvider.Actualize();
			OnLocalizableValuesActualizationEnd();
		}

		#endregion

		#region Methods: Protected

		protected void ProcessWriteLocalizableValueResult(IActionResult result) {
			if (!result.IsSuccess) {
				var e = new WriteLocalizableValueEventArgs {
					Result = result
				};
				OnWriteLocalizableValueError(e);
				TranslationLogger.Error(result);
			}
		}

		protected virtual void InternalActualizeTranslation(DateTime readFrom) {
			ResourceProvider.ReadLocalizableValues(readFrom, WriteTranslation);
		}

		protected virtual void OnWriteLocalizableValueError(WriteLocalizableValueEventArgs e) {
			if (WriteLocalizableValueError == null) {
				return;
			}
			WriteLocalizableValueError(this, e);
		}

		protected void OnActivityTracked() {
			if (ActivityTracked == null) {
				return;
			}
			ActivityTracked?.Invoke(this, EventArgs.Empty);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Actualizes translation.
		/// </summary>
		/// <param name="actualizeFrom">The date to start actualization from.</param>
		public void ActualizeTranslation(DateTime actualizeFrom) {
			if (CurrentActualizationStatus == ActualizationStatus.InProgress) {
				while (CurrentActualizationStatus == ActualizationStatus.InProgress) {
					Thread.Sleep(500);
				}
				TranslationLogger.Info(TranslationActualizationSkipMessage);
			} else {
				OnTranslationActualizationStart(actualizeFrom);
				try {
					TranslationProvider.Transaction(() => {
						InternalActualizeTranslation(actualizeFrom);
					});
				} finally {
					OnTranslationActualizationEnd();
				}
			}
		}

		/// <summary>
		/// Actualizes localizable values.
		/// </summary>
		public void ActualizeLocalizableValues() {
			List<ISysCultureInfo> sysCulturesInfo = SysCultureInfoProvider.Read();
			ActualizeLocalizableValuesInternal(sysCulturesInfo);
		}
		
		/// <inheritdoc/>
		public void ActualizeLocalizableValues(ISysCultureInfo cultureInfo, bool isForceUpdate) {
			var sysCulturesInfo = new List<ISysCultureInfo> { cultureInfo };
			if (isForceUpdate) {
				TranslationProvider.SetForceUpdate(cultureInfo);				
			}
			ActualizeLocalizableValuesInternal(sysCulturesInfo);
		}

		/// <summary>
		/// Sets translation state.
		/// </summary>
		/// <param name="translation">Translation.</param>
		public void SetIsTranslationChanged(Entity translation) {
			TranslationProvider.ReadChangedTranslationColumnsValues(translation,
					(key, value, cultureIndex, modifiedOn) => {
				SetIsChanged(translation, cultureIndex);
			});
		}

		#endregion

	}

	#endregion

}
