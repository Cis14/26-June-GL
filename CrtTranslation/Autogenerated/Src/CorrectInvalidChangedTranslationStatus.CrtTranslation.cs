namespace Terrasoft.Core.Process.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;
	using Terrasoft.Configuration.Translation;
	using global::Common.Logging;
	using Terrasoft.Core.DB;
	using Terrasoft.Common;

	#region Class: CorrectInvalidChangedTranslationStatus

	/// <exclude/>
	public partial class CorrectInvalidChangedTranslationStatus
	{

		#region Fields: Private

		private readonly ILog _log = LogManager.GetLogger("Translation");
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

		private EntitySchema _sysTranslationSchema;
		private EntitySchema SysTranslationSchema {
			get {
				_sysTranslationSchema = _sysTranslationSchema ?? UserConnection.EntitySchemaManager
					.GetInstanceByName("SysTranslation");
				return _sysTranslationSchema;
			}
		}

		#endregion

		#region Methods: Private

		private List<ISysCultureInfo> GetCultureInfoList() {
			if (!UseSpecifiedLanguageOnly || LanguageId.Equals(Guid.Empty)) {
				return SysCultureInfoProvider.Read();
			}
			ISysCultureInfo cultureInfo = SysCultureInfoProvider.GetByLanguageId(LanguageId);
			return new List<ISysCultureInfo> { cultureInfo };
		}

		private void SetCorrectIsChangedStatus(string key, string isChangedColumnName, bool isChanged) {
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
			var update = new Update(UserConnection, SysTranslationSchema.Name)
					.Set(isChangedColumnName, Column.Parameter(isChanged))
				.Where("Key").IsEqual(Column.Parameter(key))
				.And(isChangedColumnName).IsEqual(Column.Parameter(!isChanged)) as Update;
			var result = update.Execute();
			if (result == 0) {
				_log.DebugFormat("Key {0} not found in SysTranslation or already has value {1} for column {2}",
					key, isChanged, isChangedColumnName);
				return;
			}
			_log.DebugFormat("For Key {0} {1} has been set to {2}", key, isChangedColumnName, isChanged);
		}

		private HashSet<string> GetInvalidTranslations(Guid systemCultureInfoId) {
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
			_log.DebugFormat("Getting invalid translations for culture with Id {0}", systemCultureInfoId);
			HashSet<string> result = new HashSet<string>();
			var select = new Select(UserConnection)
					.Column("TranslationKey")
				.From("TranslationStatus")
				.Where("SysCultureId").IsEqual(Column.Parameter(systemCultureInfoId)) as Select;
			select.ExecuteReader(dataReader => {
				string translationKey = dataReader.GetColumnValue<string>("TranslationKey");
				result.Add(translationKey);
				_log.DebugFormat("Invalid translation found for culture {0}: {1}",
					systemCultureInfoId, translationKey);
			});
			_log.DebugFormat("Total invalid translations found for culture {0}: {1}",
				systemCultureInfoId, result.Count);
			return result;
		}

		private HashSet<string> GetChangedTranslations(string isChangedColumnName) {
			HashSet<string> result = new HashSet<string>();
			var select = new Select(UserConnection)
					.Column("Key")
				.From(SysTranslationSchema.Name)
				.Where(isChangedColumnName).IsEqual(Column.Parameter(true)) as Select;
			select.ExecuteReader(dataReader => {
				string translationKey = dataReader.GetColumnValue<string>("Key");
				result.Add(translationKey);
				_log.DebugFormat("Changed translation found: {0}", translationKey);
			});
			return result;
		}

		private ParallelOptions GetParallelOptions(CancellationToken cancellationToken) {
			var maxDegreeOfParallelism = TranslationParallelOptions.GetMaxDegreeOfParallelism(
				UserConnection, "TranslationUpdateTaskConcurrencyLimit");
			_log.InfoFormat("Correct invalid changed translations in parallel with {0} threads",
				maxDegreeOfParallelism);
			return new ParallelOptions {
				MaxDegreeOfParallelism = maxDegreeOfParallelism,
				CancellationToken = cancellationToken
			};
		}

		private void SetIsChangedForInvalidTranslations(HashSet<string> invalidTranslations,
				ISysCultureInfo sysCultureInfo, CancellationToken ct) {
			var options = GetParallelOptions(ct);
			Parallel.ForEach(invalidTranslations, options, invalidTranslation => {
				SetCorrectIsChangedStatus(invalidTranslation, sysCultureInfo.IsChangedColumnName, true);
			});
		}

		private void RemoveIsChangedForInvalidTranslations(HashSet<string> invalidTranslations,
				ISysCultureInfo sysCultureInfo, CancellationToken ct) {
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
			_log.DebugFormat("Removing IsChanged status for valid translations for culture {0}", sysCultureInfo.Name);
			var changedTranslations = GetChangedTranslations(sysCultureInfo.IsChangedColumnName);
			var options = GetParallelOptions(ct);
			Parallel.ForEach(changedTranslations, options, changedTranslation => {
				if (!invalidTranslations.Contains(changedTranslation)) {
					SetCorrectIsChangedStatus(changedTranslation, sysCultureInfo.IsChangedColumnName, false);
				}
			});
			GC.Collect(2, GCCollectionMode.Forced);
		}

		private void CorrectIsChangedTranslationStatusForCulture(ISysCultureInfo sysCultureInfo,
				CancellationToken ct) {
			try {
				var invalidTranslations = GetInvalidTranslations(sysCultureInfo.Id);
				RemoveIsChangedForInvalidTranslations(invalidTranslations, sysCultureInfo, ct);
				SetIsChangedForInvalidTranslations(invalidTranslations, sysCultureInfo, ct);
			} catch (OperationCanceledException) {
				_log.Info("Operation was canceled by user");
			} catch (Exception e) {
				const string errorMessage = "Error while correcting IsChanged status for culture {0}: {1}";
				_log.ErrorFormat(errorMessage, sysCultureInfo.Name, e);
				throw;
			} finally {
				GC.Collect(2, GCCollectionMode.Forced);
			}
		}

		private void CorrectIsChangedTranslationStatus(CancellationToken ct) {
			List<ISysCultureInfo> cultureInfoList = GetCultureInfoList();
			foreach (var sysCultureInfo in cultureInfoList) {
				if (ct.IsCancellationRequested) {
					return;
				}
				CorrectIsChangedTranslationStatusForCulture(sysCultureInfo, ct);
			}
		}

		#endregion

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {
			ApplyTranslationProcessExtension.UpdateApplyProcessStage(context, ApplySessionId,
				ApplyTranslationsStagesEnum.CorrectInvalidTranslations);
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
			CorrectIsChangedTranslationStatus(context.CancellationToken);
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