namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LlmModelRepositorySchema

	/// <exclude/>
	public class LlmModelRepositorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LlmModelRepositorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LlmModelRepositorySchema(LlmModelRepositorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("50c6d782-e37c-4a65-8807-5528565905ff");
			Name = "LlmModelRepository";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("11a936bf-8ee0-4505-a8db-e67a2dd080a5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,83,219,138,219,48,16,125,118,32,255,32,178,47,50,20,127,192,186,41,108,178,201,98,216,94,104,90,250,80,74,81,237,113,42,144,37,239,72,218,214,148,252,123,71,190,108,99,199,187,29,2,241,140,206,204,156,51,35,105,81,129,173,69,14,108,139,32,156,52,201,214,212,82,25,183,92,252,89,46,24,153,183,82,31,217,161,177,14,170,116,185,136,58,255,19,32,10,107,74,71,120,93,202,163,199,144,172,147,59,208,55,217,60,12,33,61,175,56,62,74,110,55,207,101,37,123,145,59,131,18,108,64,116,37,174,16,142,212,142,101,218,1,150,68,255,154,101,247,170,122,107,10,80,31,161,54,86,82,70,179,92,116,232,218,255,80,50,103,114,0,207,99,3,178,215,28,236,31,164,4,4,77,89,123,169,139,77,179,165,32,183,14,3,209,156,190,227,94,213,105,232,118,5,186,232,232,61,69,122,182,91,37,172,189,102,243,68,163,175,183,80,10,175,220,134,218,80,113,238,154,26,76,201,103,184,198,241,183,145,176,60,212,157,41,203,158,153,202,68,105,141,242,81,56,96,116,1,10,163,85,195,62,91,64,90,171,134,60,236,148,125,247,35,63,29,100,157,49,184,236,194,39,69,198,53,226,243,254,193,38,61,216,154,93,52,29,160,167,23,251,255,103,89,161,111,20,69,178,100,125,56,201,236,59,175,212,123,220,85,181,107,120,11,26,80,17,130,243,168,153,38,64,218,70,78,99,214,143,2,153,5,69,20,137,176,134,95,236,208,58,124,162,38,30,103,5,163,155,173,124,165,249,42,43,86,47,29,7,1,179,128,61,154,138,175,6,221,179,144,47,63,105,22,67,9,210,185,123,240,66,241,174,114,242,65,32,61,125,122,16,131,100,97,123,246,233,184,212,157,151,5,163,223,186,87,154,236,126,67,238,29,28,114,161,4,190,14,199,111,120,60,73,10,243,13,57,235,54,61,105,103,123,177,243,96,227,9,159,159,76,70,61,0,105,200,151,203,238,215,149,5,150,178,120,213,121,65,55,249,65,94,183,187,241,29,234,255,230,222,44,133,255,2,168,147,196,188,24,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("50c6d782-e37c-4a65-8807-5528565905ff"));
		}

		#endregion

	}

	#endregion

}

