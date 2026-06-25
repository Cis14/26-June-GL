namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: DefaultOpenIdUserChangeValidatorSchema

	/// <exclude/>
	public class DefaultOpenIdUserChangeValidatorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public DefaultOpenIdUserChangeValidatorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public DefaultOpenIdUserChangeValidatorSchema(DefaultOpenIdUserChangeValidatorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8ec53e46-ef7a-45a0-abd4-217bf5a8fdf0");
			Name = "DefaultOpenIdUserChangeValidator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("cafc62fc-f7d7-4a5d-acf5-62f836ef940a");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,83,205,110,226,48,16,62,83,169,239,224,205,94,18,9,229,1,218,237,74,16,74,149,3,109,181,208,189,84,61,152,120,2,150,28,27,249,167,171,8,241,238,59,182,179,109,130,202,194,5,145,241,124,63,243,141,77,36,109,192,236,104,5,100,5,90,83,163,106,155,23,74,214,124,227,52,181,92,201,252,105,7,178,100,19,103,183,215,87,251,235,171,145,51,92,110,200,178,53,22,154,219,143,239,62,90,195,169,122,126,47,45,183,28,204,201,134,57,173,172,210,131,14,148,90,130,181,248,215,144,187,227,254,161,215,94,43,226,145,225,187,134,13,30,144,66,80,99,110,200,12,106,234,132,141,35,189,24,208,197,150,202,13,252,166,130,51,138,186,1,243,218,117,77,185,100,200,148,218,118,7,170,78,203,147,168,44,123,67,216,206,173,5,175,72,229,149,206,10,145,27,114,154,15,201,246,193,201,135,253,5,216,173,98,56,192,115,16,137,135,157,224,90,41,65,10,42,35,137,167,75,3,167,146,18,42,159,10,113,131,207,49,9,75,104,137,105,205,132,53,92,190,72,110,23,138,241,154,3,203,136,95,241,104,20,72,169,16,234,207,130,74,71,133,103,252,165,4,196,190,42,164,77,238,66,235,168,23,122,254,0,22,199,112,144,30,107,38,221,53,250,63,101,50,38,53,21,6,178,219,64,205,107,146,158,49,241,207,240,72,131,117,90,18,171,29,68,240,33,252,62,56,206,66,0,37,195,203,243,213,200,222,243,10,87,204,10,37,92,35,131,253,31,30,246,51,77,74,150,116,86,222,169,38,166,218,66,67,145,102,56,92,188,212,237,50,156,162,83,186,1,237,73,75,105,44,149,21,76,219,71,124,100,105,178,236,137,15,104,123,117,239,49,240,228,133,6,106,33,82,31,165,217,97,141,213,152,249,235,27,169,130,113,179,82,115,176,213,22,41,246,93,220,179,165,91,99,164,201,4,97,239,144,144,195,103,172,223,250,170,121,0,206,181,106,102,211,116,112,16,135,202,159,53,111,168,110,99,66,185,31,103,220,133,58,62,82,207,206,45,196,139,71,231,121,105,30,157,16,79,250,190,217,225,140,3,221,175,118,18,81,184,149,207,217,178,203,228,46,221,186,191,245,200,223,197,149,145,187,225,141,185,4,115,206,79,87,14,183,60,212,15,221,75,7,201,226,99,15,223,177,58,44,30,200,95,135,4,30,18,174,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8ec53e46-ef7a-45a0-abd4-217bf5a8fdf0"));
		}

		#endregion

	}

	#endregion

}

