namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: StopEmailRequestSchema

	/// <exclude/>
	public class StopEmailRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public StopEmailRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public StopEmailRequestSchema(StopEmailRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("3d11e55e-cb8c-465a-9cb7-dfaf13400a61");
			Name = "StopEmailRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,146,79,75,195,64,16,197,207,22,250,29,134,120,209,75,114,239,31,65,107,41,30,42,165,233,77,68,166,217,73,92,216,205,198,221,217,66,45,253,238,110,54,109,197,8,122,10,51,251,126,195,123,143,64,141,154,92,131,5,193,134,172,69,103,74,78,103,166,46,101,229,45,178,52,117,58,155,231,75,35,72,185,225,224,48,28,92,121,39,235,10,242,189,99,210,227,222,156,174,125,205,82,83,154,147,149,168,228,103,188,16,84,65,119,109,169,10,3,204,20,58,55,130,123,33,54,164,27,133,76,107,250,240,228,56,170,178,44,131,137,243,90,163,221,223,157,230,72,64,105,44,216,78,9,108,192,177,105,128,52,74,149,158,177,172,199,77,28,17,42,103,160,176,84,78,147,127,243,165,15,232,40,56,223,201,226,236,41,129,172,189,246,242,136,140,129,98,139,5,191,134,69,227,183,74,22,80,68,103,121,176,50,111,157,156,32,24,193,239,75,1,58,196,132,151,34,86,214,52,100,89,82,104,99,21,239,117,239,253,10,226,98,65,236,32,52,224,218,47,191,83,23,29,164,160,80,120,41,201,166,23,52,235,179,147,29,42,79,151,113,243,55,253,45,142,169,151,164,183,100,111,158,195,111,2,83,72,34,248,38,69,114,219,214,112,238,97,225,165,128,88,193,147,128,3,84,196,227,214,233,24,142,167,200,84,139,46,117,156,187,237,207,229,241,11,25,232,86,234,138,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("3d11e55e-cb8c-465a-9cb7-dfaf13400a61"));
		}

		#endregion

	}

	#endregion

}

