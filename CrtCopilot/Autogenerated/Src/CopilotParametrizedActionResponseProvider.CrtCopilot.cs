namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.Copilot.Metadata;
	using Creatio.FeatureToggling;
	using Common.Logging;
	using Newtonsoft.Json;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: CopilotParametrizedActionResponseProvider

	/// <inheritdoc />
	[DefaultBinding(typeof(ICopilotParametrizedActionResponseProvider))]
	public class CopilotParametrizedActionResponseProvider: ICopilotParametrizedActionResponseProvider
	{

		#region Class: UseExtendedLogging

		/// <summary>
		/// Indicates whether to enable extended diagnostic logging for parameter processing.
		/// </summary>
		/// <inheritdoc cref="Creatio.FeatureToggling.FeatureMetadata" />
		public class UseExtendedLogging : FeatureMetadata
		{

			#region Constructors: Public

			/// <summary>
			/// Initializes a new instance of the <see cref="UseExtendedLogging" /> type.
			/// </summary>
			public UseExtendedLogging() {
				IsEnabled = false;
			}

			#endregion

		}

		#endregion

		#region Class: ParameterStructure

		private class ParameterStructure
		{

			#region Class: ParameterStructureJsonConverter

			public class ParameterStructureJsonConverter : JsonConverter<ParameterStructure>
			{

				#region Fields: Private

				private readonly bool _doNotOptimizeStructure;

				#endregion

				#region Constructors: Public

				public ParameterStructureJsonConverter(bool doNotOptimizeStructure) {
					_doNotOptimizeStructure = doNotOptimizeStructure;
				}

				#endregion

				#region Methods: Public

				public override void WriteJson(JsonWriter writer, ParameterStructure value,
						JsonSerializer serializer) {
					if (value == null) {
						return;
					}
					writer.WriteStartObject();
					writer.WritePropertyName(nameof(value.Type));
					writer.WriteValue(value.Type);
					writer.WritePropertyName(nameof(value.Description));
					writer.WriteValue(value.Description);
					var shouldSerializeItemProperties = _doNotOptimizeStructure || !value.IsNullOrEmpty;
					if (shouldSerializeItemProperties && value.ItemProperties != null) {
						writer.WritePropertyName(nameof(value.ItemProperties));
						serializer.Serialize(writer, value.ItemProperties);
					}
					if (value.DataValueType.UId == DataValueType.LookupDataValueTypeUId) {
						writer.WritePropertyName(nameof(value.EntitySchemaName));
						writer.WriteValue(value.EntitySchemaName);
					}
					writer.WriteEndObject();
				}

				public override ParameterStructure ReadJson(JsonReader reader, Type objectType,
						ParameterStructure existingValue, bool hasExistingValue, JsonSerializer serializer) {
					throw new NotImplementedException();
				}

				#endregion

			}

			#endregion

			#region Fields: Private

			private readonly ICopilotParameterMetaInfo _parameter;

			#endregion

			#region Constructors: Public

			public ParameterStructure(ICopilotParameterMetaInfo parameter) {
				_parameter = parameter;
			}

			#endregion

			#region Properties: Public

			public string Type => _parameter.DataValueType.Name;

			public string Description => _parameter.Description?.Value;

			public Dictionary<string, ParameterStructure> ItemProperties { get; set; }

			public ICopilotParameterMetaInfo Parameter => _parameter;

			public DataValueType DataValueType => _parameter.DataValueType;

			public string Name => _parameter.Name;

			public Guid ReferenceSchemaUId => _parameter.ReferenceSchemaUId;

			public string EntitySchemaName { get; set; }

			public bool IsNullOrEmpty { get; set; }

			#endregion

		}

		#endregion

		#region Class: ParameterProcessingContext

		private class ParameterProcessingContext
		{

			#region Properties: Public

			public Dictionary<EntitySchemaUIdRecordIdKey, List<ParameterValue>> EntityRecordIdsToLoad {
				get;
				private set;
			}

			public Dictionary<string, ParameterStructure> ParameterStructures { get; private set; }

			public Dictionary<Guid, EntitySchema> EntitySchemasCache { get; private set; }

			public UserConnection UserConnection { get; }

			public Func<ICopilotParameterMetaInfo, object> ParameterValueGetter { get; }

			public Dictionary<string, ParameterValue> ResultValues { get; }

			#endregion

			#region Constructors: Public

			public ParameterProcessingContext(UserConnection userConnection,
					Func<ICopilotParameterMetaInfo, object> parameterValueGetter) {
				ParameterValueGetter = parameterValueGetter;
				var equalityComparer = new EntitySchemaUIdRecordIdKey.EntitySchemaUIdRecordIdKeyEqualityComparer();
				EntityRecordIdsToLoad =
					new Dictionary<EntitySchemaUIdRecordIdKey, List<ParameterValue>>(equalityComparer);
				ParameterStructures = new Dictionary<string, ParameterStructure>();
				ResultValues = new Dictionary<string, ParameterValue>();
				EntitySchemasCache = new Dictionary<Guid, EntitySchema>();
				UserConnection = userConnection;
			}

			#endregion

			#region Methods: Public

			public string Serialize() {
				if (ParameterStructures.IsNullOrEmpty() && ResultValues.IsNullOrEmpty()) {
					return string.Empty;
				}
				var featureState = Features.GetIsEnabled("GetAIFeatures.DoNotOptimizeCopilotParametersStructure");
				var settings = new JsonSerializerSettings {
					Converters = new List<JsonConverter> {
						new ParameterStructure.ParameterStructureJsonConverter(featureState),
						new ParameterValue.ParameterValueJsonConverter()
					},
					NullValueHandling = NullValueHandling.Ignore
				};
				var resultObject = new {
					Structure = ParameterStructures,
					Values = ResultValues
				};
				return JsonConvert.SerializeObject(resultObject, Formatting.None, settings);
			}

			public ParameterProcessingContext CreateDerivedContext(Dictionary<string, ParameterStructure> parameters,
					Func<ICopilotParameterMetaInfo, object> parameterValueGetter) {
				return new ParameterProcessingContext(UserConnection, parameterValueGetter) {
					ParameterStructures = parameters,
					EntityRecordIdsToLoad = EntityRecordIdsToLoad,
					EntitySchemasCache = EntitySchemasCache
				};
			}

			public void SetParameterValue(ParameterStructure parameter, object value, bool isNullOrEmpty = false) {
				ResultValues[parameter.Name] = new ParameterValue(parameter, value);
				parameter.IsNullOrEmpty = isNullOrEmpty;
			}

			#endregion

		}

		#endregion

		#region Class: ParameterValue

		private class ParameterValue
		{

			#region Class: ParameterValueJsonConverter

			internal class ParameterValueJsonConverter : JsonConverter<ParameterValue>
			{

				#region Methods: Public

				public override void WriteJson(JsonWriter writer, ParameterValue value, JsonSerializer serializer) {
					if (value == null) {
						return;
					}
					if (value._parameter.DataValueType.UId == DataValueType.LookupDataValueTypeUId) {
						writer.WriteStartObject();
						writer.WritePropertyName(nameof(value.Value));
						writer.WriteValue(value.Value);
						writer.WritePropertyName(nameof(value.DisplayValue));
						writer.WriteValue(value.DisplayValue);
						writer.WriteEndObject();
						return;
					}
					serializer.Serialize(writer, value.Value);
				}

				public override ParameterValue ReadJson(JsonReader reader, Type objectType,
						ParameterValue existingValue, bool hasExistingValue, JsonSerializer serializer) {
					throw new NotImplementedException();
				}

				#endregion

			}

			#endregion

			#region Fields: Private

			private readonly ParameterStructure _parameter;

			#endregion

			#region Properties: Public

			public object Value { get; }

			public string DisplayValue { get; set; }

			#endregion

			#region Constructors: Public

			public ParameterValue(ParameterStructure parameter, object value) {
				_parameter = parameter;
				Value = value;
				DisplayValue = string.Empty;
			}

			#endregion

		}

		#endregion

		#region Class: EntitySchemaUIdRecordIdKey

		private class EntitySchemaUIdRecordIdKey
		{

			#region Class: EntitySchemaUIdRecordIdPairEqualityComparer

			public class EntitySchemaUIdRecordIdKeyEqualityComparer: IEqualityComparer<EntitySchemaUIdRecordIdKey>
			{

				#region Methods: Public

				public bool Equals(EntitySchemaUIdRecordIdKey x, EntitySchemaUIdRecordIdKey y) {
					if (ReferenceEquals(x, y)) {
						return true;
					}
					if (ReferenceEquals(x, null)) {
						return false;
					}
					if (ReferenceEquals(y, null)) {
						return false;
					}
					if (x.GetType() != y.GetType()) {
						return false;
					}
					return x.EntitySchemaUId.Equals(y.EntitySchemaUId) && x.RecordId.Equals(y.RecordId);
				}

				public int GetHashCode(EntitySchemaUIdRecordIdKey obj) {
					unchecked {
						return (obj.EntitySchemaUId.GetHashCode() * 397) ^ obj.RecordId.GetHashCode();
					}
				}

				#endregion

			}

			#endregion

			#region Properties: Public

			public Guid EntitySchemaUId { get; }

			public Guid RecordId { get; }

			#endregion

			#region Constructors: Public

			public EntitySchemaUIdRecordIdKey(Guid entitySchemaUId, Guid recordId) {
				EntitySchemaUId = entitySchemaUId;
				RecordId = recordId;
			}

			#endregion

		}

		#endregion

		#region Constants: Private

		private const int EntityRecordsChunkSize = 900;

		#endregion
		
		#region Fields: Private

		private static readonly Guid[] _compositeParameterDataValueTypeUIds = new Guid[] {
			DataValueType.CompositeObjectListDataValueTypeUId,
			DataValueType.CompositeObjectDataValueTypeUId,
			DataValueType.ObjectListDataValueTypeUId
		};

		#endregion

		#region Properties: Private

		private static bool EnableExtendedLogging => Features.GetIsEnabled<UseExtendedLogging>();
		private static ILog Logger => LogManager.GetLogger("Copilot");

		#endregion

		#region Methods: Private

		private static void LogInfo(string message) {
			if (EnableExtendedLogging) {
				Logger.Info(message);
			}
		}

		private static void LogWarning(string message) {
			if (EnableExtendedLogging) {
				Logger.Warn(message);
			}
		}

		private static void LogError(string message, Exception exception = null) {
			if (exception != null) {
				Logger.Error(message, exception);
			} else {
				Logger.Error(message);
			}
		}

		private static bool GetShouldSkipParameter(ICopilotParameterMetaInfo parameter) {
			return parameter.IsInternal || parameter.Direction == ParameterDirection.Input;
		}

		private static bool GetIsEmptyCompositeParameter(ICopilotParameterMetaInfo parameter) {
			return _compositeParameterDataValueTypeUIds.Contains(parameter.DataValueType.UId) &&
				parameter.ItemProperties.IsNullOrEmpty();
		}

		private static ParameterStructure CreateParameterStructure(UserConnection userConnection,
				ICopilotParameterMetaInfo parameter, Dictionary<Guid, EntitySchema> entitySchemasCache,
				bool isItemProperties = false) {
			if (GetIsEmptyCompositeParameter(parameter) || (!isItemProperties && GetShouldSkipParameter(parameter))) {
				return null;
			}
			var parameterStructure = new ParameterStructure(parameter);
			if (parameter.DataValueType.UId == DataValueType.LookupDataValueTypeUId &&
					parameter.ReferenceSchemaUId.IsNotEmpty()) {
				if (!entitySchemasCache.TryGetValue(parameter.ReferenceSchemaUId, out EntitySchema entitySchema)) {
					entitySchema = userConnection.EntitySchemaManager.GetInstanceByUId(parameter.ReferenceSchemaUId);
					entitySchemasCache.Add(parameter.ReferenceSchemaUId, entitySchema);
				}
				parameterStructure.EntitySchemaName = entitySchema.Name;
			}
			if (parameter.ItemProperties.IsEmpty()) {
				return parameterStructure;
			}
			parameterStructure.ItemProperties = new Dictionary<string, ParameterStructure>();
			FillParametersStructure(userConnection, parameter.ItemProperties, parameterStructure.ItemProperties,
				entitySchemasCache, true);
			return parameterStructure;
		}

		private static void FillParametersStructure(UserConnection userConnection,
				IEnumerable<ICopilotParameterMetaInfo> parameters,
				Dictionary<string, ParameterStructure> parameterStructures,
				Dictionary<Guid, EntitySchema> entitySchemasCache,
				bool isNestedCollection = false) {
			var structures = parameters.Where(w =>
					isNestedCollection || !GetShouldSkipParameter(w))
				.ToDictionary(k => k.Name, v =>
					CreateParameterStructure(userConnection, v, entitySchemasCache, isNestedCollection));
			parameterStructures.AddRange(structures);
		}

		private static void FillStructure(ParameterProcessingContext parameterProcessingContext,
				IReadOnlyList<ICopilotParameterMetaInfo> parameters) {
			FillParametersStructure(parameterProcessingContext.UserConnection, parameters,
				parameterProcessingContext.ParameterStructures, parameterProcessingContext.EntitySchemasCache);
		}

		private static void ProcessLookupParameter(ParameterProcessingContext parameterProcessingContext,
				object value, ParameterStructure parameter) {
			LogInfo($"Processing Lookup parameter '{parameter.Name}'");
			if (!(value is Guid guidValue) || guidValue.IsEmpty()) {
				parameterProcessingContext.SetParameterValue(parameter, null);
				return;
			}
			var parameterValue = new ParameterValue(parameter, guidValue);
			var pair = new EntitySchemaUIdRecordIdKey(parameter.ReferenceSchemaUId, guidValue);
			var entityRecordIdsToLoad = parameterProcessingContext.EntityRecordIdsToLoad;
			if (entityRecordIdsToLoad.TryGetValue(pair, out var existingValue)) {
				existingValue.Add(parameterValue);
			} else {
				var newValue = new List<ParameterValue> { parameterValue };
				entityRecordIdsToLoad.Add(pair, newValue);
			}
			parameterProcessingContext.ResultValues[parameter.Name] = parameterValue;
		}

		private static void LoadLookupDisplayValues(ParameterProcessingContext parameterProcessingContext) {
			var entityRecordIdToParametersMap = parameterProcessingContext.EntityRecordIdsToLoad;
			var userConnection = parameterProcessingContext.UserConnection;
			var entityGroups = entityRecordIdToParametersMap.GroupBy(e => e.Key.EntitySchemaUId);
			foreach (var entityGroup in entityGroups) {
				var entitySchema = parameterProcessingContext.EntitySchemasCache[entityGroup.Key];
				foreach (var entityChunk in entityGroup.SplitOnChunks(EntityRecordsChunkSize)) {
					var entityChunkDictionary = entityChunk.ToDictionary(k => k.Key.RecordId, v => v.Value);
					LoadEntityLookupDisplayValues(userConnection, entitySchema, entityChunkDictionary);
				}
			}
		}

		private static void LoadEntityLookupDisplayValues(UserConnection userConnection, EntitySchema entitySchema,
				Dictionary<Guid, List<ParameterValue>> entityRecords) {
			if (entitySchema.PrimaryDisplayColumn == null) {
				return;
			}
			var esq = new EntitySchemaQuery(entitySchema);
			string primaryColumnName = entitySchema.PrimaryColumn.Name;
			string displayColumnName = entitySchema.PrimaryDisplayColumn.Name;
			esq.PrimaryQueryColumn.IsAlwaysSelect = true;
			esq.AddColumn(displayColumnName);
			EntitySchemaQueryFilterCollection filters = esq.Filters;
			IEntitySchemaQueryFilterItem filter = esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				primaryColumnName, entityRecords.Keys.ToArray().Cast<object>());
			filters.Add(filter);
			EntityCollection entities = esq.GetEntityCollection(userConnection);
			foreach (Entity entity in entities) {
				var primaryColumnValue = entity.GetTypedColumnValue<Guid>(primaryColumnName);
				var parameterValues = entityRecords[primaryColumnValue];
				parameterValues.ForEach(parameter =>
					parameter.DisplayValue = entity.GetTypedColumnValue<string>(displayColumnName) ?? string.Empty);
			}
		}

		private static object GetValueFromCompositeObject(ICopilotParameterMetaInfo info,
				ICompositeObject compositeObject) {
			return compositeObject.TryGetValue(info.Name, out object value) ? value : null;
		}

		private static void ProcessCompositeObjectListParameter(ParameterProcessingContext parameterProcessingContext,
				object value, ParameterStructure parameter) {
			LogInfo($"ProcessCompositeObjectListParameter started for parameter '{parameter?.Name}'");
			var compositeObjectListResultValues = new List<Dictionary<string, ParameterValue>>();
			var isEmpty = value is ICompositeObjectList<ICompositeObject> compositeObjectList &&
				compositeObjectList.IsEmpty();
			if (value == null || isEmpty) {
				LogInfo($"Parameter '{parameter?.Name}' is null or empty. IsNull={value == null}, IsEmpty={isEmpty}");
				var resultValues = isEmpty ? compositeObjectListResultValues : null;
				parameterProcessingContext.SetParameterValue(parameter, resultValues, true);
				return;
			}
			compositeObjectList = (ICompositeObjectList<ICompositeObject>)value;
			LogInfo($"Processing CompositeObjectList '{parameter?.Name}' with {compositeObjectList.Count} items. " +
				$"ItemProperties IsNull={parameter?.ItemProperties == null}");
			int itemIndex = 0;
			foreach (ICompositeObject compositeObject in compositeObjectList) {
				itemIndex++;
				if (compositeObject == null) {
					LogWarning($"Skipping null compositeObject at index {itemIndex} in parameter '{parameter?.Name}'");
					continue;
				}
				LogInfo($"Processing item {itemIndex} of '{parameter?.Name}'");
				if (parameter?.ItemProperties == null) {
					LogError($"ItemProperties is NULL for parameter '{parameter?.Name}' at item index {itemIndex}");
					throw new InvalidOperationException($"ItemProperties is null for parameter '{parameter?.Name}' " +
						"when processing CompositeObjectList");
				}
				var processingContext = parameterProcessingContext.CreateDerivedContext(parameter.ItemProperties,
					info => GetValueFromCompositeObject(info, compositeObject));
				ProcessParameters(processingContext);
				compositeObjectListResultValues.Add(processingContext.ResultValues);
				LogInfo($"Successfully processed item {itemIndex} of '{parameter?.Name}'");
			}
			LogInfo($"ProcessCompositeObjectListParameter completed for parameter '{parameter?.Name}' with " +
				$"{compositeObjectListResultValues.Count} results");
			parameterProcessingContext.SetParameterValue(parameter, compositeObjectListResultValues);
		}

		private static void ProcessObjectListParameter(ParameterProcessingContext parameterProcessingContext,
				object value, ParameterStructure parameter) {
			LogInfo($"ProcessObjectListParameter started for parameter '{parameter?.Name}'");
			var isEmpty = value is IObjectList objectList && objectList.IsEmpty();
			var objectListResultValues = new List<ParameterValue>();
			if (value == null || isEmpty) {
				LogInfo($"Parameter '{parameter?.Name}' is null or empty. IsNull={value == null}, IsEmpty={isEmpty}");
				var resultValues = isEmpty ? objectListResultValues : null;
				parameterProcessingContext.SetParameterValue(parameter, resultValues, true);
				return;
			}
			objectList = (IObjectList)value;
			LogInfo($"Processing ObjectList '{parameter?.Name}' with {objectList.Count} items. ItemProperties " +
				$"IsNull={parameter?.ItemProperties == null}");
			int itemIndex = 0;
			foreach (object objectValue in objectList) {
				itemIndex++;
				LogInfo($"Processing item {itemIndex} of '{parameter?.Name}'");
				if (parameter?.ItemProperties == null) {
					LogError($"ItemProperties is NULL for parameter '{parameter?.Name}' at item index {itemIndex}");
					throw new InvalidOperationException($"ItemProperties is null for parameter '{parameter?.Name}' " +
						"when processing ObjectList");
				}
				var processingContext = parameterProcessingContext.CreateDerivedContext(parameter.ItemProperties,
					_ => objectValue);
				ProcessParameters(processingContext);
				objectListResultValues.Add(processingContext.ResultValues.Values.First());
				LogInfo($"Successfully processed item {itemIndex} of '{parameter?.Name}'");
			}
			LogInfo($"ProcessObjectListParameter completed for parameter '{parameter?.Name}' " +
				$"with {objectListResultValues.Count} results");
			parameterProcessingContext.SetParameterValue(parameter, objectListResultValues, false);
		}

		private static void ProcessCompositeObjectParameter(ParameterProcessingContext parameterProcessingContext,
				object value, ParameterStructure parameter) {
			LogInfo($"ProcessCompositeObjectParameter started for parameter '{parameter?.Name}'. " +
				$"ItemProperties IsNull={parameter?.ItemProperties == null}");
			if (parameter?.ItemProperties == null) {
				LogError($"ItemProperties is NULL for parameter '{parameter?.Name}' when processing CompositeObject");
				throw new InvalidOperationException($"ItemProperties is null for parameter '{parameter?.Name}' " +
					"when processing CompositeObject");
			}
			var compositeObject = (ICompositeObject)value;
			var processingContext = parameterProcessingContext.CreateDerivedContext(parameter.ItemProperties,
				info => GetValueFromCompositeObject(info, compositeObject));
			ProcessParameters(processingContext);
			parameterProcessingContext.SetParameterValue(parameter, processingContext.ResultValues);
			LogInfo($"ProcessCompositeObjectParameter completed for parameter '{parameter?.Name}'");
		}

		private static object GetParameterValue(ParameterProcessingContext parameterProcessingContext,
				ICopilotParameterMetaInfo parameter) {
			try {
				return parameterProcessingContext.ParameterValueGetter(parameter);
			} catch (Exception e) {
				var message = $"Error retrieving parameter {parameter.Name} value.";
				throw new ApplicationException(message, e);
			}
		}

		private static void ProcessParameters(ParameterProcessingContext parameterProcessingContext) {
			foreach (var parameterNameStructurePair in parameterProcessingContext.ParameterStructures) {
				ParameterStructure structure = parameterNameStructurePair.Value;
				try {
					LogInfo($"Processing parameter: Name='{structure?.Name}', Type='{structure?.DataValueType?.Name}'" +
						$", DataValueTypeUId='{structure?.DataValueType?.UId}'");
					if (structure == null) {
						LogError($"ParameterStructure is null for key '{parameterNameStructurePair.Key}'");
						continue;
					}
					object value = GetParameterValue(parameterProcessingContext, structure.Parameter);
					LogInfo($"Parameter '{structure.Name}' value retrieved: IsNull={value == null}, " +
						$"ValueType={value?.GetType()?.Name ?? "null"}");
					if (structure.DataValueType.UId == DataValueType.CompositeObjectDataValueTypeUId && value != null) {
						ProcessCompositeObjectParameter(parameterProcessingContext, value, structure);
					} else if (structure.DataValueType.UId == DataValueType.ObjectListDataValueTypeUId) {
						ProcessObjectListParameter(parameterProcessingContext, value, structure);
					} else if (structure.DataValueType.UId == DataValueType.CompositeObjectListDataValueTypeUId) {
						ProcessCompositeObjectListParameter(parameterProcessingContext, value, structure);
					} else if (structure.DataValueType.UId == DataValueType.LookupDataValueTypeUId) {
						ProcessLookupParameter(parameterProcessingContext, value, structure);
					} else {
						LogInfo($"Processing simple parameter '{structure.Name}'");
						parameterProcessingContext.SetParameterValue(structure, value);
					}
					LogInfo($"Successfully processed parameter '{structure.Name}'");
				} catch (Exception ex) {
					LogError($"Error processing parameter: Name='{structure?.Name}', " +
						$"Key='{parameterNameStructurePair.Key}', Error: {ex.Message}", ex);
					throw;
				}
			}
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public string GetParameterizedResponse(UserConnection userConnection, CopilotActionDescriptor actionDescriptor,
				Func<ICopilotParameterMetaInfo, object> parameterValueGetter) {
			return GetParameterizedResponse(userConnection, actionDescriptor.Parameters, parameterValueGetter);
		}

		/// <inheritdoc />
		public string GetParameterizedResponse(UserConnection userConnection, 
				IReadOnlyList<ICopilotParameterMetaInfo> parameters,
				Func<ICopilotParameterMetaInfo, object> parameterValueGetter) {
			var parameterProcessingContext = new ParameterProcessingContext(userConnection, parameterValueGetter);
			FillStructure(parameterProcessingContext, parameters);
			ProcessParameters(parameterProcessingContext);
			LoadLookupDisplayValues(parameterProcessingContext);
			return parameterProcessingContext.Serialize();
		}

		#endregion

	}

	#endregion

}

