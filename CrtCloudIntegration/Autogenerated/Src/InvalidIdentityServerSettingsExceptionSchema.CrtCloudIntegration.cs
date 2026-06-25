namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: InvalidIdentityServerSettingsExceptionSchema

	/// <exclude/>
	public class InvalidIdentityServerSettingsExceptionSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public InvalidIdentityServerSettingsExceptionSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public InvalidIdentityServerSettingsExceptionSchema(InvalidIdentityServerSettingsExceptionSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("45eac23e-2e39-4284-bb79-03c123cf30e3");
			Name = "InvalidIdentityServerSettingsException";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("fc1a2769-1cc9-44d3-a1a6-003d1b8450f5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,82,205,106,194,64,16,62,43,248,14,131,94,20,172,15,160,216,67,131,135,28,10,66,122,43,69,214,100,12,3,235,110,152,221,72,173,248,238,29,179,27,181,182,7,145,222,50,243,237,124,127,196,168,45,186,74,229,8,111,200,172,156,221,248,73,98,205,134,202,154,149,39,107,38,47,213,214,26,77,6,19,109,235,34,53,30,203,128,244,186,135,94,183,83,59,50,37,100,123,231,113,59,235,117,101,51,96,44,5,134,68,43,231,166,144,154,157,210,84,164,5,26,79,126,159,33,239,144,51,244,94,238,220,226,51,199,42,144,201,229,187,128,36,143,191,212,90,227,112,244,33,171,170,94,107,202,33,63,113,221,73,5,83,184,162,237,156,76,94,60,89,227,60,215,185,183,44,214,150,13,121,35,221,10,221,39,49,28,65,67,123,124,228,86,12,156,42,147,222,157,42,113,36,118,215,202,225,240,60,255,23,243,248,82,3,144,49,200,231,241,86,115,252,11,191,246,48,64,83,132,250,226,28,187,92,178,173,144,61,225,223,77,90,177,200,84,32,68,87,175,65,11,230,207,208,111,131,64,72,2,46,70,1,114,96,172,135,38,45,60,201,190,129,107,214,99,249,5,72,142,210,2,44,199,239,12,115,70,223,159,253,144,141,106,11,102,203,137,21,249,3,148,232,103,112,132,57,244,41,234,174,40,20,186,106,133,91,146,155,172,161,129,235,229,241,27,117,18,233,26,49,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("45eac23e-2e39-4284-bb79-03c123cf30e3"));
		}

		#endregion

	}

	#endregion

}

