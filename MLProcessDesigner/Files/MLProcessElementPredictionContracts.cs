namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Runtime.Serialization;

	[DataContract]
	[DebuggerDisplay("ElementId = {ElementId}; TypeName = {TypeName}; SchemaName = {SchemaName}")]
	public class MLProcessElement
	{
		[DataMember(Name = "elementId")]
		public Guid ElementId { get; set; }
		
		[DataMember(Name = "typeName")]
		public string TypeName { get; set; }
		
		[DataMember(Name = "schemaName")]
		public string SchemaName { get; set; }
		
		[DataMember(Name = "serializedParameters")]
		public string SerializedParameters { get; set; }
	}

	[DataContract]
	public class MLProcessElementPredictionRequest
	{
		[DataMember(Name = "elements")]
		public List<MLProcessElement> Elements { get; set; } = new List<MLProcessElement>();

		[DataMember(Name = "predictionParams")]
		public MLProcessElementExtraPredictionParams PredictionParams { get; set; }
	}

	[DataContract]
	public class MLProcessElementExtraPredictionParams
	{

		[DataMember(Name = "entitySchemaUId")]
		public Guid EntitySchemaUId { get; set; }
	}

	[DataContract]
	[DebuggerDisplay("TypeName = {TypeName}; SchemaName = {SchemaName}")]
	public class MLProcessElementPredictionResult
	{

		#region Constructors: Public

		public MLProcessElementPredictionResult() {
		}

		public MLProcessElementPredictionResult(string typeName, string schemaName, Guid entitySchemaUId,
				string entitySchemaCaption, double score) {
			TypeName = typeName;
			SchemaName = schemaName;
			EntitySchemaUId = entitySchemaUId;
			EntitySchemaCaption = entitySchemaCaption;
			Score = score;
		}

		#endregion

		#region Properties: Public

		[DataMember(Name = "typeName")]
		public string TypeName { get; set; }

		[DataMember(Name = "schemaName")]
		public string SchemaName { get; set; }

		[DataMember(Name = "entitySchemaUId", EmitDefaultValue = false)]
		public Guid EntitySchemaUId { get; set; }

		[DataMember(Name = "entitySchemaCaption", EmitDefaultValue = false)]
		public string EntitySchemaCaption { get; set; }

		[DataMember(Name = "score", EmitDefaultValue = false)]
		public double Score { get; set; }

		#endregion

	}

	[DataContract]
	public class MLProcessElementPredictionResponse
	{
		[DataMember(Name = "predictions")]
		public List<MLProcessElementPredictionResult> Predictions { get; set; }

		[DataMember(Name = "errorMessage")]
		public string ErrorMessage { get; set; }

		[DataMember(Name = "resultType")]
		public string ResultType { get; set; } = MLProcessElementPredictionResultType.Success;
	}

	public static class MLProcessElementPredictionResultType
	{
		public static string Success { get; set; } = nameof(Success);
		public static string ServiceNotConfigured { get; set; } = nameof(ServiceNotConfigured);
		public static string NotEnoughValidInputData { get; set; } = nameof(NotEnoughValidInputData);
		public static string Error { get; set; } = nameof(Error);
	}
}
