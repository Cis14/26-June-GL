namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LlmModelJsonSchema

	/// <exclude/>
	public class LlmModelJsonSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LlmModelJsonSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LlmModelJsonSchema(LlmModelJsonSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("c6ecc960-e834-4515-b853-6061ed6719b1");
			Name = "LlmModelJson";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("11a936bf-8ee0-4505-a8db-e67a2dd080a5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,83,77,107,227,48,16,189,23,250,31,134,246,98,67,240,15,104,182,219,195,134,210,150,77,90,54,102,247,80,122,80,156,137,173,34,75,97,52,110,200,134,252,247,149,108,199,88,177,219,203,14,198,160,249,120,111,230,105,164,69,137,118,43,50,132,20,137,132,53,27,78,126,24,189,145,121,69,130,165,209,151,23,135,203,11,112,86,89,169,115,88,238,45,99,57,29,186,146,95,149,102,89,98,178,68,146,66,201,191,117,117,144,184,192,29,27,93,83,60,217,58,214,68,95,103,130,133,35,101,18,25,191,53,190,109,181,82,50,131,76,9,107,225,167,42,231,102,141,202,87,53,225,182,39,111,215,132,185,99,130,23,50,91,36,150,104,111,224,165,174,62,225,119,28,115,44,87,72,209,194,205,12,183,112,85,122,204,171,248,173,151,229,25,90,160,125,52,146,208,182,101,153,252,64,117,83,112,128,28,121,10,214,255,142,125,206,107,212,235,166,185,192,219,246,59,71,46,204,122,180,217,144,37,53,190,171,40,238,15,237,237,67,144,39,101,151,99,221,56,26,119,224,19,79,250,35,45,79,193,179,66,111,139,74,169,223,66,85,248,32,244,90,121,154,219,161,47,121,204,181,33,156,12,203,103,184,17,149,226,115,132,49,119,11,18,98,28,167,225,153,144,43,210,117,255,110,17,62,156,252,221,30,225,243,234,29,51,142,184,144,118,2,247,134,74,81,143,149,44,140,198,73,167,64,220,67,60,142,138,233,22,50,131,20,238,201,148,158,231,91,250,61,106,21,126,119,199,24,118,5,146,123,6,112,19,236,219,196,11,59,20,95,110,160,173,78,30,173,23,238,153,254,20,146,113,233,223,82,84,3,14,106,122,131,250,203,74,163,248,76,133,99,120,100,218,127,1,209,215,106,134,54,84,203,15,215,76,117,119,247,25,25,100,130,179,226,127,154,252,66,231,190,132,157,228,161,222,135,209,21,232,174,167,143,208,14,243,217,21,247,31,90,27,116,223,63,27,111,177,229,219,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("c6ecc960-e834-4515-b853-6061ed6719b1"));
		}

		#endregion

	}

	#endregion

}

