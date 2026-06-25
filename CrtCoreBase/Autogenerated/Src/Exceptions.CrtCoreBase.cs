namespace Terrasoft.Configuration
{
	using System;
	using Terrasoft.Common;
	using Terrasoft.Core;

	[Serializable]
	public class ItemNotFoundException : ApplicationException
	{
		#region Constructors: Public

		//###, ####### ############# ###### # Workspace CR172509
		public ItemNotFoundException(UserConnection userConnection, string itemName, string objectName)
			: base(string.Format(new LocalizableString(userConnection.Workspace.ResourceStorage,
				"Exceptions", "LocalizableStrings.ItemNotFoundMessageElementOfCollection.Value"),
				itemName, objectName)) {
		}
				
		#endregion
	}
}

