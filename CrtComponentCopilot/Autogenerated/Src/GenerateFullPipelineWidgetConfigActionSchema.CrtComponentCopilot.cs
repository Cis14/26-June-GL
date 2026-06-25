namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GenerateFullPipelineWidgetConfigActionSchema

	/// <exclude/>
	public class GenerateFullPipelineWidgetConfigActionSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GenerateFullPipelineWidgetConfigActionSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GenerateFullPipelineWidgetConfigActionSchema(GenerateFullPipelineWidgetConfigActionSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("b8e4f2c9-7a1d-4e65-9f3b-2c8a5e7d9b41");
			Name = "GenerateFullPipelineWidgetConfigAction";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("6859eba8-9d49-4a99-92b8-45e03befab3b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,144,205,74,3,49,16,199,239,133,125,135,97,79,237,197,7,176,40,232,170,5,17,41,84,244,60,205,78,215,193,236,76,72,178,22,20,223,221,172,233,110,165,34,248,17,2,57,204,228,247,255,16,108,41,56,52,4,149,39,140,172,71,149,182,78,133,36,86,234,216,106,44,38,175,197,4,210,233,2,75,3,119,228,61,6,221,196,126,177,85,153,23,147,60,118,221,218,178,1,99,49,4,88,144,144,199,72,87,157,181,75,118,100,89,232,129,235,134,18,85,54,220,156,153,36,37,112,12,231,24,104,88,190,103,218,94,90,106,147,118,158,103,240,78,254,67,195,107,36,19,169,6,125,78,62,184,38,8,209,247,182,174,131,202,202,60,82,139,183,41,17,156,156,66,249,157,246,126,181,28,205,255,20,190,68,243,132,13,141,26,149,143,135,125,29,64,115,43,35,241,70,13,90,126,193,181,165,85,134,47,146,47,116,125,220,233,236,115,214,254,120,138,157,23,16,218,126,253,56,45,135,222,160,79,10,67,84,200,89,33,135,133,220,100,57,155,239,201,111,191,54,120,65,193,120,254,151,201,240,7,151,187,39,221,119,169,101,170,227,167,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("b8e4f2c9-7a1d-4e65-9f3b-2c8a5e7d9b41"));
		}

		#endregion

	}

	#endregion

}

