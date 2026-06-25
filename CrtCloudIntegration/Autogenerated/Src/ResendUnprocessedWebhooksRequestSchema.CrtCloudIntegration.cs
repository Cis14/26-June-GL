namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ResendUnprocessedWebhooksRequestSchema

	/// <exclude/>
	public class ResendUnprocessedWebhooksRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ResendUnprocessedWebhooksRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ResendUnprocessedWebhooksRequestSchema(ResendUnprocessedWebhooksRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("b1dcde58-fd6a-4fef-900b-357592cdf353");
			Name = "ResendUnprocessedWebhooksRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,144,65,75,196,48,16,133,207,91,232,127,24,216,123,123,119,151,130,120,240,160,135,178,42,158,179,217,215,26,108,147,58,147,32,101,241,191,155,116,179,186,160,136,151,129,201,204,247,222,203,88,53,66,38,165,65,143,96,86,226,58,95,221,56,219,153,62,176,242,198,217,178,56,150,197,42,136,177,61,61,204,226,49,198,249,48,64,167,161,84,183,176,96,163,55,101,17,183,234,186,166,173,132,113,84,60,55,185,223,225,45,64,60,233,65,137,80,231,152,24,2,123,72,122,193,78,236,52,68,112,160,119,236,95,156,123,149,234,172,83,95,8,77,97,63,24,157,53,118,11,255,244,205,62,103,52,91,197,253,20,121,181,102,244,49,35,181,236,38,176,55,144,43,106,23,161,37,236,143,180,203,67,214,162,235,201,220,97,174,190,246,46,211,156,227,220,27,241,91,241,28,191,210,100,64,232,72,61,252,134,36,149,143,63,140,90,176,68,94,217,116,250,121,130,252,195,43,135,203,40,34,154,200,230,196,255,230,188,142,135,58,93,97,233,151,215,88,62,1,143,70,161,106,245,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("b1dcde58-fd6a-4fef-900b-357592cdf353"));
		}

		#endregion

	}

	#endregion

}

