namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.Copilot.Metadata;
	using Terrasoft.AppFeatures;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Store;

	#region Class: SortColumn

	/// <summary>
	/// Column for sorting in <see cref="EntitySchemaQuery"/>
	/// </summary>
	public class SortColumn
	{

		#region Properties: Public

		/// <summary>
		/// Gets or sets column name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets column order direction.
		/// </summary>
		public OrderDirection OrderDirection { get; set; }

		/// <summary>
		/// Gets or sets column order position.
		/// </summary>
		public int OrderPosition { get; set; }

		#endregion

	}

	#endregion

	#region Class: BaseCopilotIntentQueryExecutor

	public abstract class BaseCopilotIntentQueryExecutor : BaseQueryExecutor, IEntityQueryExecutor
	{

		#region Fields: Private

		private static readonly string _entitySchemaName = "CopilotIntent";
		private static readonly Dictionary<string, string> _sortingColumnMappings = new Dictionary<string, string> {
			{ "Mode", "ModeName" },
			{ "Status", "StatusName" },
			{ "CreatedBy", "CreatedByName" }
		};
		private readonly Dictionary<CopilotIntentType, (Guid Id, string Color)> _intentColorMap =
			new Dictionary<CopilotIntentType, (Guid Id, string Color)> {
				{ CopilotIntentType.Skill,
					(CopilotIntentTypeConstants.SkillIntentTypeId, CopilotIntentTypeConstants.SkillTypeColor) },
				{ CopilotIntentType.Agent,
					(CopilotIntentTypeConstants.AgentIntentTypeId, CopilotIntentTypeConstants.AgentTypeColor) },
				{ CopilotIntentType.System,
					(CopilotIntentTypeConstants.SystemIntentTypeId, CopilotIntentTypeConstants.SystemTypeColor) },
				{ CopilotIntentType.WorkflowAgent,
					(CopilotIntentTypeConstants.WorkflowAgentIntentTypeId, CopilotIntentTypeConstants.WorkflowAgentTypeColor) }
			};
		private CopilotIntentSchemaManager _copilotIntentSchemaManager;

		#endregion

		#region Constructors: Protected

		protected BaseCopilotIntentQueryExecutor(UserConnection userConnection)
			: base(userConnection, "CopilotIntent") {
		}

		#endregion

		#region Properties: Private

		private EntitySchema IntentEntitySchema =>
			UserConnection.EntitySchemaManager.GetInstanceByName(_entitySchemaName);

		#endregion

		#region Properties: Public

		public CopilotIntentSchemaManager CopilotIntentSchemaManager =>
			_copilotIntentSchemaManager ?? (_copilotIntentSchemaManager =
				(CopilotIntentSchemaManager)UserConnection.GetSchemaManager(nameof(CopilotIntentSchemaManager)));

		#endregion

		#region Methods: Private

		private void ApplySorting(EntitySchemaQuery esq, EntityCollection entityCollection) {
			SortColumn sortColumn = GetSorting(esq);
			if (sortColumn is null || entityCollection.Count == 0) {
				return;
			}
			string columnName = _sortingColumnMappings.TryGetValue(sortColumn.Name,
				out string mappedName)
				? mappedName
				: sortColumn.Name;
			entityCollection.Order(columnName,
				sortColumn.OrderDirection);
		}

		private IEnumerable<Entity> FilterByLookupColumnReference(List<object> parameterValues,
				EntitySchemaColumn lookupColumn, string targetColumnName, IEnumerable<Entity> entities) {
			string lookupSchemaName = lookupColumn.ReferenceSchema.Name;
			EntitySchemaQuery esq = GetCopilotIntentLookupESQ(lookupSchemaName);
			EntityCollection lookupEntities = esq.GetEntityCollection(UserConnection);
			EntitySchemaColumn targetColumn = esq.RootSchema.Columns.FindByName(targetColumnName);
			entities = entities.Where(entity => {
				var lookupId = entity.GetTypedColumnValue<Guid>(lookupColumn.ColumnValueName);
				Entity lookupEntity =
					lookupEntities.FirstOrDefault(lookup => lookup.GetTypedColumnValue<Guid>("Id") == lookupId);
				if (lookupEntity == null) {
					return false;
				}
				var targetColumnValue = lookupEntity.GetTypedColumnValue<string>(targetColumn.ColumnValueName);
				return parameterValues.Any(p => targetColumnValue?.ToLower().Contains(p.ToString().ToLower()) ?? true);
			});
			return entities;
		}

		private IEnumerable<Entity> GetAllIntentEntities(
				IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> managerItems,
				IEnumerable<Entity> entityCollection) {
			var entities = new List<Entity>();
			foreach (ISchemaManagerItem<CopilotIntentSchema> schemaManagerItem in managerItems) {
				Entity itemEntity = entityCollection
					.FirstOrDefault(x => x.GetTypedColumnValue<Guid>("UId") == schemaManagerItem.UId);
				entities.Add(GetIntentEntityFromManagerItem(schemaManagerItem,
					itemEntity));
			}
			return entities;
		}

		private IEnumerable<Entity> GetEntitiesByFiltersCollection(FilterCollection filtersCollection,
				IEnumerable<Entity> entities) {
			if (filtersCollection.Filters.Count == 0) {
				return entities;
			}
			if (filtersCollection.LogicalOperation == LogicalOperationStrict.Or) {
				IEnumerable<Entity> resultEntities = Enumerable.Empty<Entity>();
				foreach (QueryFilterInfo filter in filtersCollection.Filters) {
					if (!(filter is CompareColumnWithValueFilter columnFilter)) {
						continue;
					}
					IEnumerable<Entity> filterredEntities = GetEntitiesBySingleFilter(columnFilter, entities);
					resultEntities = resultEntities.Union(filterredEntities);
				}
				return resultEntities;
			}
			foreach (QueryFilterInfo filter in filtersCollection.Filters) {
				if (filter is FilterCollection filterCollection) {
					entities = GetEntitiesByFiltersCollection(filterCollection, entities);
					continue;
				}
				if (!(filter is CompareColumnWithValueFilter columnFilter)) {
					continue;
				}
				entities = GetEntitiesBySingleFilter(columnFilter, entities);
			}
			return entities;
		}

		private static IEnumerable<Entity> FilterBySubIntent(List<object> parameterValues, IEnumerable<Entity> entities) {
			if (parameterValues == null || parameterValues.Count == 0) {
				return entities;
			}
			return entities.Where(entity => entity.GetTypedColumnValue<CopilotSubIntentMetaItemCollection>("SubIntents")
				.Any(subIntent => parameterValues.Contains(subIntent.UId)));
		}

		private IEnumerable<Entity> GetEntitiesBySingleFilter(CompareColumnWithValueFilter compareFilter,
				IEnumerable<Entity> entities) {
			switch (compareFilter.ColumnPath) {
				case "Name":
				case "Description": {
					entities = entities.Where(entity => compareFilter.ParameterValues.Any(p =>
						entity.GetTypedColumnValue<string>(compareFilter.ColumnPath).ToLower()
							.Contains(p.ToString().ToLower())));
					break;
				}
				case "CreatedBy.Name": {
					entities = entities.Where(entity => compareFilter.ParameterValues.Any(p =>
						entity.GetTypedColumnValue<string>("CreatedByName").ToLower()
							.Contains(p.ToString().ToLower())));
					break;
				}
				case "StatusId": {
					entities = entities.Where(entity =>
						compareFilter.ParameterValues.Contains(entity.GetTypedColumnValue<Guid>("StatusId")));
					break;
				}
				case "Id": {
					GetIntentUIdByFilter(compareFilter, out Guid intentUId);
					if (intentUId.IsNotEmpty()) {
						entities = entities.Where(entity => entity.GetTypedColumnValue<Guid>("Id") == intentUId);
					}
					break;
				}
				case "[CopilotAgentSubSkill:Intent:Id].Id": {
					entities = FilterBySubIntent(compareFilter.ParameterValues, entities);
					break;
				}
				case "Type.Name": {
				 	entities = entities.Where(entity => compareFilter.ParameterValues.Any(p =>
				 		entity.GetTypedColumnValue<string>("TypeName").ToLower()
				 			.Contains(p.ToString().ToLower())));
					break;
				}
			}
			if (GetIsLookupColumnFilter(compareFilter.ColumnPath, out EntitySchemaColumn sourceColumn,
					out string targetSchemaColumnName) && compareFilter.ColumnPath != "CreatedBy.Name") {
				return FilterByLookupColumnReference(compareFilter.ParameterValues,
					sourceColumn,
					targetSchemaColumnName,
					entities);
			}
			return entities;
		}

		private IEnumerable<Entity> GetIntentEntityCollection() {
			var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager,
				"SysSchema") {
				PrimaryQueryColumn = {
					IsAlwaysSelect = true
				},
				UseAdminRights = false
			};
			esq.AddColumn("UId");
			esq.AddColumn("CreatedBy");
			esq.AddColumn("CreatedBy.Name");
			esq.AddColumn("ModifiedOn");
			esq.AddColumn("ManagerName");
			IEntitySchemaQueryFilterItem filter = esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				"ManagerName", "CopilotIntentSchemaManager");
			esq.Filters.Add(filter);
			return esq.GetEntityCollection(UserConnection);
		}

		private IEnumerable<Entity> GetIntentEntityFromManagerItemByFilters(
				IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> managerItems, QueryFilterInfo filterInfo) {
			IEnumerable<Entity> intentEntityCollection = GetIntentEntityCollection();
			if (filterInfo is CompareColumnWithValueFilter compareFilter) {
				GetIntentUIdByFilter(filterInfo, out Guid intentUId);
				if (intentUId.IsNotEmpty()) {
					ISchemaManagerItem<CopilotIntentSchema> schemaManagerItem = managerItems
						.FirstOrDefault(item => item.UId == intentUId);
					if (schemaManagerItem != null) {
						Entity item = intentEntityCollection
							.FirstOrDefault(x => x.GetTypedColumnValue<Guid>("UId") == intentUId);
						return new[] {
							GetIntentEntityFromManagerItem(schemaManagerItem, item)
						};
					}
				}
				IEnumerable<Entity> entities = GetAllIntentEntities(managerItems, intentEntityCollection);
				return GetEntitiesBySingleFilter(compareFilter, entities);
			}
			if (filterInfo is FilterCollection filterCollection) {
				IEnumerable<Entity> entities = GetAllIntentEntities(managerItems, intentEntityCollection);
				return GetEntitiesByFiltersCollection(filterCollection, entities);
			}
			return GetAllIntentEntities(managerItems, intentEntityCollection);
		}

		private Entity GetIntentStatus(string code) {
			return GetCopilotIntentLookupESQ("CopilotIntentStatus").GetEntityCollection(UserConnection)
				.FirstOrDefault(status => status.GetTypedColumnValue<string>("Code") == code);
		}

		private void GetIntentUIdByFilter(QueryFilterInfo filter, out Guid intentUId) {
			GetIsPrimaryColumnValueFilter(filter, out intentUId);
		}

		private bool GetIsLookupColumnFilter(string columnPath, out EntitySchemaColumn sourceColumn,
				out string targetSchemaColumn) {
			string[] columnParts = columnPath.Split(new[] { '.' },
				StringSplitOptions.RemoveEmptyEntries);
			if (columnParts.Length != 2) {
				sourceColumn = null;
				targetSchemaColumn = null;
				return false;
			}
			EntitySchema entitySchema = UserConnection.EntitySchemaManager.GetInstanceByName("CopilotIntent");
			sourceColumn = entitySchema.Columns.FindByName(columnParts.First());
			targetSchemaColumn = columnParts.Last();
			return sourceColumn != null && !sourceColumn.ReferenceSchema.IsVirtual;
		}

		private SortColumn GetSorting(EntitySchemaQuery esq) {
			return esq.Columns.Where(c => c.OrderPosition == 1).Select(c => new SortColumn {
				OrderPosition = c.OrderPosition,
				OrderDirection = c.OrderDirection,
				Name = c.Name
			}).FirstOrDefault();
		}

		#endregion

		#region Methods: Protected

		protected EntitySchemaQuery GetCopilotIntentLookupESQ(string lookupName) {
			var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager,
				lookupName) {
				PrimaryQueryColumn = {
					IsAlwaysSelect = true
				},
				UseAdminRights = false
			};
			esq.AddColumn("Name");
			esq.AddColumn("Code");
			esq.Cache = UserConnection.SessionCache.WithLocalCaching("CopilotIntentStatusCache");
			esq.CacheItemName = $"{lookupName}CacheItem";
			return esq;
		}

		protected virtual Entity GetIntentEntityFromManagerItem(
				ISchemaManagerItem<CopilotIntentSchema> schemaManagerItem, Entity intentEntity) {
			CopilotIntentSchema intentSchema = schemaManagerItem.Instance;
			Entity entity = IntentEntitySchema.CreateEntity(UserConnection);
			entity.LoadColumnValue("Id", intentSchema.UId);
			entity.LoadColumnValue("Code", intentSchema.Name);
			entity.LoadColumnValue("Description", intentSchema.IntentDescription ?? intentSchema.Description);
			entity.LoadColumnValue("Name", intentSchema.Caption.ToString());
			entity.LoadColumnValue("Prompt", intentSchema.Prompt);
			entity.LoadColumnValue("LlmModel", intentSchema.LlmModel);
			if (intentEntity != null) {
				entity.LoadColumnValue("CreatedById", intentEntity.GetTypedColumnValue<Guid>("CreatedById"));
				entity.LoadColumnValue("CreatedByName", intentEntity.GetTypedColumnValue<string>("CreatedBy_Name"));
				DateTime modifiedOn = intentEntity.GetTypedColumnValue<DateTime>("ModifiedOn");
				entity.LoadColumnValue("ModifiedOn", DateTime.SpecifyKind(modifiedOn, DateTimeKind.Utc));
			}
			Entity status = GetIntentStatus(intentSchema.Status.ToString());
			if (status != null) {
				entity.LoadColumnValue("StatusId", status.GetTypedColumnValue<Guid>("Id"));
				entity.LoadColumnValue("StatusName", status.GetTypedColumnValue<string>("Name"));
			}
			if (_intentColorMap.TryGetValue(intentSchema.Type, out (Guid Id, string Color) map)) {
				entity.LoadColumnValue("TypeId", map.Id);
				entity.LoadColumnValue("TypePrimaryColor", map.Color);
				LocalizableString intentTypeCaption = CopilotIntentTypeConstants
					.GetIntentTypeCaption(UserConnection, intentSchema.Type);
				entity.LoadColumnValue("TypeName", intentTypeCaption);
			}
			entity.LoadColumnValue("SubIntents", intentSchema.SubIntents);
			return entity;
		}

		protected virtual IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> GetIntentManagerItems() {
			IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> items = CopilotIntentSchemaManager.GetItems();
			return items;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Returns intent data.
		/// </summary>
		/// <param name="esq">Filters.</param>
		/// <returns>Intent data collection.</returns>
		public EntityCollection GetEntityCollection(EntitySchemaQuery esq) {
			QueryFilterInfo filterInfo = esq.Filters.ParseFilters();
			IEnumerable<ISchemaManagerItem<CopilotIntentSchema>> intentManagerItems = GetIntentManagerItems();
			IEnumerable<Entity> entities = GetIntentEntityFromManagerItemByFilters(intentManagerItems, filterInfo);
			var collection = new EntityCollection(UserConnection, EntitySchema);
			collection.AddRange(entities);
			ApplySorting(esq, collection);
			if (esq.RowCount > 0) {
				IEnumerable<Entity> paged = collection.Skip(esq.SkipRowCount).Take(esq.RowCount);
				var pagedCollection = new EntityCollection(UserConnection, EntitySchema);
				pagedCollection.AddRange(paged);
				return pagedCollection;
			}
			return collection;
		}

		#endregion

	}

	#endregion

}

