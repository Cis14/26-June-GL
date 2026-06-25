namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GetThrottlingScheduleRequestSchema

	/// <exclude/>
	public class GetThrottlingScheduleRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GetThrottlingScheduleRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GetThrottlingScheduleRequestSchema(GetThrottlingScheduleRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("164861d9-449c-4213-a157-80657f43f33a");
			Name = "GetThrottlingScheduleRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,145,79,79,2,49,16,197,207,144,240,29,38,120,209,203,238,29,212,131,139,49,28,48,132,229,102,140,41,221,97,105,178,219,174,157,169,9,18,191,187,179,127,5,15,156,154,190,190,121,125,191,214,170,18,169,82,26,97,139,222,43,114,123,142,18,103,247,38,15,94,177,113,54,74,158,211,141,88,156,37,164,201,248,52,25,143,2,25,155,67,122,36,198,50,218,4,203,166,196,40,69,111,84,97,190,155,161,249,224,186,146,186,114,25,22,36,86,49,223,120,204,69,134,164,80,68,51,120,65,222,30,188,99,46,36,35,213,7,204,66,129,27,252,12,72,220,248,227,56,134,123,10,101,169,252,241,177,219,47,20,43,208,206,178,87,154,129,29,228,40,203,16,3,212,229,80,212,7,196,103,9,111,245,120,210,77,191,139,80,133,93,97,52,232,186,209,213,66,48,131,39,69,40,15,240,101,244,95,203,209,169,105,58,160,173,189,171,208,179,65,225,91,55,217,237,249,127,148,70,144,251,8,156,7,170,215,229,2,220,254,130,68,74,50,230,199,104,152,63,39,105,81,86,88,238,208,223,190,202,255,194,3,76,251,145,15,147,77,239,106,188,158,207,88,134,180,59,92,102,112,170,95,109,94,223,59,135,159,14,0,109,214,50,52,251,86,189,20,69,251,5,192,112,205,147,73,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("164861d9-449c-4213-a157-80657f43f33a"));
		}

		#endregion

	}

	#endregion

}

