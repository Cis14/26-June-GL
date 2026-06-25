namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UpdateMaxFunctionCallingCountPerCopilotReqInstallScriptExecutorSchema

	/// <exclude/>
	public class UpdateMaxFunctionCallingCountPerCopilotReqInstallScriptExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UpdateMaxFunctionCallingCountPerCopilotReqInstallScriptExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UpdateMaxFunctionCallingCountPerCopilotReqInstallScriptExecutorSchema(UpdateMaxFunctionCallingCountPerCopilotReqInstallScriptExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("044c4671-e11e-4295-889d-44c9a6d9df7c");
			Name = "UpdateMaxFunctionCallingCountPerCopilotReqInstallScriptExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("c23dfaf0-0051-4966-86f8-cbe8197258d0");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,144,79,75,196,48,16,197,207,21,252,14,195,158,90,88,122,16,188,184,122,170,127,216,131,32,214,245,186,196,116,186,4,98,18,51,147,186,34,126,119,147,214,46,182,10,10,73,200,36,191,188,121,47,96,196,51,146,19,18,225,1,189,23,100,91,46,43,107,90,181,11,94,176,178,6,222,143,143,178,64,202,236,38,132,199,213,225,60,85,245,27,213,200,28,75,130,139,25,57,21,44,191,161,81,35,170,184,240,164,149,4,169,5,17,108,92,35,24,111,197,254,58,24,153,248,74,104,29,217,202,6,195,119,232,43,235,148,182,124,143,47,107,67,28,239,106,233,149,227,171,61,202,192,214,159,193,250,215,243,62,70,230,188,234,162,58,72,27,25,80,134,97,107,240,245,18,219,71,161,3,70,227,39,167,171,159,28,177,79,49,183,116,48,94,217,38,209,139,255,216,12,72,188,24,130,142,73,59,171,26,24,156,97,190,161,68,27,131,189,12,132,73,89,12,190,179,78,120,232,190,60,230,209,119,49,251,243,242,6,185,207,144,79,223,47,231,174,139,62,95,166,90,200,7,189,243,201,23,140,253,178,185,126,220,140,204,95,45,150,83,197,161,225,71,90,211,18,103,28,159,27,67,56,177,120,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("044c4671-e11e-4295-889d-44c9a6d9df7c"));
		}

		#endregion

	}

	#endregion

}

