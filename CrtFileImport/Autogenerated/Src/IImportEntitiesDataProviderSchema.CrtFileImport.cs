namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IImportEntitiesDataProviderSchema

	/// <exclude/>
	public class IImportEntitiesDataProviderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IImportEntitiesDataProviderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IImportEntitiesDataProviderSchema(IImportEntitiesDataProviderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("18959618-7987-4279-b581-a64c3683bed3");
			Name = "IImportEntitiesDataProvider";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,173,84,77,111,219,48,12,61,55,64,254,3,145,94,50,96,136,239,171,23,96,249,104,23,20,197,130,166,216,93,182,233,84,168,44,25,148,228,193,40,250,223,71,217,73,211,56,113,214,195,46,182,37,190,199,71,62,83,210,162,64,91,138,20,225,9,137,132,53,185,155,204,141,206,229,214,147,112,210,232,201,173,84,184,42,74,67,110,56,120,29,14,174,188,149,122,11,155,218,58,44,110,58,107,166,42,133,105,224,217,201,29,106,36,153,50,134,81,165,79,148,76,65,106,135,148,7,185,85,155,115,169,157,116,18,237,66,56,177,38,83,201,12,137,225,175,13,233,234,154,112,203,185,224,1,221,179,201,236,55,88,55,105,218,96,20,69,16,91,95,20,130,234,233,126,99,174,80,144,133,140,211,65,78,166,128,153,207,115,36,204,62,200,213,224,68,162,16,146,26,202,86,50,131,184,20,36,10,194,28,52,59,242,125,36,27,248,6,173,101,253,85,54,130,232,93,34,142,186,162,45,185,135,57,141,163,38,220,160,43,35,179,166,70,125,166,46,182,97,124,231,25,208,73,241,229,230,255,52,60,57,223,193,63,107,186,168,191,64,133,14,193,168,125,217,128,59,94,143,220,137,97,185,212,59,197,117,216,229,108,100,111,189,78,251,156,251,165,58,5,62,138,63,97,124,198,129,20,7,3,191,66,55,223,20,250,84,46,54,183,210,22,185,163,78,99,224,12,100,201,103,219,43,223,229,142,27,58,1,190,96,205,199,199,23,250,28,144,208,121,210,150,3,251,175,16,74,140,81,176,17,21,30,27,242,100,22,179,113,183,89,56,20,194,254,44,181,47,144,194,84,196,45,176,149,158,194,161,138,203,99,247,140,233,11,136,74,72,213,156,37,166,65,218,242,248,237,181,251,172,61,7,189,121,160,141,166,247,39,153,122,205,120,68,235,149,131,74,40,201,243,143,147,51,230,252,222,197,238,143,101,198,124,17,65,71,122,223,239,53,234,172,189,119,218,53,86,252,227,97,25,158,63,133,206,20,82,188,36,50,244,192,231,83,108,177,9,252,160,45,207,216,185,163,23,254,206,98,214,16,194,109,249,54,28,188,253,5,245,43,193,77,116,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("18959618-7987-4279-b581-a64c3683bed3"));
		}

		#endregion

	}

	#endregion

}

