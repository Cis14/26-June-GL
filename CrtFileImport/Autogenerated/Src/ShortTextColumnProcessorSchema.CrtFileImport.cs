namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ShortTextColumnProcessorSchema

	/// <exclude/>
	public class ShortTextColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ShortTextColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ShortTextColumnProcessorSchema(ShortTextColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0e75194b-c61b-4a32-9829-7c88737e87a4");
			Name = "ShortTextColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("560ff4eb-7ab5-4d8f-8733-4031e1e3144b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,107,227,48,16,133,207,41,244,63,12,233,37,129,98,223,211,36,176,77,105,201,165,20,154,236,101,217,131,34,143,147,1,91,114,71,82,105,40,253,239,59,150,227,108,156,141,23,122,178,52,60,189,55,243,73,54,170,68,87,41,141,176,66,102,229,108,238,147,133,53,57,109,3,43,79,214,36,143,84,224,178,172,44,251,235,171,207,235,171,65,112,100,182,240,186,119,30,203,187,227,254,244,52,99,95,61,121,84,218,91,38,116,162,16,205,13,227,86,50,96,81,40,231,38,240,186,147,148,21,126,248,133,45,66,105,94,216,106,116,206,114,212,166,105,10,83,50,59,100,242,153,213,160,25,243,217,240,130,122,152,206,91,185,11,101,169,120,223,238,127,24,32,227,188,50,50,174,205,193,239,200,129,174,163,65,22,44,28,172,113,180,41,16,114,203,80,53,126,113,216,182,47,208,49,10,222,85,17,208,37,109,76,122,146,243,235,1,115,21,10,127,79,38,147,179,35,191,175,208,230,163,229,89,147,227,91,120,22,244,48,3,35,31,17,244,205,62,30,255,22,215,42,108,10,210,135,102,251,164,48,129,139,240,6,159,17,224,95,218,50,166,231,80,223,132,64,127,137,214,141,226,155,140,255,129,28,11,11,70,229,209,117,81,11,5,81,34,30,44,251,70,16,223,228,104,156,158,59,79,43,197,170,140,196,102,195,224,144,101,18,131,186,126,166,195,249,90,246,114,63,109,33,153,166,81,29,15,31,240,245,165,142,214,29,47,232,90,143,133,235,70,57,28,157,151,235,191,97,240,117,96,139,38,107,240,118,89,75,70,133,236,229,197,79,234,181,151,179,152,253,15,246,189,36,125,3,246,131,242,170,121,142,13,227,96,232,77,214,148,161,241,148,19,114,15,206,170,237,5,236,187,252,162,162,135,167,64,89,244,251,89,219,173,196,109,189,204,96,54,239,214,146,35,196,115,233,221,69,18,13,159,110,241,235,15,247,11,105,105,118,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0e75194b-c61b-4a32-9829-7c88737e87a4"));
		}

		#endregion

	}

	#endregion

}

