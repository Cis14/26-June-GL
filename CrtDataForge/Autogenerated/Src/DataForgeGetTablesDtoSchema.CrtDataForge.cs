namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: DataForgeGetTablesDtoSchema

	/// <exclude/>
	public class DataForgeGetTablesDtoSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public DataForgeGetTablesDtoSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public DataForgeGetTablesDtoSchema(DataForgeGetTablesDtoSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("e6c9149a-be1b-42f5-9e34-82733d2189b1");
			Name = "DataForgeGetTablesDto";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("18d347b6-8c17-4213-92f4-7f5294bb1a8f");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,189,84,77,111,219,48,12,61,167,64,255,3,209,29,182,93,156,123,211,245,176,4,235,101,29,138,164,183,97,7,69,166,29,1,146,108,144,114,135,46,216,127,31,37,57,109,226,164,159,43,122,20,245,196,247,30,41,210,43,135,220,42,141,112,141,68,138,155,42,20,211,198,87,166,238,72,5,211,248,98,166,130,250,214,80,141,199,71,235,227,163,81,199,198,215,176,184,229,128,78,144,214,162,142,48,46,46,208,35,25,61,25,98,230,157,15,198,97,177,144,91,101,205,159,148,245,30,181,77,75,9,117,99,52,94,54,37,90,209,17,72,233,32,96,129,127,32,172,229,37,76,173,98,62,133,59,89,23,24,174,213,210,34,207,197,136,8,193,132,30,143,199,112,198,157,115,138,110,207,251,243,28,91,66,70,31,24,194,10,129,122,60,184,72,6,85,67,18,10,100,240,38,234,10,49,39,248,88,30,48,62,61,136,148,144,56,129,179,204,98,195,52,222,162,250,25,113,27,237,191,36,208,118,75,107,52,232,40,252,17,221,112,10,95,21,227,189,141,209,58,89,185,115,126,69,77,139,20,12,138,253,171,148,51,223,15,189,166,128,164,103,16,75,140,189,93,107,56,64,83,237,24,235,253,98,9,21,53,238,17,147,251,46,179,205,75,116,75,164,79,63,36,27,124,129,147,82,66,39,159,163,231,141,233,239,194,122,198,194,226,235,243,156,123,13,53,134,73,212,53,129,191,189,65,244,101,246,152,206,57,58,8,14,218,191,48,206,88,69,169,130,207,105,184,130,168,109,41,245,237,11,240,219,132,21,24,185,41,49,40,99,249,96,43,119,58,183,75,249,182,205,137,237,72,205,89,245,250,30,40,122,47,40,23,20,82,217,15,212,243,16,231,104,143,179,68,214,100,218,56,142,47,167,158,109,61,126,166,130,125,215,90,189,146,125,170,30,100,126,197,95,218,31,201,89,254,19,111,187,81,242,71,147,89,203,31,208,120,185,119,105,27,190,112,193,60,177,79,6,226,223,115,173,240,199,205,56,189,215,106,217,30,202,255,90,48,18,251,7,170,214,234,232,12,7,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("e6c9149a-be1b-42f5-9e34-82733d2189b1"));
		}

		#endregion

	}

	#endregion

}

