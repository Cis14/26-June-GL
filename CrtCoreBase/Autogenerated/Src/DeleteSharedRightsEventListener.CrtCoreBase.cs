 namespace Terrasoft.Configuration 
{
	using System;
	using System.Data;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;

	/// <summary>
	/// Class starts object updating for three entities using <see cref="BaseEntityEventListener"/> implementation.
	/// </summary>
	[EntityEventListener(SchemaName = "MailboxSyncSettings")]
	class DeleteSharedRightsEventListener : BaseEntityEventListener
	{

		#region Methods: Private

		private void ProcessEntity(object sender, EntityAfterEventArgs e) {
			var entity = (Entity)sender;
			UserConnection userConnection = entity.UserConnection;
			var changedColumnValues = entity.GetChangedColumnValues();
			if (changedColumnValues.Any(x => x.Name == "IsShared") && !entity.GetTypedColumnValue<bool>("IsShared"))
			{
				DeleteSharedRights(userConnection, entity);
			}
		}
		
		private void DeleteSharedRights(UserConnection userConnection, Entity entity)
		{
			var entityId = entity.GetTypedColumnValue<Guid>("Id");

			var ownerSelect = new Select(userConnection)
				.Column("SysAdminUnitId")
				.From("MailboxSyncSettings")
				.Where("Id").IsEqual(Column.Parameter(entityId)) as Select;
			var mailboxOwnerId = ownerSelect.ExecuteScalar<Guid>();
			var del = new Delete(userConnection)
				.From("SysMailboxSyncSettingsRight")
				.Where("RecordId").IsEqual(Column.Parameter(entityId))
				.And("SysAdminUnitId").IsNotEqual(Column.Parameter(mailboxOwnerId)) as Delete;
			del.Execute();
			var sel = new Select(userConnection)
				.Column("Id")
				.From("SysMailboxSyncSettingsRight")
				.Where("RecordId").IsEqual(Column.Parameter(entityId))
				.And("SysAdminUnitId").IsEqual(Column.Parameter(mailboxOwnerId)) as Select;
			sel.BuildParametersAsValue = true;
			using (DBExecutor dbExecutor = userConnection.EnsureDBConnection())
			{
				using (IDataReader dr = sel.ExecuteReader(dbExecutor))
				{
					if (!dr.Read())
					{
						Insert ins =
							new Insert(userConnection)
								.Into("SysMailboxSyncSettingsRight")
								.Set("RecordId", Column.Parameter(entityId))
								.Set("SysAdminUnitId", Column.Parameter(mailboxOwnerId))
								.Set("Operation", Column.Parameter(EntitySchemaRecordRightOperation.Read))
								.Set("RightLevel", Column.Parameter(EntitySchemaRecordRightLevel.AllowAndGrant))
								.Set("Position", Column.Parameter(0))
								.Set("SourceId", Column.Parameter("8A248A03-E9A5-DF11-9989-485B39C18470"))
								 as Insert;
						ins.Execute();
						ins =
							new Insert(userConnection)
								.Into("SysMailboxSyncSettingsRight")
								.Set("RecordId", Column.Parameter(entityId))
								.Set("SysAdminUnitId", Column.Parameter(mailboxOwnerId))
								.Set("Operation", Column.Parameter(EntitySchemaRecordRightOperation.Edit))
								.Set("RightLevel", Column.Parameter(EntitySchemaRecordRightLevel.AllowAndGrant))
								.Set("Position", Column.Parameter(0))
								.Set("SourceId", Column.Parameter("8A248A03-E9A5-DF11-9989-485B39C18470"))
								 as Insert;
						ins.Execute();
						ins =
							new Insert(userConnection)
								.Into("SysMailboxSyncSettingsRight")
								.Set("RecordId", Column.Parameter(entityId))
								.Set("SysAdminUnitId", Column.Parameter(mailboxOwnerId))
								.Set("Operation", Column.Parameter(EntitySchemaRecordRightOperation.Delete))
								.Set("RightLevel", Column.Parameter(EntitySchemaRecordRightLevel.AllowAndGrant))
								.Set("Position", Column.Parameter(0))
								.Set("SourceId", Column.Parameter("8A248A03-E9A5-DF11-9989-485B39C18470"))
								 as Insert;
						ins.Execute();
					}
				}
			}
			var edfDel = new Delete(userConnection)
				.From("EmailDefRights")
				.Where("RecordId").IsEqual(Column.Parameter(entityId)) as Delete;
			edfDel.Execute();
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Handles entity Updated event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e"><see cref="EntityAfterEventArgs"/> instance containing  event data.
		/// </param>
		public override void OnUpdated(object sender, EntityAfterEventArgs e) {
			base.OnUpdated(sender, e);
			ProcessEntity(sender, e);
		}

		#endregion

	}
}

