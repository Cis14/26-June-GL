namespace Terrasoft.Core.Process.Configuration
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Configuration.Translation;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;

	#region Class: CheckAnyConfigurationResourceIsChangedUserTask

	/// <exclude/>
	public partial class CheckAnyConfigurationResourceIsChangedUserTask
	{
		#region Fields: Private

		private readonly ApplyTranslationActivityTracker _activityTracker = new ApplyTranslationActivityTracker();

		#endregion

		#region Properties: Private

		private ISysCultureInfoProvider _sysCultureInfoProvider;
		private ISysCultureInfoProvider SysCultureInfoProvider {
			get {
				return _sysCultureInfoProvider ?? (_sysCultureInfoProvider = 
					ClassFactory.Get<SysCultureInfoProvider>(
						new ConstructorArgument("userConnection", UserConnection)));
			}
		}

		private ITranslationProvider _translationProvider;
		private ITranslationProvider TranslationProvider {
			get {
				if (_translationProvider != null) {
					return _translationProvider;
				}
				var factory = ClassFactory.Get<ITranslationActualizersFactory>(
					new ConstructorArgument("userConnection", UserConnection));
				_translationProvider = factory.GetTranslationProvider();
				return _translationProvider;
			}
		}

		#endregion

		#region Methods: Private
		
		private bool IsAnyConfigurationResourcesChanged() {
			var isApplyForAllLanguages = !UseSpecifiedLanguageOnly || LanguageId.Equals(Guid.Empty);
			var sysCulturesInfo = isApplyForAllLanguages
				? SysCultureInfoProvider.Read()
				: new List<ISysCultureInfo> { SysCultureInfoProvider.GetByLanguageId(LanguageId) };
			return TranslationProvider.GetIsAnyConfigurationResourceChanged(sysCulturesInfo);
		}

		#endregion

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {
			ApplyTranslationProcessExtension.UpdateApplyProcessStage(context, ApplySessionId,
				ApplyTranslationsStagesEnum.CheckAnyConfigurationResourcesChanged);
			HasChangedConfigurationResources = IsForceUpdate || IsAnyConfigurationResourcesChanged();
			_activityTracker.UpdateApplySessionActiveTime(UserConnection, ApplySessionId);
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