namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: RootAccountInfoRequestSchema

	/// <exclude/>
	public class RootAccountInfoRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public RootAccountInfoRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public RootAccountInfoRequestSchema(RootAccountInfoRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ab1e6279-d876-431e-a833-76ede3fe1e04");
			Name = "RootAccountInfoRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("e340cd47-dd91-41d9-9076-90ff98ffd14e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,181,148,77,107,194,64,16,134,207,21,252,15,3,94,139,185,87,17,36,133,18,40,173,248,113,42,61,172,155,73,92,136,187,233,236,68,17,233,127,239,102,19,211,88,106,209,182,94,146,204,236,188,239,60,59,187,68,139,53,218,92,72,132,57,18,9,107,18,238,135,70,39,42,45,72,176,50,186,219,217,119,59,221,206,77,143,48,117,33,132,153,176,246,14,166,198,240,88,74,83,104,142,116,98,166,248,86,160,101,95,25,4,1,12,109,177,94,11,218,141,234,248,126,254,12,84,213,64,98,8,68,37,237,31,202,131,86,125,94,44,51,37,65,150,141,78,246,185,169,168,26,172,9,153,28,137,21,58,182,137,55,168,214,191,194,248,196,3,178,5,71,97,203,55,175,16,36,97,185,89,88,76,31,251,141,170,205,116,128,178,76,74,167,16,86,245,11,202,96,15,41,242,160,180,26,192,251,185,61,5,108,68,86,32,40,29,43,233,156,156,229,118,133,142,132,224,37,47,163,3,144,210,150,133,150,248,250,51,214,210,152,12,38,78,88,131,69,181,236,119,116,237,137,200,76,161,102,80,177,123,170,68,33,93,50,159,208,139,163,248,223,48,44,186,4,95,142,48,243,186,191,99,84,99,224,157,91,161,141,59,172,11,239,75,84,203,103,94,125,141,219,195,43,101,155,75,3,238,91,72,86,27,60,227,246,68,118,236,75,175,143,164,221,73,72,67,167,160,124,198,187,53,33,192,80,142,152,10,28,6,114,4,42,57,237,57,0,83,54,221,42,139,183,165,40,17,153,245,170,86,179,79,239,227,253,63,33,135,206,226,187,1,244,80,199,213,143,198,199,85,246,56,233,114,31,171,131,208,24,76,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ab1e6279-d876-431e-a833-76ede3fe1e04"));
		}

		#endregion

	}

	#endregion

}

