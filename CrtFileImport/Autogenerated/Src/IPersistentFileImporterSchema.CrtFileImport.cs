namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IPersistentFileImporterSchema

	/// <exclude/>
	public class IPersistentFileImporterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IPersistentFileImporterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IPersistentFileImporterSchema(IPersistentFileImporterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("ad942417-9bd3-4771-a7af-0535dd659ac6");
			Name = "IPersistentFileImporter";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,181,147,77,111,194,48,12,134,207,32,241,31,44,184,108,210,212,222,71,215,195,216,135,122,152,52,9,180,123,104,28,136,104,147,42,31,211,16,218,127,159,155,148,2,27,227,48,105,167,214,111,236,215,126,226,86,177,26,109,195,74,132,5,26,195,172,22,46,153,105,37,228,202,27,230,164,86,201,147,172,176,168,27,109,28,236,70,195,129,183,82,173,96,190,181,14,235,233,183,56,89,172,13,50,78,2,157,208,217,196,224,138,44,160,80,14,141,160,38,183,80,188,162,177,146,146,149,59,24,163,9,233,131,198,47,43,89,130,220,167,255,150,13,228,115,207,44,158,104,187,232,209,55,125,65,183,214,220,118,106,154,166,144,89,95,215,204,108,243,94,121,192,10,29,130,32,31,16,70,215,32,35,104,195,12,221,11,185,218,228,80,157,254,40,207,66,30,40,202,189,27,139,126,152,57,90,75,19,20,124,156,119,23,103,163,2,146,39,89,26,138,162,201,187,150,188,27,162,101,185,122,246,20,159,49,186,158,94,196,120,99,149,228,140,64,124,83,105,198,49,122,36,240,175,179,7,19,131,206,27,101,243,44,221,191,29,184,102,107,44,55,133,109,193,194,128,23,233,168,232,2,224,227,7,150,158,248,186,245,44,183,127,218,144,61,98,227,123,174,51,80,199,53,78,111,80,141,243,69,251,0,161,13,148,76,149,88,117,147,156,217,102,100,139,172,125,195,27,152,133,178,42,252,82,209,44,56,31,163,79,80,241,248,237,182,225,103,220,248,137,56,26,146,250,5,247,204,170,183,178,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("ad942417-9bd3-4771-a7af-0535dd659ac6"));
		}

		#endregion

	}

	#endregion

}

