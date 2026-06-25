namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: AuthenticationResultSchema

	/// <exclude/>
	public class AuthenticationResultSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public AuthenticationResultSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public AuthenticationResultSchema(AuthenticationResultSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("16a1ffba-730d-4792-8d1d-f8f8fb57e8df");
			Name = "AuthenticationResult";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("48c79191-1a42-4c6e-9843-1938fdf8bec4");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,144,77,10,194,48,16,133,215,22,122,135,1,247,237,94,69,16,221,10,69,189,64,140,211,24,104,147,48,147,44,68,188,187,105,90,69,165,234,38,48,63,239,189,47,99,68,139,236,132,68,56,32,145,96,91,251,98,109,77,173,85,32,225,181,53,121,118,205,179,60,155,76,9,85,44,97,221,8,230,25,172,130,63,163,241,90,166,165,29,114,104,124,218,43,203,18,22,28,218,86,208,101,57,212,59,116,132,28,215,25,100,39,7,91,67,148,131,120,51,1,74,46,197,195,164,124,113,113,225,216,104,57,168,199,179,39,61,231,19,180,34,235,144,188,198,72,91,37,121,63,255,4,28,8,59,147,113,174,226,169,122,37,122,32,29,173,109,96,31,164,196,72,118,5,133,126,14,220,61,183,31,113,27,100,73,218,165,95,255,187,197,247,104,246,164,141,130,109,76,22,10,199,194,167,104,78,253,57,82,221,119,223,155,177,119,7,214,235,240,102,4,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("16a1ffba-730d-4792-8d1d-f8f8fb57e8df"));
		}

		#endregion

	}

	#endregion

}

