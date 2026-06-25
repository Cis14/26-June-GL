namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GetUnprocessedWebhooksByReasonCountResponseSchema

	/// <exclude/>
	public class GetUnprocessedWebhooksByReasonCountResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GetUnprocessedWebhooksByReasonCountResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GetUnprocessedWebhooksByReasonCountResponseSchema(GetUnprocessedWebhooksByReasonCountResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("22781357-2943-4b79-a6f8-130b1658c02e");
			Name = "GetUnprocessedWebhooksByReasonCountResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,80,77,107,195,48,12,61,55,144,255,32,216,117,36,247,182,20,214,14,122,45,233,198,206,142,171,100,238,28,59,72,246,70,8,251,239,115,28,167,43,99,23,129,244,164,247,33,48,162,67,238,133,68,120,65,34,193,182,113,197,193,154,70,181,158,132,83,214,228,217,152,103,43,207,202,180,112,30,216,97,23,112,173,81,78,32,23,71,52,72,74,110,242,44,108,149,101,9,91,246,93,39,104,216,165,190,10,244,97,17,65,106,193,12,141,37,104,209,185,137,206,155,158,172,68,102,188,192,23,214,239,214,126,48,72,235,141,131,70,105,135,20,230,245,0,132,193,150,41,22,254,242,78,160,247,181,86,50,81,31,209,189,254,50,190,37,194,253,80,197,251,195,196,123,51,179,134,189,96,76,59,103,164,79,37,241,169,87,11,30,168,199,152,104,245,64,216,134,160,112,34,219,35,57,133,188,134,83,84,157,241,191,145,227,32,106,129,109,254,77,88,220,206,238,147,44,81,158,85,124,108,152,110,147,187,10,175,243,179,231,28,143,160,173,105,119,179,6,195,56,125,115,3,60,149,239,100,25,205,101,118,29,251,56,13,229,7,85,71,26,125,235,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("22781357-2943-4b79-a6f8-130b1658c02e"));
		}

		#endregion

	}

	#endregion

}

