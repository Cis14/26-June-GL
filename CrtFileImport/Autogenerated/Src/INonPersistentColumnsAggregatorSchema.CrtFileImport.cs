namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: INonPersistentColumnsAggregatorSchema

	/// <exclude/>
	public class INonPersistentColumnsAggregatorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public INonPersistentColumnsAggregatorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public INonPersistentColumnsAggregatorSchema(INonPersistentColumnsAggregatorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("872298ee-2333-4faf-aa08-02b8773d99f5");
			Name = "INonPersistentColumnsAggregator";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,109,142,65,10,194,64,12,69,215,22,122,135,64,183,210,3,116,167,5,97,54,210,69,47,48,214,204,48,48,77,74,146,174,138,119,119,212,186,210,101,62,239,253,31,242,51,234,226,39,132,17,69,188,114,176,182,103,10,41,174,226,45,49,181,151,148,209,205,11,139,213,213,86,87,135,70,48,150,28,28,25,74,40,102,7,174,231,188,206,164,131,240,132,170,44,117,85,192,101,189,229,52,65,250,114,224,174,76,3,138,38,53,36,219,157,83,140,165,208,27,11,116,127,137,189,244,8,238,236,21,127,172,50,180,193,227,61,216,32,221,63,207,189,206,146,61,1,65,162,222,236,221,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("872298ee-2333-4faf-aa08-02b8773d99f5"));
		}

		#endregion

	}

	#endregion

}

