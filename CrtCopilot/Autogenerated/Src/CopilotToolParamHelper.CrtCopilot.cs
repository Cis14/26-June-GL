namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.Copilot.Metadata;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Enrichment.Interfaces.ChatCompletion;

	#region Class: CopilotToolParamHelper

	/// <summary>
	/// Provides helper methods for defining property definitions for Copilot tool parameters.
	/// </summary>
	internal static class CopilotToolParamHelper
	{

		#region Methods: Private

		private static string AppendAlternativeName(LocalizableString description, string name, string alternativeName) {
			if (string.IsNullOrWhiteSpace(alternativeName) ||
				string.Equals(name, alternativeName, StringComparison.InvariantCultureIgnoreCase)) {
				return description?.Value;
			}
			string separator = string.Empty;
			string value = description == null ? string.Empty : description.Value;
			if (!string.IsNullOrWhiteSpace(value)) {
				separator = value.EndsWith(".") ? " " : ". ";
			}
			return $"{value}{separator}Alternative name: [{alternativeName}]";
		}

		private static PropertyDefinition DefineCompositeObjectListToolDefinition(
			ICopilotParameterMetaInfo parameterMetaInfo) {
			var properties = new Dictionary<string, PropertyDefinition>();
			foreach (ICopilotParameterMetaInfo internalParameterMetaInfo in parameterMetaInfo.ItemProperties) {
				properties[internalParameterMetaInfo.Name] = GetToolParam(internalParameterMetaInfo);
			}
			List<string> requiredProperties = parameterMetaInfo.ItemProperties
				.Where(param => param.IsRequired)
				.Select(param => param.Name).ToList();
			var objectDefinition = PropertyDefinition.DefineObject(properties, requiredProperties,
				AppendAlternativeName(parameterMetaInfo.Description, parameterMetaInfo.Name, parameterMetaInfo.Caption),
				null);
			return PropertyDefinition.DefineArray(objectDefinition);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Returns a <see cref="PropertyDefinition"/> for the specified copilot parameter metadata,
		/// mapping its data value type to the appropriate property definition.
		/// </summary>
		/// <param name="parameterMetaInfo">The metadata information of the copilot parameter.</param>
		/// <returns>The corresponding <see cref="PropertyDefinition"/>.</returns>
		/// <exception cref="NotImplementedException">
		/// Thrown when the data value type of the parameter is not supported.
		/// </exception>
		public static PropertyDefinition GetToolParam(ICopilotParameterMetaInfo parameterMetaInfo) {
			string description = AppendAlternativeName(parameterMetaInfo.Description, parameterMetaInfo.Name,
				parameterMetaInfo.Caption);
			switch (parameterMetaInfo.DataValueType) {
				case TextDataValueType _:
				case GuidDataValueType _:
				case DateTimeDataValueType _:
					return PropertyDefinition.DefineString(description);
				case FloatDataValueType _:
					return PropertyDefinition.DefineNumber(description);
				case IntegerDataValueType _:
					return PropertyDefinition.DefineInteger(description);
				case BooleanDataValueType _:
					return PropertyDefinition.DefineBoolean(description);
				case null:
					return PropertyDefinition.DefineNull(description);
				case CompositeObjectListDataValueType _:
					return DefineCompositeObjectListToolDefinition(parameterMetaInfo);
				default:
					throw new NotImplementedException(
						$"DataValueType {parameterMetaInfo.DataValueType.Name} is not supported yet");
			}
		}

		#endregion

	}

	#endregion

}
