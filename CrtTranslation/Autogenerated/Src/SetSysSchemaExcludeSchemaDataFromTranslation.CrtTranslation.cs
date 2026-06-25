namespace Terrasoft.Configuration
{
	using System;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using CoreSysSettings = Terrasoft.Core.Configuration.SysSettings;
	public class SetSysSchemaExcludeSchemaDataFromTranslation : IInstallScriptExecutor {

		#region Methods: Private

		private bool IsNeedSetSysSchemaExcludeSchemaDataFromTranslation(UserConnection userConnection) {
			var select =
				new Select(userConnection)
					.Column(Func.Count(Column.Asterisk()))
				.From("SysTranslation");
			return select.ExecuteScalar<int>() == 0;
		}

		private string GetExcludeSchemaDataFromTranslationSetting(UserConnection userConnection) {
			return CoreSysSettings.GetValue(userConnection, "ExcludeSchemaDataFromTranslation", string.Empty);
		}

		private bool IsExcludeSchemaDataFromTranslationContainsSysSchema(string excludeSchemaDataFromTranslation) {
			var excludedSchemas = excludeSchemaDataFromTranslation
				.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(s => s.Trim())
				.ToArray();
			return excludedSchemas.Contains("SysSchema");
		}

		#endregion

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			if (!IsNeedSetSysSchemaExcludeSchemaDataFromTranslation(userConnection)) {
				return;
			}
			string excludeSchemaDataFromTranslation = GetExcludeSchemaDataFromTranslationSetting(userConnection);
			string value;
			if (string.IsNullOrEmpty(excludeSchemaDataFromTranslation)) {
				value = "SysSchema";
			} else if (!IsExcludeSchemaDataFromTranslationContainsSysSchema(excludeSchemaDataFromTranslation)) {
				value = $"{excludeSchemaDataFromTranslation},SysSchema";
			} else {
				return;
			}
			CoreSysSettings.SetDefValue(userConnection, "ExcludeSchemaDataFromTranslation", value);
		}
		
		#endregion

	}
}
