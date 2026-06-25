namespace Terrasoft.Core.Process.Configuration
{
	using System;
	using System.Text;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Process;

	#region Class: CreateFilterFolder

	/// <exclude/>
	public partial class CreateFilterFolder
	{

		#region Class: FolderTreeColumns

		private static class FolderTreeColumns
		{
			public const string Id = "Id";
			public const string Name = "Name";
			public const string ParentId = "ParentId";
			public const string EntitySchemaName = "EntitySchemaName";
			public const string FilterData = "FilterData";
		}

		#endregion

		#region Class: EntitySchemaFolderColumns

		private static class EntitySchemaFolderColumns
		{
			public const string Id = "Id";
			public const string Name = "Name";
			public const string ParentId = "ParentId";
			public const string SearchData = "SearchData";
			public const string FolderTypeId = "FolderTypeId";
		}

		#endregion

		private const string _parentFolderName = "AI Filters";
		private const string _folderTreeSchemaName = "FolderTree";

		private static Guid _dynamicFolderTypeId = new Guid("65CA0946-0084-4874-B117-C13199AF3B95");

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {
			var folderSchemaName = EntitySchemaName + "Folder";
			var jsonBytes = Encoding.UTF8.GetBytes(FilterJsonConfiguration);

			var folderSchemaItem = UserConnection.EntitySchemaManager.FindItemByName(folderSchemaName);
			var useFolderTree = folderSchemaItem == null;

			var parentFolderId = GetParentFolderTreeId();

			if (parentFolderId == Guid.Empty) {
				if (!TryInsertParentTreeFolder()) {
					Log.Error($"Folder '{_parentFolderName}' was not found and could not be created.");
					return false;
				}

				parentFolderId = GetParentFolderTreeId();
			}

			var wasFolderTreeCreated = TryInsertTreeFolder(parentFolderId, jsonBytes);

			if(!wasFolderTreeCreated) {
				var wasRequired = useFolderTree ? "required" : "optional";
				Log.Error($"Failed to insert {wasRequired} FolderTree for '{folderSchemaName}' inside '{_parentFolderName}'.");
			}
			
			var parentEntityFolderId = GetParentEntitySchemaFolderId(folderSchemaName);

			if (parentEntityFolderId == Guid.Empty) {
				if (!TryInsertParentEntitySchemaFolder(folderSchemaName)) {
					Log.Error($"Folder '{_parentFolderName}' was not found and could not be created.");
					return false;
				}

				parentEntityFolderId = GetParentEntitySchemaFolderId(folderSchemaName);
			}

			var wasEntitySchemaFolderCreated = TryInsertEntitySchemaFolder(folderSchemaName, parentEntityFolderId, jsonBytes);

			if(!wasEntitySchemaFolderCreated) {
				Log.Error($"Failed to insert EntitySchemaFolder for '{folderSchemaName}' inside '{parentEntityFolderId}'.");
			}

			return wasEntitySchemaFolderCreated && ((useFolderTree && wasFolderTreeCreated) || !useFolderTree);
		}

		#endregion

		#region Methods: Private

		private bool TryInsertTreeFolder(Guid parentFolderId, byte[] jsonBytes) {
			var insertOperation = new Insert(UserConnection)
				.Into(_folderTreeSchemaName)
				.Set(FolderTreeColumns.Name, Column.Parameter(FolderName))
				.Set(FolderTreeColumns.ParentId, Column.Parameter(parentFolderId))
				.Set(FolderTreeColumns.FilterData, Column.Parameter(jsonBytes))
				.Set(FolderTreeColumns.EntitySchemaName, Column.Parameter(EntitySchemaName))
				.Execute();

			return insertOperation != 0;
		}

		private bool TryInsertParentTreeFolder() {
			var insertParentFolder = new Insert(UserConnection)
				.Into(_folderTreeSchemaName)
				.Set(FolderTreeColumns.Name, Column.Parameter(_parentFolderName))
				.Set(FolderTreeColumns.EntitySchemaName, Column.Parameter(EntitySchemaName))
				.Execute();

			return insertParentFolder != 0;
		}

		private Guid GetParentFolderTreeId() {
			var selectOperation = new Select(UserConnection)
				.Column(FolderTreeColumns.Id)
				.From(_folderTreeSchemaName)
				.Where(FolderTreeColumns.Name).IsEqual(Column.Parameter(_parentFolderName))
				.And(FolderTreeColumns.EntitySchemaName).IsEqual(Column.Parameter(EntitySchemaName)) as Select;

			return selectOperation.ExecuteScalar<Guid>();
		}

		private bool TryInsertEntitySchemaFolder(string folderSchemaName, Guid parentFolderId, byte[] jsonBytes) {
			var insertOperation = new Insert(UserConnection)
				.Into(folderSchemaName)
				.Set(EntitySchemaFolderColumns.Name, Column.Parameter(FolderName))
				.Set(EntitySchemaFolderColumns.FolderTypeId, Column.Parameter(_dynamicFolderTypeId))
				.Set(EntitySchemaFolderColumns.SearchData, Column.Parameter(jsonBytes))
				.Set(EntitySchemaFolderColumns.ParentId, Column.Parameter(parentFolderId))
				.Execute();

			return insertOperation != 0;
		}

		private bool TryInsertParentEntitySchemaFolder(string folderSchemaName) {
			var insertParentFolder = new Insert(UserConnection)
				.Into(folderSchemaName)
				.Set(EntitySchemaFolderColumns.Name, Column.Parameter(_parentFolderName))
				.Set(EntitySchemaFolderColumns.FolderTypeId, Column.Parameter(_dynamicFolderTypeId))
				.Execute();

			return insertParentFolder != 0;
		}

		private Guid GetParentEntitySchemaFolderId(string folderSchemaName) {
			var selectOperation = new Select(UserConnection)
				.Column(EntitySchemaFolderColumns.Id)
				.From(folderSchemaName)
				.Where(EntitySchemaFolderColumns.Name).IsEqual(Column.Parameter(_parentFolderName)) as Select;

			return selectOperation.ExecuteScalar<Guid>();
		}

		#endregion

	}

	#endregion

}

