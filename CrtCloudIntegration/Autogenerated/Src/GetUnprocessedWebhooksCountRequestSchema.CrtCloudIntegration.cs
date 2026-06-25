namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GetUnprocessedWebhooksCountRequestSchema

	/// <exclude/>
	public class GetUnprocessedWebhooksCountRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GetUnprocessedWebhooksCountRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GetUnprocessedWebhooksCountRequestSchema(GetUnprocessedWebhooksCountRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("d68002ce-5410-4bcd-81b7-2df6c7f2adca");
			Name = "GetUnprocessedWebhooksCountRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,145,77,107,195,48,12,134,207,13,228,63,8,122,79,238,107,9,140,30,122,216,14,97,237,216,217,117,149,204,52,177,61,73,102,132,178,255,62,59,117,183,194,198,216,69,160,143,231,213,43,219,170,17,217,43,141,176,71,34,197,174,147,106,227,108,103,250,64,74,140,179,101,113,46,139,69,96,99,123,216,77,44,56,198,254,48,160,78,77,174,182,104,145,140,94,149,69,156,170,235,26,214,28,198,81,209,212,228,252,9,223,2,178,128,30,20,51,116,142,160,71,145,164,22,172,39,167,145,25,143,240,142,135,87,231,78,12,218,5,43,213,85,171,190,17,243,225,48,24,157,117,182,40,207,223,248,75,166,55,9,206,11,35,145,140,47,150,132,125,116,10,45,57,143,36,6,249,14,218,89,106,182,252,195,243,92,200,130,112,239,205,3,78,213,215,220,173,159,171,161,71,195,178,102,161,120,82,147,1,134,115,186,114,5,156,194,199,31,139,90,36,142,188,178,233,3,38,143,252,143,93,217,92,70,49,162,137,108,46,252,111,155,151,104,143,151,87,152,243,185,26,195,39,118,6,173,208,251,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("d68002ce-5410-4bcd-81b7-2df6c7f2adca"));
		}

		#endregion

	}

	#endregion

}

