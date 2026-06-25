namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: Float4ColumnProcessorSchema

	/// <exclude/>
	public class Float4ColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public Float4ColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public Float4ColumnProcessorSchema(Float4ColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e45599cf-db78-4a8d-b5e2-31927002be8f");
			Name = "Float4ColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("560ff4eb-7ab5-4d8f-8733-4031e1e3144b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,193,75,235,64,16,198,207,21,252,31,134,122,105,65,146,131,239,164,109,65,43,149,94,30,194,179,239,34,30,182,155,73,93,72,118,227,236,172,80,196,255,221,201,166,169,77,172,130,167,238,14,223,124,223,204,47,91,171,74,244,149,210,8,15,72,164,188,203,57,153,59,155,155,77,32,197,198,217,100,97,10,92,150,149,35,62,61,121,59,61,25,4,111,236,6,254,109,61,99,121,181,191,31,118,19,126,87,79,22,74,179,35,131,94,20,162,57,35,220,72,6,204,11,229,253,37,44,10,167,248,98,238,138,80,218,123,114,26,189,119,20,133,105,154,194,196,216,103,36,195,153,211,160,9,243,233,48,234,123,242,97,58,107,245,62,148,165,162,109,123,191,182,96,172,103,101,101,89,151,3,63,27,15,186,14,6,57,144,80,112,214,155,117,129,144,59,130,170,241,171,87,104,166,2,29,115,224,85,21,1,125,210,102,164,7,33,143,183,152,171,80,240,141,177,153,52,142,120,91,161,203,71,203,222,132,227,115,248,43,212,97,10,86,126,68,16,3,254,244,85,227,39,177,172,194,186,48,122,55,230,81,29,236,176,125,161,54,120,139,228,62,25,203,122,76,161,230,47,168,239,163,113,163,248,45,220,47,116,99,97,78,168,24,125,151,177,16,16,37,226,161,103,127,3,49,77,246,174,105,223,118,82,41,82,101,68,53,29,6,143,36,123,88,212,245,211,28,206,86,114,151,15,211,22,146,73,26,213,177,121,135,238,104,228,104,213,49,130,174,239,88,152,174,149,199,81,191,92,63,255,193,251,14,43,218,172,33,219,197,44,25,21,18,203,19,191,172,207,44,189,152,253,196,249,70,146,126,129,249,86,177,106,30,97,67,55,88,243,34,103,147,161,101,147,27,164,111,88,86,237,44,224,94,229,63,41,122,184,11,38,139,126,255,107,187,7,113,91,45,51,152,206,186,181,164,33,216,215,93,29,197,208,192,233,22,223,63,0,14,83,15,101,100,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e45599cf-db78-4a8d-b5e2-31927002be8f"));
		}

		#endregion

	}

	#endregion

}

