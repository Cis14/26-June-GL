namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MediumTextColumnProcessorSchema

	/// <exclude/>
	public class MediumTextColumnProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MediumTextColumnProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MediumTextColumnProcessorSchema(MediumTextColumnProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("849d8e1e-95a1-4326-a3ca-a768f4bf5d14");
			Name = "MediumTextColumnProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("560ff4eb-7ab5-4d8f-8733-4031e1e3144b");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,149,147,65,79,227,48,16,133,207,69,226,63,140,202,165,149,86,201,189,180,149,160,136,85,15,32,164,109,185,32,14,174,51,41,35,37,118,118,108,163,173,16,255,157,137,211,80,82,154,149,56,197,30,61,191,55,243,217,49,170,68,87,41,141,176,66,102,229,108,238,147,133,53,57,109,3,43,79,214,36,183,84,224,178,172,44,251,243,179,183,243,179,65,112,100,182,240,103,231,60,150,151,159,251,175,167,25,251,234,201,173,210,222,50,161,19,133,104,46,24,183,146,1,139,66,57,55,129,59,204,40,148,43,252,231,23,182,8,165,121,96,171,209,57,203,81,156,166,41,76,201,188,32,147,207,172,6,205,152,207,134,39,212,195,116,222,202,93,40,75,197,187,118,127,101,128,140,243,202,200,188,54,7,255,66,14,116,157,13,178,96,1,97,141,163,77,129,144,91,134,170,241,171,167,56,52,6,58,102,193,171,42,2,186,164,205,73,191,4,61,221,96,174,66,225,175,201,100,114,120,228,119,21,218,124,180,60,234,114,252,11,238,5,62,204,192,200,71,4,189,211,143,199,207,98,91,133,77,65,122,223,110,175,22,38,112,146,223,224,45,50,60,16,151,73,61,135,250,54,4,252,67,244,110,20,63,196,252,141,115,44,44,24,149,71,215,165,45,28,68,137,184,183,236,157,65,140,147,79,231,244,216,122,90,41,86,101,132,54,27,6,135,44,163,24,212,245,91,29,206,215,178,151,43,106,11,201,52,141,234,120,120,15,176,55,118,180,238,152,65,215,123,44,100,55,202,225,232,184,92,255,19,131,247,61,93,52,89,3,184,75,91,50,42,100,47,239,126,82,175,189,156,197,236,127,184,175,37,233,7,184,111,148,87,205,147,108,40,7,67,127,101,77,25,26,79,57,33,247,240,172,218,94,192,190,202,143,42,122,248,29,40,139,126,143,181,221,74,220,214,203,12,102,243,110,45,57,80,60,214,94,158,68,209,0,234,22,223,63,0,47,128,33,33,125,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("849d8e1e-95a1-4326-a3ca-a768f4bf5d14"));
		}

		#endregion

	}

	#endregion

}

