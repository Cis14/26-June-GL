namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: Float1ColumnProcessorSchema

	/// <exclude/>
	public class Float1ColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public Float1ColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public Float1ColumnProcessorSchema(Float1ColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("7f839451-e4a5-40f4-bb65-4692bb45345c");
			Name = "Float1ColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("aaf0cd3b-b0e9-4378-a805-7163759e3c5e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,107,227,48,16,133,207,9,244,63,12,233,37,129,98,179,215,54,9,180,9,41,185,44,133,109,122,41,61,40,242,56,21,216,146,153,145,10,161,244,191,239,88,142,219,216,245,46,244,100,107,120,243,222,204,103,217,170,18,185,82,26,225,17,137,20,187,220,39,43,103,115,115,8,164,188,113,54,217,152,2,183,101,229,200,95,140,223,47,198,163,192,198,30,224,207,145,61,150,55,159,231,243,110,194,127,213,147,141,210,222,145,65,22,133,104,46,9,15,146,1,171,66,49,95,195,166,112,202,255,90,185,34,148,246,129,156,70,102,71,81,152,166,41,204,141,125,69,50,62,115,26,52,97,190,152,68,125,79,62,73,151,173,158,67,89,42,58,182,231,91,11,198,178,87,86,150,117,57,248,87,195,160,235,96,144,23,18,10,206,178,217,23,8,185,35,168,26,191,122,133,102,42,208,49,7,222,84,17,144,147,54,35,61,11,121,94,99,174,66,225,239,140,205,164,113,234,143,21,186,124,186,237,77,56,187,130,223,66,29,22,96,229,33,130,193,181,103,179,23,177,172,194,190,48,250,52,230,160,14,78,216,190,81,27,189,71,114,95,140,101,61,79,161,230,47,168,31,162,113,163,248,41,220,111,116,99,97,69,168,60,114,151,177,16,16,37,226,185,103,127,3,49,77,62,93,211,190,237,188,82,164,202,136,106,49,9,140,36,123,88,212,245,213,156,44,119,114,150,15,211,22,146,121,26,213,177,249,132,110,48,114,186,235,24,65,215,119,38,76,247,138,113,218,47,215,215,127,244,113,194,138,54,107,200,118,49,75,70,133,228,229,138,95,215,239,94,122,49,251,31,231,59,73,26,198,92,181,237,224,222,228,55,50,25,194,125,48,25,172,149,87,79,245,53,124,20,188,187,109,6,139,101,183,150,52,75,247,117,55,131,147,55,251,116,139,31,127,1,184,243,51,147,23,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("7f839451-e4a5-40f4-bb65-4692bb45345c"));
		}

		#endregion

	}

	#endregion

}

