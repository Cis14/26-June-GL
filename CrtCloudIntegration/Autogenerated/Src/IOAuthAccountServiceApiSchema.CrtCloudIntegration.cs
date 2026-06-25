namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IOAuthAccountServiceApiSchema

	/// <exclude/>
	public class IOAuthAccountServiceApiSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IOAuthAccountServiceApiSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IOAuthAccountServiceApiSchema(IOAuthAccountServiceApiSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("6411d790-fc03-4c7d-b30a-460690fd5e23");
			Name = "IOAuthAccountServiceApi";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,197,84,65,110,219,48,16,60,199,128,255,176,80,46,238,69,186,55,138,0,193,72,3,31,130,6,117,63,176,166,87,14,17,138,84,185,164,83,39,232,223,75,82,146,107,59,46,144,228,208,2,62,144,203,217,217,153,33,45,141,45,113,135,130,96,110,221,92,25,191,94,104,71,27,139,78,26,157,199,181,109,194,41,79,39,47,211,201,133,103,169,55,103,145,119,102,77,138,243,111,244,195,19,59,190,122,27,152,59,163,153,14,208,203,29,59,106,243,185,81,138,68,196,114,126,75,154,172,20,1,19,80,69,81,64,201,190,109,209,238,170,97,127,111,205,86,174,9,164,110,140,109,211,4,104,172,105,1,133,48,94,59,64,239,30,72,59,41,250,51,38,187,149,130,242,145,174,56,224,235,252,74,73,17,168,6,223,176,248,90,135,238,186,103,90,246,157,117,39,3,244,37,9,186,184,180,180,137,172,119,228,30,204,154,63,195,125,162,232,15,79,229,166,66,157,140,129,51,192,14,173,131,32,46,73,52,86,62,15,234,149,121,202,247,253,197,41,65,217,161,197,22,116,184,186,235,204,246,137,103,213,16,125,160,67,7,194,104,135,82,31,101,130,43,227,135,105,93,7,59,227,225,9,67,58,65,199,65,64,20,246,121,89,164,9,127,6,90,114,222,106,174,22,58,72,214,33,22,211,4,95,68,32,44,53,215,217,50,250,72,65,125,9,202,199,107,205,138,170,42,139,177,53,114,157,199,65,42,199,202,108,191,26,205,12,238,62,93,189,41,207,13,57,48,58,90,120,164,84,8,47,128,152,1,53,208,207,112,163,26,21,116,10,93,204,228,95,230,27,117,37,77,71,201,178,179,241,201,223,146,251,30,207,102,227,226,227,222,81,41,192,45,74,133,43,53,164,192,255,197,39,191,251,9,37,231,139,48,133,95,63,155,197,141,246,45,217,104,170,236,67,171,246,169,241,62,54,254,88,110,40,156,71,37,159,223,155,215,190,239,104,120,86,253,213,248,177,167,250,180,125,248,47,156,212,103,175,112,189,199,243,211,71,203,151,164,215,253,103,41,237,127,77,39,225,247,27,238,166,41,54,234,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("6411d790-fc03-4c7d-b30a-460690fd5e23"));
		}

		#endregion

	}

	#endregion

}

