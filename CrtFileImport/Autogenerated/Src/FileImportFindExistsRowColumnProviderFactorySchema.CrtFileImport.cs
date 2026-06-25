namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FileImportFindExistsRowColumnProviderFactorySchema

	/// <exclude/>
	public class FileImportFindExistsRowColumnProviderFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FileImportFindExistsRowColumnProviderFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FileImportFindExistsRowColumnProviderFactorySchema(FileImportFindExistsRowColumnProviderFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a04f7c20-82c6-48ee-8202-f1192dbd1806");
			Name = "FileImportFindExistsRowColumnProviderFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,147,79,107,194,64,16,197,207,17,252,14,139,189,68,40,241,238,191,130,214,20,15,45,210,218,83,41,101,205,78,116,33,238,134,217,141,85,196,239,222,137,27,107,163,88,12,158,66,118,39,191,247,222,204,68,241,37,152,148,71,192,166,128,200,141,142,109,48,212,42,150,243,12,185,149,90,5,161,76,96,188,76,53,218,122,109,91,175,121,153,145,106,94,170,70,232,92,56,15,66,30,89,141,18,12,85,80,205,29,194,156,152,108,152,112,99,218,236,136,14,165,18,163,181,52,214,188,234,239,161,78,178,165,154,160,94,73,1,232,16,155,253,247,173,86,139,117,165,90,0,74,43,116,196,34,132,184,215,168,130,105,180,250,196,249,120,132,152,103,137,29,80,61,185,246,237,38,5,29,251,227,42,164,230,61,129,188,23,234,31,235,49,69,15,2,84,250,190,249,73,128,52,155,37,146,130,228,13,169,212,15,214,102,149,236,146,214,118,223,195,223,33,132,18,18,65,83,152,160,92,113,11,238,50,117,47,12,129,11,173,146,13,123,55,128,180,16,10,162,124,27,216,87,86,122,239,20,72,80,194,81,203,18,84,104,44,102,185,129,92,104,159,181,208,113,185,171,36,240,79,172,148,157,52,89,190,156,158,119,98,144,102,115,230,216,243,118,255,219,126,6,187,208,226,196,241,237,187,23,60,129,45,95,184,109,60,52,227,186,121,178,51,138,127,200,142,96,51,60,155,81,46,59,54,33,112,186,132,145,226,179,4,132,223,160,102,78,97,93,144,204,84,191,1,199,104,81,104,66,164,81,152,177,58,26,106,52,247,10,222,131,251,119,255,36,234,94,178,61,216,28,5,14,86,251,228,149,27,135,242,174,203,235,138,219,149,116,233,119,22,69,178,27,148,255,89,21,119,90,62,220,253,0,158,74,123,210,76,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a04f7c20-82c6-48ee-8202-f1192dbd1806"));
		}

		#endregion

	}

	#endregion

}

