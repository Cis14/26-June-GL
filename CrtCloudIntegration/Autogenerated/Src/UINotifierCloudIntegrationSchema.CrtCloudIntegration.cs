namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UINotifierCloudIntegrationSchema

	/// <exclude/>
	public class UINotifierCloudIntegrationSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UINotifierCloudIntegrationSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UINotifierCloudIntegrationSchema(UINotifierCloudIntegrationSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e5e79c3a-7d9a-42b3-9e61-5839136f3c4b");
			Name = "UINotifierCloudIntegration";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,81,203,106,195,64,12,60,199,144,127,16,238,37,129,98,223,243,130,198,161,144,75,40,109,115,42,61,108,109,217,93,88,239,46,146,92,8,33,255,222,245,35,105,8,37,236,101,71,140,70,163,145,85,53,178,87,57,66,70,146,25,215,20,91,43,88,145,18,237,108,178,23,109,180,104,228,113,116,28,71,163,134,181,173,224,29,137,20,187,82,146,204,17,38,207,42,23,71,129,51,31,71,237,27,61,16,86,161,25,50,163,152,103,176,223,238,156,232,82,35,109,116,165,69,153,167,130,59,94,154,166,176,224,166,174,21,29,86,3,126,69,79,200,104,133,65,190,17,116,237,13,214,1,118,118,192,149,65,13,236,32,151,156,53,210,27,145,5,35,42,195,14,114,194,114,25,223,93,44,217,254,249,139,33,109,37,62,54,88,170,198,200,90,219,34,236,59,145,131,71,87,78,174,136,211,71,216,133,220,96,9,241,173,114,60,253,12,18,190,249,50,58,135,188,77,224,42,128,91,242,117,56,107,197,24,58,143,93,52,151,12,95,200,121,164,214,232,172,253,11,230,130,69,79,241,103,8,238,39,92,68,23,8,44,212,30,232,13,109,129,212,59,92,253,99,113,62,204,8,180,126,76,135,79,253,241,46,197,40,58,69,191,172,229,47,130,30,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e5e79c3a-7d9a-42b3-9e61-5839136f3c4b"));
		}

		#endregion

	}

	#endregion

}

