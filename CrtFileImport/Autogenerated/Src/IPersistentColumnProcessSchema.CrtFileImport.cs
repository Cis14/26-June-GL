namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IPersistentColumnProcessSchema

	/// <exclude/>
	public class IPersistentColumnProcessSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IPersistentColumnProcessSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IPersistentColumnProcessSchema(IPersistentColumnProcessSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8195325f-d746-484e-902f-eef26fde2768");
			Name = "IPersistentColumnProcess";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,93,143,177,110,132,48,12,134,103,144,120,7,139,91,218,133,236,45,199,114,106,37,134,74,12,125,129,52,24,26,137,56,145,157,156,90,157,238,221,27,160,237,192,224,193,254,253,127,246,79,218,161,4,109,16,222,145,89,139,159,98,115,241,52,217,57,177,142,214,83,243,106,23,236,93,240,28,171,242,86,20,85,89,132,244,177,88,3,150,34,242,180,90,251,1,89,172,68,164,120,241,75,114,52,176,55,40,146,119,111,185,138,19,227,156,81,240,134,241,211,143,242,4,195,70,168,202,85,84,74,65,43,201,57,205,223,221,223,224,229,11,77,138,8,87,189,36,20,8,59,207,210,220,192,191,71,29,77,109,208,172,29,80,142,116,174,237,246,242,176,78,48,255,41,117,151,175,32,130,97,156,206,117,127,84,85,215,170,205,190,209,174,222,142,240,27,226,225,184,11,71,244,227,243,158,228,132,52,238,73,115,119,175,202,251,15,10,114,114,221,93,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8195325f-d746-484e-902f-eef26fde2768"));
		}

		#endregion

	}

	#endregion

}

