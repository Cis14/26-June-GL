namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UpdateMobileSectionSchemaUIdsInstallScriptSchema

	/// <exclude/>
	public class UpdateMobileSectionSchemaUIdsInstallScriptSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UpdateMobileSectionSchemaUIdsInstallScriptSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UpdateMobileSectionSchemaUIdsInstallScriptSchema(UpdateMobileSectionSchemaUIdsInstallScriptSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("b03c6789-7a08-42a2-810f-5fabb1ed1a93");
			Name = "UpdateMobileSectionSchemaUIdsInstallScript";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("9c9dce71-61f1-4751-aabc-14d22fc356f0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,205,146,93,111,218,48,20,134,175,147,95,97,229,42,72,53,74,192,249,96,140,73,249,172,144,198,52,137,178,219,201,77,14,212,82,98,71,177,195,22,85,252,247,6,2,93,161,80,109,119,187,136,37,31,59,207,123,252,158,23,113,90,130,172,104,6,232,1,234,154,74,177,86,195,72,240,53,219,52,53,85,76,112,253,89,215,26,201,248,6,45,91,169,160,156,158,182,111,239,215,112,163,60,76,184,98,138,129,156,234,186,86,53,143,5,203,80,86,80,41,209,170,202,169,130,133,120,100,5,44,33,219,75,45,179,39,40,233,106,158,203,101,86,179,74,161,79,104,62,231,82,209,162,232,11,201,111,200,26,37,106,244,220,209,180,170,102,219,14,129,106,160,185,224,69,139,238,27,150,163,159,52,203,68,195,85,215,237,66,228,77,1,243,28,205,16,135,95,135,99,211,72,18,199,119,83,223,193,78,50,178,49,25,17,15,79,130,112,132,237,120,98,185,169,149,90,33,113,141,193,244,54,63,19,92,209,236,38,127,68,18,203,35,196,197,227,128,88,152,76,82,7,251,118,108,227,104,100,69,33,137,172,40,142,200,135,252,99,255,95,153,84,223,233,6,86,23,124,203,78,125,63,140,19,236,133,81,132,201,216,14,176,31,133,41,78,130,212,113,131,96,226,144,40,248,155,254,111,241,61,199,141,72,152,184,29,53,14,48,73,99,31,135,177,239,225,48,12,124,127,236,218,19,207,247,246,252,189,64,63,207,173,232,176,253,104,192,92,73,168,187,252,240,126,162,168,57,219,14,186,193,105,154,118,200,68,139,228,201,193,78,255,252,94,159,154,182,207,195,130,242,174,205,122,120,15,170,47,135,237,183,46,180,166,241,58,1,227,238,82,103,255,124,141,173,145,249,170,49,76,65,101,79,105,45,202,56,52,175,100,100,112,236,237,226,175,78,244,161,173,32,143,68,209,148,252,7,45,26,248,188,247,233,139,105,92,143,174,49,64,179,217,193,202,97,82,86,170,61,97,181,63,204,37,168,55,184,155,160,187,107,81,232,95,118,70,163,91,48,215,180,144,112,60,219,233,167,229,3,3,222,135,248,191,52,224,125,86,255,197,128,238,219,233,187,23,239,7,50,231,227,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("b03c6789-7a08-42a2-810f-5fabb1ed1a93"));
		}

		#endregion

	}

	#endregion

}

