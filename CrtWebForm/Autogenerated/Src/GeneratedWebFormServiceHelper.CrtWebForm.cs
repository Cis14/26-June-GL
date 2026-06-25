namespace Terrasoft.Configuration.GeneratedWebFormService
{
	using System;
	using System.Collections.Generic;
	
	/// <summary>
	/// ########### ######## # ######## #######. 
	/// </summary>
	public static class NameValueCorrector
	{
		/// <summary>
		/// ########### ########/######## #######.
		/// </summary>
		/// <param name="name">######## #######.</param>
		/// <param name="value">######## #######.</param>
		/// <returns>############### ########/######## #######.</returns>
		public static KeyValuePair<string, string> GetCorrectNameValue(string name, string value) {
			var correctName = NameCorrector.GetCorrectName(name);
			var correctValue = ValueCorrector.GetCorrectValue(name, value);
			return new KeyValuePair<string, string>(correctName, correctValue);
		}
	}

	/// <summary>
	/// ########### ######## #######, #### ### ### ####### #######. 
	/// </summary>
	public static class NameCorrector
	{
		/// <summary>
		/// ####### ######### ######## #######.
		/// </summary>
		private static Dictionary<string, string> synonymNames = new Dictionary<string, string>() {
					{ "Name", "Contact" },
					{ "Salutation", "Dear" },
					{ "Comment", "Commentary" },
					{ "Company", "Account" },
					{ "CompanyName", "Account" },
					{ "BusinessPhone", "BusinesPhone" },
					{ "UseEmail", "DoNotUseEmail" },
					{ "ContactId", "QualifiedContactId"}
				};

		/// <summary>
		/// ########## ######## #######, ###### ############ # ####### #########.
		/// #### ############ ## #######, ############ ########## ######## #######.
		/// </summary>
		/// <param name="name">######## #######.</param>
		/// <returns>######## ####### ## ####### ### ########## ########.</returns>
		public static string GetCorrectName(string name) {
			string value = string.Empty;
			if (synonymNames.TryGetValue(name, out value)) {
				return value;
			}
			return name;
		}
	}

	/// <summary>
	/// ########### ######## #######, #### ### ### ####### ####### ##############. 
	/// </summary>
	public static class ValueCorrector
	{
		/// <summary>
		/// ####### ##############, ###:
		/// key — ######## #######,
		/// value — ####### ## ####### ############## ######## #######.
		/// </summary>
		private static Dictionary<string, Func<string, string>> valueConverters = new Dictionary<string, Func<string, string>>() {
					{ "UseEmail", InvertTrueFalseString }
		};

		/// <summary>
		/// ########### ######## ######.
		/// </summary>
		/// <param name="value">############# ########.</param>
		/// <returns>############### ########.</returns>
		private static string InvertTrueFalseString(string value) {
			return value == "true" ? "false" : (value == "false" ? "true" : value);
		}

		/// <summary>
		/// ######### ############### ######## #######.
		/// #### ####### ############## ## #######, ########## ####### ######## #######.
		/// </summary>
		/// <param name="name">######## #######.</param>
		/// <param name="value">######## #######.</param>
		/// <returns>############### ######## #######.</returns>
		public static string GetCorrectValue(string name, string value) {
			Func<string, string> converter;
			if (valueConverters.TryGetValue(name, out converter)) {
				return converter(value);
			}
			return value;
		}
	}
}
