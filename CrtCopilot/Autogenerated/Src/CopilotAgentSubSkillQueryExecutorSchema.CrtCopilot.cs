namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CopilotAgentSubSkillQueryExecutorSchema

	/// <exclude/>
	public class CopilotAgentSubSkillQueryExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CopilotAgentSubSkillQueryExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CopilotAgentSubSkillQueryExecutorSchema(CopilotAgentSubSkillQueryExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("dc1810dd-b40a-4344-bbd1-3995e93833c1");
			Name = "CopilotAgentSubSkillQueryExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,144,79,75,196,48,16,197,207,93,216,239,48,212,75,11,75,63,192,138,7,183,174,176,160,162,172,158,196,67,154,78,107,176,77,74,50,1,203,226,119,119,250,71,215,130,165,167,48,47,191,188,247,38,90,212,232,26,33,17,82,139,130,148,73,82,211,168,202,208,122,117,90,175,2,239,148,46,225,216,58,194,250,242,119,78,77,93,27,157,220,153,178,228,241,143,62,181,72,238,145,68,46,72,156,137,103,180,86,56,83,16,51,22,231,244,100,175,73,145,66,55,11,220,10,73,198,14,4,51,23,22,75,101,52,164,149,112,110,11,99,254,117,137,154,142,62,59,126,168,170,122,242,104,219,253,39,74,207,15,251,71,175,55,88,8,95,209,78,233,156,35,34,106,27,52,69,116,232,195,219,9,31,111,224,129,63,10,174,64,243,193,208,98,66,28,191,113,68,227,179,74,73,144,93,173,229,86,176,133,157,112,56,114,51,197,131,83,95,254,188,178,209,142,172,239,190,131,55,127,236,3,7,98,12,95,140,141,94,28,90,182,209,40,169,115,244,147,49,238,172,130,45,100,220,44,154,94,109,32,252,207,60,156,209,39,161,169,144,239,24,110,122,243,224,7,63,104,98,62,140,225,4,95,227,150,168,243,97,209,126,30,212,169,200,218,55,124,71,130,183,197,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("dc1810dd-b40a-4344-bbd1-3995e93833c1"));
		}

		#endregion

	}

	#endregion

}

