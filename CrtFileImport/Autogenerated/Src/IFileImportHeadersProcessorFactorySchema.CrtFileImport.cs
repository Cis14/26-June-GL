namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportHeadersProcessorFactorySchema

	/// <exclude/>
	public class IFileImportHeadersProcessorFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportHeadersProcessorFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportHeadersProcessorFactorySchema(IFileImportHeadersProcessorFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("c0f82a9d-1f0d-4e82-bc54-fa38907c8a4f");
			Name = "IFileImportHeadersProcessorFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,145,207,106,2,49,16,198,207,10,190,195,176,189,180,80,204,189,174,123,145,218,122,40,8,214,7,72,227,236,26,112,51,203,76,114,144,210,119,111,54,254,219,109,21,132,92,50,249,230,247,77,190,113,186,70,105,180,65,248,68,102,45,84,250,241,140,92,105,171,192,218,91,114,227,185,221,225,162,110,136,61,124,143,134,131,32,214,85,61,49,227,228,70,125,252,234,188,245,22,37,10,162,164,9,95,59,107,192,58,143,92,182,150,139,11,251,29,245,6,89,150,76,6,69,136,231,218,120,226,125,107,25,59,7,15,140,85,28,6,62,208,111,105,35,47,176,76,172,195,163,82,10,114,9,117,173,121,95,156,10,51,70,237,17,182,137,11,205,137,27,133,136,96,24,203,105,246,223,62,53,17,103,170,128,50,106,133,2,199,49,197,108,177,214,112,182,82,127,189,242,70,179,174,193,197,44,167,89,16,228,152,160,67,211,198,151,21,235,120,7,115,46,116,252,215,125,165,42,114,149,64,215,185,76,228,87,105,146,172,88,117,7,235,16,83,222,251,163,234,10,143,209,7,118,82,44,156,120,237,34,129,202,123,3,201,213,169,185,165,221,84,194,27,250,243,18,31,251,63,132,126,52,207,208,29,23,46,255,123,154,28,151,142,110,115,216,123,186,255,140,134,241,252,2,24,252,141,14,177,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("c0f82a9d-1f0d-4e82-bc54-fa38907c8a4f"));
		}

		#endregion

	}

	#endregion

}

