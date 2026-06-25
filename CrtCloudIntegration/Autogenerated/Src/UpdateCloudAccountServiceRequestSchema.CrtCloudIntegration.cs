namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UpdateCloudAccountServiceRequestSchema

	/// <exclude/>
	public class UpdateCloudAccountServiceRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UpdateCloudAccountServiceRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UpdateCloudAccountServiceRequestSchema(UpdateCloudAccountServiceRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("924fae82-030c-42fa-84b9-94064bc4d89e");
			Name = "UpdateCloudAccountServiceRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,145,65,79,194,64,16,133,207,146,240,31,38,112,209,75,123,23,36,33,213,16,162,24,2,122,50,30,150,237,180,110,108,119,235,206,212,164,18,255,187,211,150,34,16,15,120,218,236,155,55,111,191,153,181,42,71,42,148,70,120,66,239,21,185,132,131,200,217,196,164,165,87,108,156,13,162,187,245,194,197,152,81,191,183,237,247,46,74,50,54,133,117,69,140,185,56,179,12,117,109,163,96,134,22,189,209,163,83,207,170,180,108,114,12,214,82,85,153,249,106,82,197,37,190,161,199,84,46,16,101,138,232,26,158,139,88,49,70,153,43,227,169,214,78,250,164,231,211,104,92,225,71,137,196,77,79,24,134,48,166,50,207,149,175,38,187,251,10,11,143,132,150,9,124,107,5,118,80,54,113,64,109,6,129,75,128,223,16,84,27,29,116,89,225,65,216,203,173,98,37,227,179,87,154,95,69,40,202,77,102,52,232,26,240,12,190,139,109,195,184,31,108,233,93,129,158,13,202,116,203,38,170,173,159,14,209,8,51,20,126,231,5,88,206,154,116,186,156,195,59,86,193,190,227,16,181,101,93,96,190,65,127,249,40,191,8,55,48,80,133,185,199,106,112,85,163,119,236,196,190,254,141,105,83,130,45,164,200,163,250,141,17,124,255,7,70,215,99,255,46,83,116,150,88,130,68,76,7,43,61,19,180,203,57,70,125,48,196,227,221,78,231,54,113,19,88,119,239,253,193,61,68,27,183,123,110,238,173,122,44,138,246,3,129,218,240,169,225,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("924fae82-030c-42fa-84b9-94064bc4d89e"));
		}

		#endregion

	}

	#endregion

}

