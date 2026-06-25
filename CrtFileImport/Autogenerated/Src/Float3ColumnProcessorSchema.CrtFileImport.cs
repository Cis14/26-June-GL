namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: Float3ColumnProcessorSchema

	/// <exclude/>
	public class Float3ColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public Float3ColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public Float3ColumnProcessorSchema(Float3ColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8ac9f761-d47f-4b4c-a8e7-4a1491cda0d9");
			Name = "Float3ColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("aaf0cd3b-b0e9-4378-a805-7163759e3c5e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,75,195,64,16,133,207,21,252,15,67,189,180,32,201,193,155,182,5,173,84,122,17,65,235,69,60,108,55,147,58,144,236,198,217,93,161,136,255,221,201,166,209,38,86,193,83,119,135,55,239,205,124,217,26,85,162,171,148,70,120,64,102,229,108,238,147,185,53,57,109,2,43,79,214,36,11,42,112,89,86,150,253,241,209,251,241,209,32,56,50,27,184,223,58,143,229,197,215,125,191,155,241,183,122,178,80,218,91,38,116,162,16,205,9,227,70,50,96,94,40,231,206,97,81,88,229,207,230,182,8,165,185,99,171,209,57,203,81,152,166,41,76,200,188,32,147,207,172,6,205,152,79,135,81,223,147,15,211,89,171,119,161,44,21,111,219,251,165,1,50,206,43,35,203,218,28,252,11,57,208,117,48,200,129,133,130,53,142,214,5,66,110,25,170,198,175,94,161,153,10,116,204,129,55,85,4,116,73,155,145,238,133,60,93,99,174,66,225,175,200,100,210,56,242,219,10,109,62,90,246,38,28,159,194,173,80,135,41,24,249,17,193,193,181,199,227,103,177,172,194,186,32,189,27,243,160,14,118,216,126,80,27,188,71,114,223,140,101,61,207,161,230,47,168,239,162,113,163,248,47,220,31,116,99,97,206,168,60,186,46,99,33,32,74,196,125,207,254,6,98,154,124,185,166,125,219,73,165,88,149,17,213,116,24,28,178,236,97,80,215,79,115,56,91,201,93,62,76,91,72,38,105,84,199,230,29,186,131,145,163,85,199,8,186,190,99,97,186,86,14,71,253,114,253,252,7,31,59,172,104,178,134,108,23,179,100,84,200,94,158,248,121,125,246,210,139,217,95,156,175,36,233,31,152,175,149,87,205,35,108,232,6,67,175,114,166,12,141,167,156,144,127,97,89,181,179,128,125,147,255,164,232,225,38,80,22,253,30,107,187,7,113,91,45,51,152,206,186,181,164,33,216,215,93,28,196,208,192,233,22,63,62,1,68,53,113,75,100,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8ac9f761-d47f-4b4c-a8e7-4a1491cda0d9"));
		}

		#endregion

	}

	#endregion

}

