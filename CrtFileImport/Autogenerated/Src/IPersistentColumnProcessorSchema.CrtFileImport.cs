namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IPersistentColumnProcessorSchema

	/// <exclude/>
	public class IPersistentColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IPersistentColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IPersistentColumnProcessorSchema(IPersistentColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("9d8cf2ba-6149-493d-80f4-bea4d4b996f5");
			Name = "IPersistentColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,117,140,65,10,2,49,12,69,215,22,122,135,30,64,230,0,186,115,64,232,110,64,47,80,75,58,4,218,100,72,210,197,48,120,119,235,86,16,254,234,253,199,163,212,64,183,148,33,60,65,36,41,23,155,102,166,130,107,151,100,200,52,221,177,66,108,27,139,121,119,120,119,234,138,180,134,199,174,6,237,234,221,32,91,127,85,204,1,201,64,202,55,21,23,16,197,33,144,205,92,123,163,69,56,131,42,203,229,239,119,14,241,150,20,126,252,81,63,194,219,187,177,15,206,94,56,37,170,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("9d8cf2ba-6149-493d-80f4-bea4d4b996f5"));
		}

		#endregion

	}

	#endregion

}

