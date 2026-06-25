namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FiscalPeriodTypeConstsSchema

	/// <exclude/>
	public class FiscalPeriodTypeConstsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FiscalPeriodTypeConstsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FiscalPeriodTypeConstsSchema(FiscalPeriodTypeConstsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a1c3d5e7-89ab-4cde-b012-3456789abcde");
			Name = "FiscalPeriodTypeConsts";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("9c4a2d44-6a8a-4c3e-b0e9-b9f3c1ad8c4e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,211,203,110,212,48,20,6,224,117,34,229,29,172,118,3,11,207,196,137,147,216,20,216,84,2,186,64,42,162,18,98,233,203,57,83,139,92,6,59,81,53,66,125,119,156,73,65,83,52,211,205,176,116,146,243,231,203,111,103,10,174,223,144,175,187,48,66,119,149,165,89,218,171,14,194,86,25,32,119,224,189,10,3,142,171,235,161,71,183,153,188,26,221,208,175,174,253,248,193,5,163,218,91,240,110,176,33,75,127,101,105,178,94,175,201,219,48,117,157,242,187,247,79,235,56,55,42,215,7,226,44,244,163,67,7,62,144,1,201,50,120,183,219,2,241,96,6,111,3,153,2,88,130,131,39,184,143,38,219,37,123,245,39,121,125,16,189,157,116,235,12,9,99,244,24,98,90,21,2,57,20,205,193,241,213,97,140,180,100,182,37,151,30,54,145,30,159,130,214,134,55,228,118,159,48,127,110,242,28,190,196,144,29,40,127,168,188,177,171,103,130,127,8,30,148,29,250,118,71,62,78,206,62,81,190,199,136,121,246,198,146,119,164,135,135,253,189,87,23,5,8,68,97,106,90,107,166,40,199,210,80,9,181,161,141,150,86,168,18,185,102,236,226,245,213,105,218,207,73,249,17,206,213,125,89,82,142,0,77,174,56,195,134,209,74,9,67,185,173,25,21,150,115,202,77,89,131,0,173,5,20,47,2,187,184,233,247,103,242,62,207,25,71,112,82,231,34,47,84,212,8,94,81,14,5,80,93,88,65,115,45,145,25,148,88,151,229,139,184,7,128,31,103,218,190,197,136,35,52,46,160,212,37,143,109,73,214,196,222,116,69,165,172,24,69,44,68,45,115,214,216,210,236,105,73,114,10,119,175,90,252,31,71,239,83,204,57,113,252,48,238,98,14,134,209,130,53,205,92,96,77,85,133,146,54,13,47,132,136,53,106,171,255,22,120,9,189,93,126,156,184,122,204,210,199,249,242,111,227,93,148,51,49,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a1c3d5e7-89ab-4cde-b012-3456789abcde"));
		}

		#endregion

	}

	#endregion

}

