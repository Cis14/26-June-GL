namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ImportDataChunkSchema

	/// <exclude/>
	public class ImportDataChunkSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ImportDataChunkSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ImportDataChunkSchema(ImportDataChunkSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("fd201402-92ae-464e-8ec8-0ef3cbe29b35");
			Name = "ImportDataChunk";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,187,110,194,48,20,134,103,144,120,135,35,186,86,68,109,183,18,88,40,173,144,58,32,37,91,213,193,36,135,212,34,182,35,95,134,20,241,238,245,37,129,42,80,72,151,72,62,254,143,255,239,92,194,9,67,85,145,12,33,69,41,137,18,91,61,89,8,190,165,133,145,68,83,193,39,175,180,196,21,171,132,212,163,225,126,52,28,24,69,121,1,73,173,52,178,233,104,104,35,119,18,11,171,132,37,55,236,25,130,118,241,101,248,46,209,68,163,151,84,102,83,210,12,208,42,46,8,6,238,221,65,42,214,82,100,168,20,204,224,225,222,69,86,252,20,121,244,145,230,140,185,141,60,217,192,33,0,32,207,3,195,117,158,180,174,174,226,132,251,64,243,46,196,206,84,71,148,37,215,84,215,142,227,150,235,162,36,74,181,182,47,68,19,255,180,151,68,81,4,177,50,140,17,89,207,155,179,191,5,177,133,220,74,129,250,172,73,171,141,58,226,88,91,194,138,72,194,128,219,193,205,198,233,120,30,71,199,160,147,125,36,40,41,41,233,55,217,148,248,121,170,53,115,88,93,170,56,157,183,245,182,248,182,195,21,74,77,209,214,176,246,169,30,253,140,221,7,18,59,11,151,180,202,93,5,39,248,115,250,22,227,205,208,188,161,104,146,109,238,30,10,212,83,80,238,115,248,211,44,116,42,88,117,154,117,195,207,103,246,246,241,75,9,89,59,151,224,2,215,109,186,59,221,60,210,207,208,141,3,220,16,251,54,177,179,178,224,63,255,240,234,105,147,6,117,231,97,191,44,191,86,255,226,223,112,248,1,171,74,227,87,88,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("fd201402-92ae-464e-8ec8-0ef3cbe29b35"));
		}

		#endregion

	}

	#endregion

}

