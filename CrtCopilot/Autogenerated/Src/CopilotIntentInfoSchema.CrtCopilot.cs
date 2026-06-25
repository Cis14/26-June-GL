namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotIntentInfoSchema

	/// <exclude/>
	public class CopilotIntentInfoSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotIntentInfoSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotIntentInfoSchema(CopilotIntentInfoSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a4b9b87e-283a-4ee0-a98e-f5700bd27f04");
			Name = "CopilotIntentInfo";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("800f00c8-04db-4ed1-bc94-0c44b7e5e4f0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,81,59,107,195,64,12,158,19,200,127,16,201,210,46,246,222,180,93,28,40,25,18,66,51,150,14,23,159,98,4,190,7,119,186,33,53,249,239,189,59,55,105,82,12,197,139,64,210,247,66,210,66,161,183,162,70,168,28,10,38,83,84,198,82,107,120,54,237,102,211,73,240,164,27,216,159,60,163,90,254,233,139,247,160,153,20,22,123,116,36,90,250,74,116,29,81,17,183,112,216,196,6,170,86,120,255,4,63,154,107,205,168,99,61,154,12,250,88,9,22,149,209,236,68,205,159,113,96,195,161,165,26,234,68,26,226,76,186,204,187,170,239,156,177,232,152,48,90,236,50,183,223,151,101,9,207,62,40,37,220,233,245,50,232,133,128,100,113,133,148,183,152,156,102,131,234,128,238,97,27,175,2,47,48,39,57,127,76,193,46,201,222,2,73,88,75,232,160,65,94,130,79,229,252,191,167,142,114,35,92,19,252,222,215,179,75,119,207,128,145,222,18,125,237,200,166,215,140,136,112,195,26,76,178,250,221,15,5,90,160,150,253,143,114,223,79,239,135,231,111,139,112,43,41,122,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a4b9b87e-283a-4ee0-a98e-f5700bd27f04"));
		}

		#endregion

	}

	#endregion

}

