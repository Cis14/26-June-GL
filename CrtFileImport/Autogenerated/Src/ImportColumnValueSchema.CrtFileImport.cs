namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ImportColumnValueSchema

	/// <exclude/>
	public class ImportColumnValueSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ImportColumnValueSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ImportColumnValueSchema(ImportColumnValueSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("f8ae54ba-7ca1-412e-af1f-79f7b6e20f3d");
			Name = "ImportColumnValue";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("52abf94a-4a51-4e9b-afae-86480a04ff1e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,84,77,107,219,64,16,61,43,144,255,48,40,23,155,22,233,94,71,130,98,19,200,161,37,56,37,151,210,195,90,30,169,11,171,93,49,187,106,234,26,255,247,238,135,36,75,177,99,156,139,97,231,227,205,123,51,79,150,172,70,221,176,2,225,7,18,49,173,74,147,44,149,44,121,213,18,51,92,201,228,129,11,124,172,27,69,230,246,102,127,123,19,181,154,203,10,158,119,218,96,189,120,243,78,86,156,85,82,105,195,11,125,146,91,183,210,240,26,147,103,36,206,4,255,231,225,109,149,173,187,35,172,236,3,150,130,105,253,5,194,184,165,18,109,45,95,152,104,209,23,165,105,10,247,186,173,107,70,187,188,123,127,149,192,165,54,76,90,1,170,4,243,155,107,40,28,8,16,54,132,26,165,209,192,61,28,20,30,15,254,56,192,164,199,75,71,128,63,87,184,105,171,10,105,197,117,35,216,110,22,251,225,217,62,134,79,32,237,162,84,57,243,145,185,125,199,135,120,254,203,53,13,114,54,2,93,160,105,55,130,23,29,139,51,74,162,189,87,51,104,126,224,40,182,86,244,147,239,11,185,183,82,125,32,160,88,189,91,252,155,12,85,99,1,86,1,51,236,27,214,27,164,217,119,75,24,50,136,131,236,71,215,21,24,247,12,181,33,119,157,229,49,191,184,48,125,173,94,63,60,154,212,235,251,115,215,93,178,27,122,135,114,27,54,50,93,207,19,169,6,201,112,188,102,69,47,253,109,175,36,232,189,112,150,157,71,130,61,84,104,22,160,221,207,225,50,79,251,209,216,214,182,48,138,174,58,38,33,51,168,167,238,221,53,104,43,17,161,32,44,179,248,196,60,113,154,191,35,206,71,26,70,172,246,70,205,58,101,121,88,200,125,234,83,231,43,199,254,200,39,30,187,216,54,220,54,63,58,99,220,208,173,243,68,195,172,91,176,39,248,185,95,247,136,196,16,235,39,184,67,197,115,112,255,61,81,52,114,171,141,23,19,239,218,116,56,91,22,208,67,104,125,132,161,163,229,162,232,204,61,187,216,244,196,135,255,47,130,101,47,35,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("f8ae54ba-7ca1-412e-af1f-79f7b6e20f3d"));
		}

		#endregion

	}

	#endregion

}

