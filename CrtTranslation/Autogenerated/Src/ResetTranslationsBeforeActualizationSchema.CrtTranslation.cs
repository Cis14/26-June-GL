namespace Terrasoft.Core.Process.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Process;

	#region Class: ResetTranslationsBeforeActualization

	/// <exclude/>
	public partial class ResetTranslationsBeforeActualization : ProcessUserTask
	{

		#region Constructors: Public

		public ResetTranslationsBeforeActualization(UserConnection userConnection)
			: base(userConnection) {
			SchemaUId = new Guid("1206db30-9a5b-4631-9d48-cbfa5247bd95");
		}

		#endregion

		#region Methods: Public

		public override void WritePropertiesData(DataWriter writer) {
			writer.WriteStartObject(Name);
			base.WritePropertiesData(writer);
			if (Status == Core.Process.ProcessStatus.Inactive) {
				writer.WriteFinishObject();
				return;
			}
			writer.WriteFinishObject();
		}

		#endregion

	}

	#endregion

}

