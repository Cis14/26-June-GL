namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ThrottlingScheduleRequestSchema

	/// <exclude/>
	public class ThrottlingScheduleRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ThrottlingScheduleRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ThrottlingScheduleRequestSchema(ThrottlingScheduleRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("9582f96f-5bbb-484d-b61c-0857214e30ce");
			Name = "ThrottlingScheduleRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,147,77,111,219,48,12,134,207,9,144,255,64,164,151,237,98,223,251,117,88,91,20,61,116,8,234,222,138,34,80,45,218,21,32,91,153,72,13,240,130,252,247,73,138,133,186,113,220,125,160,70,224,88,20,249,240,21,73,1,180,162,65,218,136,18,225,17,173,21,100,42,206,174,76,91,169,218,89,193,202,180,139,249,118,49,7,255,56,82,109,13,69,71,140,205,217,98,62,27,174,179,7,215,178,106,48,43,208,42,161,213,175,24,250,230,53,193,206,174,110,138,123,35,81,147,119,245,206,121,158,195,57,185,166,17,182,187,236,215,143,175,214,48,235,64,161,242,21,165,211,8,82,176,128,38,4,102,41,42,31,132,61,93,251,125,159,136,173,40,249,217,27,54,238,69,171,18,74,45,136,6,192,162,231,121,143,109,204,63,59,177,88,123,93,176,178,102,131,150,21,210,41,172,98,240,126,63,146,239,177,121,65,251,229,187,175,28,92,192,82,138,110,249,53,100,73,105,84,203,112,45,58,216,66,141,124,6,20,94,187,15,226,81,139,110,237,211,173,177,17,74,31,97,5,135,21,218,155,176,253,183,212,200,162,136,61,42,48,194,200,83,39,148,158,96,43,247,197,136,235,93,156,129,240,25,70,225,160,79,201,20,100,64,217,215,29,216,128,219,248,78,33,240,184,133,217,0,148,191,39,29,118,47,216,254,208,192,7,252,225,144,248,20,190,9,66,63,130,63,85,153,108,251,248,109,82,62,161,62,153,111,145,9,140,13,149,32,136,37,4,215,42,15,2,37,209,79,120,165,208,102,7,164,124,140,154,108,200,90,201,216,138,228,216,159,235,214,41,185,239,200,157,28,55,227,95,117,15,203,237,139,200,88,119,94,254,127,202,78,132,9,229,97,148,138,222,229,51,180,107,69,12,166,122,187,234,231,132,8,165,197,234,98,57,238,251,50,191,252,224,88,199,47,70,34,191,191,18,99,246,211,51,164,207,195,99,133,100,254,111,22,127,187,223,228,115,30,185,68,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("9582f96f-5bbb-484d-b61c-0857214e30ce"));
		}

		#endregion

	}

	#endregion

}

