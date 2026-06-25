namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CloudValidateDomainRequestSchema

	/// <exclude/>
	public class CloudValidateDomainRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CloudValidateDomainRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CloudValidateDomainRequestSchema(CloudValidateDomainRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ea16777d-0f3a-45c6-98f2-1baa66af9f6c");
			Name = "CloudValidateDomainRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,146,193,74,3,49,16,134,207,22,250,14,67,123,209,203,238,221,170,80,182,34,34,149,165,21,47,226,33,221,157,174,193,77,178,78,38,133,181,248,238,102,147,182,86,169,96,61,133,249,103,230,207,151,153,104,161,208,54,162,64,120,64,34,97,205,146,147,204,232,165,172,28,9,150,70,39,217,245,124,106,74,172,109,191,183,238,247,78,156,149,186,130,121,107,25,85,50,115,154,165,194,100,142,36,69,45,223,67,199,168,223,243,117,67,194,202,7,144,213,194,218,115,120,244,233,82,48,78,140,18,82,207,240,205,161,101,95,150,166,41,92,88,167,148,160,246,106,19,135,22,88,26,2,138,117,192,6,86,27,3,176,168,75,36,40,131,81,178,181,72,247,60,158,38,130,133,127,4,147,40,248,217,11,141,91,212,178,128,34,248,102,181,113,229,111,56,235,192,190,131,207,201,52,72,44,209,191,32,15,38,49,255,147,58,8,55,200,22,60,180,237,78,126,65,24,231,183,240,138,109,178,235,216,135,140,148,83,84,11,164,211,123,191,5,184,132,129,104,228,29,182,131,179,14,122,75,109,153,186,137,143,67,10,214,80,33,143,186,59,70,240,113,12,204,215,188,254,200,18,27,14,178,196,169,253,159,165,33,179,146,221,18,181,191,235,8,164,109,95,23,31,4,203,247,10,14,225,13,253,223,137,171,13,113,84,191,139,94,251,4,14,51,61,38,20,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ea16777d-0f3a-45c6-98f2-1baa66af9f6c"));
		}

		#endregion

	}

	#endregion

}

