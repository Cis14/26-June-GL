namespace Terrasoft.Configuration 
{
	using global::Common.Logging;
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using System.Linq;
	using System.Collections.Generic;

	#region Class: DeleteMicrosoftUserCspScriptExecutor

	public class DeleteMicrosoftUserCspScriptExecutor : IInstallScriptExecutor {

		#region Fields: Private

		private static readonly ILog _logger = LogManager.GetLogger("MicrosoftCspInstallScript");

		private readonly object[] _cspUsrPolicyIds = {
			new Guid("6FD45887-5677-4D31-A770-8DD74AC0E810"),
			new Guid("F93906A1-2358-4ACA-9FE7-46426F2581A5"),
			new Guid("F081BD08-18F1-40F5-AEA1-ABDCD65D0F4C"),
			new Guid("8AB235C4-054A-4DAB-8472-D2ECC73F6F52"),
			new Guid("A7593452-2AEA-4D23-B5AA-A81B997B5AB4"),
			new Guid("84E7AD88-0C2A-4C09-9405-667CDABA3115"),
			new Guid("11B63218-7009-41C6-A9F5-B3B307EC1303"),
			new Guid("09EC5FC0-8B85-4B2A-9C23-4C10CA4517A5")
		};

		private readonly Guid _frameAncestorDirectiveId = new Guid("32F1FAA1-4E6F-4194-A79C-505D3203C7A0");

		#endregion

		#region Methods: Private

		private EntitySchema GetEntitySchema(UserConnection userConnection, string schemaName) {
			EntitySchemaManager entitySchemaManager = userConnection.EntitySchemaManager;
			ISchemaManagerItem<EntitySchema> managerItem = entitySchemaManager.GetItemByName(schemaName);
			return entitySchemaManager.GetRuntimeInstanceFromMetadata(managerItem.UId);
		}
		
		private List<Guid> DeleteSysCspUsrSrcInDirectv(UserConnection userConnection) {
			var usedTrustedSourceIds = new List<Guid>();
			EntitySchema sysCspUsrSrcInDirectvEntitySchema = GetEntitySchema(userConnection, "SysCspUsrSrcInDirectv");
			var esq = new EntitySchemaQuery(sysCspUsrSrcInDirectvEntitySchema);
			esq.AddAllSchemaColumns();
			esq.Filters.Add(esq.CreateFilterWithParameters(
				FilterComparisonType.Equal, "CspUserTrustedSource", _cspUsrPolicyIds));
			esq.GetEntityCollection(userConnection)
				.ToList()
				.ForEach(entity => {
					var cspDirectiveNameId = entity.GetTypedColumnValue<Guid>("CspDirectiveNameId");
					if (cspDirectiveNameId != Guid.Empty && cspDirectiveNameId == _frameAncestorDirectiveId)	{
						entity.Delete();
					} else {
						usedTrustedSourceIds.Add(entity.GetTypedColumnValue<Guid>("CspUserTrustedSourceId"));
					}
				});
			return usedTrustedSourceIds;
		}

		private void DeleteSysCspUserTrustedSrc(UserConnection userConnection, List<Guid> usedTrustedSourceIds) {
			EntitySchema sysCspUserTrustedSrcEntitySchema = GetEntitySchema(userConnection, "SysCspUserTrustedSrc");
			var esq = new EntitySchemaQuery(sysCspUserTrustedSrcEntitySchema);
			esq.AddAllSchemaColumns();
			esq.Filters.Add(esq.CreateFilterWithParameters(
				FilterComparisonType.Equal, "Id", _cspUsrPolicyIds));
			if (usedTrustedSourceIds.Count > 0) {
				esq.Filters.Add(esq.CreateFilterWithParameters(
					FilterComparisonType.NotEqual, "Id", usedTrustedSourceIds.Cast<object>().ToArray()));
			}
			var entities = esq.GetEntityCollection(userConnection);
			esq.GetEntityCollection(userConnection)
				.ToList()
				.ForEach(entity => {
					try {
						entity.Delete();
					} catch (Exception e){
						_logger.Error($"Error deleting SysCspUserTrustedSrc record Id: {entity.PrimaryColumnValue}", e);
					}
				});
		}

		#endregion

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			var usedSourceIds = DeleteSysCspUsrSrcInDirectv(userConnection);
			DeleteSysCspUserTrustedSrc(userConnection, usedSourceIds);
		}
		
		#endregion

	}

	#endregion

}

