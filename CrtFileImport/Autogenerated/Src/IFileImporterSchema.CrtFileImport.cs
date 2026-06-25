namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImporterSchema

	/// <exclude/>
	public class IFileImporterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImporterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImporterSchema(IFileImporterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("fa525843-9e33-4417-81db-be5e451e98f6");
			Name = "IFileImporter";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,82,193,78,194,64,16,61,211,164,255,48,193,11,94,218,120,48,38,80,73,192,96,228,64,36,194,205,120,88,218,105,217,164,221,109,102,182,24,162,252,187,219,173,128,130,37,222,118,246,205,123,51,239,237,86,44,85,6,139,45,27,44,6,190,231,123,74,20,200,165,136,17,150,72,36,88,167,38,120,208,42,149,89,69,194,72,173,130,71,153,227,180,40,53,25,223,251,240,189,78,89,173,114,25,131,84,6,41,173,137,211,99,7,18,244,97,58,22,140,63,239,44,169,38,118,194,48,132,136,171,162,16,180,29,238,47,22,98,131,32,93,39,108,68,94,33,131,209,144,172,32,213,4,140,130,226,53,160,50,210,72,228,131,72,120,170,18,149,130,68,1,181,155,251,174,59,163,29,204,221,97,20,186,234,216,72,104,42,82,108,129,253,169,134,94,159,87,172,115,203,233,117,239,130,155,219,224,6,62,97,185,70,176,50,107,157,128,100,72,176,36,140,133,193,36,232,94,191,213,156,149,214,185,91,255,69,188,55,86,39,223,123,246,154,114,126,216,3,142,43,93,187,216,59,157,43,194,204,198,11,147,141,117,199,125,152,187,88,27,12,235,187,6,121,18,42,201,145,162,31,3,182,118,166,125,69,7,143,40,227,33,156,131,131,255,10,225,132,72,83,187,86,131,183,203,141,209,190,19,254,182,239,104,71,197,182,150,118,209,81,106,147,186,168,217,210,113,193,182,74,245,12,153,69,134,127,155,181,10,51,164,236,146,196,73,50,201,133,212,146,118,153,7,157,87,133,154,147,142,237,58,167,233,159,131,251,239,130,42,105,126,140,171,119,190,183,251,2,53,177,95,199,204,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("fa525843-9e33-4417-81db-be5e451e98f6"));
		}

		#endregion

	}

	#endregion

}

