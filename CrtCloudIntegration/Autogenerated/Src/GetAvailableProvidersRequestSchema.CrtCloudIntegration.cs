namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: GetAvailableProvidersRequestSchema

	/// <exclude/>
	public class GetAvailableProvidersRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public GetAvailableProvidersRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public GetAvailableProvidersRequestSchema(GetAvailableProvidersRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("7d23b130-a0c9-4a16-9005-a52b09ec2d91");
			Name = "GetAvailableProvidersRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,145,65,79,2,49,16,133,207,144,240,31,38,120,209,203,238,93,212,132,160,49,198,96,8,120,51,30,134,101,88,27,183,237,58,51,75,178,18,254,187,109,151,37,232,197,83,59,51,175,175,223,107,29,90,146,26,11,130,87,98,70,241,91,205,102,222,109,77,217,48,170,241,46,155,61,172,230,126,67,149,140,134,251,209,112,208,136,113,37,172,90,81,178,217,178,113,106,44,101,43,98,131,149,249,78,39,38,163,97,208,93,48,149,161,128,89,133,34,215,240,72,58,221,161,169,112,93,209,130,253,206,108,136,101,73,95,13,137,38,125,158,231,112,35,141,181,200,237,221,177,94,82,205,36,228,84,128,59,41,168,135,146,20,176,247,2,27,54,145,168,238,77,179,222,44,63,115,123,187,71,197,144,75,25,11,125,15,141,186,89,87,166,128,34,210,253,3,55,216,39,192,83,162,160,168,137,213,80,136,181,72,54,221,252,111,130,212,8,214,2,158,65,226,170,31,4,211,197,19,124,82,155,157,78,156,99,118,156,115,178,107,226,203,151,240,53,112,11,99,172,205,51,181,227,171,136,221,115,139,114,12,61,77,35,216,199,55,153,196,59,38,112,56,194,146,219,116,188,169,238,186,191,155,135,31,194,72,236,27,252,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("7d23b130-a0c9-4a16-9005-a52b09ec2d91"));
		}

		#endregion

	}

	#endregion

}

