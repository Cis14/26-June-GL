namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: EmailLicenseInformationResponseSchema

	/// <exclude/>
	public class EmailLicenseInformationResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public EmailLicenseInformationResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public EmailLicenseInformationResponseSchema(EmailLicenseInformationResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("c3bb4a3c-028b-42dd-b032-27136305e1d1");
			Name = "EmailLicenseInformationResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("5f5fe385-d25b-4c17-9585-cfaff007abaf");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,145,75,75,195,64,20,133,247,133,254,135,75,55,234,162,9,110,173,10,154,118,81,104,93,180,117,37,46,110,51,55,113,96,30,97,238,12,18,75,255,187,147,164,181,15,41,136,89,100,224,112,230,156,243,37,96,80,19,87,152,19,172,200,57,100,91,248,36,179,166,144,101,112,232,165,53,73,54,89,206,173,32,197,253,222,166,223,131,248,4,150,166,132,101,205,158,116,178,8,198,75,77,201,146,156,68,37,191,218,75,163,126,175,179,166,105,10,247,28,180,70,87,63,30,164,5,85,142,152,140,103,136,103,101,13,19,124,74,255,1,152,231,54,6,94,49,40,153,83,35,75,83,88,167,187,41,71,153,233,105,232,219,24,61,198,221,222,97,238,223,59,173,10,235,24,2,185,66,102,152,104,148,106,214,101,78,15,145,139,93,251,29,60,35,83,166,108,16,123,169,11,217,236,73,46,208,236,229,167,163,225,90,122,176,5,80,83,201,224,45,68,82,1,21,57,16,88,39,48,45,96,120,11,67,8,166,181,146,72,206,26,210,223,21,45,223,156,244,154,220,245,75,252,101,240,0,3,17,227,235,89,19,145,53,213,131,155,247,131,127,199,46,141,135,241,169,13,54,80,146,31,197,77,241,181,253,43,157,114,132,162,238,64,118,92,45,47,196,47,25,9,27,174,255,65,180,119,95,25,75,186,180,127,245,227,56,159,222,24,227,177,253,6,153,127,255,121,197,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("c3bb4a3c-028b-42dd-b032-27136305e1d1"));
		}

		#endregion

	}

	#endregion

}

