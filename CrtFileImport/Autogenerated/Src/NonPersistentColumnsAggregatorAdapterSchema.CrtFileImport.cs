namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: NonPersistentColumnsAggregatorAdapterSchema

	/// <exclude/>
	public class NonPersistentColumnsAggregatorAdapterSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public NonPersistentColumnsAggregatorAdapterSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public NonPersistentColumnsAggregatorAdapterSchema(NonPersistentColumnsAggregatorAdapterSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0e17349a-19a3-44d5-b001-2f4514573b56");
			Name = "NonPersistentColumnsAggregatorAdapter";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,213,84,193,74,195,64,16,61,167,208,127,24,226,165,130,36,247,218,6,74,81,233,65,9,168,31,176,221,76,227,66,178,27,102,54,66,41,254,187,211,36,198,54,182,210,131,23,143,121,121,51,239,205,188,76,172,42,145,43,165,17,94,144,72,177,219,248,104,233,236,198,228,53,41,111,156,141,238,77,129,171,178,114,228,199,163,221,120,20,212,108,108,14,207,91,246,88,10,181,40,80,239,121,28,61,160,69,50,250,182,231,28,118,36,20,92,222,92,17,230,194,6,88,22,138,121,10,79,206,166,72,108,164,155,245,210,173,46,45,47,242,92,88,202,59,90,100,170,242,72,77,101,28,199,48,51,246,77,52,124,230,52,104,194,205,60,236,74,82,114,26,153,29,133,113,34,220,170,94,23,70,40,123,141,203,36,96,10,195,86,55,176,58,111,40,216,53,166,250,121,100,103,236,169,214,194,145,169,210,70,191,101,52,190,185,46,75,69,219,228,11,88,18,42,143,12,70,170,148,149,237,187,13,248,109,133,194,68,236,70,187,200,183,204,27,245,42,241,80,102,86,41,82,37,88,9,121,30,214,140,36,54,109,155,87,152,188,202,51,232,30,136,102,113,195,62,93,172,27,253,126,55,28,38,135,244,110,223,23,57,158,188,30,249,128,99,91,55,251,118,65,176,186,179,117,137,164,214,5,206,186,20,122,237,4,134,102,174,219,162,41,172,21,227,100,208,240,39,27,118,240,113,144,205,247,55,21,39,255,54,176,191,75,224,183,101,30,172,238,10,109,214,126,251,199,135,240,136,254,205,101,167,110,224,199,237,158,61,175,246,136,191,102,122,119,38,131,46,190,73,251,39,74,247,115,163,48,37,144,1,112,222,98,139,30,131,130,125,2,82,17,140,142,2,5,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0e17349a-19a3-44d5-b001-2f4514573b56"));
		}

		#endregion

	}

	#endregion

}

