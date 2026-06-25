namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FileImportStagesEnumSchema

	/// <exclude/>
	public class FileImportStagesEnumSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FileImportStagesEnumSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FileImportStagesEnumSchema(FileImportStagesEnumSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("bb4174ad-6f70-437d-9b0f-36d3b9a6d06b");
			Name = "FileImportStagesEnum";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,205,77,10,194,48,16,134,225,117,3,185,67,14,32,5,127,182,174,66,5,119,5,189,64,12,211,18,72,50,97,102,178,146,222,221,168,11,69,187,253,222,7,190,236,18,112,113,30,204,21,136,28,227,36,189,197,60,133,185,146,147,128,185,63,133,8,231,84,144,68,171,187,86,93,169,183,24,188,129,92,147,249,180,139,184,25,120,104,99,35,79,214,141,4,197,17,252,16,115,52,219,205,59,163,7,102,139,177,166,204,255,106,247,173,134,44,65,2,172,176,253,139,89,76,37,130,172,156,29,90,94,180,90,30,213,188,204,49,232,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("bb4174ad-6f70-437d-9b0f-36d3b9a6d06b"));
		}

		#endregion

	}

	#endregion

}

