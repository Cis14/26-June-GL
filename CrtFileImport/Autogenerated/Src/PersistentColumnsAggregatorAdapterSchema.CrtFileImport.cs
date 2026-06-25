namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: PersistentColumnsAggregatorAdapterSchema

	/// <exclude/>
	public class PersistentColumnsAggregatorAdapterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public PersistentColumnsAggregatorAdapterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public PersistentColumnsAggregatorAdapterSchema(PersistentColumnsAggregatorAdapterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("a19ab3c1-267e-4152-ae6e-61811f92503a");
			Name = "PersistentColumnsAggregatorAdapter";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,221,84,193,106,227,64,12,61,183,208,127,16,238,37,11,197,190,119,83,67,200,118,151,28,90,10,219,246,62,25,43,206,128,173,49,154,153,133,16,246,223,87,182,131,99,167,177,211,133,61,237,201,30,205,211,147,158,164,17,169,18,93,165,52,194,43,50,43,103,55,62,94,90,218,152,60,176,242,198,82,252,221,20,184,42,43,203,254,230,122,127,115,125,21,156,161,28,126,238,156,199,82,160,69,129,186,198,185,248,7,18,178,209,95,59,76,159,145,81,236,114,115,203,152,11,26,150,133,114,238,30,94,144,157,17,38,242,194,20,74,114,47,108,53,58,103,121,145,169,202,35,55,78,85,88,23,70,131,174,125,62,186,44,242,92,72,149,239,124,224,12,239,17,116,7,171,49,71,9,181,111,2,30,211,20,97,158,131,22,76,157,109,147,71,139,72,146,4,230,134,182,34,217,103,86,67,146,118,86,23,202,82,241,174,51,44,25,149,71,7,70,184,20,73,169,237,6,252,174,66,65,34,130,102,220,60,68,151,85,69,18,34,238,98,36,167,65,230,149,98,85,2,73,63,31,162,224,144,37,117,106,91,19,165,111,114,6,221,25,226,121,210,160,207,59,147,165,211,108,186,174,184,40,157,244,173,62,233,120,232,232,101,213,179,183,129,20,24,42,187,171,185,174,86,143,20,74,100,181,46,112,190,122,30,205,62,133,73,101,103,184,70,193,41,140,11,253,210,240,220,195,90,57,156,157,164,123,33,131,41,86,216,195,239,255,108,242,254,209,16,76,20,188,87,181,91,164,172,125,212,195,23,254,132,126,107,179,11,143,187,173,212,232,222,136,146,190,158,95,214,100,176,200,178,89,187,53,31,201,27,191,3,211,59,200,10,106,78,45,159,148,167,254,12,141,239,170,8,120,184,105,254,15,227,217,131,124,67,231,13,53,75,26,178,227,255,196,164,252,189,140,195,0,206,198,11,217,90,135,70,177,253,1,125,1,55,3,88,6,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("a19ab3c1-267e-4152-ae6e-61811f92503a"));
		}

		#endregion

	}

	#endregion

}

