namespace Terrasoft.Configuration.FileImport
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	/// <summary>
	/// An instance of this class works with import tags.
	/// </summary>
	[DefaultBinding(typeof(IFileImportTagManager), Name = nameof(FileImportTagManager))]
	public class FileImportTagManager: IFileImportTagManager
	{
		#region Constructors: Public

		/// <summary>
		/// Creates instance of type <see cref="FileImportTagManager"/>.
		/// </summary>
		/// <param name="userConnection">User connection.</param>
		public FileImportTagManager(UserConnection userConnection) {
			_userConnection = userConnection;
			_entitySchemaTags = new Dictionary<string, string>();
			_entitySchemaInTags = new Dictionary<string, string>();
			InitEntitySchemaTagsNames();
			InitEntitySchemaInTagsNames();
		}

		#endregion

		#region Fields: Private

		/// <summary>
		/// Entity schema tags names dictionary.
		/// </summary>
		private Dictionary<string, string> _entitySchemaTags;

		/// <summary>
		/// Entity schema inTags names dictionary.
		/// </summary>
		private Dictionary<string, string> _entitySchemaInTags;

		#endregion

		#region Properties: Protected

		/// <summary>
		/// User connection. 
		/// </summary>
		private UserConnection _userConnection;
		protected UserConnection UserConnection {
			get {
				return _userConnection;
			}
		}

		/// <summary>
		/// Import tags.
		/// </summary>
		private List<Entity> _importTags;
		protected List<Entity> ImportTags {
			get {
				return _importTags;
			}
			set {
				_importTags = value;
			}
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Returns root entity schema.
		/// </summary>
		/// <param name="rootSchemaUId">Root schema unique identifier.</param>
		/// <returns>Root entity schema.</returns>
		private EntitySchema GetRootEntitySchema(Guid rootSchemaUId) {
			return UserConnection.EntitySchemaManager.GetInstanceByUId(rootSchemaUId);
		}

		/// <summary>
		/// Creates tag.
		/// </summary>
		/// <param name="tagEntitySchemaName">Root entity schema name.</param>
		/// <param name="tagName">Tag name.</param>
		/// <returns>Tag entity.</returns>
		private Entity CreateTag(string tagEntitySchemaName, string tagName) {
			EntitySchema entitySchema = UserConnection.EntitySchemaManager.FindInstanceByName(tagEntitySchemaName);
			if (entitySchema == null) {
				return null;
			}
			Entity tag = entitySchema.CreateEntity(UserConnection);
			tag.SetDefColumnValues();
			tag.SetColumnValue("Name", tagName);
			tag.SetColumnValue("TypeId", TagConsts.PrivateTagTypeUId);
			return tag;
		}

		/// <summary>
		/// Saves primary column value and tag type to importTag.
		/// </summary>
		/// <param name="importTag"></param>
		/// <param name="tag"></param>
		private void SaveTagData(ImportTag importTag, Entity tag) {
			importTag.Value = tag.PrimaryColumnValue;
			importTag.Type = new ImportTagType {
				DisplayValue = new LocalizableString(UserConnection.Workspace.ResourceStorage, "FileImportTagManager",
					"LocalizableStrings.PrivateTagDisplayValue.Value"),
				Value = TagConsts.PrivateTagTypeUId
			};
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Initializes entity schema tags names dictionary.
		/// </summary>
		protected virtual void InitEntitySchemaTagsNames() {
			_entitySchemaTags.Add("KnowledgeBase", "KnowledgeBaseTagV2");
		}

		/// <summary>
		/// Initializes entity schema inTags names dictionary.
		/// </summary>
		protected virtual void InitEntitySchemaInTagsNames() {
			_entitySchemaInTags.Add("KnowledgeBase", "KnowledgeBaseInTagV2");
		}

		/// <summary>
		/// Gets entity schema tag schema name.
		/// </summary>
		/// <param name="entitySchemaName">Entity schema name.</param>
		/// <returns></returns>
		protected virtual string GetEntitySchemaTagSchemaName(string entitySchemaName) {
			string entitySchemaTagSchemaName = string.Format("{0}Tag", entitySchemaName);
			if (_entitySchemaTags.ContainsKey(entitySchemaName)) {
				entitySchemaTagSchemaName = _entitySchemaTags[entitySchemaName];
			}
			return entitySchemaTagSchemaName;
		}

		/// <summary>
		/// Gets entity schema inTag schema name.
		/// </summary>
		/// <param name="entitySchemaName">Entity schema name.</param>
		/// <returns></returns>
		protected virtual string GetEntitySchemaInTagSchemaName(string entitySchemaName) {
			string entitySchemaInTagSchemaName = string.Format("{0}InTag", entitySchemaName);
			if (_entitySchemaInTags.ContainsKey(entitySchemaName)) {
				entitySchemaInTagSchemaName = _entitySchemaInTags[entitySchemaName];
			}
			return entitySchemaInTagSchemaName;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Creates tags.
		/// </summary>
		/// <param name="sender">Event sender object.</param>
		/// <param name="args">Event arguments.</param>
		public void CreateTags(object sender, BeforeImportEntitiesSaveEventArgs args) {
			EntitySchema rootEntitySchema = GetRootEntitySchema(args.RootSchemaUId);
			ImportTags = new List<Entity>();
			foreach (ImportTag importTag in args.ImportTags) {
				Entity tag = CreateTag(GetEntitySchemaTagSchemaName(rootEntitySchema.Name), importTag.DisplayValue);
				if (tag != null) {
					ImportTags.Add(tag);
					tag.Save();
					SaveTagData(importTag, tag);
				}
			}
		}

		/// <summary>
		/// Saves all tags for each imported entity.
		/// </summary>
		/// <param name="sender">Event sender object.</param>
		/// <param name="args">Event arguments.</param>
		public void SaveEntityTags(object sender, ImportEntitySavedEventArgs args) {
			EntitySchema rootEntitySchema = GetRootEntitySchema(args.RootSchemaUId);
			EntitySchema entitySchema =
				UserConnection.EntitySchemaManager.FindInstanceByName(GetEntitySchemaInTagSchemaName(rootEntitySchema.Name));
			foreach (Entity tag in ImportTags) {
				Entity entity = entitySchema.CreateEntity(UserConnection);
				entity.SetDefColumnValues();
				entity.SetColumnValue("TagId", tag.GetColumnValue("Id"));
				entity.SetColumnValue("EntityId", args.PrimaryEntity.GetColumnValue("Id"));
				entity.Save();
			}
		}

		/// <summary>
		/// Deletes tags if entities doesn't import.
		/// </summary>
		/// <param name="sender">Event sender object.</param>
		/// <param name="args">Event arguments.</param>
		public void DeleteNotUsedTags(object sender, AfterImportEntitiesSaveEventArgs args) {
			if (args.ImportedRowsCount == 0) {
				foreach (Entity tag in ImportTags) {
					tag.Delete();
				}
			}
		}

		#endregion
	}
}
