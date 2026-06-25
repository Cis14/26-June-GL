namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: AzureOpenAIModelQueryExecutorSchema

	/// <exclude/>
	public class AzureOpenAIModelQueryExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public AzureOpenAIModelQueryExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public AzureOpenAIModelQueryExecutorSchema(AzureOpenAIModelQueryExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1008fa9a-f646-43f7-9154-cc0bbc1e8d1a");
			Name = "AzureOpenAIModelQueryExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("11a936bf-8ee0-4505-a8db-e67a2dd080a5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,82,219,78,194,64,16,125,134,196,127,152,148,151,54,33,253,0,80,9,34,36,168,40,138,242,98,124,88,182,83,220,100,217,109,246,66,68,227,191,187,237,150,208,218,64,152,52,105,102,118,206,217,51,103,86,144,13,234,140,80,132,87,84,138,104,153,154,120,36,69,202,214,86,17,195,164,184,104,255,92,180,193,133,213,76,172,107,93,10,251,199,143,226,177,48,204,48,212,167,122,38,132,26,169,124,147,111,123,191,197,148,88,110,110,152,72,28,32,52,187,12,101,26,78,11,182,221,179,69,181,27,127,33,181,14,22,117,225,209,169,135,43,8,134,223,86,225,83,134,98,56,157,201,4,121,173,47,136,62,60,119,102,87,156,81,160,156,104,13,39,33,208,131,7,190,105,214,47,255,163,238,180,20,215,158,189,180,41,143,142,194,181,179,14,156,145,218,40,155,207,168,123,48,47,174,223,15,90,17,116,82,74,248,166,81,57,34,129,52,95,135,243,177,154,70,7,178,60,122,176,34,26,195,122,79,183,233,79,16,85,229,254,86,53,117,80,36,94,126,173,90,78,52,67,243,41,147,124,24,37,141,163,199,164,54,207,190,8,114,235,22,205,18,132,173,100,9,204,101,102,57,49,232,64,91,87,84,139,12,41,75,25,157,48,228,137,14,253,110,1,139,95,183,97,71,238,49,208,226,77,214,100,231,225,49,241,2,205,72,114,187,17,75,194,45,134,129,31,178,91,162,226,34,133,193,0,130,32,234,159,69,48,204,216,61,238,14,12,62,63,80,180,90,173,35,200,23,212,210,42,138,249,211,60,224,171,213,115,88,220,125,75,84,218,89,94,211,80,214,154,163,28,93,97,121,232,190,63,156,45,59,63,234,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1008fa9a-f646-43f7-9154-cc0bbc1e8d1a"));
		}

		#endregion

	}

	#endregion

}

