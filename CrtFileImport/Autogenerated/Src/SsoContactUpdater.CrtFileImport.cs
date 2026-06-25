namespace Terrasoft.Configuration {
	using global::Common.Logging;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Configuration.FileImport;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Sync;


	#region Class: UpdateSsoContact

	/// <summary>
	/// Class updates contact after login via SSO.
	/// </summary>
	[DefaultBinding(typeof(ISsoContactUpdater))]
	public class SsoContactUpdater: ISsoContactUpdater {

		#region Class: ContactUpdaterRetryException

		public class ContactUpdaterRetryException : Exception {
			public ContactUpdaterRetryException(string message) : base(message) {}
		}

		#endregion

		#region Constants: Private

		private const int _maxRetryCount = 3;

		private const int _retryDelayInSeconds = 3;

		#endregion

		#region Fields: Private

		private readonly ILog _log = LogManager.GetLogger("UpdateSsoContact");

		private readonly TimeSpan _retryDelayTimeSpan= TimeSpan.FromSeconds(_retryDelayInSeconds);

		private readonly UserConnection _uc;

		#endregion

		#region Constructors: Public

		public SsoContactUpdater(UserConnection uc) {
			_uc = uc;
		}

		#endregion

		#region Methods: Private

		private Dictionary<string, string> GetContactValues(Dictionary<string, string> contactValues) {
			if (GlobalAppSettings.FeatureSsoJitWithoutScheduler) {
				var rawData = ((Select)new Select(_uc).Top(1)
					.Column("Response")
					.From("SamlResponse")
					.Where("ContactId").IsEqual(Column.Parameter(_uc.CurrentUser.ContactId)))
					.ExecuteScalar<string>();
				return rawData.IsNullOrEmpty()
					? new Dictionary<string, string>()
					: Json.Deserialize<Dictionary<string, string>>(rawData);
			}
			return contactValues ?? new Dictionary<string, string>();
		}

		private void UpdateContactRights(EntitySchema contactSchema, Guid contactId,
		Dictionary<string, string> contactValues) {
			if (contactValues.TryGetValue("IsNewRecord", out string isNewRecord) &&
						isNewRecord.Equals("true", StringComparison.InvariantCultureIgnoreCase)) {
				var userId = new Guid(contactValues["SysAdminUnitId"]);
				_uc.DBSecurityEngine.AddDefRights(contactId, userId, contactId, contactSchema);
			}
		}

		private void UpdateContact(Entity contact, Dictionary<string, string> contactValues) {
			FillContactValues(contact, contactValues);
			RunFileImport(contact, contactValues);
		}

		private void FillContactValues(Entity contact, Dictionary<string, string> contactValues) {
			if(FeatureUtilities.GetIsFeatureEnabled(_uc, "DisableFillRequredColumnsOnSsoLogin")) {
				return;
			}
			foreach (var column in contact.Schema.Columns.Where(c => c.RequirementType != EntitySchemaColumnRequirementType.None)) {
				if (!contactValues.ContainsKey(column.Name)) {
					var entityColumnValue = contact.FindEntityColumnValue(column.ColumnValueName);
					if (entityColumnValue.IsLoaded && !DataTypeUtilities.ValueIsNullOrEmpty(entityColumnValue.Value)) {
						contactValues.Add(entityColumnValue.Name, entityColumnValue.Value.ToString());
					} else if (column.HasDefValue) {
						contactValues.Add(entityColumnValue.Name, column.DefValue.Value.ToString());
					}
				}
			}
		}

		private void RunFileImport(Entity contact, Dictionary<string, string> contactValues) {
			var importParamsGenerator = new BaseImportParamsGenerator();
			ImportParameters parameters = importParamsGenerator.GenerateParameters(contact.Schema, contactValues);
			var fileImporter = ClassFactory.Get<IFileImporter>(new ConstructorArgument("userConnection", _uc));
			fileImporter.ImportEntitySaveError += OnImportEntityError;
			fileImporter.ImportWithParams(parameters);
		}

		private void OnImportEntityError(object obj, ImportEntitySaveErrorEventArgs args) {
			_log.Error($"Import entity was unsuccessfully, reason is '{args.Exception.Message}'.", args.Exception);
		}

		private void ClearSamlResponseStorage() {
			if (GlobalAppSettings.FeatureSsoJitWithoutScheduler) {
				new Delete(_uc)
					.From("SamlResponse")
					.Where("ContactId").IsEqual(Column.Parameter(_uc.CurrentUser.ContactId))
					.Execute();
			}
		}

		#endregion

		#region Methods: Public

		public void UpdateContact() {
			UpdateContact(null);
		}

		public void UpdateContact(Dictionary<string, string> contactValues) {
			try {
				var actualContactValues = GetContactValues(contactValues);
				if (!actualContactValues.Any()) {
					_log.Debug($"No saml response for {_uc.CurrentUser.ContactId}, update skipped");
					return;
				}
				EntitySchema contactSchema = _uc.EntitySchemaManager.GetInstanceByName("Contact");
				if (!actualContactValues.TryGetValue(contactSchema.GetPrimaryColumnName(), out string contactId)) {
					_log.Debug($"Saml response for {_uc.CurrentUser.ContactId}, not contains contact id, update skipped");
					return;
				}
				var contact = contactSchema.CreateEntity(_uc);
				var attempt = 0;
				var policy = new SyncRetryPolicy<ContactUpdaterRetryException>();
				policy.Execute(_maxRetryCount, _retryDelayTimeSpan, () => { 
					if (contact.FetchFromDB(new Guid(contactId))) {
						_log.Debug($"Update contact started for {_uc.CurrentUser.ContactId}");
						UpdateContact(contact, actualContactValues);
						_log.Debug($"Update contact ended for {_uc.CurrentUser.ContactId}");
						UpdateContactRights(contactSchema, contact.PrimaryColumnValue, actualContactValues);
					} else {
						attempt++;
						_log.Debug($"FetchFromDB contact false for {_uc.CurrentUser.ContactId}, attempt number: {attempt}");
						if (attempt < _maxRetryCount) {
							throw new ContactUpdaterRetryException($"retry attempt: {attempt}");
						}
					}
				});
				ClearSamlResponseStorage();
			} catch (Exception e) {
				_log.Error("Error updating contact using saml response", e);
			}
		}

		#endregion

	}

	#endregion

}

