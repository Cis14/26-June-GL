namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: WebTextColumnProcessorSchema

	/// <exclude/>
	public class WebTextColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public WebTextColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public WebTextColumnProcessorSchema(WebTextColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("12ec7f7e-14ab-4185-85fd-238e9d07bc1e");
			Name = "WebTextColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,107,227,48,16,133,207,41,244,63,12,217,75,2,197,190,167,73,160,77,105,9,148,165,176,73,123,40,123,80,228,113,42,176,37,119,70,42,27,202,254,247,29,203,118,91,103,221,66,79,150,134,55,239,105,62,201,86,149,200,149,210,8,27,36,82,236,114,159,172,156,205,205,62,144,242,198,217,228,218,20,184,46,43,71,254,244,228,245,244,100,20,216,216,61,252,58,176,199,242,252,109,255,177,155,240,179,122,114,173,180,119,100,144,69,33,154,31,132,123,201,128,85,161,152,103,240,128,187,13,254,241,43,87,132,210,222,145,211,200,236,40,42,211,52,133,185,177,79,72,198,103,78,131,38,204,23,227,1,245,56,93,118,114,14,101,169,232,208,237,47,44,24,203,94,89,25,214,229,224,159,12,131,174,131,65,22,36,20,156,101,179,43,16,114,71,80,53,126,245,8,183,206,238,235,32,208,49,9,94,84,17,144,147,46,37,253,16,243,120,133,185,10,133,191,52,54,147,214,137,63,84,232,242,201,250,232,140,211,51,248,41,220,97,1,86,62,34,24,30,124,58,253,45,158,85,216,21,70,183,39,29,22,194,12,6,185,141,94,35,187,119,204,50,161,167,80,95,129,208,190,139,198,141,226,155,120,255,227,27,11,43,66,229,145,251,148,133,128,40,17,91,203,225,1,196,53,121,179,77,143,125,231,149,34,85,70,86,139,113,96,36,153,195,162,174,95,231,120,185,149,189,220,76,87,72,230,105,84,199,230,22,221,112,230,100,219,115,130,190,241,84,152,238,20,227,228,184,92,255,2,163,191,45,87,180,89,131,182,207,89,50,42,36,47,207,124,86,175,189,244,98,246,21,232,75,73,250,6,232,43,229,85,243,12,27,190,193,154,103,89,155,12,173,55,185,65,250,4,102,213,157,5,220,139,252,151,162,135,155,96,178,232,119,95,219,109,196,109,187,206,96,177,236,215,146,22,225,177,240,124,144,67,67,167,95,148,218,63,219,188,221,195,107,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("12ec7f7e-14ab-4185-85fd-238e9d07bc1e"));
		}

		#endregion

	}

	#endregion

}

