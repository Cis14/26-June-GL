namespace Terrasoft.Configuration.RightsService
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;
	using System.Security;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.SchemaManagement;
	using Terrasoft.Core.Security;
	using Terrasoft.Nui.ServiceModel.DataContract;

	#region Class: SchemaRightsHelper

	/// <summary>
	/// Helper class for managing schema rights.
	/// </summary>
	[DefaultBinding(typeof(ISchemaRightsHelper))]
	public class SchemaRightsHelper : ISchemaRightsHelper
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="SchemaRightsHelper"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		public SchemaRightsHelper(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		private Dictionary<Guid, string> GetAdminUnitListNames(IEnumerable<Guid> sysAdminUnitIds) {
			List<Guid> adminUnitIds = sysAdminUnitIds?.ToList();
			if (adminUnitIds == null || !adminUnitIds.Any()) {
				return new Dictionary<Guid, string>();
			}
			var sysAdminUnitNames = new Dictionary<Guid, string>();
			var select = (Select)new Select(_userConnection)
					.Column("Id")
					.Column("Name")
				.From("SysAdminUnit").WithHints(Hints.NoLock)
				.Where("Id").In(Column.Parameters(adminUnitIds));
			using (var dbExecutor = _userConnection.EnsureDBConnection()) {
				using (var dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						Guid id = dataReader.GetColumnValue<Guid>("Id");
						string name = dataReader.GetColumnValue<string>("Name");
						sysAdminUnitNames.Add(id, name);
					}
				}
			}
			return sysAdminUnitNames;
		}

		private LocalizableString GetLocalizableString(string localizableStringName) {
			string lsv = "LocalizableStrings." + localizableStringName + ".Value";
			return new LocalizableString(_userConnection.Workspace.ResourceStorage, nameof(SchemaRightsHelper), lsv);
		}

		#endregion

		#region Methods: Pubblic

		/// <summary>
		/// Gets the list of managed operation types for a schema.
		/// </summary>
		/// <param name="schemaType">The schema type.</param>
		/// <returns>List of operation type integers.</returns>
		public List<int> GetSchemaManagedOperations(string schemaType) {
			schemaType.CheckArgumentNullOrEmpty("SchemaType");
			ISchemaManager manager = _userConnection.GetSchemaManager(schemaType);
			ISchemaService instance = manager.GetSchemaService(_userConnection);
			IEnumerable<UserSchemaOperationRights> operations = instance.GetSchemaManagedOperations();
			return operations.Select(oper => (int)oper).ToList();
		}

		/// <summary>
		/// Gets the record rights for a specific schema and record.
		/// </summary>
		/// <param name="schemaType">The schema type.</param>
		/// <param name="recordId">The record identifier.</param>
		/// <returns>List of <see cref="SchemaRecordRight"/> objects.</returns>
		public List<SchemaRecordRight> GetRecordRights(string schemaType, Guid recordId) {
			schemaType.CheckArgumentNullOrEmpty("SchemaType");
			recordId.CheckArgumentEmpty("RecordId");
			ISchemaManager manager = _userConnection.GetSchemaManager(schemaType);
			ISchemaService instance = manager.GetSchemaService(_userConnection);
			if (!instance.GetCanEditSchemaRights()) {
				throw new SecurityException(GetLocalizableString("NoPermissionsForSchemaRightsMessage").Value);
			}
			IEnumerable<BaseSchemaOperationRight> listOfItems = instance.FindRightsBySchemaUId(recordId).ToList();
			IEnumerable<Guid> sysAdminUnitIds = listOfItems.Select(it => it.SysAdminUnitId).Distinct();
			Dictionary<Guid, string> sysAdminUnits = GetAdminUnitListNames(sysAdminUnitIds);
			var result = new List<SchemaRecordRight>();
			foreach (BaseSchemaOperationRight item in listOfItems) {
				result.Add(new SchemaRecordRight {
					Id = item.Id,
					OperationType = item.Operation,
					SysAdminUnit = new LookupColumnValue {
						DisplayValue =
							sysAdminUnits.TryGetValue(item.SysAdminUnitId, out string name)
								? name
								: GetLocalizableString("DefaultAdminUnit").Value,
						Value = item.SysAdminUnitId.ToString()
					},
					IsDeleted = false,
					IsNew = false
				});
			}
			return result;
		}

		/// <summary>
		/// Applies changes to schema record rights.
		/// </summary>
		/// <param name="recordRights">Array of record rights to apply.</param>
		/// <param name="schemaType">The schema type.</param>
		/// <param name="recordId">The record identifier.</param>
		public void ApplyChanges(SchemaRecordRight[] recordRights, string schemaType, Guid recordId) {
			schemaType.CheckArgumentNullOrEmpty("SchemaType");
			recordId.CheckArgumentEmpty("RecordId");
			ISchemaManager manager = _userConnection.GetSchemaManager(schemaType);
			ISchemaService instance = manager.GetSchemaService(_userConnection);
			List<Guid> recordIdsToDelete = recordRights.Where(rr => rr.IsDeleted).Select(rr => rr.Id).ToList();
			List<SchemaRecordRight> recordsToAdd = recordRights.Where(rr => rr.IsNew && !rr.IsDeleted).ToList();
			List<SchemaRecordRight> recordsToUpdate = recordRights.Where(rr => !rr.IsNew && !rr.IsDeleted).ToList();
			if (recordIdsToDelete.Any()) {
				instance.DeleteRightsByIds(new DeleteRightsRequest(recordIdsToDelete));
			}
			if (recordsToAdd.Any()) {
				var rightsToAdd = recordsToAdd.Select(rr => new AddRightsRecordDto {
					Operation = rr.OperationType,
					SysAdminUnitId = Guid.Parse(rr.SysAdminUnit.Value)
				});
				instance.AddRights(new AddRightsRequest(recordId, rightsToAdd));
			}
			if (recordsToUpdate.Any()) {
				var rightsToUpdate = recordsToUpdate.Select(rr => new UpdateRightsRecordDto()
				{
					RecordId = rr.Id,
					Operation = rr.OperationType,
				});
				instance.UpdateRights(new UpdateRightsRequest(recordId, rightsToUpdate));
			}
		}

		#endregion

	}

	#endregion

	#region Interface: ISchemaRightsHelper

	/// <summary>
	/// Provides methods for managing schema rights.
	/// </summary>
	public interface ISchemaRightsHelper
	{

		#region Methods: Public

		/// <summary>
		/// Gets the list of managed operation types for a schema.
		/// </summary>
		/// <param name="schemaType">The schema type.</param>
		/// <returns>List of operation type integers.</returns>
		List<int> GetSchemaManagedOperations(string schemaType);

		/// <summary>
		/// Gets the record rights for a specific schema and record.
		/// </summary>
		/// <param name="schemaType">The schema type.</param>
		/// <param name="recordId">The record identifier.</param>
		/// <returns>List of <see cref="SchemaRecordRight"/> objects.</returns>
		List<SchemaRecordRight> GetRecordRights(string schemaType, Guid recordId);

		/// <summary>
		/// Applies changes to schema record rights.
		/// </summary>
		/// <param name="recordRights">Array of record rights to apply.</param>
		/// <param name="schemaType">The schema type.</param>
		/// <param name="recordId">The record identifier.</param>
		void ApplyChanges(SchemaRecordRight[] recordRights, string schemaType, Guid recordId);

		#endregion

	}

	#endregion

	#region Class: SchemaRecordRight

	/// <summary>
	/// Represents a schema record right.
	/// </summary>
	[DataContract]
	public class SchemaRecordRight
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[DataMember(Name = "id")]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the operation type.
		/// </summary>
		[DataMember(Name = "operationType")]
		public int OperationType { get; set; }

		/// <summary>
		/// Gets or sets the system admin unit.
		/// </summary>
		[DataMember(Name = "sysAdminUnit")]
		public LookupColumnValue SysAdminUnit { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this right is deleted.
		/// </summary>
		[DataMember(Name = "isDeleted")]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this right is new.
		/// </summary>
		[DataMember(Name = "isNew")]
		public bool IsNew { get; set; }

		#endregion

	}

	#endregion

}
