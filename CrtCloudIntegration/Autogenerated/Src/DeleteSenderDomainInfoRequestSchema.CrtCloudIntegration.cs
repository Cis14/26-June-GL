namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: DeleteSenderDomainInfoRequestSchema

	/// <exclude/>
	public class DeleteSenderDomainInfoRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public DeleteSenderDomainInfoRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public DeleteSenderDomainInfoRequestSchema(DeleteSenderDomainInfoRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("aa2b05f5-7ee1-40b2-a878-1a1104cac318");
			Name = "DeleteSenderDomainInfoRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,81,77,79,194,64,16,61,75,194,127,152,148,139,94,218,59,31,30,4,99,56,96,8,229,102,60,44,219,105,221,164,221,173,51,91,18,36,254,119,119,183,128,136,85,46,109,231,117,230,205,123,243,180,168,144,107,33,17,214,72,36,216,228,54,158,26,157,171,162,33,97,149,209,241,244,49,93,152,12,75,134,126,111,223,239,221,52,172,116,1,233,142,45,86,241,170,209,86,85,24,167,72,74,148,234,35,140,140,250,61,215,55,32,44,92,1,211,82,48,15,97,134,37,90,76,81,103,72,51,83,9,165,87,248,222,32,219,208,156,36,9,140,185,169,42,65,187,251,67,189,194,154,144,81,91,6,106,91,33,55,4,126,125,137,144,5,14,200,60,173,151,121,36,73,46,88,198,140,40,74,54,32,9,243,73,116,213,101,252,32,216,201,164,173,146,120,80,24,65,226,217,94,102,194,10,55,101,73,72,251,234,128,186,217,148,74,130,244,6,59,252,205,117,110,14,12,67,248,205,10,251,224,252,116,167,37,153,26,201,42,116,199,90,6,230,246,255,229,105,2,48,103,72,27,41,145,57,62,245,156,59,111,197,46,176,218,32,221,62,187,140,97,2,81,123,178,232,206,107,63,138,103,75,62,206,86,49,236,161,64,59,2,246,143,207,127,214,63,161,11,197,103,225,223,246,13,65,251,21,38,15,223,53,153,173,114,87,248,67,89,64,182,162,108,240,84,174,175,51,124,15,116,57,59,14,248,186,211,223,242,172,161,203,229,192,229,214,230,16,234,22,253,9,58,236,11,180,199,214,125,45,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("aa2b05f5-7ee1-40b2-a878-1a1104cac318"));
		}

		#endregion

	}

	#endregion

}

