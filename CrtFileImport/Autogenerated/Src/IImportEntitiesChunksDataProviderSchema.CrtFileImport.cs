namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IImportEntitiesChunksDataProviderSchema

	/// <exclude/>
	public class IImportEntitiesChunksDataProviderSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IImportEntitiesChunksDataProviderSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IImportEntitiesChunksDataProviderSchema(IImportEntitiesChunksDataProviderSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("2a8003b9-4985-421c-aeb6-96632952b722");
			Name = "IImportEntitiesChunksDataProvider";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,83,65,110,194,48,16,60,131,196,31,86,112,105,37,148,220,11,228,66,41,202,161,18,130,126,192,177,55,96,53,177,163,181,13,69,136,191,215,113,32,74,57,144,67,143,30,207,204,206,142,19,197,74,52,21,227,8,95,72,196,140,206,109,180,212,42,151,123,71,204,74,173,162,15,89,96,90,86,154,236,104,120,25,13,7,206,72,181,135,221,217,88,44,103,15,103,47,45,10,228,181,206,68,107,84,72,146,123,142,103,77,8,247,30,133,84,89,164,220,207,123,131,180,113,93,41,43,173,68,179,60,56,245,109,222,153,101,27,210,71,41,144,130,48,142,99,152,27,87,150,140,206,201,237,220,8,65,120,46,84,55,50,228,154,60,17,17,56,97,190,24,247,175,19,133,201,231,48,183,30,59,134,56,137,238,19,227,206,200,202,101,133,228,32,239,209,251,147,183,203,181,230,247,155,121,131,215,80,184,155,63,132,72,18,184,132,189,219,198,62,209,30,180,48,13,248,216,70,0,118,236,136,62,157,47,160,12,59,2,203,180,179,117,51,28,77,120,29,210,39,32,52,174,176,81,235,210,221,112,0,71,45,69,48,218,52,42,20,91,125,122,89,59,143,242,58,91,42,166,117,1,181,83,170,4,254,76,33,211,186,0,227,120,205,134,5,88,114,248,58,123,146,114,139,150,36,250,164,188,253,70,224,36,237,161,55,185,121,30,61,93,41,87,34,177,172,192,121,55,252,54,136,18,88,163,237,194,230,207,82,61,137,75,253,239,102,67,177,141,83,127,140,9,42,209,60,123,56,95,155,95,167,3,94,127,1,124,233,142,117,176,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("2a8003b9-4985-421c-aeb6-96632952b722"));
		}

		#endregion

	}

	#endregion

}

