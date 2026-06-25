namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ISysCultureInfoProviderSchema

	/// <exclude/>
	public class ISysCultureInfoProviderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ISysCultureInfoProviderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ISysCultureInfoProviderSchema(ISysCultureInfoProviderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e8d77611-044f-49a7-9b8f-472b0b015ec7");
			Name = "ISysCultureInfoProvider";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("6d2d1275-178c-4cc9-a265-eb797a3ca54a");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,189,82,65,110,131,48,16,60,39,82,254,96,37,151,246,2,247,132,114,40,7,132,68,165,168,201,7,92,188,80,75,176,70,107,187,42,138,242,247,26,187,80,149,170,106,47,237,205,30,102,102,103,22,35,239,64,247,188,2,118,6,34,174,85,109,162,76,97,45,27,75,220,72,133,209,153,56,234,214,159,55,235,203,102,189,178,90,98,195,78,131,54,208,29,22,119,167,109,91,168,70,178,142,114,64,32,89,57,142,99,237,8,26,135,178,2,13,80,237,6,238,89,225,52,153,109,141,37,40,176,86,71,82,47,82,0,121,122,111,159,90,89,49,57,177,191,39,175,46,94,48,15,120,0,243,172,132,222,179,163,183,8,31,227,56,102,137,182,93,199,105,72,39,32,7,163,153,246,185,89,21,172,221,192,90,81,23,154,207,194,120,169,76,122,78,188,99,232,150,119,183,149,98,155,158,22,46,2,208,200,90,2,69,73,236,185,94,186,232,192,30,129,139,155,220,74,225,4,183,135,127,72,138,2,94,191,134,29,209,159,115,186,95,17,168,83,210,191,78,219,114,108,44,111,160,112,251,45,223,207,191,221,172,139,112,63,148,179,65,88,242,135,225,84,161,148,218,36,11,109,26,234,78,148,177,118,166,44,154,25,217,1,138,240,214,252,253,26,158,247,39,240,250,6,210,54,98,185,87,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e8d77611-044f-49a7-9b8f-472b0b015ec7"));
		}

		#endregion

	}

	#endregion

}

