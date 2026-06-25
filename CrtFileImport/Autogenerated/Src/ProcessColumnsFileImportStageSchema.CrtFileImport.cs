namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ProcessColumnsFileImportStageSchema

	/// <exclude/>
	public class ProcessColumnsFileImportStageSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ProcessColumnsFileImportStageSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ProcessColumnsFileImportStageSchema(ProcessColumnsFileImportStageSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("7af17616-4f9f-4644-894a-b58b57826cf5");
			Name = "ProcessColumnsFileImportStage";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,82,193,78,220,48,16,61,7,137,127,24,209,75,86,133,228,190,203,82,193,178,168,145,218,10,169,229,132,122,48,241,36,88,74,236,104,108,87,69,171,254,123,39,113,2,100,217,186,106,79,113,198,111,222,243,123,51,90,180,104,59,81,34,124,67,34,97,77,229,178,141,209,149,170,61,9,167,140,206,110,84,131,69,219,25,114,199,71,187,227,163,196,91,165,235,25,154,112,245,135,122,118,35,74,103,72,161,101,4,99,222,17,214,204,9,176,105,132,181,75,184,37,83,162,181,27,211,248,86,219,23,165,175,78,212,56,116,228,121,14,231,74,63,34,41,39,77,9,37,97,181,62,185,18,22,247,208,39,249,197,4,183,190,109,5,61,77,255,219,159,88,122,135,208,5,49,40,131,218,132,206,95,193,239,175,177,18,190,113,87,74,75,118,147,186,167,14,77,149,22,7,244,22,167,240,133,179,131,53,104,254,48,40,234,101,177,248,206,244,157,127,104,20,123,232,205,199,189,195,18,14,104,50,197,110,72,229,57,72,30,149,117,228,251,144,251,56,7,254,128,24,181,162,42,233,157,69,98,10,141,101,63,106,240,179,223,211,158,38,73,138,177,247,178,174,89,84,176,210,165,20,157,67,154,130,28,37,12,45,66,195,18,30,248,233,233,30,217,91,52,236,224,215,232,6,181,12,134,230,238,24,219,33,57,94,159,185,183,127,92,138,41,11,243,131,151,83,73,132,61,148,221,106,223,194,112,44,36,172,47,14,222,103,209,36,87,113,35,159,209,61,26,25,22,222,113,32,40,255,211,200,212,30,247,242,1,10,205,3,210,162,25,31,157,6,196,173,32,222,85,190,177,208,61,31,121,14,195,220,54,123,243,153,12,111,137,12,193,251,53,4,142,79,166,174,145,178,143,66,203,6,135,187,85,180,61,125,165,20,71,6,161,179,191,9,17,58,79,58,58,163,173,118,170,223,154,183,67,74,146,195,43,23,170,243,34,215,126,3,249,151,179,51,30,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("7af17616-4f9f-4644-894a-b58b57826cf5"));
		}

		#endregion

	}

	#endregion

}

