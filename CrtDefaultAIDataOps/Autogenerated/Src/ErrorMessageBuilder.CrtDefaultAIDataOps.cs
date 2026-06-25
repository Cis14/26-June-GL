using System;
using System.Collections.Generic;
using Terrasoft.Common;
using Terrasoft.Core.BusinessRules.Models.Errors;

namespace Terrasoft.Configuration
{
	public static class ErrorMessageBuilder
	{
		private static readonly string _separator = Environment.NewLine;

		public static string AddError(string errorOutput, string message) {
			if (message.IsNullOrEmpty()) {
				return errorOutput;
			}
			if (errorOutput.IsNullOrEmpty()) {
				errorOutput = message;
			} else {
				errorOutput += _separator + message;
			}
			return errorOutput;
		}

		public static string AddBusinessRulesErrors(string errorOutput, List<BusinessRuleExecutionError> errors) {
			var messages = errors.ConvertAll(e => e.Message);
			if (messages.Count == 0) {
				return errorOutput;
			}
			if (errorOutput.IsNullOrEmpty()) {
				errorOutput = string.Join(_separator, messages);
			} else {
				errorOutput += _separator + string.Join(_separator, messages);
			}
			return errorOutput;
		}
	}
}
