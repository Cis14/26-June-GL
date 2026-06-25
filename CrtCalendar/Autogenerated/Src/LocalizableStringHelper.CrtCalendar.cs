namespace Terrasoft.Configuration
{
	using Terrasoft.Common;
	using Terrasoft.Core;

	#region Class: LocalizableStringHelper

	/// <summary>
	/// ############### ##### ## ###### # ############## ########.
	/// </summary>
	public static class LocalizableStringHelper
	{

		#region Methods: Public

		/// <summary>
		/// ######## ######## ############# ######.
		/// </summary>
		/// <param name="userConnection">########### ############</param>
		/// <param name="schemaName">### #####, ########## ############# ######</param>
		/// <param name="localizableStringName">### ############# ######</param>
		/// <returns>######## ############# ######</returns>
		public static string GetValue(UserConnection userConnection, string schemaName, string localizableStringName) {
			var result = string.Empty;
			if (!string.IsNullOrEmpty(localizableStringName)) {
				string lsv = "LocalizableStrings." + localizableStringName + ".Value";
				var ls = new LocalizableString(userConnection.Workspace.ResourceStorage, schemaName, lsv);
				result = ls.ToString();
			}
			return result;
		}

		#endregion

	}

	#endregion

}
