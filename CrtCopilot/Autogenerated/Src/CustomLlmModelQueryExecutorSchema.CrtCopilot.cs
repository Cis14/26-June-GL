namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CustomLlmModelQueryExecutorSchema

	/// <exclude/>
	public class CustomLlmModelQueryExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CustomLlmModelQueryExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CustomLlmModelQueryExecutorSchema(CustomLlmModelQueryExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5ee5b9af-43fb-4ea0-9bea-eb362ae0b949");
			Name = "CustomLlmModelQueryExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("11a936bf-8ee0-4505-a8db-e67a2dd080a5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,82,93,75,195,48,20,125,31,236,63,92,186,151,20,74,127,192,166,62,56,55,80,156,76,54,125,17,31,178,228,118,6,210,164,228,99,56,196,255,110,210,84,108,135,14,47,133,210,123,207,57,61,231,38,138,214,104,27,202,16,182,104,12,181,186,114,229,92,171,74,236,189,161,78,104,53,30,125,140,71,16,202,91,161,246,3,148,193,217,223,163,114,161,156,112,2,237,57,204,146,50,167,77,2,37,216,203,13,86,212,75,119,45,20,15,4,226,142,13,234,138,220,182,106,199,71,143,230,184,120,71,230,3,45,47,224,33,184,135,75,200,230,222,58,93,223,203,122,165,57,202,1,42,203,95,147,114,227,119,82,48,96,146,90,11,103,8,48,133,95,251,23,137,211,14,238,172,86,87,73,182,219,78,172,137,193,125,216,24,132,253,89,103,124,140,102,167,176,110,255,251,157,175,231,228,140,7,242,100,209,4,25,133,44,158,65,88,94,255,51,255,145,138,53,133,29,181,72,134,152,226,116,41,89,222,183,250,217,247,51,65,197,147,245,65,183,75,179,66,247,166,121,12,98,180,11,226,200,7,89,190,155,160,15,225,108,5,71,56,104,193,97,173,27,47,169,195,64,58,132,166,217,52,200,68,37,216,82,160,228,150,164,227,4,108,95,5,156,172,22,88,123,3,7,142,99,37,120,185,65,55,215,210,215,234,153,74,143,36,139,148,116,103,179,162,163,150,91,29,187,36,207,103,255,8,221,13,195,243,5,40,250,56,140,15,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5ee5b9af-43fb-4ea0-9bea-eb362ae0b949"));
		}

		#endregion

	}

	#endregion

}

