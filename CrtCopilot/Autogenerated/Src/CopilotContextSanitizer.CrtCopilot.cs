using System;

namespace Creatio.Copilot 
{
	[Obsolete("8.3.3 | Class is not in use and will be removed in upcoming releases")]
	public static class CopilotContextSanitizer {
		private const int MaxStringLength = 512;
		private const string LongStringTag = "#LONG_STRING#";

		/// <summary>
		/// Processes the given value. If the value is a string and its length exceeds 512 characters, 
		/// it replaces the value with the #LONG_STRING# tag.
		/// </summary>
		/// <param name="value">The value to process.</param>
		/// <returns>The processed value.</returns>
		public static object ProcessValue(object value) {
			if (value is string stringValue) {
				if (stringValue.Length > MaxStringLength) {
					return LongStringTag;
				}
			}

			return value;
		}
	}
}
