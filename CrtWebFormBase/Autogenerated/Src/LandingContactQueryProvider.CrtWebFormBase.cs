namespace Terrasoft.Configuration
{
	using System;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;

	#region Class: LandingContactQueryProvider

	/// <summary>
	/// Provides Query models to the specified schemas.
	/// </summary>
	public class LandingContactQueryProvider
	{

		#region Fields: Private

		private UserConnection _userConnection;

		#endregion

		public LandingContactQueryProvider(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#region Methods: Private

		private (string name, Guid uId) GetSchemaNameByLanding(Guid webFormId) {
			var schemaUidSelect = new Select(_userConnection)
				.Column("LT", "SchemaUidId")
				.From("GeneratedWebForm").As("GWF")
				.InnerJoin("LandingType").As("LT")
					.On("LT", "Id").IsEqual("GWF", "TypeId")
				.Where("GWF", "Id").IsEqual(Column.Parameter(webFormId)) as Select;
			schemaUidSelect.SpecifyNoLockHints();
			schemaUidSelect.IsCacheEnabled = true;
			var schemaUid = schemaUidSelect.ExecuteScalar<Guid>();
			var schema = _userConnection.EntitySchemaManager.GetItems().FirstOrDefault(x => x.UId == schemaUid);
			return (schema?.Name ?? string.Empty, schemaUid);
		}

		private (string contactColumn, string webFormColumn) GetLandingEntityInfo(string entitySchemaName) {
			var select = new Select(_userConnection)
				.Column("cle", "ContactColumn")
				.Column("cle", "WebFormColumn")
				.From("CampaignLandingEntity").As("cle")
				.InnerJoin("SysSchema").As("ss").On("ss", "UId").IsEqual("cle", "EntityObjectId")
				.Where("ss", "Name").IsEqual(Column.Parameter(entitySchemaName)) as Select;
			select.SpecifyNoLockHints();
			var result = default((string, string));
			select.ExecuteReader(reader => {
				result = (reader.GetColumnValue<string>("ContactColumn"),
					reader.GetColumnValue<string>("WebFormColumn"));
			});
			if (result == default((string, string))) {
				throw new ArgumentException(string.Format("Unregistered entity {0} of campaign landing element",
					entitySchemaName));
			}
			return result;
		}

		private void RemoveExtraColumns(ref Select contactSelect) {
			var columns = contactSelect.Columns
				.Where(x => x.Alias == "ContactId" || x.Alias == "LinkedEntityId")
				.ToList();
			contactSelect.Columns.Clear();
			contactSelect.Columns.AddRange(columns);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Prepares <see cref="ContactSelectModel"/> with select query and columns list for the specified webform.
		/// </summary>
		/// <param name="webFormId">The unique identifier of landing.</param>
		/// <returns>An instance of <see cref="ContactSelectModel"/>.</returns>
		/// <exception cref="ArgumentException">In case of schema absence.</exception>
		public virtual ContactSelectModel GetSelectModel(Guid webFormId) {
			var landingSchemaInfo = GetSchemaNameByLanding(webFormId);
			var landingEntityInfo = GetLandingEntityInfo(landingSchemaInfo.name);
			if (string.IsNullOrWhiteSpace(landingSchemaInfo.name)) {
				throw new ArgumentException(string.Format("Unknown schema of webform {0}", webFormId));
			}
			var entitySchema = _userConnection.EntitySchemaManager.GetInstanceByName(landingSchemaInfo.name);
			var esq = new EntitySchemaQuery(entitySchema) {
				IsDistinct = true
			};
			esq.AddColumn("Id").SetForcedQueryColumnValueAlias("LinkedEntityId");
			esq.AddColumn(landingEntityInfo.contactColumn).SetForcedQueryColumnValueAlias("ContactId");
			var isNotNullContactFilter = esq.CreateIsNotNullFilter(landingEntityInfo.contactColumn);
			var webFormFilter = esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				landingEntityInfo.webFormColumn, webFormId);
			var activeStateFilter = esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				landingEntityInfo.webFormColumn + ".State", LendingConsts.ActiveLendingStateId);
			esq.Filters.AddRange(new [] {
				isNotNullContactFilter,
				webFormFilter,
				activeStateFilter
			});
			var contactSelect = esq.GetSelectQuery(_userConnection);
			RemoveExtraColumns(ref contactSelect);
			return new ContactSelectModel {
				ContactSelect = contactSelect,
				ContactIdColumnName = "ContactId",
				AudienceSchemaUId = landingSchemaInfo.uId
			};
		}

		/// <summary>
		/// Provides path to contact column for the specified landing.
		/// </summary>
		/// <param name="webFormId">Landing identifier.</param>
		/// <returns>String value of contact column path.</returns>
		public string GetLandingEntityContactColumn(Guid webFormId) {
			var landingSchemaInfo = GetSchemaNameByLanding(webFormId);
			var landingEntityInfo = GetLandingEntityInfo(landingSchemaInfo.name);
			return landingEntityInfo.contactColumn;
		}

		#endregion

	}

	#endregion

}
