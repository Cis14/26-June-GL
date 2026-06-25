namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IImportParamsGeneratorSchema

	/// <exclude/>
	public class IImportParamsGeneratorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IImportParamsGeneratorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IImportParamsGeneratorSchema(IImportParamsGeneratorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("36bfcac9-e88e-4dbb-b7ce-a67ca5579bb2");
			Name = "IImportParamsGenerator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("9d05c8ee-17e3-41aa-adfe-7e36f0a4c27c");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,205,82,177,110,194,48,16,157,131,196,63,156,96,105,37,148,236,37,205,66,41,98,168,132,4,234,238,134,35,88,138,237,232,236,32,161,170,255,94,231,28,2,133,146,165,75,167,196,231,119,239,222,123,62,45,20,218,74,228,8,27,36,18,214,236,92,60,51,122,39,139,154,132,147,70,15,7,159,195,65,84,91,169,11,88,31,173,67,229,239,203,18,243,230,210,198,11,212,72,50,159,118,152,59,52,241,171,44,113,169,42,67,238,119,44,97,60,215,78,58,137,214,3,60,100,76,88,248,70,88,106,135,180,243,10,159,96,25,8,86,130,132,178,60,89,56,67,140,78,146,4,82,91,43,37,232,152,181,231,21,153,131,220,162,5,133,110,111,182,22,118,134,64,50,5,84,204,1,69,32,105,4,158,72,146,11,150,170,254,40,101,14,242,36,225,174,130,168,9,169,147,236,7,87,72,141,21,214,118,35,142,11,11,116,22,220,30,33,55,101,173,52,28,68,89,163,141,59,124,114,221,144,50,162,59,110,122,90,207,200,23,201,15,229,121,82,235,200,135,62,129,240,205,96,198,205,239,220,11,172,63,42,144,95,39,250,10,186,199,168,183,193,82,123,110,253,189,133,60,155,82,143,61,14,7,131,71,142,27,125,138,189,6,25,5,218,35,159,71,193,211,40,107,108,182,254,210,132,1,191,227,177,89,158,227,58,223,163,18,161,43,84,192,114,233,182,153,208,213,164,109,150,38,167,191,230,234,226,125,89,110,231,227,92,122,152,95,140,130,203,185,19,232,137,59,152,120,156,246,174,196,223,50,251,15,25,156,12,94,237,78,216,168,159,69,95,251,6,56,73,34,114,127,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("36bfcac9-e88e-4dbb-b7ce-a67ca5579bb2"));
		}

		#endregion

	}

	#endregion

}

