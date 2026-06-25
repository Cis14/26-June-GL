namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ResendUnprocessedWebhooksResponseSchema

	/// <exclude/>
	public class ResendUnprocessedWebhooksResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ResendUnprocessedWebhooksResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ResendUnprocessedWebhooksResponseSchema(ResendUnprocessedWebhooksResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0885ef7c-db46-462f-9cdc-c917447e8450");
			Name = "ResendUnprocessedWebhooksResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,77,141,65,10,194,48,16,69,215,6,114,135,57,65,179,23,17,212,27,84,197,117,154,78,106,176,205,132,153,70,145,226,221,45,18,171,155,129,255,63,239,77,180,3,74,178,14,225,132,204,86,200,143,213,129,162,15,93,102,59,6,138,90,77,90,173,140,49,176,145,60,12,150,159,219,146,235,25,164,40,8,174,183,34,224,137,129,81,48,182,33,118,144,99,98,114,40,130,45,60,176,185,18,221,164,250,138,204,159,41,229,166,15,174,56,234,15,127,254,177,151,130,46,191,214,176,183,130,165,62,34,223,131,195,93,10,203,62,193,75,43,173,230,243,6,127,91,173,249,218,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0885ef7c-db46-462f-9cdc-c917447e8450"));
		}

		#endregion

	}

	#endregion

}

