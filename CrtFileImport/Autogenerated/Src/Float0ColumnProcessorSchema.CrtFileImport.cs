namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: Float0ColumnProcessorSchema

	/// <exclude/>
	public class Float0ColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public Float0ColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public Float0ColumnProcessorSchema(Float0ColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("2c781d2e-62e1-42e1-8588-5c14e1b48647");
			Name = "Float0ColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("28117f91-27b8-43f6-8b95-0e32534298ab");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,157,83,65,75,243,64,16,189,11,254,135,161,94,90,144,68,175,218,22,180,82,233,69,132,207,122,145,239,48,221,76,234,66,178,27,102,119,133,82,252,239,223,36,105,52,73,243,85,48,151,36,179,111,222,219,121,111,215,96,78,174,64,69,240,66,204,232,108,234,163,133,53,169,222,6,70,175,173,137,150,58,163,85,94,88,246,231,103,251,243,51,144,39,56,109,182,240,103,231,60,229,183,237,82,155,131,233,196,82,180,68,229,45,107,114,2,170,97,23,76,91,209,131,69,134,206,221,192,50,179,232,175,22,54,11,185,121,102,171,200,57,203,13,54,142,99,152,106,243,78,172,125,98,21,40,166,116,54,170,90,122,29,163,120,222,106,113,33,207,145,119,173,210,157,1,109,156,71,35,14,216,20,252,187,118,160,202,29,128,124,176,88,99,141,211,155,140,32,181,12,69,205,90,142,83,105,93,131,170,212,224,3,179,64,46,106,41,197,93,169,183,7,74,49,100,254,94,155,68,218,199,126,87,144,77,199,171,222,110,39,151,240,36,129,192,12,140,188,4,48,232,194,100,242,183,102,45,194,38,211,234,176,223,65,40,28,140,60,242,177,108,223,55,118,118,236,151,129,61,135,50,29,73,225,185,82,104,227,126,97,253,127,236,111,202,11,38,244,228,186,57,136,63,130,39,106,243,95,31,11,68,61,133,120,88,98,90,32,99,94,153,58,27,5,71,44,83,26,82,229,249,30,205,215,242,47,65,54,133,104,26,87,232,22,197,193,231,65,135,199,235,14,29,116,217,39,18,192,6,29,141,251,229,253,55,251,103,39,6,50,73,157,196,80,56,162,90,16,123,185,54,55,229,183,23,54,74,126,78,231,94,118,112,42,156,162,161,2,251,33,215,84,39,4,143,65,39,240,128,30,95,203,163,253,34,113,172,87,9,204,230,221,90,84,91,210,199,221,158,158,232,107,224,238,210,231,63,34,197,56,104,140,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("2c781d2e-62e1-42e1-8588-5c14e1b48647"));
		}

		#endregion

	}

	#endregion

}

