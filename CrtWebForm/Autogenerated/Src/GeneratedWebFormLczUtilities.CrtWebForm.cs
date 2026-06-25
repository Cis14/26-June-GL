namespace Terrasoft.Configuration.GeneratedWebFormService
{
	using Terrasoft.Common;
	using Terrasoft.Core;

	#region Class: GeneratedWebFormLczUtilities

	/// <summary>
	/// Utility class for work with localizable storage.
	/// </summary>
	public class GeneratedWebFormLczUtilities
	{
		#region Methods: Public
		
		/// <summary>
		/// Returns localizable value from resource storage.
		/// </summary>
		/// <param name="lczName">Name of string in resource file.</param>
		/// <param name="resourceManagerName">Name of resource manager.</param>
		/// <param name="userConnection">Instance of user connection.</param>
		/// <returns>Localizable value from resource storage.</returns>
		public static string GetLczStringValue(string lczName, string resourceManagerName, UserConnection userConnection) {
			string localizableStringName = string.Format("LocalizableStrings.{0}.Value", lczName);
			var localizableString = new LocalizableString(
				userConnection.Workspace.ResourceStorage, resourceManagerName, localizableStringName);
			string value = localizableString.Value;
			if (value == null) {
				value = localizableString.GetCultureValue(GeneralResourceStorage.DefCulture, false);
			}
			return value;
		}

		#endregion
	}

	#endregion
}

