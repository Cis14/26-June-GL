namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ISysCultureInfoSchema

	/// <exclude/>
	public class ISysCultureInfoSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ISysCultureInfoSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ISysCultureInfoSchema(ISysCultureInfoSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ea1fdfd0-6ef8-44d6-b478-24f2418a19b8");
			Name = "ISysCultureInfo";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("6d2d1275-178c-4cc9-a265-eb797a3ca54a");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,82,77,75,196,48,16,61,167,176,255,33,176,247,246,190,43,130,244,32,5,145,5,23,239,217,102,26,3,237,164,76,18,113,89,252,239,78,91,187,84,165,213,122,40,164,51,239,99,38,47,168,26,240,173,42,65,30,129,72,121,87,133,52,119,88,89,19,73,5,235,48,61,146,66,95,247,231,77,114,217,36,34,122,139,70,62,157,125,128,102,191,73,184,178,37,48,220,150,5,6,160,138,197,118,178,224,126,30,235,16,9,10,172,92,15,107,227,169,182,165,180,35,234,39,72,92,122,224,85,240,64,174,5,10,22,252,78,30,122,246,208,207,178,76,222,248,216,52,138,206,183,99,97,152,72,150,131,160,180,26,48,216,202,2,165,87,74,54,229,220,71,171,101,161,101,183,147,16,6,194,190,59,188,47,56,60,40,52,81,153,191,106,143,240,255,121,148,78,195,140,186,15,212,69,240,200,217,173,80,254,126,63,168,225,109,198,128,35,226,48,185,191,66,126,242,78,120,246,58,54,40,145,7,92,94,97,66,202,123,206,202,157,158,129,56,134,114,165,235,148,245,155,237,39,165,240,249,11,39,3,122,9,47,68,247,157,156,171,25,127,87,6,251,58,35,186,5,212,195,19,239,255,135,234,215,34,215,62,0,132,164,195,8,156,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ea1fdfd0-6ef8-44d6-b478-24f2418a19b8"));
		}

		#endregion

	}

	#endregion

}

