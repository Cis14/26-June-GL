namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: WebhookRejectionReasonSchema

	/// <exclude/>
	public class WebhookRejectionReasonSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public WebhookRejectionReasonSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public WebhookRejectionReasonSchema(WebhookRejectionReasonSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("387a80b3-6c65-46b4-bc40-f159e93c7cd5");
			Name = "WebhookRejectionReason";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,148,221,110,26,65,12,133,175,131,196,59,248,1,42,88,154,208,54,82,83,169,138,146,138,86,74,37,104,212,75,52,204,154,197,133,245,144,249,129,210,42,239,94,207,254,16,212,138,93,232,37,104,206,249,236,179,182,129,85,142,110,173,52,194,55,180,86,57,51,247,189,91,195,115,202,130,85,158,12,119,59,191,187,157,139,126,191,15,239,93,200,115,101,119,31,170,223,99,148,231,12,115,99,97,139,179,133,49,75,176,248,3,117,84,245,106,77,255,64,180,14,179,21,105,64,14,57,124,47,21,227,90,80,154,201,163,72,251,7,87,252,113,175,104,133,41,120,3,41,58,180,164,86,244,11,107,116,111,47,59,36,94,76,208,110,72,227,244,243,228,235,195,136,61,102,150,252,14,110,32,121,213,237,28,5,125,92,19,44,113,7,228,32,39,231,136,51,144,30,23,202,1,241,70,168,105,236,57,87,190,133,57,42,31,139,219,23,140,204,65,19,243,165,185,66,164,252,190,51,201,203,147,39,116,45,184,49,58,111,73,123,76,239,162,32,18,95,55,118,169,181,9,236,29,184,210,32,182,203,198,131,218,72,41,106,182,194,35,188,90,55,29,38,151,194,184,60,37,201,173,42,189,231,162,76,37,69,80,127,193,219,88,143,188,100,179,229,125,150,87,77,212,106,182,226,152,120,226,98,138,235,230,254,187,128,177,9,30,31,140,191,47,12,110,96,216,84,192,40,197,242,19,156,25,237,173,197,88,237,116,52,41,179,125,211,4,169,30,159,235,93,58,191,61,109,22,85,240,139,216,138,62,156,71,139,79,65,146,141,41,86,166,45,200,171,100,32,200,119,45,195,136,206,129,165,108,33,223,68,150,46,224,57,254,177,165,235,38,255,71,198,159,107,140,203,1,114,230,100,155,79,54,31,38,73,220,221,198,131,241,9,89,238,145,134,80,78,169,36,212,124,25,11,209,221,83,32,217,117,137,55,38,125,112,104,2,91,212,38,99,185,110,105,101,117,164,198,106,43,98,125,3,249,253,220,237,60,255,1,15,185,222,211,209,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("387a80b3-6c65-46b4-bc40-f159e93c7cd5"));
		}

		#endregion

	}

	#endregion

}

