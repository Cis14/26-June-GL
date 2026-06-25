namespace Terrasoft.Core.Process.Configuration
{
	using Creatio.FeatureToggling;
	using System;
	using Terrasoft.Common;
	using Terrasoft.Configuration.Translation;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Feature;
	using Terrasoft.Core.Packages;
	using Terrasoft.Core.Process;

	#region Class: ApplyTranslationUserTask

	/// <exclude/>
	public partial class ApplyTranslationUserTask
	{

		#region Methods: Private

		private void ApplyTranslations() {
			var service = new TranslationService(UserConnection);
			service.InitializeFaultToleranceBehavior();
			const string sysSettingsCode = "PackageToStoreTranslations";
			if (Features.GetIsEnabled("DisplayPackageForLocalizationsInApplyTranslation")) {
				if (!LocalizationPackage.Equals(Guid.Empty)) {
					var packageId = LocalizationPackage;
					var sysSettings = new SysSettings(UserConnection);
					if (sysSettings.FetchFromDB("Code", sysSettingsCode)) {
						var packageUId = WorkspaceUtilities.GetPackageUIdById(packageId, UserConnection);
						SysSettings.SetDefValue(UserConnection, sysSettingsCode, packageUId);
					}
				}
			}
			if (Features.GetIsEnabled("UsePackageForLocalizationsInApplyTranslation")) {
				if (SysSettings.TryGetValue(UserConnection, sysSettingsCode, out object packageToStoreTranslations)) {
					var packageUId = packageToStoreTranslations as Guid? ?? Guid.Empty;
					if (packageUId.IsNotEmpty()) {
						var isPackageForeign = WorkspaceUtilities.GetIsPackageForeignByUId(UserConnection, packageUId);
						if (isPackageForeign) {
							var packageName = WorkspaceUtilities.GetPackageNameByUId(UserConnection, packageUId);
							string message = string.Format(new LocalizableString(UserConnection.ResourceStorage,
								"ApplyTranslationUserTask", "LocalizableStrings.TranslationsPackageIsNotWritable.Value"),
								packageName);
							throw new InvalidOperationException(message);
						}
					}
				}
			}
			if (!UseSpecifiedLanguageOnly || LanguageId.Equals(Guid.Empty)){
				service.ApplyTranslation(ApplySessionId);
				return;
			}
			ISysCultureInfo cultureInfo = ApplyTranslationProcessExtension.GetCultureInfo(UserConnection, LanguageId);
			service.ApplyTranslationForCulture(cultureInfo, IsForceUpdate, ApplySessionId);
			service.ResetFaultToleranceBehavior();
		}

		#endregion

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {
			ApplyTranslationProcessExtension.UpdateApplyProcessStage(context, ApplySessionId,
				ApplyTranslationsStagesEnum.ApplyTranslations);
			ApplyTranslations();
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