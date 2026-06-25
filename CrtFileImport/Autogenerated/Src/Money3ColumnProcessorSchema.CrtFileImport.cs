namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: Money3ColumnProcessorSchema

	/// <exclude/>
	public class Money3ColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public Money3ColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public Money3ColumnProcessorSchema(Money3ColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("65c6a8d9-89f7-4001-b8eb-87947a9104be");
			Name = "Money3ColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,84,193,78,227,48,16,189,35,241,15,163,114,105,37,148,28,246,86,218,74,219,162,162,30,88,33,45,221,11,226,224,58,147,98,41,25,103,199,54,82,85,241,239,59,73,26,72,66,22,16,185,196,25,191,121,111,252,94,18,82,57,186,66,105,132,123,100,86,206,166,62,90,89,74,205,62,176,242,198,82,180,54,25,110,242,194,178,63,63,59,158,159,129,92,193,25,218,195,239,131,243,152,95,181,75,109,14,198,15,182,162,181,210,222,178,65,39,160,26,118,193,184,23,61,88,101,202,185,41,220,90,194,195,143,149,205,66,78,119,108,53,58,103,185,193,198,113,12,51,67,79,200,198,39,86,131,102,76,231,163,117,102,149,239,117,140,226,69,171,197,133,60,87,124,104,149,126,18,24,114,94,145,56,96,83,240,79,198,129,46,39,0,89,176,88,99,201,153,93,134,144,90,134,162,102,45,143,83,141,7,186,18,131,103,149,5,116,81,75,40,238,42,61,92,99,170,66,230,151,134,18,233,30,251,67,129,54,29,111,122,195,78,46,225,151,228,1,115,32,185,9,96,208,132,201,228,177,102,45,194,46,51,250,52,238,32,20,166,48,100,74,221,126,108,220,236,184,47,231,245,28,202,112,36,132,187,74,161,141,251,134,243,255,113,191,41,175,24,149,71,215,141,65,252,17,60,226,137,191,58,220,123,254,168,39,16,15,43,204,10,197,42,175,60,157,143,130,67,150,67,18,234,242,237,30,45,182,242,44,57,54,133,104,22,87,232,22,197,201,230,65,131,199,219,14,29,116,217,39,226,255,78,57,28,247,203,199,55,246,151,78,10,72,73,29,196,80,54,162,90,32,123,249,104,166,229,218,11,27,38,159,135,179,148,9,190,153,205,181,242,170,126,187,235,72,2,153,191,178,54,9,146,55,169,65,254,66,2,69,51,41,216,103,249,7,72,47,220,4,147,84,220,127,74,234,123,97,222,110,18,152,47,186,181,168,118,188,143,187,250,216,176,87,63,187,91,47,255,0,156,127,150,170,233,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("65c6a8d9-89f7-4001-b8eb-87947a9104be"));
		}

		#endregion

	}

	#endregion

}

