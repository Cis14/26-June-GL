namespace Terrasoft.Configuration {
	using System;
	using System.Collections.Generic;
	using System.Data;
	using global::Common.Logging;
	using Newtonsoft.Json.Linq;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Encryption;

	#region Class: DesToAesCtiMigrationInstallScriptExecutor

	internal class DesToAesCtiMigrationInstallScriptExecutor : IInstallScriptExecutor {

		#region Consts: Private

		private const string ctiosSettingsPage = "SetupCiscoParametersPage";
		private const string avayaSettingsPage = "SetupAvayaParametersPage";
		private const string avayaPasswordField = "AgentPassword";
		private const string ctiosPasswordField = "Password";

		#endregion

		#region Fields: Private

		private readonly ILog _logger = LogManager.GetLogger("DesToAesCtiMigration");
		private UserConnection _uc;

		#endregion

		#region Methods: Private

		private IEnumerable<Guid> GetProviderIds(string setupPageSchemaName) {
			var select = new Select(_uc)
				.Column("Id")
				.From("SysMsgLib")
				.Where("SetupPageSchemaName").IsEqual(Column.Parameter(setupPageSchemaName)) as Select;
			var result = new List<Guid>();
			using (DBExecutor dbExecutor = _uc.EnsureDBConnection()) {
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						result.Add(dataReader.GetColumnValue<Guid>("Id"));
					}
				}
			}
			return result;
		}

		private Dictionary<Guid, string> GetUserSettings(IEnumerable<Guid> ctiproviderIds) {
			var select = new Select(_uc)
					.Column("Id")
					.Column("ConnectionParams")
					.From("SysMsgUserSettings")
					.Where("SysMsgLibId").In(Column.Parameters(ctiproviderIds)) as Select;
			var result = new Dictionary<Guid, string>();
			using (DBExecutor dbExecutor = _uc.EnsureDBConnection()) {
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						result.Add(dataReader.GetColumnValue<Guid>("Id"), dataReader.GetColumnValue<string>("ConnectionParams"));
					}
				}
			}
			return result;
		}

		private void SaveSetting(Guid settingsId, string settingJson) {
			var update = new Update(_uc, "SysMsgUserSettings")
				.Set("ConnectionParams", Column.Parameter(settingJson))
				.Where("Id").IsEqual(Column.Parameter(settingsId));
			update.Execute();
		}

		private void MigrateUserSetting(Guid settingsId, string settingsJson, string passwordFieldName, ICmsCryptoProvider encryptProvider) {
			if (settingsId.IsEmpty() || settingsJson.IsNullOrEmpty()) {
				return;
			}
			try {
				var data = Json.Deserialize(settingsJson) as JObject;
				var encryptedPassword = (string)data[passwordFieldName];
				var provider = CryptoProviderFactory.GetDecryptProvider(encryptedPassword);
				if (provider != encryptProvider && provider.TryDecrypt(encryptedPassword, out var decryptedPassword)) {
					data[passwordFieldName] = encryptProvider.Encrypt(decryptedPassword);
					SaveSetting(settingsId, Json.Serialize(data, true));
				}
			} catch (Exception ex) {
				_logger?.Error($"Error on SysMsgUserSettings {settingsId} migration.", ex);
			}
		}

		private void MigrateProviderSettings(string providerPageName, string passwordFieldName, ICmsCryptoProvider encryptProvider) {
			var providerIds = GetProviderIds(providerPageName);
			foreach (var kvp in GetUserSettings(providerIds)) {
				MigrateUserSetting(kvp.Key, kvp.Value, passwordFieldName, encryptProvider);
			}
		}

		#endregion

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			_uc = userConnection;
			var encryptProvider = CryptoProviderFactory.GetEncryptProvider();
			MigrateProviderSettings(avayaSettingsPage, avayaPasswordField, encryptProvider);
			MigrateProviderSettings(ctiosSettingsPage, ctiosPasswordField, encryptProvider);
		}

		#endregion

	}

	#endregion

}
