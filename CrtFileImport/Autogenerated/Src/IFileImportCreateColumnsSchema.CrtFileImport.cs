namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportCreateColumnsSchema

	/// <exclude/>
	public class IFileImportCreateColumnsSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportCreateColumnsSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportCreateColumnsSchema(IFileImportCreateColumnsSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("23856807-8b4a-4f1f-8589-38bbae2e4f42");
			Name = "IFileImportCreateColumns";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,101,142,203,10,194,48,16,69,215,13,228,31,178,84,40,253,1,151,65,161,107,197,253,24,166,117,32,143,50,153,44,68,250,239,182,181,208,138,187,203,189,103,14,19,33,96,30,192,161,185,33,51,228,212,73,99,83,236,168,47,12,66,41,54,23,242,216,134,33,177,104,245,214,170,42,153,98,255,67,51,54,231,40,36,132,249,164,213,132,12,229,225,201,25,138,130,220,205,238,118,147,88,70,16,180,201,151,16,243,196,206,202,106,157,150,210,236,137,195,34,126,93,221,19,3,24,78,73,190,177,54,251,97,61,196,191,170,54,123,243,29,124,65,227,182,124,156,222,173,70,173,198,15,53,99,15,75,6,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("23856807-8b4a-4f1f-8589-38bbae2e4f42"));
		}

		#endregion

	}

	#endregion

}

