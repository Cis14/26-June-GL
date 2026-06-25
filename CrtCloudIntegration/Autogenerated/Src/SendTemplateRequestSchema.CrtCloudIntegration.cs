namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: SendTemplateRequestSchema

	/// <exclude/>
	public class SendTemplateRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public SendTemplateRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public SendTemplateRequestSchema(SendTemplateRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1b1f76d4-1295-4ca5-bbc0-4faf38c61644");
			Name = "SendTemplateRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,148,193,78,227,64,12,134,207,32,241,14,86,184,192,37,185,83,64,130,130,16,135,34,68,123,91,173,42,55,113,194,72,73,38,140,29,86,221,138,119,199,51,105,160,45,85,161,156,18,123,198,255,204,247,59,78,141,21,113,131,41,193,132,156,67,182,185,196,67,91,231,166,104,29,138,177,117,60,188,29,143,108,70,37,31,29,46,142,14,15,90,54,117,1,227,57,11,85,131,141,56,126,106,107,49,21,197,99,114,6,75,243,63,40,232,46,221,119,236,168,208,0,134,37,50,159,193,152,234,108,66,85,83,162,208,19,189,180,196,18,182,37,73,2,231,220,86,21,186,249,229,50,14,37,144,91,7,174,219,9,98,129,85,0,102,40,233,51,216,92,23,82,211,24,170,133,97,54,7,89,10,199,189,96,178,161,120,206,68,88,178,133,212,81,126,17,125,139,30,95,35,147,66,189,154,180,191,109,4,137,87,251,115,131,130,90,37,14,83,249,171,137,166,157,149,38,133,52,220,121,11,37,156,193,87,49,173,91,4,252,15,155,30,157,109,200,137,33,245,234,49,72,118,235,155,254,132,196,29,41,183,218,195,254,41,207,4,84,161,41,65,59,203,88,4,23,190,218,208,101,94,177,108,233,35,156,104,169,183,102,105,203,173,87,25,117,34,81,114,185,162,243,89,22,248,71,84,205,200,157,60,232,183,4,23,16,45,207,141,78,189,31,189,33,171,98,208,63,23,80,144,12,252,189,7,240,182,15,96,237,143,210,190,251,247,213,110,255,156,243,27,133,221,132,125,193,212,171,172,115,178,56,63,16,125,215,67,197,175,49,187,62,154,76,63,108,147,27,114,251,33,238,168,222,141,23,10,167,38,91,39,187,107,77,214,181,241,62,251,61,146,255,63,120,223,253,252,170,81,251,17,101,234,40,160,14,126,80,233,187,0,255,144,189,158,0,202,143,25,253,249,83,148,173,205,243,99,123,37,219,24,143,117,165,155,208,16,119,217,245,164,230,222,1,75,133,142,225,85,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1b1f76d4-1295-4ca5-bbc0-4faf38c61644"));
		}

		#endregion

	}

	#endregion

}

