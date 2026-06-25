namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ValidateDomainResponseSchema

	/// <exclude/>
	public class ValidateDomainResponseSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ValidateDomainResponseSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ValidateDomainResponseSchema(ValidateDomainResponseSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1b8d4e4f-63de-4e19-b659-7f78f9c969e6");
			Name = "ValidateDomainResponse";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,145,63,79,195,48,16,197,231,68,202,119,56,181,11,44,206,78,129,37,101,96,40,170,18,196,130,24,156,228,18,89,242,159,200,103,35,149,136,239,142,227,132,168,84,29,89,44,221,189,119,239,126,58,107,174,144,6,222,32,188,162,181,156,76,231,88,97,116,39,122,111,185,19,70,179,226,169,58,152,22,37,101,233,152,165,137,39,161,123,168,78,228,80,177,210,107,39,20,178,10,173,224,82,124,197,137,93,150,6,223,214,98,31,10,40,36,39,186,131,183,32,183,220,97,133,186,69,187,55,138,11,93,134,205,70,19,70,127,158,231,112,79,94,41,110,79,143,75,93,226,96,145,80,59,2,187,120,193,116,208,198,105,248,156,35,167,37,65,245,210,177,223,152,252,44,103,240,181,20,13,52,19,198,74,113,185,63,25,35,195,10,125,180,102,64,235,4,6,242,99,12,152,245,75,200,216,120,38,168,124,211,32,17,91,61,231,4,201,251,158,59,126,64,85,163,189,121,9,247,134,7,216,8,90,102,54,183,31,147,103,193,172,141,145,33,112,209,96,132,30,221,14,104,122,190,255,31,33,158,227,42,64,84,174,173,223,134,255,155,143,20,235,185,251,183,25,122,63,221,246,122,165,86,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1b8d4e4f-63de-4e19-b659-7f78f9c969e6"));
		}

		#endregion

	}

	#endregion

}

