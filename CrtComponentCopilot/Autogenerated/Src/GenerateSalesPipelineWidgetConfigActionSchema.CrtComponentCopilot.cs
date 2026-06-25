namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GenerateSalesPipelineWidgetConfigActionSchema

	/// <exclude/>
	public class GenerateSalesPipelineWidgetConfigActionSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GenerateSalesPipelineWidgetConfigActionSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GenerateSalesPipelineWidgetConfigActionSchema(GenerateSalesPipelineWidgetConfigActionSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("4a7c9e2f-8b1d-4f3a-9c5e-7d2a8f4b6e1c");
			Name = "GenerateSalesPipelineWidgetConfigAction";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("6859eba8-9d49-4a99-92b8-45e03befab3b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,144,93,75,195,48,20,134,239,11,253,15,135,94,109,55,254,0,135,130,86,25,136,200,160,162,215,103,233,187,26,76,79,74,146,58,80,252,239,166,203,218,201,68,240,35,4,114,113,78,158,247,67,184,133,239,88,129,74,7,14,218,158,148,182,237,172,64,66,105,59,109,108,200,179,183,60,163,120,122,175,165,161,123,56,199,222,110,194,176,216,90,89,228,89,26,119,253,218,104,69,202,176,247,180,132,192,113,64,197,6,126,165,59,24,45,120,212,117,131,136,149,141,110,46,84,212,18,58,165,75,246,24,183,31,52,182,215,6,109,20,79,243,68,222,235,239,68,156,13,80,1,53,217,151,104,68,215,32,31,220,224,235,198,91,169,212,19,90,190,139,145,232,236,156,138,111,197,15,187,197,100,255,167,244,21,171,103,110,48,137,148,46,28,55,118,4,77,189,76,196,91,171,216,232,87,94,27,84,9,190,140,190,184,27,242,206,230,159,195,14,199,33,244,78,72,176,253,250,113,86,140,197,209,46,42,141,89,41,133,165,148,150,82,151,197,124,113,64,191,255,218,225,21,188,114,250,95,46,253,95,108,238,159,120,63,0,155,190,249,113,171,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("4a7c9e2f-8b1d-4f3a-9c5e-7d2a8f4b6e1c"));
		}

		#endregion

	}

	#endregion

}

