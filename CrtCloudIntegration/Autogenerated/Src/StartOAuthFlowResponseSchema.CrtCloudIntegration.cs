namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: StartOAuthFlowResponseSchema

	/// <exclude/>
	public class StartOAuthFlowResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public StartOAuthFlowResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public StartOAuthFlowResponseSchema(StartOAuthFlowResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("b4b97235-8444-47bd-8398-56bb3b3cb9ef");
			Name = "StartOAuthFlowResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,101,144,193,74,3,65,12,134,207,22,250,14,1,175,178,123,111,69,168,11,130,7,177,84,125,128,56,205,174,3,179,147,154,100,20,45,125,119,103,119,186,197,234,101,32,153,63,249,191,252,17,123,210,29,58,130,70,172,9,156,182,247,209,168,19,52,207,177,122,224,45,5,173,54,89,194,81,73,231,179,253,124,118,145,212,199,14,158,73,4,149,91,171,26,142,173,239,82,153,89,206,103,89,114,41,212,229,2,154,128,170,11,120,50,20,187,11,252,185,161,247,68,106,131,38,171,234,186,134,107,77,125,143,242,117,115,172,39,47,104,133,123,80,118,30,3,160,115,156,162,129,146,124,120,71,87,224,56,26,250,168,128,33,128,143,45,131,49,232,224,2,152,236,13,218,236,85,77,14,245,47,139,93,122,13,222,129,27,184,10,214,227,42,15,20,182,163,243,2,110,81,105,85,60,167,110,158,221,143,208,167,219,214,194,59,18,243,148,15,92,143,107,203,255,223,171,198,198,139,132,115,68,22,255,61,6,118,98,253,15,59,209,170,201,144,248,41,197,97,217,30,58,178,101,78,36,63,135,35,24,197,109,97,27,235,210,61,111,30,126,0,68,49,30,80,240,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("b4b97235-8444-47bd-8398-56bb3b3cb9ef"));
		}

		#endregion

	}

	#endregion

}

