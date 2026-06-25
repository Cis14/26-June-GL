namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: AccountInfoRequestSchema

	/// <exclude/>
	public class AccountInfoRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public AccountInfoRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public AccountInfoRequestSchema(AccountInfoRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("d34c2d1e-3df3-49cb-81c1-5fa5d286583e");
			Name = "AccountInfoRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,81,193,78,195,48,12,61,175,82,255,193,26,23,184,180,247,109,32,141,129,16,66,67,211,186,27,226,224,101,94,137,104,147,18,59,147,202,196,191,147,166,172,8,132,196,37,145,95,222,243,243,115,12,214,196,13,42,130,13,57,135,108,247,146,45,172,217,235,210,59,20,109,77,182,184,45,150,118,71,21,167,201,49,77,210,100,228,89,155,18,138,150,133,234,108,237,141,232,154,178,130,156,198,74,191,71,205,52,242,206,28,149,161,128,69,133,204,19,152,43,101,3,249,222,236,237,154,222,60,177,68,214,211,13,10,6,71,113,168,228,57,0,141,223,86,90,129,234,84,127,136,96,2,215,200,20,252,14,90,209,208,105,212,207,54,152,174,156,109,200,137,166,224,188,138,29,251,247,60,207,97,198,190,174,209,181,87,39,224,142,132,193,58,224,238,150,23,2,244,225,12,193,84,140,3,175,212,102,131,56,255,173,158,29,176,242,52,148,155,255,244,223,244,152,125,73,245,150,220,249,99,248,8,184,132,113,39,125,160,118,124,209,237,226,180,12,22,215,237,124,222,191,193,17,74,146,105,55,238,20,62,190,114,147,217,245,209,99,221,163,63,193,128,125,2,174,237,37,248,238,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("d34c2d1e-3df3-49cb-81c1-5fa5d286583e"));
		}

		#endregion

	}

	#endregion

}

