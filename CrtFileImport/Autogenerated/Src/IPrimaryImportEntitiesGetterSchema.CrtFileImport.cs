namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IPrimaryImportEntitiesGetterSchema

	/// <exclude/>
	public class IPrimaryImportEntitiesGetterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IPrimaryImportEntitiesGetterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IPrimaryImportEntitiesGetterSchema(IPrimaryImportEntitiesGetterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e6f63fcc-5fd6-4ac9-8d4c-acfa8b8d627e");
			Name = "IPrimaryImportEntitiesGetter";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,145,65,106,195,48,16,69,215,49,248,14,67,86,45,20,233,0,117,189,41,105,48,217,24,218,11,40,102,28,68,165,177,25,73,11,19,114,247,74,86,237,152,180,59,205,232,125,253,255,17,41,139,110,84,29,194,23,50,43,55,244,94,188,15,212,235,75,96,229,245,64,226,67,27,108,236,56,176,47,139,107,89,236,130,211,116,129,207,201,121,180,17,53,6,187,196,57,113,68,66,214,221,235,202,108,95,100,20,7,242,218,107,116,17,136,200,24,206,70,119,160,201,35,247,201,191,105,89,91,197,83,246,90,224,35,250,8,68,62,89,239,164,148,80,185,96,19,87,47,139,136,56,24,179,24,240,87,39,86,92,62,242,213,168,88,89,160,216,252,109,63,159,49,90,184,125,157,157,225,190,18,149,156,135,255,165,223,56,197,250,193,82,148,158,112,130,46,15,127,69,140,62,48,185,186,125,204,88,201,229,42,177,205,129,130,69,86,103,131,213,92,127,170,83,183,167,28,171,93,83,109,2,190,192,86,148,193,156,169,134,123,188,231,244,37,183,178,184,253,0,195,137,209,221,237,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e6f63fcc-5fd6-4ac9-8d4c-acfa8b8d627e"));
		}

		#endregion

	}

	#endregion

}

