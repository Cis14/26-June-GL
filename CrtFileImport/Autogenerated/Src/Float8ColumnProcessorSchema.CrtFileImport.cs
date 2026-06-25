namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: Float8ColumnProcessorSchema

	/// <exclude/>
	public class Float8ColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public Float8ColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public Float8ColumnProcessorSchema(Float8ColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("841cbf68-2096-4585-a283-015e96ff338e");
			Name = "Float8ColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("b51eda84-5cfb-4f7f-b7eb-7f869b20e7e8");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,75,195,64,16,133,207,21,252,15,67,189,180,32,201,85,180,45,104,165,210,139,8,90,47,226,97,187,153,212,129,100,55,206,238,10,69,252,239,78,54,141,54,177,10,158,186,59,188,121,111,230,203,214,168,18,93,165,52,194,3,50,43,103,115,159,204,173,201,105,19,88,121,178,38,89,80,129,203,178,178,236,143,143,222,143,143,6,193,145,217,192,253,214,121,44,47,190,238,251,221,140,191,213,147,133,210,222,50,161,19,133,104,78,24,55,146,1,243,66,57,119,14,139,194,42,127,54,183,69,40,205,29,91,141,206,89,142,194,52,77,97,66,230,5,153,124,102,53,104,198,124,58,140,250,158,124,152,206,90,189,11,101,169,120,219,222,47,13,144,113,94,25,89,214,230,224,95,200,129,174,131,65,14,44,20,172,113,180,46,16,114,203,80,53,126,245,10,205,84,160,99,14,188,169,34,160,75,218,140,116,47,228,233,26,115,21,10,127,69,38,147,198,145,223,86,104,243,209,178,55,225,248,20,110,133,58,76,193,200,143,8,14,174,61,30,63,139,101,21,214,5,233,221,152,7,117,176,195,246,131,218,224,61,146,251,102,44,235,121,14,53,127,65,125,23,141,27,197,127,225,254,160,27,11,115,70,229,209,117,25,11,1,81,34,238,123,246,55,16,211,228,203,53,237,219,78,42,197,170,140,168,166,195,224,144,101,15,131,186,126,154,195,217,74,238,242,97,218,66,50,73,163,58,54,239,208,29,140,28,173,58,70,208,245,29,11,211,181,114,56,234,151,235,231,63,248,216,97,69,147,53,100,187,152,37,163,66,246,242,196,207,235,179,151,94,204,254,226,124,37,73,255,192,124,173,188,106,30,97,67,55,24,122,149,51,101,104,60,229,132,252,11,203,170,157,5,236,155,252,39,69,15,55,129,178,232,247,88,219,61,136,219,106,153,193,116,214,173,37,13,193,190,238,226,32,134,6,78,183,248,241,9,117,95,2,0,100,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("841cbf68-2096-4585-a283-015e96ff338e"));
		}

		#endregion

	}

	#endregion

}

