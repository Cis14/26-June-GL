namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportServiceSchema

	/// <exclude/>
	public class IFileImportServiceSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportServiceSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportServiceSchema(IFileImportServiceSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("1053b299-f5b8-465b-9060-5fe6f2707c72");
			Name = "IFileImportService";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("39b49a3d-3e30-4c01-a6cc-3961eeed3fd5");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,229,85,77,107,194,64,16,61,71,240,63,44,122,137,32,249,1,74,47,45,88,82,72,21,35,120,16,15,107,50,73,183,77,118,211,157,77,64,74,255,123,39,217,196,42,22,47,61,169,167,176,111,222,124,188,153,221,140,228,57,96,193,35,96,43,208,154,163,74,140,247,164,100,34,210,82,115,35,148,244,102,34,3,63,47,148,54,253,222,87,191,231,148,40,100,202,194,61,26,200,189,16,116,37,34,8,84,12,217,244,146,209,91,195,142,8,68,25,106,72,41,46,99,190,52,160,19,74,61,97,254,111,146,214,169,161,110,218,3,21,100,52,143,140,251,74,213,178,7,54,56,163,15,70,91,226,23,229,46,19,17,19,93,224,63,227,58,181,136,67,21,1,152,55,21,227,132,45,26,223,38,173,179,153,23,96,213,119,153,183,13,76,26,124,89,169,15,112,173,91,93,202,98,30,174,6,99,182,132,207,18,208,204,148,206,185,33,156,168,1,32,242,20,44,228,189,160,146,99,246,168,226,125,104,246,25,156,80,14,168,183,214,188,40,32,30,215,233,28,103,73,163,81,18,225,114,212,70,186,115,50,180,86,108,231,207,66,94,65,221,10,119,52,189,94,133,221,28,17,169,108,95,38,234,32,239,25,204,153,209,61,27,125,43,128,105,251,189,230,86,92,30,118,215,141,249,238,29,232,209,220,115,35,26,237,119,125,27,172,232,27,215,159,149,185,196,128,252,105,251,28,255,21,78,45,11,174,105,127,208,110,192,155,110,71,165,68,92,223,253,127,139,31,130,140,237,154,164,211,183,93,222,71,16,33,63,188,128,44,136,62,8,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("1053b299-f5b8-465b-9060-5fe6f2707c72"));
		}

		#endregion

	}

	#endregion

}

