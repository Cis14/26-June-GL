namespace Terrasoft.Configuration.ML
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.ServiceModel;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Web;
	using Newtonsoft.Json;
	using Terrasoft.Common;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;
	using Terrasoft.ML.Interfaces;
	using Terrasoft.Web.Common;

	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class MLProcessPredictionService : BaseService
	{

		#region Class: MLProcessElementPredictionResultEqualityComparer

		private class MLProcessElementPredictionResultEqualityComparer : 
			IEqualityComparer<MLProcessElementPredictionResult>
		{
			public bool Equals(MLProcessElementPredictionResult x, MLProcessElementPredictionResult y) {
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
				return x.TypeName == y.TypeName
					&& x.SchemaName == y.SchemaName
					&& x.EntitySchemaUId.Equals(y.EntitySchemaUId);
			}

			public int GetHashCode(MLProcessElementPredictionResult obj) {
				return HashCode.Combine(obj.TypeName, obj.SchemaName, obj.EntitySchemaUId);
			}
		}

		#endregion

		#region Constants: Private

		private const int PredictedOptionsCount = 5;

		#endregion

		#region Fields: Private

		private static readonly HashSet<string> _excludedElementsList = new HashSet<string> {
			"Terrasoft.Core.Process.ProcessSchemaStartSignalEvent",
			"Terrasoft.Core.Process.ProcessSchemaTerminateEvent",
			"Terrasoft.Core.Process.ProcessSchemaStartTimerEvent",
			"Terrasoft.Core.Process.ProcessSchemaStartMessageEvent",
			"Terrasoft.Core.Process.ProcessSchemaStartEvent",
		};

		#endregion

		#region Properties: Private

		private HashSet<Guid> _coreUserTasksUIds;
		private HashSet<Guid> CoreUserTasksUIds => _coreUserTasksUIds ?? (_coreUserTasksUIds = GetCoreUserTasksUIds());

		#endregion

		#region Methods: Private

		private HashSet<Guid> GetCoreUserTasksUIds() {
			var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager.GetInstanceByName("SysProcessUserTask"));
			esq.AddColumn("SysUserTaskSchemaUId");
			var entities = esq.GetEntityCollection(UserConnection);
			var result = new HashSet<Guid>();
			foreach (var entity in entities) {
				result.Add(entity.GetTypedColumnValue<Guid>("SysUserTaskSchemaUId"));
			}
			return result;
		}

		private IEnumerable<MLProcessElementPredictionResult> RemoveUnsupportedUserTaskSchemas(
				IEnumerable<MLProcessElementPredictionResult> predictions) => 
			predictions.Select(prediction => {
				if (prediction.TypeName != "Terrasoft.Core.Process.ProcessSchemaUserTask"
					|| string.IsNullOrWhiteSpace(prediction.SchemaName)) {
					return prediction;
				}
				var userTaskSchema = UserConnection.ProcessUserTaskSchemaManager.FindItemByName(prediction.SchemaName);
				if (userTaskSchema == null || !CoreUserTasksUIds.Contains(userTaskSchema.UId)) {
					prediction.SchemaName = string.Empty;
				}
				return prediction;
			}).Distinct(new MLProcessElementPredictionResultEqualityComparer());

		private List<MLProcessElementPredictionResult> GetElementSuggestions(List<SequencePredictionOutput> results) {
			var elementPredictionResults = new List<MLProcessElementPredictionResult>();
			var predictedElements =
					results.FirstOrDefault()?.PredictedElements ?? Enumerable.Empty<SequencePredictionOutputValue>();
			if (!predictedElements.Any()) {
				return elementPredictionResults;
			}
			foreach (SequencePredictionOutputValue predictedElement in predictedElements.OrderByDescending(e => e.Score)) {
				var deserialized = MLProcessElementValueSerializer.Deserialize(predictedElement.Value);
				if (_excludedElementsList.Contains(deserialized.Value.TypeName)) {
					continue;
				}
				Guid entitySchemaUId = Guid.Empty;
				string caption = string.Empty;
				if (!string.IsNullOrWhiteSpace(deserialized.Value.EntitySchemaName) &&
					MLProcessElementParameterParser.TryGetEntityParameterName(deserialized.Value.SchemaName, out _)) {
					var schema = UserConnection.EntitySchemaManager.FindItemByName(deserialized.Value.EntitySchemaName);
					if (schema != null) {
						entitySchemaUId = schema.UId;
						caption = schema.Caption;
					}
				}
				var processElementPredictionResult = new MLProcessElementPredictionResult(deserialized.Value.TypeName,
					deserialized.Value.SchemaName, entitySchemaUId, caption, predictedElement.Score);
				elementPredictionResults.Add(processElementPredictionResult);
			}
			return elementPredictionResults;
		}

		private string GetEntitySchemaNameFromParameters(MLProcessElement processElement) {
			var parameters = JsonConvert.DeserializeObject<List<ProcessSchemaParameter>>(
				processElement.SerializedParameters);
			var entitySchemaParamValue = MLProcessElementParameterParser.GetElementEntitySchemaUId(
				processElement.SchemaName ?? processElement.TypeName, parameters);
			string entitySchemaName = null;
			if (entitySchemaParamValue.HasValue) {
				entitySchemaName = UserConnection.EntitySchemaManager.FindItemByUId(entitySchemaParamValue.Value)
					?.Name;
			}
			return entitySchemaName;
		}

		private List<Dictionary<string, object>> CreatePredictionData(MLProcessElementPredictionRequest request) {
			List<Dictionary<string, object>> predictionData = new List<Dictionary<string, object>>();
			for (var position = request.Elements.Count - 1; position >= 0; position--) {
				MLProcessElement element = request.Elements[position];
				string entitySchemaName = GetEntitySchemaNameForPredictions(request, element, position);
				if (IsEntitySchemaRequired(element) && entitySchemaName.IsNullOrEmpty()) {
					break;
				}
				string sequenceItemValue = MLProcessElementValueSerializer.Serialize(element.TypeName,
					element.SchemaName, entitySchemaName);
				predictionData.Add(new Dictionary<string, object> {
					{ "Position", position },
					{ "Value", sequenceItemValue }
				});
			}
			return predictionData;
		}

		private string GetEntitySchemaNameForPredictions(MLProcessElementPredictionRequest request,
				MLProcessElement element, int position) {
			string entitySchemaName = GetEntitySchemaNameFromParameters(element);
			if (position != request.Elements.Count - 1) {
				return entitySchemaName;
			}
			if (TryGetEntitySchemaFromExtraParameters(request.PredictionParams,
					out string extraParamsEntitySchemaName)) {
				entitySchemaName = extraParamsEntitySchemaName;
			}
			return entitySchemaName;
		}

		private bool IsEntitySchemaRequired(MLProcessElement element) {
			string elementType = element.SchemaName ?? element.TypeName;
			return MLProcessElementParameterParser.TryGetEntityParameterName(elementType, out _);
		}

		private bool TryGetEntitySchemaFromExtraParameters(MLProcessElementExtraPredictionParams extraPredictionParams,
				out string entitySchemaName) {
			if (extraPredictionParams?.EntitySchemaUId.IsEmpty() != false) {
				entitySchemaName = null;
				return false;
			}
			entitySchemaName = UserConnection.EntitySchemaManager
				.GetItemByUId(extraPredictionParams.EntitySchemaUId).Name;
			return true;
		}

		private bool IsMLConfigured(out string errorMessage) {
			errorMessage = null;
			IMLServiceConfig serviceConfig =
				ClassFactory.Get<IMLServiceConfig>(new ConstructorArgument("userConnection", UserConnection));
			bool hasLicense = serviceConfig.HasLicense();
			if (!hasLicense) {
				errorMessage = "No active ML license";
				return false;
			}
			bool isServiceConfigured = serviceConfig.IsServiceConfigured(
				new Guid(MLConsts.SequencePredictionProblemType));
			if (!isServiceConfigured) {
				errorMessage = "ML service is not configured for sequence prediction";
				return false;
			}
			return true;
		}

		#endregion

		#region Methods: Public

		[OperationContract]
		[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
			RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		[return: MessageParameter(Name = "result")]
		public MLProcessElementPredictionResponse PredictNextElement(MLProcessElementPredictionRequest request) {
			if (!IsMLConfigured(out string errorMessage)) {
				return new MLProcessElementPredictionResponse {
					ResultType = MLProcessElementPredictionResultType.ServiceNotConfigured,
					ErrorMessage = errorMessage
				};
			}
			IMLParametrizedPredictor<SequencePredictionParams, SequencePredictionOutput> predictor =
				ClassFactory.Get<IMLParametrizedPredictor<SequencePredictionParams, SequencePredictionOutput>>(
					MLConsts.SequencePredictionProblemType);
			IMLModelLoader modelLoader = ClassFactory.Get<IMLModelLoader>();
			if (!SysSettings.TryGetValue(UserConnection, "MLProcessElementPredictionModel",
					out object modelId)) {
				return new MLProcessElementPredictionResponse {
					ResultType = MLProcessElementPredictionResultType.ServiceNotConfigured,
					ErrorMessage = "System setting with code 'MLProcessElementPredictionModel' should be set"
				};
			}
			if (!modelLoader.TryLoadModelForPrediction(UserConnection, (Guid)modelId,
					out MLModelConfig model)) {
				return new MLProcessElementPredictionResponse {
					ResultType = MLProcessElementPredictionResultType.ServiceNotConfigured,
					ErrorMessage = $"No active ML model found by Id {modelId}"
				};
			}
			List<Dictionary<string, object>> predictionData = CreatePredictionData(request);
			if (predictionData.IsEmpty()) {
				return new MLProcessElementPredictionResponse {
					ResultType = MLProcessElementPredictionResultType.NotEnoughValidInputData,
					ErrorMessage = "No data for prediction"
				};
			}
			List<SequencePredictionOutput> results = predictor.Predict(model, predictionData,
				new SequencePredictionParams {
					PossibleContinuationsCount = PredictedOptionsCount + _excludedElementsList.Count
				});
			IEnumerable<MLProcessElementPredictionResult> elementPredictionResults = GetElementSuggestions(results);
			elementPredictionResults = RemoveUnsupportedUserTaskSchemas(elementPredictionResults);
			return new MLProcessElementPredictionResponse {
				Predictions = elementPredictionResults
					.Take(PredictedOptionsCount)
					.ToList()
			};
		}

		#endregion

	}
}
