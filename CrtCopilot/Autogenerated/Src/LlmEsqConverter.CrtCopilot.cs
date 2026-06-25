namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Terrasoft.Common;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Nui.ServiceModel.DataContract;
	using FilterType = Terrasoft.Nui.ServiceModel.DataContract.FilterType;

	#region Serializable types

	public class SerializableParameter
	{
		public DataValueType? DataValueType { get; set; }

		public object Value { get; set; }

		public string ClassName { get; set; }
	}

	public class SerializableDateParameter : SerializableParameter
	{
		public string DateValue { get; set; }
	}

	public class SerializableExpression
	{

		#region Properties: Public

		public EntitySchemaQueryExpressionType? ExpressionType { get; set; }

		public bool? IsBlock { get; set; }

		public string ColumnPath { get; set; }

		public SerializableParameter Parameter { get; set; }

		public FunctionType? FunctionType { get; set; }

		public EntitySchemaQueryMacrosType? MacrosType { get; set; }

		public SerializableExpression FunctionArgument { get; set; }

		public SerializableExpression[] FunctionArguments { get; set; }

		public DateDiffQueryFunctionInterval? DateDiffInterval { get; set; }

		public DatePart? DatePartType { get; set; }

		public AggregationType? AggregationType { get; set; }

		public AggregationEvalType? AggregationEvalType { get; set; }

		public SerializableFilters SubFilters { get; set; }

		public ArithmeticOperation? ArithmeticOperation { get; set; }

		public SerializableExpression LeftArithmeticOperand { get; set; }

		public SerializableExpression RightArithmeticOperand { get; set; }

		public OrderDirection? SubOrderDirection { get; set; }

		public string SubOrderColumn { get; set; }

		public string ClassName { get; set; }

		#endregion

	}

	public class SerializableSelectQueryColumn
	{

		public OrderDirection? OrderDirection { get; set; }

		public int? OrderPosition { get; set; }

		public string Caption { get; set; }

		public bool? IsVisible { get; set; }

		public SerializableExpression Expression { get; set; }
	}

	#endregion

	#region Converter types

	public interface ILlmEsqConverter
	{
		SerializableSelectQueryColumn ConvertToEsqColumn(string select);

		SerializableFilters ConvertToEsqFilter(LLMUnknownFilterResponseContract filtersConfig,
			string rootSchemaName = "defaultSchema");
	}

	[DefaultBinding(typeof(ILlmEsqConverter))]
	public class LlmEsqConverter : ILlmEsqConverter
	{

		public SerializableSelectQueryColumn ConvertToEsqColumn(string select) {
			var match = Regex.Match(select, @"^(COUNT|SUM|AVG|MIN|MAX)\(([^)]+)\)$");
			if (!match.Success) {
				throw new ArgumentException("Invalid select format. Expected format: FUNCTION(Column)");
			}

			string functionType = match.Groups[1].Value;
			string columnPath = match.Groups[2].Value;
			AggregationType aggregationType;
			switch (functionType) {
			case "COUNT":
			aggregationType = AggregationType.Count;
			break;
			case "SUM":
			aggregationType = AggregationType.Sum;
			break;
			case "AVG":
			aggregationType = AggregationType.Avg;
			break;
			case "MIN":
			aggregationType = AggregationType.Min;
			break;
			case "MAX":
			aggregationType = AggregationType.Max;
			break;
			default:
			throw new ArgumentException($"Unsupported aggregation function: {functionType}");
			}
			return new SerializableSelectQueryColumn {
				Expression = new SerializableExpression {
					FunctionType = FunctionType.Aggregation,
					FunctionArgument = new SerializableExpression {
						ColumnPath = columnPath,
						ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn
					},
					ExpressionType = EntitySchemaQueryExpressionType.Function,
					AggregationType = aggregationType,
					AggregationEvalType = aggregationType == AggregationType.Count
						? AggregationEvalType.Distinct
						: AggregationEvalType.None,
				},
				OrderDirection = OrderDirection.None,
				OrderPosition = -1
			};
		}

		public SerializableFilters ConvertToEsqFilter(LLMUnknownFilterResponseContract filtersConfig,
			string rootSchemaName = "defaultSchema") {
			var filters = new SerializableFilters {
				FilterType = FilterType.FilterGroup,
				IsEnabled = true,
				RootSchemaName = rootSchemaName,
				ClassName = "Terrasoft.FilterGroup",
				Key = string.Empty,
				LogicalOperation = filtersConfig.logicalOperation == "AND"
					? LogicalOperationStrict.And
					: LogicalOperationStrict.Or,
				Items = new Dictionary<string, SerializableFilter>(),
			};
			foreach (var filterWithIndex in filtersConfig.filters.Select((item, index) => new {
				item,
				index
			})) {
				int index = filterWithIndex.index;
				string key = string.Concat("Filter_", index);
				var filtersConverter = ClassFactory.Get<ILlmEsqFiltersConverter>();
				filters.Items.Add(key, filtersConverter.Convert(filterWithIndex.item, rootSchemaName, index));
			}

			foreach (var filterWithIndex in filtersConfig.backwardReferenceFilters.Select((item, index) => new {
				item,
				index
			})) {
				int index = filterWithIndex.index;
				string key = string.Concat("BackwardReferenceFilter_", index);
				var filtersConverter = ClassFactory.Get<ILlmEsqFiltersConverter>();
				filters.Items.Add(key, filtersConverter.ConvertBackwardReferenceFitler(filterWithIndex.item, rootSchemaName, index));
			}


			return filters;
		}
	}

	#endregion

}

