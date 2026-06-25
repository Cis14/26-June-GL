namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportFactorySchema

	/// <exclude/>
	public class IFileImportFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportFactorySchema(IFileImportFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("40607472-ce99-41c0-a6b6-c3ff07be295d");
			Name = "IFileImportFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,81,77,107,132,48,16,61,43,248,31,6,123,233,66,49,247,214,10,203,82,139,183,61,180,63,32,171,163,4,76,34,147,164,32,101,255,123,19,179,117,235,118,115,203,99,222,199,188,81,92,162,153,120,139,240,129,68,220,232,222,22,7,173,122,49,56,226,86,104,85,212,98,196,70,78,154,108,150,126,103,41,248,231,140,80,195,134,64,248,146,165,89,154,60,16,14,158,4,141,178,72,189,151,125,134,230,42,80,243,214,106,154,195,164,159,101,140,65,105,156,148,156,230,234,242,95,121,208,107,130,147,19,99,23,172,80,89,97,5,26,176,26,174,114,197,175,8,251,163,50,185,211,40,90,16,171,208,61,255,196,47,146,252,11,176,0,239,104,97,34,17,176,104,59,67,47,84,135,84,172,20,118,203,41,39,78,92,130,242,93,190,230,206,32,249,6,21,182,161,190,188,138,214,16,96,48,104,173,223,199,20,37,91,40,247,21,90,61,58,169,204,145,116,139,198,104,202,171,67,68,224,139,143,14,77,177,161,55,199,152,246,109,9,91,47,89,195,22,119,224,199,207,77,54,216,70,125,130,230,226,179,31,6,127,71,238,187,218,119,124,242,69,194,109,164,157,63,119,114,142,39,71,213,197,171,135,239,249,7,21,227,85,65,81,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("40607472-ce99-41c0-a6b6-c3ff07be295d"));
		}

		#endregion

	}

	#endregion

}

