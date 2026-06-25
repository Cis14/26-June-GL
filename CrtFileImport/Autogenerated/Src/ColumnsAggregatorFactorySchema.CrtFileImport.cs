namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: ColumnsAggregatorFactorySchema

	/// <exclude/>
	public class ColumnsAggregatorFactorySchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public ColumnsAggregatorFactorySchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public ColumnsAggregatorFactorySchema(ColumnsAggregatorFactorySchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("32c31bbf-f71f-44eb-9406-7b7d802e4208");
			Name = "ColumnsAggregatorFactory";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,82,77,75,195,64,16,61,183,208,255,48,228,148,128,36,119,107,133,90,173,244,82,122,208,147,136,108,147,217,184,144,236,134,153,93,164,136,255,221,73,107,212,90,22,161,144,15,118,247,205,123,111,222,172,85,45,114,167,74,132,7,36,82,236,180,207,23,206,106,83,7,82,222,56,155,47,77,131,171,182,115,228,39,227,247,201,120,20,216,216,250,8,77,56,141,236,231,75,85,122,71,6,89,16,130,41,138,2,174,140,125,69,50,190,114,37,148,132,122,150,172,22,174,9,173,229,121,93,19,214,74,10,14,101,187,164,184,30,138,56,180,173,162,221,176,22,67,13,182,104,61,131,177,30,73,247,29,104,71,61,163,242,8,229,129,17,212,55,229,64,84,252,98,122,186,69,173,66,227,111,140,173,196,124,234,119,29,58,157,70,253,100,23,176,150,188,96,6,86,126,130,140,2,179,103,161,239,194,182,49,210,100,163,152,33,6,133,75,136,234,9,71,159,248,121,177,13,242,167,176,121,165,58,201,12,238,209,159,156,165,143,140,36,23,192,98,217,79,31,194,209,50,131,189,159,81,156,243,229,43,249,13,185,18,153,29,77,247,5,70,67,122,76,149,139,250,138,151,50,173,64,120,103,213,182,193,42,77,68,125,131,196,134,189,204,246,231,230,37,217,160,60,58,17,232,167,129,111,240,83,22,243,246,199,64,118,112,246,1,216,48,254,199,190,118,246,108,129,253,151,80,250,180,145,120,122,136,188,242,124,2,53,165,181,4,143,3,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("32c31bbf-f71f-44eb-9406-7b7d802e4208"));
		}

		#endregion

	}

	#endregion

}

