 namespace Terrasoft.Configuration
{
	using Terrasoft.Core;
	using Terrasoft.Core.DB;

	public class UpdateOAuthClientAppGrantTypeInstallScriptExecutor : IInstallScriptExecutor
	{
		#region Methods: Private

		private void UpdateGrantTypeIdForAuthClientApp(UserConnection userConnection) {
			const string serverToServerDefaultGrantTypeId = "73f0cc6d-0634-4db3-96ab-0cb549958f91";
			var update = new Update(userConnection, "OAuthClientApp")
				.Set("GrantTypeId", Column.Parameter(serverToServerDefaultGrantTypeId))
				.Where("GrantTypeId").IsNull() as Update;
			update?.Execute();
		}

		#endregion

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			UpdateGrantTypeIdForAuthClientApp(userConnection);
		}

		#endregion
	}
}
