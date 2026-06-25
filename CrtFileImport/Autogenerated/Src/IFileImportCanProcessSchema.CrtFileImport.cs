namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportCanProcessSchema

	/// <exclude/>
	public class IFileImportCanProcessSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportCanProcessSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportCanProcessSchema(IFileImportCanProcessSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0ebbb19c-387f-4ea6-9d4f-066fbfef8b64");
			Name = "IFileImportCanProcess";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,101,143,193,10,194,48,12,134,207,22,250,14,61,234,101,47,224,113,42,236,38,232,11,116,53,155,129,54,25,77,122,16,241,221,221,230,96,200,32,135,240,243,253,95,8,249,4,50,248,0,238,14,57,123,225,78,171,154,169,195,190,100,175,200,84,93,48,66,147,6,206,106,205,219,154,93,17,164,254,143,206,80,157,73,81,17,228,104,205,136,12,165,141,24,28,146,66,238,38,119,179,74,106,79,215,204,1,68,70,112,242,237,90,230,232,214,120,63,187,94,183,240,132,228,107,142,37,145,131,77,116,248,93,218,148,151,35,51,115,2,81,164,249,11,247,88,247,165,250,177,102,156,47,249,168,143,214,0,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0ebbb19c-387f-4ea6-9d4f-066fbfef8b64"));
		}

		#endregion

	}

	#endregion

}

