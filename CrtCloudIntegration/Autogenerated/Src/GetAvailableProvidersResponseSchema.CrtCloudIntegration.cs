namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GetAvailableProvidersResponseSchema

	/// <exclude/>
	public class GetAvailableProvidersResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GetAvailableProvidersResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GetAvailableProvidersResponseSchema(GetAvailableProvidersResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a32e2754-ebd1-43a6-8c8d-70af49bb850d");
			Name = "GetAvailableProvidersResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,197,84,77,107,219,64,16,61,203,224,255,48,164,151,22,130,116,175,93,67,113,66,240,33,193,216,189,149,30,214,171,145,189,176,218,21,59,187,14,169,201,127,239,236,90,178,101,59,161,52,24,122,211,124,189,121,111,102,180,70,212,72,141,144,8,63,208,57,65,182,242,249,212,154,74,173,131,19,94,89,147,79,239,151,143,182,68,77,195,193,110,56,200,2,41,179,134,229,11,121,172,57,83,107,148,49,141,242,7,52,232,148,28,157,231,44,130,241,170,198,124,201,81,161,213,239,132,202,89,156,247,201,225,154,13,152,106,65,244,21,30,208,127,223,10,165,197,74,227,220,217,173,42,209,209,130,233,49,60,166,130,162,40,96,76,161,174,133,123,153,180,246,2,27,135,132,198,19,248,13,2,127,7,237,193,86,201,242,27,103,189,215,145,14,201,13,150,65,99,222,225,20,61,160,159,119,194,11,214,237,157,144,254,23,59,154,176,210,74,130,140,204,254,70,44,219,37,114,7,57,156,210,160,243,10,89,211,60,225,236,227,231,236,147,131,177,9,172,3,194,86,128,60,204,52,138,16,93,95,168,249,35,234,104,58,2,249,1,179,175,100,47,229,17,235,21,186,207,79,188,93,248,6,55,226,130,253,205,151,168,178,147,57,187,55,161,70,23,51,198,93,202,204,84,118,2,151,186,97,7,107,244,163,72,120,4,175,173,114,52,229,94,124,178,247,222,190,51,187,220,118,191,207,255,92,238,41,143,235,237,82,192,86,232,128,160,76,169,36,31,61,179,124,222,32,243,118,137,124,183,70,80,196,43,55,134,87,142,229,59,43,77,158,132,118,52,229,196,187,128,227,66,78,64,85,239,35,142,192,198,150,207,138,240,54,22,85,66,83,170,234,181,58,34,191,117,59,138,166,29,216,233,209,172,172,213,48,59,70,207,15,35,203,174,62,168,18,43,17,15,160,226,218,131,68,16,82,90,126,100,174,54,188,182,203,53,70,119,183,135,122,115,112,109,236,163,99,139,172,77,108,100,79,21,252,195,179,208,149,68,251,148,34,121,23,247,48,239,37,124,232,191,31,14,94,255,0,47,203,158,37,98,6,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a32e2754-ebd1-43a6-8c8d-70af49bb850d"));
		}

		#endregion

	}

	#endregion

}

