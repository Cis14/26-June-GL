namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ImportEntitySavedEventArgsSchema

	/// <exclude/>
	public class ImportEntitySavedEventArgsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ImportEntitySavedEventArgsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ImportEntitySavedEventArgsSchema(ImportEntitySavedEventArgsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("d9862337-3e49-447c-a6fe-f4c905fd9341");
			Name = "ImportEntitySavedEventArgs";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1101e5cd-b945-4f88-8001-faccb4fdb24c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,143,193,106,195,48,12,134,207,41,244,29,12,189,231,1,218,211,8,221,200,97,80,150,237,1,188,88,113,205,98,57,88,114,183,16,246,238,115,236,80,186,110,228,98,144,244,233,211,111,148,22,104,144,45,136,87,240,94,146,235,184,172,28,118,70,7,47,217,56,44,31,77,15,181,29,156,231,237,102,218,110,138,64,6,181,104,70,98,176,135,107,125,187,237,161,60,34,27,54,64,17,136,200,206,131,142,42,81,245,146,104,47,178,45,33,99,35,47,160,142,23,64,126,240,154,18,61,132,247,222,180,162,157,225,21,86,68,17,118,238,25,136,164,134,27,69,49,37,205,245,234,201,187,1,252,156,102,47,78,201,157,231,203,157,167,96,212,114,166,137,174,184,81,43,49,9,13,124,16,52,63,223,127,241,23,231,184,105,207,96,229,219,42,156,115,199,4,198,74,63,46,213,29,94,20,247,246,234,28,240,99,213,27,12,114,12,241,89,163,130,175,255,184,29,160,202,223,79,117,238,254,110,198,222,15,16,73,126,79,253,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("d9862337-3e49-447c-a6fe-f4c905fd9341"));
		}

		#endregion

	}

	#endregion

}

