namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: BaseWebhookServiceApiResponseSchema

	/// <exclude/>
	public class BaseWebhookServiceApiResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public BaseWebhookServiceApiResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public BaseWebhookServiceApiResponseSchema(BaseWebhookServiceApiResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a401f71a-5d4b-4161-b971-d3a3cb3eca4a");
			Name = "BaseWebhookServiceApiResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,93,143,193,106,195,48,12,134,207,13,228,29,4,189,39,247,181,20,186,189,64,105,7,61,59,238,159,204,44,177,92,201,222,40,101,239,190,56,105,70,217,69,32,249,215,231,79,228,205,0,13,198,130,222,33,98,148,219,88,189,177,111,93,151,196,68,199,190,44,238,101,177,170,235,154,182,154,134,193,200,109,247,232,143,184,38,104,36,219,27,85,106,89,168,67,140,206,119,148,124,16,182,80,197,133,190,209,124,48,127,42,89,78,62,86,11,171,126,130,133,212,244,206,62,56,175,70,113,158,119,78,144,47,103,177,15,238,56,58,178,87,140,225,108,179,90,11,186,209,141,14,194,1,18,29,244,133,14,19,165,44,242,251,127,221,105,112,74,54,59,85,127,129,103,135,69,162,97,238,151,36,221,243,69,27,210,92,126,102,242,26,254,50,127,62,245,211,116,44,191,132,10,64,126,72,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a401f71a-5d4b-4161-b971-d3a3cb3eca4a"));
		}

		#endregion

	}

	#endregion

}

