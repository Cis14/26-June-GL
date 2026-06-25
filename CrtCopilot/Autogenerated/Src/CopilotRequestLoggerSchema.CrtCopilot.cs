namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotRequestLoggerSchema

	/// <exclude/>
	public class CopilotRequestLoggerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotRequestLoggerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotRequestLoggerSchema(CopilotRequestLoggerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("2bdc15d3-8dbc-493f-b7c4-24b2d92a7d35");
			Name = "CopilotRequestLogger";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("bad99159-33f2-43af-aab2-3508b9685277");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,147,207,106,227,48,16,198,207,41,244,29,6,122,73,46,121,128,134,82,218,100,211,13,236,66,105,114,43,165,40,246,196,43,42,107,188,163,81,33,132,125,247,29,217,105,154,22,57,185,8,107,252,155,207,159,231,143,55,53,134,198,20,8,83,70,35,150,198,83,106,172,35,185,188,216,93,94,12,98,176,190,130,229,54,8,214,147,195,125,133,204,38,208,70,20,102,28,207,77,33,196,22,131,18,202,92,49,86,150,60,76,157,9,225,26,246,122,79,248,55,98,144,133,223,80,75,53,113,237,108,1,69,130,50,12,40,178,107,193,131,222,220,162,43,85,240,49,54,154,217,189,219,171,56,82,87,179,200,233,7,60,236,160,66,153,64,72,199,191,35,106,102,4,87,182,70,88,138,97,73,183,94,116,77,228,96,17,230,198,58,44,123,169,32,156,202,241,131,153,184,23,178,94,96,69,98,220,93,145,220,133,243,224,138,222,240,12,247,200,84,55,114,30,188,213,210,214,141,195,244,229,60,221,86,24,125,217,21,185,189,119,209,111,193,147,93,253,69,85,133,220,114,207,51,220,152,232,228,222,250,82,139,51,148,109,131,180,25,46,114,9,163,209,139,102,168,77,100,111,92,118,22,58,16,174,33,43,112,98,72,216,190,107,131,247,83,210,93,64,39,188,36,239,182,7,177,159,54,232,228,110,151,122,152,10,225,181,200,133,39,217,34,125,22,68,123,42,28,211,10,180,195,185,254,62,156,57,227,195,30,7,89,3,35,72,171,56,24,228,237,193,77,62,43,173,235,32,223,224,131,247,223,40,127,168,204,219,126,136,182,132,165,121,199,175,254,135,153,93,229,207,231,15,175,140,18,217,247,84,116,156,145,61,214,56,97,61,27,213,224,127,135,88,200,113,200,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("2bdc15d3-8dbc-493f-b7c4-24b2d92a7d35"));
		}

		#endregion

	}

	#endregion

}

