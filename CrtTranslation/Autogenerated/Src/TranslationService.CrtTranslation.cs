namespace Terrasoft.Configuration.Translation
{
	using System;
	using System.Collections.Generic;
	using System.ServiceModel.Activation;
	using System.Web.SessionState;
	using Creatio.FeatureToggling;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Factories;
	using Terrasoft.Web.Common;
	using Terrasoft.Core.DB;
	
	
	#region Class: TranslationService

	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class TranslationService : BaseService, ITranslationService, IReadOnlySessionState
	{

		#region Constants: Private

		private const string TranslationActualizedOnSysSettingCode = "TranslationActualizedOn";
		private const string TranslationActualizedErrorMessageName = "ActualizeTranslationError";
		private const string TranslationActualizedFinishMessageName = "ActualizeTranslationFinished";
		private const string CacheKey = "SkipGenerateStaticContentDuringApplyTranslation";

		#endregion
		
		#region Fields: Private

		private readonly UserConnection _userConnection;
		private readonly ApplyTranslationActivityTracker _activityTracker = new ApplyTranslationActivityTracker();
		private Guid _currentApplySessionId;
		private bool _isFaultTolerancyApplyTranslation = false;

		#endregion
		
		#region Constructors: Public
		
		public TranslationService() {
			_userConnection = base.UserConnection;
		}

		public TranslationService(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Properties: Private

		private ITranslationActualizersFactory _translationActualizersFactory;
		private ITranslationActualizersFactory TranslationActualizersFactory {
			get {
				if (_translationActualizersFactory == null) {
					_translationActualizersFactory = ClassFactory.Get<ITranslationActualizersFactory>(
						new ConstructorArgument("userConnection", _userConnection));
				}
				return _translationActualizersFactory;
			}
		}

		/// <summary>
		/// Translation actualizer.
		/// </summary>
		private ITranslationActualizer _translationActualizer;
		private ITranslationActualizer TranslationActualizer {
			get {
				return _translationActualizer ??
					(_translationActualizer = TranslationActualizersFactory.GetTranslationActualizer());
			}
		}

		private ITranslationCleaningService _translationCleaningService;
		private ITranslationCleaningService TranslationCleaningService =>
			_translationCleaningService ?? (_translationCleaningService = ClassFactory.Get<ITranslationCleaningService>(
				new ConstructorArgument("userConnection", _userConnection)));

		/// <summary>
		/// Translation actualization date.
		/// </summary>
		/// <remarks>
		/// Kind = DateTimeKind.Utc
		/// </remarks>
		private DateTime TranslationActualizedOn {
			get {
				DataValueType dataValueType =
					_userConnection.DataValueTypeManager.GetDataValueTypeByValueType(typeof(DateTime));
				DateTime dateTime = SysSettings.GetValue(_userConnection, TranslationActualizedOnSysSettingCode,
					DateTime.MinValue);
				DateTime saveDateTime = (DateTime)dataValueType.GetValueForSave(_userConnection, dateTime);
				return saveDateTime;
			}
		}

		private List<string> _translationApplyingLog;
		private List<string> TranslationApplyingLog {
			get {
				return _translationApplyingLog ?? (_translationApplyingLog = new List<string>());
			}
		}

		#endregion

		#region Methods: Private

		private void OnWriteLocalizableValueError(object sender, WriteLocalizableValueEventArgs e) {
			TranslationApplyingLog.Add(e.Result.Message);
		}

		private IEnumerable<TranslationColumnConfigure> GetSysTranslationColumnsConfigured() {
			var translationColumnsConfigurator = ClassFactory.Get<ISysTranslationColumnsConfigurator>(
				new ConstructorArgument("userConnection", _userConnection));
			return translationColumnsConfigurator.GetTranslationColumnsConfigured();
		}
		
		private void OnActivityTracked(object sender, EventArgs e) {
			_activityTracker.UpdateApplySessionActiveTime(_userConnection, _currentApplySessionId.ToString());
		}

		private TranslationServiceResponse ApplyTranslationInternal(Action actualizeAction, Guid applySessionId) {
			if (applySessionId != Guid.Empty) {
				_currentApplySessionId = applySessionId;
				TranslationActualizer.ActivityTracked += OnActivityTracked;
			}
			try {
				if (!_isFaultTolerancyApplyTranslation) {
					ClearUnusedReference();	
				}
				TranslationActualizer.WriteLocalizableValueError += OnWriteLocalizableValueError;
				actualizeAction();
			} catch (Exception e) {
				return TranslationServiceResponse.CreateFailureResult(e);
			} finally {
				TranslationActualizer.WriteLocalizableValueError -= OnWriteLocalizableValueError;
				if (applySessionId != Guid.Empty) {
					TranslationActualizer.ActivityTracked -= OnActivityTracked;
				}
			}
			return TranslationServiceResponse.CreateSuccessResult(TranslationApplyingLog);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Actualizes translation.
		/// </summary>
		public void ActualizeTranslation() {
			try {
				TranslationActualizer.ActualizeTranslation(TranslationActualizedOn);
				SysSettings.SetDefValue(_userConnection, TranslationActualizedOnSysSettingCode, DateTime.UtcNow);
				MsgChannelUtilities.PostMessage(_userConnection, TranslationActualizedFinishMessageName, string.Empty);
			} catch (Exception ex) {
				MsgChannelUtilities.PostMessage(_userConnection, TranslationActualizedErrorMessageName, ex.Message);
				throw;
			}
		}

		/// <summary>
		/// Applies translation.
		/// </summary>
		public TranslationServiceResponse ApplyTranslation(Guid applySessionId) {
			return ApplyTranslationInternal(() => TranslationActualizer.ActualizeLocalizableValues(), applySessionId);
		}

		public TranslationServiceResponse ApplyTranslation() {
			return ApplyTranslation(Guid.Empty);
		}

		public TranslationServiceResponse ApplyTranslationForCulture(ISysCultureInfo cultureInfo, bool isForceUpdate) {
			return ApplyTranslationForCulture(cultureInfo, isForceUpdate, Guid.Empty);
		}

		/// <summary>
		/// Applies translation for a specific culture.
		/// </summary>
		/// <param name="cultureInfo">The culture information.</param>
		/// <param name="isForceUpdate">If set to <c>true</c>, forces the update.</param>
		/// <returns>A response indicating the result of the translation application.</returns>
		public TranslationServiceResponse ApplyTranslationForCulture(ISysCultureInfo cultureInfo, bool isForceUpdate, Guid applySessionId) {
			return ApplyTranslationInternal(() => TranslationActualizer.ActualizeLocalizableValues(cultureInfo, isForceUpdate), applySessionId);
		}

		/// <summary>
		/// Checks if translations can be applied.
		/// </summary>
		public TranslationServiceResponse CheckTranslation() {
			return TranslationServiceResponse.CreateSuccessResult();
		}
		
		/// <summary>
		/// Remove all unused references.
		/// </summary>
		public void ClearUnusedReference() {
			if (Features.GetIsEnabled<UseStoredProcedureForCleaningUnusedResources>()) {
				var storedProcedure = new StoredProcedure(_userConnection, "tsp_RemoveUnusedReferences");
				storedProcedure.PackageName = "tspkg_Translation";
				storedProcedure.Execute();
			} else {
				TranslationCleaningService.ClearUnusedReferences();
			}
		}

		/// <summary>
		/// Initialize fault tolerance behavior for service.
		/// </summary>
		public void InitializeFaultToleranceBehavior() {
			_isFaultTolerancyApplyTranslation = true;
			_userConnection.ApplicationCache[CacheKey] = true;
		}

		/// <summary>
		/// Reset fault tolerance behavior for service.
		/// </summary>
		public void ResetFaultToleranceBehavior() {
			_isFaultTolerancyApplyTranslation = false;
			_userConnection.ApplicationCache[CacheKey] = false;
			
		}

		#endregion

	}

	#endregion

}
