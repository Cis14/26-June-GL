 namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;

	#region Class: DeleteOAuth20FeatureIntegrationInstallScript

	public class DeleteOAuth20FeatureIntegrationInstallScript : IInstallScriptExecutor
	{

		#region Methods: Private

		private static void DeleteOAuth20FeatureIntegrationFeature(UserConnection userConnection) {
			EntitySchemaManager entitySchemaManager = userConnection.EntitySchemaManager;
			Entity feature = entitySchemaManager.GetEntityByName("Feature", userConnection);
			var entityCondition = new Dictionary<string, object> {
				{ "Code", "OAuth20Integration" }
			};
			if (!feature.FetchFromDB(entityCondition)) {
				 return;
			};
			feature.Delete();
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Execute script for delete "OAuth20Integration" feature.
		/// </summary>
		/// <param name="userConnection">Instance of the <see cref="UserConnection"/> type.</param>
		public void Execute(UserConnection userConnection) {
			DeleteOAuth20FeatureIntegrationFeature(userConnection);
		}

		#endregion

	}

	#endregion

}

