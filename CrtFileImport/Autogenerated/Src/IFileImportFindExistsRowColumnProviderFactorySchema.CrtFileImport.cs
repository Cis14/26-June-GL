namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportFindExistsRowColumnProviderFactorySchema

	/// <exclude/>
	public class IFileImportFindExistsRowColumnProviderFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportFindExistsRowColumnProviderFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportFindExistsRowColumnProviderFactorySchema(IFileImportFindExistsRowColumnProviderFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("2083a9a6-213a-488c-9bd5-42f89aaaf1c3");
			Name = "IFileImportFindExistsRowColumnProviderFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,165,82,203,78,195,48,16,60,55,82,255,97,85,46,112,137,239,37,68,66,21,65,57,32,85,21,63,96,156,117,176,148,172,163,181,83,168,80,255,29,231,9,237,169,208,163,119,118,103,103,102,77,178,70,215,72,133,240,138,204,210,89,237,227,141,37,109,202,150,165,55,150,226,204,84,152,215,141,101,191,140,190,150,209,50,90,220,48,150,1,129,156,60,178,14,179,107,200,127,186,50,67,197,211,167,113,222,237,236,199,198,86,109,77,91,182,123,83,32,103,82,121,203,135,158,68,8,1,137,107,235,90,242,33,29,223,59,108,24,29,146,119,32,65,15,205,160,45,131,98,12,98,168,4,67,206,75,82,232,192,234,48,142,216,65,250,97,117,217,254,149,72,227,105,181,248,181,187,105,223,42,163,2,249,232,231,175,118,22,67,46,115,48,47,232,223,109,225,214,176,237,137,7,240,220,240,232,216,179,193,125,240,35,105,54,247,95,111,125,84,181,36,89,118,81,169,30,6,234,14,60,173,43,90,238,32,29,8,193,244,140,96,27,28,14,237,226,89,165,56,151,153,48,250,150,201,165,143,215,171,140,19,49,177,117,244,151,205,193,51,250,211,202,237,221,253,24,58,82,49,228,222,191,143,195,23,61,41,30,191,1,59,110,73,44,230,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("2083a9a6-213a-488c-9bd5-42f89aaaf1c3"));
		}

		#endregion

	}

	#endregion

}

