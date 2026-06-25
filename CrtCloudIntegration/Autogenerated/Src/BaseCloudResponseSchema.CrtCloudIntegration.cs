namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: BaseCloudResponseSchema

	/// <exclude/>
	public class BaseCloudResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public BaseCloudResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public BaseCloudResponseSchema(BaseCloudResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("4d2a5529-1027-4e39-b5d9-500029d6ce6b");
			Name = "BaseCloudResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,147,75,111,194,48,12,199,207,32,241,29,44,184,78,244,206,235,176,106,154,118,64,66,131,47,16,82,183,68,107,147,46,78,134,38,196,119,159,147,150,199,54,216,216,78,109,252,248,251,103,199,209,162,66,170,133,68,88,161,181,130,76,238,134,169,209,185,42,188,21,78,25,61,76,31,150,115,147,97,73,189,238,174,215,237,117,59,3,139,5,59,32,45,5,209,8,238,5,97,90,26,159,61,179,144,209,132,49,40,73,18,152,144,175,42,97,223,103,237,57,38,64,110,44,172,57,7,108,27,15,185,53,21,112,25,216,42,183,1,105,170,138,213,107,107,106,180,78,33,13,15,114,201,153,94,237,215,165,146,32,163,228,5,132,78,195,122,132,93,28,229,70,176,136,185,141,255,43,103,52,60,162,35,16,240,38,74,143,160,116,166,36,143,66,23,176,221,160,219,160,5,183,81,116,194,231,127,242,82,34,69,210,239,168,141,37,138,29,143,0,19,57,115,214,227,36,145,51,80,57,44,157,112,158,0,95,189,40,9,250,230,165,63,6,19,138,109,21,225,93,136,206,217,17,195,207,170,156,68,219,121,172,141,41,225,137,150,13,15,236,130,171,83,160,131,29,243,58,111,245,161,208,116,218,22,217,135,144,253,111,195,224,59,163,240,101,34,160,168,112,123,175,161,78,104,209,114,115,72,174,25,22,102,103,253,65,159,119,207,216,254,15,141,145,179,225,6,90,250,29,112,79,227,128,20,27,184,157,157,151,157,68,129,183,195,175,46,38,93,195,155,55,145,255,231,59,46,149,228,39,247,55,202,107,169,223,88,149,118,144,114,204,37,202,1,234,172,121,49,241,220,88,63,27,217,246,1,225,50,86,111,53,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("4d2a5529-1027-4e39-b5d9-500029d6ce6b"));
		}

		#endregion

	}

	#endregion

}

