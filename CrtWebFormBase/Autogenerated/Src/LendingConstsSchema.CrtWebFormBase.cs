namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LendingConstsSchema

	/// <exclude/>
	public class LendingConstsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LendingConstsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LendingConstsSchema(LendingConstsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5d6b4cef-d71c-4217-89fd-d792efa796f2");
			Name = "LendingConsts";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("aa93560d-2a86-466f-a9e4-295d1f97bfe2");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,146,77,79,131,64,16,134,207,109,210,255,48,41,23,61,108,161,184,124,89,53,161,11,24,19,227,165,254,129,21,182,132,4,150,102,119,209,16,227,127,119,161,180,1,123,48,58,167,249,120,231,157,231,48,156,86,76,30,104,202,224,149,9,65,101,189,87,43,82,243,125,145,55,130,170,162,230,240,185,152,207,26,89,240,28,118,173,84,172,218,44,230,186,99,8,150,119,83,82,82,41,111,225,153,241,76,75,244,166,84,178,23,152,166,9,119,178,169,42,42,218,135,161,54,134,56,39,227,28,166,253,11,161,177,58,185,154,35,219,67,243,86,22,41,72,165,97,83,72,59,154,41,76,199,175,117,23,60,19,160,17,5,58,165,231,165,241,185,31,247,4,163,89,205,203,22,30,155,34,131,48,85,197,59,27,174,239,180,130,61,101,112,15,156,125,244,243,171,101,178,141,237,192,93,99,68,44,155,32,140,221,24,133,196,39,40,246,18,215,38,216,15,130,152,44,175,55,127,7,254,63,243,75,77,127,163,182,112,24,217,206,218,71,216,242,99,132,157,196,67,126,224,122,232,198,77,236,136,4,219,40,9,157,158,122,246,117,124,13,109,117,252,142,174,212,189,111,24,187,185,177,100,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5d6b4cef-d71c-4217-89fd-d792efa796f2"));
		}

		#endregion

	}

	#endregion

}

