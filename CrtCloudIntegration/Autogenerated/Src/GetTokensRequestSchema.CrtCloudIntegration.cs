namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GetTokensRequestSchema

	/// <exclude/>
	public class GetTokensRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GetTokensRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GetTokensRequestSchema(GetTokensRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a97aed5f-6169-4529-9b9a-e0bbe2b1e025");
			Name = "GetTokensRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,145,65,75,195,64,16,133,207,9,228,63,12,245,82,47,201,189,85,65,35,136,66,75,104,189,137,135,109,58,13,139,217,221,184,51,171,212,210,255,110,54,73,107,90,130,226,101,97,103,191,55,239,237,140,35,169,11,152,227,39,27,77,102,195,241,19,25,61,141,66,215,212,151,91,98,84,241,194,105,150,10,227,37,90,41,74,249,37,88,54,80,20,106,161,144,42,145,35,164,150,211,210,184,245,163,102,44,108,67,196,51,179,198,146,226,5,190,59,36,166,40,220,121,77,112,97,177,168,159,33,45,5,209,4,30,144,159,205,27,234,14,107,144,202,173,74,153,67,238,137,115,0,38,112,39,8,151,38,175,195,100,165,224,141,177,170,142,246,33,115,60,246,8,106,171,96,223,218,161,94,183,142,191,185,211,159,246,116,218,60,72,146,4,174,200,41,37,236,246,230,80,184,173,170,90,217,124,31,252,112,142,96,210,39,95,238,5,139,25,170,21,218,241,188,166,224,26,70,226,71,57,186,124,109,40,191,139,204,154,10,45,111,199,3,64,23,147,216,250,101,245,173,119,80,32,79,129,252,209,78,97,56,238,97,124,255,204,90,117,50,127,31,14,59,64,156,166,205,122,192,121,220,193,197,237,191,1,225,69,2,64,171,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a97aed5f-6169-4529-9b9a-e0bbe2b1e025"));
		}

		#endregion

	}

	#endregion

}

