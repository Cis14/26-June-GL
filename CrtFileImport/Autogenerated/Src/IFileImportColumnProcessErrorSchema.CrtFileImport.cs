namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IFileImportColumnProcessErrorSchema

	/// <exclude/>
	public class IFileImportColumnProcessErrorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IFileImportColumnProcessErrorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IFileImportColumnProcessErrorSchema(IFileImportColumnProcessErrorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("27561062-e504-4d29-b9de-042dd1879b77");
			Name = "IFileImportColumnProcessError";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,101,141,65,10,194,48,20,68,215,13,228,14,57,65,47,160,8,82,42,118,39,232,5,98,252,9,129,228,255,242,127,34,72,233,221,109,235,66,193,205,44,102,230,205,160,205,32,163,117,96,110,192,108,133,124,105,59,66,31,67,101,91,34,97,123,138,9,134,60,18,23,173,38,173,154,42,17,131,185,190,164,64,222,105,181,56,99,189,167,232,76,196,2,236,215,169,225,203,116,148,106,198,11,147,3,145,158,153,120,1,38,179,113,13,60,1,139,233,87,61,91,124,36,224,253,127,127,139,143,28,228,96,126,237,207,245,172,213,252,6,114,128,16,227,195,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("27561062-e504-4d29-b9de-042dd1879b77"));
		}

		#endregion

	}

	#endregion

}

