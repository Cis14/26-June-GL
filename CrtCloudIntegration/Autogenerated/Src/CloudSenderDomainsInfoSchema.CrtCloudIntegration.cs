namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CloudSenderDomainsInfoSchema

	/// <exclude/>
	public class CloudSenderDomainsInfoSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CloudSenderDomainsInfoSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CloudSenderDomainsInfoSchema(CloudSenderDomainsInfoSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("f2e8d79a-460e-4a42-b17f-4d076f55b6e6");
			Name = "CloudSenderDomainsInfo";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,213,84,193,106,27,49,16,61,39,144,127,24,146,67,111,222,123,237,230,98,183,198,164,6,227,45,185,171,210,104,51,88,43,45,146,214,96,76,255,189,35,201,54,169,139,195,58,129,66,79,66,163,153,121,239,205,140,198,138,22,67,39,36,194,15,244,94,4,167,227,104,234,172,166,166,247,34,146,179,163,233,215,122,233,20,154,112,119,187,191,187,189,233,3,217,6,234,93,136,216,142,207,238,28,105,12,202,20,22,70,115,180,232,73,178,15,123,61,120,108,216,10,83,35,66,248,204,135,235,85,141,86,161,159,185,86,144,13,11,171,93,246,172,170,10,38,161,111,91,225,119,143,135,251,26,59,143,1,109,12,192,103,199,217,17,156,134,248,130,16,208,111,137,217,107,231,249,45,122,194,109,226,163,74,214,108,230,64,149,108,200,38,51,58,66,84,175,48,186,254,167,33,9,50,145,187,200,237,102,159,249,157,164,172,188,235,208,71,66,214,179,202,9,202,251,185,128,108,152,35,115,207,92,248,76,188,143,4,133,85,64,241,19,63,118,165,108,167,20,213,121,142,73,64,20,38,56,144,30,245,151,251,215,20,239,171,236,117,208,241,157,66,156,252,37,227,17,14,114,96,15,13,198,113,226,50,134,95,87,145,70,45,122,19,65,109,168,29,1,143,73,204,249,182,194,244,8,164,161,243,110,75,12,88,34,210,96,24,132,13,238,114,27,132,49,71,213,23,68,30,232,7,238,34,183,107,86,192,102,79,139,229,19,167,248,79,72,51,219,53,74,231,213,251,9,215,171,111,133,221,32,196,186,211,207,89,202,63,194,155,177,125,115,29,98,153,187,132,65,42,239,20,254,169,169,68,195,240,114,240,243,41,246,202,234,174,143,251,34,68,17,251,97,77,172,179,235,245,16,146,215,228,219,0,100,35,79,160,26,94,186,83,110,222,210,65,52,195,90,180,44,190,239,159,8,254,14,54,146,38,244,111,227,205,123,82,176,248,192,168,75,33,95,242,103,27,164,107,154,188,47,236,130,7,222,116,101,49,231,123,177,254,105,100,219,111,79,14,202,74,238,6,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("f2e8d79a-460e-4a42-b17f-4d076f55b6e6"));
		}

		#endregion

	}

	#endregion

}

