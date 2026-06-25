namespace Terrasoft.Core.Process.Configuration
{
	using Terrasoft.Core.Process;
	using Terrasoft.Configuration.ML;
	using Terrasoft.Core.Factories;
    using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;

	#region Class: GetScoreAndExplanation

	/// <exclude/>
	public partial class GetScoreAndExplanation
	{

		#region Fields: Private

		private readonly Dictionary<string, Tuple<Guid, Guid>> _typeMap = new Dictionary<string, Tuple<Guid, Guid>> {
			{ "Contact", Tuple.Create(new Guid("16be3651-8fe2-4159-8dd0-a803d4683dd3"), new Guid("ef892d2f-eebd-6e64-7acc-02f7e3f981af")) },
			{ "Account", Tuple.Create(new Guid("25d7c1ab-1de0-4501-b402-02e0e5a72d6e"), new Guid("ce154785-3dc3-631a-b1a1-91f8c28300fd")) },
			{ "Lead", Tuple.Create(new Guid("41af89e9-750b-4ebb-8cac-ff39b64841ec"), new Guid("f3735b14-5953-4b62-b5cd-23c5e4860a14")) }
		};
		
		#endregion

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {
			var mlPredictorService = ClassFactory.Get<MLPredictorService>();
			if (_typeMap.TryGetValue(SchemaType, out var values)) {
				var schemaUId = values.Item1;
				var predictiveScoreColumnUId = values.Item2;
				var result = mlPredictorService.ScoreAndExplain(schemaUId, predictiveScoreColumnUId, IdForAnalysis);
				ColumnsAffectingScore = Common.Json.Json.Serialize(result);
			} else {
				throw new ArgumentException($"Unsupported type: {SchemaType}");
			}
			return true;
		}

		#endregion

	}

	#endregion

}