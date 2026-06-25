namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ValidateDomainRequestSchema

	/// <exclude/>
	public class ValidateDomainRequestSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ValidateDomainRequestSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ValidateDomainRequestSchema(ValidateDomainRequestSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("2a8b6c28-1350-4a59-be14-2e541a2d0e5e");
			Name = "ValidateDomainRequest";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,93,145,63,79,195,48,16,197,103,42,245,59,156,202,66,151,100,167,192,146,32,212,161,168,74,16,11,98,112,157,75,100,41,182,195,157,141,84,34,190,59,142,243,71,165,75,164,247,238,229,249,119,182,17,26,185,19,18,225,13,137,4,219,218,37,153,53,181,106,60,9,167,172,73,178,231,242,96,43,108,121,189,234,215,171,27,207,202,52,80,158,217,161,222,93,233,164,240,198,41,141,73,137,164,68,171,126,98,67,72,133,220,45,97,19,4,100,173,96,190,135,247,48,174,132,195,18,77,133,148,91,45,148,41,2,137,53,140,49,159,166,41,60,176,215,90,208,249,105,210,5,118,132,140,198,49,16,126,121,100,7,181,37,24,8,90,132,42,150,192,247,216,60,160,207,53,233,69,207,71,46,156,8,27,58,18,210,125,6,163,243,167,86,73,144,3,215,130,53,3,197,67,66,168,143,76,203,18,71,178,29,146,83,24,54,57,198,255,199,249,53,116,52,246,12,165,151,18,153,147,37,115,73,52,34,29,80,159,144,238,94,195,123,192,35,108,198,101,246,213,102,59,48,206,144,47,94,85,144,79,35,232,161,65,183,3,30,62,191,19,96,184,206,145,49,234,209,253,111,6,239,15,166,38,13,149,245,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("2a8b6c28-1350-4a59-be14-2e541a2d0e5e"));
		}

		#endregion

	}

	#endregion

}

