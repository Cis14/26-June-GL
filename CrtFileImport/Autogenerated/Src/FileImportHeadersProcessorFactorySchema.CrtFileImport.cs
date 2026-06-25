namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: FileImportHeadersProcessorFactorySchema

	/// <exclude/>
	public class FileImportHeadersProcessorFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public FileImportHeadersProcessorFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public FileImportHeadersProcessorFactorySchema(FileImportHeadersProcessorFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("f308309d-78f0-4eae-9765-b45ee0dd84b7");
			Name = "FileImportHeadersProcessorFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,83,77,75,195,64,16,61,71,240,63,12,57,37,80,146,187,253,0,173,86,123,17,161,245,36,30,214,205,164,93,72,118,195,236,110,37,72,255,187,155,47,107,90,66,64,33,36,187,147,121,111,222,190,153,149,44,71,93,48,142,176,69,34,166,85,106,162,165,146,169,216,89,98,70,40,25,173,68,134,235,188,80,100,224,235,250,202,179,90,200,93,47,153,112,58,16,143,30,164,17,70,160,30,76,88,49,110,20,53,25,46,39,142,99,152,105,155,231,140,202,69,183,23,114,143,36,76,162,56,112,194,116,238,175,79,146,158,144,37,72,250,133,20,71,173,21,53,124,165,31,119,104,247,231,32,18,4,158,49,173,33,85,84,113,48,131,176,175,145,80,116,200,174,90,252,171,252,219,61,166,204,102,230,78,200,196,137,15,76,89,160,74,131,241,250,225,4,158,157,177,48,7,233,62,14,50,142,8,223,93,193,194,126,100,130,183,98,71,49,55,48,174,164,238,153,123,46,172,253,167,183,151,102,117,226,47,25,150,149,227,206,249,71,52,63,100,193,171,70,114,131,38,145,87,83,6,182,183,157,64,61,57,229,134,239,49,103,64,74,153,102,25,214,231,241,188,3,163,51,200,45,237,116,229,55,126,130,11,106,67,182,82,235,162,54,71,105,2,191,159,237,79,206,224,225,180,230,21,41,4,167,106,81,211,196,57,248,155,82,111,137,73,157,213,119,194,239,100,120,132,198,146,132,101,213,176,214,159,200,29,115,214,207,31,246,116,17,92,158,162,149,114,172,223,67,5,218,201,252,11,115,69,124,172,174,219,241,27,241,134,175,204,253,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("f308309d-78f0-4eae-9765-b45ee0dd84b7"));
		}

		#endregion

	}

	#endregion

}

