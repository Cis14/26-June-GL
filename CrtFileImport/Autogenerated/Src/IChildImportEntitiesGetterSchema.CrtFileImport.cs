namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: IChildImportEntitiesGetterSchema

	/// <exclude/>
	public class IChildImportEntitiesGetterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public IChildImportEntitiesGetterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public IChildImportEntitiesGetterSchema(IChildImportEntitiesGetterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("62efa7fa-c711-4c24-a8e7-b07f25c59080");
			Name = "IChildImportEntitiesGetter";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,93,144,193,110,195,32,12,134,207,141,148,119,64,61,109,23,120,128,177,92,162,182,202,109,82,247,2,148,57,29,18,56,145,13,135,104,234,187,23,146,37,237,118,3,251,251,253,97,208,4,224,209,88,16,159,64,100,120,232,163,108,7,236,221,53,145,137,110,64,121,116,30,186,48,14,20,235,234,167,174,118,137,29,94,197,121,226,8,33,163,222,131,45,28,203,19,32,144,179,111,27,243,60,145,64,30,48,186,232,128,51,144,145,49,93,188,179,194,97,4,234,139,191,107,191,157,255,90,76,43,122,130,152,219,153,46,226,157,82,74,104,78,33,24,154,154,181,144,17,22,182,68,5,252,166,228,6,171,255,180,30,13,153,32,48,111,253,190,159,207,144,5,188,111,22,175,120,148,164,86,243,229,17,37,136,137,144,155,246,175,76,171,181,81,200,238,128,41,0,153,139,7,61,111,49,53,229,137,47,203,252,143,109,252,147,233,181,252,216,173,174,110,119,67,5,57,140,140,1,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("62efa7fa-c711-4c24-a8e7-b07f25c59080"));
		}

		#endregion

	}

	#endregion

}

