namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: OpenAIModelQueryExecutorSchema

	/// <exclude/>
	public class OpenAIModelQueryExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public OpenAIModelQueryExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public OpenAIModelQueryExecutorSchema(OpenAIModelQueryExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("bb2364d3-583a-4d7b-b337-ea6fc5c3e5cc");
			Name = "OpenAIModelQueryExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("11a936bf-8ee0-4505-a8db-e67a2dd080a5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,82,203,110,194,48,16,188,35,241,15,171,112,73,164,40,31,0,109,17,165,32,209,150,150,170,143,75,213,131,177,55,212,146,177,35,63,80,81,197,191,215,137,131,72,138,82,117,47,209,174,103,199,51,227,72,178,69,83,16,138,240,130,90,19,163,114,155,77,149,204,249,198,105,98,185,146,253,222,119,191,7,190,156,225,114,211,66,105,28,117,31,101,51,105,185,229,104,254,194,204,9,181,74,7,80,128,189,223,96,78,156,176,215,92,50,191,16,219,125,129,42,143,23,21,219,254,201,161,222,207,190,144,58,191,150,164,240,224,213,195,37,68,143,5,202,201,98,169,24,138,22,36,74,62,2,109,225,214,130,83,160,130,24,3,93,104,24,194,189,216,158,207,47,26,11,183,70,201,171,192,89,231,82,214,64,227,198,103,5,62,57,99,181,43,77,153,33,172,170,75,143,206,26,50,186,4,196,175,6,181,231,144,72,203,232,125,102,205,54,129,19,81,89,67,88,19,131,113,27,148,182,194,136,146,166,202,67,83,202,0,37,11,170,91,211,218,200,18,237,167,98,165,7,173,172,103,70,214,178,113,28,130,218,249,7,229,12,97,167,56,131,149,42,156,32,22,253,210,206,15,245,115,129,148,231,156,206,57,10,102,226,240,134,128,213,39,133,95,169,2,173,126,187,150,226,178,2,60,123,70,59,85,194,109,229,27,17,14,227,40,248,75,235,173,172,106,97,60,134,40,74,70,255,34,152,20,252,14,247,39,134,208,159,83,116,166,86,31,30,126,0,121,181,255,175,67,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("bb2364d3-583a-4d7b-b337-ea6fc5c3e5cc"));
		}

		#endregion

	}

	#endregion

}

