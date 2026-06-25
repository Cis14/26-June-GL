namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: TokenInfosResponseSchema

	/// <exclude/>
	public class TokenInfosResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public TokenInfosResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public TokenInfosResponseSchema(TokenInfosResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5c5f2197-25b9-40c8-8034-e8561b25d0ff");
			Name = "TokenInfosResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("f3e00ac6-0422-4cac-8e36-b64e7b099372");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,147,77,75,195,64,16,134,207,22,250,31,134,122,209,75,114,183,165,160,65,36,66,165,216,222,196,195,38,153,132,197,205,110,216,217,69,66,232,127,119,63,210,15,17,161,135,92,178,204,188,51,243,190,60,16,201,90,164,142,149,8,153,54,153,80,182,202,165,193,70,51,195,149,76,54,170,66,65,201,187,27,81,146,144,230,179,97,62,187,177,196,101,3,111,248,109,92,83,213,38,121,37,37,151,39,97,215,147,193,54,201,148,16,88,250,51,148,188,160,68,205,203,243,204,30,181,102,97,55,83,178,230,141,141,134,110,192,141,220,106,108,92,1,153,96,68,15,176,87,95,40,115,89,43,10,106,154,166,176,34,219,182,76,247,235,177,222,197,18,184,155,2,86,40,107,192,248,45,40,122,232,4,51,181,210,45,48,89,1,235,58,193,203,224,117,60,149,94,220,234,108,225,100,40,189,241,217,23,46,50,60,49,66,55,56,132,40,167,164,91,173,58,212,134,163,139,187,13,55,162,254,225,201,140,98,127,183,8,153,22,247,159,94,26,173,200,232,192,35,164,29,160,65,179,4,242,159,195,232,128,178,138,38,174,138,189,203,214,164,180,104,26,92,52,45,47,250,13,44,127,150,182,69,205,10,129,171,8,111,29,221,104,90,124,49,248,63,4,243,51,185,13,227,18,88,89,42,43,205,245,140,252,117,135,201,63,143,113,247,248,143,193,240,39,230,124,118,248,1,173,61,87,201,167,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5c5f2197-25b9-40c8-8034-e8561b25d0ff"));
		}

		#endregion

	}

	#endregion

}

