namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: CloudDomainValidationResponseSchema

	/// <exclude/>
	public class CloudDomainValidationResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public CloudDomainValidationResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public CloudDomainValidationResponseSchema(CloudDomainValidationResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8a51fd68-a15a-454b-8b02-f4355f0fa9ae");
			Name = "CloudDomainValidationResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,197,84,77,107,27,49,16,61,39,144,255,32,146,75,11,97,125,111,210,92,156,80,10,117,9,187,165,151,146,131,172,29,111,5,250,48,51,146,193,53,253,239,157,29,109,156,120,227,22,187,165,228,98,172,153,55,111,222,72,243,54,104,15,180,212,6,212,23,64,212,20,23,169,154,198,176,176,93,70,157,108,12,213,244,174,153,197,22,28,157,157,110,206,78,79,50,217,208,169,102,77,9,60,35,157,3,211,195,168,250,0,1,208,154,171,49,166,206,33,89,15,85,195,89,237,236,15,97,101,20,227,46,16,58,62,168,169,211,68,239,212,87,78,183,58,193,109,244,218,134,154,117,49,47,8,114,50,153,168,107,202,222,107,92,223,12,231,26,150,8,4,33,145,194,1,171,226,66,165,239,160,8,112,101,121,166,69,68,206,37,180,176,234,21,181,66,76,18,230,194,182,143,1,135,92,245,216,98,242,172,199,183,91,157,52,223,69,66,109,210,3,7,150,121,238,172,81,166,87,203,154,99,110,139,210,65,55,79,242,164,249,100,35,186,183,35,222,99,92,2,38,11,60,231,189,240,148,252,120,48,9,124,164,114,23,213,22,241,92,87,17,54,3,63,7,124,243,153,223,79,189,87,231,150,164,226,252,237,67,225,29,180,206,99,116,76,39,57,181,81,29,164,43,158,156,127,126,254,185,61,101,99,128,232,40,1,77,169,17,9,99,1,67,238,96,9,77,210,41,31,211,158,164,96,183,55,241,195,247,123,40,169,131,91,207,88,167,238,224,136,222,190,84,236,109,62,176,253,223,187,71,48,17,91,122,218,195,93,41,159,44,165,107,217,215,90,128,59,251,154,93,186,81,245,152,96,159,222,11,118,76,217,102,57,151,232,40,56,114,244,254,126,135,56,186,119,49,10,186,247,180,86,101,68,181,218,82,93,42,27,140,203,226,97,203,5,146,177,105,125,201,224,225,65,248,111,104,133,137,197,68,99,249,211,210,62,18,25,62,116,17,215,127,235,252,223,77,246,154,190,255,55,215,223,33,242,103,209,191,234,246,215,47,31,231,40,11,76,135,194,189,98,234,29,200,161,27,254,98,199,57,250,11,15,117,200,92,53,7,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8a51fd68-a15a-454b-8b02-f4355f0fa9ae"));
		}

		#endregion

	}

	#endregion

}

