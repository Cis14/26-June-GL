namespace Terrasoft.Core.Process.Configuration
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Process;

	#region Class: ResetTranslationsBeforeActualization

	/// <exclude/>
	public partial class ResetTranslationsBeforeActualization
	{

		#region Methods: Private

		/// <summary>
		/// Cleanup all translations to ensure new and fresh data after full actualization.
		/// </summary>
		/// <param name="userConnection">User connection</param>
		private void RemoveAllTranslations(UserConnection userConnection) {
			var delete = new Delete(userConnection).From("SysTranslation");
			delete?.Execute();
		}

		/// <summary>
		/// Reset TranslationActualizedOn sys setting to minimum value to trigger full actualization.
		/// <param name="userConnection">User connection</param>
		/// </summary>
		private void ResetTranslationActualizedOnSysSetting(UserConnection userConnection) {
			const string translationActualizedOnSysSettingCode = "TranslationActualizedOn";
			SysSettings.SetDefValue(userConnection, translationActualizedOnSysSettingCode, DateTime.MinValue);
		}

		#endregion

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {
			RemoveAllTranslations(context.UserConnection);
			ResetTranslationActualizedOnSysSetting(context.UserConnection);
			return true;
		}

		#endregion

		#region Methods: Public

		public override bool CompleteExecuting(params object[] parameters) {
			return base.CompleteExecuting(parameters);
		}

		public override void CancelExecuting(params object[] parameters) {
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