namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotWorkflowExecutionStatusSchema

	/// <exclude/>
	public class CopilotWorkflowExecutionStatusSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotWorkflowExecutionStatusSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotWorkflowExecutionStatusSchema(CopilotWorkflowExecutionStatusSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("b4badb85-7741-4647-93fd-5f0f29b0b658");
			Name = "CopilotWorkflowExecutionStatus";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,145,77,106,195,64,12,133,215,54,248,14,130,108,139,189,15,33,27,147,30,160,45,100,173,140,229,102,232,252,49,154,193,9,33,119,207,100,108,135,64,49,129,118,41,61,233,123,79,8,12,106,98,135,130,160,245,132,65,218,186,181,78,42,27,170,242,82,149,85,89,172,60,125,75,107,96,103,162,94,195,36,238,173,255,233,149,29,118,39,18,49,45,153,207,128,33,114,94,104,154,6,54,28,181,70,127,222,78,245,7,57,79,76,38,48,132,35,1,231,105,176,61,224,76,132,97,66,2,205,204,122,134,53,79,52,23,15,74,10,160,148,230,101,152,98,188,224,87,162,220,248,74,57,150,189,65,88,237,20,5,234,128,163,16,196,220,71,165,206,245,3,247,156,169,104,231,225,183,191,27,246,40,21,117,11,6,239,89,252,7,125,64,6,129,70,208,178,71,155,229,164,167,226,58,190,158,76,55,126,255,94,166,222,13,240,210,175,228,46,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("b4badb85-7741-4647-93fd-5f0f29b0b658"));
		}

		#endregion

	}

	#endregion

}

