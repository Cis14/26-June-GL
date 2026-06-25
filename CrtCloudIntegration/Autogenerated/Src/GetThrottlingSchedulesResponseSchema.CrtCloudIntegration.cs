namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GetThrottlingSchedulesResponseSchema

	/// <exclude/>
	public class GetThrottlingSchedulesResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GetThrottlingSchedulesResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GetThrottlingSchedulesResponseSchema(GetThrottlingSchedulesResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("dfe8f2e9-f969-4657-87c8-6095ab77b87d");
			Name = "GetThrottlingSchedulesResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,145,49,111,194,48,16,133,103,144,248,15,39,186,180,75,178,23,202,66,43,150,182,66,132,173,234,96,204,17,44,57,118,228,59,15,20,245,191,247,236,64,68,161,82,167,248,158,159,159,223,231,56,213,32,181,74,35,172,49,4,69,126,199,197,220,187,157,169,99,80,108,188,43,230,47,213,74,44,222,17,210,104,120,28,13,7,145,140,171,161,58,16,99,35,102,107,81,39,39,21,11,116,24,140,158,92,123,86,209,177,105,176,168,100,87,89,243,149,131,197,37,190,187,128,181,12,48,183,138,232,17,22,200,235,125,240,204,86,142,87,122,143,219,104,145,206,247,231,19,101,89,194,148,98,211,168,112,152,157,230,21,182,1,9,29,19,240,30,65,214,209,50,248,93,158,184,15,4,58,37,22,231,156,242,34,232,227,89,177,18,118,14,74,243,167,8,109,220,88,163,65,167,106,255,54,27,28,115,187,30,104,25,124,139,129,13,10,213,50,7,117,251,215,245,179,32,225,4,62,0,165,175,238,95,180,35,184,105,79,69,31,116,217,191,3,120,195,102,131,225,254,93,254,43,60,193,152,111,59,191,26,226,241,67,2,60,19,38,101,122,75,55,131,63,136,147,23,142,80,35,79,82,221,9,124,159,184,209,109,59,244,60,119,234,111,81,180,31,140,48,50,247,111,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("dfe8f2e9-f969-4657-87c8-6095ab77b87d"));
		}

		#endregion

	}

	#endregion

}

