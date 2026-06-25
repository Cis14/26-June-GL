namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: LongTextColumnProcessorSchema

	/// <exclude/>
	public class LongTextColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public LongTextColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public LongTextColumnProcessorSchema(LongTextColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("9c7c9656-e810-4ae6-bd3f-7aa194ca6b08");
			Name = "LongTextColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("560ff4eb-7ab5-4d8f-8733-4031e1e3144b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,81,75,235,64,16,133,159,43,248,31,134,250,210,130,36,239,181,45,104,69,41,136,8,183,245,69,238,195,118,51,169,11,201,110,156,157,149,91,196,255,238,100,211,120,77,109,4,159,178,59,156,61,103,230,219,141,85,37,250,74,105,132,21,18,41,239,114,78,22,206,230,102,27,72,177,113,54,185,49,5,46,203,202,17,159,158,188,157,158,12,130,55,118,11,127,118,158,177,188,248,220,127,61,77,216,87,79,110,148,102,71,6,189,40,68,115,70,184,149,12,88,20,202,251,9,220,57,187,93,225,63,94,184,34,148,246,129,156,70,239,29,69,105,154,166,48,53,246,25,201,112,230,52,104,194,124,54,60,162,30,166,243,86,238,67,89,42,218,181,251,75,11,198,122,86,86,166,117,57,240,179,241,160,235,100,144,5,9,6,103,189,217,20,8,185,35,168,26,191,122,134,182,45,208,49,9,94,85,17,208,39,109,74,250,37,230,233,26,115,21,10,190,50,54,147,163,35,222,85,232,242,209,242,160,199,241,57,220,11,120,152,129,149,143,8,122,38,31,143,255,138,105,21,54,133,209,251,86,123,148,48,129,163,228,6,111,145,222,127,210,50,35,83,168,111,65,128,63,68,231,70,241,75,192,223,8,199,194,130,80,49,250,46,103,97,32,74,196,189,101,207,4,98,155,124,250,166,135,198,211,74,145,42,35,174,217,48,120,36,25,196,162,174,95,232,112,190,150,189,92,78,91,72,166,105,84,199,195,123,120,61,161,163,117,199,10,186,206,99,161,186,81,30,71,135,229,250,63,24,188,239,201,162,205,26,184,93,210,146,81,33,177,188,245,73,189,102,57,139,217,79,168,175,36,233,23,168,175,21,171,230,41,54,132,131,53,47,178,54,25,90,54,185,65,234,161,89,181,189,128,123,149,159,83,244,112,27,76,22,253,30,107,187,149,184,173,151,25,204,230,221,90,210,50,60,84,94,28,5,209,224,233,22,223,63,0,53,24,3,3,111,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("9c7c9656-e810-4ae6-bd3f-7aa194ca6b08"));
		}

		#endregion

	}

	#endregion

}

