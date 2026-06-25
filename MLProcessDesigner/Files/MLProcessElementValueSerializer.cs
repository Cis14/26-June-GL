namespace Terrasoft.Configuration
{
	using System;
	using Terrasoft.Common;

	public class MLProcessElementValueSerializer
	{
		
		private const char PredictedValuePartsSeparator = ':';
		
		public static string Serialize(string typeName, string schemaName, string entitySchemaName) {
			return 
				(typeName ?? string.Empty) + PredictedValuePartsSeparator + 
				(schemaName ?? string.Empty) + PredictedValuePartsSeparator + 
				(entitySchemaName ?? string.Empty);
		}

		public static (string TypeName, string SchemaName, string EntitySchemaName)? Deserialize(string value) {
			if (value.IsNullOrEmpty()) {
				return null;
			}
			var parts = value.Split(PredictedValuePartsSeparator);
			if (parts.Length != 3) {
				throw new FormatException($"Predicted value should contain 3 components divided by " + 
					$"{PredictedValuePartsSeparator}. Found only {parts.Length}");
			} 
			return (parts[0], parts[1], parts[2]);
		}
	}
}
