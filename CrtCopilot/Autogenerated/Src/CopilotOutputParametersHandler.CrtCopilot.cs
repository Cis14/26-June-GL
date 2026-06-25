namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using Creatio.Copilot.Metadata;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using System.Globalization;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using Terrasoft.Common;
	using Terrasoft.Configuration.Enrichment;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;

	#region Interface: ICopilotOutputParametersHandler

	/// <summary>
	/// Copilot output parameters handler.
	/// </summary>
	public interface ICopilotOutputParametersHandler
	{

		#region Methods: Public

		/// <summary>
		/// Handles the output parameters by parsing them according to their types defined in the intent schema.
		/// </summary>
		/// <param name="intentExecutionResult">The dictionary containing the intent execution result.</param>
		/// <param name="intentOutputParameters">The collection of intent schema parameters that define the expected types of the output parameters.</param>
		/// <returns>A dictionary containing the parsed output parameters with their respective types.</returns>
		Dictionary<string, object> HandleOutputParameters(Dictionary<string, JToken> intentExecutionResult,
			CopilotIntentSchemaParameterCollection intentOutputParameters);

		/// <summary>
		/// Gets the JSON schema for the output parameters based on the copilot intent schema.
		/// </summary>
		/// <param name="intent">The copilot intent schema containing the parameter definitions.</param>
		/// <returns>A JSON schema object that describes the structure and types of the output parameters.</returns>
		JsonSchema GetOutputParametersJsonSchema(CopilotIntentSchema intent);

		#endregion

	}

	#endregion

	#region Class: CopilotOutputParametersHandler

	/// <summary>
	/// Copilot output parameters handler.
	/// </summary>
	/// <inheritdoc cref="Creatio.Copilot.ICopilotOutputParametersHandler"/>
	[DefaultBinding(typeof(ICopilotOutputParametersHandler))]
	internal class CopilotOutputParametersHandler : ICopilotOutputParametersHandler
	{

		#region Constants: Private

		private const string OutputParametersPropertyName = "OutputParameters";

		private const string DefaultParameterName = "content";
		private const string DefaultParameterDescription = "main response content";

		#endregion

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private readonly IJsonSchemaManager _jsonSchemaManager;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="CopilotOutputParametersHandler"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="jsonSchemaManager"></param>
		public CopilotOutputParametersHandler(UserConnection userConnection, IJsonSchemaManager jsonSchemaManager) {
			_userConnection = userConnection;
			_jsonSchemaManager = jsonSchemaManager;
		}

		#endregion

		#region Methods: Private

		private object ParseValueByType(string outputParameter, Type type) {
			switch (type) {
				case Type t when t == typeof(int):
					return int.Parse(outputParameter, CultureInfo.InvariantCulture);
				case Type t when t == typeof(double):
					return double.Parse(outputParameter, NumberStyles.Float, CultureInfo.InvariantCulture);
				case Type t when t == typeof(float):
					return float.Parse(outputParameter, NumberStyles.Float, CultureInfo.InvariantCulture);
				case Type t when t == typeof(decimal):
					return decimal.Parse(outputParameter, NumberStyles.Float, CultureInfo.InvariantCulture);
				case Type t when t == typeof(DateTime):
					DateTime dateTime = DateTime.Parse(outputParameter, CultureInfo.InvariantCulture,
						DateTimeStyles.RoundtripKind);
					return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
				case Type t when t == typeof(bool):
					return bool.Parse(outputParameter);
				case Type t when t == typeof(Guid):
					return Guid.Parse(outputParameter);
				case Type t when t == typeof(string):
					return outputParameter;
				default:
					throw new NotSupportedException($"Type {type} is not supported.");
			}
		}

		private JsonSchema GenerateJsonSchemaFromParameters(CopilotIntentSchema intent) {
			CopilotIntentSchemaParameterCollection outputParameters = intent.OutputParameters;
			var propsDictionary = new Dictionary<string, PropertyDefinition>();
			var propsReqList = new List<string>();
			var converter = new CopilotIntentSchemaParameterConverter(_userConnection);
			foreach (CopilotIntentSchemaParameter parameter in outputParameters) {
				ICopilotParameterMetaInfo metaParam = converter.Convert(parameter);
				PropertyDefinition propDef = CopilotToolParamHelper.GetToolParam(metaParam);
				if (!metaParam.IsRequired) {
					propDef.TypeList.Add("null");
				}

				propsDictionary.Add(metaParam.Name, propDef);
				propsReqList.Add(metaParam.Name);
			}

			if (propsDictionary.Count == 0) {
				PropertyDefinition propDef = PropertyDefinition.DefineString(DefaultParameterDescription);
				propDef.Title = DefaultParameterName;
				propsDictionary.Add(propDef.Title, propDef);
				propsReqList.Add(propDef.Title);
			}

			var propDefAll = PropertyDefinition.DefineObject(propsDictionary, propsReqList, null);
			return new JsonSchema {
				Name = OutputParametersPropertyName,
				Strict = true,
				Schema = propDefAll
			};
		}

		private static bool IsResponseFormatJsonSchemaSet(CopilotIntentSchema intent) {
			return !string.IsNullOrWhiteSpace(intent.ResponseFormatJsonSchema.PackageName) &&
			       !string.IsNullOrWhiteSpace(intent.ResponseFormatJsonSchema.SchemaName);
		}

		private JsonSchema GenerateJsonSchemaFromFile(string packageName, string schemaName) {
			PropertyDefinition propertyDefinition;
			string jsonSchema;
			try {
				jsonSchema = _jsonSchemaManager.ReadSchema(packageName, schemaName);
			} catch (Exception e) {
				throw new InvalidOperationException(
					$"Failed to read JSON schema in package {packageName} with name {schemaName}:\n{e.Message}", e);
			}

			if (string.IsNullOrWhiteSpace(jsonSchema)) {
				throw new InvalidOperationException(
					$"JSON schema in package {packageName} with name {schemaName} is empty or null.");
			}

			try {
				var jsonSerializerSettings = new JsonSerializerSettings {
					ContractResolver = new PropertyDefinitionContractResolver()
				};
				propertyDefinition = JsonConvert.DeserializeObject<PropertyDefinition>(jsonSchema,
					jsonSerializerSettings);
			} catch (Exception e) {
				throw new InvalidOperationException(
					$"Invalid JSON schema format of schema {schemaName} in package {packageName}:\n{e.Message}", e);
			}

			return new JsonSchema {
				Name = OutputParametersPropertyName,
				Strict = true,
				Schema = propertyDefinition
			};
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public Dictionary<string, object> HandleOutputParameters(Dictionary<string, JToken> intentExecutionResult,
			CopilotIntentSchemaParameterCollection intentOutputParameters) {
			var resultParameters = new Dictionary<string, object>();
			DataValueTypeManager typeManager = _userConnection.DataValueTypeManager;
			foreach (CopilotIntentSchemaParameter parameter in intentOutputParameters) {
				Type type = typeManager.FindInstanceByUId(parameter.DataValueTypeUId)?.ValueType;
				if (type != null) {
					resultParameters[parameter.Name] = ParseValueByType(intentExecutionResult[parameter.Name]?.ToString(), type);
				} else {
					resultParameters[parameter.Name] = intentExecutionResult[parameter.Name]?.ToObject<object>();
				}
			}

			return resultParameters;
		}

		/// <inheritdoc/>
		public JsonSchema GetOutputParametersJsonSchema(CopilotIntentSchema intent) {
			intent.CheckArgumentNull(nameof(intent));
			return IsResponseFormatJsonSchemaSet(intent)
				? GenerateJsonSchemaFromFile(intent.ResponseFormatJsonSchema.PackageName,
					intent.ResponseFormatJsonSchema.SchemaName)
				: GenerateJsonSchemaFromParameters(intent);
		}

		#endregion

	}

	#endregion

}

