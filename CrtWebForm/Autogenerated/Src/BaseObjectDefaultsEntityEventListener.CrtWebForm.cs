namespace Terrasoft.Configuration
{
	using System;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Core.DB;
	using Terrasoft.Core;
	using Terrasoft.Common;
	using System.Collections.Generic;
	 
	#region Class: BaseObjectDefaultsEventListener

	/// <summary>
	/// Base listener for 'ObjectDefaults' entity events.
	/// </summary>
	/// <seealso cref="Terrasoft.Core.Entities.Events.BaseEntityEventListener" />
	public class BaseObjectDefaultsEntityEventListener: BaseEntityEventListener {
	
		#region Methods: Public

		/// <summary>
		/// Handles entity Saving event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">The <see cref="T:Terrasoft.Core.Entities.EntityBeforeEventArgs" /> instance containing the
		/// event data.</param>
		public override void OnSaving(object sender, EntityBeforeEventArgs e) {
			base.OnSaving(sender, e);
            Entity entity = (Entity)sender;
			var cardDislayValue = entity.GetTypedColumnValue<string>("DisplayValue");
			if (cardDislayValue.IsNotNullOrEmpty()) {
				return;
			}
            var landingId = entity.GetTypedColumnValue<Guid>("LandingId");
            var guidValue = entity.GetTypedColumnValue<Guid>("GuidValue");
            var userConnection = entity.UserConnection;
            Select selectQuery = new Select(userConnection)
                    .Top(1)
                        .Column("lt", "SchemaUidId").As("schemaUId")
                    .From("GeneratedWebForm", "gwf")
                    .InnerJoin("LandingType").As("lt")
                        .On("lt", "Id").IsEqual("gwf", "TypeId")
                    .Where("gwf", "Id").IsEqual(Column.Parameter(landingId))
                        as Select;
            selectQuery.ExecuteSingleRecord(r =>
                r.GetColumnValue<Guid>("schemaUId"), out var schemaUId);
			if (schemaUId == Guid.Empty) {
				return;
			}
            EntitySchema rootSchema = userConnection.EntitySchemaManager.GetInstanceByUId(schemaUId);
            if (rootSchema == null) {
                return;
            }
            var columnUId = entity.GetTypedColumnValue<Guid>("ColumnUId");
            var column = rootSchema.Columns.FindByUId(columnUId);
            if (column == null) {
                return;
            }
            var displayValues = new List<string>();
            if (column.DataValueType.UId == DataValueType.LookupDataValueTypeUId) {
                EntitySchema lookupSchema = userConnection.EntitySchemaManager.GetInstanceByName(column.ReferenceSchema.Name);
                var lookupEntity = lookupSchema.CreateEntity(userConnection);
                var lookupDisplayValue = lookupEntity.FetchPrimaryInfoFromDB(lookupSchema.PrimaryColumn, guidValue) 
					? lookupEntity.PrimaryDisplayColumnValue : string.Empty;
				displayValues.Add(lookupDisplayValue);
            }
			var dtf = userConnection.CurrentUser.Culture.DateTimeFormat;
			var dateTimeValue = entity.GetTypedColumnValue<DateTime>("DateTimeValue");
			if (dateTimeValue != DateTime.MinValue) {
                if (column.DataValueType.UId == DataValueType.TimeDataValueTypeUId) {
					var timeDisplayValue = dateTimeValue.ToString(dtf.ShortTimePattern);
                    displayValues.Add(timeDisplayValue);
                }
                if (column.DataValueType.UId == DataValueType.DateDataValueTypeUId) {
                    var dateDisplayValue = dateTimeValue.ToString(dtf.ShortDatePattern);
                    displayValues.Add(dateDisplayValue);
                }
                if (column.DataValueType.UId == DataValueType.DateTimeDataValueTypeUId) {
                    var datetTimeDisplayValue = entity.GetColumnDisplayValue(entity.Schema.Columns.GetByName("DateTimeValue"));
                    displayValues.Add(datetTimeDisplayValue);
                }
            }
			var columns = new String[] { "BooleanValue", "FloatValue", "IntegerValue", "TextValue" };
			columns.ForEach(
				columnName => displayValues.Add(entity.GetColumnDisplayValue(entity.Schema.Columns.GetByName(columnName)))
			);
			var displayValue = StringUtilities.ConcatIfNotEmpty(displayValues, "");
            entity.SetColumnValue("DisplayValue", displayValue.Substring(0, Math.Min(displayValue.Length, 500)));
		}

		#endregion

	}

	#endregion

}

