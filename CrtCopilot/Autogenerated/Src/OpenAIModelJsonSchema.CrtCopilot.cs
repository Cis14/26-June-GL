namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: OpenAIModelJsonSchema

	/// <exclude/>
	public class OpenAIModelJsonSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public OpenAIModelJsonSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public OpenAIModelJsonSchema(OpenAIModelJsonSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("50a2b8a0-cee6-4c21-85e2-db9782d70d2a");
			Name = "OpenAIModelJson";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("11a936bf-8ee0-4505-a8db-e67a2dd080a5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,79,203,106,195,64,12,188,7,242,15,34,185,164,23,127,64,66,15,161,189,244,145,52,52,189,149,80,20,71,49,162,251,66,43,83,220,208,127,239,174,215,13,62,84,44,11,210,140,102,70,14,45,197,128,53,193,27,137,96,244,103,173,238,188,59,115,211,10,42,123,55,157,92,166,19,72,213,70,118,13,236,187,168,100,171,215,214,41,91,170,246,36,140,134,191,123,234,106,76,220,210,151,122,215,235,61,198,30,43,232,251,61,42,38,7,21,172,245,80,102,161,61,26,174,161,54,24,35,188,4,114,235,135,141,63,145,201,139,176,132,103,99,175,109,89,24,34,229,154,11,53,201,27,118,226,3,137,50,197,37,236,122,189,63,199,171,235,134,236,145,100,177,77,39,195,45,204,48,240,199,39,117,179,155,195,136,151,61,6,169,110,241,47,101,8,27,85,242,153,235,192,79,212,193,5,26,210,21,196,252,253,140,141,231,228,78,37,97,153,37,48,189,95,50,190,145,45,118,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("50a2b8a0-cee6-4c21-85e2-db9782d70d2a"));
		}

		#endregion

	}

	#endregion

}

