namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: RootAccountInfoResponseSchema

	/// <exclude/>
	public class RootAccountInfoResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public RootAccountInfoResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public RootAccountInfoResponseSchema(RootAccountInfoResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8c2134ec-81df-40a8-823f-e035797122bd");
			Name = "RootAccountInfoResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("edc99e2c-9094-4ed6-903f-e907a7c24faf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,146,193,106,195,48,12,134,207,13,228,29,4,189,39,247,110,12,182,12,70,96,176,146,182,15,224,58,74,106,72,236,32,43,27,165,236,221,231,216,73,215,13,58,8,235,197,32,233,255,165,79,182,181,104,209,118,66,34,108,145,72,88,83,113,146,25,93,169,186,39,193,202,232,56,58,197,81,28,45,150,132,181,11,33,107,132,181,43,40,140,225,71,41,77,175,57,215,149,41,92,19,163,45,122,105,154,166,112,111,251,182,21,116,124,24,227,172,49,125,9,52,202,224,121,251,6,31,138,15,160,156,153,90,63,9,196,222,244,12,34,116,77,166,70,233,69,167,174,223,55,74,130,28,24,174,33,192,10,158,132,197,177,242,13,182,8,123,156,23,89,147,233,144,88,161,219,102,237,251,134,250,111,250,128,79,56,48,194,174,120,77,206,162,75,178,9,205,50,41,93,79,250,29,53,112,130,26,249,14,236,112,124,254,49,226,5,217,130,161,65,104,129,15,8,114,156,41,27,133,154,65,149,238,84,149,66,154,67,144,121,115,94,222,12,195,162,75,240,124,132,141,247,253,31,35,92,3,31,93,133,222,145,230,190,72,62,218,55,222,125,229,125,150,168,203,240,75,124,28,178,63,147,46,247,5,38,132,234,112,59,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8c2134ec-81df-40a8-823f-e035797122bd"));
		}

		#endregion

	}

	#endregion

}

