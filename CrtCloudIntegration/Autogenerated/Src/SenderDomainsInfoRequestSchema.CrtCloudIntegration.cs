namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: SenderDomainsInfoRequestSchema

	/// <exclude/>
	public class SenderDomainsInfoRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public SenderDomainsInfoRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public SenderDomainsInfoRequestSchema(SenderDomainsInfoRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("de4bf405-ea8f-4a2e-9f9c-d9b55688e01f");
			Name = "SenderDomainsInfoRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,145,61,79,195,48,16,134,103,42,245,63,156,194,2,139,179,247,131,129,22,33,134,162,170,97,67,12,110,122,9,150,98,59,220,57,149,74,213,255,142,63,154,170,180,66,76,214,157,239,125,125,239,99,48,82,35,183,178,68,120,67,34,201,182,114,98,102,77,165,234,142,164,83,214,136,217,83,177,176,27,108,120,56,216,15,7,195,193,77,199,202,212,80,236,216,161,22,171,206,56,165,81,20,72,74,54,234,59,106,198,113,238,150,176,246,5,204,26,201,60,130,2,205,6,105,110,181,84,134,95,76,101,87,248,213,33,187,56,155,231,57,76,184,211,90,210,238,225,88,71,29,84,150,128,210,36,56,11,53,58,224,232,4,155,100,5,202,123,137,222,35,191,48,153,48,162,108,216,66,73,88,77,179,127,67,138,71,201,232,195,108,85,137,199,5,51,200,131,219,251,92,58,233,85,142,100,233,62,124,163,237,214,141,42,161,140,107,254,149,14,70,112,237,232,197,9,229,137,209,146,108,139,228,20,122,80,203,232,155,238,47,185,196,198,51,58,6,143,133,195,233,62,17,90,178,91,21,144,132,223,236,41,245,120,60,64,113,178,58,199,147,18,45,80,175,145,238,94,131,114,10,89,111,21,234,236,62,196,236,115,178,163,240,239,203,179,1,216,135,151,198,97,145,49,28,142,137,60,136,20,42,214,169,251,187,121,248,1,154,10,173,144,118,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("de4bf405-ea8f-4a2e-9f9c-d9b55688e01f"));
		}

		#endregion

	}

	#endregion

}

