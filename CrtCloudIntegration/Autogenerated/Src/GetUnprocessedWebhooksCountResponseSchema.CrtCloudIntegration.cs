namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GetUnprocessedWebhooksCountResponseSchema

	/// <exclude/>
	public class GetUnprocessedWebhooksCountResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GetUnprocessedWebhooksCountResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GetUnprocessedWebhooksCountResponseSchema(GetUnprocessedWebhooksCountResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("c319249e-1752-4b69-9a38-baced1951831");
			Name = "GetUnprocessedWebhooksCountResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,80,77,107,195,48,12,61,55,144,255,32,216,117,36,247,182,20,182,14,122,13,107,199,206,174,171,100,166,137,109,36,187,37,132,253,247,217,174,211,149,177,131,5,214,147,222,135,180,24,144,173,144,8,7,36,18,108,90,87,109,141,110,85,231,73,56,101,116,89,76,101,177,240,172,116,7,251,145,29,14,1,239,123,148,17,228,106,135,26,73,201,85,89,132,169,186,174,97,205,126,24,4,141,155,252,127,15,244,97,16,65,246,130,25,90,67,208,161,115,145,206,107,75,70,34,51,158,224,138,199,47,99,206,12,210,120,237,170,153,172,126,96,179,254,216,43,153,121,118,232,62,126,215,63,243,246,54,46,223,21,151,240,42,24,51,182,71,186,40,137,47,86,205,120,160,156,146,237,197,19,97,23,210,64,67,198,34,57,133,188,132,38,169,69,52,190,191,201,82,35,169,129,105,255,13,82,221,215,30,51,204,33,222,84,186,95,232,174,179,191,6,137,85,56,175,150,120,24,45,62,67,111,116,183,185,105,48,76,241,104,43,224,88,190,179,105,212,167,155,239,244,79,221,80,126,0,76,76,201,54,209,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("c319249e-1752-4b69-9a38-baced1951831"));
		}

		#endregion

	}

	#endregion

}

