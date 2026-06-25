namespace Terrasoft.Core.Process
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Drawing;
	using System.Globalization;
	using System.Text;
	using Terrasoft.Common;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Configuration;

	#region Class: UpdateOpenIdContactNameMethodsWrapper

	/// <exclude/>
	public class UpdateOpenIdContactNameMethodsWrapper : ProcessModel
	{

		public UpdateOpenIdContactNameMethodsWrapper(Process process)
			: base(process) {
			AddScriptTaskMethod("ScriptTask1Execute", ScriptTask1Execute);
		}

		#region Methods: Private

		private bool ScriptTask1Execute(ProcessExecutingContext context) {
			UserConnection userConnection = Get<UserConnection>("UserConnection");
			Guid contactId = Get<Guid>("ContactId");
			EntitySchema contactSchema = userConnection.EntitySchemaManager.GetInstanceByName("Contact");
			var contact = contactSchema.CreateEntity(userConnection);
			var converter = ContactUtilities.GetContactConverter(userConnection);
			if (contact.FetchFromDB(contactId) && contact.GetTypedColumnValue<string>("Name").IsNotNullOrEmpty()) {
				ContactSgm contactSgm = converter.GetContactSgm(contact.GetTypedColumnValue<string>("Name"));
				contact.SetColumnValue("Surname", contactSgm.Surname);
				contact.SetColumnValue("GivenName", contactSgm.GivenName);
				contact.SetColumnValue("MiddleName", contactSgm.MiddleName);
				contact.Save();
			}
			return true;
		}

		#endregion

	}

	#endregion

}

